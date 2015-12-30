using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class SnapshotManager : ISnapshotManagerModel
    {
        // Singleton class instance
        private static SnapshotManager SnapshotManagerInstance;

        private MemorySharp MemoryEditor;

        private List<Snapshot> SnapshotList;    // Snapshots being managed
        private Snapshot ActiveSnapshot;        // Reference to the active snapshot being used by Anathema
        
        public event SnapshotManagerEventHandler UpdateSnapshotDisplay;

        private SnapshotManager()
        {
            SnapshotList = new List<Snapshot>();
            ActiveSnapshot = null;

            InitializeObserver();
        }

        public static SnapshotManager GetInstance()
        {
            if (SnapshotManagerInstance == null)
                SnapshotManagerInstance = new SnapshotManager();

            return SnapshotManagerInstance;
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void DeleteSnapshot()
        {

        }

        public void SetActiveSnapshot(Snapshot Snapshot)
        {
            if (!SnapshotList.Contains(Snapshot))
                SnapshotList.Add(Snapshot);

            ActiveSnapshot = Snapshot;
            
            SnapshotManagerEventArgs SnapshotManagerEventArgs = new SnapshotManagerEventArgs();
            SnapshotManagerEventArgs.SnapshotList = SnapshotList;
            UpdateSnapshotDisplay.Invoke(this, SnapshotManagerEventArgs);
        }

        public void SaveSnapshot(Snapshot Snapshot)
        {
            if (!SnapshotList.Contains(Snapshot))
                SnapshotList.Add(Snapshot);

            Snapshot.SetTimeStampToNow();

            // Set the most recently saved snapshot as the active snapshot
            SetActiveSnapshot(Snapshot);
        }

        public Boolean HasActiveSnapshot()
        {
            if (ActiveSnapshot == null)
                return false;

            return true;
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="MemoryEditor"></param>
        /// <returns></returns>
        public Snapshot GetActiveSnapshot()
        {
            // Take a snapshot if there are none
            if (!HasActiveSnapshot())
                return SnapshotAllRegions();

            // Return the snapshot
            return ActiveSnapshot;
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
        public Snapshot SnapshotAllRegions()
        {
            if (MemoryEditor == null)
                return new Snapshot();

            // Query all virtual pages
            List<RemoteVirtualPage> VirtualPages = new List<RemoteVirtualPage>();
            foreach (RemoteVirtualPage Page in MemoryEditor.Memory.VirtualPages)
                VirtualPages.Add(Page);

            // Convert each virtual page to a remote region (a more condensed representation of the information)
            List<SnapshotRegion> MemoryRegions = new List<SnapshotRegion>();
            for (int PageIndex = 0; PageIndex < VirtualPages.Count; PageIndex++)
                MemoryRegions.Add(new SnapshotRegion(VirtualPages[PageIndex].Information.BaseAddress, (Int32)VirtualPages[PageIndex].Information.RegionSize));

            return new Snapshot(MemoryRegions.ToArray());
        }
        
    }
}
