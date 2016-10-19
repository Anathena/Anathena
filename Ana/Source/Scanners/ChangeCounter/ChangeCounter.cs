namespace Ana.Source.Scanners.ChangeCounter
{
    using Snapshots;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;

    internal class ChangeCounter : ScannerBase
    {
        public ChangeCounter() : base("Change Counter")
        {
            this.ProgressLock = new Object();
        }

        /// <summary>
        /// Gets or sets the snapshot being labeled with change counts
        /// </summary>
        private Snapshot<UInt16> Snapshot { get; set; }

        private UInt16 MinChanges { get; set; }

        private UInt16 MaxChanges { get; set; }

        private Int32 VariableSize { get; set; }

        private Object ProgressLock { get; set; }

        public void SetMinChanges(UInt16 minChanges)
        {
            this.MinChanges = minChanges;
        }

        public void SetMaxChanges(UInt16 maxChanges)
        {
            this.MaxChanges = maxChanges;
        }

        public void SetVariableSize(Int32 variableSize)
        {
            this.VariableSize = variableSize;
        }

        public override void Begin()
        {
            // Initialize labeled snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot().CloneAs<UInt16>();

            if (this.Snapshot == null)
            {
                return;
            }

            this.Snapshot.SetVariableSize(this.VariableSize);
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
                SnapshotRegion region = (SnapshotRegion)regionObject;

                if (!region.CanCompare())
                {
                    return;
                }

                foreach (SnapshotElement<UInt16> Element in region)
                {
                    if (Element.Changed())
                    {
                        Element.ElementLabel++;
                    }
                }

                using (TimedLock.Lock(ProgressLock))
                {
                    processedPages++;
                }
            });
        }

        protected override void End()
        {
            base.End();

            // Mark regions as valid or invalid based on number of changes
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