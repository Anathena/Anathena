using System;

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

        public Boolean[] GetTypeSettings()
        {
            Array TypeEnumValues = Enum.GetValues(typeof(MemoryTypeEnum));
            Boolean[] TypeSettings = new Boolean[TypeEnumValues.Length];
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeEnum.None)] = Properties.Settings.Default.MemoryTypeNone;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeEnum.Private)] = Properties.Settings.Default.MemoryTypePrivate;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeEnum.Image)] = Properties.Settings.Default.MemoryTypeImage;
            TypeSettings[Array.IndexOf(TypeEnumValues, MemoryTypeEnum.Mapped)] = Properties.Settings.Default.MemoryTypeMapped;

            return TypeSettings;
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

    } // End class

} // End namespace