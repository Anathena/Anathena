namespace Ana.Source.Engine.Architecture
{
    using Assembler;
    using Disassembler;

    /// <summary>
    /// A class containing an x86/64 assembler and disassembler
    /// </summary>
    internal class Architecture86_64 : IArchitecture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Architecture86_64" /> class
        /// </summary>
        public Architecture86_64()
        {
            this.Assembler = AssemblerFactory.GetAssembler(ArchitectureType.x86_64);
            this.Disassembler = DisassemblerFactory.GetDisassembler(ArchitectureType.x86_64);
        }

        /// <summary>
        /// Gets an x86/x64 assembler
        /// </summary>
        public IAssembler Assembler { get; private set; }

        /// <summary>
        /// Gets an x86/x64 disassembler
        /// </summary>
        public IDisassembler Disassembler { get; private set; }

        /// <summary>
        /// Gets an x86/x64 instruction assembler
        /// </summary>
        /// <returns>An x86/x64 assembler</returns>
        public IAssembler GetAssembler()
        {
            return this.Assembler;
        }

        /// <summary>
        /// Gets an x86/x64 instruction disassembler
        /// </summary>
        /// <returns>An x86/x64 disassembler</returns>
        public IDisassembler GetDisassembler()
        {
            return this.Disassembler;
        }
    }
    //// End class
}
//// End namespace