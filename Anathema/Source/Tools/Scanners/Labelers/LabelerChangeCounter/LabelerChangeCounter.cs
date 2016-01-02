using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class LabelerChangeCounter : ILabelerChangeCounterModel
    {
        private enum ChangeCountingModeEnum
        {
            Changing,
            Increasing,
            Decreasing,
        }

        // Snapshot being labeled with change counts
        private Snapshot<UInt16> LabeledSnapshot;

        // User controlled variables
        private Int32 MinChanges;
        private Int32 MaxChanges;
        private Int32 VariableSize;
        private ChangeCountingModeEnum ChangeCountingMode;

        public LabelerChangeCounter()
        {

        }

        public override void SetMinChanges(Int32 MinChanges)
        {
            this.MinChanges = MinChanges;
        }

        public override void SetMaxChanges(Int32 MaxChanges)
        {
            this.MaxChanges = MaxChanges;
        }

        public override void SetVariableSize(Int32 VariableSize)
        {
            this.VariableSize = VariableSize;
        }

        public override void SetScanModeChanging()
        {
            ChangeCountingMode = ChangeCountingModeEnum.Changing;
        }

        public override void SetScanModeIncreasing()
        {
            ChangeCountingMode = ChangeCountingModeEnum.Increasing;
        }

        public override void SetScanModeDecreasing()
        {
            ChangeCountingMode = ChangeCountingModeEnum.Decreasing;
        }

        public override void BeginScan()
        {
            // Initialize labeled snapshot
            LabeledSnapshot = new Snapshot<UInt16>(SnapshotManager.GetInstance().GetActiveSnapshot());
            LabeledSnapshot.SetVariableSize(VariableSize);

            // Initialize change counts to zero
            foreach (SnapshotRegion<UInt16> Region in LabeledSnapshot)
                foreach (SnapshotElement<UInt16> Element in Region)
                    Element.MemoryLabel = 0;

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            // Read memory to get current values
            LabeledSnapshot.ReadAllMemory();

            foreach (SnapshotRegion<UInt16> Region in LabeledSnapshot)
            {
                foreach (SnapshotElement<UInt16> Element in Region)
                {
                    if (!Element.CanCompare())
                        continue;

                    switch (ChangeCountingMode)
                    {
                        case ChangeCountingModeEnum.Changing:
                            if (Element.Changed())
                                Element.MemoryLabel++;
                            break;
                        case ChangeCountingModeEnum.Increasing:
                            if (Element.Increased())
                                Element.MemoryLabel++;
                            break;
                        case ChangeCountingModeEnum.Decreasing:
                            if (Element.Decreased())
                                Element.MemoryLabel++;
                            break;
                    }
                }
            }
        }

        public override void EndScan()
        {
            base.EndScan();

            List<SnapshotRegion<UInt16>> FilteredElements = new List<SnapshotRegion<UInt16>>();

            foreach (SnapshotRegion<UInt16> Region in LabeledSnapshot)
            {
                foreach (SnapshotElement<UInt16> Element in Region)
                {
                    if (Element.MemoryLabel.Value >= MinChanges && Element.MemoryLabel.Value <= MaxChanges)
                        FilteredElements.Add(new SnapshotRegion<UInt16>(Element));
                }
            }

            Snapshot<UInt16> FilteredSnapshot = new Snapshot<UInt16>(FilteredElements.ToArray());
            SnapshotManager.GetInstance().SaveSnapshot(FilteredSnapshot);
        }

    } // End class

} // End namespace