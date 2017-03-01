namespace Ana.Source.Project.ProjectItems
{
    using Engine.Input.HotKeys;
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a hotkey for a project item.
    /// </summary>
    [KnownType(typeof(IHotkey))]
    [DataContract]
    internal class ProjectItemHotkey : INotifyPropertyChanged
    {
        /// <summary>
        /// The delay in miliseconds between hotkey activations
        /// </summary>
        private const Int32 ActivationDelayMs = 400;

        /// <summary>
        /// The target guid, from which the target project is derived.
        /// </summary>
        private Guid targetGuid;

        /// <summary>
        /// The hotkey bound to the project item.
        /// </summary>
        private IHotkey hotkey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItemHotkey" /> class.
        /// </summary>
        /// <param name="hotkey">The initial hotkey bound to the project item.</param>
        public ProjectItemHotkey(IHotkey hotkey)
        {
            this.TimeSinceLastActivation = DateTime.MinValue;
            this.Hotkey = hotkey;
        }

        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the target guid, from which the target project is derived.
        /// </summary>
        [DataMember]
        public Guid TargetGuid
        {
            get
            {
                return this.targetGuid;
            }

            set
            {
                this.targetGuid = value;
                this.TargetProjectItem = ProjectExplorerViewModel.GetInstance().ProjectRoot.FindProjectItemByGuid(this.targetGuid);
                this.NotifyPropertyChanged(nameof(this.TargetGuid));
            }
        }

        /// <summary>
        /// Gets or sets the hotkey bound to the project item.
        /// </summary>
        [DataMember]
        public IHotkey Hotkey
        {
            get
            {
                return this.hotkey;
            }

            set
            {
                this.hotkey = value;
                this.NotifyPropertyChanged(nameof(this.Hotkey));
            }
        }

        /// <summary>
        /// Gets the name of the hotkey keys as a string.
        /// </summary>
        public String HotkeyName
        {
            get
            {
                return this.Hotkey?.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the project item bound to the hotkey.
        /// </summary>
        private ProjectItem TargetProjectItem { get; set; }

        /// <summary>
        /// Gets or sets the time since the hotkey was last triggered.
        /// </summary>
        private DateTime TimeSinceLastActivation { get; set; }

        /// <summary>
        /// Activates this hotkey, toggling the corresponding project item.
        /// </summary>
        public void Activate()
        {
            if (DateTime.Now - this.TimeSinceLastActivation > TimeSpan.FromMilliseconds(ProjectItemHotkey.ActivationDelayMs))
            {
                if (this.TargetProjectItem != null)
                {
                    this.TargetProjectItem.IsActivated = !this.TargetProjectItem.IsActivated;
                }

                this.TimeSinceLastActivation = DateTime.Now;
            }
        }

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void NotifyPropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //// End class
}
//// End namespace