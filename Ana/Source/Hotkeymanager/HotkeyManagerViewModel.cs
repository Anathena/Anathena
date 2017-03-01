namespace Ana.Source.HotkeyManager
{
    using Docking;
    using Editors.HotkeyEditor;
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using Main;
    using Mvvm.Command;
    using Output;
    using Project.ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Threading;
    using System.Windows.Input;
    using Utils.Extensions;

    /// <summary>
    /// View model for the Hotkey Manager.
    /// </summary>
    internal class HotkeyManagerViewModel : ToolViewModel, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(HotkeyManagerViewModel);

        /// <summary>
        /// The file extension for hotkeys.
        /// </summary>
        private const String HotkeyFileExtension = ".hotkeys";

        /// <summary>
        /// Singleton instance of the <see cref="HotkeyManagerViewModel" /> class.
        /// </summary>
        private static Lazy<HotkeyManagerViewModel> inputCorrelatorViewModelInstance = new Lazy<HotkeyManagerViewModel>(
                () => { return new HotkeyManagerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The collection of project item hotkeys.
        /// </summary>
        private List<ProjectItemHotkey> hotKeys;

        /// <summary>
        /// Prevents a default instance of the <see cref="HotkeyManagerViewModel" /> class from being created.
        /// </summary>
        private HotkeyManagerViewModel() : base("Hotkey Manager")
        {
            this.ContentId = HotkeyManagerViewModel.ToolContentId;
            this.NewHotkeyCommand = new RelayCommand(() => this.NewHotkey(), () => true);
            this.hotKeys = new List<ProjectItemHotkey>();

            EngineCore.GetInstance().Input?.GetKeyboardCapture().Subscribe(this);
            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a command to create a new hotkey.
        /// </summary>
        public ICommand NewHotkeyCommand { get; private set; }

        /// <summary>
        /// Gets the collection of project item hotkeys.
        /// </summary>
        public ObservableCollection<ProjectItemHotkey> Hotkeys
        {
            get
            {
                return new ObservableCollection<ProjectItemHotkey>(this.hotKeys == null ? new List<ProjectItemHotkey>() : this.hotKeys);
            }

            set
            {
                this.hotKeys = new List<ProjectItemHotkey>(value);
                this.RaisePropertyChanged(nameof(this.Hotkeys));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="HotkeyManagerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static HotkeyManagerViewModel GetInstance()
        {
            return inputCorrelatorViewModelInstance.Value;
        }

        /// <summary>
        /// Event received when a key is released.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        public void OnKeyPress(SharpDX.DirectInput.Key key)
        {
        }

        /// <summary>
        /// Event received when a key is down.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        public void OnKeyRelease(SharpDX.DirectInput.Key key)
        {
        }

        /// <summary>
        /// Event received when a key is down.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        public void OnKeyDown(SharpDX.DirectInput.Key key)
        {
        }

        /// <summary>
        /// Saves the hotkey profile for this project.
        /// </summary>
        /// <param name="projectFilePath">The path to the main project file.</param>
        public void Save(String projectFilePath)
        {
            try
            {
                String hotkeyFilePath = this.GetHotkeyFilePathFromProjectFilePath(projectFilePath);

                using (FileStream fileStream = new FileStream(hotkeyFilePath, FileMode.Create, FileAccess.Write))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectItemHotkey[]));
                    serializer.WriteObject(fileStream, this.hotKeys.ToArray());
                }
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Unable to save hotkey profile.");
                return;
            }
        }

        /// <summary>
        /// Opens the hotkey profile for this project.
        /// </summary>
        /// <param name="projectFilePath">The path to the main project file.</param>
        public void Open(String projectFilePath)
        {
            try
            {
                String hotkeyFilePath = this.GetHotkeyFilePathFromProjectFilePath(projectFilePath);

                if (File.Exists(hotkeyFilePath))
                {
                    using (FileStream fileStream = new FileStream(hotkeyFilePath, FileMode.Open, FileAccess.Read))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectItemHotkey[]));
                        ProjectItemHotkey[] projectItemHotkeys = serializer.ReadObject(fileStream) as ProjectItemHotkey[];

                        this.hotKeys = projectItemHotkeys == null ? new List<ProjectItemHotkey>() : new List<ProjectItemHotkey>(projectItemHotkeys);
                        this.RaisePropertyChanged(nameof(this.Hotkeys));
                    }
                }
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Unable to open hotkey profile.");
                return;
            }
        }

        /// <summary>
        /// Imports a hotkey profile for this project.
        /// </summary>
        /// <param name="projectFilePath">The path to the imported project file.</param>
        public void Import(String projectFilePath)
        {
            try
            {
                String hotkeyFilePath = this.GetHotkeyFilePathFromProjectFilePath(projectFilePath);

                if (File.Exists(hotkeyFilePath))
                {
                    using (FileStream fileStream = new FileStream(hotkeyFilePath, FileMode.Open, FileAccess.Read))
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ProjectItemHotkey[]));
                        ProjectItemHotkey[] projectItemHotkeys = serializer.ReadObject(fileStream) as ProjectItemHotkey[];

                        if (!projectItemHotkeys.IsNullOrEmpty())
                        {
                            this.hotKeys.AddRange(serializer.ReadObject(fileStream) as ProjectItemHotkey[]);
                            this.RaisePropertyChanged(nameof(this.Hotkeys));
                        }
                    }
                }
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Unable to open hotkey profile.");
                return;
            }
        }

        /// <summary>
        /// Event received when a set of keys are down.
        /// </summary>
        /// <param name="pressedKeys">The down keys.</param>
        public void OnUpdateAllDownKeys(HashSet<SharpDX.DirectInput.Key> pressedKeys)
        {
            foreach (ProjectItemHotkey projectItemHotkey in this.hotKeys)
            {
                if (projectItemHotkey?.Hotkey is KeyboardHotkey)
                {
                    KeyboardHotkey keyboardHotkey = projectItemHotkey.Hotkey as KeyboardHotkey;

                    if (keyboardHotkey.ActivationKeys.All(x => pressedKeys.Contains(x)))
                    {
                        projectItemHotkey.Activate();
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new empty hotkey, not bound to any particular project item.
        /// </summary>
        private void NewHotkey()
        {
            HotkeyEditorModel hotkeyEditor = new HotkeyEditorModel();
            IHotkey newHotkey = hotkeyEditor.EditValue(context: null, provider: null, value: null) as IHotkey;

            if (newHotkey != null)
            {
                this.hotKeys.Add(new ProjectItemHotkey(newHotkey));
                this.RaisePropertyChanged(nameof(this.Hotkeys));
            }
        }

        /// <summary>
        /// Gets the hotkey file path that corresponds to this project file path.
        /// </summary>
        /// <param name="projectFilePath">The path to the project file.</param>
        /// <returns>The file path to the hotkey file.</returns>
        private String GetHotkeyFilePathFromProjectFilePath(String projectFilePath)
        {
            return Path.Combine(Path.GetDirectoryName(projectFilePath), Path.GetFileNameWithoutExtension(projectFilePath)) + HotkeyManagerViewModel.HotkeyFileExtension;
        }
    }
    //// End class
}
//// End namespace