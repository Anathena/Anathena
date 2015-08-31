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
    public class KeyboardTests
    {
        /// <summary>
        /// Presses and releases the bottom arrow key during 3 seconds in Scintilla.
        /// </summary>
        /// <remarks>Manual assert.</remarks>
        [TestMethod]
        public void ArrowBottom3Sec()
        {
            // Arrange
            var scintilla = Resources.MemorySharp.Windows.GetWindowsByClassName("Scintilla").First();

            // Act
            scintilla.Keyboard.Press(Keys.Down, TimeSpan.FromMilliseconds(20));
            Thread.Sleep(3000);
            scintilla.Keyboard.Release(Keys.Down);

            // Assert
            Assert.Inconclusive("Manual assert");
        }

        /// <summary>
        /// Presses and releases the key F1.
        /// </summary>
        /// <remarks>Manual assert.</remarks>
        [TestMethod]
        public void PostKey_MainWindow()
        {
            // Arrange
            var window = Resources.MemorySharp.Windows.MainWindow;

            // Act
            window.Keyboard.PressRelease(Keys.F1);

            // Assert
            Assert.Inconclusive("Manual assert");
        }

        /// <summary>
        /// Writes a text in Scintilla.
        /// </summary>
        /// <remarks>Manual assert.</remarks>
        [TestMethod]
        public void WriteText()
        {
            // Arrange
            var scintilla = Resources.MemorySharp.Windows.GetWindowsByClassName("Scintilla").First();

            // Act
            scintilla.Keyboard.Write("<B1n@rYsH-arP^^$$#>");

            // Assert
            Assert.Inconclusive("Manual assert");
        }
    }
}
