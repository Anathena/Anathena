/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

namespace Binarysharp.MemoryManagement.Threading
{
    #region SegmentRegisters
    /// <summary>
    /// List of segment registers.
    /// </summary>
    public enum SegmentRegisters
    {
        /// <summary>
        /// The code segment.
        /// </summary>
        Cs,
        /// <summary>
        /// The Data segment.
        /// </summary>
        Ds,
        /// <summary>
        /// The extra data segment.
        /// </summary>
        Es,
        /// <summary>
        /// The points to Thread Information Block (TIB).
        /// </summary>
        Fs,
        /// <summary>
        /// The extra data segment.
        /// </summary>
        Gs,
        /// <summary>
        /// The stack segment.
        /// </summary>
        Ss
    }
    #endregion
}
