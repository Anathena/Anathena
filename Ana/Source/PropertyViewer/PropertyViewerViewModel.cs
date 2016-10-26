namespace Ana.Source.PropertyViewer
{
    using Docking;
    using Main;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Prevents a default instance of the <see cref="PropertyViewerViewModel" /> class from being created
        /// </summary>
        private PropertyViewerViewModel() : base("Property Viewer")
        {
            this.ContentId = PropertyViewerViewModel.ToolContentId;
            this.IsVisible = true;
            this.ObserverLock = new Object();
            this.PropertyViewerObservers = new List<IPropertyViewerObserver>();

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
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
                this.NotifyObservers();
            }
        }

        private Object ObserverLock { get; set; }

        /// <summary>
        /// Gets or sets objects observing changes in the selected objects
        /// </summary>
        private List<IPropertyViewerObserver> PropertyViewerObservers { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="PropertyViewerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static PropertyViewerViewModel GetInstance()
        {
            return PropertyViewerViewModel.propertyViewerViewModelInstance.Value;
        }

        /// <summary>
        /// Subscribes the given object to changes in the selected objects
        /// </summary>
        /// <param name="propertyViewerObserver">The object to observe selected objects changes</param>
        public void Subscribe(IPropertyViewerObserver propertyViewerObserver)
        {
            lock (this.ObserverLock)
            {
                if (!this.PropertyViewerObservers.Contains(propertyViewerObserver))
                {
                    this.PropertyViewerObservers.Add(propertyViewerObserver);
                    propertyViewerObserver.Update(this.TargetObjects);
                }
            }
        }

        /// <summary>
        /// Unsubscribes the given object from changes in the selected objects
        /// </summary>
        /// <param name="propertyViewerObserver">The object to observe selected objects changes</param>
        public void Unsubscribe(IPropertyViewerObserver propertyViewerObserver)
        {
            lock (this.ObserverLock)
            {
                if (this.PropertyViewerObservers.Contains(propertyViewerObserver))
                {
                    this.PropertyViewerObservers.Remove(propertyViewerObserver);
                }
            }
        }

        public void SetTargetObjects(params Object[] targetObjects)
        {
            this.TargetObjects = targetObjects;
        }

        /// <summary>
        /// Notify all observing objects of a change in the selected objects
        /// </summary>
        private void NotifyObservers()
        {
            lock (this.ObserverLock)
            {
                foreach (IPropertyViewerObserver observer in this.PropertyViewerObservers)
                {
                    observer.Update(this.TargetObjects);
                }
            }
        }
    }
    //// End class
}
//// End namespace