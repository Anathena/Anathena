using Anathena.Source.Engine;
using Anathena.Source.Engine.OperatingSystems;
using Anathena.Source.Engine.Processes;
using Anathena.Source.UserSettings;
using Anathena.Source.Utils;
using Anathena.Source.Utils.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Anathena.Source.Snapshots.Prefilter
{
    class RadialPointerPrefilter : RepeatedTask, ISnapshotPrefilter, IProcessObserver
    {
        // Singleton instance of Prefilter
        private static Lazy<ISnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(() => { return new RadialPointerPrefilter(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private const Int32 PointerRadius = 1024;
        private const Int32 RegionLimit = 2096;
        private const Int32 RescanTime = 800;
        private const Int32 CompletionThreshold = 97;

        // private ConcurrentDictionary<IntPtr, IntPtr> PointerPool;
        private LinkedList<SnapshotRegion> RegionList;
        private Snapshot<Null> FilteredSnapshot;

        private Object RegionLock;
        private Object ElementLock;

        private ProgressItem PrefilterProgress;

        private RadialPointerPrefilter()
        {
            // PointerPool = new ConcurrentDictionary<IntPtr, IntPtr>();
            RegionList = new LinkedList<SnapshotRegion>();
            FilteredSnapshot = new Snapshot<Null>();

            RegionLock = new Object();
            ElementLock = new Object();

            PrefilterProgress = new ProgressItem();
            PrefilterProgress.SetProgressLabel("Prefiltering");
            PrefilterProgress.RestrictProgress();

            FilteredSnapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

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
            using (TimedLock.Lock(RegionLock))
            {
                // PointerPool = new ConcurrentDictionary<IntPtr, IntPtr>();
                RegionList = new LinkedList<SnapshotRegion>();
                FilteredSnapshot.ClearSnapshotRegions();
            }
        }

        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            lock (RegionLock)
            {
                List<SnapshotRegion<Null>> Regions = new List<SnapshotRegion<Null>>();

                // Static bases are also considered valid
                foreach (NormalizedModule NormalizedModule in EngineCore.Memory.GetModules())
                    Regions.Add(new SnapshotRegion<Null>(NormalizedModule.BaseAddress, NormalizedModule.RegionSize));

                FilteredSnapshot.AddSnapshotRegions(Regions);

                return new Snapshot<Null>(FilteredSnapshot);
            }
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
        private IEnumerable<NormalizedRegion> CollectNewPages()
        {
            List<NormalizedRegion> NewRegions = new List<NormalizedRegion>();

            // Gather current regions from the target process
            IEnumerable<NormalizedRegion> QueriedVirtualRegions = SnapshotManager.GetInstance().CollectSnapshotRegions();

            // Sort our lists (descending)
            IOrderedEnumerable<NormalizedRegion> QueriedRegionsSorted = QueriedVirtualRegions.OrderByDescending(X => X.BaseAddress.ToUInt64());
            IOrderedEnumerable<SnapshotRegion> CurrentRegionsSorted;

            using (TimedLock.Lock(RegionLock))
            {
                CurrentRegionsSorted = RegionList.OrderByDescending(X => X.BaseAddress.ToUInt64());
            }

            // Create comparison stacks
            Stack<NormalizedRegion> CurrentRegionStack = new Stack<NormalizedRegion>();
            Stack<NormalizedRegion> QueriedRegionStack = new Stack<NormalizedRegion>();

            CurrentRegionsSorted.ForEach(X => CurrentRegionStack.Push(X));
            QueriedRegionsSorted.ForEach(X => QueriedRegionStack.Push(X));

            // Begin stack based comparison algorithm to resolve our chunk processing queue
            NormalizedRegion QueriedRegion = QueriedRegionStack.Count == 0 ? null : QueriedRegionStack.Pop();
            NormalizedRegion CurrentRegion = CurrentRegionStack.Count == 0 ? null : CurrentRegionStack.Pop();

            while (QueriedRegionStack.Count > 0 && CurrentRegionStack.Count > 0)
            {
                // New region we have not seen yet
                if (QueriedRegion < CurrentRegion)
                {
                    NewRegions.Add(QueriedRegion);

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
                NewRegions.Add(QueriedRegion);
                QueriedRegion = QueriedRegionStack.Pop();
            }

            return NewRegions;
        }

        /// <summary>
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            // Check for newly allocated pages, adding them to the front (highest processing priority)
            using (TimedLock.Lock(RegionLock))
            {
                foreach (NormalizedRegion NewPage in CollectNewPages())
                    RegionList.AddFirst(new SnapshotRegion<Null>(NewPage));
            }

            Snapshot<Null> Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().CollectSnapshot(UseSettings: false, UsePrefilter: false));
            dynamic InvalidPointerMin = EngineCore.Memory.IsProcess32Bit() ? (UInt32)UInt16.MaxValue : (UInt64)UInt16.MaxValue;
            dynamic InvalidPointerMax = EngineCore.Memory.IsProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;
            ConcurrentBag<SnapshotRegion<Null>> FoundRegions = new ConcurrentBag<SnapshotRegion<Null>>();

            using (TimedLock.Lock(RegionLock))
            {
                // Process the allowed amount of chunks from the priority queue
                Parallel.For(0, Math.Min(RegionList.Count, RegionLimit), Index =>
                {
                    SnapshotRegion Region;
                    Boolean Success;

                    // Grab next available element
                    lock (ElementLock)
                    {
                        Region = RegionList.First();
                        RegionList.RemoveFirst();
                    }

                    // Set to type of a pointer
                    Region.SetElementType(EngineCore.Memory.IsProcess32Bit() ? typeof(UInt32) : typeof(UInt64));

                    // Enforce 4-byte alignment of pointers
                    Region.SetAlignment(sizeof(Int32));

                    // Read current page data for chunk
                    Region.ReadAllRegionMemory(EngineCore, out Success);

                    // Read failed; Deallocated page
                    if (!Success)
                        return;

                    if (!Region.HasValues())
                        return;

                    foreach (SnapshotElement Element in Region)
                    {
                        // Enforce user mode memory pointers
                        if (Element.LessThanValue(InvalidPointerMin) || Element.GreaterThanValue(InvalidPointerMax))
                            continue;

                        // Enforce 4-byte alignment of destination
                        if (Element.GetValue() % 4 != 0)
                            continue;

                        IntPtr Value = new IntPtr(Element.GetValue());

                        // Check if it is possible that this pointer is valid, if so keep it
                        if (Snapshot.ContainsAddress(Value))
                            FoundRegions.Add(new SnapshotRegion<Null>(Value, PointerRadius));
                        // PointerPool[Element.BaseAddress] = Value;
                    }

                    // Clear the saved values, we do not need them now
                    Region.SetCurrentValues(null);

                    // Put at the end of our processing list -- this region now has the lowest priority
                    using (TimedLock.Lock(ElementLock))
                    {
                        RegionList.AddLast(Region);
                    }
                });

                FilteredSnapshot.AddSnapshotRegions(FoundRegions);
            }
        }

        protected override void End()
        {
            base.End();
        }

        private void UpdateProgress()
        {
            Int32 UnprocessedCount;
            Int32 ChunkCount;

            using (TimedLock.Lock(RegionLock))
            {
                UnprocessedCount = 0;
                // UnprocessedCount = RegionList.Where(X => X.IsProcessed()).Count();
                ChunkCount = RegionList.Count();
            }

            PrefilterProgress.UpdateProgress((Double)UnprocessedCount / (Double)ChunkCount);

            if (PrefilterProgress.GetProgress() > CompletionThreshold)
                PrefilterProgress.FinishProgress();
        }

    } // End class

} // End namespace