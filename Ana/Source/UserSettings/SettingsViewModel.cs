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
    /// View model for the Settings.
    /// </summary>
    internal class SettingsViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(SettingsViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        private static Lazy<SettingsViewModel> settingsViewModelInstance = new Lazy<SettingsViewModel>(
                () => { return new SettingsViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Settings that control the degree of parallelism for multithreaded tasks.
        /// </summary>
        private static Lazy<ParallelOptions> parallelSettings = new Lazy<ParallelOptions>(
                () =>
                {
                    ParallelOptions parallelOptions = new ParallelOptions()
                    {
                        // Only use 75% of available processing power, as not to interfere with other programs
                        MaxDegreeOfParallelism = (Environment.ProcessorCount * 3) / 4
                    };
                    return parallelOptions;
                },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="SettingsViewModel"/> class from being created.
        /// </summary>
        private SettingsViewModel() : base("Settings")
        {
            this.ContentId = SettingsViewModel.ToolContentId;

            // Subscribe async to avoid a deadlock situation
            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// Gets the parallelism settings.
        /// </summary>
        public ParallelOptions ParallelSettings
        {
            get
            {
                return parallelSettings.Value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not 'write' flags are required in retrieved virtual memory pages.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not 'execute' flags are required in retrieved virtual memory pages.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not 'copy on write' flags are required in retrieved virtual memory pages.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'write' flags.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'execute' flags.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages exclude those with 'copy on write' flags.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'none' memory type.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'private' memory type.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'mapped' memory type.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages allow 'image' memory type.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages must be in usermode range.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether or not retrieved virtual memory pages can be in any address range.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a the interval of reupdating frozen values.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a the interval between repeated scans.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a the interval between reading scan results.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a the interval between reading values in the table.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a the allowed period of time for a given input to register as correlated with memory changes.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the virtual memory alignment required in scans.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the start address of virtual memory scans.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the end address of virtual memory scans.
        /// </summary>
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
        /// Gets a singleton instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static SettingsViewModel GetInstance()
        {
            return SettingsViewModel.settingsViewModelInstance.Value;
        }

        /// <summary>
        /// Gets the allowed type settings for virtual memory queries based on the set type flags.
        /// </summary>
        /// <returns>The flags of the allowed types for virtual memory queries.</returns>
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

        /// <summary>
        /// Gets the required protection settings for virtual memory queries based on the set type flags.
        /// </summary>
        /// <returns>The flags of the required protections for virtual memory queries.</returns>
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

        /// <summary>
        /// Gets the excluded protection settings for virtual memory queries based on the set type flags.
        /// </summary>
        /// <returns>The flags of the excluded protections for virtual memory queries.</returns>
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