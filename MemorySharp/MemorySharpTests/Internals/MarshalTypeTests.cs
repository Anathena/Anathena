/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemorySharpTests.Internals
{
    [TestClass]
    public class MarshalTypeTests
    {
        /// <summary>
        /// Mashals a double object into an array of bytes.
        /// </summary>
        [TestMethod]
        public void ToByteArrayDoubleType()
        {
            // Arrange
            const double originalValue = 3.141592;
            var valueInBytes = BitConverter.GetBytes(originalValue);

            // Act
            var ret = MarshalType<double>.ObjectToByteArray(originalValue);

            // Assert
            CollectionAssert.AreEqual(valueInBytes, ret, "Both variables aren't equal.");
        }

        /// <summary>
        /// Marshals a Point structure in and out a block of unmanaged memory.
        /// </summary>
        [TestMethod]
        public void ToManagedObjectPointStruct()
        {
            // Arrange
            var customStruct = Resources.CustomStruct;

            // Act
            var byteArray = MarshalType<Point>.ObjectToByteArray(customStruct);
            var customStruct2 = MarshalType<Point>.ByteArrayToObject(byteArray);

            // Assert
            Assert.AreEqual(customStruct, customStruct2, "Both structures are not equal.");
        }

        /// <summary>
        /// Converts a pointer to a long type.
        /// </summary>
        [TestMethod]
        public void PtrToObjectLong()
        {
            // Arrange
            var ptr = new IntPtr(32);

            // Act
            var value = MarshalType<long>.PtrToObject(Resources.MemorySharp, ptr);

            // Assert
            Assert.AreEqual(32, value);
        }

        /// <summary>
        /// Converts a pointer to a structure.
        /// </summary>
        [TestMethod]
        public void PtrToObjectString()
        {
            // Arrange
            var point = Resources.CustomStruct;
            var sharp = Resources.MemorySharp;

            // Act
            using (var memory = sharp.Memory.Allocate(MarshalType<Point>.Size))
            {
                memory.Write(point);
                var ret = MarshalType<Point>.PtrToObject(sharp, memory.BaseAddress);

                // Assert
                Assert.AreEqual(point, ret);
            }
        }
    }
}
