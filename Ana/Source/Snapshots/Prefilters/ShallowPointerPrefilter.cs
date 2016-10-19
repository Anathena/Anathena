namespace Ana.Source.Snapshots.Prefilter
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
    using Utils.DataStructures;
    using Utils.Extensions;

    /// <summary>
    /// Class to collect all pointers in the module base of the target process, and slowly grow from there.
    /// The growth radius will be small, so only a small subset of the processes memory should be explored
    /// </summary>
    internal class ShallowPointerPrefilter : RepeatedTask, ISnapshotPrefilter
    {
        private const Int32 PointerRadius = 2048;
        private const Int32 RegionLimit = 8192;
        private const Int32 RescanTime = 4096;
        private const Int32 CompletionThreshold = 97;

        /// <summary>
        /// Singleton instance of the <see cref="ShallowPointerPrefilter"/> class
        /// </summary>
        private static Lazy<ISnapshotPrefilter> snapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(
            () => { return new ShallowPointerPrefilter(); },
            LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets or sets the number of regions processed by this prefilter
        /// </summary>
        private Int64 processedCount;

        private ShallowPointerPrefilter()
        {
            this.FilteredSnapshot = new Snapshot<Null>();
            this.RegionLock = new Object();
            this.processedCount = 0;

            this.FilteredSnapshot.Alignment = SettingsViewModel.GetInstance().GetAlignmentSettings();
        }

        /// <summary>
        /// Gets or sets a lock for accessing snapshot regions
        /// </summary>
        private Object RegionLock { get; set; }

        /// <summary>
        /// Gets or sets the snapshot constructed by this prefilter
        /// </summary>
        private Snapshot<Null> FilteredSnapshot { get; set; }

        public static ISnapshotPrefilter GetInstance()
        {
            return ShallowPointerPrefilter.snapshotPrefilterInstance.Value;
        }

        public void BeginPrefilter()
        {
            this.Begin();
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            lock (this.RegionLock)
            {
                return this.FilteredSnapshot.Clone();
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

        protected override void End()
        {
            base.End();
        }

        /// <summary>
        /// Processes all pages, computing checksums to determine chunks of virtual pages that have changed
        /// </summary>
        private void ProcessPages()
        {
            Snapshot snapshot = SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false).Clone();
            Boolean isOpenedProcess32Bit = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit();
            dynamic invalidPointerMin = isOpenedProcess32Bit ? (UInt32)UInt16.MaxValue : (UInt64)UInt16.MaxValue;
            dynamic invalidPointerMax = isOpenedProcess32Bit ? Int32.MaxValue : Int64.MaxValue;
            ConcurrentHashSet<IntPtr> foundPointers = new ConcurrentHashSet<IntPtr>();

            // Add static bases
            List<SnapshotRegion<Null>> baseRegions = new List<SnapshotRegion<Null>>();
            foreach (NormalizedModule normalizedModule in EngineCore.GetInstance().OperatingSystemAdapter.GetModules())
            {
                baseRegions.Add(new SnapshotRegion<Null>(normalizedModule.BaseAddress, normalizedModule.RegionSize));
            }

            this.FilteredSnapshot.AddSnapshotRegions(baseRegions);

            using (TimedLock.Lock(this.RegionLock))
            {
                List<SnapshotRegion> filteredRegions = new List<SnapshotRegion>(this.FilteredSnapshot.GetSnapshotRegions().OrderBy(x => x.TimeSinceLastRead));

                // Process the allowed amount of chunks from the priority queue
                Parallel.For(
                    0,
                    Math.Min(filteredRegions.Count, ShallowPointerPrefilter.RegionLimit),
                    Index =>
                    {
                        Interlocked.Increment(ref processedCount);

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
                            if (element.GetCurrentValue() % 4 != 0)
                            {
                                continue;
                            }

                            IntPtr Value = new IntPtr(element.GetCurrentValue());

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
                    foundRegions.Add(new SnapshotRegion<Null>(pointer.Subtract(ShallowPointerPrefilter.PointerRadius), ShallowPointerPrefilter.PointerRadius * 2));
                }

                this.FilteredSnapshot.AddSnapshotRegions(foundRegions);
                this.processedCount = Math.Max(this.processedCount, this.FilteredSnapshot.GetRegionCount());
            }
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