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

        public FilterManualScan()
        {

        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            Snapshot = new Snapshot(SnapshotManager.GetInstance().GetActiveSnapshot());

            // Initialize change counts to zero
            foreach (SnapshotRegion<UInt16> Region in Snapshot)
                foreach (SnapshotElement<UInt16> Element in Region)
                    Element.MemoryLabel = 0;

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
                }
            }
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