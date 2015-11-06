using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    class SnapshotManager
    {
        private List<Snapshot> Snapshots;   // Snapshots being managed
        private Snapshot ActiveSnapshot;    // Reference to the active snapshot being used by Anathema

        public SnapshotManager()
        {
            Snapshots = new List<Snapshot>();
            ActiveSnapshot = null;
        }

        public void SaveSnapshot(List<RemoteRegion> MemoryRegions)
        {
            Snapshots.Add(new Snapshot(MemoryRegions));
            
            // Set the most recently saved snapshot as the active snapshot
            ActiveSnapshot = Snapshots[Snapshots.Count - 1];
        }

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="MemoryEditor"></param>
        /// <returns></returns>
        public List<RemoteRegion> GetActiveSnapshot(MemorySharp MemoryEditor)
        {
            // Take a snapshot if there are none
            if (ActiveSnapshot == null)
                SnapshotAllMemory(MemoryEditor);

            // Return the snapshot
            return ActiveSnapshot.GetMemoryRegions();
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
        public void SnapshotAllMemory(MemorySharp MemoryEditor)
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
