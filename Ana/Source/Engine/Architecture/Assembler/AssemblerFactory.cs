namespace Ana.Source.Engine.Architecture.Assembler
{
    using System;

    /// <summary>
    /// A factory that returns an assembler based on the system architecture
    /// </summary>
    class AssemblerFactory
    {
        /// <summary>
        /// Gets an assembler based on the system architecture
        /// </summary>
        /// <param name="architectureType"></param>
        /// <returns>An object implementing IAssembler based on the system architecture</returns>
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