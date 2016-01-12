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

        private Settings()
        {

        }

        public static Settings GetInstance()
        {
            if (_Settings == null)
                _Settings = new Settings();
            return _Settings;
        }

        public void UpdateRequiredStateSettings(Boolean Free, Boolean Commit, Boolean Reserve)
        {
            Properties.Settings.Default.MemoryStateRequired = (Int32)((Free ? MemoryStateFlags.Commit : 0) | (Commit ? MemoryStateFlags.Commit : 0) | (Reserve ? MemoryStateFlags.Reserve : 0));
        }

        public void UpdateRequiredTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Properties.Settings.Default.MemoryStateRequired = (Int32)((Private ? MemoryTypeFlags.Private : 0) | (Mapped ? MemoryTypeFlags.Mapped : 0) | (Image ? MemoryTypeFlags.Image : 0));
        }

        public void UpdateRequiredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Properties.Settings.Default.MemoryStateRequired = (Int32)((NoAccess ? MemoryProtectionFlags.NoAccess : 0) | (ReadOnly ? MemoryProtectionFlags.ReadOnly : 0) |
                (ReadWrite ? MemoryProtectionFlags.ReadWrite : 0) | (WriteCopy ? MemoryProtectionFlags.WriteCopy : 0) | (Execute ? MemoryProtectionFlags.Execute : 0) |
                (ExecuteRead ? MemoryProtectionFlags.ExecuteRead : 0) | (ExecuteReadWrite ? MemoryProtectionFlags.ExecuteReadWrite : 0) |
                (ExecuteWriteCopy ? MemoryProtectionFlags.ExecuteWriteCopy : 0) | (Guard ? MemoryProtectionFlags.Guard : 0) |
                (NoCache ? MemoryProtectionFlags.NoCache : 0) | (WriteCombine ? MemoryProtectionFlags.WriteCombine : 0));
        }

        public void UpdateIgnoredStateSettings(Boolean Free, Boolean Commit, Boolean Reserve)
        {
            Properties.Settings.Default.MemoryStateIgnored = (Int32)((Free ? MemoryStateFlags.Commit : 0) | (Commit ? MemoryStateFlags.Commit : 0) | (Reserve ? MemoryStateFlags.Reserve : 0));
        }

        public void UpdateIgnoredTypeSettings(Boolean Private, Boolean Mapped, Boolean Image)
        {
            Properties.Settings.Default.MemoryStateIgnored = (Int32)((Private ? MemoryTypeFlags.Private : 0) | (Mapped ? MemoryTypeFlags.Mapped : 0) | (Image ? MemoryTypeFlags.Image : 0));
        }

        public void UpdateIgnoredProtectionSettings(Boolean NoAccess, Boolean ReadOnly, Boolean ReadWrite, Boolean WriteCopy, Boolean Execute,
            Boolean ExecuteRead, Boolean ExecuteReadWrite, Boolean ExecuteWriteCopy, Boolean Guard, Boolean NoCache, Boolean WriteCombine)
        {
            Properties.Settings.Default.MemoryStateIgnored = (Int32)((NoAccess ? MemoryProtectionFlags.NoAccess : 0) | (ReadOnly ? MemoryProtectionFlags.ReadOnly : 0) |
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
            Properties.Settings.Default.ResultReadInterval  = ResultReadInterval;
        }

        public void UpdateTableReadInterval(Int32 TableReadInterval)
        {
            Properties.Settings.Default.TableReadInterval = TableReadInterval;
        }

        public MemoryStateFlags GetRequiredStateSettings()
        {
            return (MemoryStateFlags)Properties.Settings.Default.MemoryStateRequired;
        }

        public MemoryTypeFlags GetRequiredTypeSettings()
        {
            return (MemoryTypeFlags)Properties.Settings.Default.MemoryTypeRequired;
        }

        public MemoryProtectionFlags GetRequiredProtectionSettings()
        {
            return (MemoryProtectionFlags)Properties.Settings.Default.MemoryProtectionRequired;
        }

        public MemoryStateFlags GetIgnoredStateSettings()
        {
            return (MemoryStateFlags)Properties.Settings.Default.MemoryStateIgnored;
        }

        public MemoryTypeFlags GetIgnoredTypeSettings()
        {
            return (MemoryTypeFlags)Properties.Settings.Default.MemoryTypeIgnored;
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
    }
}
