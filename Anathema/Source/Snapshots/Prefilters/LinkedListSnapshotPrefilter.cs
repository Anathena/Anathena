using Anathema.Source.Engine;
using Anathema.Source.Engine.OperatingSystems;
using Anathema.Source.Engine.Processes;
using Anathema.Source.UserSettings;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Source.Snapshots.Prefilter
{
    /// <summary>
    /// SnapshotPrefilter is a heuristic process that drastically improves scan speed. It capitalizes on the fact that
    /// > 95% of memory remains constant in most processes, at least for a short time span. Users will
    /// likely be hunting variables that are in the remaining 5%, which Divine will isolate.
    /// 
    /// This is a heuristic because it assumes that the variable, or a variable in the same chunk on the same page,
    /// has changed before the user requests a snapshot of the target processes memory
    /// 
    /// Steps are as follows:
    /// 1) Update a queue of chunks to process, based on timestamp since last edit. Add or remove chunks that
    ///     become allocated or deallocated
    /// 2) Cycle through x chunks (to be determined how many is reasonable), computing the hash of each. Skip chunks
    ///     that have already changed. We can indefinitely mark them as a dynamic region.
    /// 3) On snapshot request, we can do a grow+mask operation of current chunks against the current virtual pages.
    /// </summary>
    class LinkedListSnapshotPrefilter : RepeatedTask, ISnapshotPrefilter, IProcessObserver
    {
        // Singleton instance of Prefilter
        private static Lazy<ISnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(() => { return new LinkedListSnapshotPrefilter(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private const Int32 ChunkLimit = 8192;
        private const Int32 ChunkSize = 4096;
        private const Int32 RescanTime = 800;

        private LinkedList<RegionProperties> ChunkList;
        private Object ChunkLock;
        private Object ElementLock;

        private ProgressItem PrefilterProgress;

        private LinkedListSnapshotPrefilter()
        {
            ChunkList = new LinkedList<RegionProperties>();
            ChunkLock = new Object();
            ElementLock = new Object();

            PrefilterProgress = new ProgressItem();
            PrefilterProgress.SetProgressLabel("Prefiltering");
            PrefilterProgress.RestrictProgress();

            InitializeProcessObserver();
        }

        public static ISnapshotPrefilter GetInstance()
        {
            return SnapshotPrefilterInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

            // Clear processing queue on process update
            using (TimedLock.Lock(ChunkLock))
            {
                ChunkList = new LinkedList<RegionProperties>();
            }
        }

        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            List<SnapshotRegion> Regions = new List<SnapshotRegion>();

            using (TimedLock.Lock(ChunkLock))
            {
                foreach (RegionProperties VirtualPage in ChunkList)
                {
                    if (!VirtualPage.HasChanged())
                        continue;

                    SnapshotRegion NewRegion = new SnapshotRegion<Null>(VirtualPage);
                    NewRegion.SetAlignment(Settings.GetInstance().GetAlignmentSettings());
                    Regions.Add(NewRegion);
                }
            }

            // Create snapshot from valid regions, do standard expand/mask operations to catch lost bytes for larger data types
            Snapshot<Null> PrefilteredSnapshot = new Snapshot<Null>(Regions);
            PrefilteredSnapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());
            PrefilteredSnapshot.ExpandAllRegionsOutward(PrimitiveTypes.GetLargestPrimitiveSize() - 1);
            PrefilteredSnapshot = new Snapshot<Null>(PrefilteredSnapshot.MaskRegions(PrefilteredSnapshot, Regions));

            return new Snapshot<Null>(Regions);

        }

        public override void Begin()
        {
            this.UpdateInterval = RescanTime;
            base.Begin();
        }

        protected override void Update()
        {
            if (EngineCore == null)
                return;

            ProcessPages();
            UpdateProgress();
        }

        /// <summary>
        /// Queries virtual pages from the OS to dertermine if any allocations or deallocations have happened
        /// </summary>
        private IEnumerable<RegionProperties> CollectNewPages()
        {
            List<RegionProperties> NewRegions = new List<RegionProperties>();

            // Gather current regions from the target process
            IEnumerable<NormalizedRegion> QueriedVirtualRegions = SnapshotManager.GetInstance().CollectSnapshotRegions();
            List<NormalizedRegion> QueriedChunkedRegions = new List<NormalizedRegion>();

            // Chunk all virtual regions into a standardized size
            QueriedVirtualRegions.ForEach(X => QueriedChunkedRegions.AddRange(X.ChunkNormalizedRegion(ChunkSize)));

            // Sort our lists (descending)
            IOrderedEnumerable<NormalizedRegion> QueriedRegionsSorted = QueriedChunkedRegions.OrderByDescending(X => X.BaseAddress.ToUInt64());
            IOrderedEnumerable<RegionProperties> CurrentRegionsSorted;

            using (TimedLock.Lock(ChunkLock))
            {
                CurrentRegionsSorted = ChunkList.OrderByDescending(X => X.BaseAddress.ToUInt64());
            }

            // Create comparison stacks
            Stack<RegionProperties> CurrentRegionStack = new Stack<RegionProperties>();
            Stack<NormalizedRegion> QueriedRegionStack = new Stack<NormalizedRegion>();

            CurrentRegionsSorted.ForEach(X => CurrentRegionStack.Push(X));
            QueriedRegionsSorted.ForEach(X => QueriedRegionStack.Push(X));

            // Begin stack based comparison algorithm to resolve our chunk processing queue
            NormalizedRegion QueriedRegion = QueriedRegionStack.Count == 0 ? null : QueriedRegionStack.Pop();
            RegionProperties CurrentRegion = CurrentRegionStack.Count == 0 ? null : CurrentRegionStack.Pop();

            while (QueriedRegionStack.Count > 0 && CurrentRegionStack.Count > 0)
            {
                // New region we have not seen yet
                if (QueriedRegion < CurrentRegion)
                {
                    NewRegions.Add(new RegionProperties(QueriedRegion));

                    QueriedRegion = QueriedRegionStack.Pop();
                }
                // Region that went missing (deallocated)
                else if (QueriedRegion > CurrentRegion)
                {
                    CurrentRegion = CurrentRegionStack.Pop();
                }
                // Region we have already seen
                else
                {
                    QueriedRegion = QueriedRegionStack.Pop();
                    CurrentRegion = CurrentRegionStack.Pop();
                }
            }

            // Add remaining queried regions
            while (QueriedRegionStack.Count > 0)
            {
                NewRegions.Add(new RegionProperties(QueriedRegion));
                QueriedRegion = QueriedRegionStack.Pop();
            }

            return NewRegions;
        }

        /// <summary>
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            // Check for newly allocated pages
            using (TimedLock.Lock(ChunkLock))
            {
                foreach (RegionProperties NewPage in CollectNewPages())
                    ChunkList.AddFirst(NewPage);
            }

            using (TimedLock.Lock(ChunkLock))
            {
                // Process the allowed amount of chunks from the priority queue
                Parallel.For(0, Math.Min(ChunkList.Count, ChunkLimit), Index =>
                {
                    RegionProperties Chunk;
                    Boolean Success;

                    // Grab next available element
                    lock (ElementLock)
                    {
                        Chunk = ChunkList.First();
                        ChunkList.RemoveFirst();

                        // Do not process chunks that have been marked as changed
                        if (Chunk.HasChanged())
                        {
                            ChunkList.AddLast(Chunk);
                            return;
                        }
                    }

                    // Read current page data for chunk
                    Byte[] PageData = EngineCore.Memory.ReadBytes(Chunk.BaseAddress, Chunk.RegionSize, out Success);

                    // Read failed; Deallocated page
                    if (!Success)
                        return;

                    // Update chunk
                    Chunk.Update(PageData);

                    // Recycle it
                    using (TimedLock.Lock(ElementLock))
                    {
                        ChunkList.AddLast(Chunk);
                    }
                });
            }
        }

        protected override void End() { }

        private void UpdateProgress()
        {
            Int32 UnprocessedCount;
            Int32 ChunkCount;

            using (TimedLock.Lock(ChunkLock))
            {
                UnprocessedCount = ChunkList.Where(X => X.IsProcessed()).Count();
                ChunkCount = ChunkList.Count();
            }

            PrefilterProgress.UpdateProgress((Double)UnprocessedCount / (Double)ChunkCount);

            if (PrefilterProgress.GetProgress() > 97)
                PrefilterProgress.FinishProgress();
        }

        internal class RegionProperties : NormalizedRegion
        {
            private UInt64? Checksum;
            private Boolean Changed;

            public RegionProperties(NormalizedRegion Region) : this(Region.BaseAddress, Region.RegionSize) { }
            public RegionProperties(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize)
            {
                Checksum = null;
                Changed = false;
            }

            public Boolean IsProcessed()
            {
                if (Checksum == null)
                    return false;

                return true;
            }

            public Boolean HasChanged()
            {
                return Changed;
            }

            public void Update(Byte[] Memory)
            {
                UInt64 CurrentChecksum;

                CurrentChecksum = Hashing.ComputeCheckSum(Memory);

                if (this.Checksum == null)
                {
                    this.Checksum = CurrentChecksum;
                    return;
                }

                Changed = this.Checksum != CurrentChecksum;
            }

        } // End class

    } // End class

} // End namespace