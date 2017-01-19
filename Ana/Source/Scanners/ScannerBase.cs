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
        public ScannerBase(String scannerName, Boolean isRepeated, DependencyBehavior dependencyBehavior) : base(
            taskName: scannerName,
            isRepeated: isRepeated,
            trackProgress: true,
            dependencyBehavior: dependencyBehavior)
        {
            this.ScannerName = scannerName;
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
        /// Begins the scan.
        /// </summary>
        protected override void OnBegin()
        {
            this.ScanCount = 0;
            this.UpdateInterval = SettingsViewModel.GetInstance().RescanInterval;
        }

        /// <summary>
        /// Updates the scan.
        /// </summary>
        protected override void OnUpdate()
        {
            this.ScanCount++;
            this.UpdateInterval = SettingsViewModel.GetInstance().RescanInterval;
        }
    }
    //// End class
}
//// End namespace