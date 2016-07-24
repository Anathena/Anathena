using Anathema.Source.Engine.OperatingSystems;
using System;
using System.Reflection;
using System.Threading;

namespace Anathema.Source.Utils.Setting
{
    [Obfuscation(Exclude = true)]
    class Settings : ISettingsModel
    {
        // Singleton instance of Settings
        private static Lazy<Settings> SettingsInstance = new Lazy<Settings>(() => { return new Settings(); }, LazyThreadSafetyMode.PublicationOnly);

        private Settings() { }

        public void OnGUIOpen() { }

        [Obfuscation(Exclude = true)]
        public static Settings GetInstance()
        {
            return SettingsInstance.Value;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateTypeSettings(Boolean None, Boolean Private, Boolean Mapped, Boolean Image)
        {
            Properties.Settings.Default.MemoryTypeNone = None;
            Properties.Settings.Default.MemoryTypePrivate = Private;
            Properties.Settings.Default.MemoryTypeMapped = Mapped;
            Properties.Settings.Default.MemoryTypeImage = Image;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateRequiredProtectionSettings(Boolean RequiredWrite, Boolean RequiredExecute, Boolean RequiredCopyOnWrite)
        {
            Properties.Settings.Default.RequiredWrite = RequiredWrite;
            Properties.Settings.Default.RequiredExecute = RequiredExecute;
            Properties.Settings.Default.RequiredCopyOnWrite = RequiredCopyOnWrite;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateIgnoredProtectionSettings(Boolean ExcludedWrite, Boolean ExcludedExecute, Boolean ExcludedCopyOnWrite)
        {
            Properties.Settings.Default.ExcludedWrite = ExcludedWrite;
            Properties.Settings.Default.ExcludedExecute = ExcludedExecute;
            Properties.Settings.Default.ExcludedCopyOnWrite = ExcludedCopyOnWrite;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateFreezeInterval(Int32 FreezeInterval)
        {
            Properties.Settings.Default.FreezeInterval = FreezeInterval;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateRescanInterval(Int32 RescanInterval)
        {
            Properties.Settings.Default.RescanInterval = RescanInterval;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateResultReadInterval(Int32 ResultReadInterval)
        {
            Properties.Settings.Default.ResultReadInterval = ResultReadInterval;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateTableReadInterval(Int32 TableReadInterval)
        {
            Properties.Settings.Default.TableReadInterval = TableReadInterval;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateInputCorrelatorTimeOutInterval(Int32 InputCorrelatorTimeOutInterval)
        {
            Properties.Settings.Default.InputCorrelatorTimeOutInterval = InputCorrelatorTimeOutInterval;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateAlignmentSettings(Int32 Alignment)
        {
            Properties.Settings.Default.Alignment = Alignment;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateIsUserMode(Boolean IsUserMode)
        {
            Properties.Settings.Default.IsUserMode = IsUserMode;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateStartAddress(UInt64 StartAddress)
        {
            Properties.Settings.Default.StartAddress = StartAddress;
        }

        [Obfuscation(Exclude = true)]
        public void UpdateEndAddress(UInt64 EndAddress)
        {
            Properties.Settings.Default.EndAddress = EndAddress;
        }

        [Obfuscation(Exclude = true)]
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

        [Obfuscation(Exclude = true)]
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

        [Obfuscation(Exclude = true)]
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

        [Obfuscation(Exclude = true)]
        public Int32 GetFreezeInterval()
        {
            return Properties.Settings.Default.FreezeInterval;
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetRescanInterval()
        {
            return Properties.Settings.Default.RescanInterval;
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetResultReadInterval()
        {
            return Properties.Settings.Default.ResultReadInterval;
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetTableReadInterval()
        {
            return Properties.Settings.Default.TableReadInterval;
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return Properties.Settings.Default.InputCorrelatorTimeOutInterval;
        }

        [Obfuscation(Exclude = true)]
        public Int32 GetAlignmentSettings()
        {
            return Properties.Settings.Default.Alignment;
        }

        [Obfuscation(Exclude = true)]
        public Boolean GetIsUserMode()
        {
            return Properties.Settings.Default.IsUserMode;
        }

        [Obfuscation(Exclude = true)]
        public UInt64 GetStartAddress()
        {
            return Properties.Settings.Default.StartAddress;
        }

        [Obfuscation(Exclude = true)]
        public UInt64 GetEndAddress()
        {
            return Properties.Settings.Default.EndAddress;
        }

    } // End class

} // End namespace