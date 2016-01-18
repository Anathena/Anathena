using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Binarysharp.MemoryManagement.Native;

namespace Anathema
{
    public partial class GUISettings : Form, ISettingsView
    {
        private SettingsPresenter SettingsPresenter;

        public GUISettings()
        {
            InitializeComponent();

            SettingsPresenter = new SettingsPresenter(this, Settings.GetInstance());

            FetchSettings();
        }

        private void FetchSettings()
        {
            Boolean[] RequiredTypeSettings = SettingsPresenter.GetTypeSettings();
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            NoneCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.None)];
            PrivateCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)];
            MappedCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)];
            ImageCheckBox.Checked = RequiredTypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)];

            Boolean[] RequiredProtectionSettings = SettingsPresenter.GetRequiredProtectionSettings();
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            NoAccessCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] ? CheckState.Checked : CheckState.Indeterminate;
            ReadOnlyCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] ? CheckState.Checked : CheckState.Indeterminate;
            ReadWriteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] ? CheckState.Checked : CheckState.Indeterminate;
            WriteCopyCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] ? CheckState.Checked : CheckState.Indeterminate;
            ExecuteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] ? CheckState.Checked : CheckState.Indeterminate;
            ExecuteReadCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] ? CheckState.Checked : CheckState.Indeterminate;
            ExecuteReadWriteCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] ? CheckState.Checked : CheckState.Indeterminate;
            ExecuteWriteCopyCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] ? CheckState.Checked : CheckState.Indeterminate;
            GuardCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] ? CheckState.Checked : CheckState.Indeterminate;
            NoCacheCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] ? CheckState.Checked : CheckState.Indeterminate;
            WriteCombineCheckBox.CheckState = RequiredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] ? CheckState.Checked : CheckState.Indeterminate;

            Boolean[] IgnoredProtectionSettings = SettingsPresenter.GetIgnoredProtectionSettings();
            NoAccessCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] ? CheckState.Unchecked : NoAccessCheckBox.CheckState;
            ReadOnlyCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] ? CheckState.Unchecked : ReadOnlyCheckBox.CheckState;
            ReadWriteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] ? CheckState.Unchecked : ReadWriteCheckBox.CheckState;
            WriteCopyCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] ? CheckState.Unchecked : WriteCopyCheckBox.CheckState;
            ExecuteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] ? CheckState.Unchecked : ExecuteCheckBox.CheckState;
            ExecuteReadCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] ? CheckState.Unchecked : ExecuteReadCheckBox.CheckState;
            ExecuteReadWriteCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] ? CheckState.Unchecked : ExecuteReadWriteCheckBox.CheckState;
            ExecuteWriteCopyCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] ? CheckState.Unchecked : ExecuteWriteCopyCheckBox.CheckState;
            GuardCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] ? CheckState.Unchecked : GuardCheckBox.CheckState;
            NoCacheCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] ? CheckState.Unchecked : NoCacheCheckBox.CheckState;
            WriteCombineCheckBox.CheckState = IgnoredProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] ? CheckState.Unchecked : WriteCombineCheckBox.CheckState;

            FreezeIntervalTextBox.Text = SettingsPresenter.GetFreezeInterval();
            RescanIntervalTextBox.Text = SettingsPresenter.GetRescanInterval();
            TableReadIntervalTextBox.Text = SettingsPresenter.GetResultReadInterval();
            ResultsReadIntervalTextBox.Text = SettingsPresenter.GetTableReadInterval();
            InputCorrelatorTimeoutIntervalTextBox.Text = SettingsPresenter.GetInputCorrelatorTimeOutInterval();
        }

        private void SaveSettings()
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

            SettingsPresenter.UpdateTypeSettings(NoneCheckBox.Checked, PrivateCheckBox.Checked, MappedCheckBox.Checked, ImageCheckBox.Checked);
            SettingsPresenter.UpdateRequiredProtectionSettings(NoAccessCheckBox.CheckState == CheckState.Checked, ReadOnlyCheckBox.CheckState == CheckState.Checked, ReadWriteCheckBox.CheckState == CheckState.Checked,
                WriteCopyCheckBox.CheckState == CheckState.Checked, ExecuteCheckBox.CheckState == CheckState.Checked, ExecuteReadCheckBox.CheckState == CheckState.Checked, ExecuteReadWriteCheckBox.CheckState == CheckState.Checked,
                ExecuteWriteCopyCheckBox.CheckState == CheckState.Checked, GuardCheckBox.CheckState == CheckState.Checked, NoCacheCheckBox.CheckState == CheckState.Checked, WriteCombineCheckBox.CheckState == CheckState.Checked);
            SettingsPresenter.UpdateIgnoredProtectionSettings(NoAccessCheckBox.CheckState == CheckState.Unchecked, ReadOnlyCheckBox.CheckState == CheckState.Unchecked, ReadWriteCheckBox.CheckState == CheckState.Unchecked,
                WriteCopyCheckBox.CheckState == CheckState.Unchecked, ExecuteCheckBox.CheckState == CheckState.Unchecked, ExecuteReadCheckBox.CheckState == CheckState.Unchecked, ExecuteReadWriteCheckBox.CheckState == CheckState.Unchecked,
                ExecuteWriteCopyCheckBox.CheckState == CheckState.Unchecked, GuardCheckBox.CheckState == CheckState.Unchecked, NoCacheCheckBox.CheckState == CheckState.Unchecked, WriteCombineCheckBox.CheckState == CheckState.Unchecked);
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
