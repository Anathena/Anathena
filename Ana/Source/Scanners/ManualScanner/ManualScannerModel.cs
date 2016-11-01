namespace Ana.Source.Scanners.ManualScanner
{
    using Results.ScanResults;
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
                this.End();
                return;
            }

            this.Snapshot.MarkAllValid();
            this.Snapshot.ElementType = ScanResultsViewModel.GetInstance().ActiveType;
            this.Snapshot.Alignment = SettingsViewModel.GetInstance().Alignment;

            base.Begin();
        }

        protected override void OnUpdate()
        {
            Int32 processedPages = 0;

            // Read memory to get current values
            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
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
                                if (!element.IncreasedByValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (!element.DecreasedByValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.Equal:
                                if (!element.EqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!element.NotEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!element.GreaterThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!element.GreaterThanOrEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThan:
                                if (!element.LessThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.Valid = false;
                                }

                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!element.LessThanOrEqualToValue(scanConstraint.ConstraintValue))
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

            base.OnUpdate();
            this.CancelFlag = true;
        }

        private void ScanRegion(SnapshotRegion region)
        {
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
                            if (!element.IncreasedByValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.DecreasedByX:
                            if (!element.DecreasedByValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.Equal:
                            if (!element.EqualToValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.NotEqual:
                            if (!element.NotEqualToValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.GreaterThan:
                            if (!element.GreaterThanValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.GreaterThanOrEqual:
                            if (!element.GreaterThanOrEqualToValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.LessThan:
                            if (!element.LessThanValue(scanConstraint.ConstraintValue))
                            {
                                element.Valid = false;
                            }

                            break;
                        case ConstraintsEnum.LessThanOrEqual:
                            if (!element.LessThanOrEqualToValue(scanConstraint.ConstraintValue))
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
        }

        protected override void OnEnd()
        {
            base.OnEnd();

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