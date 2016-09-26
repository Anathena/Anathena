using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Ana.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [KnownType(typeof(ProjectItem))]
    [KnownType(typeof(FolderItem))]
    [KnownType(typeof(ScriptItem))]
    [KnownType(typeof(AddressItem))]
    //[KnownType(typeof(IHotKey))]
    //[KnownType(typeof(KeyboardHotKey))]
    //[KnownType(typeof(ControllerHotKey))]
    //[KnownType(typeof(MouseHotKey))]
    [DataContract()]
    public abstract class ProjectItem // : IKeyboardObserver, IControllerObserver, IMouseObserver
    {
        [Browsable(false)]
        private const Int32 HotKeyDelay = 400;

        [Browsable(false)]
        private ProjectItem parent;

        [Browsable(false)]
        public ProjectItem Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }

        [Browsable(false)]
        private List<ProjectItem> children;

        [DataMember()]
        [Browsable(false)]
        public List<ProjectItem> Children
        {
            get
            {
                if (children == null)
                {
                    children = new List<ProjectItem>();
                }

                return children;
            }
            set
            {
                children = value;

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }

        [Browsable(false)]
        private String description;

        [DataMember()]
        [Category("Properties"), DisplayName("Description"), Description("Description to be shown for the Project Items")]
        public String Description
        {
            get { return description; }
            set
            {
                description = value; UpdateEntryVisual();

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }

        /*
        [Browsable(false)]
        private IEnumerable<IHotKey> _HotKeys;

        [DataMember()]
        [Category("Properties"), DisplayName("HotKeys"), Description("Hot key to activate item")]
        public IEnumerable<IHotKey> HotKeys
        {
            get { return _HotKeys; }
            set
            {
                _HotKeys = value; UpdateHotKeyListeners();

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }*/

        [DataMember()]
        [Browsable(false)]
        private UInt32 textColorARGB;

        [Category("Properties"), DisplayName("Text Color"), Description("Display Color")]
        public Color TextColor
        {
            get { return Color.FromArgb(unchecked((Int32)(textColorARGB))); }
            set
            {
                textColorARGB = value == null ? 0 : unchecked((UInt32)(value.ToArgb())); UpdateEntryVisual();

                ProjectExplorer.GetInstance().ProjectChanged();
            }
        }

        [Browsable(false)]
        protected Boolean Activated { get; set; }

        public ProjectItem() : this(String.Empty) { }
        public ProjectItem(String Description)
        {
            // Bypass setters/getters to avoid triggering any GUI updates in constructor
            this.description = Description == null ? String.Empty : Description;
            this.parent = null;
            this.children = new List<ProjectItem>();
            this.textColorARGB = unchecked((UInt32)SystemColors.ControlText.ToArgb());
            this.Activated = false;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext streamingContext)
        {

        }

        public virtual void SetActivationState(Boolean activated)
        {
            this.Activated = activated;
        }

        public Boolean GetActivationState()
        {
            return Activated;
        }

        public void AddChild(ProjectItem projectItem)
        {
            projectItem.Parent = this;

            if (Children == null)
                Children = new List<ProjectItem>();

            Children.Add(projectItem);
        }

        public void AddSibling(ProjectItem projectItem, Boolean after)
        {
            projectItem.Parent = this.Parent;

            if (after)
                Parent?.Children?.Insert(Parent.Children.IndexOf(this) + 1, projectItem);
            else
                Parent?.Children?.Insert(Parent.Children.IndexOf(this), projectItem);
        }

        public void Delete(IEnumerable<ProjectItem> toDelete)
        {
            if (toDelete == null)
            {
                return;
            }

            // Sort children and nodes to delete (Makes the algorithm O(nlogn) rather than O(n^2))
            IEnumerable<ProjectItem> childrenSorted = Children.ToList().OrderBy(X => X.GetHashCode());
            toDelete = toDelete.OrderBy(X => X.GetHashCode());

            if (toDelete.Count() <= 0 || childrenSorted.Count() <= 0)
                return;

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
                    Children.Remove(nextNode);

                    nextDelete = null;
                    nextNode = null;
                }

                if (nextDelete == null)
                {
                    if (toDelete.Count() <= 0)
                        break;

                    nextDelete = toDelete.First();
                    toDelete = toDelete.Skip(1);
                }

                if (nextNode == null)
                {
                    if (childrenSorted.Count() <= 0)
                        break;

                    nextNode = childrenSorted.First();
                    childrenSorted = childrenSorted.Skip(1);
                }
            }
        }

        public void BuildParents(ProjectItem parent = null)
        {
            this.Parent = parent;

            foreach (ProjectItem child in Children)
            {
                child.BuildParents(this);
            }
        }

        public Boolean HasNode(ProjectItem ProjectItem)
        {
            if (Children.Contains(ProjectItem))
            {
                return true;
            }

            foreach (ProjectItem Child in Children)
            {
                if (Child.HasNode(ProjectItem))
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

            if (Children.Contains(projectItem))
            {
                projectItem.Parent = null;
                Children.Remove(projectItem);
                return true;
            }
            else
            {
                foreach (ProjectItem Child in Children)
                {
                    if (Child.RemoveNode(projectItem))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateEntryVisual()
        {
            ProjectExplorer.GetInstance().RefreshProjectStructure();
        }

        public abstract void Update();

        /*
        private void UpdateHotKeyListeners()
        {
            // Determine if any hotkeys we have are keyboard events
            if (HotKeys != null && HotKeys.Any(X => X.GetType().IsAssignableFrom(typeof(KeyboardHotKey))))
                EngineCore.InputManager.GetKeyboardCapture().Subscribe(this);
            else
                EngineCore.InputManager.GetKeyboardCapture().Unsubscribe(this);

            // Determine if any hotkeys we have are controller events
            if (HotKeys != null && HotKeys.Any(X => X.GetType().IsAssignableFrom(typeof(ControllerHotKey))))
                EngineCore.InputManager.GetControllerCapture().Subscribe(this);
            else
                EngineCore.InputManager.GetControllerCapture().Unsubscribe(this);

            // Determine if any hotkeys we have are mouse events
            if (HotKeys != null && HotKeys.Any(X => X.GetType().IsAssignableFrom(typeof(MouseHotKey))))
                EngineCore.InputManager.GetMouseCapture().Subscribe(this);
            else
                EngineCore.InputManager.GetMouseCapture().Unsubscribe(this);
           
        }

        public void OnKeyPress(Key Key) { }

        public void OnKeyDown(Key Key) { }

        public void OnKeyRelease(Key Key)
        {
            // Reset hotkey delay if any of the hotkey keys are released
            if (HotKeys.Where(X => X.GetType().IsAssignableFrom(typeof(KeyboardHotKey))).Cast<KeyboardHotKey>().Any(X => X.GetActivationKeys().Any(Y => Key == Y)))
                LastActivated = DateTime.MinValue;
        }

        public void OnUpdateAllDownKeys(HashSet<Key> PressedKeys)
        {
            if ((DateTime.Now - LastActivated).TotalMilliseconds < HotKeyDelay)
                return;

            // If any of our keyboard hotkeys include the current set of pressed keys, trigger activation/deactivation
            if (HotKeys.Where(X => X.GetType().IsAssignableFrom(typeof(KeyboardHotKey))).Cast<KeyboardHotKey>().Any(X => X.GetActivationKeys().All(Y => PressedKeys.Contains(Y))))
            {
                LastActivated = DateTime.Now;
                SetActivationState(!Activated);
                UpdateEntryVisual();
            }
        }
         */
    } // End class

} // End namespace