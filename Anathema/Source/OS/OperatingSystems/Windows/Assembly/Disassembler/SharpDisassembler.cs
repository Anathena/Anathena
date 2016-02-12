using SharpDisasm;
using System;
using System.Collections.Generic;

namespace Anathema.MemoryManagement.Assembly.Disassembler
{
    /// <summary>
    /// Implement Fasm.NET compiler for 32-bit development.
    /// More info: https://github.com/ZenLulz/Fasm.NET
    /// </summary>
    public class SharpDisassembler : IDisassembler
    {
        private SharpDisasm.Disassembler Disassembler;

        public List<Instruction> Disassemble(Byte[] Bytes, Boolean Architecture32Bit, UInt64 Address)
        {
            Disassembler = new SharpDisasm.Disassembler(Bytes, Architecture32Bit ? SharpDisasm.ArchitectureMode.x86_32 : SharpDisasm.ArchitectureMode.x86_64, Address);
            return new List<Instruction>(Disassembler.Disassemble());
        }

    } // End class

} // End namespace