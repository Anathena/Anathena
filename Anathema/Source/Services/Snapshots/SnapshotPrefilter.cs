using System;
using System.Collections.Generic;
using Anathema.Utils.OS;
using Anathema.Utils;
using Anathema.Services.ProcessManager;
using Anathema.Services.Snapshots;
using Anathema.Scanners;
using Anathema.Utils.Extensions;
using System.Linq;

namespace Anathema.Services.Snapshots
{
    /// <summary>
    /// SnapshotPrefilter is a heuristic process that drastically improves scan speed. It capitalizes on the fact that
    /// > 95% of memory remains constant in most processes, at least for a short time span. Users will
    /// likely be hunting variables that are in the remaining 5%, which Divine will isolate.
    /// 
    /// This is a heuristic because it assumes that the variable, or one on the same page, has changed before the user
    /// requests a snapshot of the target processes memory
    /// 
    /// Steps are as follows:
    /// REPEAT INDEFINITELY:
    /// 1) Query all memory pages
    /// 2) Compare to current pages. If a new page, add to current with an infinite timestamp. If an existing page, do nothing
    /// 3) Load balance the current pages. Organize by timestamp. Grab some portion of the pages (fixed amount, size, or %).
    ///     Do not re-process pages with a last processed time greater than some threshold (ie 15 seconds)
    /// 4) Fetch memory from these pages performing a checksum.
    /// 5) Compare to old checksum. If changed, mark page as a dynamic page. If unchanged, mark it as a static page.
    /// </summary>
    class SnapshotPrefilter : RepeatedTask, IProcessObserver
    {
        private static SnapshotPrefilter SnapshotPrefilterInstance;

        private static TimeSpan PageTimeout = TimeSpan.FromSeconds(15);
        private static Int32 PageLimit = 64;

        private OSInterface OSInterface;

        private List<RegionProperties> VirtualPages;

        private SnapshotPrefilter()
        {
            VirtualPages = new List<RegionProperties>();

            InitializeProcessObserver();
        }

        public static SnapshotPrefilter GetInstance()
        {
            if (SnapshotPrefilterInstance == null)
                SnapshotPrefilterInstance = new SnapshotPrefilter();
            return SnapshotPrefilterInstance;
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        public Snapshot GetPrefilteredSnapshot()
        {
            List<SnapshotRegion> Regions = new List<SnapshotRegion>();
            foreach (RegionProperties VirtualPage in VirtualPages)
                if (VirtualPage.HasChanged())
                    Regions.Add(new SnapshotRegion<Null>(VirtualPage));

            return new Snapshot<Null>(Regions);
        }

        public override void Begin()
        {
            this.WaitTime = 200;
            base.Begin();
        }

        protected override void Update()
        {
            if (OSInterface == null)
                return;

            UpdatePages();
            ProcessPages();
        }

        /// <summary>
        /// Queries virtual pages from the OS to dertermine if any allocations or deallocations have happened
        /// </summary>
        private void UpdatePages()
        {
            List<RegionProperties> NewRegions = new List<RegionProperties>();
            Stack<NormalizedRegion> QueriedRegions = new Stack<NormalizedRegion>(SnapshotManager.GetInstance().CollectSnapshotRegions());
            Stack<RegionProperties> CurrentRegions = new Stack<RegionProperties>();

            VirtualPages.ForEach(X => CurrentRegions.Push(X));

            // Hack to ensure the stacks are in order (as at this point they are reversed)
            QueriedRegions = new Stack<NormalizedRegion>(QueriedRegions);
            CurrentRegions = new Stack<RegionProperties>(CurrentRegions);

            NormalizedRegion QueriedRegion = QueriedRegions.Count == 0 ? null : QueriedRegions.Pop();
            RegionProperties CurrentRegion = CurrentRegions.Count == 0 ? null : CurrentRegions.Pop();

            // Update list of current regions in the target process we are tracking
            while (QueriedRegions.Count > 0 && CurrentRegions.Count > 0)
            {
                // New region we have not seen yet
                if (QueriedRegion.BaseAddress.ToUInt64() < CurrentRegion.BaseAddress.ToUInt64())
                {
                    NewRegions.Add(new RegionProperties(QueriedRegion));

                    QueriedRegion = QueriedRegions.Pop();
                }
                // Region we have already seen
                else if (QueriedRegion.BaseAddress.ToUInt64() == CurrentRegion.BaseAddress.ToUInt64())
                {
                    NewRegions.Add(CurrentRegion);

                    QueriedRegion = QueriedRegions.Pop();
                    CurrentRegion = CurrentRegions.Pop();
                }
                // Region that went missing (deallocated)
                else if (QueriedRegion.BaseAddress.ToUInt64() > CurrentRegion.BaseAddress.ToUInt64())
                {
                    CurrentRegion = CurrentRegions.Pop();
                }
            }

            // Add remaining queried regions
            while (QueriedRegions.Count > 0)
            {
                NewRegions.Add(new RegionProperties(QueriedRegion));
                QueriedRegion = QueriedRegions.Pop();
            }

            this.VirtualPages = NewRegions;
        }

        private void ProcessPages()
        {
            Int32 ProcessedCount = 0;
            DateTime Now = DateTime.Now;

            foreach (RegionProperties RegionProperties in VirtualPages)
            {
                if (Now - RegionProperties.GetLastUpdatedTimeStamp() > PageTimeout)
                {
                    Boolean Success;
                    Byte[] PageData = OSInterface.Process.ReadBytes(RegionProperties.BaseAddress, RegionProperties.RegionSize, out Success);

                    if (!Success)
                        continue;

                    RegionProperties.Update(PageData);
                    ProcessedCount++;
                }

                if (ProcessedCount > PageLimit)
                    break;
            }
        }

        public override void End()
        {
            base.End();
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