using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Capture.Hook
{
    /// <summary>
    /// Provides a safe handle around a block of unmanaged memory.
    /// </summary>
    public class SafeHGlobal : SafeHandle
    {
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// <returns>true if the handle value is invalid; otherwise, false.</returns>
        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeHGlobal"/> class.
        /// </summary>
        /// <param name="sizeInBytes">The size of the block of memory to allocate, in bytes.</param>
        public SafeHGlobal(int sizeInBytes)
            : base(Marshal.AllocHGlobal(sizeInBytes), true)
        {
        }

        /// <summary>
        /// When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns>
        /// true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.
        /// </returns>
        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            return true;
        }
    }
}
