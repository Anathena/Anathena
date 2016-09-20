namespace Ana.Source.Engine.Architecture.Assembler
{
    using System;

    class AssemblerFactory
    {
        public static IAssembler GetAssembler(ArchitectureType architectureType)
        {
            switch (architectureType)
            {
                case ArchitectureType.x86_64:
                    return new Fasm32Assembler();
                default:
                    throw new Exception("Assembler not supported for specified architecture");
            }
        }
    }
    //// End class
}
//// End namespace