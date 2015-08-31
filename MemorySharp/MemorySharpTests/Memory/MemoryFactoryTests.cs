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
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Memory
{
    [TestClass]
    public class MemoryFactoryTests
    {
        /// <summary>
        /// Gets all memory regions.
        /// </summary>
        [TestMethod]
        public void Regions_GetAllMemoryRegions()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            var ret = sharp.Memory.VirtualPages.ToArray();

            // Assert
            Assert.IsTrue(ret.Any(), "The array of memory regions is empty.");
            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Allocates 1MB of memory in the remote process.
        /// </summary>
        [TestMethod]
        public void Allocate_Allocate1MBMemoryRegion()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const int size = 1024*1024;

            // Act
            var allocated = sharp.Memory.Allocate(size);
            // Fill the cave
            try
            {
                for (var i = 0; i < allocated.Information.RegionSize; i++)
                {
                    allocated.Write(i, (byte)1);
                }
            }
            catch (Exception)
            {
                Assert.Fail("Cannot write into the memory.");
            }

            // Assert
            Assert.IsTrue(size - 4096 < allocated.Information.RegionSize && allocated.Information.RegionSize < size + 4096, "The allocated memory is wrong."); // 4096 = size of a page
            Assert.AreEqual(MemoryStateFlags.Commit, allocated.Information.State, "The state of the memory is incorrect.");
            Assert.AreEqual(MemoryProtectionFlags.ExecuteReadWrite, allocated.Information.Protect, "The protection of the memory is incorrect.");
            allocated.Dispose();
            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Allocates a chunk of memory using the keyword 'using'.
        /// </summary>
        [TestMethod]
        public void Allocate_WithUsingStatement()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            RemoteAllocation remoteAllocation;

            // Act
            using (remoteAllocation = sharp.Memory.Allocate(1))
            {
                Assert.IsTrue(remoteAllocation.IsValid, "The chunk of memory couldn't be allocated.");
                Assert.AreEqual(4096, remoteAllocation.Information.RegionSize, "The chunk of memory doesn't have the default size of a page.");
            }

            // Assert
            Assert.IsFalse(remoteAllocation.IsValid, "The pointer is still valid.");
            Resources.EndTests(sharp);
        }
    }
}
