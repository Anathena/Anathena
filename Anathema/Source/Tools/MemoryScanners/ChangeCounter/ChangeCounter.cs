using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    class ChangeCounter : IChangeCounterModel
    {
        // Snapshot being labeled with change counts
        private Snapshot<UInt16> Snapshot;

        // User controlled variables
        private Int32 MinChanges;
        private Int32 MaxChanges;
        private Int32 VariableSize;

        public ChangeCounter()
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

        public override void Begin()
        {
            // Initialize labeled snapshot
            Snapshot = new Snapshot<UInt16>(SnapshotManager.GetInstance().GetActiveSnapshot());
            Snapshot.SetVariableSize(VariableSize);

            // Initialize change counts to zero
            Snapshot.SetElementLabels(0);
            Snapshot.MarkAllValid();

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            // Read memory to get current values
            Snapshot.ReadAllSnapshotMemory();

            Parallel.ForEach(Snapshot.Cast<Object>(), (object RegionObject) =>
            {
                SnapshotRegion Region = (SnapshotRegion)RegionObject;

                if (!Region.CanCompare())
                    return;

                foreach (SnapshotElement<UInt16> Element in Region)
                {
                    if (Element.Changed())
                        Element.ElementLabel++;
                }
            }); // End regions

            OnEventUpdateScanCount(new ScannerEventArgs(this.ScanCount));
        }

        public override void End()
        {
            base.End();
            
            // Mark regions as valid or invalid based on number of changes
            Snapshot.MarkAllInvalid();
            foreach (SnapshotRegion<UInt16> Region in Snapshot)
                foreach (SnapshotElement<UInt16> Element in Region)
                    if (Element.ElementLabel.Value >= MinChanges && Element.ElementLabel.Value <= MaxChanges)
                        Element.Valid = true;

            // Create a snapshot from the valid regions
            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Change Counter");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);
        }

        private void CleanUp()
        {
            Snapshot = null;
        }

    } // End class

} // End namespace