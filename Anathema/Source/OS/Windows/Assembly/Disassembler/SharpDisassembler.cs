/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using SharpDisasm;
using System;
using System.Collections.Generic;

namespace Binarysharp.MemoryManagement.Assembly.Disassembler
{
    /// <summary>
    /// Implement Fasm.NET compiler for 32-bit development.
    /// More info: https://github.com/ZenLulz/Fasm.NET
    /// </summary>
    public class SharpDisassembler : IDisassembler
    {
        private SharpDisasm.Disassembler Disassembler;

        public List<Instruction> Disassemble(byte[] bytes, bool IsProcess32Bit, UInt64 Address)
        {
            Disassembler = new SharpDisasm.Disassembler(bytes, IsProcess32Bit ? SharpDisasm.ArchitectureMode.x86_32 : SharpDisasm.ArchitectureMode.x86_64, Address);
            return new List<Instruction>(Disassembler.Disassemble());
        }

    } // End class

} // End namespace