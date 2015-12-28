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
        private Int32 ChunkSize;
        private Int32 MinChanges;

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

        public void SetMinChanges(Int32 MinChanges)
        {
            this.MinChanges = MinChanges;
        }

        public void BeginFilter()
        {
            this.MemoryRegions = SnapshotManager.GetSnapshotManagerInstance().GetActiveSnapshot().GetMemoryRegions();
            this.ChunkRoots = new List<MemoryChunkRoots>();

            // Initialize filter tree roots
            for (int PageIndex = 0; PageIndex < MemoryRegions.Count; PageIndex++)
                ChunkRoots.Add(new MemoryChunkRoots(MemoryRegions[PageIndex].BaseAddress, MemoryRegions[PageIndex].RegionSize, ChunkSize));

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
                    Tree.ProcessChanges(PageData);
                }
                // Error reading this page -- kill it (may have been deallocated)
                else
                {
                    Tree.KillRegion();
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
            List<RemoteRegion> ChangedRegions = new List<RemoteRegion>();
            for (Int32 Index = 0; Index < ChunkRoots.Count; Index++)
                ChunkRoots[Index].GetChangedRegions(ChangedRegions, MinChanges);
            ChunkRoots = null;

            // Convert trees to a list of memory regions
            List<RemoteRegion> FilteredRegions = ChangedRegions.ConvertAll(Page => (RemoteRegion)Page);

            // Create snapshot with results
            Snapshot FilteredSnapshot = new Snapshot(FilteredRegions);

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            FilteredSnapshot.GrowRegions(sizeof(UInt64));
            FilteredSnapshot.MaskRegions(MemoryRegions);

            // Send the size of the filtered memory to the GUI
            FilterChunksEventArgs Args = new FilterChunksEventArgs();
            Args.FilterResultSize = FilteredSnapshot.GetSize();
            EventUpdateMemorySize.Invoke(this, Args);

            return FilteredSnapshot;
        }

        public class MemoryChunkRoots : RemoteRegion
        {
            private Boolean Dead;
            private RemoteRegion[] Chunks;
            private UInt16[] ChangeCounts;
            private UInt32?[] Checksums;

            public MemoryChunkRoots(IntPtr BaseAddress, Int32 RegionSize, Int32 ChunkSize) : base(null, BaseAddress, RegionSize)
            {
                // Initialize state variables
                Dead = false;

                Int32 ChunkCount = RegionSize / ChunkSize + 1;

                Chunks = new RemoteRegion[ChunkCount];
                ChangeCounts = new UInt16[ChunkCount];
                Checksums = new UInt32?[ChunkCount];

                // Initialize all chunks and checksums
                for (Int32 Index = 0; Index < ChunkCount; Index++)
                {
                    Int32 ChunkRegionSize = ChunkSize;

                    if (Index == ChunkCount - 1)
                        ChunkRegionSize = RegionSize % ChunkSize;

                    Chunks[Index] = new RemoteRegion(null, BaseAddress, ChunkRegionSize);
                    ChangeCounts[Index] = 0;
                    Checksums[Index] = 0;

                    BaseAddress += ChunkSize;
                }
            }

            public void GetChangedRegions(List<RemoteRegion> AcceptedRegions, Int32 MinChanges)
            {
                if (IsDead())
                    return;

                for (Int32 Index = 0; Index < Chunks.Length; Index++)
                {
                    if (ChangeCounts[Index] >= MinChanges)
                        AcceptedRegions.Add(Chunks[Index]);
                }
            }

            public void KillRegion()
            {
                Dead = true;
            }

            public Boolean IsDead()
            {
                return Dead;
            }

            public void ProcessChanges(Byte[] Data)
            {
                for (Int32 Index = 0; Index < Chunks.Length; Index++)
                {
                    UInt32 NewChecksum = Hashing.ComputeCheckSum(Data, 
                        (UInt32)((UInt64)Chunks[Index].BaseAddress - (UInt64)this.BaseAddress),
                        (UInt32)((UInt64)Chunks[Index].EndAddress - (UInt64)this.BaseAddress));

                    if (Checksums[Index].HasValue && Checksums[Index] != NewChecksum)
                        ChangeCounts[Index]++;

                    Checksums[Index] = NewChecksum;
                }
            }

        } // End class

    } // End class

} // End namespace