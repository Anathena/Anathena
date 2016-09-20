namespace Ana.Source.Engine.Architecture.Disassembler
{
    using System;

    class DisassemblerFactory
    {
        public static IDisassembler GetDisassembler(ArchitectureType architectureType)
        {
            switch (architectureType)
            {
                case ArchitectureType.x86_64:
                    return new SharpDisassembler();
                default:
                    throw new Exception("Assembler not supported for specified architecture");
            }
        }
    }
    //// End class
}
//// End namespace