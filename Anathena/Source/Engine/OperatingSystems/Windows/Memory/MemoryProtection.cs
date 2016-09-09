using Anathena.Source.Engine.OperatingSystems.Windows.Native;
using System;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Class providing tools for manipulating memory protection.
    /// </summary>
    public class MemoryProtection
    {
        /// <summary>
        /// The reference of the <see cref="Windows.WindowsOperatingSystem"/> object.
        /// </summary>
        private readonly WindowsOperatingSystem WindowsOperatingSystem;

        /// <summary>
        /// The base address of the altered memory.
        /// </summary>
        public IntPtr BaseAddress { get; private set; }

        /// <summary>
        /// States if the <see cref="MemoryProtection"/> object nust be disposed when it is collected.
        /// </summary>
        public Boolean MustBeDisposed { get; set; }

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
        public Int32 Size { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryProtection"/> class.
        /// </summary>
        /// <param name="WindowsOperatingSystem">The reference of the <see cref="Windows.WindowsOperatingSystem"/> object.</param>
        /// <param name="BaseAddress">The base address of the memory to change the protection.</param>
        /// <param name="Size">The size of the memory to change.</param>
        /// <param name="Protection">The new protection to apply.</param>
        /// <param name="MustBeDisposed">The resource will be automatically disposed when the finalizer collects the object.</param>
        public MemoryProtection(WindowsOperatingSystem WindowsOperatingSystem, IntPtr BaseAddress, Int32 Size, MemoryProtectionFlags Protection = MemoryProtectionFlags.ExecuteReadWrite,
                                Boolean MustBeDisposed = true)
        {
            // Save the parameters
            this.WindowsOperatingSystem = WindowsOperatingSystem;
            this.BaseAddress = BaseAddress;
            NewProtection = Protection;
            this.Size = Size;
            this.MustBeDisposed = MustBeDisposed;

            // Change the memory protection
            OldProtection = MemoryCore.ChangeProtection(this.WindowsOperatingSystem.Handle, BaseAddress, Size, Protection);
        }

    } // End class

} // End namespace