namespace Ana.Source.Project.ProjectItems
{
    using Engine;
    using Engine.Input.Controller;
    using Engine.Input.HotKeys;
    using Engine.Input.Keyboard;
    using Engine.Input.Mouse;
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Utils.HotkeyEditor;
    using Utils.TypeConverters;

    /// <summary>
    /// A base class for all project items that can be added to the project explorer
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [KnownType(typeof(ProjectItem))]
    [KnownType(typeof(FolderItem))]
    [KnownType(typeof(ScriptItem))]
    [KnownType(typeof(AddressItem))]
    [KnownType(typeof(IHotkey))]
    [KnownType(typeof(KeyboardHotkey))]
    [KnownType(typeof(ControllerHotkey))]
    [KnownType(typeof(MouseHotKey))]
    [DataContract]
    internal abstract class ProjectItem : INotifyPropertyChanged, IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        [Browsable(false)]
        private const Int32 HotkeyDelay = 400;

        [Browsable(false)]
        private ProjectItem parent;

        [Browsable(false)]
        private List<ProjectItem> children;

        [Browsable(false)]
        private String description;

        [Browsable(false)]
        private Boolean isActivated;

        [Browsable(false)]
        private IEnumerable<IHotkey> hotkeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItem" /> class
        /// </summary>
        public ProjectItem() : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItem" /> class
        /// </summary>
        /// <param name="description">The description of the project item</param>
        public ProjectItem(String description)
        {
            // Bypass setters/getters to avoid triggering any view updates in constructor
            this.description = description == null ? String.Empty : description;
            this.parent = null;
            this.children = new List<ProjectItem>();
            this.IsActivated = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the parent of this project item
        /// </summary>
        [Browsable(false)]
        public ProjectItem Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.Parent));
            }
        }

        /// <summary>
        /// Gets or sets the children of this project item
        /// TODO: This belongs literally in nothing but folder item, why the fuck is it here
        /// </summary>
        [DataMember]
        [Browsable(false)]
        public List<ProjectItem> Children
        {
            get
            {
                if (this.children == null)
                {
                    this.children = new List<ProjectItem>();
                }

                return this.children;
            }

            set
            {
                this.children = value;
                ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
                this.NotifyPropertyChanged(nameof(this.Children));
            }
        }

        /// <summary>
        /// Gets or sets the description for this object
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
            }
        }

        /// <summary>
        /// Gets or sets hot keys that activate this project item
        /// </summary>
        [DataMember]
        [TypeConverter(typeof(HotkeyConverter))]
        [Editor(typeof(HotkeyEditorModel), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("HotKeys"), Description("Hot key to activate item")]
        public IEnumerable<IHotkey> Hotkeys
        {
            get
            {
                return this.hotkeys;
            }

            set
            {
                this.hotkeys = value;
                this.UpdateHotkeyListeners();
                this.NotifyPropertyChanged(nameof(this.Hotkeys));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not this item is activated
        /// </summary>
        [Browsable(false)]
        public Boolean IsActivated
        {
            get
            {
                return this.isActivated;
            }

            set
            {
                this.isActivated = value;
                this.OnActivationChanged();
                this.NotifyPropertyChanged(nameof(this.IsActivated));
            }
        }

        /// <summary>
        /// Gets or sets the time since this item was last activated
        /// </summary>
        [Browsable(false)]
        private DateTime LastActivated { get; set; }

        /// <summary>
        /// Invoked when this object is deserialized
        /// </summary>
        /// <param name="streamingContext">Streaming context</param>
        [OnDeserialized]
        public void OnDeserialized(StreamingContext streamingContext)
        {
        }

        /// <summary>
        /// Updates the project item
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Adds a project item as a child under this one
        /// </summary>
        /// <param name="projectItem">The child project item</param>
        public void AddChild(ProjectItem projectItem)
        {
            projectItem.Parent = this;

            if (this.Children == null)
            {
                this.Children = new List<ProjectItem>();
            }

            this.Children.Add(projectItem);
        }

        /// <summary>
        /// Removes a project item as a child under this one
        /// </summary>
        /// <param name="projectItem">The child project item</param>
        public void RemoveChild(ProjectItem projectItem)
        {
            projectItem.Parent = this;

            if (this.Children == null)
            {
                this.Children = new List<ProjectItem>();
            }

            if (this.Children.Contains(projectItem))
            {
                this.Children.Remove(projectItem);
            }
        }

        /// <summary>
        /// Adds a project item as a sibling to this one
        /// </summary>
        /// <param name="projectItem">The child project item</param>
        public void AddSibling(ProjectItem projectItem, Boolean after)
        {
            projectItem.Parent = this.Parent;

            if (after)
            {
                this.Parent?.Children?.Insert(this.Parent.Children.IndexOf(this) + 1, projectItem);
            }
            else
            {
                this.Parent?.Children?.Insert(this.Parent.Children.IndexOf(this), projectItem);
            }
        }

        /// <summary>
        /// Deletes the specified children from this item
        /// </summary>
        /// <param name="toDelete">The children to delete</param>
        public void DeleteChildren(IEnumerable<ProjectItem> toDelete)
        {
            if (toDelete == null)
            {
                return;
            }

            // Sort children and nodes to delete (Makes the algorithm O(nlogn) rather than O(n^2))
            IEnumerable<ProjectItem> childrenSorted = this.Children.ToList().OrderBy(x => x.GetHashCode());
            toDelete = toDelete.OrderBy(x => x.GetHashCode());

            if (toDelete.Count() <= 0 || childrenSorted.Count() <= 0)
            {
                return;
            }

            ProjectItem nextDelete = toDelete.First();
            ProjectItem nextNode = childrenSorted.First();

            toDelete = toDelete.Skip(1);
            childrenSorted = childrenSorted.Skip(1);

            // Walk through both lists and see if there are elements in common and delete them
            while (nextDelete != null && nextNode != null)
            {
                if (nextNode.GetHashCode() > nextDelete.GetHashCode())
                {
                    nextDelete = null;
                }
                else if (nextNode.GetHashCode() < nextDelete.GetHashCode())
                {
                    nextNode = null;
                }
                else if (nextNode.GetHashCode() == nextDelete.GetHashCode())
                {
                    this.Children.Remove(nextNode);

                    nextDelete = null;
                    nextNode = null;
                }

                if (nextDelete == null)
                {
                    if (toDelete.Count() <= 0)
                    {
                        break;
                    }

                    nextDelete = toDelete.First();
                    toDelete = toDelete.Skip(1);
                }

                if (nextNode == null)
                {
                    if (childrenSorted.Count() <= 0)
                    {
                        break;
                    }

                    nextNode = childrenSorted.First();
                    childrenSorted = childrenSorted.Skip(1);
                }
            }
        }

        /// <summary>
        /// Reconstructs the parents for all nodes of this graph. Call this from the root
        /// Needed since we cannot serialize this to json or we will get cyclic dependencies
        /// </summary>
        /// <param name="parent"></param>
        public void BuildParents(ProjectItem parent = null)
        {
            this.Parent = parent;

            foreach (ProjectItem child in this.Children)
            {
                child.BuildParents(this);
            }
        }

        /// <summary>
        /// Determines if this item or any of its children contain an item
        /// </summary>
        /// <param name="projectItem">The item to search for</param>
        /// <returns>Returns true if the item is found</returns>
        public Boolean HasNode(ProjectItem projectItem)
        {
            if (this.Children.Contains(projectItem))
            {
                return true;
            }

            foreach (ProjectItem child in this.Children)
            {
                if (child.HasNode(projectItem))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the specified item from this item's children recursively
        /// </summary>
        /// <param name="projectItem">The item to remove</param>
        /// <returns>Returns true if the removal succeeded</returns>
        public Boolean RemoveNode(ProjectItem projectItem)
        {
            if (projectItem == null)
            {
                return false;
            }

            if (this.Children.Contains(projectItem))
            {
                projectItem.Parent = null;
                this.Children.Remove(projectItem);
                return true;
            }
            else
            {
                foreach (ProjectItem child in this.Children)
                {
                    if (child.RemoveNode(projectItem))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnKeyPress(Key key)
        {
        }

        public void OnKeyDown(Key key)
        {
        }

        public void OnKeyRelease(Key key)
        {
            // Reset hotkey delay if any of the hotkey keys are released
            if (this.Hotkeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.GetActivationKeys().Any(y => key == y)))
            {
                this.LastActivated = DateTime.MinValue;
            }
        }

        public void OnUpdateAllDownKeys(HashSet<Key> pressedKeys)
        {
            if ((DateTime.Now - this.LastActivated).TotalMilliseconds < ProjectItem.HotkeyDelay)
            {
                return;
            }

            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (this.Hotkeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.GetActivationKeys().All(y => pressedKeys.Contains(y))))
            {
                this.LastActivated = DateTime.Now;
                this.IsActivated = !this.IsActivated;
            }
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnActivationChanged()
        {
        }

        private void UpdateHotkeyListeners()
        {
            // Determine if any hotkeys we have are keyboard events
            if (this.Hotkeys != null && this.Hotkeys.Any(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))))
            {
                EngineCore.GetInstance().Input.GetKeyboardCapture().Subscribe(this);
            }
            else
            {
                EngineCore.GetInstance().Input.GetKeyboardCapture().Unsubscribe(this);
            }

            // Determine if any hotkeys we have are controller events
            if (this.Hotkeys != null && this.Hotkeys.Any(x => x.GetType().IsAssignableFrom(typeof(ControllerHotkey))))
            {
                EngineCore.GetInstance().Input.GetControllerCapture().Subscribe(this);
            }
            else
            {
                EngineCore.GetInstance().Input.GetControllerCapture().Unsubscribe(this);
            }

            // Determine if any hotkeys we have are mouse events
            if (this.Hotkeys != null && this.Hotkeys.Any(x => x.GetType().IsAssignableFrom(typeof(MouseHotKey))))
            {
                EngineCore.GetInstance().Input.GetMouseCapture().Subscribe(this);
            }
            else
            {
                EngineCore.GetInstance().Input.GetMouseCapture().Unsubscribe(this);
            }
        }
    }
    //// End class
}
//// End namespace