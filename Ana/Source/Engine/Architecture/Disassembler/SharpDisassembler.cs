namespace Ana.Source.Engine.Architecture.Disassembler
{
    using SharpDisasm;
    using System;
    using System.Collections.Generic;
    using Utils.Extensions;

    /// <summary>
    /// Implements Fasm.NET compiler for 32-bit development.
    /// More info: https://github.com/ZenLulz/Fasm.NET
    /// </summary>
    public class SharpDisassembler : IDisassembler
    {
        private Disassembler Disassembler;

        public List<Instruction> Disassemble(Byte[] Bytes, Boolean Architecture32Bit, IntPtr Address)
        {
            Disassembler = new Disassembler(Bytes, Architecture32Bit ? ArchitectureMode.x86_32 : ArchitectureMode.x86_64, Address.ToUInt64());
            return new List<Instruction>(Disassembler.Disassemble());
        }

    } // End class

} // End namespace