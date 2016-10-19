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
            Snapshot snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).Clone();
            snapshot.ElementType = typeof(Int32);
            snapshot.Alignment = SettingsViewModel.GetInstance().Alignment;
            snapshot.ReadAllSnapshotMemory();
            snapshot.ScanMethod = this.ScannerName;
            SnapshotManager.GetInstance().SaveSnapshot(snapshot);
        }
    }
    //// End class
}
//// End namespace