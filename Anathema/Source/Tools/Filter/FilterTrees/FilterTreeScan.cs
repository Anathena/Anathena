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
    /// Consantly queries the OS for all of the memory of the target program to determine if any of it has changed.
    /// This information is stored in an extremely compact way, as a series of bit arrays (0 = unchanged, 1 = changed)
    /// This information can then be used to drastically reduce the search space of a target process (95% is a reasonable amount)
    /// 
    /// Ideally this class will be used as follows
    /// 0) An instance of this class is made, and the Begin function is called with a specified page threshold.
    /// 1) Query all active memory pages running on the target process and save their metadata (page properties, address, size, etc)
    /// 
    /// REPEAT:
    /// 2) Perform a 64 bit hash on each memory page. A total of 2 will be kept for each page (for comparison purposes)
    /// 3) Once 2 have been collected, we can compare to determine if the pages have changed and store this in the history.
    /// 4) If a page has changed, we can then split that page into two (if it is larger than the given threshold), allowing us
    /// to determine which half is of interest with subsequent loop iterations
    /// END ON USER REQUEST. Preferably after enough time to split the pages all the way down to the threshold size.
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
        private const Int32 AbortTime = 3000;       // Time to wait before giving up when ending scan
        private Int32 WaitTime = 200;               // Time to wait (in ms) for a cancel request between each scan
        private static Int32 PageSplitThreshold;    // User specified minimum page size for dynamic pages

        // User-tuned variables
        private Int32 VariableSize;

        // Event stubs
        public event EventHandler EventFilterFinished;
        public event FilterTreeScanEventHandler EventUpdateMemorySize;

        public FilterTreeScan()
        {
            FilterTreeScan.PageSplitThreshold = 64;
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
                    await Task.Delay(WaitTime, CancelRequest.Token); // Await with cancellation
                }
            }, CancelRequest.Token);
        }

        private void ApplyFilter()
        {
            foreach (MemoryChangeTree Tree in FilterTrees)
            // Parallel.For(0, FilterTrees.Count, PageIndex => // Upwards of a x2 increase in speed
           /// Parallel.ForEach(FilterTrees, (Tree) =>
            {
                Boolean Success;
                Byte[] PageData = MemoryEditor.ReadBytes(Tree.BaseAddress, Tree.RegionSize, out Success, false);

                // Process the changes that have occurred since the last sampling for this memory page
                if (Success)
                {
                    Tree.ProcessChanges(PageData, Tree.BaseAddress.ToInt64());
                }
                // Error reading this page -- delete it (may have been deallocated)
                else
                {
                    Tree.Dead = true;
                }
            }//);

            for (Int32 Index = 0; Index < FilterTrees.Count; Index++)
            {
                if (FilterTrees[Index].Dead)
                    FilterTrees.RemoveAt(Index--);
            }
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

            // Convert 
            List<RemoteRegion> FilteredRegions = ChangedRegions.ConvertAll(Page => (RemoteRegion)Page);

            // Merge and collect any adjacent regions from the accepted list of memory pages
            //FilteredRegions = CombineRegions(FilteredRegions);

            // Send the size of the filtered memory to the GUI
            FilterHashTreesEventArgs Args = new FilterHashTreesEventArgs();
            Args.FilterResultSize = GetFinalSize(FilteredRegions);
            EventUpdateMemorySize.Invoke(this, Args);

            return new Snapshot(FilteredRegions);
        }

        // Merging regions the naïve way is O(n^2) and can take upwards of 15 seconds. A faster approach is a stack based algorithm O(??) (<20 ms)
        private List<RemoteRegion> CombineRegions(List<RemoteRegion> Regions)
        {
            if (Regions.Count == 0)
                return Regions;

            // First, sort by start address
            Regions.OrderBy(x => x.BaseAddress);

            // Create and initialize the stack with the first region
            Stack<RemoteRegion> MergedRegions = new Stack<RemoteRegion>();
            MergedRegions.Push(Regions[0]);

            // Build the remaining regions
            for (Int32 Index = MergedRegions.Count; Index < Regions.Count; Index++)
            {
                RemoteRegion Top = MergedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if ((UInt64)Top.BaseAddress < (UInt64)Regions[Index].BaseAddress)
                {
                    MergedRegions.Push(Regions[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if ((UInt64)(Top.EndAddress) < (UInt64)Regions[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)Regions[Index].EndAddress - (UInt64)Top.BaseAddress);
                    MergedRegions.Pop();
                    MergedRegions.Push(Top);
                }
            }

            return MergedRegions.ToList();
        }

        public class MemoryChangeTree : RemoteRegion
        {
            public bool Dead;

            private Int64[] Checksums;  // Can look into UInt32 if memory is a large concern for these trees
            private Int32 QueryCount;   // Number of times changes have been queried

            private MemoryChangeTree ChildLeft;
            private MemoryChangeTree ChildRight;

            private Boolean HasChanged;
            private Boolean StateUnknown;
            
            public MemoryChangeTree(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize)
            {
                // Initialize state variables
                Checksums = new Int64[2];
                QueryCount = 0;

                StateUnknown = true;
                HasChanged = false;
                ChildLeft = null;
                ChildRight = null;
            }

            public void GetChangedRegions(List<MemoryChangeTree> AcceptedPages)
            {
                // Add this page to the accepted list if we are a leaf on the tree structure with changes (or we are unsure)
                if (ChildLeft == null && ChildRight == null)
                {
                    if (HasChanged || StateUnknown)
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

            public Boolean ProcessChanges(Byte[] Data, Int64 RootBaseAddress)
            {
                // No need to process a page that has already changed
                if (RegionSize <= FilterTreeScan.PageSplitThreshold && HasChanged)
                    return HasChanged;

                // If this node has no children, this node is a leaf and thus does the processing
                if (ChildLeft == null && ChildRight == null)
                {
                    // Calculate changes for this page
                    Checksums[QueryCount % 2] = ComputeCheckSum(Data, (Int32)(BaseAddress.ToInt64() - RootBaseAddress), RegionSize);

                    // Update the history, given that 2 checksums have been collected
                    if (QueryCount++ != 0)
                    {
                        HasChanged = HasChanged | (Checksums[0] != Checksums[1]);
                        StateUnknown = false;
                    }

                    // This page needs to be split if a change was detected and the size is above the threshold
                    if (HasChanged && RegionSize > FilterTreeScan.PageSplitThreshold)
                    {
                        ChildLeft = new MemoryChangeTree(BaseAddress, RegionSize / 2);
                        ChildRight = new MemoryChangeTree(BaseAddress + RegionSize / 2, RegionSize - RegionSize / 2);

                        // We no longer need the history for this page since we split it
                        Checksums = null;
                    }
                }

                // Pass the data down to the children if they exist
                if (ChildLeft != null && ChildRight != null)
                {
                    return (ChildLeft.ProcessChanges(Data, RootBaseAddress) & ChildRight.ProcessChanges(Data, RootBaseAddress));
                }

                return HasChanged;
            }

            /// <summary>
            /// This should be replaced with a real checksum function with better collision avoidance
            /// </summary>
            public unsafe static Int64 ComputeCheckSum(Byte[] Data, Int64 Start, Int64 End)
            {
                unchecked
                {
                    Int64 Hash = 0;

                    // Hashing function
                    for (; Start < End; Start += sizeof(Int64))
                    {
                        fixed (Byte* Value = &Data[Start])
                        {
                            Hash += *(Int64*)(Value);
                        }
                    }
                    for (; Start < End; Start++)
                    {
                        fixed (Byte* Value = &Data[Start])
                        {
                            Hash += *(Value);
                        }
                    }

                    return Hash;
                }
            }

        } // End class

    } // End class

} // End namespace