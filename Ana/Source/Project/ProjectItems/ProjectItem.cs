namespace Ana.Source.Project.ProjectItems
{
    using Editors.TextEditor;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.Serialization;
    using Utils.TypeConverters;

    /// <summary>
    /// A base class for all project items that can be added to the project explorer.
    /// </summary>
    [KnownType(typeof(ProjectItem))]
    [KnownType(typeof(FolderItem))]
    [KnownType(typeof(ScriptItem))]
    [KnownType(typeof(AddressItem))]
    [DataContract]
    internal abstract class ProjectItem : INotifyPropertyChanged
    {
        /// <summary>
        /// The parent of this project item.
        /// </summary>
        [Browsable(false)]
        private FolderItem parent;

        /// <summary>
        /// The description of this project item.
        /// </summary>
        [Browsable(false)]
        private String description;

        /// <summary>
        /// The extended description of this project item.
        /// </summary>
        [Browsable(false)]
        private String extendedDescription;

        /// <summary>
        /// The unique identifier of this project item.
        /// </summary>
        [Browsable(false)]
        private Guid guid;

        /// <summary>
        /// A value indicating whether this project item has been activated.
        /// </summary>
        [Browsable(false)]
        private Boolean isActivated;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItem" /> class.
        /// </summary>
        public ProjectItem() : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItem" /> class.
        /// </summary>
        /// <param name="description">The description of the project item.</param>
        public ProjectItem(String description)
        {
            // Bypass setters/getters to avoid triggering any view updates in constructor
            this.description = description == null ? String.Empty : description;
            this.parent = null;
            this.isActivated = false;
            this.guid = Guid.NewGuid();
        }

        /// <summary>
        /// Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the parent of this project item.
        /// </summary>
        [Browsable(false)]
        public FolderItem Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
                this.NotifyPropertyChanged(nameof(this.Parent));
            }
        }

        /// <summary>
        /// Gets or sets the description for this object.
        /// </summary>
        [DataMember]
        [Category("Properties"), DisplayName("Description"), Description("Description to be shown for the Project Items")]
        public String Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.Description));
                ProjectExplorerViewModel.GetInstance().OnPropertyUpdate();
            }
        }

        /// <summary>
        /// Gets or sets the description for this object.
        /// </summary>
        [DataMember]
        [TypeConverter(typeof(TextConverter))]
        [Editor(typeof(TextEditorModel), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("Extended Description"), Description("Extended description for additional information about this item")]
        public String ExtendedDescription
        {
            get
            {
                return this.extendedDescription;
            }

            set
            {
                this.extendedDescription = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.ExtendedDescription));
                ProjectExplorerViewModel.GetInstance().OnPropertyUpdate();
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier of this project item.
        /// </summary>
        [DataMember]
        // [Browsable(false)]
        [Category("Properties"), DisplayName("GUID"), Description("Extended description for additional information about this item")]
        public Guid Guid
        {
            get
            {
                return this.guid;
            }

            set
            {
                this.guid = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.Guid));
                ProjectExplorerViewModel.GetInstance().OnPropertyUpdate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not this item is activated.
        /// </summary>
        [Browsable(false)]
        public Boolean IsActivated
        {
            get
            {
                return this.CanActivate && this.isActivated;
            }

            set
            {
                if (!this.CanActivate)
                {
                    return;
                }

                this.isActivated = value;
                this.OnActivationChanged();
                this.NotifyPropertyChanged(nameof(this.IsActivated));
                ProjectExplorerViewModel.GetInstance().OnPropertyUpdate();
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this item can be activated.
        /// </summary>
        [Browsable(false)]
        public Boolean CanActivate
        {
            get
            {
                return this.IsActivatable();
            }
        }

        /// <summary>
        /// Gets or sets the time since this item was last activated.
        /// </summary>
        [Browsable(false)]
        private DateTime LastActivated { get; set; }

        /// <summary>
        /// Invoked when this object is deserialized.
        /// </summary>
        /// <param name="streamingContext">Streaming context</param>
        [OnDeserialized]
        public void OnDeserialized(StreamingContext streamingContext)
        {
            if (this.Guid == null || this.Guid == Guid.Empty)
            {
                this.guid = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Updates event for this project item.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Reconstructs the parents for all nodes of this graph. Call this from the root.
        /// Needed since we cannot serialize the parent to json or we will get cyclic dependencies.
        /// </summary>
        /// <param name="parent">The parent of this project item.</param>
        public virtual void BuildParents(FolderItem parent = null)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Clones the project item.
        /// </summary>
        /// <returns>The clone of the project item.</returns>
        public abstract ProjectItem Clone();

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void NotifyPropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Deactivates this item without triggering the <see cref="OnActivationChanged" /> function.
        /// </summary>
        protected void ResetActivation()
        {
            this.isActivated = false;
            this.NotifyPropertyChanged(nameof(this.IsActivated));
        }

        /// <summary>
        /// Called when the activation state changes.
        /// </summary>
        protected virtual void OnActivationChanged()
        {
        }

        /// <summary>
        /// Overridable function indicating if this script can be activated.
        /// </summary>
        /// <returns>True if the script can be activated, otherwise false.</returns>
        protected virtual Boolean IsActivatable()
        {
            return true;
        }
    }
    //// End class
}
//// End namespace