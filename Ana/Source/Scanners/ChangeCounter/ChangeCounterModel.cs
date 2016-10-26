namespace Ana.Source.Scanners.ChangeCounter
{
    using Results;
    using Snapshots;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;

    internal class ChangeCounterModel : ScannerBase
    {
        public ChangeCounterModel(Action updateScanCount) : base("Change Counter")
        {
            this.UpdateScanCount = updateScanCount;
            this.ProgressLock = new Object();
        }

        /// <summary>
        /// Gets or sets the snapshot being labeled with change counts
        /// </summary>
        private Snapshot<UInt16> Snapshot { get; set; }

        private UInt16 MinChanges { get; set; }

        private UInt16 MaxChanges { get; set; }

        private Object ProgressLock { get; set; }

        private Action UpdateScanCount { get; set; }

        public void SetMinChanges(UInt16 minChanges)
        {
            this.MinChanges = minChanges;
        }

        public void SetMaxChanges(UInt16 maxChanges)
        {
            this.MaxChanges = maxChanges;
        }

        public override void Begin()
        {
            // Initialize labeled snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot().CloneAs<UInt16>();

            if (this.Snapshot == null)
            {
                return;
            }

            this.Snapshot.ElementType = ScanResultsViewModel.GetInstance().ActiveType;
            this.Snapshot.Alignment = SettingsViewModel.GetInstance().Alignment;

            // Initialize change counts to zero
            this.Snapshot.SetElementLabels(0);

            base.Begin();
        }

        protected override void Update()
        {
            Int32 processedPages = 0;

            // Read memory to get current values
            this.Snapshot.ReadAllSnapshotMemory();

            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                (Object regionObject) =>
            {
                SnapshotRegion region = regionObject as SnapshotRegion;

                if (!region.CanCompare())
                {
                    return;
                }

                foreach (SnapshotElement<UInt16> element in region)
                {
                    if (element.Changed())
                    {
                        element.ElementLabel++;
                    }
                }

                using (TimedLock.Lock(ProgressLock))
                {
                    processedPages++;
                }
            });

            base.Update();
            this.UpdateScanCount?.Invoke();
        }

        protected override void OnEnd()
        {
            base.OnEnd();

            // Mark regions as valid or invalid based on number of changes
            /* 
             this.Snapshot.MarkAllInvalid();

             if (this.Snapshot.GetRegionCount() > 0)
              {
                  foreach (SnapshotRegion<UInt16> region in this.Snapshot)
                  {
                      foreach (SnapshotElement<UInt16> element in region)
                      {
                          if (element.ElementLabel.Value >= this.MinChanges && element.ElementLabel.Value <= this.MaxChanges)
                          {
                              element.Valid = true;
                          }
                      }
                  }
              }

              // Create a snapshot from the valid regions
              this.Snapshot.DiscardInvalidRegions();
              */

            this.Snapshot.ScanMethod = this.ScannerName;

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);
            //// Main.GetInstance().OpenLabelThresholder();
        }

        private void CleanUp()
        {
            this.Snapshot = null;
        }
    }
    //// End class
}
//// End namespace