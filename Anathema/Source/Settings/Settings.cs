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
            // if (Saved)
            //  LoadFromDisk();
            // else
            SetDefaultSettings();
        }

        private void SetDefaultSettings()
        {
            Array StateEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Commit)] = true;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Reserve)] = true;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Free)] = true;

            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)] = true;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)] = false;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)] = true;

            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] = true;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = true;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] = false;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] = false;

            FreezeInterval = 100;
            RescanInterval = 400;
            ResultReadInterval = 400;
            TableReadInterval = 400;
    }

        public void UpdateStateSettings(Boolean Free, Boolean Commit, Boolean Reserve)
        {
            Array StateEnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Commit)] = Free;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Reserve)] = Commit;
            StateSettings[Array.IndexOf(StateEnumValues, MemoryStateFlags.Free)] = Reserve;
        }

        public void UpdateTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)] = Private;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)] = Mapped;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)] = Image;
        }

        public void UpdateProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Array ProtectionEnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoAccess)] = NoAccess;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadOnly)] = ReadOnly;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ReadWrite)] = ReadWrite;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCopy)] = WriteCopy;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Execute)] = Execute;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteRead)] = ExecuteRead;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = ExecuteReadWrite;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = ExecuteWriteCopy;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.Guard)] = Guard;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.NoCache)] = NoCache;
            ProtectionSettings[Array.IndexOf(ProtectionEnumValues, MemoryProtectionFlags.WriteCombine)] = WriteCombine;
        }

        public void UpdateIntervalSettings(Int32 FreezeInterval, Int32 RescanInterval, Int32 ResultReadInterval, Int32 TableReadInterval)
        {
            this.FreezeInterval = FreezeInterval;
            this.RescanInterval = RescanInterval;
            this.ResultReadInterval = ResultReadInterval;
            this.TableReadInterval = TableReadInterval;
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
