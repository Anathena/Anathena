using Anathema.Source.Engine.Architecture.Assembler;
using Anathema.Source.Engine.Architecture.Disassembler;

namespace Anathema.Source.Engine.Architecture
{
    public class ArchitectureInterface
    {
        public IAssembler Assembler { get; private set; }
        public IDisassembler Disassembler { get; private set; }

        public ArchitectureInterface()
        {
            Assembler = new Fasm32Assembler();
            Disassembler = new SharpDisassembler();
        }

    } // End interface

} // End namespace