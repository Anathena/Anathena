using Anathema.Source.SystemInternals.OperatingSystems.Windows.Internals;
using Anathema.Source.SystemInternals.OperatingSystems.Windows.Native;
using Anathema.Source.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathema.Source.SystemInternals.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Class providing tools for manipulating memory space.
    /// </summary>
    public class MemoryFactory : IFactory
    {
        /// <summary>
        /// The reference of the <see cref="WindowsOperatingSystem"/> object.
        /// </summary>
        protected readonly WindowsOperatingSystem WindowsOperatingSystem;
        /// <summary>
        /// The list containing all allocated memory.
        /// </summary>
        protected readonly List<RemoteAllocation> InternalRemoteAllocations;

        #region Properties
        #region RemoteAllocations
        /// <summary>
        /// A collection containing all allocated memory in the remote process.
        /// </summary>
        public IEnumerable<RemoteAllocation> RemoteAllocations
        {
            get { return InternalRemoteAllocations.AsReadOnly(); }
        }

        #endregion
        #region Regions
        /// <summary>
        /// Gets all blocks of memory allocated in the remote process.
        /// </summary>
        public IEnumerable<RemoteVirtualPage> VirtualPages(IntPtr StartAddress, IntPtr EndAddress,
            MemoryProtectionFlags RequiredProtection, MemoryProtectionFlags ExcludedProtection, MemoryTypeEnum AllowedTypes)
        {
            return MemoryCore.Query(WindowsOperatingSystem.Handle, StartAddress, EndAddress, RequiredProtection, ExcludedProtection, AllowedTypes).Select(x => new RemoteVirtualPage(WindowsOperatingSystem, x.BaseAddress));
        }

        public IEnumerable<RemoteVirtualPage> AllVirtualPages()
        {
            return MemoryCore.Query(WindowsOperatingSystem.Handle, IntPtr.Zero, IntPtr.Zero.MaxValue(), 0, 0,
                MemoryTypeEnum.None | MemoryTypeEnum.Private | MemoryTypeEnum.Image | MemoryTypeEnum.Mapped).Select(x => new RemoteVirtualPage(WindowsOperatingSystem, x.BaseAddress));
        }

        #endregion
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryFactory"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="SystemInternals.OperatingSystems.WindowsOSInterface"/> object.</param>
        internal MemoryFactory(WindowsOperatingSystem WindowsOperatingSystem)
        {
            // Save the parameter
            this.WindowsOperatingSystem = WindowsOperatingSystem;

            // Create a list containing all allocated memory
            InternalRemoteAllocations = new List<RemoteAllocation>();
        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~MemoryFactory()
        {
            Dispose();
        }

        #endregion

        #region Methods
        #region Allocate
        /// <summary>
        /// Allocates a region of memory within the virtual address space of the remote process.
        /// </summary>
        /// <param name="Size">The size of the memory to allocate.</param>
        /// <param name="Protection">The protection of the memory to allocate.</param>
        /// <param name="MustBeDisposed">The allocated memory will be released when the finalizer collects the object.</param>
        /// <returns>A new instance of the <see cref="RemoteAllocation"/> class.</returns>
        public RemoteAllocation Allocate(Int32 Size, MemoryProtectionFlags Protection = MemoryProtectionFlags.ExecuteReadWrite, Boolean MustBeDisposed = true)
        {
            // Allocate a memory space
            RemoteAllocation Memory = new RemoteAllocation(WindowsOperatingSystem, Size, Protection, MustBeDisposed);

            // Add the memory in the list
            InternalRemoteAllocations.Add(Memory);
            return Memory;
        }

        #endregion
        #region Deallocate
        /// <summary>
        /// Deallocates a region of memory previously allocated within the virtual address space of the remote process.
        /// </summary>
        /// <param name="Allocation">The allocated memory to release.</param>
        public void Deallocate(RemoteAllocation Allocation)
        {
            // Dispose the element
            if (!Allocation.IsDisposed)
                Allocation.Dispose();

            // Remove the element from the allocated memory list
            if (InternalRemoteAllocations.Contains(Allocation))
                InternalRemoteAllocations.Remove(Allocation);
        }

        #endregion
        #region Dispose (implementation of IFactory)
        /// <summary>
        /// Releases all resources used by the <see cref="MemoryFactory"/> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Release all allocated memories which must be disposed
            foreach (RemoteAllocation AllocatedMemory in InternalRemoteAllocations.Where(X => X.MustBeDisposed).ToArray())
            {
                AllocatedMemory.Dispose();
            }
            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        #endregion
        #endregion

    } // End class

} // End namespace