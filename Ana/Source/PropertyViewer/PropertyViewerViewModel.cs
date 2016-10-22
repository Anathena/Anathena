namespace Ana.Source.PropertyViewer
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Property Viewer
    /// </summary>
    internal class PropertyViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(PropertyViewerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="PropertyViewerViewModel" /> class
        /// </summary>
        private static Lazy<PropertyViewerViewModel> propertyViewerViewModelInstance = new Lazy<PropertyViewerViewModel>(
                () => { return new PropertyViewerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);


        private Object[] targetObjects;

        private Object targetObject;

        /// <summary>
        /// Prevents a default instance of the <see cref="PropertyViewerViewModel" /> class from being created
        /// </summary>
        private PropertyViewerViewModel() : base("Property Viewer")
        {
            this.ContentId = ToolContentId;
            this.IsVisible = true;

            MainViewModel.GetInstance().Subscribe(this);
        }

        public Object[] TargetObjects
        {
            get
            {
                return this.targetObjects;
            }

            private set
            {
                this.targetObjects = value;
                this.RaisePropertyChanged(nameof(this.TargetObjects));
            }
        }

        public Object TargetObject
        {
            get
            {
                return this.targetObject;
            }

            private set
            {
                this.targetObject = value;
                this.RaisePropertyChanged(nameof(this.TargetObject));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="PropertyViewerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static PropertyViewerViewModel GetInstance()
        {
            return propertyViewerViewModelInstance.Value;
        }

        public void SetTargetObjects(params Object[] targetObjects)
        {
            this.TargetObjects = targetObjects;
        }

        public void SetTargetObject(Object targetObject)
        {
            this.TargetObject = targetObject;
        }
    }
    //// End class
}
//// End namespace