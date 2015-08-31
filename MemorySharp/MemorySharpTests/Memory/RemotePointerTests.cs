/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Memory
{
    [TestClass]
    public class RemotePointerTests
    {
        /// <summary>
        /// Checks the implementation of IEquatable.
        /// </summary>
        [TestMethod]
        public void Equality()
        {
            // Arrange
            var notepad = Resources.MemorySharp;
            var own = new MemorySharp(Resources.ProcessSelf);

            // Act
            var ptr1 = notepad[IntPtr.Zero];
            var ptr2 = notepad[IntPtr.Zero];
            var ptr3 = own[IntPtr.Zero];

            // Assert
            Assert.AreEqual(ptr1, ptr2, "IEquatable is not properly implemented.");
            Assert.AreNotEqual(ptr2, ptr3, "IEquatable is not properly implemented.");
        }

        /// <summary>
        /// Changes the protection of a chunk of memory.
        /// </summary>
        [TestMethod]
        public void ChangeProtection()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            // Allocate a chunk of memory for testing purpose
            using (var memory = sharp.Memory.Allocate(1))
            {
                // Check we can write inside
                try
                {
                    memory.Write(1);
                }
                catch (Exception)
                {
                    Assert.Fail("Unable to write inside the chunk of memory (issue with memory allocation ?).");
                }
                // Change the protection to read only
                using (var change = memory.ChangeProtection(MemoryProtectionFlags.ReadOnly))
                {
                    // Check we CANNOT write inside
                    try
                    {
                        memory.Write(1);
                        Assert.Fail("The memory mustn't be writable here.");
                    }
                    // ReSharper disable EmptyGeneralCatchClause
                    catch
                    // ReSharper restore EmptyGeneralCatchClause
                    {
                        // All is right
                    }
                    // Some asserts
                    Assert.AreEqual(MemoryProtectionFlags.ExecuteReadWrite, change.OldProtection, "The old memory protection is wrong.");
                    Assert.AreEqual(MemoryProtectionFlags.ReadOnly, change.NewProtection, "The new memory protection is wrong.");
                }
                // Check we can write inside
                try
                {
                    memory.Write(1);
                }
                catch (Exception)
                {
                    Assert.Fail("Unable to write inside the chunk of memory (issue with memory allocation ?).");
                }
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Executes some asm code.
        /// </summary>
        [TestMethod]
        public void Execute()
        {
            Resources.Restart();
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            // Allocate a chunk of memory for testing purpose
            using (var memory = sharp.Memory.Allocate(1))
            {
                // Write some asm inside !
                //    MOV EAX, 0x66
                //    RETN
                memory.Write(new byte[] { 0xB8, 0x66, 0x00, 0x00, 0x00, 0xC3 });

                // Execute the asm code
                var ret = memory.Execute<int>();

                // Assert
                Assert.AreEqual(0x66, ret, "The return value is wrong.");
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Executes asynchronously some asm code.
        /// </summary>
        [TestMethod]
        public void ExecuteAsync()
        {
            Resources.Restart();
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            // Allocate a chunk of memory for testing purpose
            using (var memory = sharp.Memory.Allocate(1))
            {
                // Write some asm inside !
                //    MOV EAX, 0x66
                //    RETN
                memory.Write(new byte[] { 0xB8, 0x66, 0x00, 0x00, 0x00, 0xC3 });

                // Execute the asm code
                var ret = memory.ExecuteAsync<int>();

                // Assert
                Assert.AreEqual(0x66, ret.Result, "The return value is wrong.");
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Writes and reads an integer.
        /// </summary>
        [TestMethod]
        public void WriteAndReadInteger()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            // Allocate a chunk of memory for testing purpose
            using (var memory = sharp.Memory.Allocate(1))
            {
                memory.Write(66);

                // Assert
                Assert.AreEqual(66, memory.Read<int>(), "Error when the memory was writen/read.");
            }

            Resources.EndTests(sharp);
        }
    }
}
