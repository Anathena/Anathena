namespace Ana.Source.PropertyViewer
{
    using System;

    /// <summary>
    /// Interface for a class which listens for changes in the selected properties
    /// </summary>
    internal interface IPropertyViewerObserver
    {
        /// <summary>
        /// Recieves an update of the selected objects
        /// </summary>
        /// <param name="targetObjects">The target objects being viewed</param>
        void Update(Object[] targetObjects);
    }
    //// End interface
}
//// End namespace