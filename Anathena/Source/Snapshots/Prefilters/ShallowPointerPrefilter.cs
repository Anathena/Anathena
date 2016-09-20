using Ana.Source.Engine;
using Ana.Source.Engine.OperatingSystems;
using Ana.Source.Engine.Processes;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using Ana.Source.Utils.DataStructures;
using Ana.Source.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ana.Source.Snapshots.Prefilter
{
    /// <summary>
    /// Class to collect all pointers in the module base of the target process, and slowly grow from there
    /// 
    /// The growth radius will be small, so only a small subset of the processes memory should be explored
    /// </summary>
    class ShallowPointerPrefilter : RepeatedTask, ISnapshotPrefilter, IProcessObserver
    {
        // Singleton instance of Prefilter
        private static Lazy<ISnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(() => { return new ShallowPointerPrefilter(); }, LazyThreadSafetyMode.PublicationOnly);

        private EngineCore EngineCore;

        private const Int32 PointerRadius = 2048;
        private const Int32 RegionLimit = 8192;
        private const Int32 RescanTime = 4096;
        private const Int32 CompletionThreshold = 97;

        private Int64 ProcessedCount;

        private Snapshot<Null> FilteredSnapshot;

        private Object RegionLock;

        private ProgressItem PrefilterProgress;

        private ShallowPointerPrefilter()
        {
            FilteredSnapshot = new Snapshot<Null>();

            RegionLock = new Object();
            ProcessedCount = 0;

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
                FilteredSnapshot.ClearSnapshotRegions();
                ProcessedCount = 0;
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
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            Snapshot<Null> Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().CollectSnapshot(UseSettings: false, UsePrefilter: false));
            dynamic InvalidPointerMin = EngineCore.Memory.IsProcess32Bit() ? (UInt32)UInt16.MaxValue : (UInt64)UInt16.MaxValue;
            dynamic InvalidPointerMax = EngineCore.Memory.IsProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;
            ConcurrentHashSet<IntPtr> FoundPointers = new ConcurrentHashSet<IntPtr>();

            // Add static bases
            List<SnapshotRegion<Null>> BaseRegions = new List<SnapshotRegion<Null>>();
            foreach (NormalizedModule NormalizedModule in EngineCore.Memory.GetModules())
                BaseRegions.Add(new SnapshotRegion<Null>(NormalizedModule.BaseAddress, NormalizedModule.RegionSize));
            FilteredSnapshot.AddSnapshotRegions(BaseRegions);

            using (TimedLock.Lock(RegionLock))
            {
                List<SnapshotRegion> FilteredRegions = new List<SnapshotRegion>(FilteredSnapshot.GetSnapshotRegions().OrderBy(X => X.TimeSinceLastRead));

                // Process the allowed amount of chunks from the priority queue
                Parallel.For(0, Math.Min(FilteredRegions.Count, RegionLimit), Index =>
                {
                    Interlocked.Increment(ref ProcessedCount);

                    SnapshotRegion Region = FilteredRegions[Index];
                    Boolean Success;

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
                            FoundPointers.Add(Value);
                    }

                    // Clear the saved values, we do not need them now
                    Region.SetCurrentValues(null);
                });

                List<SnapshotRegion<Null>> FoundRegions = new List<SnapshotRegion<Null>>();
                foreach (IntPtr Pointer in FoundPointers)
                    FoundRegions.Add(new SnapshotRegion<Null>(Pointer.Subtract(PointerRadius), PointerRadius * 2));

                FilteredSnapshot.AddSnapshotRegions(FoundRegions);
                ProcessedCount = Math.Max(ProcessedCount, FilteredSnapshot.GetRegionCount());
            }
        }

        protected override void End()
        {
            base.End();
        }

        private void UpdateProgress()
        {
            Int32 RegionCount = 1;

            using (TimedLock.Lock(RegionLock))
            {
                if (FilteredSnapshot != null)
                    RegionCount = Math.Max(RegionCount, FilteredSnapshot.GetRegionCount());
            }

            PrefilterProgress.UpdateProgress((Double)ProcessedCount / (Double)RegionCount);

            if (PrefilterProgress.GetProgress() > CompletionThreshold)
                PrefilterProgress.FinishProgress();
        }

    } // End class

} // End namespace