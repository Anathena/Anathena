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
        private UInt16 MinChanges;
        private UInt16 MaxChanges;
        private Int32 VariableSize;

        public ChangeCounter()
        {

        }

        public override void SetMinChanges(UInt16 MinChanges)
        {
            this.MinChanges = MinChanges;
        }

        public override void SetMaxChanges(UInt16 MaxChanges)
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

            if (Snapshot == null)
                return;

            Snapshot.SetVariableSize(VariableSize);

            // Initialize change counts to zero
            Snapshot.SetElementLabels(0);

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

            if (Snapshot.GetRegionCount() > 0)
            {
                foreach (SnapshotRegion<UInt16> Region in Snapshot)
                    foreach (SnapshotElement<UInt16> Element in Region)
                        if (Element.ElementLabel.Value >= MinChanges && Element.ElementLabel.Value <= MaxChanges)
                            Element.Valid = true;
            }

            // Create a snapshot from the valid regions
            Snapshot.DiscardInvalidRegions();
            Snapshot.SetScanMethod("Change Counter");

            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            Main.GetInstance().OpenLabelThresholder();
        }

        private void CleanUp()
        {
            Snapshot = null;
        }

    } // End class

} // End namespace