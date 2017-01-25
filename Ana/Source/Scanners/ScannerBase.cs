namespace Ana.Source.Scanners
{
    using ActionScheduler;
    using System;
    using UserSettings;

    /// <summary>
    /// The base of all scanner classes.
    /// </summary>
    internal abstract class ScannerBase : ScheduledTask
    {
        /// <summary>
        /// The number of scans completed.
        /// </summary>
        private Int32 scanCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScannerBase" /> class.
        /// </summary>
        /// <param name="scannerName">The name of this scanner.</param>
        /// <param name="isRepeated">A value indicating whether this scan is repeated.</param>
        /// <param name="dependencyBehavior">The object defining task dependencies.</param>
        public ScannerBase(String scannerName, Boolean isRepeated, DependencyBehavior dependencyBehavior) : base(
            taskName: scannerName,
            isRepeated: isRepeated,
            trackProgress: true,
            dependencyBehavior: dependencyBehavior)
        {
            this.ScannerName = scannerName;
            this.UpdateInterval = SettingsViewModel.GetInstance().RescanInterval;
        }

        /// <summary>
        /// Gets the number of scans that have been executed.
        /// </summary>
        public Int32 ScanCount
        {
            get
            {
                return this.scanCount;
            }

            private set
            {
                this.scanCount = value;
                this.NotifyPropertyChanged(nameof(this.ScanCount));
            }
        }

        /// <summary>
        /// Gets the name of this scanner.
        /// </summary>
        protected String ScannerName { get; private set; }

        /// <summary>
        /// Called when the scan begins.
        /// </summary>
        protected override void OnBegin()
        {
            this.ScanCount = 0;
            this.IsTaskComplete = false;

            base.OnBegin();
        }

        /// <summary>
        /// Called when the scan updates.
        /// </summary>
        protected override void OnUpdate()
        {
            this.ScanCount++;

            base.OnUpdate();
        }

        /// <summary>
        /// Called when the scan ends.
        /// </summary>
        protected override void OnEnd()
        {
            this.IsTaskComplete = true;

            base.OnEnd();
        }
    }
    //// End class
}
//// End namespace