using System;
using System.ComponentModel;
using Anathema.MemoryManagement.Helpers;
using Anathema.MemoryManagement.Internals;
using Anathema.MemoryManagement.Native;

namespace Anathema.MemoryManagement.Threading
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
        /// <param name="ProcessHandle">A handle to the process in which the thread is to be created.</param>
        /// <param name="StartAddress">A pointer to the application-defined function to be executed by the thread and represents the starting address of the thread in the remote process.</param>
        /// <param name="Parameter">A pointer to a variable to be passed to the thread function.</param>
        /// <param name="CreationFlags">The flags that control the creation of the thread.</param>
        /// <returns>A handle to the new thread.</returns>
        public static SafeMemoryHandle CreateRemoteThread(SafeMemoryHandle ProcessHandle, IntPtr StartAddress, IntPtr Parameter, ThreadCreationFlags CreationFlags = ThreadCreationFlags.Run)
        {
            // Check if the handles are valid
            HandleManipulator.ValidateAsArgument(ProcessHandle, "processHandle");
            HandleManipulator.ValidateAsArgument(StartAddress, "startAddress");

            // Create the remote thread
            Int32 ThreadId;
            SafeMemoryHandle MemoryHandle = NativeMethods.CreateRemoteThread(ProcessHandle, IntPtr.Zero, 0, StartAddress, Parameter, CreationFlags, out ThreadId);

            // If the thread is created
            if (!MemoryHandle.IsClosed && !MemoryHandle.IsInvalid)
                return MemoryHandle;

            // Else couldn't create thread, throws an exception
            throw new Win32Exception(string.Format("Couldn't create the thread at 0x{0}.", StartAddress.ToString("X")));
        }

        #endregion

        #region GetExitCodeThread
        /// <summary>
        /// Retrieves the termination status of the specified thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread.</param>
        /// <returns>Nullable type: the return value is A pointer to a variable to receive the thread termination status or <code>null</code> if it is running.</returns>
        public static IntPtr? GetExitCodeThread(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Create the variable storing the output exit code
            IntPtr ExitCode;

            // Get the exit code of the thread
            if (!NativeMethods.GetExitCodeThread(ThreadHandle, out ExitCode))
                throw new Win32Exception("Couldn't get the exit code of the thread.");

            // If the thread is still active
            if (ExitCode == new IntPtr(259))
                return null;

            return ExitCode;
        }

        #endregion

        #region GetThreadContext
        /// <summary>
        /// Retrieves the context of the specified thread.
        /// </summary>
        /// <param name="threadHandle">A handle to the thread whose context is to be retrieved.</param>
        /// <param name="ContextFlags ">Determines which registers are returned or set.</param>
        /// <returns>A <see cref="ThreadContext"/> structure that receives the appropriate context of the specified thread.</returns>
        public static ThreadContext GetThreadContext(SafeMemoryHandle threadHandle, ThreadContextFlags ContextFlags  = ThreadContextFlags.Full)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(threadHandle, "threadHandle");

            // Allocate a thread context structure
            ThreadContext Context = new ThreadContext {ContextFlags = ContextFlags };
            
            // Set the context flag

            // Get the thread context
            if (NativeMethods.GetThreadContext(threadHandle, ref Context))
                return Context;

            // Else couldn't get the thread context, throws an exception
            throw new Win32Exception("Couldn't get the thread context.");
        }
        #endregion

        #region GetThreadSelectorEntry
        /// <summary>
        /// Retrieves a descriptor table entry for the specified selector and thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread containing the specified selector.</param>
        /// <param name="Selector">The global or local selector value to look up in the thread's descriptor tables.</param>
        /// <returns>A pointer to an <see cref="LdtEntry"/> structure that receives a copy of the descriptor table entry.</returns>
        public static LdtEntry GetThreadSelectorEntry(SafeMemoryHandle ThreadHandle, UInt32 Selector)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Get the selector entry
            LdtEntry Entry;

            if (NativeMethods.GetThreadSelectorEntry(ThreadHandle, Selector, out Entry))
                return Entry;

            // Else couldn't get the selector entry, throws an exception
            throw new Win32Exception(String.Format("Couldn't get the selector entry for this selector: {0}.", Selector));
        }

        #endregion

        #region OpenThread
        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="AccessFlags">The access to the thread object.</param>
        /// <param name="ThreadId">The identifier of the thread to be opened.</param>
        /// <returns>An open handle to the specified thread.</returns>
        public static SafeMemoryHandle OpenThread(ThreadAccessFlags AccessFlags, Int32 ThreadId)
        {
            // Open the thread
            SafeMemoryHandle MemoryHandle = NativeMethods.OpenThread(AccessFlags, false, ThreadId);

            // If the thread was opened
            if (!MemoryHandle.IsClosed && !MemoryHandle.IsInvalid)
                return MemoryHandle;

            // Else couldn't open the thread, throws an exception
            throw new Win32Exception(String.Format("Couldn't open the thread #{0}.", ThreadId));
        }

        #endregion

        #region NtQueryInformationThread
        /// <summary>
        /// Retrieves information about the specified thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread to query.</param>
        /// <returns>A <see cref="ThreadBasicInformation"/> structure containg thread information.</returns>
        public static ThreadBasicInformation NtQueryInformationThread(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Create a structure to store thread info
            ThreadBasicInformation ThreadInfo = new ThreadBasicInformation();

            // Get the thread info
            UInt32 Result = NativeMethods.NtQueryInformationThread(ThreadHandle, 0, ref ThreadInfo, MarshalType<ThreadBasicInformation>.Size, IntPtr.Zero);

            // If the function succeeded
            if (Result == 0)
                return ThreadInfo;

            // Else, couldn't get the thread info, throws an exception
            throw new ApplicationException(String.Format("Couldn't get the information from the thread, error code '{0}'.", Result));
        }

        #endregion

        #region ResumeThread
        /// <summary>
        /// Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread to be restarted.</param>
        /// <returns>The thread's previous suspend count.</returns>
        public static uint ResumeThread(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Resume the thread
            UInt32 Result = NativeMethods.ResumeThread(ThreadHandle);

            // If the function failed
            if (Result == UInt32.MaxValue)
                throw new Win32Exception("Couldn't resume the thread.");

            return Result;
        }

        #endregion

        #region SetThreadContext
        /// <summary>
        /// Sets the context for the specified thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread whose context is to be set.</param>
        /// <param name="Context">A pointer to a <see cref="ThreadContext"/> structure that contains the context to be set in the specified thread.</param>
        public static void SetThreadContext(SafeMemoryHandle ThreadHandle, ThreadContext Context)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");
            
            // Set the thread context
            if(!NativeMethods.SetThreadContext(ThreadHandle, ref Context))
                throw new Win32Exception("Couldn't set the thread context.");
        }

        #endregion

        #region SuspendThread
        /// <summary>
        /// Suspends the specified thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread that is to be suspended.</param>
        /// <returns>The thread's previous suspend count.</returns>
        public static UInt32 SuspendThread(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Suspend the thread
            UInt32 Result = NativeMethods.SuspendThread(ThreadHandle);

            // If the function failed
            if (Result == UInt32.MaxValue)
                throw new Win32Exception("Couldn't suspend the thread.");

            return Result;
        }

        #endregion

        #region TerminateThread
        /// <summary>
        /// Terminates a thread.
        /// </summary>
        /// <param name="ThreadHandle">A handle to the thread to be terminated.</param>
        /// <param name="ExitCode">The exit code for the thread.</param>
        public static void TerminateThread(SafeMemoryHandle ThreadHandle, Int32 ExitCode)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(ThreadHandle, "threadHandle");

            // Terminate the thread
            Boolean Success = NativeMethods.TerminateThread(ThreadHandle, ExitCode);

            // If the function failed
            if(!Success)
                throw new Win32Exception("Couldn't terminate the thread.");
        }

        #endregion

        #region WaitForSingleObject
        /// <summary>
        /// Waits until the specified object is in the signaled state or the time-out interval elapses.
        /// </summary>
        /// <param name="Handle">A handle to the object.</param>
        /// <param name="Timeout">The time-out interval. If this parameter is NULL, the function does not enter a wait state if the object is not signaled; it always returns immediately.</param>
        /// <returns>Indicates the <see cref="WaitValues"/> event that caused the function to return.</returns>
        public static WaitValues WaitForSingleObject(SafeMemoryHandle Handle, TimeSpan? Timeout)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(Handle, "handle");

            // Wait for single object
            WaitValues Values = NativeMethods.WaitForSingleObject(Handle, Timeout.HasValue ? Convert.ToUInt32(Timeout.Value.TotalMilliseconds) : 0);

            // If the function failed
            if (Values == WaitValues.Failed)
                throw new Win32Exception("The WaitForSingleObject function call failed.");

            return Values;
        }

        /// <summary>
        /// Waits an infinite amount of time for the specified object to become signaled.
        /// </summary>
        /// <param name="Handle">A handle to the object.</param>
        /// <returns>If the function succeeds, the return value indicates the event that caused the function to return.</returns>
        public static WaitValues WaitForSingleObject(SafeMemoryHandle Handle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(Handle, "handle");

            // Wait for single object
            WaitValues Values = NativeMethods.WaitForSingleObject(Handle, 0xFFFFFFFF);

            // If the function failed
            if (Values == WaitValues.Failed)
                throw new Win32Exception("The WaitForSingleObject function call failed.");

            return Values;
        }

        #endregion

    } // End class

} // End namespace