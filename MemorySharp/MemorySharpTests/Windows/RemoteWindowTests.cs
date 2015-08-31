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
using System.Threading;
using Binarysharp.MemoryManagement.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Windows
{
    [TestClass]
    public class RemoteWindowTests
    {
        /// <summary>
        /// Gets the children of the main window and checks if Scintilla is there.
        /// </summary>
        [TestMethod]
        public void ChildrenAndClassName()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            var window = sharp.Windows.MainWindow.Children.FirstOrDefault(w => w.ClassName == "Scintilla");

            // Assert
            Assert.AreNotEqual(null, window, "Cannot find Scintilla window.");
        }

        /// <summary>
        /// Gets and sets the height and the width.
        /// </summary>
        [TestMethod]
        public void GetSetHeightWidthPosition()
        {
            // Arrange
            var window = Resources.MemorySharp.Windows.MainWindow;
            const int size = 200;

            // Act
            window.Height = size;
            window.Width = size;
            window.X = size;
            window.Y = size;
            var height = window.Height;
            var width = window.Width;
            var x = window.X;
            var y = window.Y;

            // Assert
            Assert.AreEqual(size, height, "The height cannot be got/set.");
            Assert.AreEqual(size, width, "The width cannot be got/set.");
            Assert.AreEqual(size, x, "The X-coordinate cannot be got/set.");
            Assert.AreEqual(size, y, "The Y-coordinate cannot be got/set.");
        }

        /// <summary>
        /// Activates the window.
        /// </summary>
        [TestMethod]
        public void ActivateAndIsActivated()
        {
            // Arrange
            var window = Resources.MemorySharp.Windows.MainWindow;

            // Act
            window.Activate();
            Thread.Sleep(1000);
            var ret = window.IsActivated;

            // Assert
            Assert.AreEqual(true, ret, "The window is not activated.");
        }

        /// <summary>
        /// Gets and sets the state of the window.
        /// </summary>
        [TestMethod]
        public void GetSetState()
        {
            // Arrange
            var window = Resources.MemorySharp.Windows.MainWindow;

            // Act
            window.State = WindowStates.ShowMinimized;
            var ret = window.State;

            // Assert
            Assert.AreEqual(WindowStates.ShowMinimized, ret, "The window is not minimized.");
        }

        /// <summary>
        /// Get and sets the title.
        /// </summary>
        [TestMethod]
        public void Title()
        {
            // Arrange
            var window = Resources.MemorySharp.Windows.MainWindow;
            const string title = "I love cookies";

            // Act
            window.Title = title;
            var ret = window.Title;

            // Assert
            Assert.AreEqual(title, ret, "The title cannot be got/set.");
            Resources.Restart();
        }

        /// <summary>
        /// Closes the test process.
        /// </summary>
        [TestMethod]
        public void Close()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            sharp.Windows.MainWindow.Close();
            Thread.Sleep(2000);

            // Assert
            Assert.IsTrue(sharp.IsRunning, "The process is not closed.");
            Resources.Restart();
        }
    }
}
