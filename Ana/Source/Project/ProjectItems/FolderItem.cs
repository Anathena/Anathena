namespace Ana.Source.Project.ProjectItems
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines a folder that can be added to the project explorer, which can contain other project items.
    /// </summary>
    [DataContract]
    internal class FolderItem : ProjectItem
    {
        /// <summary>
        /// The children of this folder item.
        /// </summary>
        [Browsable(false)]
        private List<ProjectItem> children;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderItem" /> class.
        /// </summary>
        public FolderItem() : this("New Folder")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderItem" /> class.
        /// </summary>
        /// <param name="description">The description of the folder.</param>
        public FolderItem(String description) : base(description)
        {
            this.children = new List<ProjectItem>();
            this.ChildrenLock = new Object();
        }

        /// <summary>
        /// Gets or sets the children of this project item.
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
        /// Gets or sets a lock for the access of the children in this folder.
        /// </summary>
        private Object ChildrenLock { get; set; }

        /// <summary>
        /// Invoked when this object is deserialized.
        /// </summary>
        /// <param name="streamingContext">Streaming context.</param>
        [OnDeserialized]
        public new void OnDeserialized(StreamingContext streamingContext)
        {
            this.ChildrenLock = new Object();

            if (this.Children == null)
            {
                this.children = new List<ProjectItem>();
            }
        }

        /// <summary>
        /// Clones the project item.
        /// </summary>
        /// <returns>The clone of the project item.</returns>
        public override ProjectItem Clone()
        {
            FolderItem clone = new FolderItem();
            clone.Description = this.Description;
            clone.Parent = this.Parent;
            clone.children = new List<ProjectItem>();

            lock (this.ChildrenLock)
            {
                if (this.Children != null && this.Children.Count > 0)
                {
                    foreach (ProjectItem projectItem in this.Children)
                    {
                        clone.AddChild(projectItem.Clone());
                    }
                }
            }

            return clone;
        }

        public List<ProjectItem> Flatten()
        {
            List<ProjectItem> flattenedItems = new List<ProjectItem>();
            this.FlattenHelper(flattenedItems);

            return flattenedItems;
        }

        public ProjectItem FindProjectItemByGuid(Guid guid)
        {
            foreach (ProjectItem projectItem in this.Flatten())
            {
                if (projectItem.Guid == guid)
                {
                    return projectItem;
                }
            }

            return null;
        }

        /// <summary>
        /// Update event for this project item. Updates all children.
        /// </summary>
        public override void Update()
        {
            lock (this.ChildrenLock)
            {
                this.Children.ForEach(x => x.Update());
            }
        }

        /// <summary>
        /// Reconstructs the parents for all nodes of this graph. Call this from the root.
        /// Needed since we cannot serialize the parent to json or we will get cyclic dependencies.
        /// </summary>
        /// <param name="parent">The parent of this project item.</param>
        public override void BuildParents(FolderItem parent = null)
        {
            base.BuildParents(parent);

            lock (this.ChildrenLock)
            {
                foreach (ProjectItem child in this.Children)
                {
                    child.BuildParents(this as FolderItem);
                }
            }
        }

        /// <summary>
        /// Adds a project item as a child under this one.
        /// </summary>
        /// <param name="newChild">The child project item.</param>
        public void AddChild(ProjectItem newChild)
        {
            lock (this.ChildrenLock)
            {
                newChild.Parent = this;

                if (this.Children == null)
                {
                    this.Children = new List<ProjectItem>();
                }

                this.Children.Add(newChild);
            }

            ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
        }

        /// <summary>
        /// Adds a project item as a sibling to the specified object.
        /// </summary>
        /// <param name="targetChild">The child project item.</param>
        /// <param name="newChild">The new child project item to add as a sibling.</param>
        /// <param name="after">A value indicating whether or not the new child should be inserted before or after the target.</param>
        public void AddSibling(ProjectItem targetChild, ProjectItem newChild, Boolean after)
        {
            lock (this.ChildrenLock)
            {
                if (!this.Children.Contains(targetChild))
                {
                    return;
                }

                newChild.Parent = this;

                if (after)
                {
                    this.Children?.Insert(this.Children.IndexOf(targetChild) + 1, newChild);
                }
                else
                {
                    this.Children?.Insert(this.Children.IndexOf(targetChild), newChild);
                }
            }

            ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;
        }

        /// <summary>
        /// Determines if this item or any of its children contain an item.
        /// </summary>
        /// <param name="projectItem">The item to search for.</param>
        /// <returns>Returns true if the item is found.</returns>
        public Boolean HasNode(ProjectItem projectItem)
        {
            lock (this.ChildrenLock)
            {
                if (this.Children.Contains(projectItem))
                {
                    return true;
                }

                foreach (ProjectItem child in this.Children)
                {
                    if (child is FolderItem)
                    {
                        if (child != null && (child as FolderItem).HasNode(projectItem))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the specified items from this item's children recursively.
        /// </summary>
        /// <param name="projectItems">The items to remove.</param>
        public void RemoveNodes(IEnumerable<ProjectItem> projectItems)
        {
            if (projectItems == null)
            {
                return;
            }

            foreach (ProjectItem projectItem in projectItems)
            {
                this.RemoveNode(projectItem);
            }
        }

        /// <summary>
        /// Removes the specified item from this item's children recursively.
        /// </summary>
        /// <param name="projectItem">The item to remove.</param>
        /// <returns>Returns true if the removal succeeded.</returns>
        public Boolean RemoveNode(ProjectItem projectItem)
        {
            Boolean removeSuccess = false;

            if (projectItem == null)
            {
                return false;
            }

            lock (this.ChildrenLock)
            {
                if (this.Children.Contains(projectItem))
                {
                    projectItem.Parent = null;
                    this.Children.Remove(projectItem);
                    removeSuccess = true;
                }
                else
                {
                    foreach (ProjectItem child in this.Children)
                    {
                        if (child is FolderItem)
                        {
                            if (child != null && (child as FolderItem).RemoveNode(projectItem))
                            {
                                removeSuccess = true;
                            }
                        }
                    }
                }
            }

            ProjectExplorerViewModel.GetInstance().HasUnsavedChanges = true;

            return removeSuccess;
        }

        /// <summary>
        /// Function indicating if this script can be activated.
        /// </summary>
        /// <returns>Always false for folders.</returns>
        protected override Boolean IsActivatable()
        {
            return false;
        }

        private void FlattenHelper(List<ProjectItem> flattenedList)
        {
            foreach (ProjectItem projectItem in this.Children)
            {
                if (!(projectItem is FolderItem))
                {
                    flattenedList.Add(projectItem);
                }
                else
                {
                    FolderItem folderItem = projectItem as FolderItem;
                    folderItem.FlattenHelper(flattenedList);
                }
            }
        }
    }
    //// End class
}
//// End namespace