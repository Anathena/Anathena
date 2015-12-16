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
        #region Fields
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        protected readonly MemorySharp MemorySharp;
        #endregion

        #region Properties
        #region MainThread
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
        #endregion
        #region NativeThreads (internal)
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
        #endregion
        #region RemoteThreads
        /// <summary>
        /// Gets the threads from the remote process.
        /// </summary>
        public IEnumerable<RemoteThread> RemoteThreads
        {
            get { return NativeThreads.Select(t => new RemoteThread(MemorySharp, t)); }
        }
        #endregion
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
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadFactory"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        internal ThreadFactory(MemorySharp memorySharp)
        {
            // Save the parameter
            MemorySharp = memorySharp;
        }
        #endregion

        #region Method
        #region Create
        /// <summary>
        /// Creates a thread that runs in the remote process.
        /// </summary>
        /// <param name="address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="parameter">A variable to be passed to the thread function.</param>
        /// <param name="isStarted">Sets if the thread must be started just after being created.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread Create(IntPtr address, dynamic parameter, bool isStarted = true)
        {
            // Marshal the parameter
            var marshalledParameter = MarshalValue.Marshal(MemorySharp, parameter);

            //Create the thread
            var ret = ThreadCore.NtQueryInformationThread(
                ThreadCore.CreateRemoteThread(MemorySharp.Handle, address, marshalledParameter.Reference, ThreadCreationFlags.Suspended));

            // Find the managed object corresponding to this thread
            var result = new RemoteThread(MemorySharp, MemorySharp.Threads.NativeThreads.First(t => t.Id == ret.ThreadId), marshalledParameter);

            // If the thread must be started
            if (isStarted)
                result.Resume();
            return result;
        }
        /// <summary>
        /// Creates a thread that runs in the remote process.
        /// </summary>
        /// <param name="address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="isStarted">Sets if the thread must be started just after being created.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread Create(IntPtr address, bool isStarted = true)
        {
            //Create the thread
            var ret = ThreadCore.NtQueryInformationThread(
                ThreadCore.CreateRemoteThread(MemorySharp.Handle, address, IntPtr.Zero, ThreadCreationFlags.Suspended));

            // Find the managed object corresponding to this thread
            var result = new RemoteThread(MemorySharp, MemorySharp.Threads.NativeThreads.First(t => t.Id == ret.ThreadId));

            // If the thread must be started
            if (isStarted)
                result.Resume();
            return result;
        }
        #endregion
        #region CreateAndJoin
        /// <summary>
        /// Creates a thread in the remote process and blocks the calling thread until the thread terminates.
        /// </summary>
        /// <param name="address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <param name="parameter">A variable to be passed to the thread function.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread CreateAndJoin(IntPtr address, dynamic parameter)
        {
            // Create the thread
            var ret = Create(address, parameter);
            // Wait the end of the thread
            ret.Join();
            // Return the thread
            return ret;
        }
        /// <summary>
        /// Creates a thread in the remote process and blocks the calling thread until the thread terminates.
        /// </summary>
        /// <param name="address">
        /// A pointer to the application-defined function to be executed by the thread and represents 
        /// the starting address of the thread in the remote process.
        /// </param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread CreateAndJoin(IntPtr address)
        {
            // Create the thread
            var ret = Create(address);
            // Wait the end of the thread
            ret.Join();
            // Return the thread
            return ret;
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
        /// <param name="id">The id of the thread.</param>
        /// <returns>A new instance of the <see cref="RemoteThread"/> class.</returns>
        public RemoteThread GetThreadById(int id)
        {
            return new RemoteThread(MemorySharp, NativeThreads.First(t => t.Id == id));
        }
        #endregion
        #region ResumeAll
        /// <summary>
        /// Resumes all threads.
        /// </summary>
        public void ResumeAll()
        {
            foreach (var thread in RemoteThreads)
            {
                thread.Resume();
            }
        }
        #endregion
        #region SuspendAll
        /// <summary>
        /// Suspends all threads.
        /// </summary>
        public void SuspendAll()
        {
            foreach (var thread in RemoteThreads)
            {
                thread.Suspend();
            }
        }
        #endregion
        #endregion
    }
}
