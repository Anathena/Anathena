using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System.Collections.Concurrent;

namespace Anathema
{
    class ValueCollector : IValueCollectorModel
    {
        private Snapshot<Null> Snapshot;
        private Type ElementType;

        public ValueCollector()
        {

        }

        public override void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;
        }

        public override void Begin()
        {
            this.Snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot(true));
            this.Snapshot.SetElementType(ElementType == null ? typeof(Int32) : ElementType);
            this.Snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());

            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            Snapshot.ReadAllSnapshotMemory();

            OnEventUpdateScanCount(new ScannerEventArgs(this.ScanCount));

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