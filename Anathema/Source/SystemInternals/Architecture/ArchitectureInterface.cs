using Anathema.Source.SystemInternals.Architecture.Assembler;
using Anathema.Source.SystemInternals.Architecture.Disassembler;

namespace Anathema.Source.SystemInternals.Architecture
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