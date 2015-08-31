/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using Binarysharp.MemoryManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Modules
{
    [TestClass]
    public class RemoteModuleTests
    {
        /// <summary>
        /// Finds the Beep function in a module loaded in the remote process.
        /// </summary>
        [TestMethod]
        public void FindFunctionInModuleAlreadyLoaded()
        {
            // Arrange
            var memorysharp = Resources.MemorySharp;

            // Act
            var ret = memorysharp.Modules["kernel32.dll"].FindFunction("Beep");

            // Assert
            Assert.IsTrue(ret != null && ret.Name == "Beep");
        }

        /// <summary>
        /// Finds the Beep function in a module loaded in the remote process and checks the module cache.
        /// </summary>
        [TestMethod]
        public void FindFunctionCheckCachedFunctions()
        {
            // Arrange
            var memorysharp = Resources.MemorySharp;

            // Act
            var func1 = memorysharp.Modules["kernel32.dll"].FindFunction("Beep");
            var func2 = memorysharp.Modules["kernel32.dll"].FindFunction("Beep");

            // Assert
            Assert.AreSame(func1, func2, "The cache does not work properly.");
        }

        /// <summary>
        /// Finds the a function in a module loaded in the remote process but not in the calling process.
        /// </summary>
        [TestMethod]
        public void FindFunctionInModuleNotLoaded()
        {
            // Arrange
            var memorysharp = Resources.MemorySharp;

            // Act
            var ret = memorysharp.Modules["SciLexer.dll"].FindFunction("Scintilla_DirectFunction");

            // Assert
            Assert.IsTrue(ret != null && ret.Name == "Scintilla_DirectFunction");
        }

        /// <summary>
        /// Checks if the implementation of IEquatable interface.
        /// </summary>
        [TestMethod]
        public void Equals()
        {
            // Arrange
            var remote = Resources.MemorySharp;
            var own = new MemorySharp(Resources.ProcessSelf);

            // Act
            var module1 = remote["kernel32"];
            var module2 = remote["kernel32"];
            var module3 = own["kernel32"];

            // Assert
            Assert.AreEqual(module1, module2);
            Assert.AreNotEqual(module1, module3);
        }
    }
}
