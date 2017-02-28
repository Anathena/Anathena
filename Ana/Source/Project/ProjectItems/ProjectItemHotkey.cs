namespace Ana.Source.Project.ProjectItems
{
    using Engine.Input.HotKeys;
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a hotkey for a project item.
    /// </summary>
    [DataContract]
    internal class ProjectItemHotkey : INotifyPropertyChanged
    {
        private const Int32 ActivationDelayMs = 200;

        private Guid targetGuid;

        private IHotkey hotkey;

        public ProjectItemHotkey(IHotkey hotkey)
        {
            this.TimeSinceLastActivation = DateTime.MinValue;
            this.Hotkey = hotkey;
        }

        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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

        public String HotkeyName
        {
            get
            {
                return this.Hotkey?.ToString();
            }
        }

        private ProjectItem TargetProjectItem { get; set; }

        private DateTime TimeSinceLastActivation { get; set; }

        public void Activate()
        {
            if (DateTime.Now - TimeSinceLastActivation > TimeSpan.FromMilliseconds(ProjectItemHotkey.ActivationDelayMs))
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