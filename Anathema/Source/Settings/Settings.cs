using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Native;
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

        private Settings()
        {

        }

        public static Settings GetInstance()
        {
            if (_Settings == null)
                _Settings = new Settings();
            return _Settings;
        }

        public void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image)
        {
            Properties.Settings.Default.MemoryTypeNone = None;
            Properties.Settings.Default.MemoryTypePrivate = Private;
            Properties.Settings.Default.MemoryTypeMapped = Mapped;
            Properties.Settings.Default.MemoryTypeImage = Image;
        }

        public void UpdateRequiredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Properties.Settings.Default.MemoryProtectionRequired = (Int32)((NoAccess ? MemoryProtectionFlags.NoAccess : 0) | (ReadOnly ? MemoryProtectionFlags.ReadOnly : 0) |
                (ReadWrite ? MemoryProtectionFlags.ReadWrite : 0) | (WriteCopy ? MemoryProtectionFlags.WriteCopy : 0) | (Execute ? MemoryProtectionFlags.Execute : 0) |
                (ExecuteRead ? MemoryProtectionFlags.ExecuteRead : 0) | (ExecuteReadWrite ? MemoryProtectionFlags.ExecuteReadWrite : 0) |
                (ExecuteWriteCopy ? MemoryProtectionFlags.ExecuteWriteCopy : 0) | (Guard ? MemoryProtectionFlags.Guard : 0) |
                (NoCache ? MemoryProtectionFlags.NoCache : 0) | (WriteCombine ? MemoryProtectionFlags.WriteCombine : 0));
        }

        public void UpdateIgnoredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Properties.Settings.Default.MemoryProtectionIgnored = (Int32)((NoAccess ? MemoryProtectionFlags.NoAccess : 0) | (ReadOnly ? MemoryProtectionFlags.ReadOnly : 0) |
                (ReadWrite ? MemoryProtectionFlags.ReadWrite : 0) | (WriteCopy ? MemoryProtectionFlags.WriteCopy : 0) | (Execute ? MemoryProtectionFlags.Execute : 0) |
                (ExecuteRead ? MemoryProtectionFlags.ExecuteRead : 0) | (ExecuteReadWrite ? MemoryProtectionFlags.ExecuteReadWrite : 0) |
                (ExecuteWriteCopy ? MemoryProtectionFlags.ExecuteWriteCopy : 0) | (Guard ? MemoryProtectionFlags.Guard : 0) |
                (NoCache ? MemoryProtectionFlags.NoCache : 0) | (WriteCombine ? MemoryProtectionFlags.WriteCombine : 0));
        }

        public void UpdateFreezeInterval(Int32 FreezeInterval)
        {
            Properties.Settings.Default.FreezeInterval = FreezeInterval;
        }

        public void UpdateRescanInterval(Int32 RescanInterval)
        {
            Properties.Settings.Default.RescanInterval = RescanInterval;
        }

        public void UpdateResultReadInterval(Int32 ResultReadInterval)
        {
            Properties.Settings.Default.ResultReadInterval = ResultReadInterval;
        }

        public void UpdateTableReadInterval(Int32 TableReadInterval)
        {
            Properties.Settings.Default.TableReadInterval = TableReadInterval;
        }

        public void UpdateInputCorrelatorTimeOutInterval(Int32 InputCorrelatorTimeOutInterval)
        {
            Properties.Settings.Default.InputCorrelatorTimeOutInterval = InputCorrelatorTimeOutInterval;
        }

        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            Properties.Settings.Default.Alignment = Alignment;
        }

        public Boolean[] GetTypeSettings()
        {
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeFlags));
            Boolean[] TypeSettings = new Boolean[TypeEnumValues.Length];
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.None)] = Properties.Settings.Default.MemoryTypeNone;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Private)] = Properties.Settings.Default.MemoryTypePrivate;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Image)] = Properties.Settings.Default.MemoryTypeImage;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeFlags.Mapped)] = Properties.Settings.Default.MemoryTypeMapped;

            return TypeSettings;
        }

        public MemoryProtectionFlags GetRequiredProtectionSettings()
        {
            return (MemoryProtectionFlags)Properties.Settings.Default.MemoryProtectionRequired;
        }

        public MemoryProtectionFlags GetIgnoredProtectionSettings()
        {
            return (MemoryProtectionFlags)Properties.Settings.Default.MemoryProtectionIgnored;
        }

        public Int32 GetFreezeInterval()
        {
            return Properties.Settings.Default.FreezeInterval;
        }

        public Int32 GetRescanInterval()
        {
            return Properties.Settings.Default.RescanInterval;
        }

        public Int32 GetResultReadInterval()
        {
            return Properties.Settings.Default.ResultReadInterval;
        }

        public Int32 GetTableReadInterval()
        {
            return Properties.Settings.Default.TableReadInterval;
        }

        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return Properties.Settings.Default.InputCorrelatorTimeOutInterval;
        }

        public Int32 GetAlignmentSettings()
        {
            return Properties.Settings.Default.Alignment;
        }

    } // End class

} // End namespace