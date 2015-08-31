/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Native;
using Binarysharp.MemoryManagement.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Threading
{
    [TestClass]
    public class ThreadCoreTests
    {
        /// <summary>
        /// Suspends and restores the main thread.
        /// </summary>
        [TestMethod]
        public void SuspendRestore()
        {
            // Arrange
            var threadHandle = ThreadCore.OpenThread(ThreadAccessFlags.SuspendResume, Resources.ProcessTest.Threads[0].Id);

            // Act
            try
            {
                var retSuspend = ThreadCore.SuspendThread(threadHandle);
                var retResume = ThreadCore.ResumeThread(threadHandle);

                // Assert
                Assert.AreEqual((uint)0, retSuspend);
                Assert.AreEqual((uint)1, retResume);
            }
            catch (Win32Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Terminates the main thread.
        /// </summary>
        [TestMethod]
        public void Terminate()
        {
            // Arrange
            var handle = ThreadCore.OpenThread(ThreadAccessFlags.Terminate, Resources.ProcessTest.Threads[0].Id);
            var threadId = Resources.ProcessTest.Threads[0].Id;

            // Act
            try
            {
                ThreadCore.TerminateThread(handle, 0);
            }
            // Assert
            catch (Win32Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Resources.ProcessTest.Refresh();
            Assert.AreNotEqual(threadId, Resources.ProcessTest.Threads[0].Id, "The main thread was not properly killed.");
            Resources.Restart();
        }

        /// <summary>
        /// Creates an suspended remote thread.
        /// </summary>
        [TestMethod]
        public void CreateRemoteThread()
        {
            // Arrange
            var handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, Resources.ProcessTest.Id);

            // Act
            var thread = ThreadCore.CreateRemoteThread(handle, new IntPtr(1), IntPtr.Zero ,ThreadCreationFlags.Suspended);
            var threadId = HandleManipulator.HandleToThreadId(thread);

            // Assert
            Assert.IsFalse(thread.IsInvalid);
            Assert.IsTrue(Resources.ProcessTest.Threads.Cast<ProcessThread>().Any(t => t.Id == threadId));
        }

        /// <summary>
        /// Gets main thread context of the process test
        /// </summary>
        [TestMethod]
        public void GetSetThreadContextSuspendResume()
        {
            // Arrange
            var handle = ThreadCore.OpenThread(ThreadAccessFlags.AllAccess, Resources.ProcessTest.Threads[0].Id);

            // Act
            try
            {
                ThreadCore.SuspendThread(handle);

                // Get the context
                var original = ThreadCore.GetThreadContext(handle);
                var modified = original;

                Assert.AreNotEqual(0, modified.Eip);

                // Set a value to eax
                modified.Eax = 0x666;
                // Set the context
                ThreadCore.SetThreadContext(handle, modified);
                // Re-get the context to check if it's all right
                modified = ThreadCore.GetThreadContext(handle);

                Assert.AreEqual((uint)0x666, modified.Eax);

                // Restore the original context
                ThreadCore.SetThreadContext(handle, original);
            }
            finally
            {
                ThreadCore.ResumeThread(handle);
            }
        }

        /// <summary>
        /// Waits for the end of the main thread.
        /// </summary>
        [TestMethod]
        public void WaitForSingleObject()
        {
            // Arrange
            var handle = ThreadCore.OpenThread(ThreadAccessFlags.Synchronize, Resources.ProcessTest.Threads[0].Id);

            // Act
            var task = new Task<WaitValues>(() => ThreadCore.WaitForSingleObject(handle));
            task.Start();
            Resources.Restart();

            // Assert
            Assert.AreEqual(WaitValues.Signaled, task.Result);
        }

        /// <summary>
        /// Gets the exit code of the main thread.
        /// </summary>
        [TestMethod]
        public void GetExitCodeThread()
        {
            // Arrange
            var handle = ThreadCore.OpenThread(ThreadAccessFlags.QueryInformation | ThreadAccessFlags.Terminate, Resources.ProcessTest.Threads[0].Id);

            // Act
            var exitCodeBefore = ThreadCore.GetExitCodeThread(handle);
            // Exit the application
            ThreadCore.TerminateThread(handle, 1337);
            // Wait the process is terminating
            Thread.Sleep(2000);
            // Check the exit code
            var exitCodeAfter = ThreadCore.GetExitCodeThread(handle);

            // Assert
            Assert.AreEqual(null, exitCodeBefore);
            Assert.AreEqual((IntPtr)1337, exitCodeAfter, "The exit code didn't match to the expected value.");

        }
    }
}
