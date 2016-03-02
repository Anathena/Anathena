using Anathema.Disassembler;
using Anathema.MemoryManagement.Assembly.Assembler;

namespace Anathema
{
    public class Architecture
    {
        public IAssembler Assembler { get; private set; }
        public IDisassembler Disassembler { get; private set; }

        public Architecture()
        {
            Assembler = new Fasm32Assembler();
            Disassembler = new SharpDisassembler();
        }

    } // End interface

} // End namespace