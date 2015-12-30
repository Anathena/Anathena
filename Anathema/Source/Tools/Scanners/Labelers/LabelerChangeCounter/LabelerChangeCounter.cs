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
        private Snapshot<UInt16> LabeledSnapshot;

        // User controlled variables
        private Int32 MinChanges;
        private Int32 MaxChanges;
        private Int32 VariableSize;

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

        public override void BeginScan()
        {
            Snapshot InitialSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();

            // Initialize labeled snapshot
            LabeledSnapshot = new Snapshot<UInt16>(InitialSnapshot);

            // Initialize labels to a value of 0
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
                    if (Element.CurrentValue == null || Element.PreviousValue == null)
                        continue;

                    if (Element.CurrentValue != Element.PreviousValue)
                        Element.MemoryLabel++;
                }
            }
        }

        public override void EndScan()
        {
            base.EndScan();

            SnapshotManager.GetInstance().SaveSnapshot(LabeledSnapshot);
        }

    } // End class

} // End namespace