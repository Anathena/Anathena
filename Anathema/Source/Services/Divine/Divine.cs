using System;
using System.Collections.Generic;
using Anathema.Utils.OS;
using Anathema.Utils;
using Anathema.Services.ProcessManager;
using Anathema.Services.Snapshots;
using Anathema.Scanners;

namespace Anathema.Services.Divine
{
    /// <summary>
    /// Divine is a heuristics engine that drastically improves scan speed. It capitalizes on the fact that
    /// > 95% of memory remains constant in most processes, at least for a short time span. Users will
    /// likely be hunting variables that are in the remaining 5%, which Divine will isolate.
    /// </summary>
    class Divine : IProcessObserver
    {
        private static Divine DivineInstance;

        private OSInterface OSInterface;

        private List<RegionProperties> VirtualPages;

        private PagePolling PagePollingTask;
        private UpdatePageStatus UpdatePageStatusTask;

        private Divine()
        {
            VirtualPages = new List<RegionProperties>();
            PagePollingTask = new PagePolling();
            UpdatePageStatusTask = new UpdatePageStatus();

            InitializeProcessObserver();

            // Start the heuristic tasks
            PagePollingTask.Begin();
            UpdatePageStatusTask.Begin();
        }

        public static Divine GetInstance()
        {
            if (DivineInstance == null)
                DivineInstance = new Divine();
            return DivineInstance;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        internal class RegionProperties : SnapshotRegion<Null>
        {
            public DateTime LastQuery;
            public UInt64? Checksum;
            public Boolean Valid;

            public RegionProperties(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize)
            {
                LastQuery = DateTime.MinValue;
                Checksum = null;
                Valid = false;
            }

        } // End class

        internal class PagePolling : RepeatedTask
        {
            public PagePolling()
            {

            }

            public override void Begin()
            {
                this.WaitTime = 200;
                base.Begin();
            }

            protected override void Update()
            {
                Divine Divine = Divine.GetInstance();

                if (Divine.OSInterface == null)
                    return;

                // Collect virtual pages
                List<NormalizedRegion> Regions = new List<NormalizedRegion>(Divine.OSInterface.Process.GetAllVirtualPages());

                // This does not have to be n^2 //

                // Determine which regions we have we get to keep
                foreach (RegionProperties SavedRegion in Divine.VirtualPages.ToArray())
                {
                    Boolean Present = false;

                    // Check if the saved region is present in the target's pages
                    foreach (NormalizedRegion Region in Regions)
                    {
                        if (SavedRegion.BaseAddress == Region.BaseAddress && SavedRegion.RegionSize == Region.RegionSize)
                        {
                            Present = true;
                        }
                    }

                    if (!Present)
                        Divine.VirtualPages.Remove(SavedRegion);
                }

                // Determine which regions we need to add
                foreach (NormalizedRegion Region in Regions)
                {
                    Boolean Present = false;

                    // Check if the saved region is present in the target's pages
                    foreach (RegionProperties SavedRegion in Divine.VirtualPages.ToArray())
                    {
                        if (SavedRegion.BaseAddress == Region.BaseAddress && SavedRegion.RegionSize == Region.RegionSize)
                        {
                            Present = true;
                        }
                    }

                    if (!Present)
                        Divine.VirtualPages.Add(new RegionProperties(Region.BaseAddress, Region.RegionSize));
                }
            }

            public override void End()
            {
                base.End();
            }

        } // End class

        internal class UpdatePageStatus : RepeatedTask
        {
            public UpdatePageStatus()
            {

            }

            public override void Begin()
            {
                this.WaitTime = 800;
                base.Begin();
            }

            protected override void Update()
            {
                Divine Divine = Divine.GetInstance();

                if (Divine.OSInterface == null)
                    return;

                Divine.VirtualPages.Sort();
                List<RegionProperties> NextBatch = new List<RegionProperties>();

                // Construct next batch to process
                foreach (RegionProperties SavedRegion in Divine.VirtualPages)
                {
                    if (NextBatch.Count > 10)
                        break;

                    NextBatch.Add(SavedRegion);
                }

                foreach (RegionProperties SavedRegion in NextBatch)
                {
                    Byte[] Values = SavedRegion.ReadAllSnapshotMemory(Divine.OSInterface, false);
                    UInt64 Checksum = Hashing.ComputeCheckSum(Values);

                    if (SavedRegion.Checksum == null)
                    {
                        SavedRegion.Checksum = Checksum;
                        continue;
                    }

                    if (SavedRegion.Checksum != Checksum)
                        SavedRegion.Valid = true;
                    else
                        SavedRegion.Valid = false;

                    SavedRegion.LastQuery = DateTime.Now;
                }
            }

            public override void End()
            {
                base.End();
            }

        } // End class

    } // End class

} // End namespace