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
    /// REPEAT:
    /// 1) Perform a check sum on each memory page. A total of 2 will be kept for each page (for comparison purposes)
    /// 2) Once 2 have been collected, we can compare to determine if the pages have changed and store this in the history.
    /// 3) If a page has changed, we can then split that page into two if it is larger than the given threshold (mark halves as unknown)
    /// 4) End on user request. Keep all leaves marked as changed, or all leaves marked as unknown. Discard unchanged blocks.
    ///
    /// </summary>
    class FilterTreeScan : IFilterTreeScanModel
    {
        private MemorySharp MemoryEditor;

        private List<RemoteRegion> MemoryRegions;
        private List<MemoryChangeTree> FilterTrees;         // List of memory pages that we may be interested in
        private CancellationTokenSource CancelRequest;      // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                         // Event that constantly checks the target process for changes

        // Variables
        private const Int32 AbortTime = 3000;       // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 200;               // Time to wait (in ms) for a cancel request between each scan
        private static Int32 PageSplitThreshold;    // User specified minimum page size for dynamic pages

        // User-tuned variables
        private Int32 VariableSize;

        // Event stubs
        public event EventHandler EventFilterFinished;
        public event FilterTreeScanEventHandler EventUpdateMemorySize;

        public FilterTreeScan()
        {
            FilterTreeScan.PageSplitThreshold = 1;
            InitializeObserver();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public void SetVariableSize(Int32 VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        public UInt64 GetFinalSize(List<RemoteRegion> FilteredMemoryRegions)
        {
            UInt64 Value = 0;

            if (FilteredMemoryRegions != null)
                for (int Index = 0; Index < FilteredMemoryRegions.Count; Index++)
                    Value += (UInt64)FilteredMemoryRegions[Index].RegionSize;

            return Value;
        }

        public void BeginFilter()
        {
            this.MemoryRegions = SnapshotManager.GetSnapshotManagerInstance().GetActiveSnapshot().GetMemoryRegions();
            this.FilterTrees = new List<MemoryChangeTree>();

            // Initialize filter tree roots
            for (int PageIndex = 0; PageIndex < MemoryRegions.Count; PageIndex++)
                FilterTrees.Add(new MemoryChangeTree(MemoryRegions[PageIndex].BaseAddress, MemoryRegions[PageIndex].RegionSize));

            CancelRequest = new CancellationTokenSource();
            ChangeScanner = Task.Run(async () =>
            {
                while (true)
                {
                    // Query the target process for memory changes
                    ApplyFilter();

                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);
        }

        private void ApplyFilter()
        {
            //foreach (MemoryChangeTree Tree in FilterTrees)
            // Parallel.For(0, FilterTrees.Count, PageIndex => // Upwards of a x2 increase in speed
            Parallel.ForEach(FilterTrees, (Tree) =>
            {
                if (Tree.IsDead())
                    return; // Works as 'continue' in a parallel foreach

                Boolean SuccessReading;
                Byte[] PageData = MemoryEditor.ReadBytes(Tree.BaseAddress, Tree.RegionSize, out SuccessReading, false);

                // Process the changes that have occurred since the last sampling for this memory page
                if (SuccessReading)
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

        public Snapshot EndFilter()
        {
            // Wait for the filter to finish
            CancelRequest.Cancel();
            try { ChangeScanner.Wait(AbortTime); }
            catch (AggregateException) { }

            // Collect the pages that have changed
            List<MemoryChangeTree> ChangedRegions = new List<MemoryChangeTree>();
            for (Int32 Index = 0; Index < FilterTrees.Count; Index++)
                FilterTrees[Index].GetChangedRegions(ChangedRegions);
            FilterTrees = null;

            // Convert trees to a list of memory regions
            List<RemoteRegion> FilteredRegions = ChangedRegions.ConvertAll(Page => (RemoteRegion)Page);

            // Send the size of the filtered memory to the GUI
            FilterHashTreesEventArgs Args = new FilterHashTreesEventArgs();
            Args.FilterResultSize = GetFinalSize(FilteredRegions);
            EventUpdateMemorySize.Invoke(this, Args);

            // Create snapshot with results
            Snapshot FilteredSnapshot = new Snapshot(FilteredRegions);

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            FilteredSnapshot.GrowRegions(sizeof(UInt64));
            FilteredSnapshot.MaskRegions(MemoryRegions);

            return FilteredSnapshot;
        }

        public class MemoryChangeTree : RemoteRegion
        {
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

            public MemoryChangeTree(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize)
            {
                // Initialize state variables
                State = StateEnum.Unchanged;
                Checksum = null;

                ChildRight = null;
                ChildLeft = null;
            }

            private Boolean HasChanged()
            {
                return (State == StateEnum.HasChanged);
            }

            public Boolean IsDead()
            {
                return (State == StateEnum.Deallocated);
            }

            public void KillTree()
            {
                State = StateEnum.Deallocated;
            }

            public void GetChangedRegions(List<MemoryChangeTree> AcceptedPages)
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

            public Boolean ProcessChanges(Byte[] Data, IntPtr RootBaseAddress)
            {
                // No need to process a page that has already changed
                if (RegionSize <= FilterTreeScan.PageSplitThreshold && State == StateEnum.HasChanged)
                    return HasChanged();

                if (State == StateEnum.Deallocated)
                    return HasChanged();

                if (RootBaseAddress.ToInt64() == 0x1B170000)
                {
                    int k = 1;
                }

                // If this node has no children, this node is a leaf and thus does the processing
                if (ChildLeft == null && ChildRight == null)
                {
                    // Calculate changes for this page
                    UInt64 StartIndex = (UInt64)BaseAddress - (UInt64)RootBaseAddress;
                    UInt64 NewChecksum = ComputeCheckSum(Data, StartIndex, StartIndex + (UInt64)RegionSize);

                    // Determine if state has changed after collecting 2 or more checksums
                    if (Checksum.HasValue)
                    {
                        if (Checksum != NewChecksum)
                            State = StateEnum.HasChanged;
                    }

                    // Save most recent checksum
                    Checksum = NewChecksum;

                    // This page needs to be split if a change was detected and the size is above the threshold
                    if (State == StateEnum.HasChanged && RegionSize > FilterTreeScan.PageSplitThreshold)
                    {
                        ChildLeft = new MemoryChangeTree(BaseAddress, RegionSize / 2);
                        ChildRight = new MemoryChangeTree(BaseAddress + RegionSize / 2, RegionSize - RegionSize / 2);
                    }
                }

                // Pass the data down to the children if they exist
                if (ChildLeft != null && ChildRight != null)
                {
                    return (ChildLeft.ProcessChanges(Data, RootBaseAddress) & ChildRight.ProcessChanges(Data, RootBaseAddress));
                }

                return HasChanged();
            }

            /// <summary>
            /// This should be replaced with a real checksum function with better collision avoidance
            /// </summary>
            public unsafe static UInt64 ComputeCheckSum(Byte[] Data, UInt64 Start, UInt64 End)
            {
                unchecked
                {
                    UInt64 Hash = 0;

                    // Hashing function
                    for (; Start < End; Start += sizeof(UInt64))
                    {
                        fixed (Byte* Value = &Data[Start])
                        {
                            Hash ^= *(UInt64*)(Value);
                        }
                    }
                    for (; Start < End; Start++)
                    {
                        fixed (Byte* Value = &Data[Start])
                        {
                            Hash ^= (UInt64)(*(Value));
                        }
                    }

                    return Hash;
                }
            }

        } // End class

    } // End class

} // End namespace