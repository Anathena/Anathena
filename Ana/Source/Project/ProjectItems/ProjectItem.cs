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

        [DataMember]
        [Browsable(false)]
        private Boolean isActivated;

        [Browsable(false)]
        private IEnumerable<IHotkey> hotkeys;

        public ProjectItem() : this(String.Empty)
        {
        }

        public ProjectItem(String description)
        {
            // Bypass setters/getters to avoid triggering any view updates in constructor
            this.description = description == null ? String.Empty : description;
            this.parent = null;
            this.children = new List<ProjectItem>();
            this.IsActivated = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.Parent));
            }
        }

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
                ProjectExplorerDeprecated.GetInstance().ProjectChanged();
                this.NotifyPropertyChanged(nameof(this.Children));
            }
        }

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
                this.NotifyPropertyChanged(nameof(this.Description));
            }
        }

        [DataMember()]
        [TypeConverter(typeof(HotkeyConverter))]
        [Editor(typeof(HotkeyEditorModel), typeof(UITypeEditor))]
        [Category("Properties"), DisplayName("HotKeys"), Description("Hot key to activate item")]
        public IEnumerable<IHotkey> Hotkeys
        {
            get
            {
                return hotkeys;
            }
            set
            {
                hotkeys = value;
                this.UpdateHotkeyListeners();
                this.NotifyPropertyChanged(nameof(this.Hotkeys));
            }
        }

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

        [Browsable(false)]
        private DateTime LastActivated { get; set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext streamingContext)
        {
        }

        public abstract void Update();

        public void AddChild(ProjectItem projectItem)
        {
            projectItem.Parent = this;

            if (this.Children == null)
            {
                this.Children = new List<ProjectItem>();
            }

            this.Children.Add(projectItem);
        }

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

        public void Delete(IEnumerable<ProjectItem> toDelete)
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

        public void BuildParents(ProjectItem parent = null)
        {
            this.Parent = parent;

            foreach (ProjectItem child in this.Children)
            {
                child.BuildParents(this);
            }
        }

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

        public void OnKeyPress(Key key) { }

        public void OnKeyDown(Key key) { }

        public void OnKeyRelease(Key key)
        {
            // Reset hotkey delay if any of the hotkey keys are released
            if (this.Hotkeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.GetActivationKeys().Any(y => key == y)))
            {
                this.LastActivated = DateTime.MinValue;
            }
        }

        public void OnUpdateAllDownKeys(HashSet<Key> PressedKeys)
        {
            if ((DateTime.Now - this.LastActivated).TotalMilliseconds < ProjectItem.HotkeyDelay)
            {
                return;
            }

            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (this.Hotkeys.Where(x => x.GetType().IsAssignableFrom(typeof(KeyboardHotkey))).Cast<KeyboardHotkey>().Any(x => x.GetActivationKeys().All(y => PressedKeys.Contains(y))))
            {
                this.LastActivated = DateTime.Now;
                this.IsActivated = !this.IsActivated;
            }
        }
    }
    //// End class
}
//// End namespace