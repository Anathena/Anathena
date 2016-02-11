using System;

namespace Anathema
{
    public class Architecture
    {
        public IAssembler Assembler { get; private set; }
        public IDisassembler Disassembler { get; private set; }

        public Architecture()
        {
            // Assembler = new FASM();
            // Disassembler = new SharpDisasm();
        }

    } // End interface

} // End namespace