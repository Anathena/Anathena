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

        private List<Snapshot> SnapshotList;   // Snapshots being managed
        private Snapshot ActiveSnapshot;    // Reference to the active snapshot being used by Anathema
        
        public event SnapshotManagerEventHandler UpdateSnapshotDisplay;

        private SnapshotManager()
        {
            SnapshotList = new List<Snapshot>();
            ActiveSnapshot = null;

            InitializeObserver();
        }

        public static SnapshotManager GetSnapshotManagerInstance()
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

        public void SaveSnapshot(List<RemoteRegion> MemoryRegions)
        {
            SaveSnapshot(new Snapshot(MemoryRegions));
        }

        public void SaveSnapshot(Snapshot Snapshot)
        {
            Snapshot.SetTimeStampToNow();

            SnapshotList.Add(Snapshot);
            
            // Set the most recently saved snapshot as the active snapshot
            ActiveSnapshot = Snapshot;

            SnapshotManagerEventArgs SnapshotManagerEventArgs = new SnapshotManagerEventArgs();
            SnapshotManagerEventArgs.SnapshotList = SnapshotList;
            UpdateSnapshotDisplay.Invoke(this, SnapshotManagerEventArgs);
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="MemoryEditor"></param>
        /// <returns></returns>
        public Snapshot GetActiveSnapshot()
        {
            // Take a snapshot if there are none
            if (ActiveSnapshot == null)
                SnapshotAllMemory();

            // Return the snapshot
            return ActiveSnapshot;
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
        public void SnapshotAllMemory()
        {
            if (MemoryEditor == null)
                return;

            // Query all virtual pages
            List<RemoteVirtualPage> VirtualPages = new List<RemoteVirtualPage>();
            foreach (RemoteVirtualPage Page in MemoryEditor.Memory.VirtualPages)
                VirtualPages.Add(Page);

            // Convert each virtual page to a remote region (a more condensed representation of the information)
            List<RemoteRegion> MemoryRegions = new List<RemoteRegion>();
            for (int PageIndex = 0; PageIndex < VirtualPages.Count; PageIndex++)
                MemoryRegions.Add(new RemoteRegion(MemoryEditor, VirtualPages[PageIndex].Information.BaseAddress, VirtualPages[PageIndex].Information.RegionSize));

            SaveSnapshot(MemoryRegions);
        }
    }
}
