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
            Boolean[] StateSettings = SettingsPresenter.GetStateSettings();
            Array StateEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            CommitCheckBox.Checked = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Commit)];
            ReserveCheckBox.Checked = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Reserve)];
            FreeCheckBox.Checked = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Free)];

            Boolean[] TypeSettings = SettingsPresenter.GetTypeSettings();
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            PrivateCheckBox.Checked = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)];
            MappedCheckBox.Checked = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)];
            ImageCheckBox.Checked = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)];

            Boolean[] ProtectionSettings = SettingsPresenter.GetProtectionSettings();
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            NoAccessCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)];
            ReadOnlyCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)];
            ReadWriteCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)];
            WriteCopyCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)];
            ExecuteCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)];
            ExecuteReadCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)];
            ExecuteReadWriteCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)];
            ExecuteWriteCopyCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)];
            GuardCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)];
            NoCacheCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)];
            WriteCombineCheckBox.Checked = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)];

            FreezeIntervalTextBox.Text = SettingsPresenter.GetFreezeInterval();
            RepeatScanIntervalTextBox.Text = SettingsPresenter.GetRescanInterval();
            TableReadIntervalTextBox.Text = SettingsPresenter.GetResultReadInterval();
            ResultsReadIntervalTextBox.Text = SettingsPresenter.GetTableReadInterval();
        }

        private void SaveSettings()
        {
            if (CheckSyntax.Int32Value(FreezeIntervalTextBox.Text))
                SettingsPresenter.UpdateFreezeInterval(FreezeIntervalTextBox.Text);

            if (CheckSyntax.Int32Value(RepeatScanIntervalTextBox.Text))
                SettingsPresenter.UpdateFreezeInterval(RepeatScanIntervalTextBox.Text);

            if (CheckSyntax.Int32Value(TableReadIntervalTextBox.Text))
                SettingsPresenter.UpdateFreezeInterval(TableReadIntervalTextBox.Text);

            if (CheckSyntax.Int32Value(ResultsReadIntervalTextBox.Text))
                SettingsPresenter.UpdateFreezeInterval(ResultsReadIntervalTextBox.Text);

            SettingsPresenter.UpdateStateSettings(CommitCheckBox.Checked, ReserveCheckBox.Checked, FreeCheckBox.Checked);
            SettingsPresenter.UpdateTypeSettings(PrivateCheckBox.Checked, MappedCheckBox.Checked, ImageCheckBox.Checked);
            SettingsPresenter.UpdateProtectionSettings(NoAccessCheckBox.Checked, ReadOnlyCheckBox.Checked, ReadWriteCheckBox.Checked, WriteCopyCheckBox.Checked, ExecuteCheckBox.Checked,
                 ExecuteReadCheckBox.Checked, ExecuteReadWriteCheckBox.Checked, ExecuteWriteCopyCheckBox.Checked, GuardCheckBox.Checked, NoCacheCheckBox.Checked, WriteCombineCheckBox.Checked);

            SettingsPresenter.SaveSettings();
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
