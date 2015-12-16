/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Binarysharp.MemoryManagement.Native
{
    /// <summary>
    /// Represents a Win32 handle safely managed.
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public sealed class SafeMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Parameterless constructor for handles built by the system (like <see cref="NativeMethods.OpenProcess"/>).
        /// </summary>
        public SafeMemoryHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeMemoryHandle"/> class, specifying the handle to keep in safe.
        /// </summary>
        /// <param name="handle">The handle to keep in safe.</param>
        public SafeMemoryHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        /// <summary>
        /// Executes the code required to free the handle.
        /// </summary>
        /// <returns>True if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            // Check whether the handle is set AND whether the handle has been successfully closed
            return handle != IntPtr.Zero && NativeMethods.CloseHandle(handle);
        }
    }
}
