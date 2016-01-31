/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Threading
{
    /// <summary>
    /// Class providing tools for manipulating threads.
    /// </summary>
    public class ThreadFactory : IFactory
    {
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly MemorySharp MemorySharp;

        /// <summary>
        /// Gets the main thread of the remote process.
        /// </summary>
        public RemoteThread MainThread
        {
            get
            {
                return new RemoteThread(MemorySharp, NativeThreads.Aggregate((current, next) => next.StartTime < current.StartTime ? next : current));
            }
        }

        /// <summary>
        /// Gets the native threads from the remote process.
        /// </summary>
        internal IEnumerable<ProcessThread> NativeThreads
        {
            get
            {
                // Refresh the process info
                MemorySharp.Native.Refresh();
                // Enumerates all threads
                return MemorySharp.Native.Threads.Cast<ProcessThread>();
            }
        }

        /// <summary>
        /// Gets the threads from the remote process.
        /// </summary>
        public IEnumerable<RemoteThread> RemoteThreads
        {
            get { return NativeThreads.Select(x => new RemoteThread(MemorySharp, x)); }
        }
        
        #region This
        /// <summary>
        /// Gets the thread corresponding to an id.
        /// </summary>
        /// <param name="threadId">The unique identifier of the thread to get.</param>
        /// <returns>A new instance of a <see cref="RemoteThread"/> class.</returns>
        public RemoteThread this[int threadId]
        {
            get
            {
                return new RemoteThread(MemorySharp, NativeThreads.First(t => t.Id == threadId));
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadFactory"/> class.
        /// </summary>
        /// <param name="MemorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        internal ThreadFactory(MemorySharp MemorySharp)
        {
            // Save the parameter
            this.MemorySharp = MemorySharp;
        }

        #endregion

        #region Method
        #region Create
        /// <summary>
        /// Creates a thread that runs in the remote process.
        /// </summary>
        /// <param name="Address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="Parameter">A variable to be passed to the thread function.</param>
        /// <param name="IsStarted">Sets if the thread must be started just after being created.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread Create(IntPtr Address, dynamic Parameter, Boolean IsStarted = true)
        {
            // Marshal the parameter
            MarshalledValue<dynamic> MarshalledParameter = MarshalValue.Marshal(MemorySharp, Parameter);

            //Create the thread
            ThreadBasicInformation ThreadInfo = ThreadCore.NtQueryInformationThread(
                ThreadCore.CreateRemoteThread(MemorySharp.Handle, Address, MarshalledParameter.Reference, ThreadCreationFlags.Suspended));

            // Find the managed object corresponding to this thread
            RemoteThread RemoteThread = new RemoteThread(MemorySharp, MemorySharp.Threads.NativeThreads.First(x => x.Id == ThreadInfo.ThreadId), MarshalledParameter);

            // If the thread must be started
            if (IsStarted)
                RemoteThread.Resume();

            return RemoteThread;
        }

        /// <summary>
        /// Creates a thread that runs in the remote process.
        /// </summary>
        /// <param name="Address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="IsStarted">Sets if the thread must be started just after being created.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread Create(IntPtr Address, Boolean IsStarted = true)
        {
            //Create the thread
            ThreadBasicInformation ThreadInfo = ThreadCore.NtQueryInformationThread(
                ThreadCore.CreateRemoteThread(MemorySharp.Handle, Address, IntPtr.Zero, ThreadCreationFlags.Suspended));

            // Find the managed object corresponding to this thread
            RemoteThread RemoteThread = new RemoteThread(MemorySharp, MemorySharp.Threads.NativeThreads.First(x => x.Id == ThreadInfo.ThreadId));

            // If the thread must be started
            if (IsStarted)
                RemoteThread.Resume();

            return RemoteThread;
        }

        #endregion
        #region CreateAndJoin
        /// <summary>
        /// Creates a thread in the remote process and blocks the calling thread until the thread terminates.
        /// </summary>
        /// <param name="Address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="Parameter">A variable to be passed to the thread function.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread CreateAndJoin(IntPtr Address, dynamic Parameter)
        {
            // Create the thread
            RemoteThread RemoteThread = Create(Address, Parameter);

            // Wait the end of the thread
            RemoteThread.Join();

            // Return the thread
            return RemoteThread;
        }

        /// <summary>
        /// Creates a thread in the remote process and blocks the calling thread until the thread terminates.
        /// </summary>
        /// <param name="Address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread CreateAndJoin(IntPtr Address)
        {
            // Create the thread
            RemoteThread RemoteThread = Create(Address);

            // Wait the end of the thread
            RemoteThread.Join();

            // Return the thread
            return RemoteThread;
        }

        #endregion
        #region Dispose (implementation of IFactory)
        /// <summary>
        /// Releases all resources used by the <see cref="ThreadFactory"/> object.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose... yet
        }

        #endregion
        #region GetThreadById
        /// <summary>
        /// Gets a thread by its id in the remote process.
        /// </summary>
        /// <param name="Id">The id of the thread.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread GetThreadById(Int32 Id)
        {
            return new RemoteThread(MemorySharp, NativeThreads.First(x => x.Id == Id));
        }

        #endregion
        #region ResumeAll
        /// <summary>
        /// Resumes all threads.
        /// </summary>
        public void ResumeAll()
        {
            foreach (RemoteThread Thread in RemoteThreads)
            {
                Thread.Resume();
            }
        }

        #endregion
        #region SuspendAll
        /// <summary>
        /// Suspends all threads.
        /// </summary>
        public void SuspendAll()
        {
            foreach (RemoteThread Thread in RemoteThreads)
            {
                Thread.Suspend();
            }
        }

        #endregion
        #endregion

    } // End class

} // End namespace