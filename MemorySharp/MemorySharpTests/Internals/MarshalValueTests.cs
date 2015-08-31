/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using Binarysharp.MemoryManagement.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Internals
{
    [TestClass]
    public class MarshalValueTests
    {
        /// <summary>
        /// Marshals an integer.
        /// </summary>
        [TestMethod]
        public void MarshalInteger()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const int value = 1024;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(value, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a float.
        /// </summary>
        [TestMethod]
        public void MarshalFloat()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const float value = 1024f;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(0x44800000, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a boolean.
        /// </summary>
        [TestMethod]
        public void MarshalBoolean()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const bool value = true;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(0x1, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a char.
        /// </summary>
        [TestMethod]
        public void MarshalChar()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const char value = 'A';

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(0x41, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a byte.
        /// </summary>
        [TestMethod]
        public void MarshalByte()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const byte value = 0x90;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(value, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a custom structure.
        /// </summary>
        [TestMethod]
        public void Marshal_CustomStruct()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            var value = Resources.CustomStruct;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {

                // Assert
                Assert.AreEqual(Resources.CustomStruct.X, pointer.Allocated.Read<int>(0));
                Assert.AreEqual(Resources.CustomStruct.Y, pointer.Allocated.Read<int>(4));
                Assert.AreEqual(Resources.CustomStruct.Z, pointer.Allocated.Read<int>(8));
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a string.
        /// </summary>
        [TestMethod]
        public void MarshalString()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const string path = "If you read that, you're *really* like unit tests.";

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, path))
            {
                // Assert
                Assert.AreEqual(path, pointer.Allocated.ReadString(0));
            }

            Resources.EndTests(sharp);
        }

        /// <summary>
        /// Marshals a short.
        /// </summary>
        [TestMethod]
        public void MarshalShort()
        {
            // Arrange
            var sharp = Resources.MemorySharp;
            const short value = 1024;

            // Act
            using (var pointer = MarshalValue.Marshal(sharp, value))
            {
                // Assert
                Assert.AreEqual(value, pointer.Reference.ToInt32());
            }

            Resources.EndTests(sharp);
        }
    }
}
