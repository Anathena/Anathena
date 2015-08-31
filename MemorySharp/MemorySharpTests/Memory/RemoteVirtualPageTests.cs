/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System.Threading;
using Binarysharp.MemoryManagement.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Memory
{
    [TestClass]
    public class RemoteVirtualPageTests
    {
        /// <summary>
        /// Changes the protection of the main module and restores it.
        /// </summary>
        [TestMethod]
        public void ChangeProtection()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act - Assert
            Resources.EndTests(sharp);
            Assert.AreEqual(MemoryProtectionFlags.ReadOnly, sharp.Modules.MainModule.Information.Protect, "The main module is not in readonly.");
            using (sharp.Modules.MainModule.ChangeProtection(MemoryProtectionFlags.ExecuteRead))
            {
                Assert.AreEqual(MemoryProtectionFlags.ExecuteRead, sharp.Modules.MainModule.Information.Protect, "The main module protection couldn't be changed.");
            }
            Assert.AreEqual(MemoryProtectionFlags.ReadOnly, sharp.Modules.MainModule.Information.Protect, "The main module is not in readonly (2).");
        }
    }
}
