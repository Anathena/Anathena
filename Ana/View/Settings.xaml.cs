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
            this.AlignmentHexDecBox.SetValue(SettingsViewModel.GetInstance().Alignment);
            this.alignment.Children.Add(WinformsHostingHelper.CreateHostedControl(this.AlignmentHexDecBox));

            this.ScanRangeStartHexDecBox = new HexDecTextBox(typeof(UInt64));
            this.ScanRangeStartHexDecBox.TextChanged += this.StartRangeUpdated;
            this.ScanRangeStartHexDecBox.IsHex = true;
            this.ScanRangeStartHexDecBox.SetValue(SettingsViewModel.GetInstance().StartAddress);
            this.scanRangeStart.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ScanRangeStartHexDecBox));

            this.ScanRangeEndHexDecBox = new HexDecTextBox(typeof(UInt64));
            this.ScanRangeEndHexDecBox.TextChanged += this.EndRangeUpdated;
            this.ScanRangeEndHexDecBox.IsHex = true;
            this.ScanRangeEndHexDecBox.SetValue(SettingsViewModel.GetInstance().EndAddress);
            this.scanRangeEnd.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ScanRangeEndHexDecBox));

            this.FreezeIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.FreezeIntervalHexDecBox.TextChanged += this.FreezeIntervalUpdated;
            this.FreezeIntervalHexDecBox.SetValue(SettingsViewModel.GetInstance().FreezeInterval);
            this.freezeInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.FreezeIntervalHexDecBox));

            this.RescanIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.RescanIntervalHexDecBox.TextChanged += this.RescanIntervalUpdated;
            this.RescanIntervalHexDecBox.SetValue(SettingsViewModel.GetInstance().RescanInterval);
            this.rescanInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.RescanIntervalHexDecBox));

            this.TableReadIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.TableReadIntervalHexDecBox.TextChanged += this.TableReadIntervalUpdated;
            this.TableReadIntervalHexDecBox.SetValue(SettingsViewModel.GetInstance().TableReadInterval);
            this.tableReadInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.TableReadIntervalHexDecBox));

            this.ResultReadIntervalHexDecBox = new HexDecTextBox(typeof(Int32));
            this.ResultReadIntervalHexDecBox.TextChanged += this.ResultReadIntervalUpdated;
            this.ResultReadIntervalHexDecBox.SetValue(SettingsViewModel.GetInstance().ResultReadInterval);
            this.resultReadInterval.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ResultReadIntervalHexDecBox));

            this.InputCorrelatorTimeoutHexDecBox = new HexDecTextBox(typeof(Int32));
            this.InputCorrelatorTimeoutHexDecBox.TextChanged += this.InputCorrelatorTimeoutUpdated;
            this.InputCorrelatorTimeoutHexDecBox.SetValue(SettingsViewModel.GetInstance().InputCorrelatorTimeOutInterval);
            this.inputCorrelatorTimeout.Children.Add(WinformsHostingHelper.CreateHostedControl(this.InputCorrelatorTimeoutHexDecBox));
        }

        private HexDecTextBox AlignmentHexDecBox { get; set; }

        private HexDecTextBox ScanRangeStartHexDecBox { get; set; }

        private HexDecTextBox ScanRangeEndHexDecBox { get; set; }

        private HexDecTextBox FreezeIntervalHexDecBox { get; set; }

        private HexDecTextBox RescanIntervalHexDecBox { get; set; }

        private HexDecTextBox TableReadIntervalHexDecBox { get; set; }

        private HexDecTextBox ResultReadIntervalHexDecBox { get; set; }

        private HexDecTextBox InputCorrelatorTimeoutHexDecBox { get; set; }

        private void AlignmentUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.AlignmentHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().Alignment = value;
        }

        private void StartRangeUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ScanRangeStartHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().StartAddress = value;
        }

        private void EndRangeUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ScanRangeEndHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().EndAddress = value;
        }

        private void FreezeIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.FreezeIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().FreezeInterval = value;
        }

        private void RescanIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.RescanIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().RescanInterval = value;
        }

        private void TableReadIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.TableReadIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().TableReadInterval = value;
        }

        private void ResultReadIntervalUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.ResultReadIntervalHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().ResultReadInterval = value;
        }

        private void InputCorrelatorTimeoutUpdated(Object sender, EventArgs e)
        {
            dynamic value = this.InputCorrelatorTimeoutHexDecBox.GetValue();

            if (value == null)
            {
                return;
            }

            SettingsViewModel.GetInstance().InputCorrelatorTimeOutInterval = value;
        }
    }
    //// End class
}
//// End namespace