namespace Ana.Source.UserSettings
{
    using Docking;
    using Engine.OperatingSystems;
    using Main;
    using Properties;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// View model for the Settings
    /// </summary>
    internal class SettingsViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(SettingsViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SettingsViewModel" /> class
        /// </summary>
        private static Lazy<SettingsViewModel> settingsViewModelInstance = new Lazy<SettingsViewModel>(
                () => { return new SettingsViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsViewModel" /> class from being created
        /// </summary>
        private SettingsViewModel() : base("Settings")
        {
            this.ContentId = ToolContentId;

            // Subscribe async to avoid a deadlock situation
            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        public Boolean RequiredProtectionWrite
        {
            get
            {
                return Settings.Default.RequiredWrite;
            }

            set
            {
                Settings.Default.RequiredWrite = value;
            }
        }

        public Boolean RequiredProtectionExecute
        {
            get
            {
                return Settings.Default.RequiredExecute;
            }

            set
            {
                Settings.Default.RequiredExecute = value;
            }
        }

        public Boolean RequiredProtectionCopyOnWrite
        {
            get
            {
                return Settings.Default.RequiredCopyOnWrite;
            }

            set
            {
                Settings.Default.RequiredCopyOnWrite = value;
            }
        }

        public Boolean ExcludedProtectionWrite
        {
            get
            {
                return Settings.Default.ExcludedWrite;
            }

            set
            {
                Settings.Default.ExcludedWrite = value;
            }
        }

        public Boolean ExcludedProtectionExecute
        {
            get
            {
                return Settings.Default.ExcludedExecute;
            }

            set
            {
                Settings.Default.ExcludedExecute = value;
            }
        }

        public Boolean ExcludedProtectionCopyOnWrite
        {
            get
            {
                return Settings.Default.ExcludedCopyOnWrite;
            }

            set
            {
                Settings.Default.ExcludedCopyOnWrite = value;
            }
        }

        public Boolean MemoryTypeNone
        {
            get
            {
                return Settings.Default.MemoryTypeNone;
            }

            set
            {
                Settings.Default.MemoryTypeNone = value;
            }
        }

        public Boolean MemoryTypePrivate
        {
            get
            {
                return Settings.Default.MemoryTypePrivate;
            }

            set
            {
                Settings.Default.MemoryTypePrivate = value;
            }
        }

        public Boolean MemoryTypeMapped
        {
            get
            {
                return Settings.Default.MemoryTypeMapped;
            }

            set
            {
                Settings.Default.MemoryTypeMapped = value;
            }
        }

        public Boolean MemoryTypeImage
        {
            get
            {
                return Settings.Default.MemoryTypeImage;
            }

            set
            {
                Settings.Default.MemoryTypeImage = value;
            }
        }
        /*
         * 
         * 

        public void UpdateFreezeInterval(Int32 freezeInterval)
        {
            Settings.Default.FreezeInterval = freezeInterval;
        }

        public void UpdateRescanInterval(Int32 rescanInterval)
        {
            Settings.Default.RescanInterval = rescanInterval;
        }

        public void UpdateResultReadInterval(Int32 resultReadInterval)
        {
            Settings.Default.ResultReadInterval = resultReadInterval;
        }

        public void UpdateTableReadInterval(Int32 tableReadInterval)
        {
            Settings.Default.TableReadInterval = tableReadInterval;
        }

        public void UpdateInputCorrelatorTimeOutInterval(Int32 inputCorrelatorTimeOutInterval)
        {
            Settings.Default.InputCorrelatorTimeOutInterval = inputCorrelatorTimeOutInterval;
        }

        public void UpdateAlignmentSettings(Int32 alignment)
        {
            Settings.Default.Alignment = alignment;
        }

        public void UpdateIsUserMode(Boolean isUserMode)
        {
            Settings.Default.IsUserMode = isUserMode;
        }

        public void UpdateStartAddress(UInt64 startAddress)
        {
            Settings.Default.StartAddress = startAddress;
        }

        public void UpdateEndAddress(UInt64 endAddress)
        {
            Settings.Default.EndAddress = endAddress;
        }
        */

        /// <summary>
        /// Gets a singleton instance of the <see cref="SettingsViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SettingsViewModel GetInstance()
        {
            return settingsViewModelInstance.Value;
        }

        public MemoryTypeEnum GetAllowedTypeSettings()
        {
            MemoryTypeEnum result = 0;

            if (Settings.Default.MemoryTypeNone)
            {
                result |= MemoryTypeEnum.None;
            }

            if (Settings.Default.MemoryTypePrivate)
            {
                result |= MemoryTypeEnum.Private;
            }

            if (Settings.Default.MemoryTypeImage)
            {
                result |= MemoryTypeEnum.Image;
            }

            if (Settings.Default.MemoryTypeMapped)
            {
                result |= MemoryTypeEnum.Mapped;
            }

            return result;
        }

        public MemoryProtectionEnum GetRequiredProtectionSettings()
        {
            MemoryProtectionEnum result = 0;

            if (Settings.Default.RequiredWrite)
            {
                result |= MemoryProtectionEnum.Write;
            }

            if (Settings.Default.RequiredExecute)
            {
                result |= MemoryProtectionEnum.Execute;
            }

            if (Settings.Default.RequiredCopyOnWrite)
            {
                result |= MemoryProtectionEnum.CopyOnWrite;
            }

            return result;
        }

        public MemoryProtectionEnum GetExcludedProtectionSettings()
        {
            MemoryProtectionEnum result = 0;

            if (Settings.Default.ExcludedWrite)
            {
                result |= MemoryProtectionEnum.Write;
            }

            if (Settings.Default.ExcludedExecute)
            {
                result |= MemoryProtectionEnum.Execute;
            }

            if (Settings.Default.ExcludedCopyOnWrite)
            {
                result |= MemoryProtectionEnum.CopyOnWrite;
            }

            return result;
        }

        public Int32 GetFreezeInterval()
        {
            return Settings.Default.FreezeInterval;
        }

        public Int32 GetRescanInterval()
        {
            return Settings.Default.RescanInterval;
        }

        public Int32 GetResultReadInterval()
        {
            return Settings.Default.ResultReadInterval;
        }

        public Int32 GetTableReadInterval()
        {
            return Settings.Default.TableReadInterval;
        }

        public Int32 GetInputCorrelatorTimeOutInterval()
        {
            return Settings.Default.InputCorrelatorTimeOutInterval;
        }

        public Int32 GetAlignmentSettings()
        {
            return Settings.Default.Alignment;
        }

        public Boolean GetIsUserMode() // TODO: Boolean prop
        {
            return Settings.Default.IsUserMode;
        }

        public UInt64 GetStartAddress()
        {
            return Settings.Default.StartAddress;
        }

        public UInt64 GetEndAddress()
        {
            return Settings.Default.EndAddress;
        }
    }
    //// End class
}
//// End namespace