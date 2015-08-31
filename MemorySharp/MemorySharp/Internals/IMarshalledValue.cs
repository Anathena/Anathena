/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Memory;

namespace Binarysharp.MemoryManagement.Internals
{
    /// <summary>
    /// Interface representing a value within the memory of a remote process.
    /// </summary>
    public interface IMarshalledValue : IDisposable
    {
        /// <summary>
        /// The memory allocated where the value is fully written if needed. It can be unused.
        /// </summary>
        RemoteAllocation Allocated { get; }
        /// <summary>
        /// The reference of the value. It can be directly the value or a pointer.
        /// </summary>
        IntPtr Reference { get; }
    }
}
