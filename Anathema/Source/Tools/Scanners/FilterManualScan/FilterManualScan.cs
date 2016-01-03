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
        private Snapshot FilteredSnapshot;

        // User controlled variables
        private ScanConstraints ScanConstraints;

        public FilterManualScan()
        {
            ScanConstraints = new ScanConstraints();
        }

        public override void AddConstraint(ValueConstraintsEnum ValueConstraint, Type ElementType, dynamic Value)
        {
            ScanConstraints.AddConstraint(new ScanConstraintItem(ValueConstraint, ElementType, Value));

            FilterManualScanEventArgs FilterManualScanEventArgs = new FilterManualScanEventArgs();
            FilterManualScanEventArgs.ScanConstraints = ScanConstraints;
            OnEventUpdateDisplay(FilterManualScanEventArgs);
        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            Snapshot ActiveSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();
            Snapshot = new Snapshot(ActiveSnapshot);

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            List<SnapshotRegion> FilteredElements = new List<SnapshotRegion>();

            // Read memory to get current values
            Snapshot.ReadAllMemory();

            foreach (SnapshotRegion Region in Snapshot)
            {
                foreach (SnapshotElement Element in Region)
                {
                    if (!Element.CanCompare())
                        continue;
                    
                    Boolean FailedConstraints = false;

                    // Enforce each value constraint
                    foreach (ScanConstraintItem ScanConstraint in ScanConstraints)
                    {
                        Element.ElementType = ScanConstraint.ElementType;

                        switch (ScanConstraint.ValueConstraints)
                        {
                            case ValueConstraintsEnum.Unchanged:
                                if (!Element.Unchanged())
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.Changed:
                                if (!Element.Changed())
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.Increased:
                                if (!Element.Increased())
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.Decreased:
                                if (!Element.Decreased())
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.IncreasedByX:
                                if (!Element.IncreasedByValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.DecreasedByX:
                                if (!Element.DecreasedByValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.Equal:
                                if (!Element.EqualToValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.NotEqual:
                                if (!Element.NotEqualToValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.GreaterThan:
                                if (!Element.GreaterThanValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                            case ValueConstraintsEnum.LessThan:
                                if (!Element.LessThanValue(ScanConstraint.Value))
                                    FailedConstraints = true;
                                break;
                        }

                        if (FailedConstraints)
                            break;
                    }

                    if (!FailedConstraints)
                        FilteredElements.Add(new SnapshotRegion(Element));
                }
            }

            FilteredSnapshot = new Snapshot(FilteredElements.ToArray());

            EndScan();
        }

        public override void EndScan()
        {
            base.EndScan();

            FilteredSnapshot.SetScanMethod("Manual Scan");
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

    } // End class

} // End namespace