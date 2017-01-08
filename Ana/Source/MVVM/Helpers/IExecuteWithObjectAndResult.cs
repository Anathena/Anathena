namespace Ana.Source.Mvvm.Helpers
{
    using System;

    /// <summary>
    /// This interface is meant for the <see cref="WeakFunc{T}" /> class and can be useful if you store multiple
    /// WeakFunc{T} instances but don't know in advance what type T represents.
    /// </summary>
    ////[ClassInfo(typeof(WeakAction))]
    internal interface IExecuteWithObjectAndResult
    {
        /// <summary>
        /// Executes a Func and returns the result.
        /// </summary>
        /// <param name="parameter">A parameter passed as an object, to be casted to the appropriate type.</param>
        /// <returns>The result of the operation.</returns>
        Object ExecuteWithObject(Object parameter);
    }
    //// End class
}
//// End namespace