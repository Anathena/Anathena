/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;

namespace Binarysharp.MemoryManagement.Assembly.Assembler
{
    /// <summary>
    /// Interface defining an assembler.
    /// </summary>
    public interface IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="Asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(String Asm);
        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="Asm">The assembly code.</param>
        /// <param name="BaseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(String Asm, IntPtr BaseAddress);
    }
}
