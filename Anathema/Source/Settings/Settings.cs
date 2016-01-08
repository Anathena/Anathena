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
        private Int32[] StateSettings = new Int32[Enum.GetNames(typeof(MemoryStateFlags)).Length];
        private Int32[] TypeSettings = new Int32[Enum.GetNames(typeof(MemoryTypeFlags)).Length];
        private Int32[] ProtectionSettings = new Int32[Enum.GetNames(typeof(MemoryProtectionFlags)).Length];

        private Int32 FreezeInterval;
        private Int32 RescanInterval;
        private Int32 ResultReadInterval;
        private Int32 TableReadInterval;

        public Settings()
        {
            LoadSettings();
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
            Array EnumValues;

            EnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Commit)] = 1;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Reserve)] = 0;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Free)] = 0;

            EnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Private)] = 1;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Mapped)] = 0;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Image)] = 1;

            EnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoAccess)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadOnly)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadWrite)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCopy)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Execute)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteRead)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = 1;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Guard)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoCache)] = 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCombine)] = 0;
        }

        public void UpdateStateSettings(Boolean Free, Boolean Commit, Boolean Reserve)
        {
            Array EnumValues = Enum.GetValues(typeof(MemoryStateFlags));
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Commit)] = Free ? 1 : 0;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Reserve)] = Commit ? 1 : 0;
            StateSettings[Array.IndexOf(EnumValues, MemoryStateFlags.Free)] = Reserve ? 1 : 0;
        }

        public void UpdateTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Array EnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Private)] = Private ? 1 : 0;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Mapped)] = Mapped ? 1 : 0;
            TypeSettings[Array.IndexOf(EnumValues, MemoryTypeFlags.Image)] = Image ? 1 : 0;
        }

        public void UpdateProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Array EnumValues = Enum.GetValues(typeof(MemoryProtectionFlags));
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoAccess)] = NoAccess ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadOnly)] = ReadOnly ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ReadWrite)] = ReadWrite ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCopy)] = WriteCopy ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Execute)] = Execute ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteRead)] = ExecuteRead ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteReadWrite)] = ExecuteReadWrite ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.ExecuteWriteCopy)] = ExecuteWriteCopy ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.Guard)] = Guard ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.NoCache)] = NoCache ? 1 : 0;
            ProtectionSettings[Array.IndexOf(EnumValues, MemoryProtectionFlags.WriteCombine)] = WriteCombine ? 1 : 0;
        }

        public void UpdateIntervalSettings(Int32 FreezeInterval, Int32 RescanInterval, Int32 ResultReadInterval, Int32 TableReadInterval)
        {
            this.FreezeInterval = FreezeInterval;
            this.RescanInterval = RescanInterval;
            this.ResultReadInterval = ResultReadInterval;
            this.TableReadInterval = TableReadInterval;
        }
    }
}
