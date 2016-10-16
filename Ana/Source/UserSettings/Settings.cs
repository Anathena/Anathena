namespace Ana.Source.UserSettings
{
    using Engine.OperatingSystems;
    using System;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal class Settings
    {
        private static Lazy<Settings> settingsInstance = new Lazy<Settings>(
            () => { return new Settings(); },
            LazyThreadSafetyMode.PublicationOnly);

        private Settings()
        {
        }

        public static Settings GetInstance()
        {
            return settingsInstance.Value;
        }

        public void OnGUIOpen()
        {
        }

        public void UpdateTypeSettings(Boolean noneMemory, Boolean privateMemory, Boolean mappedMemory, Boolean imageMemory)
        {
            Properties.Settings.Default.MemoryTypeNone = noneMemory;
            Properties.Settings.Default.MemoryTypePrivate = privateMemory;
            Properties.Settings.Default.MemoryTypeMapped = mappedMemory;
            Properties.Settings.Default.MemoryTypeImage = imageMemory;
        }

        public void UpdateRequiredProtectionSettings(Boolean requiredWrite, Boolean requiredExecute, Boolean requiredCopyOnWrite)
        {
            Properties.Settings.Default.RequiredWrite = requiredWrite;
            Properties.Settings.Default.RequiredExecute = requiredExecute;
            Properties.Settings.Default.RequiredCopyOnWrite = requiredCopyOnWrite;
        }

        public void UpdateIgnoredProtectionSettings(Boolean excludedWrite, Boolean excludedExecute, Boolean excludedCopyOnWrite)
        {
            Properties.Settings.Default.ExcludedWrite = excludedWrite;
            Properties.Settings.Default.ExcludedExecute = excludedExecute;
            Properties.Settings.Default.ExcludedCopyOnWrite = excludedCopyOnWrite;
        }

        public void UpdateFreezeInterval(Int32 freezeInterval)
        {
            Properties.Settings.Default.FreezeInterval = freezeInterval;
        }

        public void UpdateRescanInterval(Int32 rescanInterval)
        {
            Properties.Settings.Default.RescanInterval = rescanInterval;
        }

        public void UpdateResultReadInterval(Int32 resultReadInterval)
        {
            Properties.Settings.Default.ResultReadInterval = resultReadInterval;
        }

        public void UpdateTableReadInterval(Int32 tableReadInterval)
        {
            Properties.Settings.Default.TableReadInterval = tableReadInterval;
        }

        public void UpdateInputCorrelatorTimeOutInterval(Int32 inputCorrelatorTimeOutInterval)
        {
            Properties.Settings.Default.InputCorrelatorTimeOutInterval = inputCorrelatorTimeOutInterval;
        }

        public void UpdateAlignmentSettings(Int32 alignment)
        {
            Properties.Settings.Default.Alignment = alignment;
        }

        public void UpdateIsUserMode(Boolean isUserMode)
        {
            Properties.Settings.Default.IsUserMode = isUserMode;
        }

        public void UpdateStartAddress(UInt64 startAddress)
        {
            Properties.Settings.Default.StartAddress = startAddress;
        }

        public void UpdateEndAddress(UInt64 endAddress)
        {
            Properties.Settings.Default.EndAddress = endAddress;
        }

        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            MemoryTypeEnum result = 0;

            if (Properties.Settings.Default.MemoryTypeNone)
            {
                result |= MemoryTypeEnum.None;
            }

            if (Properties.Settings.Default.MemoryTypePrivate)
            {
                result |= MemoryTypeEnum.Private;
            }

            if (Properties.Settings.Default.MemoryTypeImage)
            {
                result |= MemoryTypeEnum.Image;
            }

            if (Properties.Settings.Default.MemoryTypeMapped)
            {
                result |= MemoryTypeEnum.Mapped;
            }

            return result;
        }

        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            MemoryProtectionEnum result = 0;

            if (Properties.Settings.Default.RequiredWrite)
            {
                result |= MemoryProtectionEnum.Write;
            }

            if (Properties.Settings.Default.RequiredExecute)
            {
                result |= MemoryProtectionEnum.Execute;
            }

            if (Properties.Settings.Default.RequiredCopyOnWrite)
            {
                result |= MemoryProtectionEnum.CopyOnWrite;
            }

            return result;
        }

        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            MemoryProtectionEnum result = 0;

            if (Properties.Settings.Default.ExcludedWrite)
            {
                result |= MemoryProtectionEnum.Write;
            }

            if (Properties.Settings.Default.ExcludedExecute)
            {
                result |= MemoryProtectionEnum.Execute;
            }

            if (Properties.Settings.Default.ExcludedCopyOnWrite)
            {
                result |= MemoryProtectionEnum.CopyOnWrite;
            }

            return result;
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
    }
    //// End class
}
//// End namespace