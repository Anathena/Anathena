using Anathema.Snapshots;
using System;

namespace Anathema.Scanners.ValueCollector
{
    class ValueCollector : IScannerModel
    {
        private Snapshot<Null> Snapshot;

        public ValueCollector()
        {

        }

        public override void Begin()
        {
            this.Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot(true));
            this.Snapshot.SetElementType(typeof(Int32));
            this.Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Snapshot.ReadAllSnapshotMemory();

            CancelFlag = true;
        }

        public override void End()
        {
            // Wait for the scan to finish
            base.End();

            Snapshot.SetScanMethod("Value Collector");

            // Save result
            SnapshotManager.GetInstance().SaveSnapshot(Snapshot);

            CleanUp();
        }

        private void CleanUp()
        {
            Snapshot = null;
        }

    } // End class

} // End namespace