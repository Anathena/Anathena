using System;
using System.Runtime.InteropServices;

namespace Anathema.Source.OS.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Class representing a block of memory allocated in the local process.
    /// </summary>
    public class LocalUnmanagedMemory : IDisposable
    {
        #region Properties
        /// <summary>
        /// The address where the data is allocated.
        /// </summary>
        public IntPtr Address { get; private set; }
        /// <summary>
        /// The size of the allocated memory.
        /// </summary>
        public Int32 Size { get; private set; }
        #endregion

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalUnmanagedMemory"/> class, allocating a block of memory in the local process.
        /// </summary>
        /// <param name="Size">The size to allocate.</param>
        public LocalUnmanagedMemory(Int32 Size)
        {
            // Allocate the memory
            this.Size = Size;
            Address = Marshal.AllocHGlobal(this.Size);
        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~LocalUnmanagedMemory()
        {
            Dispose();
        }

        #endregion

        #region Methods
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Releases the memory held by the <see cref="LocalUnmanagedMemory"/> object.
        /// </summary>
        public virtual void Dispose()
        {
            // Free the allocated memory
            Marshal.FreeHGlobal(Address);
            // Remove the pointer
            Address = IntPtr.Zero;
            // Avoid the finalizer
            GC.SuppressFinalize(this);
        }

        #endregion
        #region Read
        /// <summary>
        /// Reads data from the unmanaged block of memory.
        /// </summary>
        /// <typeparam name="T">The type of data to return.</typeparam>
        /// <returns>The return value is the block of memory casted in the specified type.</returns>
        public T Read<T>()
        {
            // Marshal data from the block of memory to a new allocated managed object
            return (T)Marshal.PtrToStructure(Address, typeof(T));
        }

        /// <summary>
        /// Reads an array of bytes from the unmanaged block of memory.
        /// </summary>
        /// <returns>The return value is the block of memory.</returns>
        public Byte[] Read()
        {
            // Allocate an array to store data
            Byte[] Bytes = new Byte[Size];

            // Copy the block of memory to the array
            Marshal.Copy(Address, Bytes, 0, Size);

            // Return the array
            return Bytes;
        }

        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Size = {0:X}", Size);
        }

        #endregion
        #region Write
        /// <summary>
        /// Writes an array of bytes to the unmanaged block of memory.
        /// </summary>
        /// <param name="ByteArray">The array of bytes to write.</param>
        public void Write(Byte[] ByteArray)
        {
            // Copy the array of bytes into the block of memory
            Marshal.Copy(ByteArray, 0, Address, Size);
        }

        /// <summary>
        /// Write data to the unmanaged block of memory.
        /// </summary>
        /// <typeparam name="T">The type of data to write.</typeparam>
        /// <param name="Data">The data to write.</param>
        public void Write<T>(T Data)
        {
            // Marshal data from the managed object to the block of memory
            Marshal.StructureToPtr(Data, Address, false);
        }

        #endregion
        #endregion

    } // End class

} // End namespace