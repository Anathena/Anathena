namespace Ana.Source.Mvvm.Helpers
{
    using System;

    /// <summary>
    /// This interface is meant for the <see cref="WeakAction{T}" /> class and can be useful if you store multiple
    /// WeakAction{T} instances but don't know in advance what type T represents.
    /// </summary>
    internal interface IExecuteWithObject
    {
        /// <summary>
        /// Gets the target of the WeakAction.
        /// </summary>
        Object Target
        {
            get;
        }

        /// <summary>
        /// Executes an action.
        /// </summary>
        /// <param name="parameter">A parameter passed as an object, to be casted to the appropriate type.</param>
        void ExecuteWithObject(Object parameter);

        /// <summary>
        /// Deletes all references, which notifies the cleanup method that this entry must be deleted.
        /// </summary>
        void MarkForDeletion();
    }
    //// End class
}
//// End namespace