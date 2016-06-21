using Anathema.Source.Engine.OperatingSystems.Windows.Native;
using System;
using System.Diagnostics;
using System.Linq;

namespace Anathema.Source.Engine.OperatingSystems.Windows.Helpers
{
    /// <summary>
    /// Static helper class providing tools for manipulating handles.
    /// </summary>
    public static class HandleManipulator
    {
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="Handle">A valid handle to an open object.</param>
        public static void CloseHandle(IntPtr Handle)
        {
            NativeMethods.CloseHandle(Handle);
        }

        /// <summary>
        /// Converts an handle into a <see cref="Process"/> object assuming this is a process handle.
        /// </summary>
        /// <param name="ProcessHandle">A valid handle to an opened process.</param>
        /// <returns>A <see cref="Process"/> object from the specified handle.</returns>
        public static Process HandleToProcess(SafeMemoryHandle ProcessHandle)
        {
            // Search the process by iterating the processes list
            return Process.GetProcesses().First(X => X.Id == HandleToProcessId(ProcessHandle));
        }

        /// <summary>
        /// Converts an handle into a process id assuming this is a process handle.
        /// </summary>
        /// <param name="ProcessHandle">A valid handle to an opened process.</param>
        /// <returns>A process id from the specified handle.</returns>
        public static Int32 HandleToProcessId(SafeMemoryHandle ProcessHandle)
        {
            // Check if the handle is valid
            if (!ValidateAsArgument(ProcessHandle))
                return 0;

            // Find the process id
            Int32 ProcessID = NativeMethods.GetProcessId(ProcessHandle);

            // If the process id is valid
            if (ProcessID != 0)
                return ProcessID;

            return 0;
        }

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
                ProcessThread ProcessThread = Process.Threads.Cast<ProcessThread>().FirstOrDefault(X => X.Id == HandleToThreadId(ThreadHandle));
                if (ProcessThread != null)
                    return ProcessThread;
            }

            return null;
        }

        /// <summary>
        /// Converts an handle into a thread id assuming this is a thread handle.
        /// </summary>
        /// <param name="ThreadHandle">A valid handle to an opened thread.</param>
        /// <returns>A thread id from the specified handle.</returns>
        public static Int32 HandleToThreadId(SafeMemoryHandle ThreadHandle)
        {
            // Check if the handle is valid
            if (!ValidateAsArgument(ThreadHandle))
                return 0;

            // Find the thread id
            Int32 ThreadID = NativeMethods.GetThreadId(ThreadHandle);

            // If the thread id is valid
            if (ThreadID != 0)
                return ThreadID;

            return 0;
        }

        /// <summary>
        /// Validates an handle to fit correctly as argument.
        /// </summary>
        /// <param name="Handle">A handle to validate.</param>
        /// <param name="ArgumentName">The name of the argument that represents the handle in its original function.</param>
        public static Boolean ValidateAsArgument(SafeMemoryHandle Handle)
        {
            // Check if the handle is not null
            if (Handle == null)
                return false;

            // Check if the handle is valid
            if (Handle.IsClosed || Handle.IsInvalid)
                return false;

            return true;
        }

    } // End class

} // End namespace