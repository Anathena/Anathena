using System;

namespace Anathema.Source.SystemInternals.OperatingSystems.Windows.Internals
{
    /// <summary>
    /// Defines an IDisposable interface with a known state.
    /// </summary>
    public interface IDisposableState : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the element is disposed.
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// Gets a value indicating whether the element must be disposed when the Garbage Collector collects the object.
        /// </summary>
        bool MustBeDisposed { get; }
    }

} // End namespace