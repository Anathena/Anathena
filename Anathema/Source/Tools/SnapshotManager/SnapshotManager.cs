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

        // Event stubs
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

        /// <summary>
        /// Returns the memory regions associated with the current snapshot. If none exist, a query will be done.
        /// </summary>
        /// <param name="MemoryEditor"></param>
        /// <returns></returns>
        public Snapshot GetActiveSnapshot()
        {
            // Take a snapshot if there are none, or the current one is empty
            if (!HasActiveSnapshot() || Snapshots.Peek().GetMemorySize() == 0)
                return SnapshotAllRegions();

            // Return the snapshot
            return Snapshots.Peek();
        }

        /// <summary>
        /// Determines if there is a valid snapshot saved
        /// </summary>
        /// <returns></returns>
        public Boolean HasActiveSnapshot()
        {
            if (Snapshots.Count == 0 || Snapshots.Peek() == null || Snapshots.Peek().GetMemorySize() == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Take a snapshot of all memory regions in the target process
        /// </summary>
        public Snapshot SnapshotAllRegions()
        {
            if (MemoryEditor == null)
                return new Snapshot<Null>();

            // Query all virtual pages
            List<RemoteVirtualPage> VirtualPages = new List<RemoteVirtualPage>();
            foreach (RemoteVirtualPage Page in MemoryEditor.Memory.VirtualPages)
                VirtualPages.Add(Page);

            // Convert each virtual page to a remote region (a more condensed representation of the information)
            List<SnapshotRegion> MemoryRegions = new List<SnapshotRegion>();
            for (int PageIndex = 0; PageIndex < VirtualPages.Count; PageIndex++)
                MemoryRegions.Add(new SnapshotRegion<Null>(VirtualPages[PageIndex].Information.BaseAddress, (Int32)VirtualPages[PageIndex].Information.RegionSize));

            return new Snapshot<Null>(MemoryRegions.ToArray());
        }

        /// <summary>
        /// Creates a new empty snapshot, which becomes the new active snapshot
        /// </summary>
        public void CreateNewSnapshot()
        {
            if (Snapshots.Count != 0 && Snapshots.Peek() == null)
                return;

            ClearSnapshots();

            SaveSnapshot(null);
        }

        /// <summary>
        /// Reverses an undo action
        /// </summary>
        public void RedoSnapshot()
        {
            if (DeletedSnapshots.Count == 0)
                return;

            Snapshots.Push(DeletedSnapshots.Pop());

            UpdateDisplay();
        }

        /// <summary>
        /// Undoes the current active snapshot, reverting to the previous snapshot
        /// </summary>
        public void UndoSnapshot()
        {
            if (Snapshots.Count == 0)
                return;

            DeletedSnapshots.Push(Snapshots.Pop());

            if (DeletedSnapshots.Peek() == null)
                DeletedSnapshots.Pop();

            UpdateDisplay();
        }

        /// <summary>
        /// Clears all snapshot records
        /// </summary>
        public void ClearSnapshots()
        {
            Snapshots.Clear();
            DeletedSnapshots.Clear();

            UpdateDisplay();
        }

        /// <summary>
        /// Saves a new snapshot, which becomes the current active snapshot
        /// </summary>
        /// <param name="Snapshot"></param>
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

        /// <summary>
        /// Updates display with current snapshot information
        /// </summary>
        private void UpdateDisplay()
        {
            SnapshotManagerEventArgs SnapshotManagerEventArgs = new SnapshotManagerEventArgs();
            SnapshotManagerEventArgs.Snapshots = Snapshots;
            SnapshotManagerEventArgs.DeletedSnapshots = DeletedSnapshots;
            UpdateSnapshotDisplay.Invoke(this, SnapshotManagerEventArgs);
        }
    }
}
