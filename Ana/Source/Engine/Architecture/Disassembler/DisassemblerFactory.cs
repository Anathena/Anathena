namespace Ana.Source.Engine.Architecture.Disassembler
{
    using System;

    /// <summary>
    /// A factory that returns a disassembler based on the system architecture.
    /// </summary>
    internal class DisassemblerFactory
    {
        /// <summary>
        /// Gets a disassembler based on the system architecture.
        /// </summary>
        /// <param name="architectureType">The system architecture.</param>
        /// <returns>An object implementing IDisassembler based on the system architecture.</returns>
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