/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Binarysharp.MemoryManagement.Native;
using Binarysharp.MemoryManagement.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Windows
{
    [TestClass]
    public class WindowCoreTests
    {
        /// <summary>
        /// Gets all windows and checks if the process test is present.
        /// </summary>
        [TestMethod]
        public void EnumTopLevelWindowsCheckPrecence()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            var handles = WindowCore.EnumTopLevelWindows().ToArray();

            // Assert
            Assert.AreNotEqual(0, handles.Count(), "Cannot enumerate windows.");
            Assert.IsTrue(handles.Any(h => h == handle), "Cannot find the process test in the enumerated windows.");
        }

        /// <summary>
        /// Gets all windows and checks whether the function is thread-safe.
        /// </summary>
        [TestMethod]
        public void EnumTopLevelWindowsThreadSafe()
        {
            // Arrange
            IEnumerable<IntPtr> handles1 = null;
            IEnumerable<IntPtr> handles2 = null;
            var r = new ManualResetEvent(false);

// ReSharper disable ImplicitlyCapturedClosure
            var t1 = new Thread(() =>
// ReSharper restore ImplicitlyCapturedClosure
                {
                    r.WaitOne();
                    handles1 = WindowCore.EnumTopLevelWindows();
                });
// ReSharper disable ImplicitlyCapturedClosure
            var t2 = new Thread(() =>
// ReSharper restore ImplicitlyCapturedClosure
                {
                    r.WaitOne();
                    handles2 = WindowCore.EnumTopLevelWindows();
                });

            // Act
            t1.Start();
            t2.Start();
            r.Set();
            t1.Join();
            t2.Join();

            // Assert
            Assert.AreEqual(handles1.Count(), handles2.Count(), "The function is not thread-safe.");
        }

        /// <summary>
        /// Gets child windows and check Scintilla component.
        /// </summary>
        [TestMethod]
        public void EnumChildWindows_CheckIfScintillaPresent()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            var handles = WindowCore.EnumChildWindows(handle).ToArray();

            // Assert
            Assert.AreNotEqual(0, handles.Count(), "Cannot enumerate windows.");
            Assert.IsTrue(handles.Any(h => WindowCore.GetClassName(h) == "Scintilla"), "Cannot find Scientilla in the enumerated windows.");
        }

        /// <summary>
        /// Activates and retrieves the foreground state the test process.
        /// </summary>
        [TestMethod]
        public void SetGetForegroundWindow()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            try
            {
                WindowCore.SetForegroundWindow(handle);
                var ret = WindowCore.GetForegroundWindow();
                Assert.AreEqual(handle, ret, "Couldn't set or get the foreground.");
            }
            catch (ApplicationException ex)
            {
                // Assert
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Uses PostMessage to close the process test main window.
        /// </summary>
        [TestMethod]
        public void PostMessageCloseWindow()
        {
            // Arrange
            var process = Resources.ProcessTest;

            // Act
            WindowCore.PostMessage(process.MainWindowHandle, WindowsMessages.Close, UIntPtr.Zero, UIntPtr.Zero);
            Thread.Sleep(1000);

            // Assert
            Assert.IsTrue(process.HasExited, "The process didn't exited.");
            Resources.Restart();
        }

        /// <summary>
        /// Uses SendMessage to close the process test main window.
        /// </summary>
        [TestMethod]
        public void SendMessageCloseWindow()
        {
            // Arrange
            var process = Resources.ProcessTest;

            // Act
            WindowCore.SendMessage(process.MainWindowHandle, WindowsMessages.Close, UIntPtr.Zero, IntPtr.Zero);
            Thread.Sleep(1000);

            // Assert
            Assert.IsTrue(process.HasExited, "The process didn't exited.");
            Resources.Restart();
        }

        /// <summary>
        /// Compares the thread id got from GetWindowThreadId and the first thread id of the test process.
        /// </summary>
        [TestMethod]
        public void GetWindowThreadId()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            var threadId = WindowCore.GetWindowThreadId(handle);

            // Assert
            Assert.AreEqual(Resources.ProcessTest.Threads[0].Id, threadId, "Thread id are not equal."); // This is the case for Notepad++
        }

        /// <summary>
        /// Sets and retrieves the window's title bar.
        /// </summary>
        [TestMethod]
        public void SetGetWindowText()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;
            const string value = "I love cookies";

            // Act
            WindowCore.SetWindowText(handle, value);
            var title = WindowCore.GetWindowText(handle);

            // Assert
            Assert.AreEqual(value, title, "Couldn't retrieve the title correctly.");
            Resources.Restart();
        }

        /// <summary>
        /// Gets the class name of the process test main window.
        /// </summary>
        [TestMethod]
        public void GetClassName()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            var className = WindowCore.GetClassName(handle);

            // Assert
            Assert.AreEqual("Notepad++", className, "The class name strings are not equal (or it's note Notepad++).");
        }

        /// <summary>
        /// Sets the window to the position 400x400 and size 400x400.
        /// </summary>
        [TestMethod]
        public void SetGetWindowPlacement()
        {
            // Arrange
            var handle = Resources.ProcessTest.MainWindowHandle;

            // Act
            WindowCore.SetWindowPlacement(handle, 400, 400, 400, 400);
            var placement = WindowCore.GetWindowPlacement(handle);

            // Assert
            Assert.AreEqual(400, (placement.NormalPosition.Top + placement.NormalPosition.Left + placement.NormalPosition.Height + placement.NormalPosition.Width) / 4);
        }
    }
}
