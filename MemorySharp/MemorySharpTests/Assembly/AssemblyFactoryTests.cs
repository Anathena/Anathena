/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using Binarysharp.MemoryManagement.Assembly;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Assembly
{
    [TestClass]
    public class AssemblyFactoryTests
    {
        /// <summary>
        /// Injects some mnemonics.
        /// </summary>
        [TestMethod]
        public void Inject()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            using (var memory = sharp.Memory.Allocate(1))
            {
                sharp.Assembly.Inject(
                    new[]
                        {
                            "push 0",
                            "add esp, 4",
                            "retn"
                        },
                    memory.BaseAddress);

                // Assert
                CollectionAssert.AreEqual(new byte[] { 0x6A, 00, 0x83, 0xC4, 04, 0xC3 }, sharp.Read<byte>(memory.BaseAddress, 6, false));
            }
            
            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Injects some mnemonics using a transaction.
        /// </summary>
        [TestMethod]
        public void TransactionInject()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            using (var memory = sharp.Memory.Allocate(1))
            {
                using (var t = sharp.Assembly.BeginTransaction(memory.BaseAddress, false))
                {
                    t.AddLine("push 0");
                    t.AddLine("add esp, 4");
                    t.AddLine("retn");
                }
                // Assert
                CollectionAssert.AreEqual(new byte[] { 0x6A, 00, 0x83, 0xC4, 04, 0xC3 }, sharp.Read<byte>(memory.BaseAddress, 6, false));

            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Injects and executes some mnemonics.
        /// </summary>
        [TestMethod]
        public void InjectAndExecute()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            var asm = new[]
                {
                    "use32",
                    "mov eax, 66",
                    "retn"
                };

            // Act
            var ret = sharp.Assembly.InjectAndExecute<int>(asm);

            // Assert
            Assert.AreEqual(66, ret, "The return value is incorrect.");
            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Injects and executes some mnemonics with a transaction.
        /// </summary>
        [TestMethod]
        public void InjectAndExecuteWithTransaction()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            AssemblyTransaction t;

            // Act
            using (t = sharp.Assembly.BeginTransaction())
            {
                t.AddLine("use32");
                t.AddLine("mov eax, 66");
                t.AddLine("retn");
            }

            // Assert
            Assert.AreEqual(66, t.GetExitCode<int>(), "The return value is incorrect.");
            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Writes some mnemonics and execute them.
        /// </summary>
        [TestMethod]
        public void Execute()
        {
            // Arrange
            var sharp = Resources.MemorySharp;

            // Act
            using (var memory = sharp.Memory.Allocate(1))
            {
                using (var t = sharp.Assembly.BeginTransaction(memory.BaseAddress))
                {
                    t.AddLine("use32");
                    t.AddLine("mov eax, 66");
                    t.AddLine("retn");
                }

                var ret = memory.Execute<int>();

                // Assert
                Assert.AreEqual(66, ret, "The return value is incorrect.");
            }

            Resources.EndTests(sharp);
        }
    }
}
