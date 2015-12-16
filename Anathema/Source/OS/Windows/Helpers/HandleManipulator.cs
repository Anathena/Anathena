/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Helpers
{
    /// <summary>
    /// Static helper class providing tools for manipulating handles.
    /// </summary>
    public static class HandleManipulator
    {
        #region CloseHandle
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr handle)
        {
            // Check if the handle is valid
            ValidateAsArgument(handle, "handle");

            // Close the handle
            if(!NativeMethods.CloseHandle(handle))
                throw new Win32Exception("Couldn't close the handle correctly.");
        }
        #endregion

        #region HandleToProcess
        /// <summary>
        /// Converts an handle into a <see cref="Process"/> object assuming this is a process handle.
        /// </summary>
        /// <param name="processHandle">A valid handle to an opened process.</param>
        /// <returns>A <see cref="Process"/> object from the specified handle.</returns>
        public static Process HandleToProcess(SafeMemoryHandle processHandle)
        {
            // Search the process by iterating the processes list
            return Process.GetProcesses().First(p => p.Id == HandleToProcessId(processHandle));
        }
        #endregion

        #region HandleToProcessId
        /// <summary>
        /// Converts an handle into a process id assuming this is a process handle.
        /// </summary>
        /// <param name="processHandle">A valid handle to an opened process.</param>
        /// <returns>A process id from the specified handle.</returns>
        public static int HandleToProcessId(SafeMemoryHandle processHandle)
        {
            // Check if the handle is valid
            ValidateAsArgument(processHandle, "processHandle");

            // Find the process id
            var ret = NativeMethods.GetProcessId(processHandle);

            // If the process id is valid
            if (ret != 0)
                return ret;

            // Else the function failed, throws an exception
            throw new Win32Exception("Couldn't find the process id of the specified handle.");
        }
        #endregion

        #region HandleToThread
        /// <summary>
        /// Converts an handle into a <see cref="ProcessThread"/> object assuming this is a thread handle.
        /// </summary>
        /// <param name="threadHandle">A valid handle to an opened thread.</param>
        /// <returns>A <see cref="ProcessThread"/> object from the specified handle.</returns>
        public static ProcessThread HandleToThread(SafeMemoryHandle threadHandle)
        {
            // Search the thread by iterating the processes list
            foreach (var process in Process.GetProcesses())
            {
                var ret = process.Threads.Cast<ProcessThread>().FirstOrDefault(t => t.Id == HandleToThreadId(threadHandle));
                if (ret != null)
                    return ret;
            }

            // If no thread was found, throws a exception like the First() function with no element
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        #endregion

        #region HandleToThreadId
        /// <summary>
        /// Converts an handle into a thread id assuming this is a thread handle.
        /// </summary>
        /// <param name="threadHandle">A valid handle to an opened thread.</param>
        /// <returns>A thread id from the specified handle.</returns>
        public static int HandleToThreadId(SafeMemoryHandle threadHandle)
        {
            // Check if the handle is valid
            ValidateAsArgument(threadHandle, "threadHandle");

            // Find the thread id
            var ret = NativeMethods.GetThreadId(threadHandle);

            // If the thread id is valid
            if (ret != 0)
                return ret;

            //Else the function failed, throws an exception
            throw new Win32Exception("Couldn't find the thread id of the specified handle.");
        }
        #endregion

        #region ValidateAsArgument
        /// <summary>
        /// Validates an handle to fit correctly as argument.
        /// </summary>
        /// <param name="handle">A handle to validate.</param>
        /// <param name="argumentName">The name of the argument that represents the handle in its original function.</param>
        public static void ValidateAsArgument(IntPtr handle, string argumentName)
        {
            // Check if the handle is not null
            if(handle == null)
                throw new ArgumentNullException(argumentName);

            // Check if the handle is valid
            if(handle == IntPtr.Zero)
                throw new ArgumentException("The handle is not valid.", argumentName);
        }

        /// <summary>
        /// Validates an handle to fit correctly as argument.
        /// </summary>
        /// <param name="handle">A handle to validate.</param>
        /// <param name="argumentName">The name of the argument that represents the handle in its original function.</param>
        public static void ValidateAsArgument(SafeMemoryHandle handle, string argumentName)
        {
            // Check if the handle is not null
            if (handle == null)
                throw new ArgumentNullException(argumentName);

            // Check if the handle is valid
            if(handle.IsClosed || handle.IsInvalid)
                throw new ArgumentException("The handle is not valid or closed.", argumentName);
        }
        #endregion
    }
}
