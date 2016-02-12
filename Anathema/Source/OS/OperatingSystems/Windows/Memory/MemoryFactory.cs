using System;
using System.Collections.Generic;
using System.Linq;
using Anathema.MemoryManagement.Internals;
using Anathema.MemoryManagement.Native;

namespace Anathema.MemoryManagement.Memory
{
    /// <summary>
    /// Class providing tools for manipulating memory space.
    /// </summary>
    public class MemoryFactory : IFactory
    {
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly WindowsOSInterface MemorySharp;
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
        public IEnumerable<RemoteVirtualPage> VirtualPages
        {
            get
            {
#if x86
                IntPtr AddressTo = new IntPtr(0x7fffffff);
#else
                IntPtr AddressTo = new IntPtr(0x7fffffffffffffff);
#endif
                return MemoryCore.Query(MemorySharp.Handle, IntPtr.Zero, AddressTo).Select(x => new RemoteVirtualPage(MemorySharp, x.BaseAddress));
            }
        }

        public IEnumerable<RemoteVirtualPage> AllVirtualPages
        {
            get
            {
#if x86
                IntPtr AddressTo = new IntPtr(0x7fffffff);
#else
                IntPtr AddressTo = new IntPtr(0x7fffffffffffffff);
#endif
                return MemoryCore.Query(MemorySharp.Handle, IntPtr.Zero, AddressTo, true).Select(x => new RemoteVirtualPage(MemorySharp, x.BaseAddress));
            }
        }

        #endregion
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryFactory"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        internal MemoryFactory(WindowsOSInterface MemorySharp)
        {
            // Save the parameter
            this.MemorySharp = MemorySharp;

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
            RemoteAllocation Memory = new RemoteAllocation(MemorySharp, Size, Protection, MustBeDisposed);

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
            if(!Allocation.IsDisposed)
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
            foreach (RemoteAllocation AllocatedMemory in InternalRemoteAllocations.Where(x => x.MustBeDisposed).ToArray())
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