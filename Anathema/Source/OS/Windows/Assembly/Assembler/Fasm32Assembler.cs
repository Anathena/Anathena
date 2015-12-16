/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using Binarysharp.Assemblers.Fasm;
using System;

namespace Binarysharp.MemoryManagement.Assembly.Assembler
{
    /// <summary>
    /// Implement Fasm.NET compiler for 32-bit development.
    /// More info: https://github.com/ZenLulz/Fasm.NET
    /// </summary>
    public class Fasm32Assembler : IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public byte[] Assemble(string asm)
        {
            // Assemble and return the code
            return Assemble(asm, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public byte[] Assemble(string asm, IntPtr baseAddress)
        {
            // Rebase the code
            asm = String.Format("use32\norg 0x{0:X8}\n", baseAddress.ToInt64()) + asm;
            // Assemble and return the code
            return FasmNet.Assemble(asm);
        }
    }
}
