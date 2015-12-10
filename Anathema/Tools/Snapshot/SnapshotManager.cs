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
        private static SnapshotManager SnapshotManagerInstance;

        private MemorySharp MemoryEditor;

        private List<Snapshot> Snapshots;   // Snapshots being managed
        private Snapshot ActiveSnapshot;    // Reference to the active snapshot being used by Anathema

        // Event stubs
        public event EventHandler UpdateSnapshotDisplay;

        private SnapshotManager()
        {
            Snapshots = new List<Snapshot>();
            ActiveSnapshot = null;

            InitializeObserver();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public static SnapshotManager GetSnapshotManagerInstance()
        {
            if (SnapshotManagerInstance == null)
                SnapshotManagerInstance = new SnapshotManager();

            return SnapshotManagerInstance;
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
            Snapshots.Add(Snapshot);
            
            // Set the most recently saved snapshot as the active snapshot
            ActiveSnapshot = Snapshot;
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="MemoryEditor"></param>
        /// <returns></returns>
        public Snapshot GetActiveSnapshot(MemorySharp MemoryEditor)
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
