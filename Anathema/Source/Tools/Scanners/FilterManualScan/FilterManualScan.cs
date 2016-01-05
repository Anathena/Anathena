using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class FilterManualScan : IFilterManualScanModel
    {
        // Snapshot being labeled with change counts
        private Snapshot Snapshot;

        // User controlled variables
        private ScanConstraints ScanConstraints;

        public FilterManualScan()
        {
            ScanConstraints = new ScanConstraints();
        }

        public override void AddConstraint(ValueConstraintsEnum ValueConstraint, Type ElementType, dynamic Value)
        {
            ScanConstraints.AddConstraint(new ScanConstraintItem(ValueConstraint, ElementType, Value));
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
            FilterManualScanEventArgs FilterManualScanEventArgs = new FilterManualScanEventArgs();
            FilterManualScanEventArgs.ScanConstraints = ScanConstraints;
            OnEventUpdateDisplay(FilterManualScanEventArgs);
        }

        public override void BeginScan()
        {
            // Initialize snapshot
            Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());

            Snapshot.MarkAllValid();

            base.BeginScanRunOnce();
        }

        protected override void UpdateScan()
        {
            // Read memory to get current values
            Snapshot.ReadAllMemory();

            // Enforce each value constraint
            foreach (ScanConstraintItem ScanConstraint in ScanConstraints)
            {
                Parallel.ForEach(Snapshot.Cast<Object>(), (RegionObject) =>
                {
                    SnapshotRegion Region = (SnapshotRegion)RegionObject;

                    if (!Region.CanCompare())
                        return;

                    Region.SetElementType(ScanConstraint.ElementType);

                    foreach (SnapshotElement Element in Region)
                    {
                        if (!Element.Valid)
                            continue;

                        Element.ElementType = ScanConstraint.ElementType;

                        switch (ScanConstraint.ValueConstraints)
                        {
                            case ValueConstraintsEnum.Unchanged:
                                if (!Element.Unchanged())
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.Changed:
                                if (!Element.Changed())
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.Increased:
                                if (!Element.Increased())
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.Decreased:
                                if (!Element.Decreased())
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.IncreasedByX:
                                if (!Element.IncreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.DecreasedByX:
                                if (!Element.DecreasedByValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.Equal:
                                if (!Element.EqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.NotEqual:
                                if (!Element.NotEqualToValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.GreaterThan:
                                if (!Element.GreaterThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                            case ValueConstraintsEnum.LessThan:
                                if (!Element.LessThanValue(ScanConstraint.Value))
                                    Element.Valid = false;
                                break;
                        }

                    } // End foreach Element

                }); // End foreach Region

            } // End foreach Constraint
        }

        public override void EndScan()
        {
            // base.EndScan();

            Snapshot FilteredSnapshot = new Snapshot(Snapshot.GetValidRegions());
            FilteredSnapshot.SetScanMethod("Manual Scan");
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

    } // End class

} // End namespace