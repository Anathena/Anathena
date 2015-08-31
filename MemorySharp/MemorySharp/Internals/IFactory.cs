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
    /// Define a factory for the library.
    /// </summary>
    /// <remarks>At the moment, the factories are just disposable.</remarks>
    public interface IFactory : IDisposable
    {
    }
}