using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;

namespace Anathema
{
    /// <summary>
    /// Consantly queries the OS for all of the memory of the target program to determine if any of it has changed via chunk hashing.
    /// This information can then be used to drastically reduce the search space of a target process (95% is a reasonable amount)
    /// 
    /// Scan cycle:
    /// 1) Perform a checksum on each memory page. A total of 2 will be kept for each page (for comparison purposes)
    /// 2) Once 2 have been collected, we can compare to determine if the pages have changed and store this in the history.
    /// 3) If a page has changed, we can then split that page into two if it is larger than the given threshold (mark halves as unknown)
    /// 4) End on user request. Keep all leaves marked as changed, or all leaves marked as unknown. Discard unchanged blocks.
    ///
    /// </summary>
    class TreeScanner : ITreeScannerModel
    {
        // Variables
        private Snapshot<Null> Snapshot;
        private List<FilterTree> FilterTrees; // Trees to grow to search for changes
        private Int32 LeafSize;

        public TreeScanner()
        {

        }

        /// <summary>
        /// Heuristic algorithm to determine the proper leaf size for this tree scan based on the current region size.
        /// Leaves vary in powers of 2 from 32B to 256B
        /// </summary>
        /// <param name="MemorySize"></param>
        /// <returns></returns>
        private Int32 SetLeafSize(UInt64 MemorySize)
        {
            UInt64 MB = (UInt64)(MemorySize >> 20);
            Int32 MBBits = 0;
            while ((MB >>= 1) != 0) { MBBits++; }
            MBBits = (MBBits <= 5 ? 5 : (MBBits >= 8 ? 8 : MBBits));
            return (Int32)(1 << MBBits);
        }

        public override void Begin()
        {
            this.Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot(true));
            this.Snapshot.SetElementType(typeof(Byte));
            this.Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());
            this.FilterTrees = new List<FilterTree>();
            this.LeafSize = SetLeafSize(Snapshot.GetMemorySize());

            // Initialize filter tree roots
            foreach (SnapshotRegion SnapshotRegion in Snapshot)
                FilterTrees.Add(new FilterTree(SnapshotRegion.BaseAddress, SnapshotRegion.RegionSize, LeafSize));

            base.Begin();
        }

        protected override void Update()
        {
            OSInterface OSInterface = Snapshot.GetOSInterface();
            try
            {
                Parallel.ForEach(FilterTrees, (Tree) =>
                {
                    // Process the changes that have occurred since the last sampling for this memory page
                    Tree.ProcessChanges(Tree.ReadAllSnapshotMemory(OSInterface, false), Tree.BaseAddress);
                });
            }
            catch (ScanFailedException)
            {

            }

            OnEventUpdateScanCount(new ScannerEventArgs(this.ScanCount));
        }

        public override void End()
        {
            // Wait for the filter to finish
            base.End();

            // Collect the pages that have changed
            List<SnapshotRegion> FilteredRegions = new List<SnapshotRegion>();
            for (Int32 Index = 0; Index < FilterTrees.Count; Index++)
                FilterTrees[Index].GetChangedRegions(FilteredRegions);

            // Create snapshot with results
            Snapshot = new Snapshot<Null>(FilteredRegions);
            Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            Snapshot.ExpandAllRegionsOutward(sizeof(UInt64) - 1);
            Snapshot = new Snapshot<Null>(Snapshot.MaskRegions(Snapshot, Snapshot.GetSnapshotRegions()));
            Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            // Read memory so that there are values for the next scan to process
            Snapshot.ReadAllSnapshotMemory();
            Snapshot.SetScanMethod("Tree Scan");

            // Save the snapshot
            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            CleanUp();
        }

        private void CleanUp()
        {
            FilterTrees = null;
            Snapshot = null;
        }

        public class FilterTree : SnapshotRegion<Null>
        {
            private enum StateEnum
            {
                Unchanged,
                HasChanged
            }

            private FilterTree ChildRight;
            private FilterTree ChildLeft;
            private Int32 LeafSize;
            private UInt64? Checksum;
            private StateEnum State;

            public FilterTree(IntPtr BaseAddress, Int32 RegionSize, Int32 LeafSize) : base(BaseAddress, RegionSize)
            {
                // Initialize state variables
                State = StateEnum.Unchanged;
                this.LeafSize = LeafSize;
                Checksum = null;

                ChildRight = null;
                ChildLeft = null;
            }

            public void GetChangedRegions(List<SnapshotRegion> AcceptedPages)
            {
                // Add this page to the accepted list if we are a leaf on the tree structure with changes (or we are unsure)
                if (ChildLeft == null && ChildRight == null)
                {
                    if (State == StateEnum.HasChanged)
                        AcceptedPages.Add(this);
                }
                // This node has children; propagate the request (in equal halves) downwards
                else
                {
                    ChildLeft.GetChangedRegions(AcceptedPages);
                    ChildRight.GetChangedRegions(AcceptedPages);
                }
            }

            public void ProcessChanges(Byte[] Data, IntPtr RootBaseAddress)
            {
                // No need to process a page that has already changed
                if (RegionSize <= LeafSize && State == StateEnum.HasChanged)
                    return;

                // If this node has no children, this node is a leaf and thus does the processing
                if (ChildLeft == null && ChildRight == null)
                {
                    // Calculate changes for this page
                    UInt64 StartIndex = (UInt64)BaseAddress - (UInt64)RootBaseAddress;
                    UInt64 NewChecksum = Hashing.ComputeCheckSum(Data, StartIndex, StartIndex + (UInt64)RegionSize);

                    // Determine if state has changed after collecting 2 or more checksums
                    if (Checksum.HasValue)
                    {
                        if (Checksum != NewChecksum)
                            State = StateEnum.HasChanged;
                    }

                    // Save most recent checksum
                    Checksum = NewChecksum;

                    // This page needs to be split if a change was detected and the size is above the threshold
                    if (State == StateEnum.HasChanged && RegionSize > LeafSize)
                    {
                        ChildLeft = new FilterTree(BaseAddress, RegionSize / 2, LeafSize);
                        ChildRight = new FilterTree(BaseAddress + RegionSize / 2, RegionSize - RegionSize / 2, LeafSize);
                    }
                }

                // Pass the data down to the children if they exist
                if (ChildLeft != null && ChildRight != null)
                {
                    ChildLeft.ProcessChanges(Data, RootBaseAddress);
                    ChildRight.ProcessChanges(Data, RootBaseAddress);
                }
            }

        } // End class

    } // End class

} // End namespace