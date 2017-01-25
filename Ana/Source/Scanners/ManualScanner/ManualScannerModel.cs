namespace Ana.Source.Scanners.ManualScanner
{
    using ActionScheduler;
    using BackgroundScans.Prefilters;
    using ScanConstraints;
    using Snapshots;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UserSettings;

    /// <summary>
    /// A memory scanning class for classic manual memory scanning techniques.
    /// </summary>
    internal class ManualScannerModel : ScannerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualScannerModel" /> class.
        /// </summary>
        public ManualScannerModel() : base(
            scannerName: "Manual Scan",
            isRepeated: false,
            dependencyBehavior: new DependencyBehavior(dependencies: typeof(ISnapshotPrefilter)))
        {
            this.ProgressLock = new Object();
        }

        /// <summary>
        /// Gets or sets the snapshot on which we perform the manual scan.
        /// </summary>
        private Snapshot Snapshot { get; set; }

        /// <summary>
        /// Gets or sets the scan constraint manager.
        /// </summary>
        private ScanConstraintManager ScanConstraintManager { get; set; }

        /// <summary>
        /// Gets or sets a lock object for updating scan progress.
        /// </summary>
        private Object ProgressLock { get; set; }

        /// <summary>
        /// Sets the scan constraints for this scan.
        /// </summary>
        /// <param name="scanConstraintManager">The scan constraint manager, which contains all scan constraints.</param>
        public void SetScanConstraintManager(ScanConstraintManager scanConstraintManager)
        {
            this.ScanConstraintManager = scanConstraintManager;
        }

        /// <summary>
        /// Called when the scan begins.
        /// </summary>
        protected override void OnBegin()
        {
            // Initialize snapshot
            this.Snapshot = SnapshotManager.GetInstance().GetActiveSnapshot(createIfNone: true).Clone(this.ScannerName);

            if (this.Snapshot == null || this.ScanConstraintManager == null || this.ScanConstraintManager.Count() <= 0)
            {
                this.Cancel();
                return;
            }

            this.Snapshot.SetAllValidBits(true);
        }

        /// <summary>
        /// Called when the scan updates.
        /// </summary>
        protected override void OnUpdate()
        {
            Int32 processedPages = 0;

            // Read memory to get current values
            Parallel.ForEach(
                this.Snapshot.Cast<Object>(),
                SettingsViewModel.GetInstance().ParallelSettings,
                (regionObject) =>
            {
                SnapshotRegion region = regionObject as SnapshotRegion;
                Boolean readSuccess;

                region.ReadAllRegionMemory(out readSuccess, keepValues: true);

                if (!readSuccess)
                {
                    region.SetAllValidBits(false);
                    return;
                }

                // Ignore region if it requires current & previous values, but we cannot find them
                if (this.ScanConstraintManager.HasRelativeConstraint() && !region.CanCompare())
                {
                    region.SetAllValidBits(false);
                    return;
                }

                foreach (SnapshotElementRef element in region)
                {
                    // Enforce each value constraint on the element
                    foreach (ScanConstraint scanConstraint in this.ScanConstraintManager)
                    {
                        switch (scanConstraint.Constraint)
                        {
                            case ConstraintsEnum.Unchanged:
                                if (!element.Unchanged())
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.Changed:
                                if (!element.Changed())
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.Increased:
                                if (!element.Increased())
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.Decreased:
                                if (!element.Decreased())
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.IncreasedByX:
                                if (!element.IncreasedByValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.DecreasedByX:
                                if (!element.DecreasedByValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.Equal:
                                if (!element.EqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.NotEqual:
                                if (!element.NotEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.GreaterThan:
                                if (!element.GreaterThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.GreaterThanOrEqual:
                                if (!element.GreaterThanOrEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.LessThan:
                                if (!element.LessThanValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.LessThanOrEqual:
                                if (!element.LessThanOrEqualToValue(scanConstraint.ConstraintValue))
                                {
                                    element.SetValid(false);
                                }

                                break;
                            case ConstraintsEnum.NotScientificNotation:
                                if (element.IsScientificNotation())
                                {
                                    element.SetValid(false);
                                }

                                break;
                        }
                    }
                    //// End foreach Constraint
                }
                //// End foreach Element

                lock (this.ProgressLock)
                {
                    processedPages++;
                    this.UpdateProgress(processedPages, this.Snapshot.GetRegionCount());
                }
            });
            //// End foreach Region

            base.OnUpdate();
        }

        /// <summary>
        /// Called when the scan ends.
        /// </summary>
        protected override void OnEnd()
        {
            this.Snapshot.DiscardInvalidRegions();
            SnapshotManager.GetInstance().SaveSnapshot(this.Snapshot);
            Snapshot = null;

            base.OnEnd();
        }
    }
    //// End class
}
//// End namespace