using Anathema.Source.Engine;
using Anathema.Source.Engine.OperatingSystems;
using Anathema.Source.Engine.Processes;
using Anathema.Source.Prefilter;
using Anathema.Source.Scanners.ValueCollector;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Anathema.Source.Utils.Snapshots
{
    class SnapshotManager : ISnapshotManagerModel
    {
        // Singleton instance of Snapshot Manager
        private static Lazy<SnapshotManager> SnapshotManagerInstance = new Lazy<SnapshotManager>(() => { return new SnapshotManager(); }, LazyThreadSafetyMode.PublicationOnly);

        // Lock to ensure multiple entities do not try and update the snapshot list at the same time
        private Object AccessLock;

        private EngineCore EngineCore;
        private Stack<Snapshot> Snapshots;          // Snapshots being managed
        private Stack<Snapshot> DeletedSnapshots;   // Deleted snapshots for the capability of redoing after undo

        // Event stubs
        public event SnapshotManagerEventHandler UpdateSnapshotCount;

        private SnapshotManager()
        {
            AccessLock = new Object();
            Snapshots = new Stack<Snapshot>();
            DeletedSnapshots = new Stack<Snapshot>();

            InitializeProcessObserver();
        }

        public void OnGUIOpen() { }

        public static SnapshotManager GetInstance()
        {
            return SnapshotManagerInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="CreateIfNone"></param>
        /// <returns></returns>
        public Snapshot GetActiveSnapshot(Boolean CreateIfNone = true)
        {
            using (TimedLock.Lock(AccessLock))
            {
                // Take a snapshot if there are none, or the current one is empty
                if (Snapshots.Count == 0 || Snapshots.Peek() == null || Snapshots.Peek().GetElementCount() == 0)
                {
                    if (CreateIfNone)
                        return CollectSnapshot();
                    else
                        return null;
                }

                // Return the snapshot
                return Snapshots.Peek();
            }
        }

        public IEnumerable<NormalizedRegion> CollectSnapshotRegions(Boolean UseSettings = true)
        {
            IntPtr StartAddress;
            IntPtr EndAddress;

            MemoryProtectionEnum RequiredPageFlags;
            MemoryProtectionEnum ExcludedPageFlags;
            MemoryTypeEnum AllowedTypeFlags;

            // Use settings parameters
            if (UseSettings)
            {
                RequiredPageFlags = Settings.GetInstance().GetRequiredProtectionSettings();
                ExcludedPageFlags = Settings.GetInstance().GetExcludedProtectionSettings();
                AllowedTypeFlags = Settings.GetInstance().GetAllowedTypeSettings();

                if (Settings.GetInstance().GetIsUserMode())
                {
                    StartAddress = IntPtr.Zero;
                    EndAddress = IntPtr.Zero.MaxUserMode(EngineCore.Memory.IsProcess32Bit());
                }
                else
                {
                    StartAddress = Settings.GetInstance().GetStartAddress().ToIntPtr();
                    EndAddress = Settings.GetInstance().GetEndAddress().ToIntPtr();
                }
            }
            // Standard pointer scan parameters
            else
            {
                StartAddress = IntPtr.Zero;
                EndAddress = IntPtr.Zero.MaxUserMode(EngineCore.Memory.IsProcess32Bit());
                RequiredPageFlags = 0;
                ExcludedPageFlags = 0;
                AllowedTypeFlags = MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped;
            }

            // Collect virtual pages
            List<NormalizedRegion> VirtualPages = new List<NormalizedRegion>();
            foreach (NormalizedRegion Page in EngineCore.Memory.GetVirtualPages(RequiredPageFlags, ExcludedPageFlags, AllowedTypeFlags, StartAddress, EndAddress))
                VirtualPages.Add(Page);

            return VirtualPages;
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
        public Snapshot CollectSnapshot(Boolean UseSettings = true, Boolean UsePrefilter = true)
        {
            if (EngineCore == null)
                return new Snapshot<Null>();

            if (UsePrefilter)
                return SnapshotPrefilterFactory.GetSnapshotPrefilter(typeof(LinkedListSnapshotPrefilter)).GetPrefilteredSnapshot();

            IEnumerable<NormalizedRegion> VirtualPages = CollectSnapshotRegions(UseSettings);

            // Convert each virtual page to a snapshot region (a more condensed representation of the information)
            List<SnapshotRegion> MemoryRegions = new List<SnapshotRegion>();
            VirtualPages.ForEach(X => MemoryRegions.Add(new SnapshotRegion<Null>(X.BaseAddress, X.RegionSize)));

            return new Snapshot<Null>(MemoryRegions);
        }

        /// <summary>
        /// Runs a value collector scan, which will gather all values to be used in subsequent scans
        /// </summary>
        public void CollectValues()
        {
            ValueCollector ValueCollector = new ValueCollector();
            ValueCollector.Begin();
        }

        /// <summary>
        /// Creates a new empty snapshot, which becomes the new active snapshot
        /// </summary>
        public void CreateNewSnapshot()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Snapshots.Count != 0 && Snapshots.Peek() == null)
                    return;
            }

            ClearSnapshots();

            SaveSnapshot(null);
        }

        /// <summary>
        /// Reverses an undo action
        /// </summary>
        public void RedoSnapshot()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (DeletedSnapshots.Count == 0)
                    return;

                Snapshots.Push(DeletedSnapshots.Pop());
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Undoes the current active snapshot, reverting to the previous snapshot
        /// </summary>
        public void UndoSnapshot()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Snapshots.Count == 0)
                    return;

                DeletedSnapshots.Push(Snapshots.Pop());

                if (DeletedSnapshots.Peek() == null)
                    DeletedSnapshots.Pop();
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Clears all snapshot records
        /// </summary>
        public void ClearSnapshots()
        {
            using (TimedLock.Lock(AccessLock))
            {
                Snapshots.Clear();
                DeletedSnapshots.Clear();
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Saves a new snapshot, which becomes the current active snapshot
        /// </summary>
        /// <param name="Snapshot"></param>
        public void SaveSnapshot(Snapshot Snapshot)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Snapshot != null)
                    Snapshot.SetTimeStampToNow();

                if (Snapshots.Count != 0 && Snapshots.Peek() == null)
                    Snapshots.Pop();

                Snapshots.Push(Snapshot);

                DeletedSnapshots.Clear();
            }

            UpdateDisplay();
        }

        public Snapshot GetSnapshotAtIndex(Int32 Index)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (Index < Snapshots.Count)
                {
                    if (Index < Snapshots.Count)
                        return Snapshots.Reverse().ElementAt(Index);
                }
                else
                {
                    Index -= Snapshots.Count;
                    if (Index < DeletedSnapshots.Count)
                        return DeletedSnapshots.ElementAt(Index);
                }
            }
            return null;
        }

        /// <summary>
        /// Current solution to modifying a snapshot not through the manager and displaying the results
        /// </summary>
        public void ForceRefresh()
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Updates display with current snapshot information
        /// </summary>
        private void UpdateDisplay()
        {
            SnapshotManagerEventArgs SnapshotManagerEventArgs = new SnapshotManagerEventArgs();

            using (TimedLock.Lock(AccessLock))
            {
                SnapshotManagerEventArgs.DeletedSnapshotCount = DeletedSnapshots.Count;
                SnapshotManagerEventArgs.SnapshotCount = Snapshots.Count;
            }

            UpdateSnapshotCount?.Invoke(this, SnapshotManagerEventArgs);
        }

    } // End class

} // End namespace