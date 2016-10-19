namespace Ana.Source.Scanners.ManualScanner
{
    using ScanConstraints;
    using Snapshots;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;

    internal class ManualScannerModel : ScannerBase
    {
        public ManualScannerModel() : base("Manual Scan")
        {
            this.ProgressLock = new Object();
        }

        private Snapshot Snapshot { get; set; }

        private ScanConstraintManager ScanConstraintManager { get; set; }

        private Object ProgressLock { get; set; }

        public void SetScanConstraintManager(ScanConstraintManager scanConstraintManager)
        {
            this.ScanConstraintManager = scanConstraintManager;
        }

        public override void Begin()
        {
            // Initialize snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).Clone();

            if (this.Snapshot == null || this.ScanConstraintManager == null || this.ScanConstraintManager.GetCount() <= 0)
            {
                this.TriggerEnd();
                return;
            }

            this.Snapshot.MarkAllValid();
            this.Snapshot.ElementType = this.ScanConstraintManager.ElementType;
            this.Snapshot.Alignment = SettingsViewModel.GetInstance().GetAlignmentSettings();

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Int32 processedPages = 0;

            // Read memory to get current values
            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                (regionObject) =>
            {
                SnapshotRegion region = (SnapshotRegion)regionObject;
                Boolean readSuccess;

                region.ReadAllRegionMemory(out readSuccess, keepValues: true);

                if (!readSuccess)
                {
                    region.MarkAllInvalid();
                    return;
                }

                if (this.ScanConstraintManager.HasRelativeConstraint() && !region.CanCompare())
                {
                    region.MarkAllInvalid();
                    return;
                }

                foreach (SnapshotElement element in region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint scanConstraint in this.ScanConstraintManager)
                    {
                        switch (scanConstraint.Constraint)
                        {
                            case ConstraintsEnum.Unchanged:
                                if (!element.Unchanged())
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.Changed:
                                if (!element.Changed())
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.Increased:
                                if (!element.Increased())
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.Decreased:
                                if (!element.Decreased())
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.IncreasedByX:
                                if (!element.IncreasedByValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (!element.DecreasedByValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.Equal:
                                if (!element.EqualToValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!element.NotEqualToValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!element.GreaterThanValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!element.GreaterThanOrEqualToValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThan:
                                if (!element.LessThanValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!element.LessThanOrEqualToValue(scanConstraint.Value))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.NotScientificNotation:
                                if (element.IsScientificNotation())
                                {
                                    element.Valid = false;
                                }

                                break;
                        }
                    }
                    //// End foreach Constraint
                }
                //// End foreach Element
                using (TimedLock.Lock(this.ProgressLock))
                {
                    processedPages++;
                }
            });
            //// End foreach Region

            this.CancelFlag = true;
        }

        protected override void End()
        {
            base.End();

            this.Snapshot.DiscardInvalidRegions();
            this.Snapshot.ScanMethod = this.ScannerName;

            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);

            this.CleanUp();
        }

        private void CleanUp()
        {
            Snapshot = null;
        }
    }
    //// End class
}
//// End namespace