using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System.Collections.Concurrent;

namespace Anathema
{
    class ChunkScanner : IChunkScannerModel
    {
        private List<MemoryChunkRoots> ChunkRoots;
        private Snapshot Snapshot;
        private Int32 ChunkSize;

        // User controlled variables
        private Int32 MinChanges;

        public ChunkScanner()
        {

        }

        public override void SetMinChanges(Int32 MinChanges)
        {
            this.MinChanges = MinChanges;
        }

        /// <summary>
        /// Heuristic algorithm to determine the proper chunk size for this chunk scan based on the current region size.
        /// Chunks vary in powers of 2 from 32B to 1024B
        /// </summary>
        /// <param name="MemorySize"></param>
        /// <returns></returns>
        private Int32 SetChunkSize(UInt64 MemorySize)
        {
            UInt32 Value = (UInt32)(Snapshot.GetMemorySize() >> 20);
            Int32 Bits = 0;
            while ((Value >>= 1) != 0) { Bits++; }
            Bits = (Bits <= 5 ? 5 : (Bits >= 10 ? 10 : Bits));
            return (Int32)(1 << Bits);
        }

        public override void BeginScan()
        {
            this.ChunkSize = SetChunkSize(Snapshot.GetMemorySize());
            this.Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());
            this.ChunkRoots = new List<MemoryChunkRoots>();

            List<SnapshotRegion> Chunks = new List<SnapshotRegion>();
            foreach (SnapshotRegion MemoryRegion in Snapshot)
            { 

            }

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            MemorySharp MemoryEditor = Snapshot.GetMemoryEditor();

            Parallel.ForEach(ChunkRoots, (ChunkRoot) =>
            {
                try
                {
                    // Process the changes that have occurred since the last sampling for this memory page
                    ChunkRoot.ProcessChanges(ChunkRoot.ReadAllSnapshotMemory(MemoryEditor, false));
                }
                catch (ScanFailedException)
                {
                    // Fuck it
                }
            });
        }

        public override void EndScan()
        {
            // Wait for the filter to finish
            base.EndScan();

            // Collect the pages that have changed
            List<SnapshotRegion> ChangedRegions = new List<SnapshotRegion>();
            for (Int32 Index = 0; Index < ChunkRoots.Count; Index++)
                ChunkRoots[Index].GetChangedRegions(ChangedRegions, MinChanges);
            ChunkRoots = null;

            // Convert trees to a list of memory regions
            List<SnapshotRegion> FilteredRegions = ChangedRegions.ConvertAll(Page => (SnapshotRegion)Page);

            // Create snapshot with results
            Snapshot FilteredSnapshot = new Snapshot(FilteredRegions.ToArray());

            // Grow regions by the size of the largest standard variable and mask this with the original memory list.
            FilteredSnapshot.SetVariableSize(sizeof(UInt64));
            FilteredSnapshot.ExpandAllRegionsOutward();
            FilteredSnapshot = new Snapshot(FilteredSnapshot.MaskRegions(Snapshot, FilteredSnapshot.GetSnapshotRegions()));

            // Read memory so that there are values for the next scan to process
            FilteredSnapshot.ReadAllSnapshotMemory();
            FilteredSnapshot.SetScanMethod("Chunk Scan");

            // Save result
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

        public class MemoryChunkRoots : SnapshotRegion
        {
            private SnapshotRegion[] Chunks;
            private UInt16[] ChangeCounts;
            private UInt32?[] Checksums;

            public MemoryChunkRoots(SnapshotRegion Region, Int32 ChunkSize) : base(Region.BaseAddress, Region.RegionSize)
            {
                // Initialize state variables
                Int32 ChunkCount = RegionSize / ChunkSize + 1;
                IntPtr CurrentBase = Region.BaseAddress;
                Chunks = new SnapshotRegion[ChunkCount];
                ChangeCounts = new UInt16[ChunkCount];
                Checksums = new UInt32?[ChunkCount];

                // Initialize all chunks and checksums
                for (Int32 Index = 0; Index < ChunkCount; Index++)
                {
                    Int32 ChunkRegionSize = ChunkSize;

                    if (Index == ChunkCount - 1)
                        ChunkRegionSize = RegionSize % ChunkSize;

                    Chunks[Index] = new SnapshotRegion(CurrentBase, ChunkRegionSize);
                    ChangeCounts[Index] = 0;
                    Checksums[Index] = 0;

                    CurrentBase += ChunkSize;
                }
            }

            public void GetChangedRegions(List<SnapshotRegion> AcceptedRegions, Int32 MinChanges)
            {
                for (Int32 Index = 0; Index < Chunks.Length; Index++)
                {
                    if (ChangeCounts[Index] >= MinChanges)
                        AcceptedRegions.Add(Chunks[Index]);
                }
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