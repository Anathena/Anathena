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

            // Initialize labeled snapshot with counts set to 0
            LabeledSnapshot = new Snapshot<UInt16>(InitialSnapshot.GetMemoryRegions());
            List<UInt16[]> Counts = new List<UInt16[]>();
            for (Int32 RegionIndex = 0; RegionIndex < LabeledSnapshot.GetMemoryRegions().Count; RegionIndex++)
                Counts.Add(new UInt16[LabeledSnapshot.GetMemoryRegions()[RegionIndex].RegionSize]);
            LabeledSnapshot.AssignLabels(Counts);

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            if (!LabeledSnapshot.HasLabels())
                throw new Exception("Count labels missing");

            // Read memory to get current values
            LabeledSnapshot.ReadAllMemory();
            List<Byte[]> PreviousScan = LabeledSnapshot.GetPreviousMemoryValues();
            List<Byte[]> CurrentScan = LabeledSnapshot.GetCurrentMemoryValues();

            if (CurrentScan == null || PreviousScan == null)
                return;
            
            List<UInt16[]> Labels = LabeledSnapshot.GetMemoryLabels();

            // Update the labels with the new count of number of changes
            for (Int32 RegionIndex = 0; RegionIndex < CurrentScan.Count; RegionIndex++)
            {
                for (Int32 ElementIndex = 0; ElementIndex < CurrentScan[RegionIndex].Length; ElementIndex++)
                {
                    if (CurrentScan[RegionIndex][ElementIndex] != PreviousScan[RegionIndex][ElementIndex])
                    {
                        Labels[RegionIndex][ElementIndex]++;
                    }
                }
            }

            // Save new labels
            LabeledSnapshot.AssignLabels(Labels);
        }

        public override void EndScan()
        {
            base.EndScan();

            SnapshotManager.GetInstance().SaveSnapshot(LabeledSnapshot);
        }

    } // End class

} // End namespace