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

        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            // Read memory to get current values
            Snapshot.ReadAllMemory();

            foreach (SnapshotRegion Region in Snapshot)
            {
                foreach (SnapshotElement Element in Region)
                {
                    if (!Element.CanCompare())
                        continue;

                    // Enforce each constraint
                    foreach (ScanConstraintItem ScanConstraint in ScanConstraints)
                    {
                        Element.ElementType = ScanConstraint.ElementType;

                        switch (ScanConstraint.ValueConstraints)
                        {
                            case ValueConstraintsEnum.Unchanged:
                                break;
                            case ValueConstraintsEnum.Changed:
                                break;
                            case ValueConstraintsEnum.Increased:
                                break;
                            case ValueConstraintsEnum.Decreased:
                                break;
                            case ValueConstraintsEnum.IncreasedByX:
                                break;
                            case ValueConstraintsEnum.DecreasedByX:
                                break;
                            case ValueConstraintsEnum.Equal:
                                break;
                            case ValueConstraintsEnum.NotEqual:
                                break;
                            case ValueConstraintsEnum.GreaterThan:
                                break;
                            case ValueConstraintsEnum.LessThan:
                                break;
                        }
                    }
                }
            }

            EndScan();
        }

        public override void EndScan()
        {
            base.EndScan();

            List<SnapshotRegion> FilteredElements = new List<SnapshotRegion>();

            foreach (SnapshotRegion Region in Snapshot)
            {
                foreach (SnapshotElement Element in Region)
                {
                    FilteredElements.Add(new SnapshotRegion(Element));
                }
            }

            Snapshot FilteredSnapshot = new Snapshot(FilteredElements.ToArray());
            FilteredSnapshot.SetScanMethod("Manual Scan");
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

    } // End class

} // End namespace