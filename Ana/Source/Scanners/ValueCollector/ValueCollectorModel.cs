namespace Ana.Source.Scanners.ValueCollector
{
    using ActionScheduler;
    using BackgroundScans.Prefilters;
    using Snapshots;

    /// <summary>
    /// Collect values for the current snapshot, or a new one if none exists. The values are then assigned to a new snapshot.
    /// </summary>
    internal class ValueCollectorModel : ScannerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueCollectorModel" /> class.
        /// </summary>
        public ValueCollectorModel() : base(
            scannerName: "Value Collector",
            isRepeated: false,
            dependencyBehavior: new DependencyBehavior(dependencies: typeof(ISnapshotPrefilter)))
        {
        }

        /// <summary>
        /// Performs the value collection scan.
        /// </summary>
        protected override void OnBegin()
        {
            Snapshot snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).Clone(this.ScannerName);
            snapshot.ReadAllMemory();
            SnapshotManager.GetInstance().SaveSnapshot(snapshot);
        }
    }
    //// End class
}
//// End namespace