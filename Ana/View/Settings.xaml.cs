namespace Ana.View
{
    using Controls;
    using Source.UserSettings;
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    internal partial class Settings : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings" /> class
        /// </summary>
        public Settings()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            this.AlignmentHexDecBox = new HexDecTextBox(typeof(Int32));
            this.AlignmentHexDecBox.TextChanged += this.AlignmentUpdated;
            this.AlignmentHexDecBox.IsHex = true;
            this.AlignmentHexDecBox.SetValue(this.SettingsViewModel.Alignment);
            this.alignment.Children.Add(WinformsHostingHelper.CreateHostedControl(this.AlignmentHexDecBox));

            this.ScanRangeStartHexDecBox = new HexDecTextBox(typeof(UInt64));
            this.ScanRangeStartHexDecBox.TextChanged += this.StartRangeUpdated;
            this.ScanRangeStartHexDecBox.IsHex = true;
            this.ScanRangeStartHexDecBox.SetValue(this.SettingsViewModel.StartAddress);
            this.scanRangeStart.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ScanRangeStartHexDecBox));

            this.ScanRangeEndHexDecBox = new HexDecTextBox(typeof(UInt64));
            this.ScanRangeEndHexDecBox.TextChanged += this.EndRangeUpdated;
            this.ScanRangeEndHexDecBox.IsHex = true;
            this.ScanRangeEndHexDecBox.SetValue(this.SettingsViewModel.EndAddress);
            this.scanRangeEnd.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ScanRangeEndHexDecBox));

            this.FreezeIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.FreezeIntervalHexDecBox.TextChanged += this.FreezeIntervalUpdated;
            this.FreezeIntervalHexDecBox.SetValue(this.SettingsViewModel.FreezeInterval);
            this.freezeInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.FreezeIntervalHexDecBox));

            this.RescanIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.RescanIntervalHexDecBox.TextChanged += this.RescanIntervalUpdated;
            this.RescanIntervalHexDecBox.SetValue(this.SettingsViewModel.RescanInterval);
            this.rescanInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.RescanIntervalHexDecBox));

            this.TableReadIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.TableReadIntervalHexDecBox.TextChanged += this.TableReadIntervalUpdated;
            this.TableReadIntervalHexDecBox.SetValue(this.SettingsViewModel.TableReadInterval);
            this.tableReadInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.TableReadIntervalHexDecBox));

            this.ResultReadIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.ResultReadIntervalHexDecBox.TextChanged += this.ResultReadIntervalUpdated;
            this.ResultReadIntervalHexDecBox.SetValue(this.SettingsViewModel.ResultReadInterval);
            this.resultReadInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ResultReadIntervalHexDecBox));

            this.InputCorrelatorTimeoutHexDecBox = new HexDecTextBox(typeof(Int32));
            this.InputCorrelatorTimeoutHexDecBox.TextChanged += this.InputCorrelatorTimeoutUpdated;
            this.InputCorrelatorTimeoutHexDecBox.SetValue(this.SettingsViewModel.InputCorrelatorTimeOutInterval);
            this.inputCorrelatorTimeout.Children.Add(WinformsHostingHelper.CreateHostedControl(this.InputCorrelatorTimeoutHexDecBox));
        }

        /// <summary>
        /// Gets the view model associated with this view.
        /// </summary>
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return this.DataContext as SettingsViewModel;
            }
        }

        /// <summary>
        /// Gets or sets the hex dec box for the scan alignment
        /// </summary>
        private HexDecTextBox AlignmentHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the scan range start
        /// </summary>
        private HexDecTextBox ScanRangeStartHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the scan range end
        /// </summary>
        private HexDecTextBox ScanRangeEndHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the freeze interval
        /// </summary>
        private HexDecTextBox FreezeIntervalHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the rescan interval
        /// </summary>
        private HexDecTextBox RescanIntervalHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the table read interval
        /// </summary>
        private HexDecTextBox TableReadIntervalHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the input correlation timeout
        /// </summary>
        private HexDecTextBox ResultReadIntervalHexDecBox { get; set; }

        /// <summary>
        /// Gets or sets the hex dec box for the input correlation timeout
        /// </summary>
        private HexDecTextBox InputCorrelatorTimeoutHexDecBox { get; set; }

        /// <summary>
        /// Invoked when the scan alignment is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void AlignmentUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.AlignmentHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.Alignment = value;
        }

        /// <summary>
        /// Invoked when the scan start range is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void StartRangeUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ScanRangeStartHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.StartAddress = value;
        }

        /// <summary>
        /// Invoked when the scan end range is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void EndRangeUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ScanRangeEndHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.EndAddress = value;
        }

        /// <summary>
        /// Invoked when the freeze interval is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void FreezeIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.FreezeIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.FreezeInterval = value;
        }

        /// <summary>
        /// Invoked when the rescan interval is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void RescanIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.RescanIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.RescanInterval = value;
        }

        /// <summary>
        /// Invoked when the table read interval is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void TableReadIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.TableReadIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.TableReadInterval = value;
        }

        /// <summary>
        /// Invoked when the result read interval is changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void ResultReadIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ResultReadIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.ResultReadInterval = value;
        }

        /// <summary>
        /// Invoked when the input correlator timeout changed, and informs the viewmodel.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event args</param>
        private void InputCorrelatorTimeoutUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.InputCorrelatorTimeoutHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            this.SettingsViewModel.InputCorrelatorTimeOutInterval = value;
        }
    }
    //// End class
}
//// End namespace