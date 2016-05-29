using Anathema.Source.OS.OperatingSystems.Windows.Native;
using System;

namespace Anathema.Source.OS.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Class providing tools for manipulating memory protection.
    /// </summary>
    public class MemoryProtection : IDisposable
    {
        /// <summary>
        /// The reference of the <see cref="WindowsOSInterface"/> object.
        /// </summary>
        private readonly WindowsOSInterface _WindowsOSInterface;
        /// <summary>
        /// The base address of the altered memory.
        /// </summary>
        public IntPtr BaseAddress { get; private set; }
        /// <summary>
        /// States if the <see cref="MemoryProtection"/> object nust be disposed when it is collected.
        /// </summary>
        public bool MustBeDisposed { get; set; }
        /// <summary>
        /// Defines the new protection applied to the memory.
        /// </summary>
        public MemoryProtectionFlags NewProtection { get; private set; }
        /// <summary>
        /// References the inital protection of the memory.
        /// </summary>
        public MemoryProtectionFlags OldProtection { get; private set; }
        /// <summary>
        /// The size of the altered memory.
        /// </summary>
        public int Size { get; private set; }

        #region Constructor/Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryProtection"/> class.
        /// </summary>
        /// <param name="WindowsOSInterface">The reference of the <see cref="WindowsOSInterface"/> object.</param>
        /// <param name="BaseAddress">The base address of the memory to change the protection.</param>
        /// <param name="Size">The size of the memory to change.</param>
        /// <param name="Protection">The new protection to apply.</param>
        /// <param name="MustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        public MemoryProtection(WindowsOSInterface WindowsOSInterface, IntPtr BaseAddress, Int32 Size, MemoryProtectionFlags Protection = MemoryProtectionFlags.ExecuteReadWrite,
                                Boolean MustBeDisposed = true)
        {
            // Save the parameters
            _WindowsOSInterface = WindowsOSInterface;
            this.BaseAddress = BaseAddress;
            NewProtection = Protection;
            this.Size = Size;
            this.MustBeDisposed = MustBeDisposed;

            // Change the memory protection
            OldProtection = MemoryCore.ChangeProtection(_WindowsOSInterface.Handle, BaseAddress, Size, Protection);
        }

        /// <summary>
        /// Frees resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~MemoryProtection()
        {
            if (MustBeDisposed)
                Dispose();
        }

        #endregion

        #region Methods
        #region Dispose (implementation of IDisposable)
        /// <summary>
        /// Restores the initial protection of the memory.
        /// </summary>
        public virtual void Dispose()
        {
            // Restore the memory protection
            MemoryCore.ChangeProtection(_WindowsOSInterface.Handle, BaseAddress, Size, OldProtection);

            // Avoid the finalizer 
            GC.SuppressFinalize(this);
        }

        #endregion
        #region ToString (override)
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("BaseAddress = 0x{0:X} NewProtection = {1} OldProtection = {2}", BaseAddress.ToInt64(), NewProtection, OldProtection);
        }

        #endregion
        #endregion

    } // End class

} // End namespace