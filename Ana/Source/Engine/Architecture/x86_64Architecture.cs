using Ana.Source.Engine.Architecture.Assembler;
using Ana.Source.Engine.Architecture.Disassembler;
using System;

namespace Ana.Source.Engine.Architecture
{
    public class x86_64Architecture : IArchitecture
    {
        public IAssembler Assembler { get; private set; }

        public IDisassembler Disassembler { get; private set; }

        public x86_64Architecture()
        {
            Assembler = AssemblerFactory.GetAssembler(ArchitectureType.x86_64);
            Disassembler = DisassemblerFactory.GetDisassembler(ArchitectureType.x86_64);
        }

        public IAssembler GetAssembler()
        {
            throw new NotImplementedException();
        }

        public IDisassembler GetDisassembler()
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace