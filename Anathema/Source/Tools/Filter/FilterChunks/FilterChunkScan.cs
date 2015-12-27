using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class FilterChunkScan : IFilterChunkScanModel
    {
        private MemorySharp MemoryEditor;

        private List<RemoteRegion> MemoryRegions;
        private List<MemoryChunkRoots> ChunkRoots;          // List of memory pages that we may be interested in
        private CancellationTokenSource CancelRequest;      // Tells the scan task to cancel (ie finish)
        private Task ChangeScanner;                         // Event that constantly checks the target process for changes

        // Variables
        private const Int32 AbortTime = 3000;       // Time to wait (in ms) before giving up when ending scan
        private Int32 WaitTime = 200;               // Time to wait (in ms) for a cancel request between each scan
        private static Int32 PageSplitThreshold;    // User specified minimum page size for dynamic pages
        private Int32 ChunkSize = 256;

        // Event stubs
        public event EventHandler EventFilterFinished;
        public event FilterChunkScanEventHandler EventUpdateMemorySize;

        public FilterChunkScan()
        {
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

        public void SetChunkSize(Int32 ChunkSize)
        {
            this.ChunkSize = ChunkSize;
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
            this.ChunkRoots = new List<MemoryChunkRoots>();

            // Initialize filter tree roots
            for (int PageIndex = 0; PageIndex < MemoryRegions.Count; PageIndex++)
                ChunkRoots.Add(new MemoryChunkRoots(MemoryRegions[PageIndex].BaseAddress, MemoryRegions[PageIndex].RegionSize));

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
            Parallel.ForEach(ChunkRoots, (Tree) =>
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
            List<MemoryChunkRoots> ChangedRegions = new List<MemoryChunkRoots>();
            for (Int32 Index = 0; Index < ChunkRoots.Count; Index++)
                ChunkRoots[Index].GetChangedRegions(ChangedRegions);
            ChunkRoots = null;

            // Convert trees to a list of memory regions
            List<RemoteRegion> FilteredRegions = ChangedRegions.ConvertAll(Page => (RemoteRegion)Page);

            // Send the size of the filtered memory to the GUI
            FilterChunksEventArgs Args = new FilterChunksEventArgs();
            Args.FilterResultSize = GetFinalSize(FilteredRegions);
            EventUpdateMemorySize.Invoke(this, Args);

            // Create snapshot with results
            Snapshot FilteredSnapshot = new Snapshot(FilteredRegions);

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            FilteredSnapshot.GrowRegions(sizeof(UInt64));
            FilteredSnapshot.MaskRegions(MemoryRegions);

            return FilteredSnapshot;
        }

        public class MemoryChunkRoots : RemoteRegion
        {
            private enum StateEnum
            {
                Unchanged,
                HasChanged,
                Deallocated
            }

            private MemoryChunkRoots ChildRight;
            private MemoryChunkRoots ChildLeft;
            private UInt64? Checksum;
            private StateEnum State;

            public MemoryChunkRoots(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize)
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

            public void GetChangedRegions(List<MemoryChunkRoots> AcceptedPages)
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
                if (RegionSize <= FilterChunkScan.PageSplitThreshold && State == StateEnum.HasChanged)
                    return HasChanged();

                if (State == StateEnum.Deallocated)
                    return HasChanged();

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
                    if (State == StateEnum.HasChanged && RegionSize > FilterChunkScan.PageSplitThreshold)
                    {
                        ChildLeft = new MemoryChunkRoots(BaseAddress, RegionSize / 2);
                        ChildRight = new MemoryChunkRoots(BaseAddress + RegionSize / 2, RegionSize - RegionSize / 2);
                    }
                }

                // Pass the data down to the children if they exist
                if (ChildLeft != null && ChildRight != null)
                {
                    return (ChildLeft.ProcessChanges(Data, RootBaseAddress) & ChildRight.ProcessChanges(Data, RootBaseAddress));
                }

                return HasChanged();
            }

        } // End class

    } // End class

} // End namespace