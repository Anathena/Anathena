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
using System.Linq;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Memory
{
    [TestClass]
    public class MemoryCoreTests
    {
        /// <summary>
        /// Allocates and free a memory page.
        /// </summary>
        [TestMethod]
        public void AllocateFree()
        {
            // Arrange
            var handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, Resources.ProcessTest.Id);

            // Act
            try
            {
                var address = MemoryCore.Allocate(handle, 1);
                MemoryCore.Free(handle, address);
            }
            catch (Win32Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        /// <summary>
        /// Change the protection, writes and reads the first bytes of the memory.
        /// </summary>
        [TestMethod]
        public void VirtualProtectExWriteReadBytes()
        {
            // Arrange
            var handle = MemoryCore.OpenProcess(ProcessAccessFlags.AllAccess, Resources.ProcessTest.Id);
            var expected = new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 };
            var memory = new IntPtr(0x00400000);

            // Act
            try
            {
                MemoryCore.ChangeProtection(handle, memory, 5, MemoryProtectionFlags.ExecuteReadWrite);
                MemoryCore.WriteBytes(handle, memory, expected);
                var actual = MemoryCore.ReadBytes(handle, memory, 5);

                // Assert
                CollectionAssert.AreEqual(expected, actual, "The collections are not equal.");
            }
            catch (Win32Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// List all memory regions.
        /// </summary>
        [TestMethod]
        public void VirtualQueryEx_AnyProcess_ListAllMemoryPages()
        {
            // Arrange
            var handle = Resources.MemorySharp.Handle;
            var starting = new IntPtr(0);
            var ending = new IntPtr(0x07fffffff);

            // Act
            var regions = MemoryCore.Query(handle, starting, ending);

            // Assert
            Assert.AreNotEqual(0, regions.Count(), "Memory pages cannot be gathered.");
        }
    }
}
