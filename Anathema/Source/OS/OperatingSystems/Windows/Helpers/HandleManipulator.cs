using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Anathema.MemoryManagement.Native;

namespace Anathema.MemoryManagement.Helpers
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
        /// <param name="Handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr Handle)
        {
            // Check if the handle is valid
            ValidateAsArgument(Handle, "handle");

            // Close the handle
            if(!NativeMethods.CloseHandle(Handle))
                throw new Win32Exception("Couldn't close the handle correctly.");
        }
        #endregion

        #region HandleToProcess
        /// <summary>
        /// Converts an handle into a <see cref="Process"/> object assuming this is a process handle.
        /// </summary>
        /// <param name="ProcessHandle">A valid handle to an opened process.</param>
        /// <returns>A <see cref="Process"/> object from the specified handle.</returns>
        public static Process HandleToProcess(SafeMemoryHandle ProcessHandle)
        {
            // Search the process by iterating the processes list
            return Process.GetProcesses().First(x => x.Id == HandleToProcessId(ProcessHandle));
        }
        #endregion

        #region HandleToProcessId
        /// <summary>
        /// Converts an handle into a process id assuming this is a process handle.
        /// </summary>
        /// <param name="ProcessHandle">A valid handle to an opened process.</param>
        /// <returns>A process id from the specified handle.</returns>
        public static int HandleToProcessId(SafeMemoryHandle ProcessHandle)
        {
            // Check if the handle is valid
            ValidateAsArgument(ProcessHandle, "processHandle");

            // Find the process id
            Int32 ProcessID = NativeMethods.GetProcessId(ProcessHandle);

            // If the process id is valid
            if (ProcessID != 0)
                return ProcessID;

            // Else the function failed, throws an exception
            throw new Win32Exception("Couldn't find the process id of the specified handle.");
        }

        #endregion

        #region HandleToThread
        /// <summary>
        /// Converts an handle into a <see cref="ProcessThread"/> object assuming this is a thread handle.
        /// </summary>
        /// <param name="ThreadHandle">A valid handle to an opened thread.</param>
        /// <returns>A <see cref="ProcessThread"/> object from the specified handle.</returns>
        public static ProcessThread HandleToThread(SafeMemoryHandle ThreadHandle)
        {
            // Search the thread by iterating the processes list
            foreach (Process Process in Process.GetProcesses())
            {
                ProcessThread ProcessThread = Process.Threads.Cast<ProcessThread>().FirstOrDefault(t => t.Id == HandleToThreadId(ThreadHandle));
                if (ProcessThread != null)
                    return ProcessThread;
            }

            // If no thread was found, throws a exception like the First() function with no element
            throw new InvalidOperationException("Sequence contains no matching element");
        }
        #endregion

        #region HandleToThreadId
        /// <summary>
        /// Converts an handle into a thread id assuming this is a thread handle.
        /// </summary>
        /// <param name="ThreadHandle">A valid handle to an opened thread.</param>
        /// <returns>A thread id from the specified handle.</returns>
        public static int HandleToThreadId(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            ValidateAsArgument(ThreadHandle, "threadHandle");

            // Find the thread id
            Int32 ThreadID = NativeMethods.GetThreadId(ThreadHandle);

            // If the thread id is valid
            if (ThreadID != 0)
                return ThreadID;

            //Else the function failed, throws an exception
            throw new Win32Exception("Couldn't find the thread id of the specified handle.");
        }

        #endregion

        #region ValidateAsArgument
        /// <summary>
        /// Validates an handle to fit correctly as argument.
        /// </summary>
        /// <param name="Handle">A handle to validate.</param>
        /// <param name="ArgumentName">The name of the argument that represents the handle in its original function.</param>
        public static Boolean ValidateAsArgument(IntPtr Handle, String ArgumentName)
        {
            // Check if the handle is not null
            if (Handle == null)
                return false;// throw new ArgumentNullException(argumentName);

            // Check if the handle is valid
            if (Handle == IntPtr.Zero)
                return false;// throw new ArgumentException("The handle is not valid.", argumentName);

            return true;
        }

        /// <summary>
        /// Validates an handle to fit correctly as argument.
        /// </summary>
        /// <param name="Handle">A handle to validate.</param>
        /// <param name="ArgumentName">The name of the argument that represents the handle in its original function.</param>
        public static void ValidateAsArgument(SafeMemoryHandle Handle, string ArgumentName)
        {
            // Check if the handle is not null
            if (Handle == null)
                throw new ArgumentNullException(ArgumentName);

            // Check if the handle is valid
            if(Handle.IsClosed || Handle.IsInvalid)
                throw new ArgumentException("The handle is not valid or closed.", ArgumentName);
        }
        #endregion

    } // End class

} // End namespace