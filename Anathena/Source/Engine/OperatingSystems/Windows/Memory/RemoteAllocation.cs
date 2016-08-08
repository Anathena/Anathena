using Anathena.Source.Engine.OperatingSystems.Windows.Internals;
using Anathena.Source.Engine.OperatingSystems.Windows.Native;
using System;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Class representing an allocated memory in a remote process.
    /// </summary>
    public class RemoteAllocation : RemoteVirtualPage, IDisposableState
    {
        /// <summary>
        /// Gets a value indicating whether the element is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// Gets a value indicating whether the element must be disposed when the Garbage Collector collects the object.
        /// </summary>
        public bool MustBeDisposed { get; set; }

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteAllocation"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="WindowsOperatingSystem"/> object.</param>
        /// <param name="Size">The size of the allocated memory.</param>
        /// <param name="Protection">The protection of the allocated memory.</param>
        /// <param name="MustBeDisposed">The allocated memory will be released when the finalizer collects the object.</param>
        internal RemoteAllocation(WindowsOperatingSystem WindowsOperatingSystem, Int32 Size, MemoryProtectionFlags Protection = MemoryProtectionFlags.ExecuteReadWrite,
                                 Boolean MustBeDisposed = true)
            : base(WindowsOperatingSystem, MemoryCore.Allocate(WindowsOperatingSystem.Handle, Size, Protection))
        {
            // Set local vars
            this.MustBeDisposed = MustBeDisposed;
            IsDisposed = false;
        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~RemoteAllocation()
        {
            if (MustBeDisposed)
                Dispose();
        }

        #endregion

        #region Methods
        #region Dispose (implementation of IDisposableState)
        /// <summary>
        /// Releases all resources used by the <see cref="RemoteAllocation"/> object.
        /// </summary>
        /// <remarks>Don't use the IDisposable pattern because the class is sealed.</remarks>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                // Set the flag to true
                IsDisposed = true;

                // Release the allocated memory
                Release();

                // Remove this object from the collection of allocated memory
                WindowsOperatingSystem.Memory.Deallocate(this);

                // Remove the pointer
                BaseAddress = IntPtr.Zero;

                // Avoid the finalizer 
                GC.SuppressFinalize(this);
            }
        }
        #endregion
        #endregion

    } // End class

} // End namespace