using System;
using System.Windows.Forms;
using Anathema.MemoryManagement.Native;

namespace Anathema
{
    public partial class GUISettings : Form, ISettingsView
    {
        private SettingsPresenter SettingsPresenter;

        public GUISettings()
        {
            InitializeComponent();

            SettingsPresenter = new SettingsPresenter(this, Settings.GetInstance());
            AlignmentTextBox.SetElementType(typeof(Int32));

            FetchSettings();
        }

        private void FetchSettings()
        {
            FetchScanSettings();
            FetchGeneralSettings();
        }

        private void FetchScanSettings()
        {
            Boolean[] RequiredTypeSettings = SettingsPresenter.GetAllowedTypeSettings();
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            NoneCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.None)];
            PrivateCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)];
            MappedCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)];
            ImageCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)];

            Boolean[] RequiredProtectionSettings = SettingsPresenter.GetRequiredProtectionSettings();
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            RequiredCopyOnWriteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] ? CheckState.Checked : CheckState.Indeterminate;
            RequiredWriteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] ? CheckState.Checked : CheckState.Indeterminate;
            RequiredExecuteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] ? CheckState.Checked : CheckState.Indeterminate;

            Boolean[] IgnoredProtectionSettings = SettingsPresenter.GetExcludedProtectionSettings();
            RequiredCopyOnWriteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] ? CheckState.Unchecked : RequiredCopyOnWriteCheckBox.CheckState;
            RequiredWriteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] ? CheckState.Unchecked : RequiredWriteCheckBox.CheckState;
            RequiredExecuteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] ? CheckState.Unchecked : RequiredExecuteCheckBox.CheckState;

            AlignmentTextBox.SetValue(SettingsPresenter.GetAlignmentSettings());
        }

        private void FetchGeneralSettings()
        {
            FreezeIntervalTextBox.Text = SettingsPresenter.GetFreezeInterval();
            RescanIntervalTextBox.Text = SettingsPresenter.GetRescanInterval();
            TableReadIntervalTextBox.Text = SettingsPresenter.GetTableReadInterval();
            ResultsReadIntervalTextBox.Text = SettingsPresenter.GetResultReadInterval();
            InputCorrelatorTimeoutIntervalTextBox.Text = SettingsPresenter.GetInputCorrelatorTimeOutInterval();
        }


        private void SaveSettings()
        {
            SaveGeneralSettings();
            SaveScanSettings();
        }

        private void SaveGeneralSettings()
        {
            if (CheckSyntax.IsInt32(FreezeIntervalTextBox.Text))
                SettingsPresenter.UpdateFreezeInterval(FreezeIntervalTextBox.Text);

            if (CheckSyntax.IsInt32(RescanIntervalTextBox.Text))
                SettingsPresenter.UpdateRescanInterval(RescanIntervalTextBox.Text);

            if (CheckSyntax.IsInt32(TableReadIntervalTextBox.Text))
                SettingsPresenter.UpdateTableReadInterval(TableReadIntervalTextBox.Text);

            if (CheckSyntax.IsInt32(ResultsReadIntervalTextBox.Text))
                SettingsPresenter.UpdateResultReadInterval(ResultsReadIntervalTextBox.Text);

            if (CheckSyntax.IsInt32(InputCorrelatorTimeoutIntervalTextBox.Text))
                SettingsPresenter.UpdateInputCorrelatorTimeOutInterval(InputCorrelatorTimeoutIntervalTextBox.Text);
        }

        private void SaveScanSettings()
        {
            SettingsPresenter.UpdateTypeSettings(NoneCheckBox.Checked, PrivateCheckBox.Checked, MappedCheckBox.Checked, ImageCheckBox.Checked);
            /*SettingsPresenter.UpdateRequiredProtectionSettings(RequiredCopyOnWriteCheckBox.CheckState == CheckState.Checked, RequiredWriteCheckBox.CheckState == CheckState.Checked, RequiredExecuteCheckBox.CheckState == CheckState.Checked,
                WriteCopyCheckBox.CheckState == CheckState.Checked, RequiredExecuteCheckBox.CheckState == CheckState.Checked, ExecuteReadCheckBox.CheckState == CheckState.Checked, ExecuteReadWriteCheckBox.CheckState == CheckState.Checked,
                ExecuteWriteCopyCheckBox.CheckState == CheckState.Checked, GuardCheckBox.CheckState == CheckState.Checked, NoCacheCheckBox.CheckState == CheckState.Checked, WriteCombineCheckBox.CheckState == CheckState.Checked);
            SettingsPresenter.UpdateIgnoredProtectionSettings(RequiredCopyOnWriteCheckBox.CheckState == CheckState.Unchecked, RequiredWriteCheckBox.CheckState == CheckState.Unchecked, RequiredExecuteCheckBox.CheckState == CheckState.Unchecked,
                WriteCopyCheckBox.CheckState == CheckState.Unchecked, RequiredExecuteCheckBox.CheckState == CheckState.Unchecked, ExecuteReadCheckBox.CheckState == CheckState.Unchecked, ExecuteReadWriteCheckBox.CheckState == CheckState.Unchecked,
                ExecuteWriteCopyCheckBox.CheckState == CheckState.Unchecked, GuardCheckBox.CheckState == CheckState.Unchecked, NoCacheCheckBox.CheckState == CheckState.Unchecked, WriteCombineCheckBox.CheckState == CheckState.Unchecked);
                */
            if (AlignmentTextBox.IsValid())
                SettingsPresenter.UpdateAlignmentSettings(Conversions.ParseValue(typeof(Int32), AlignmentTextBox.GetValueAsDecimal()));
        }

        #region Events

        private void AcceptButton_Click(Object Sender, EventArgs E)
        {
            SaveSettings();
            this.Close();
        }

        #endregion
    }
}
