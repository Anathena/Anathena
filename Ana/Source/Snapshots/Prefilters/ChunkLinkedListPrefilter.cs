namespace Ana.Source.Snapshots.Prefilters
{
    using Engine;
    using Engine.OperatingSystems;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;
    using Utils.Extensions;

    /// <summary>
    /// <para>
    /// SnapshotPrefilter is a heuristic process that drastically improves scan speed. It capitalizes on the fact that
    /// > 95% of memory remains constant in most processes, at least for a short time span. Users will
    /// likely be hunting variables that are in the remaining 5%, which Divine will isolate.
    /// </para>
    /// <para>
    /// This is a heuristic because it assumes that the variable, or a variable in the same chunk on the same page,
    /// has changed before the user requests a snapshot of the target processes memory
    /// </para>
    /// <para>
    /// Steps are as follows:
    /// 1) Update a queue of chunks to process, based on timestamp since last edit. Add or remove chunks that
    ///     become allocated or deallocated
    /// 2) Cycle through x chunks (to be determined how many is reasonable), computing the hash of each. Skip chunks
    ///     that have already changed. We can indefinitely mark them as a dynamic region.
    /// 3) On snapshot request, we can do a grow+mask operation of current chunks against the current virtual pages.
    /// </para>
    /// </summary>
    internal class ChunkLinkedListPrefilter : RepeatedTask, ISnapshotPrefilter
    {
        private const Int32 ChunkLimit = 16384;

        private const Int32 ChunkSize = 4096;

        private const Int32 RescanTime = 800;

        // Singleton instance of Prefilter
        private static Lazy<ISnapshotPrefilter> snapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(
            () => { return new ChunkLinkedListPrefilter(); },
            LazyThreadSafetyMode.PublicationOnly);

        private ChunkLinkedListPrefilter()
        {
            this.ChunkList = new LinkedList<RegionProperties>();
            this.ChunkLock = new Object();
            this.ElementLock = new Object();
        }

        private LinkedList<RegionProperties> ChunkList { get; set; }

        private Object ChunkLock { get; set; }

        private Object ElementLock { get; set; }

        public static ISnapshotPrefilter GetInstance()
        {
            return ChunkLinkedListPrefilter.snapshotPrefilterInstance.Value;
        }

        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            List<SnapshotRegion> regions = new List<SnapshotRegion>();

            using (TimedLock.Lock(this.ChunkLock))
            {
                foreach (RegionProperties virtualPage in this.ChunkList)
                {
                    if (!virtualPage.HasChanged())
                    {
                        continue;
                    }

                    SnapshotRegion newRegion = new SnapshotRegion<Null>(virtualPage);
                    newRegion.SetAlignment(SettingsViewModel.GetInstance().Alignment);
                    regions.Add(newRegion);
                }
            }

            // Create snapshot from valid regions, do standard expand/mask operations to catch lost bytes for larger data types
            Snapshot<Null> prefilteredSnapshot = new Snapshot<Null>(regions);
            prefilteredSnapshot.Alignment = SettingsViewModel.GetInstance().Alignment;
            prefilteredSnapshot.ExpandAllRegionsOutward(PrimitiveTypes.GetLargestPrimitiveSize() - 1);
            prefilteredSnapshot = new Snapshot<Null>(prefilteredSnapshot.MaskRegions(prefilteredSnapshot, regions));

            return new Snapshot<Null>(regions);
        }

        public override void Begin()
        {
            this.UpdateInterval = ChunkLinkedListPrefilter.RescanTime;
            base.Begin();
        }

        protected override void Update()
        {
            this.ProcessPages();
            this.UpdateProgress();
        }

        /// <summary>
        /// Queries virtual pages from the OS to dertermine if any allocations or deallocations have happened
        /// </summary>
        private IEnumerable<RegionProperties> CollectNewPages()
        {
            List<RegionProperties> newRegions = new List<RegionProperties>();

            // Gather current regions from the target process
            IEnumerable<NormalizedRegion> queriedVirtualRegions = SnapshotManager.GetInstance().CollectSnapshotRegions();
            List<NormalizedRegion> queriedChunkedRegions = new List<NormalizedRegion>();

            // Chunk all virtual regions into a standardized size
            queriedVirtualRegions.ForEach(x => queriedChunkedRegions.AddRange(x.ChunkNormalizedRegion(ChunkLinkedListPrefilter.ChunkSize)));

            // Sort our lists (descending)
            IOrderedEnumerable<NormalizedRegion> queriedRegionsSorted = queriedChunkedRegions.OrderByDescending(x => x.BaseAddress.ToUInt64());
            IOrderedEnumerable<RegionProperties> currentRegionsSorted;

            using (TimedLock.Lock(this.ChunkLock))
            {
                currentRegionsSorted = this.ChunkList.OrderByDescending(x => x.BaseAddress.ToUInt64());
            }

            // Create comparison stacks
            Stack<RegionProperties> currentRegionStack = new Stack<RegionProperties>();
            Stack<NormalizedRegion> queriedRegionStack = new Stack<NormalizedRegion>();

            currentRegionsSorted.ForEach(x => currentRegionStack.Push(x));
            queriedRegionsSorted.ForEach(x => queriedRegionStack.Push(x));

            // Begin stack based comparison algorithm to resolve our chunk processing queue
            NormalizedRegion queriedRegion = queriedRegionStack.Count == 0 ? null : queriedRegionStack.Pop();
            RegionProperties currentRegion = currentRegionStack.Count == 0 ? null : currentRegionStack.Pop();

            while (queriedRegionStack.Count > 0 && currentRegionStack.Count > 0)
            {
                if (queriedRegion < currentRegion)
                {
                    // New region we have not seen yet
                    newRegions.Add(new RegionProperties(queriedRegion));
                    queriedRegion = queriedRegionStack.Pop();
                }
                else if (queriedRegion > currentRegion)
                {
                    // Region that went missing (deallocated)
                    currentRegion = currentRegionStack.Pop();
                }
                else
                {
                    // Region we have already seen
                    queriedRegion = queriedRegionStack.Pop();
                    currentRegion = currentRegionStack.Pop();
                }
            }

            // Add remaining queried regions
            while (queriedRegionStack.Count > 0)
            {
                newRegions.Add(new RegionProperties(queriedRegion));
                queriedRegion = queriedRegionStack.Pop();
            }

            return newRegions;
        }

        /// <summary>
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            // Check for newly allocated pages
            using (TimedLock.Lock(this.ChunkLock))
            {
                foreach (RegionProperties newPage in this.CollectNewPages())
                {
                    this.ChunkList.AddFirst(newPage);
                }
            }

            using (TimedLock.Lock(this.ChunkLock))
            {
                // Process the allowed amount of chunks from the priority queue
                Parallel.For(
                    0,
                    Math.Min(this.ChunkList.Count, ChunkLinkedListPrefilter.ChunkLimit),
                    SettingsViewModel.GetInstance().ParallelSettings,
                    index =>
                {
                    RegionProperties chunk;
                    Boolean success = false;

                    // Grab next available element
                    lock (this.ElementLock)
                    {
                        chunk = this.ChunkList.First();
                        this.ChunkList.RemoveFirst();

                        // Do not process chunks that have been marked as changed
                        if (chunk.HasChanged())
                        {
                            this.ChunkList.AddLast(chunk);
                            return;
                        }
                    }

                    // Read current page data for chunk
                    Byte[] pageData = EngineCore.GetInstance().OperatingSystemAdapter?.ReadBytes(chunk.BaseAddress, chunk.RegionSize, out success);

                    // Read failed; Deallocated page
                    if (!success)
                    {
                        return;
                    }

                    // Update chunk
                    chunk.Update(pageData);

                    // Recycle it
                    using (TimedLock.Lock(this.ElementLock))
                    {
                        ChunkList.AddLast(chunk);
                    }
                });
            }
        }

        private void UpdateProgress()
        {
            Int32 processedCount;
            Int32 chunkCount;

            using (TimedLock.Lock(this.ChunkLock))
            {
                processedCount = this.ChunkList.Where(x => x.IsProcessed()).Count();
                chunkCount = this.ChunkList.Count();
            }
        }

        protected override void OnEnd()
        {
            base.OnEnd();
        }

        internal class RegionProperties : NormalizedRegion
        {
            public RegionProperties(NormalizedRegion region) : this(region.BaseAddress, region.RegionSize)
            {
            }

            public RegionProperties(IntPtr baseAddress, Int32 regionSize) : base(baseAddress, regionSize)
            {
                this.Checksum = null;
                this.Changed = false;
            }

            private UInt64? Checksum { get; set; }

            private Boolean Changed { get; set; }

            public Boolean IsProcessed()
            {
                if (this.Checksum == null)
                {
                    return false;
                }

                return true;
            }

            public Boolean HasChanged()
            {
                return this.Changed;
            }

            public void Update(Byte[] memory)
            {
                UInt64 currentChecksum;

                currentChecksum = Hashing.ComputeCheckSum(memory);

                if (this.Checksum == null)
                {
                    this.Checksum = currentChecksum;
                    return;
                }

                this.Changed = this.Checksum != currentChecksum;
            }
        }
        //// End class
    }
    //// End class
}
//// End namespace