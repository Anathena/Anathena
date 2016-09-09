using System;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Memory
{
    /// <summary>
    /// Represents a virtual page in the remote process.
    /// </summary>
    public class RemoteVirtualPage
    {
        /// <summary>
        /// Base address of the virtual page
        /// </summary>
        public IntPtr BaseAddress { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteVirtualPage"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="WindowsOperatingSystem"/> object.</param>
        /// <param name="baseAddress">The base address of the virtual page.</param>
        internal RemoteVirtualPage(IntPtr BaseAddress)
        {
            this.BaseAddress = BaseAddress;
        }

    } // End class

} // End namespace