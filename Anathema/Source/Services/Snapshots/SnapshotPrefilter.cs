using Anathema.Scanners;
using Anathema.Services.ProcessManager;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Extensions;
using Anathema.User.UserSettings;
using Anathema.Utils;
using Anathema.Utils.Extensions;
using Anathema.Utils.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anathema.Services.Snapshots
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
    class SnapshotPrefilter : RepeatedTask, IProcessObserver
    {
        // Singleton instance of prefilter
        private static Lazy<SnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<SnapshotPrefilter>(() => { return new SnapshotPrefilter(); });

        private OSInterface OSInterface;

        private const Int32 ChunkLimit = 65536;
        private const Int32 ChunkSize = 2048;
        private const Int32 RescanTime = 200;

        private Queue<RegionProperties> ChunkQueue;
        private Object QueueLock;
        private Object ElementLock;

        private ProgressItem PrefilterProgress;

        private SnapshotPrefilter()
        {
            ChunkQueue = new Queue<RegionProperties>();
            QueueLock = new Object();
            ElementLock = new Object();

            PrefilterProgress = new ProgressItem();
            PrefilterProgress.SetProgressLabel("Analyzing");
            PrefilterProgress.SetCompletionThreshold(0.97);
            PrefilterProgress.RestrictProgress();

            InitializeProcessObserver();
        }
        
        public static SnapshotPrefilter GetInstance()
        {
            return SnapshotPrefilterInstance.Value;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            // Clear processing queue on process update
            using (TimedLock.Lock(QueueLock))
            // lock (QueueLock)
            {
                this.OSInterface = OSInterface;
                ChunkQueue = new Queue<RegionProperties>();
            }
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            using (TimedLock.Lock(QueueLock))
            // lock (QueueLock)
            {
                List<SnapshotRegion> Regions = new List<SnapshotRegion>();
                foreach (RegionProperties VirtualPage in ChunkQueue)
                {
                    if (VirtualPage.HasChanged())
                    {
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
            using (TimedLock.Lock(QueueLock))
            // lock (QueueLock)
            {
                List<RegionProperties> NewRegions = new List<RegionProperties>();

                // Gather current regions from the target process
                IEnumerable<NormalizedRegion> QueriedVirtualRegions = SnapshotManager.GetInstance().CollectSnapshotRegions();
                List<NormalizedRegion> QueriedChunkedRegions = new List<NormalizedRegion>();

                // Chunk all virtual regions into a standardized size
                QueriedVirtualRegions.ForEach(x => QueriedChunkedRegions.AddRange(x.ChunkNormalizedRegion(ChunkSize)));

                // Sort our lists (descending)
                IOrderedEnumerable<RegionProperties> CurrentRegionsSorted = ChunkQueue.OrderByDescending(X => X.BaseAddress.ToUInt64());
                IOrderedEnumerable<NormalizedRegion> QueriedRegionsSorted = QueriedChunkedRegions.OrderByDescending(X => X.BaseAddress.ToUInt64());

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
        }

        private void ProcessPages(IEnumerable<RegionProperties> NewPages)
        {
            using (TimedLock.Lock(QueueLock))
            // lock (QueueLock)
            {
                UpdateProgress(NewPages);

                // Construct queue
                ChunkQueue = new Queue<RegionProperties>();
                IOrderedEnumerable<RegionProperties> QueriedRegionsSorted = NewPages.OrderBy(X => X.GetLastUpdatedTimeStamp());
                QueriedRegionsSorted.ForEach(X => ChunkQueue.Enqueue(X));

                // Process the allowed amount of chunks from the priority queue
                Parallel.For(0, Math.Min(ChunkQueue.Count, ChunkLimit), Index =>
                {
                    Boolean Success;
                    RegionProperties RegionProperties;

                    // Search for next page that needs processing
                    using (TimedLock.Lock(ElementLock))
                    // lock (ElementLock)
                    {
                        do
                        {
                            if (ChunkQueue.Count <= 0)
                                return;

                            RegionProperties = ChunkQueue.Dequeue();
                            ChunkQueue.Enqueue(RegionProperties);

                        } while (RegionProperties.HasChanged());
                    }

                    if (RegionProperties.HasChanged())
                        return;

                    Byte[] PageData = OSInterface.Process.ReadBytes(RegionProperties.BaseAddress, RegionProperties.RegionSize, out Success);

                    if (!Success)
                        return;

                    RegionProperties.Update(PageData);
                });
            }
        }

        public override void End()
        {
            base.End();
        }

        private void UpdateProgress(IEnumerable<RegionProperties> NewPages)
        {
            Int32 UnprocessedCount = 0;

            UnprocessedCount = NewPages.Where(X => X.IsProcessed()).Count();

            PrefilterProgress.UpdateProgress((Double)UnprocessedCount / (Double)NewPages.Count());
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