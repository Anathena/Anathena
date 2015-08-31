/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Modules
{
    [TestClass]
    public class ModuleFactoryTests
    {
        /// <summary>
        /// Gets all modules and check the difference with the native API.
        /// </summary>
        [TestMethod]
        public void Modules()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            var modules = sharp.Modules.RemoteModules.ToArray();

            // Assert
            Assert.AreEqual(Resources.ProcessTest.Modules.Count, modules.Length, "The number of modules doesn't match.");
        }

        /// <summary>
        /// Verifies the MainModule property.
        /// </summary>
        [TestMethod]
        public void MainModule()
        {
            // Arrange
            var process = Resources.ProcessTest;
            var memorySharp = Resources.MemorySharp;

            // Act
            var mainModule = memorySharp.Modules.MainModule;

            // Assert
            Assert.AreEqual(process.MainModule.BaseAddress, mainModule.Native.BaseAddress, "Both modules aren't equal.");
        }

        /// <summary>
        /// Injects and ejects a library.
        /// </summary>
        [TestMethod]
        public void InjectEjectLibrary()
        {
            Resources.Restart();
            // Arrange
            var sharp = Resources.MemorySharp;
            var dllPath = Resources.LibraryTest;

            // Act
            using (var lib = sharp.Modules.Inject(dllPath))
            {
                // Assert
                Assert.AreNotEqual(IntPtr.Zero, lib.BaseAddress, "The library couldn't be loaded properly.");
                Assert.IsTrue(sharp.Modules.InjectedModules.Any(m => m == lib), "The collection of injected modules doesn't contain the module.");
                Assert.IsTrue(Resources.ProcessTest.Modules.Cast<ProcessModule>().Any(m => m.FileName.ToLower() == dllPath.ToLower()), "Cannot find the module using native API.");
            }

            Resources.EndTests(sharp);
        }
    }
}
