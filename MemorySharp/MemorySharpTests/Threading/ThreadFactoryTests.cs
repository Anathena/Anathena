/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Threading
{
    [TestClass]
    public class ThreadFactoryTests
    {
        /// <summary>
        /// Creates a remote thread.
        /// </summary>
        [TestMethod]
        public void CreateThread()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            var thread = sharp.Threads.Create(new IntPtr(1), false);

            // Assert
            Assert.IsTrue(sharp.Threads.RemoteThreads.Any(t => t.Id == thread.Id));
        }

        /// <summary>
        /// Gets the main thread by its id.
        /// </summary>
        [TestMethod]
        public void GetThreadById()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            var mainThread = Resources.ProcessTest.Threads[0];

            // Act
            var mainThread2 = sharp.Threads.GetThreadById(mainThread.Id);

            // Assert
            Assert.AreEqual(mainThread.Id, mainThread2.Id, "Cannot get the main thread properly.");
        }

        /// <summary>
        /// Retrieves remote threads.
        /// </summary>
        [TestMethod]
        public void RemoteThreadsAny()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            var ret = sharp.Threads.RemoteThreads.ToArray();

            // Assert
            Assert.IsTrue(ret.Any(), "Cannot gather remote threads.");
            Assert.AreEqual(Resources.ProcessTest.Threads.Count, ret.Count(), "The number of thread does not match.");
        }

        /// <summary>
        /// Suspends and resumes all the threads.
        /// </summary>
        [TestMethod]
        public void SuspenAllResumeAll()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act - Assert
            sharp.Threads.SuspendAll();
            Assert.IsFalse(sharp.Threads.RemoteThreads.Any(t => !t.IsSuspended), "A thread is not suspended.");
            sharp.Threads.ResumeAll();
        }
    }
}
