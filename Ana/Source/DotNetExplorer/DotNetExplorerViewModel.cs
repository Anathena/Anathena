namespace Ana.Source.DotNetExplorer
{
    using Docking;
    using Engine.AddressResolver;
    using Engine.AddressResolver.DotNet;
    using Main;
    using Mvvm.Command;
    using Project;
    using Project.ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// View model for the .Net Explorer.
    /// </summary>
    internal class DotNetExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(DotNetExplorerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="DotNetExplorerViewModel" /> class.
        /// </summary>
        private static Lazy<DotNetExplorerViewModel> dotNetExplorerViewModelInstance = new Lazy<DotNetExplorerViewModel>(
                () => { return new DotNetExplorerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The collection of dot net objects in the target process.
        /// </summary>
        private ReadOnlyCollection<DotNetObjectViewModel> dotNetObjects;

        /// <summary>
        /// Prevents a default instance of the <see cref="DotNetExplorerViewModel" /> class from being created.
        /// </summary>
        private DotNetExplorerViewModel() : base(".Net Explorer")
        {
            this.ContentId = DotNetExplorerViewModel.ToolContentId;
            this.dotNetObjects = new ReadOnlyCollection<DotNetObjectViewModel>(new List<DotNetObjectViewModel>());
            this.RefreshObjectsCommand = new RelayCommand(() => this.RefreshObjects(), () => true);
            this.AddDotNetObjectCommand = new RelayCommand<DotNetObjectViewModel>((dotNetObjectViewModel) => this.AddDotNetObject(dotNetObjectViewModel), (dotNetObjectViewModel) => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a command to refresh the list of active .Net objects based on gathered managed heap data.
        /// </summary>
        public ICommand RefreshObjectsCommand { get; private set; }

        /// <summary>
        /// Gets a command to add a .Net object to the project explorer.
        /// </summary>
        public ICommand AddDotNetObjectCommand { get; private set; }

        /// <summary>
        /// Gets or sets the collection of dot net objects in the target process.
        /// </summary>
        public ReadOnlyCollection<DotNetObjectViewModel> DotNetObjects
        {
            get
            {
                return this.dotNetObjects;
            }

            set
            {
                this.dotNetObjects = value;
                this.RaisePropertyChanged(nameof(this.DotNetObjects));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="DotNetExplorerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static DotNetExplorerViewModel GetInstance()
        {
            return dotNetExplorerViewModelInstance.Value;
        }

        /// <summary>
        /// Refreshes the list of active .Net objects based on gathered managed heap data.
        /// </summary>
        private void RefreshObjects()
        {
            IEnumerable<DotNetObject> dotNetObjects = DotNetObjectCollector.GetInstance().ObjectTrees;

            if (dotNetObjects == null)
            {
                dotNetObjects = new List<DotNetObject>();
            }

            this.DotNetObjects = new ReadOnlyCollection<DotNetObjectViewModel>(dotNetObjects.Select(x => new DotNetObjectViewModel(x)).ToList());
        }

        /// <summary>
        /// Adds a .Net object to the project explorer.
        /// </summary>
        /// <param name="dotNetObjectViewModel">The view model of the .Net object.</param>
        private void AddDotNetObject(DotNetObjectViewModel dotNetObjectViewModel)
        {
            DotNetObject dotNetObject = dotNetObjectViewModel.DotNetObject;
            AddressItem addressItem = new AddressItem();

            addressItem.Description = dotNetObject.Name;
            addressItem.ElementType = dotNetObject.ElementType == typeof(Boolean) ? typeof(Byte) : dotNetObject.ElementType;
            addressItem.BaseIdentifier = dotNetObject.GetFullName();
            addressItem.ResolveType = AddressResolver.ResolveTypeEnum.DotNet;

            ProjectExplorerViewModel.GetInstance().AddNewProjectItems(true, addressItem);
        }
    }
    //// End class
}
//// End namespace