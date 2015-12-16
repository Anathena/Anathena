/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;

namespace Binarysharp.MemoryManagement.Internals
{
    /// <summary>
    /// Defines an IDisposable interface with a known state.
    /// </summary>
    public interface IDisposableState : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the element is disposed.
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// Gets a value indicating whether the element must be disposed when the Garbage Collector collects the object.
        /// </summary>
        bool MustBeDisposed { get; }
    }
}
