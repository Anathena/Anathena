using Anathema.Utils.OS;
using System;

namespace Anathema.User.UserSettings
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

        public void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite)
        {
            Properties.Settings.Default.RequiredWrite = RequiredWrite;
            Properties.Settings.Default.RequiredExecute = RequiredExecute;
            Properties.Settings.Default.RequiredCopyOnWrite = RequiredCopyOnWrite;
        }

        public void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            Properties.Settings.Default.ExcludedWrite = ExcludedWrite;
            Properties.Settings.Default.ExcludedExecute = ExcludedExecute;
            Properties.Settings.Default.ExcludedCopyOnWrite = ExcludedCopyOnWrite;
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

        public void UpdateIsUserMode(Boolean IsUserMode)
        {
            Properties.Settings.Default.IsUserMode = IsUserMode;
        }

        public void UpdateStartAddress(UInt64 StartAddress)
        {
            Properties.Settings.Default.StartAddress = StartAddress;
        }

        public void UpdateEndAddress(UInt64 EndAddress)
        {
            Properties.Settings.Default.EndAddress = EndAddress;
        }

        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            MemoryTypeEnum Result = 0;

            if (Properties.Settings.Default.MemoryTypeNone)
                Result |= MemoryTypeEnum.None;
            if (Properties.Settings.Default.MemoryTypePrivate)
                Result |= MemoryTypeEnum.Private;
            if (Properties.Settings.Default.MemoryTypeImage)
                Result |= MemoryTypeEnum.Image;
            if (Properties.Settings.Default.MemoryTypeMapped)
                Result |= MemoryTypeEnum.Mapped;

            return Result;
        }

        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            MemoryProtectionEnum Result = 0;

            if (Properties.Settings.Default.RequiredWrite)
                Result |= MemoryProtectionEnum.Write;
            if (Properties.Settings.Default.RequiredExecute)
                Result |= MemoryProtectionEnum.Execute;
            if (Properties.Settings.Default.RequiredCopyOnWrite)
                Result |= MemoryProtectionEnum.CopyOnWrite;

            return Result;
        }

        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            MemoryProtectionEnum Result = 0;

            if (Properties.Settings.Default.ExcludedWrite)
                Result |= MemoryProtectionEnum.Write;
            if (Properties.Settings.Default.ExcludedExecute)
                Result |= MemoryProtectionEnum.Execute;
            if (Properties.Settings.Default.ExcludedCopyOnWrite)
                Result |= MemoryProtectionEnum.CopyOnWrite;

            return Result;
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

        public Boolean GetIsUserMode()
        {
            return Properties.Settings.Default.IsUserMode;
        }

        public UInt64 GetStartAddress()
        {
            return Properties.Settings.Default.StartAddress;
        }

        public UInt64 GetEndAddress()
        {
            return Properties.Settings.Default.EndAddress;
        }

    } // End class

} // End namespace