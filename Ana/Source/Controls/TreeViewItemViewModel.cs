using Ana.Source.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Ana.Source.Controls
{
    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    internal class TreeViewItemViewModel : ViewModelBase
    {
        private static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        private readonly ObservableCollection<TreeViewItemViewModel> children;
        private readonly TreeViewItemViewModel parent;

        private Boolean isExpanded;
        private Boolean isSelected;

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, Boolean lazyLoadChildren = false)
        {
            this.parent = parent;

            children = new ObservableCollection<TreeViewItemViewModel>();

            if (lazyLoadChildren)
            {
                children.Add(DummyChild);
            }
        }

        // This is used to create the DummyChild instance.
        private TreeViewItemViewModel()
        {
        }

        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public Boolean HasDummyChild
        {
            get
            {
                return this.Children.Count == 1 && this.Children[0] == DummyChild;
            }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public Boolean IsExpanded
        {
            get
            {
                return isExpanded;
            }

            set
            {
                if (value != isExpanded)
                {
                    isExpanded = value;
                    this.RaisePropertyChanged(nameof(this.IsExpanded));
                }

                // Expand all the way up to the root.
                if (isExpanded && parent != null)
                {
                    parent.IsExpanded = true;
                }

                // Lazy load the child items, if necessary.
                if (this.HasDummyChild)
                {
                    this.Children.Remove(DummyChild);
                    this.LoadChildren();
                }
            }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public Boolean IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    this.RaisePropertyChanged(nameof(this.IsSelected));
                }
            }
        }

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }

        public TreeViewItemViewModel Parent
        {
            get { return parent; }
        }
    }
    //// End class
}
//// End namespace