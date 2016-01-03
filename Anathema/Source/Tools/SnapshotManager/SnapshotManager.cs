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

        private Stack<Snapshot> Snapshots;          // Snapshots being managed
        private Stack<Snapshot> DeletedSnapshots;   // Deleted snapshots for the capability of redoing after undo

        public event SnapshotManagerEventHandler UpdateSnapshotDisplay;

        private SnapshotManager()
        {
            Snapshots = new Stack<Snapshot>();
            DeletedSnapshots = new Stack<Snapshot>();

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

        public void CreateNewSnapshot()
        {
            if (Snapshots.Count != 0 && Snapshots.Peek() == null)
                return;

            SaveSnapshot(null);
        }

        public void RedoSnapshot()
        {
            if (DeletedSnapshots.Count == 0)
                return;

            Snapshots.Push(DeletedSnapshots.Pop());

            UpdateDisplay();
        }

        public void UndoSnapshot()
        {
            if (Snapshots.Count == 0)
                return;

            DeletedSnapshots.Push(Snapshots.Pop());

            if (DeletedSnapshots.Peek() == null)
                DeletedSnapshots.Pop();

            UpdateDisplay();
        }

        public void ClearSnapshots()
        {
            Snapshots.Clear();
            DeletedSnapshots.Clear();

            UpdateDisplay();
        }

        public void SaveSnapshot(Snapshot Snapshot)
        {
            if (Snapshot != null)
                Snapshot.SetTimeStampToNow();

            if (Snapshots.Count != 0 && Snapshots.Peek() == null)
                Snapshots.Pop();

            Snapshots.Push(Snapshot);

            DeletedSnapshots.Clear();

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            SnapshotManagerEventArgs SnapshotManagerEventArgs = new SnapshotManagerEventArgs();
            SnapshotManagerEventArgs.Snapshots = Snapshots;
            SnapshotManagerEventArgs.DeletedSnapshots = DeletedSnapshots;
            UpdateSnapshotDisplay.Invoke(this, SnapshotManagerEventArgs);
        }

        public Boolean HasActiveSnapshot()
        {
            if (Snapshots.Count == 0 || Snapshots.Peek() == null)
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
            return Snapshots.Peek();
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
