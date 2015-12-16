/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

namespace Binarysharp.MemoryManagement.Internals
{
    /// <summary>
    /// Defines an element able to be activated in the remote process.
    /// </summary>
    public interface IApplicableElement : IDisposableState
    {
        /// <summary>
        /// States if the element is enabled.
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// Disables the element.
        /// </summary>
        void Disable();
        /// <summary>
        /// Enables the element.
        /// </summary>
        void Enable();
    }
}
