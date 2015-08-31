/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Binarysharp.MemoryManagement.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Modules
{
    [TestClass]
    public class ModuleCoreTests
    {
        /// <summary>
        /// Loads and frees a library.
        /// </summary>
        [TestMethod]
        public void LoadFreeLibrary()
        {
            // Arrange
            var dllPath = Resources.LibraryTest;

            // Act
            var module = ModuleCore.LoadLibrary(dllPath);

            // Assert
            Assert.AreEqual(module.FileName, dllPath, "The module cannot be loaded correctly.");
            Assert.IsTrue(Resources.ProcessSelf.Modules.Cast<ProcessModule>().Any(m => m.FileName == dllPath), "The module cannot be found.");

            // Act 2
            ModuleCore.FreeLibrary(module);

            // Assert 2
            Assert.IsFalse(Resources.ProcessSelf.Modules.Cast<ProcessModule>().Any(m => m.FileName == dllPath), "The module cannot be freed.");
        }

        /// <summary>
        /// Gets the pointer of MessageBoxW function from Win32 API.
        /// </summary>
        [TestMethod]
        public void GetProcAddressMessageBoxW()
        {
            // Arrange
            const string moduleName = "User32.dll";
            const string functionName = "MessageBoxW";

            // Act
            try
            {
                ModuleCore.GetProcAddress(moduleName, functionName);
            }
            // Assert
            catch (Win32Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
