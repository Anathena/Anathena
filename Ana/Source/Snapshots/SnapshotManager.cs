namespace Ana.Source.Snapshots
{
    using Engine;
    using Engine.OperatingSystems;
    using Prefilter;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using UserSettings;
    using Utils;
    using Utils.Extensions;

    /// <summary>
    /// Manages snapshots of memory taken from the target process
    /// </summary>
    internal class SnapshotManager
    {
        /// <summary>
        /// Singleton instance of Snapshot Manager
        /// </summary>
        private static Lazy<SnapshotManager> snapshotManagerInstance = new Lazy<SnapshotManager>(
            () => { return new SnapshotManager(); },
            LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SnapshotManager" /> class from being created
        /// </summary>
        private SnapshotManager()
        {
            this.AccessLock = new Object();
            this.ObserverLock = new Object();
            this.Snapshots = new Stack<Snapshot>();
            this.DeletedSnapshots = new Stack<Snapshot>();
            this.SnapshotObservers = new List<ISnapshotObserver>();
        }

        /// <summary>
        /// Gets or sets a lock to ensure multiple entities do not try and update the snapshot list at the same time
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Gets or sets a lock to ensure multiple entities do not try and update the snapshot list at the same time
        /// </summary>
        private Object ObserverLock { get; set; }

        /// <summary>
        /// Gets or sets the snapshots being managed
        /// </summary>
        private Stack<Snapshot> Snapshots { get; set; }

        /// <summary>
        /// Gets or sets the deleted snapshots for the capability of redoing after undo
        /// </summary>
        private Stack<Snapshot> DeletedSnapshots { get; set; }

        /// <summary>
        /// Gets or sets objects observing changes in the active snapshot
        /// </summary>
        private List<ISnapshotObserver> SnapshotObservers { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SnapshotManager"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SnapshotManager GetInstance()
        {
            return SnapshotManager.snapshotManagerInstance.Value;
        }

        /// <summary>
        /// Subscribes the given object to changes in the active snapshot
        /// </summary>
        /// <param name="snapshotObserver">The object to observe active snapshot changes</param>
        public void Subscribe(ISnapshotObserver snapshotObserver)
        {
            lock (this.ObserverLock)
            {
                if (!this.SnapshotObservers.Contains(snapshotObserver))
                {
                    this.SnapshotObservers.Add(snapshotObserver);
                    snapshotObserver.Update(this.GetActiveSnapshot());
                }
            }
        }

        /// <summary>
        /// Unsubscribes the given object from changes in the active snapshot
        /// </summary>
        /// <param name="snapshotObserver">The object to observe active snapshot changes</param>
        public void Unsubscribe(ISnapshotObserver snapshotObserver)
        {
            lock (this.ObserverLock)
            {
                if (this.SnapshotObservers.Contains(snapshotObserver))
                {
                    this.SnapshotObservers.Remove(snapshotObserver);
                }
            }
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="createIfNone">Creates a snapshot if none exists</param>
        /// <returns>The current active snapshot of memory in the target process</returns>
        public Snapshot GetActiveSnapshot(Boolean createIfNone = true)
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                // Take a snapshot if there are none, or the current one is empty
                if (this.Snapshots.Count == 0 || this.Snapshots.Peek() == null || this.Snapshots.Peek().GetElementCount() == 0)
                {
                    if (createIfNone)
                    {
                        return this.CollectSnapshot();
                    }
                    else
                    {
                        return null;
                    }
                }

                // Return the snapshot
                return this.Snapshots.Peek();
            }
        }

        /// <summary>
        /// Collects all snapshot regions in the target process
        /// </summary>
        /// <param name="useSettings">Whether or not to apply user settings to the query</param>
        /// <returns>Regions of memory in the target process</returns>
        public IEnumerable<NormalizedRegion> CollectSnapshotRegions(Boolean useSettings = true)
        {
            IntPtr startAddress;
            IntPtr endAddress;

            MemoryProtectionEnum requiredPageFlags;
            MemoryProtectionEnum excludedPageFlags;
            MemoryTypeEnum allowedTypeFlags;

            if (useSettings)
            {
                // Use settings parameters
                requiredPageFlags = SettingsViewModel.GetInstance().GetRequiredProtectionSettings();
                excludedPageFlags = SettingsViewModel.GetInstance().GetExcludedProtectionSettings();
                allowedTypeFlags = SettingsViewModel.GetInstance().GetAllowedTypeSettings();

                if (SettingsViewModel.GetInstance().GetIsUserMode())
                {
                    startAddress = IntPtr.Zero;
                    endAddress = EngineCore.GetInstance().OperatingSystemAdapter.GetMaximumUserModeAddress();
                }
                else
                {
                    startAddress = SettingsViewModel.GetInstance().GetStartAddress().ToIntPtr();
                    endAddress = SettingsViewModel.GetInstance().GetEndAddress().ToIntPtr();
                }
            }
            else
            {
                // Standard pointer scan parameters
                startAddress = IntPtr.Zero;
                endAddress = EngineCore.GetInstance().OperatingSystemAdapter.GetMaximumUserModeAddress();
                requiredPageFlags = 0;
                excludedPageFlags = 0;
                allowedTypeFlags = MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped;
            }

            // Collect virtual pages
            List<NormalizedRegion> virtualPages = new List<NormalizedRegion>();
            foreach (NormalizedRegion page in EngineCore.GetInstance().OperatingSystemAdapter.GetVirtualPages(
                    requiredPageFlags,
                    excludedPageFlags,
                    allowedTypeFlags,
                    startAddress,
                    endAddress))
            {
                virtualPages.Add(page);
            }

            return virtualPages;
        }

        /// <summary>
        /// Collects a new snapshot of memory in the target process
        /// </summary>
        /// <param name="useSettings">Whether or not to apply user settings to the query</param>
        /// <param name="usePrefilter">Whether or not to apply the active prefilter to the query</param>
        /// <returns>The snapshot of memory taken in the target process</returns>
        public Snapshot CollectSnapshot(Boolean useSettings = true, Boolean usePrefilter = true)
        {
            if (usePrefilter)
            {
                return SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(ShallowPointerPrefilter)).GetPrefilteredSnapshot();
            }

            IEnumerable<NormalizedRegion> virtualPages = this.CollectSnapshotRegions(useSettings);

            // Convert each virtual page to a snapshot region (a more condensed representation of the information)
            List<SnapshotRegion> memoryRegions = new List<SnapshotRegion>();
            virtualPages.ForEach(x => memoryRegions.Add(new SnapshotRegion<Null>(x.BaseAddress, x.RegionSize)));

            return new Snapshot<Null>(memoryRegions);
        }

        /// <summary>
        /// Creates a new empty snapshot, which becomes the new active snapshot
        /// </summary>
        public void CreateNewSnapshot()
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (this.Snapshots.Count != 0 && this.Snapshots.Peek() == null)
                {
                    return;
                }
            }

            this.ClearSnapshots();
            this.SaveSnapshot(null);
        }

        /// <summary>
        /// Reverses an undo action
        /// </summary>
        public void RedoSnapshot()
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (this.DeletedSnapshots.Count == 0)
                {
                    return;
                }

                this.Snapshots.Push(this.DeletedSnapshots.Pop());
                this.NotifyObservers();
            }
        }

        /// <summary>
        /// Undoes the current active snapshot, reverting to the previous snapshot
        /// </summary>
        public void UndoSnapshot()
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (this.Snapshots.Count == 0)
                {
                    return;
                }

                this.DeletedSnapshots.Push(this.Snapshots.Pop());

                if (this.DeletedSnapshots.Peek() == null)
                {
                    this.DeletedSnapshots.Pop();
                }

                this.NotifyObservers();
            }
        }

        /// <summary>
        /// Clears all snapshot records
        /// </summary>
        public void ClearSnapshots()
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                this.Snapshots.Clear();
                this.DeletedSnapshots.Clear();
                this.NotifyObservers();
            }
        }

        /// <summary>
        /// Saves a new snapshot, which becomes the current active snapshot
        /// </summary>
        /// <param name="snapshot">The snapshot to save</param>
        public void SaveSnapshot(Snapshot snapshot)
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (this.Snapshots.Count != 0 && this.Snapshots.Peek() == null)
                {
                    this.Snapshots.Pop();
                }

                this.Snapshots.Push(snapshot);
                this.DeletedSnapshots.Clear();
                this.NotifyObservers();
            }
        }

        /// <summary>
        /// Notify all observing objects of an active snapshot change
        /// </summary>
        private void NotifyObservers()
        {
            lock (this.ObserverLock)
            {
                Snapshot activeSnapshot = this.GetActiveSnapshot(createIfNone: false);
                foreach (ISnapshotObserver observer in this.SnapshotObservers)
                {
                    observer.Update(activeSnapshot);
                }
            }
        }
    }
    //// End class
}
//// End namespace