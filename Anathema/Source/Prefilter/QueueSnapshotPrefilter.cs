using Anathema.Source.OS;
using Anathema.Source.OS.Processes;
using Anathema.Source.Scanners;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Setting;
using Anathema.Source.Utils.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anathema.Source.Prefilter
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
    class QueueSnapshotPrefilter : RepeatedTask, ISnapshotPrefilter, IProcessObserver
    {
        // Singleton instance of prefilter
        private static Lazy<ISnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(() => { return new QueueSnapshotPrefilter(); });

        private OSInterface OSInterface;

        private const Int32 ChunkLimit = 32768;
        private const Int32 ChunkSize = 4096;
        private const Int32 RescanTime = 400;

        private Queue<RegionProperties> ChunkQueue;
        private Object QueueLock;
        private Object ElementLock;

        private ProgressItem PrefilterProgress;

        private QueueSnapshotPrefilter()
        {
            ChunkQueue = new Queue<RegionProperties>();
            QueueLock = new Object();
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

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;

            // Clear processing queue on process update
            using (TimedLock.Lock(QueueLock))
            {
                ChunkQueue = new Queue<RegionProperties>();
            }
        }

        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            List<SnapshotRegion> Regions = new List<SnapshotRegion>();

            using (TimedLock.Lock(QueueLock))
            {
                foreach (RegionProperties VirtualPage in ChunkQueue)
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
            this.WaitTime = RescanTime;
            base.Begin();
        }

        protected override void Update()
        {
            if (OSInterface == null)
                return;

            ProcessPages(ResolvePages());
        }

        /// <summary>
        /// Queries virtual pages from the OS to dertermine if any allocations or deallocations have happened
        /// </summary>
        private IEnumerable<RegionProperties> ResolvePages()
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

            using (TimedLock.Lock(QueueLock))
            {
                CurrentRegionsSorted = ChunkQueue.OrderByDescending(X => X.BaseAddress.ToUInt64());
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
                    // Check for deallocation + reallocation of a different size
                    if (CurrentRegion.RegionSize != QueriedRegion.RegionSize)
                        NewRegions.Add(new RegionProperties(QueriedRegion));
                    else
                        NewRegions.Add(CurrentRegion);

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

        private void ProcessPages(IEnumerable<RegionProperties> NewPages)
        {
            UpdateProgress(NewPages);

            Queue<RegionProperties> OriginalQueue = ChunkQueue;

            // Construct queue
            Queue<RegionProperties> NewChunkQueue = new Queue<RegionProperties>();
            IOrderedEnumerable<RegionProperties> QueriedRegionsSorted = NewPages.OrderBy(X => X.GetLastUpdatedTimeStamp());
            QueriedRegionsSorted.ForEach(X => NewChunkQueue.Enqueue(X));

            // Process the allowed amount of chunks from the priority queue
            Parallel.For(0, Math.Min(NewChunkQueue.Count, ChunkLimit), Index =>
            {
                Boolean Success;
                RegionProperties RegionProperties;

                // Search for next page that needs processing
                using (TimedLock.Lock(ElementLock))
                {
                    do
                    {
                        if (NewChunkQueue.Count <= 0)
                            return;

                        RegionProperties = NewChunkQueue.Dequeue();
                        NewChunkQueue.Enqueue(RegionProperties);

                    } while (RegionProperties.HasChanged());
                }

                if (RegionProperties.HasChanged())
                    return;

                Byte[] PageData = OSInterface.Process.ReadBytes(RegionProperties.BaseAddress, RegionProperties.RegionSize, out Success);

                if (!Success)
                    return;

                RegionProperties.Update(PageData);
            });

            using (TimedLock.Lock(QueueLock))
            {
                if (OriginalQueue != ChunkQueue)
                    return;

                ChunkQueue = NewChunkQueue;
            }
        }

        protected override void End() { }

        private void UpdateProgress(IEnumerable<RegionProperties> NewPages)
        {
            Int32 UnprocessedCount = 0;

            UnprocessedCount = NewPages.Where(X => X.IsProcessed()).Count();

            PrefilterProgress.UpdateProgress((Double)UnprocessedCount / (Double)NewPages.Count());

            if (PrefilterProgress.GetProgress() > 97)
                PrefilterProgress.FinishProgress();
        }

        internal class RegionProperties : NormalizedRegion
        {
            private DateTime LastUpdatedTimeStamp;
            private UInt64? Checksum;
            private Boolean Changed;

            public RegionProperties(NormalizedRegion Region) : this(Region.BaseAddress, Region.RegionSize) { }
            public RegionProperties(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize)
            {
                LastUpdatedTimeStamp = DateTime.MinValue;
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

                LastUpdatedTimeStamp = DateTime.Now;
                CurrentChecksum = Hashing.ComputeCheckSum(Memory);

                if (this.Checksum == null)
                {
                    this.Checksum = CurrentChecksum;
                    return;
                }

                Changed = this.Checksum != CurrentChecksum;
            }

            public DateTime GetLastUpdatedTimeStamp()
            {
                return LastUpdatedTimeStamp;
            }

        } // End class

    } // End class

} // End namespace