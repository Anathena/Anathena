namespace Ana.Source.Results.ScanResults
{
    using System;

    /// <summary>
    /// Interface for a class which listens for changes in the active data type
    /// </summary>
    internal interface IResultsObserver
    {
        /// <summary>
        /// Recieves an update of the active type in the results
        /// </summary>
        /// <param name="activeType">The active data type</param>
        void Update(Type activeType);
    }
    //// End interface
}
//// End namespace