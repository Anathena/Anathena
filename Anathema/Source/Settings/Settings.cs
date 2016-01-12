using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    class Settings : ISettingsModel
    {
        private static Settings _Settings;
        private Boolean[] StateSettings = new Boolean[Enum.GetNames(typeof(MemoryStateFlags)).Length];
        private Boolean[] TypeSettings = new Boolean[Enum.GetNames(typeof(MemoryTypeFlags)).Length];
        private Boolean[] ProtectionSettings = new Boolean[Enum.GetNames(typeof(MemoryProtectionFlags)).Length];

        private Int32 FreezeInterval;
        private Int32 RescanInterval;
        private Int32 ResultReadInterval;
        private Int32 TableReadInterval;

        private Settings()
        {
            LoadSettings();
        }

        public static Settings GetInstance()
        {
            if (_Settings == null)
                _Settings = new Settings();
            return _Settings;
        }

        private void LoadSettings()
        {
            UpdateStateSettings(Properties.Settings.Default.StateFree, Properties.Settings.Default.StateCommit, Properties.Settings.Default.StateReserve);
            UpdateTypeSettings(Properties.Settings.Default.TypePrivate, Properties.Settings.Default.TypeMapped, Properties.Settings.Default.TypeImage);
            UpdateProtectionSettings(Properties.Settings.Default.ProtectionNoAccess, Properties.Settings.Default.ProtectionReadOnly, Properties.Settings.Default.ProtectionReadWrite,
                Properties.Settings.Default.ProtectionWriteCopy, Properties.Settings.Default.ProtectionExecute, Properties.Settings.Default.ProtectionExecuteRead, 
                Properties.Settings.Default.ProtectionExecuteReadWrite, Properties.Settings.Default.ProtectionExecuteWriteCopy, Properties.Settings.Default.ProtectionGuard, 
                Properties.Settings.Default.ProtectionNoCache, Properties.Settings.Default.ProtectionWriteCombine);

            UpdateFreezeInterval(Properties.Settings.Default.FreezeInterval);
            UpdateRescanInterval(Properties.Settings.Default.RescanInterval);
            UpdateResultReadInterval(Properties.Settings.Default.ResultReadInterval);
            UpdateTableReadInterval(Properties.Settings.Default.TableReadInterval);
        }

        public void UpdateStateSettings(Boolean Free, Boolean Commit, Boolean Reserve)
        {
            Array StateEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            Properties.Settings.Default.StateCommit = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Commit)] = Free;
            Properties.Settings.Default.StateReserve = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Reserve)] = Commit;
            Properties.Settings.Default.StateFree = StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Free)] = Reserve;
        }

        public void UpdateTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            Properties.Settings.Default.TypePrivate = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)] = Private;
            Properties.Settings.Default.TypeMapped = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)] = Mapped;
            Properties.Settings.Default.TypeImage = TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)] = Image;
        }

        public void UpdateProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            Properties.Settings.Default.ProtectionNoAccess = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] = NoAccess;
            Properties.Settings.Default.ProtectionReadOnly = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] = ReadOnly;
            Properties.Settings.Default.ProtectionReadWrite = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] = ReadWrite;
            Properties.Settings.Default.ProtectionWriteCopy = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] = WriteCopy;
            Properties.Settings.Default.ProtectionExecute = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] = Execute;
            Properties.Settings.Default.ProtectionExecuteRead = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] = ExecuteRead;
            Properties.Settings.Default.ProtectionExecuteReadWrite = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = ExecuteReadWrite;
            Properties.Settings.Default.ProtectionExecuteWriteCopy = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = ExecuteWriteCopy;
            Properties.Settings.Default.ProtectionGuard = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] = Guard;
            Properties.Settings.Default.ProtectionNoCache = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] = NoCache;
            Properties.Settings.Default.ProtectionWriteCombine = ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] = WriteCombine;
        }

        public void UpdateFreezeInterval(Int32 FreezeInterval)
        {
            Properties.Settings.Default.FreezeInterval = this.FreezeInterval = FreezeInterval;
        }

        public void UpdateRescanInterval(Int32 RescanInterval)
        {
            Properties.Settings.Default.RescanInterval = this.RescanInterval = RescanInterval;
        }

        public void UpdateResultReadInterval(Int32 ResultReadInterval)
        {
            Properties.Settings.Default.ResultReadInterval = this.ResultReadInterval = ResultReadInterval;
        }

        public void UpdateTableReadInterval(Int32 TableReadInterval)
        {
            Properties.Settings.Default.TableReadInterval = this.TableReadInterval = TableReadInterval;
        }

        public Boolean[] GetStateSettings()
        {
            return StateSettings;
        }

        public Boolean[] GetTypeSettings()
        {
            return TypeSettings;
        }

        public Boolean[] GetProtectionSettings()
        {
            return ProtectionSettings;
        }

        public Int32 GetFreezeInterval()
        {
            return FreezeInterval;
        }

        public Int32 GetRescanInterval()
        {
            return RescanInterval;
        }

        public Int32 GetResultReadInterval()
        {
            return ResultReadInterval;
        }

        public Int32 GetTableReadInterval()
        {
            return TableReadInterval;
        }
    }
}
