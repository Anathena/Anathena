using Ana.Source.Engine;
using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Snapshots.Prefilter;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using Ana.Source.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ana.Source.Snapshots
{
    /// <summary>
    /// Manages snapshots of memory taken from the target process
    /// </summary>
    internal class SnapshotManager
    {
        /// <summary>
        /// Singleton instance of Snapshot Manager
        /// </summary>
        private static Lazy<SnapshotManager> SnapshotManagerInstance = new Lazy<SnapshotManager>(
            () => { return new SnapshotManager(); },
            LazyThreadSafetyMode.PublicationOnly);

        private List<ISnapshotObserver> snapshotObservers;

        private SnapshotManager()
        {
            this.AccessLock = new Object();
            this.ObserverLock = new Object();
            this.Snapshots = new Stack<Snapshot>();
            this.DeletedSnapshots = new Stack<Snapshot>();
            this.snapshotObservers = new List<ISnapshotObserver>();
        }

        /// <summary>
        /// Lock to ensure multiple entities do not try and update the snapshot list at the same time
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Lock to ensure multiple entities do not try and update the snapshot list at the same time
        /// </summary>
        private Object ObserverLock { get; set; }

        /// <summary>
        /// Gets or sets the snapshots being managed
        /// </summary>
        private Stack<Snapshot> Snapshots { get; set; }

        /// <summary>
        /// Gets or sets the deleted snapshots for the capability of redoing after undo
        /// </summary>
        private Stack<Snapshot> DeletedSnapshots;

        public static SnapshotManager GetInstance()
        {
            return SnapshotManager.SnapshotManagerInstance.Value;
        }

        public void Subscribe(ISnapshotObserver snapshotObserver)
        {
            lock (ObserverLock)
            {
                if (!snapshotObservers.Contains(snapshotObserver))
                {
                    snapshotObservers.Add(snapshotObserver);
                    snapshotObserver.Update(GetActiveSnapshot());
                }
            }
        }

        public void Unsubscribe(ISnapshotObserver snapshotObserver)
        {
            lock (ObserverLock)
            {
                if (snapshotObservers.Contains(snapshotObserver))
                {
                    snapshotObservers.Remove(snapshotObserver);
                }
            }
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="createIfNone"></param>
        /// <returns></returns>
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

        public IEnumerable<NormalizedRegion> CollectSnapshotRegions(Boolean useSettings = true)
        {
            IntPtr startAddress;
            IntPtr endAddress;

            MemoryProtectionEnum requiredPageFlags;
            MemoryProtectionEnum excludedPageFlags;
            MemoryTypeEnum allowedTypeFlags;

            // Use settings parameters
            if (useSettings)
            {
                requiredPageFlags = Settings.GetInstance().GetRequiredProtectionSettings();
                excludedPageFlags = Settings.GetInstance().GetExcludedProtectionSettings();
                allowedTypeFlags = Settings.GetInstance().GetAllowedTypeSettings();

                if (Settings.GetInstance().GetIsUserMode())
                {
                    startAddress = IntPtr.Zero;
                    endAddress = EngineCore.GetInstance().OperatingSystemAdapter.GetMaximumUserModeAddress();
                }
                else
                {
                    startAddress = Settings.GetInstance().GetStartAddress().ToIntPtr();
                    endAddress = Settings.GetInstance().GetEndAddress().ToIntPtr();
                }
            }
            // Standard pointer scan parameters
            else
            {
                startAddress = IntPtr.Zero;
                endAddress = EngineCore.GetInstance().OperatingSystemAdapter.GetMaximumUserModeAddress();
                requiredPageFlags = 0;
                excludedPageFlags = 0;
                allowedTypeFlags = MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped;
            }

            // Collect virtual pages
            List<NormalizedRegion> virtualPages = new List<NormalizedRegion>();
            foreach (NormalizedRegion Page in EngineCore.GetInstance().OperatingSystemAdapter.GetVirtualPages(
                    requiredPageFlags,
                    excludedPageFlags,
                    allowedTypeFlags,
                    startAddress,
                    endAddress))
            {
                virtualPages.Add(Page);
            }

            return virtualPages;
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
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
        /// <param name="snapshot"></param>
        public void SaveSnapshot(Snapshot snapshot)
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (snapshot != null)
                {
                    snapshot.SetTimeStampToNow();
                }

                if (this.Snapshots.Count != 0 && this.Snapshots.Peek() == null)
                {
                    this.Snapshots.Pop();
                }

                this.Snapshots.Push(snapshot);
                this.DeletedSnapshots.Clear();
                this.NotifyObservers();
            }
        }

        public Snapshot GetSnapshotAtIndex(Int32 index)
        {
            using (TimedLock.Lock(this.AccessLock))
            {
                if (index < this.Snapshots.Count)
                {
                    if (index < this.Snapshots.Count)
                    {
                        return this.Snapshots.Reverse().ElementAt(index);
                    }
                }
                else
                {
                    index -= this.Snapshots.Count;
                    if (index < this.DeletedSnapshots.Count)
                    {
                        return this.DeletedSnapshots.ElementAt(index);
                    }
                }
            }

            return null;
        }

        private void NotifyObservers()
        {
            lock (ObserverLock)
            {
                Snapshot activeSnapshot = GetActiveSnapshot();
                foreach (ISnapshotObserver observer in snapshotObservers)
                {
                    observer.Update(activeSnapshot);
                }
            }
        }
    }
    //// End class
}
//// End namespace