using System;

namespace Anathema
{
    public class Architecture
    {
        public FUTUREIAssembler Assembler { get; private set; }
        public FUTUREIDisassembler Disassembler { get; private set; }

        public Architecture()
        {
            // Assembler = new FASM();
            // Disassembler = new SharpDisasm();
        }

    } // End interface

} // End namespace