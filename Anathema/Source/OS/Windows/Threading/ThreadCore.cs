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
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Threading
{
    /// <summary>
    /// Static core class providing tools for manipulating threads.
    /// </summary>
    public static class ThreadCore
    {
        #region CreateRemoteThread
        /// <summary>
        /// Creates a thread that runs in the virtual address space of another process.
        /// </summary>
        /// <param name="processHandle">A handle to the process in which the thread is to be created.</param>
        /// <param name="startAddress">A pointer to the application-defined function to be executed by the thread and represents the starting address of the thread in the remote process.</param>
        /// <param name="parameter">A pointer to a variable to be passed to the thread function.</param>
        /// <param name="creationFlags">The flags that control the creation of the thread.</param>
        /// <returns>A handle to the new thread.</returns>
        public static SafeMemoryHandle CreateRemoteThread(SafeMemoryHandle processHandle, IntPtr startAddress, IntPtr parameter, ThreadCreationFlags creationFlags = ThreadCreationFlags.Run)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(processHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(startAddress, "startAddress");

            // Create the remote thread
            int threadId;
            var ret = NativeMethods.CreateRemoteThread(processHandle, IntPtr.Zero, 0, startAddress, parameter, creationFlags, out threadId);

            // If the thread is created
            if (!ret.IsClosed && !ret.IsInvalid)
                return ret;

            // Else couldn't create thread, throws an exception
            throw new Win32Exception(string.Format("Couldn't create the thread at 0x{0}.", startAddress.ToString("X")));
        }
        #endregion

        #region GetExitCodeThread
        /// <summary>
        /// Retrieves the termination status of the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread.</param>
        /// <returns>Nullable type: the return value is A pointer to a variable to receive the thread termination status or <code>null</code> if it is running.</returns>
        public static IntPtr? GetExitCodeThread(SafeMemoryHandle threadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Create the variable storing the output exit code
            IntPtr exitCode;

            // Get the exit code of the thread
            if (!NativeMethods.GetExitCodeThread(threadHandle, out exitCode))
                throw new Win32Exception("Couldn't get the exit code of the thread.");

            // If the thread is still active
            if (exitCode == new IntPtr(259))
                return null;

            return exitCode;
        }
        #endregion

        #region GetThreadContext
        /// <summary>
        /// Retrieves the context of the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread whose context is to be retrieved.</param>
        /// <param name="contextFlags">Determines which registers are returned or set.</param>
        /// <returns>A <see cref="ThreadContext"/> structure that receives the appropriate context of the specified thread.</returns>
        public static ThreadContext GetThreadContext(SafeMemoryHandle threadHandle, ThreadContextFlags contextFlags = ThreadContextFlags.Full)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Allocate a thread context structure
            var context = new ThreadContext {ContextFlags = contextFlags};
            
            // Set the context flag

            // Get the thread context
            if (NativeMethods.GetThreadContext(threadHandle, ref context))
                return context;

            // Else couldn't get the thread context, throws an exception
            throw new Win32Exception("Couldn't get the thread context.");
        }
        #endregion

        #region GetThreadSelectorEntry
        /// <summary>
        /// Retrieves a descriptor table entry for the specified selector and thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread containing the specified selector.</param>
        /// <param name="selector">The global or local selector value to look up in the thread's descriptor tables.</param>
        /// <returns>A pointer to an <see cref="LdtEntry"/> structure that receives a copy of the descriptor table entry.</returns>
        public static LdtEntry GetThreadSelectorEntry(SafeMemoryHandle threadHandle, uint selector)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Get the selector entry
            LdtEntry entry;
            if (NativeMethods.GetThreadSelectorEntry(threadHandle, selector, out entry))
                return entry;

            // Else couldn't get the selector entry, throws an exception
            throw new Win32Exception(string.Format("Couldn't get the selector entry for this selector: {0}.", selector));
        }
        #endregion

        #region OpenThread
        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="accessFlags">The access to the thread object.</param>
        /// <param name="threadId">The identifier of the thread to be opened.</param>
        /// <returns>An open handle to the specified thread.</returns>
        public static SafeMemoryHandle OpenThread(ThreadAccessFlags accessFlags, int threadId)
        {
            // Open the thread
            var ret = NativeMethods.OpenThread(accessFlags, false, threadId);

            // If the thread was opened
            if (!ret.IsClosed && !ret.IsInvalid)
                return ret;

            // Else couldn't open the thread, throws an exception
            throw new Win32Exception(string.Format("Couldn't open the thread #{0}.", threadId));
        }
        #endregion

        #region NtQueryInformationThread
        /// <summary>
        /// Retrieves information about the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread to query.</param>
        /// <returns>A <see cref="ThreadBasicInformation"/> structure containg thread information.</returns>
        public static ThreadBasicInformation NtQueryInformationThread(SafeMemoryHandle threadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Create a structure to store thread info
            var info = new ThreadBasicInformation();

            // Get the thread info
            var ret = NativeMethods.NtQueryInformationThread(threadHandle, 0, ref info, MarshalType<ThreadBasicInformation>.Size, IntPtr.Zero);

            // If the function succeeded
            if (ret == 0)
                return info;

            // Else, couldn't get the thread info, throws an exception
            throw new ApplicationException(string.Format("Couldn't get the information from the thread, error code '{0}'.", ret));
        }
        #endregion

        #region ResumeThread
        /// <summary>
        /// Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread to be restarted.</param>
        /// <returns>The thread's previous suspend count.</returns>
        public static uint ResumeThread(SafeMemoryHandle threadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Resume the thread
            var ret = NativeMethods.ResumeThread(threadHandle);

            // If the function failed
            if (ret == uint.MaxValue)
                throw new Win32Exception("Couldn't resume the thread.");

            return ret;
        }
        #endregion

        #region SetThreadContext
        /// <summary>
        /// Sets the context for the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread whose context is to be set.</param>
        /// <param name="context">A pointer to a <see cref="ThreadContext"/> structure that contains the context to be set in the specified thread.</param>
        public static void SetThreadContext(SafeMemoryHandle threadHandle, ThreadContext context)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");
            
            // Set the thread context
            if(!NativeMethods.SetThreadContext(threadHandle, ref context))
                throw new Win32Exception("Couldn't set the thread context.");
        }
        #endregion

        #region SuspendThread
        /// <summary>
        /// Suspends the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread that is to be suspended.</param>
        /// <returns>The thread's previous suspend count.</returns>
        public static uint SuspendThread(SafeMemoryHandle threadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Suspend the thread
            var ret = NativeMethods.SuspendThread(threadHandle);

            // If the function failed
            if (ret == uint.MaxValue)
                throw new Win32Exception("Couldn't suspend the thread.");

            return ret;
        }
        #endregion

        #region TerminateThread
        /// <summary>
        /// Terminates a thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread to be terminated.</param>
        /// <param name="exitCode">The exit code for the thread.</param>
        public static void TerminateThread(SafeMemoryHandle threadHandle, int exitCode)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Terminate the thread
            var ret = NativeMethods.TerminateThread(threadHandle, exitCode);

            // If the function failed
            if(!ret)
                throw new Win32Exception("Couldn't terminate the thread.");
        }
        #endregion

        #region WaitForSingleObject
        /// <summary>
        /// Waits until the specified object is in the signaled state or the time-out interval elapses.
        /// </summary>
        /// <param name="handle">A handle to the object.</param>
        /// <param name="timeout">The time-out interval. If this parameter is NULL, the function does not enter a wait state if the object is not signaled; it always returns immediately.</param>
        /// <returns>Indicates the <see cref="WaitValues"/> event that caused the function to return.</returns>
        public static WaitValues WaitForSingleObject(SafeMemoryHandle handle, TimeSpan? timeout)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(handle, "handle");

            // Wait for single object
            var ret = NativeMethods.WaitForSingleObject(handle, timeout.HasValue ? Convert.ToUInt32(timeout.Value.TotalMilliseconds) : 0);

            // If the function failed
            if (ret == WaitValues.Failed)
                throw new Win32Exception("The WaitForSingleObject function call failed.");

            return ret;
        }

        /// <summary>
        /// Waits an infinite amount of time for the specified object to become signaled.
        /// </summary>
        /// <param name="handle">A handle to the object.</param>
        /// <returns>If the function succeeds, the return value indicates the event that caused the function to return.</returns>
        public static WaitValues WaitForSingleObject(SafeMemoryHandle handle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(handle, "handle");

            // Wait for single object
            var ret = NativeMethods.WaitForSingleObject(handle, 0xFFFFFFFF);

            // If the function failed
            if (ret == WaitValues.Failed)
                throw new Win32Exception("The WaitForSingleObject function call failed.");

            return ret;
        }
        #endregion
    }
}
