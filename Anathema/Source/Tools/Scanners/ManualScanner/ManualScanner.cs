using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class ManualScanner : IManualScannerModel
    {
        // Snapshot being labeled with change counts
        private Snapshot<Null> Snapshot;

        // User controlled variables
        private ScanConstraintManager ScanConstraints;

        public ManualScanner()
        {
            ScanConstraints = new ScanConstraintManager();
        }

        public override void SetElementType(Type ElementType)
        {
            ScanConstraints.SetElementType(ElementType);
            UpdateDisplay();
        }

        public override Type GetElementType()
        {
            return ScanConstraints.GetElementType();
        }

        public override void AddConstraint(ConstraintsEnum ValueConstraint, dynamic Value)
        {
            ScanConstraints.AddConstraint(new ScanConstraint(ValueConstraint, Value));
            UpdateDisplay();
        }

        public override void RemoveConstraints(Int32[] ConstraintIndicies)
        {
            ScanConstraints.RemoveConstraints(ConstraintIndicies);
            UpdateDisplay();
        }

        public override void ClearConstraints()
        {
            ScanConstraints.ClearConstraints();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            ManualScannerEventArgs FilterManualScanEventArgs = new ManualScannerEventArgs();
            FilterManualScanEventArgs.ScanConstraints = ScanConstraints;
            OnEventUpdateDisplay(FilterManualScanEventArgs);
        }

        public override void Begin()
        {
            // Initialize snapshot
            Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot());

            if (Snapshot == null)
                return;

            Snapshot.MarkAllValid();
            Snapshot.SetElementType(ScanConstraints.GetElementType());

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            // Read memory to get current values
            Snapshot.ReadAllSnapshotMemory();

            Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;

                if (!Region.CanCompare())
                    return;

                foreach (SnapshotElement Element in Region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint ScanConstraint in ScanConstraints)
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

            }); // End foreach Region
            
            CancelFlag = true;
        }

        public override void End()
        {
            base.End();
            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Manual Scan");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            OnEventScanFinished(new ManualScannerEventArgs());

            CleanUp();
        }

        private void CleanUp()
        {
            Snapshot = null;
        }

    } // End class

} // End namespace