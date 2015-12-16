/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

namespace Binarysharp.MemoryManagement.Helpers
{
    /// <summary>
    /// Static helper used to create or get a singleton from another class.
    /// </summary>
    /// <typeparam name="T">The type to create or get a singleton.</typeparam>
    public static class Singleton<T> where T : new()
    {
        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        public static readonly T Instance = new T();
    }
}
