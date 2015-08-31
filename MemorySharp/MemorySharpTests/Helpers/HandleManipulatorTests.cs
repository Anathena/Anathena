/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using Binarysharp.MemoryManagement.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Helpers
{
    [TestClass]
    public class HandleManipulatorTests
    {
        /// <summary>
        /// Converts a handle to a managed process.
        /// </summary>
        [TestMethod]
        public void HandleToProcess()
        {
            // Arrange
            var processHandle = Resources.MemorySharp.Handle;

            // Act
            var ret = HandleManipulator.HandleToProcess(processHandle);

            // Assert
            Assert.AreEqual(Resources.ProcessTest.Id, ret.Id, "The both process id are not equal.");
        }

        /// <summary>
        /// Converts a handle to a process id.
        /// </summary>
        [TestMethod]
        public void HandleToProcessId()
        {
            // Arrange
            var processHandle = Resources.MemorySharp.Handle;

            // Act
            var ret = HandleManipulator.HandleToProcessId(processHandle);

            // Assert
            Assert.AreEqual(Resources.ProcessTest.Id, ret, "The both process id are not equal.");
        }

        /// <summary>
        /// Converts a handle to a managed thread.
        /// </summary>
        [TestMethod]
        public void HandleToThread()
        {
            // Arrange
            var threadHandle = Resources.MemorySharp.Threads.MainThread;

            // Act
            var ret = HandleManipulator.HandleToThread(threadHandle.Handle);

            // Assert
            Assert.AreEqual(threadHandle.Id, ret.Id, "The both thread id are equal.");
        }

        /// <summary>
        /// Converts a handle to a thread id.
        /// </summary>
        [TestMethod]
        public void HandleToThreadId()
        {
            // Arrange
            var threadHandle = Resources.MemorySharp.Threads.MainThread;

            // Act
            var ret = HandleManipulator.HandleToThreadId(threadHandle.Handle);

            // Assert
            Assert.AreEqual(threadHandle.Id, ret, "The both thread id are equal.");
        }
    }
}
