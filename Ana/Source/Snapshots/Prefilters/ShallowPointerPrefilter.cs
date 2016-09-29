using Ana.Source.Engine;
using Ana.Source.Engine.OperatingSystems;
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
    class ShallowPointerPrefilter : RepeatedTask, ISnapshotPrefilter
    {
        // Singleton instance of Prefilter
        private static Lazy<ISnapshotPrefilter> SnapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(
            () => { return new ShallowPointerPrefilter(); },
            LazyThreadSafetyMode.PublicationOnly);

        private const Int32 PointerRadius = 2048;
        private const Int32 RegionLimit = 8192;
        private const Int32 RescanTime = 4096;
        private const Int32 CompletionThreshold = 97;

        /// <summary>
        /// 
        /// </summary>
        private Int64 ProcessedCount;

        private ShallowPointerPrefilter()
        {
            this.FilteredSnapshot = new Snapshot<Null>();
            this.RegionLock = new Object();
            this.ProcessedCount = 0;

            this.FilteredSnapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());
        }

        /// <summary>
        /// 
        /// </summary>
        private Object RegionLock { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Snapshot<Null> FilteredSnapshot { get; set; }

        public static ISnapshotPrefilter GetInstance()
        {
            return ShallowPointerPrefilter.SnapshotPrefilterInstance.Value;
        }


        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            lock (this.RegionLock)
            {
                return new Snapshot<Null>(this.FilteredSnapshot);
            }
        }

        public override void Begin()
        {
            this.UpdateInterval = RescanTime;
            base.Begin();
        }

        protected override void Update()
        {
            this.ProcessPages();
            this.UpdateProgress();
        }

        /// <summary>
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            Snapshot<Null> snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false));
            dynamic invalidPointerMin = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? (UInt32)UInt16.MaxValue : (UInt64)UInt16.MaxValue;
            dynamic invalidPointerMax = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? Int32.MaxValue : Int64.MaxValue;
            ConcurrentHashSet<IntPtr> foundPointers = new ConcurrentHashSet<IntPtr>();

            // Add static bases
            List<SnapshotRegion<Null>> baseRegions = new List<SnapshotRegion<Null>>();
            foreach (NormalizedModule normalizedModule in EngineCore.GetInstance().OperatingSystemAdapter.GetModules())
            {
                baseRegions.Add(new SnapshotRegion<Null>(normalizedModule.BaseAddress, normalizedModule.RegionSize));
            }

            this.FilteredSnapshot.AddSnapshotRegions(baseRegions);

            using (TimedLock.Lock(RegionLock))
            {
                List<SnapshotRegion> filteredRegions = new List<SnapshotRegion>(this.FilteredSnapshot.GetSnapshotRegions().OrderBy(x => x.TimeSinceLastRead));

                // Process the allowed amount of chunks from the priority queue
                Parallel.For(0, Math.Min(filteredRegions.Count, RegionLimit), Index =>
                {
                    Interlocked.Increment(ref ProcessedCount);

                    SnapshotRegion region = filteredRegions[Index];
                    Boolean success;

                    // Set to type of a pointer
                    region.SetElementType(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? typeof(UInt32) : typeof(UInt64));

                    // Enforce 4-byte alignment of pointers
                    region.SetAlignment(sizeof(Int32));

                    // Read current page data for chunk
                    region.ReadAllRegionMemory(out success);

                    // Read failed; Deallocated page
                    if (!success)
                    {
                        return;
                    }

                    if (!region.HasValues())
                    {
                        return;
                    }

                    foreach (SnapshotElement element in region)
                    {
                        // Enforce user mode memory pointers
                        if (element.LessThanValue(invalidPointerMin) || element.GreaterThanValue(invalidPointerMax))
                        {
                            continue;
                        }

                        // Enforce 4-byte alignment of destination
                        if (element.GetValue() % 4 != 0)
                        {
                            continue;
                        }

                        IntPtr Value = new IntPtr(element.GetValue());

                        // Check if it is possible that this pointer is valid, if so keep it
                        if (snapshot.ContainsAddress(Value))
                        {
                            foundPointers.Add(Value);
                        }
                    }

                    // Clear the saved values, we do not need them now
                    region.SetCurrentValues(null);
                });

                List<SnapshotRegion<Null>> foundRegions = new List<SnapshotRegion<Null>>();
                foreach (IntPtr pointer in foundPointers)
                {
                    foundRegions.Add(new SnapshotRegion<Null>(pointer.Subtract(PointerRadius), PointerRadius * 2));
                }

                this.FilteredSnapshot.AddSnapshotRegions(foundRegions);
                this.ProcessedCount = Math.Max(this.ProcessedCount, this.FilteredSnapshot.GetRegionCount());
            }
        }

        protected override void End()
        {
            base.End();
        }

        private void UpdateProgress()
        {
            Int32 regionCount = 1;

            using (TimedLock.Lock(this.RegionLock))
            {
                if (this.FilteredSnapshot != null)
                {
                    regionCount = Math.Max(regionCount, this.FilteredSnapshot.GetRegionCount());
                }
            }
        }
    }
    //// End class
}
//// End namespace