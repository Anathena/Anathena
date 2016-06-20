namespace Anathema.Source.Engine.Architecture.Disassembler
{
    class DisassemblerFactory
    {

        public static IDisassembler GetDisassembler()
        {
            return new SharpDisassembler();
        }

    } // End class

} // End namespace