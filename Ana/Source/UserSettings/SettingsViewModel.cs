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

        private String debug;
        private UInt64 debug2;

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsViewModel" /> class from being created
        /// </summary>
        private SettingsViewModel() : base("Settings")
        {
            this.ContentId = ToolContentId;

            // Subscribe async to avoid a deadlock situation
            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        public String Debug
        {
            get
            {
                return (String)this.debug;
            }
            set
            {
                this.debug = value;
                this.RaisePropertyChanged(nameof(this.Debug));
            }
        }

        public UInt64 Debug2
        {
            get
            {
                return (UInt64)this.debug2;
            }
            set
            {
                this.debug2 = value;
                this.RaisePropertyChanged(nameof(this.Debug2));
            }
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

        public Boolean IsUserMode
        {
            get
            {
                return Settings.Default.IsUserMode;
            }

            set
            {
                Settings.Default.IsUserMode = value;
                this.RaisePropertyChanged(nameof(this.IsUserMode));
                this.RaisePropertyChanged(nameof(this.IsNotUserMode));
            }
        }

        public Boolean IsNotUserMode
        {
            get
            {
                return !Settings.Default.IsUserMode;
            }

            set
            {
                Settings.Default.IsUserMode = !value;
                this.RaisePropertyChanged(nameof(this.IsUserMode));
                this.RaisePropertyChanged(nameof(this.IsNotUserMode));
            }
        }

        public Int32 FreezeInterval
        {
            get
            {
                return Settings.Default.FreezeInterval;
            }

            set
            {
                Settings.Default.FreezeInterval = value;
            }
        }

        public Int32 RescanInterval
        {
            get
            {
                return Settings.Default.RescanInterval;
            }

            set
            {
                Settings.Default.RescanInterval = value;
            }
        }

        public Int32 ResultReadInterval
        {
            get
            {
                return Settings.Default.ResultReadInterval;
            }

            set
            {
                Settings.Default.ResultReadInterval = value;
            }
        }

        public Int32 TableReadInterval
        {
            get
            {
                return Settings.Default.TableReadInterval;
            }

            set
            {
                Settings.Default.TableReadInterval = value;
            }
        }

        public Int32 InputCorrelatorTimeOutInterval
        {
            get
            {
                return Settings.Default.InputCorrelatorTimeOutInterval;
            }

            set
            {
                Settings.Default.InputCorrelatorTimeOutInterval = value;
            }
        }

        public Int32 Alignment
        {
            get
            {
                return Settings.Default.Alignment;
            }

            set
            {
                Settings.Default.Alignment = value;
            }
        }

        public UInt64 StartAddress
        {
            get
            {
                return Settings.Default.StartAddress;
            }

            set
            {
                Settings.Default.StartAddress = value;
            }
        }

        public UInt64 EndAddress
        {
            get
            {
                return Settings.Default.EndAddress;
            }

            set
            {
                Settings.Default.EndAddress = value;
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="SettingsViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static SettingsViewModel GetInstance()
        {
            return SettingsViewModel.settingsViewModelInstance.Value;
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
    }
    //// End class
}
//// End namespace