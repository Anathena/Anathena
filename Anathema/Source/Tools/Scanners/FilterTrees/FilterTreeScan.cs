using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

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
    class FilterTreeScan : IFilterTreeScanModel
    {
        // Variables
        private Snapshot Snapshot;
        private List<MemoryChangeTree> FilterTrees; // Trees to grow to search for changes
        
        public FilterTreeScan()
        {

        }

        public override void BeginScan()
        {
            this.Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());
            this.FilterTrees = new List<MemoryChangeTree>();

            // Initialize filter tree roots
            SnapshotRegion[] MemoryRegions = Snapshot.GetSnapshotData();
            for (int PageIndex = 0; PageIndex < MemoryRegions.Length; PageIndex++)
                FilterTrees.Add(new MemoryChangeTree(MemoryRegions[PageIndex].BaseAddress, MemoryRegions[PageIndex].RegionSize));

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            Snapshot.ReadAllMemory(true);

            Parallel.ForEach(FilterTrees, (Tree) =>
            {
                if (Tree.IsDead())
                    return; // Works as 'continue' in a parallel foreach
                
                Byte[] PageData = Snapshot.GetSnapshotData()[FilterTrees.IndexOf(Tree)].GetCurrentValues();

                // Process the changes that have occurred since the last sampling for this memory page
                if (PageData != null)
                {
                    Tree.ProcessChanges(PageData, Tree.BaseAddress);
                }
                // Error reading this page -- kill it (may have been deallocated)
                else
                {
                    Tree.KillTree();
                }
            });
        }

        public override void EndScan()
        {
            // Wait for the filter to finish
            base.EndScan();

            // Collect the pages that have changed
            List<SnapshotRegion> FilteredRegions = new List<SnapshotRegion>();
            for (Int32 Index = 0; Index < FilterTrees.Count; Index++)
                FilterTrees[Index].GetChangedRegions(FilteredRegions);
            FilterTrees = null;
            
            // Create snapshot with results
            Snapshot FilteredSnapshot = new Snapshot(FilteredRegions.ToArray());
            FilteredSnapshot.SetScanMethod("Tree Scan");

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            FilteredSnapshot.GrowAllRegions();
            FilteredSnapshot.MaskRegions(Snapshot);

            // Read memory so that there are values for the next scan to process
            FilteredSnapshot.ReadAllMemory();

            // Send the size of the filtered memory to the GUI
            FilterTreesEventArgs Args = new FilterTreesEventArgs();
            Args.FilterResultSize = FilteredSnapshot.GetMemorySize();
            OnEventUpdateMemorySize(Args);

            // Save the snapshot
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

        public class MemoryChangeTree : SnapshotRegion
        {
            // Experimentally found that splitting pages on boundaries of 64 or 128 works best.
            private const Int32 PageSplitThreshold = 64;

            private enum StateEnum
            {
                Unchanged,
                HasChanged,
                Deallocated
            }

            private MemoryChangeTree ChildRight;
            private MemoryChangeTree ChildLeft;
            private UInt64? Checksum;
            private StateEnum State;

            public MemoryChangeTree(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize)
            {
                // Initialize state variables
                State = StateEnum.Unchanged;
                Checksum = null;

                ChildRight = null;
                ChildLeft = null;
            }

            public Boolean IsDead()
            {
                return (State == StateEnum.Deallocated);
            }

            public void KillTree()
            {
                State = StateEnum.Deallocated;
            }

            public void GetChangedRegions(List<SnapshotRegion> AcceptedPages)
            {
                // Add this page to the accepted list if we are a leaf on the tree structure with changes (or we are unsure)
                if (ChildLeft == null && ChildRight == null)
                {
                    if (State == StateEnum.HasChanged)
                    {
                        AcceptedPages.Add(this);
                    }
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
                if (RegionSize <= PageSplitThreshold && State == StateEnum.HasChanged)
                    return;

                if (State == StateEnum.Deallocated)
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
                    if (State == StateEnum.HasChanged && RegionSize > PageSplitThreshold)
                    {
                        ChildLeft = new MemoryChangeTree(BaseAddress, RegionSize / 2);
                        ChildRight = new MemoryChangeTree(BaseAddress + RegionSize / 2, RegionSize - RegionSize / 2);
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