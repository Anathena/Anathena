using Anathena.Source.Engine.OperatingSystems;
using Anathena.Source.UserSettings;
using Anathena.Source.Utils.Validation;
using System;
using System.Windows.Forms;

namespace Anathena.GUI.Tools
{
    public partial class GUISettings : Form, ISettingsView
    {
        private SettingsPresenter settingsPresenter;
        private Object accessLock;

        public GUISettings()
        {
            InitializeComponent();

            settingsPresenter = new SettingsPresenter(this, Settings.GetInstance());
            accessLock = new Object();

            AlignmentTextBox.SetElementType(typeof(Int32));
            StartAddressTextBox.SetElementType(typeof(UInt64));
            EndAddressTextBox.SetElementType(typeof(UInt64));

            FreezeIntervalTextBox.SetElementType(typeof(Int32));
            RescanIntervalTextBox.SetElementType(typeof(Int32));
            TableReadIntervalTextBox.SetElementType(typeof(Int32));
            ResultsReadIntervalTextBox.SetElementType(typeof(Int32));
            InputCorrelatorTimeOutIntervalTextBox.SetElementType(typeof(Int32));

            FetchSettings();
        }

        private void FetchSettings()
        {
            FetchScanSettings();
            FetchGeneralSettings();
        }

        private void FetchScanSettings()
        {
            MemoryTypeEnum requiredTypeSettings = settingsPresenter.GetAllowedTypeSettings();
            NoneCheckBox.Checked = (requiredTypeSettings & MemoryTypeEnum.None) != 0;
            PrivateCheckBox.Checked = (requiredTypeSettings & MemoryTypeEnum.Private) != 0;
            MappedCheckBox.Checked = (requiredTypeSettings & MemoryTypeEnum.Mapped) != 0;
            ImageCheckBox.Checked = (requiredTypeSettings & MemoryTypeEnum.Image) != 0;

            MemoryProtectionEnum requiredProtectionSettings = settingsPresenter.GetRequiredProtectionSettings();
            RequiredWriteCheckBox.Checked = (requiredProtectionSettings & MemoryProtectionEnum.Write) != 0;
            RequiredExecuteCheckBox.Checked = (requiredProtectionSettings & MemoryProtectionEnum.Execute) != 0;
            RequiredCopyOnWriteCheckBox.Checked = (requiredProtectionSettings & MemoryProtectionEnum.CopyOnWrite) != 0;

            MemoryProtectionEnum ignoredProtectionSettings = settingsPresenter.GetExcludedProtectionSettings();
            ExcludedWriteCheckBox.Checked = (ignoredProtectionSettings & MemoryProtectionEnum.Write) != 0;
            ExcludedExecuteCheckBox.Checked = (ignoredProtectionSettings & MemoryProtectionEnum.Execute) != 0;
            ExcludedCopyOnWriteCheckBox.Checked = (ignoredProtectionSettings & MemoryProtectionEnum.CopyOnWrite) != 0;

            AlignmentTextBox.SetValue(settingsPresenter.GetAlignmentSettings());

            if (settingsPresenter.GetIsUserMode())
                ScanUserModeRadioButton.Checked = true;
            else
                ScanCustomRangeRadioButton.Checked = true;

            StartAddressTextBox.SetValue(settingsPresenter.GetStartAddress());
            EndAddressTextBox.SetValue(settingsPresenter.GetEndAddress());
        }

        private void FetchGeneralSettings()
        {
            FreezeIntervalTextBox.SetValue(settingsPresenter.GetFreezeInterval());
            RescanIntervalTextBox.SetValue(settingsPresenter.GetRescanInterval());
            TableReadIntervalTextBox.SetValue(settingsPresenter.GetTableReadInterval());
            ResultsReadIntervalTextBox.SetValue(settingsPresenter.GetResultReadInterval());
            InputCorrelatorTimeOutIntervalTextBox.SetValue(settingsPresenter.GetInputCorrelatorTimeOutInterval());
        }

        private void SaveSettings()
        {
            SaveGeneralSettings();
            SaveScanSettings();
        }

