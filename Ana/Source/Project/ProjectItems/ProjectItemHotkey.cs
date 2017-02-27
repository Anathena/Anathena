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
        private Guid targetGuid;

        private IHotkey hotkey;

        public ProjectItemHotkey(IHotkey hotkey)
        {
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