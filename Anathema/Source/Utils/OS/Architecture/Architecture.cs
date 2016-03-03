using Anathema.Utils.OS.Architecture.Assembler;
using Anathema.Utils.OS.Architecture.Disassembler;

namespace Anathema.Utils.OS.Architecture
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