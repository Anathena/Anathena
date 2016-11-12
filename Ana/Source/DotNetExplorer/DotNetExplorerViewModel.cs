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
    /// View model for the .Net Explorer
    /// </summary>
    internal class DotNetExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(DotNetExplorerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="DotNetExplorerViewModel" /> class
        /// </summary>
        private static Lazy<DotNetExplorerViewModel> dotNetExplorerViewModelInstance = new Lazy<DotNetExplorerViewModel>(
                () => { return new DotNetExplorerViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The collection of dot net objects in the target process
        /// </summary>
        private ReadOnlyCollection<DotNetObjectViewModel> dotNetObjects;

        /// <summary>
        /// Prevents a default instance of the <see cref="DotNetExplorerViewModel" /> class from being created
        /// </summary>
        private DotNetExplorerViewModel() : base(".Net Explorer")
        {
            this.ContentId = DotNetExplorerViewModel.ToolContentId;
            this.dotNetObjects = new ReadOnlyCollection<DotNetObjectViewModel>(new List<DotNetObjectViewModel>());
            this.RefreshObjectsCommand = new RelayCommand(() => RefreshObjects(), () => true);
            this.AddDotNetObjectCommand = new RelayCommand<DotNetObjectViewModel>((dotNetObjectViewModel) => AddDotNetObject(dotNetObjectViewModel), (dotNetObjectViewModel) => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand RefreshObjectsCommand { get; private set; }

        public ICommand AddDotNetObjectCommand { get; private set; }

        /// <summary>
        /// Gets or sets the collection of dot net objects in the target process
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
        /// Gets a singleton instance of the <see cref="DotNetExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static DotNetExplorerViewModel GetInstance()
        {
            return dotNetExplorerViewModelInstance.Value;
        }

        private void RefreshObjects()
        {
            IEnumerable<DotNetObject> dotNetObjects = DotNetObjectCollector.GetInstance().GetObjectTrees();

            if (dotNetObjects == null)
            {
                dotNetObjects = new List<DotNetObject>();
            }

            this.DotNetObjects = new ReadOnlyCollection<DotNetObjectViewModel>(dotNetObjects.Select(x => new DotNetObjectViewModel(x)).ToList());
        }

        private void AddDotNetObject(DotNetObjectViewModel dotNetObjectViewModel)
        {
            DotNetObject dotNetObject = dotNetObjectViewModel.DotNetObject;
            AddressItem addressItem = new AddressItem();

            addressItem.Description = dotNetObject.Name;
            addressItem.BaseIdentifier = dotNetObject.GetFullName();
            addressItem.ResolveType = AddressResolver.ResolveTypeEnum.DotNet;

            ProjectExplorerViewModel.GetInstance().AddNewProjectItem(addressItem);
        }
    }
    //// End class
}
//// End namespace