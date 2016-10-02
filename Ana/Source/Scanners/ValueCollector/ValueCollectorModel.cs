namespace Ana.Source.Scanners.ValueCollector
{
    using Snapshots;
    using System;
    using UserSettings;

    /// <summary>
    /// Collect values for the current snapshot, or a new one if none exists. The values are then assigned to a new snapshot.
    /// </summary>
    internal class ValueCollectorModel : ScannerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueCollectorModel" /> class
        /// </summary>
        public ValueCollectorModel() : base("Value Collector")
        {
        }

        /// <summary>
        /// Performs the value collection scan
        /// </summary>
        public override void Begin()
        {
            Snapshot<Null> snapshot = new Snapshot<Null>(SnapshotManager.GetInstance().GetActiveSnapshot());
            snapshot.SetElementType(typeof(Int32));
            snapshot.SetAlignment(Settings.GetInstance().GetAlignmentSettings());
            snapshot.ReadAllSnapshotMemory();
            snapshot.SetScanMethod(this.ScannerName);
            SnapshotManager.GetInstance().SaveSnapshot(snapshot);
        }
    }
    //// End class
}
//// End namespace