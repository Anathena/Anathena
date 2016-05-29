using Anathema.Source.OS;
using Anathema.Source.Utils.Setting;
using Anathema.Source.Utils.Validation;
using System;
using System.Windows.Forms;

namespace Anathema
{
    public partial class GUISettings : Form, ISettingsView
    {
        private SettingsPresenter SettingsPresenter;
        private Object AccessLock;

        public GUISettings()
        {
            InitializeComponent();

            SettingsPresenter = new SettingsPresenter(this, Settings.GetInstance());
            AccessLock = new Object();

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
            MemoryTypeEnum RequiredTypeSettings = SettingsPresenter.GetAllowedTypeSettings();
            NoneCheckBox.Checked = (RequiredTypeSettings & MemoryTypeEnum.None) != 0;
            PrivateCheckBox.Checked = (RequiredTypeSettings & MemoryTypeEnum.Private) != 0;
            MappedCheckBox.Checked = (RequiredTypeSettings & MemoryTypeEnum.Mapped) != 0;
            ImageCheckBox.Checked = (RequiredTypeSettings & MemoryTypeEnum.Image) != 0;

            MemoryProtectionEnum RequiredProtectionSettings = SettingsPresenter.GetRequiredProtectionSettings();
            RequiredWriteCheckBox.Checked = (RequiredProtectionSettings & MemoryProtectionEnum.Write) != 0;
            RequiredExecuteCheckBox.Checked = (RequiredProtectionSettings & MemoryProtectionEnum.Execute) != 0;
            RequiredCopyOnWriteCheckBox.Checked = (RequiredProtectionSettings & MemoryProtectionEnum.CopyOnWrite) != 0;

            MemoryProtectionEnum IgnoredProtectionSettings = SettingsPresenter.GetExcludedProtectionSettings();
            ExcludedWriteCheckBox.Checked = (IgnoredProtectionSettings & MemoryProtectionEnum.Write) != 0;
            ExcludedExecuteCheckBox.Checked = (IgnoredProtectionSettings & MemoryProtectionEnum.Execute) != 0;
            ExcludedCopyOnWriteCheckBox.Checked = (IgnoredProtectionSettings & MemoryProtectionEnum.CopyOnWrite) != 0;

            AlignmentTextBox.SetValue(SettingsPresenter.GetAlignmentSettings());

            if (SettingsPresenter.GetIsUserMode())
                ScanUserModeRadioButton.Checked = true;
            else
                ScanCustomRangeRadioButton.Checked = true;

            StartAddressTextBox.SetValue(SettingsPresenter.GetStartAddress());
            EndAddressTextBox.SetValue(SettingsPresenter.GetEndAddress());
        }

        private void FetchGeneralSettings()
        {
            FreezeIntervalTextBox.SetValue(SettingsPresenter.GetFreezeInterval());
            RescanIntervalTextBox.SetValue(SettingsPresenter.GetRescanInterval());
            TableReadIntervalTextBox.SetValue(SettingsPresenter.GetTableReadInterval());
            ResultsReadIntervalTextBox.SetValue(SettingsPresenter.GetResultReadInterval());
            InputCorrelatorTimeOutIntervalTextBox.SetValue(SettingsPresenter.GetInputCorrelatorTimeOutInterval());
        }

        private void SaveSettings()
        {
            SaveGeneralSettings();
            SaveScanSettings();
        }

        private void SaveGeneralSettings()
        {
            if (FreezeIntervalTextBox.IsValid())
                SettingsPresenter.UpdateFreezeInterval(FreezeIntervalTextBox.GetValueAsDecimal());

            if (RescanIntervalTextBox.IsValid())
                SettingsPresenter.UpdateRescanInterval(RescanIntervalTextBox.GetValueAsDecimal());

            if (TableReadIntervalTextBox.IsValid())
                SettingsPresenter.UpdateTableReadInterval(TableReadIntervalTextBox.GetValueAsDecimal());

            if (ResultsReadIntervalTextBox.IsValid())
                SettingsPresenter.UpdateResultReadInterval(ResultsReadIntervalTextBox.GetValueAsDecimal());

            if (InputCorrelatorTimeOutIntervalTextBox.IsValid())
                SettingsPresenter.UpdateInputCorrelatorTimeOutInterval(InputCorrelatorTimeOutIntervalTextBox.GetValueAsDecimal());

            if (ScanUserModeRadioButton.Checked)
                SettingsPresenter.SetScanUserMode(true);
            else
                SettingsPresenter.SetScanUserMode(true);
        }

        private void SaveScanSettings()
        {
            SettingsPresenter.UpdateTypeSettings(NoneCheckBox.Checked, PrivateCheckBox.Checked, MappedCheckBox.Checked, ImageCheckBox.Checked);
            SettingsPresenter.UpdateRequiredProtectionSettings(RequiredWriteCheckBox.Checked, RequiredExecuteCheckBox.Checked, RequiredCopyOnWriteCheckBox.Checked);
            SettingsPresenter.UpdateExcludedProtectionSettings(ExcludedWriteCheckBox.Checked, ExcludedExecuteCheckBox.Checked, ExcludedCopyOnWriteCheckBox.Checked);

            if (AlignmentTextBox.IsValid())
                SettingsPresenter.UpdateAlignmentSettings(Conversions.ParseValue(typeof(Int32), AlignmentTextBox.GetValueAsDecimal()));

            SettingsPresenter.SetScanUserMode(ScanUserModeRadioButton.Checked);

            if (StartAddressTextBox.IsValid())
                SettingsPresenter.UpdateStartAddress(Conversions.ParseValue(typeof(UInt64), StartAddressTextBox.GetValueAsDecimal()));

            if (EndAddressTextBox.IsValid())
                SettingsPresenter.UpdateEndAddress(Conversions.ParseValue(typeof(UInt64), EndAddressTextBox.GetValueAsDecimal()));
        }

        #region Events

        private void AcceptButton_Click(Object Sender, EventArgs E)
        {
            SaveSettings();
            this.Close();
        }

        private void ScanUserModeRadioButton_CheckedChanged(Object Sender, EventArgs E)
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

        private void ScanCustomRangeRadioButton_CheckedChanged(Object Sender, EventArgs E)
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

        private void RequiredWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (RequiredWriteCheckBox.Checked)
                ExcludedWriteCheckBox.Checked = !RequiredWriteCheckBox.Checked;
        }

        private void RequiredExecuteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (RequiredExecuteCheckBox.Checked)
                ExcludedExecuteCheckBox.Checked = !RequiredExecuteCheckBox.Checked;
        }

        private void RequiredCopyOnWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (RequiredCopyOnWriteCheckBox.Checked)
                ExcludedCopyOnWriteCheckBox.Checked = !RequiredCopyOnWriteCheckBox.Checked;
        }

        private void ExcludedWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (ExcludedWriteCheckBox.Checked)
                RequiredWriteCheckBox.Checked = !ExcludedWriteCheckBox.Checked;
        }

        private void ExcludedExecuteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (ExcludedExecuteCheckBox.Checked)
                RequiredExecuteCheckBox.Checked = !ExcludedExecuteCheckBox.Checked;
        }

        private void ExcludedCopyOnWriteCheckBox_CheckedChanged(Object Sender, EventArgs E)
        {
            if (ExcludedCopyOnWriteCheckBox.Checked)
                RequiredCopyOnWriteCheckBox.Checked = !ExcludedCopyOnWriteCheckBox.Checked;
        }

        #endregion

    } // End class

} // End namespace