        private void SaveGeneralSettings()
        {
            if (FreezeIntervalTextBox.IsValid())
                settingsPresenter.UpdateFreezeInterval(FreezeIntervalTextBox.GetValueAsDecimal());

            if (RescanIntervalTextBox.IsValid())
                settingsPresenter.UpdateRescanInterval(RescanIntervalTextBox.GetValueAsDecimal());

            if (TableReadIntervalTextBox.IsValid())
                settingsPresenter.UpdateTableReadInterval(TableReadIntervalTextBox.GetValueAsDecimal());

            if (ResultsReadIntervalTextBox.IsValid())
                settingsPresenter.UpdateResultReadInterval(ResultsReadIntervalTextBox.GetValueAsDecimal());

            if (InputCorrelatorTimeOutIntervalTextBox.IsValid())
                settingsPresenter.UpdateInputCorrelatorTimeOutInterval(InputCorrelatorTimeOutIntervalTextBox.GetValueAsDecimal());

            if (ScanUserModeRadioButton.Checked)
                settingsPresenter.SetScanUserMode(true);
            else
                settingsPresenter.SetScanUserMode(true);
        }

        private void SaveScanSettings()
        {
            settingsPresenter.UpdateTypeSettings(NoneCheckBox.Checked, PrivateCheckBox.Checked, MappedCheckBox.Checked, ImageCheckBox.Checked);
            settingsPresenter.UpdateRequiredProtectionSettings(RequiredWriteCheckBox.Checked, RequiredExecuteCheckBox.Checked, RequiredCopyOnWriteCheckBox.Checked);
            settingsPresenter.UpdateExcludedProtectionSettings(ExcludedWriteCheckBox.Checked, ExcludedExecuteCheckBox.Checked, ExcludedCopyOnWriteCheckBox.Checked);

            if (AlignmentTextBox.IsValid())
                settingsPresenter.UpdateAlignmentSettings(Conversions.ParseDecStringAsValue(typeof(Int32), AlignmentTextBox.GetValueAsDecimal()));

            settingsPresenter.SetScanUserMode(ScanUserModeRadioButton.Checked);

            if (StartAddressTextBox.IsValid())
                settingsPresenter.UpdateStartAddress(Conversions.ParseDecStringAsValue(typeof(UInt64), StartAddressTextBox.GetValueAsDecimal()));

            if (EndAddressTextBox.IsValid())
                settingsPresenter.UpdateEndAddress(Conversions.ParseDecStringAsValue(typeof(UInt64), EndAddressTextBox.GetValueAsDecimal()));
        }

        #region Events

        private void AcceptSettingsButton_Click(Object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void ScanUserModeRadioButton_CheckedChanged(Object sender, EventArgs e)
        {
            if (ScanUserModeRadioButton.Checked == true)
            {
                StartAddressTextBox.Enabled = false;
                EndAddressTextBox.Enabled = false;
            }
            else
            {
                StartAddressTextBox.Enabled = true;
                EndAddressTextBox.Enabled = true;
            }
        }

        private void ScanCustomRangeRadioButton_CheckedChanged(Object sender, EventArgs e)
        {
            if (ScanCustomRangeRadioButton.Checked == true)
            {
                StartAddressTextBox.Enabled = true;
                EndAddressTextBox.Enabled = true;
            }
            else
            {
                StartAddressTextBox.Enabled = false;
                EndAddressTextBox.Enabled = false;
            }
        }

        private void RequiredWriteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (RequiredWriteCheckBox.Checked)
                ExcludedWriteCheckBox.Checked = !RequiredWriteCheckBox.Checked;
        }

        private void RequiredExecuteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (RequiredExecuteCheckBox.Checked)
                ExcludedExecuteCheckBox.Checked = !RequiredExecuteCheckBox.Checked;
        }

        private void RequiredCopyOnWriteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (RequiredCopyOnWriteCheckBox.Checked)
                ExcludedCopyOnWriteCheckBox.Checked = !RequiredCopyOnWriteCheckBox.Checked;
        }

        private void ExcludedWriteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (ExcludedWriteCheckBox.Checked)
                RequiredWriteCheckBox.Checked = !ExcludedWriteCheckBox.Checked;
        }

        private void ExcludedExecuteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (ExcludedExecuteCheckBox.Checked)
                RequiredExecuteCheckBox.Checked = !ExcludedExecuteCheckBox.Checked;
        }

        private void ExcludedCopyOnWriteCheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            if (ExcludedCopyOnWriteCheckBox.Checked)
                RequiredCopyOnWriteCheckBox.Checked = !ExcludedCopyOnWriteCheckBox.Checked;
        }

        #endregion

    } // End class

} // End namespace