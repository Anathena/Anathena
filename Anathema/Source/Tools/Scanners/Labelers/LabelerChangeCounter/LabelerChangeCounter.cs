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
            // Grab the current snapshot and assign counts of 0 to all addresses
            Snapshot InitialSnapshot = SnapshotManager.GetInstance().GetActiveSnapshot();
            List<UInt16> Counts = new List<UInt16>(new UInt16[InitialSnapshot.GetSize()]);
            LabeledSnapshot = new Snapshot<UInt16>(InitialSnapshot.GetMemoryRegions());
            LabeledSnapshot.AssignLabels(Counts);

            base.BeginScan();
        }

        protected override void UpdateScan()
        {
            if (!LabeledSnapshot.HasLabels())
                throw new Exception("Count labels missing");

            // Get previous values
            List<Byte[]> PreviousScan = LabeledSnapshot.GetReadMemory();

            // Read memory to get current values
            LabeledSnapshot.ReadAllMemory();
            List<Byte[]> CurrentScan = LabeledSnapshot.GetReadMemory();

            if (CurrentScan == null || PreviousScan == null)
                return;
            
            List<Int32> LabelMapping = LabeledSnapshot.GetLabelMapping();
            List<UInt16> Labels = LabeledSnapshot.GetMemoryLabels();

            // Update the labels with the new count of number of changes
            for (Int32 RegionIndex = 0; RegionIndex < CurrentScan.Count; RegionIndex++)
            {
                for (Int32 ElementIndex = 0; ElementIndex < CurrentScan[RegionIndex].Length; ElementIndex++)
                {
                    if (CurrentScan[RegionIndex][ElementIndex] != PreviousScan[RegionIndex][ElementIndex])
                    {
                        Labels[LabelMapping[RegionIndex] + ElementIndex]++;
                    }
                }
            }

            // Save new labels
            LabeledSnapshot.AssignLabels(Labels);
        }

        public override void EndScan()
        {
            base.EndScan();
        }

    } // End class

} // End namespace