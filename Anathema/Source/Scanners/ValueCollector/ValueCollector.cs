using Anathema.Source.Snapshots;
using Anathema.Source.UserSettings;
using System;

namespace Anathema.Source.Scanners.ValueCollector
{
    class ValueCollector : IScannerModel
    {
        private Snapshot<Null> Snapshot;

        public ValueCollector() { }

        public override void Begin()
        {
            this.Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot());
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

        protected override void End()
        {
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