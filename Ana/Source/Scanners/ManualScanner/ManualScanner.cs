using Ana.Source.Scanners.ScanConstraints;
using Ana.Source.Snapshots;
using Ana.Source.UserSettings;
using Ana.Source.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ana.Source.Scanners.ManualScanner
{
    class ManualScanner : ScannerBase
    {
        private Snapshot<Null> Snapshot;

        private ScanConstraintManager ScanConstraintManager;
        private Object ProgressLock;

        public ManualScanner() : base("Manual Scan")
        {
            ProgressLock = new Object();
        }

        public void SetScanConstraintManager(ScanConstraintManager ScanConstraintManager)
        {
            this.ScanConstraintManager = ScanConstraintManager;
        }

        public override void Begin()
        {
            // Initialize snapshot
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot());

            if (Snapshot == null || ScanConstraintManager == null || ScanConstraintManager.GetCount() <= 0)
            {
                TriggerEnd();
                return;
            }

            Snapshot.MarkAllValid();
            Snapshot.SetElementType(ScanConstraintManager.GetElementType());
            Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Int32 ProcessedPages = 0;

            // Read memory to get current values
            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;
                Boolean Success;


                Region.ReadAllRegionMemory(out Success, true);

                if (!Success)
                {
                    Region.MarkAllInvalid();
                    return;
                }

                if (ScanConstraintManager.HasRelativeConstraint() && !Region.CanCompare())
                {
                    Region.MarkAllInvalid();
                    return;
                }

                foreach (SnapshotElement Element in Region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint ScanConstraint in ScanConstraintManager)
                    {
                        switch (ScanConstraint.Constraint)
                        {
                            case ConstraintsEnum.Unchanged:
                                if (!Element.Unchanged())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Changed:
                                if (!Element.Changed())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Increased:
                                if (!Element.Increased())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Decreased:
                                if (!Element.Decreased())
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.IncreasedByX:
                                if (!Element.IncreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (!Element.DecreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.Equal:
                                if (!Element.EqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!Element.NotEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!Element.GreaterThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!Element.GreaterThanOrEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.LessThan:
                                if (!Element.LessThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!Element.LessThanOrEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ConstraintsEnum.NotScientificNotation:
                                if (Element.IsScientificNotation())
                                    Element.Valid = false;
                                break;
                        }

                    } // End foreach Constraint

                } // End foreach Element

                using (TimedLock.Lock(ProgressLock))
                {
                    ProcessedPages++;
                }

            }); // End foreach Region

            CancelFlag = true;
        }

        protected override void End()
        {
            base.End();

            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod(this.ScannerName);

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            CleanUp();
        }

        private void CleanUp()
        {
            Snapshot = null;
        }

    } // End class

} // End namespace