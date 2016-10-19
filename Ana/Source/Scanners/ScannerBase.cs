namespace Ana.Source.Scanners
{
    using System;
    using UserSettings;
    using Utils;

    /// <summary>
    /// The base of all scanner classes
    /// </summary>
    internal abstract class ScannerBase : RepeatedTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScannerBase" /> class
        /// </summary>
        /// <param name="scannerName">The name of this scanner</param>
        public ScannerBase(String scannerName)
        {
            this.ScannerName = scannerName;
        }

        /// <summary>
        /// Gets the number of scans that have been executed
        /// </summary>
        public Int32 ScanCount { get; private set; }

        /// <summary>
        /// Gets the name of this scanner
        /// </summary>
        protected String ScannerName { get; private set; }

        /// <summary>
        /// Begins the scan
        /// </summary>
        public override void Begin()
        {
            this.ScanCount = 0;
            this.UpdateInterval = SettingsViewModel.GetInstance().RescanInterval;
            base.Begin();
        }

        /// <summary>
        /// Updates the scan
        /// </summary>
        protected override void Update()
        {
            this.ScanCount++;
            this.UpdateInterval = SettingsViewModel.GetInstance().RescanInterval;
        }
    }
    //// End class
}
//// End namespace