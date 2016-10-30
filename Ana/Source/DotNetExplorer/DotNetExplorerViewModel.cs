namespace Ana.Source.DotNetExplorer
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

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
        /// Prevents a default instance of the <see cref="DotNetExplorerViewModel" /> class from being created
        /// </summary>
        private DotNetExplorerViewModel() : base(".Net Explorer")
        {
            this.ContentId = DotNetExplorerViewModel.ToolContentId;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="DotNetExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static DotNetExplorerViewModel GetInstance()
        {
            return dotNetExplorerViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace