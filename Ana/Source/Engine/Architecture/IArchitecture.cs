namespace Ana.Source.Engine.Architecture
{
    using Assembler;
    using Disassembler;

    /// <summary>
    /// An interface defining an object that can assemble and disassemble instructions
    /// </summary>
    public interface IArchitecture
    {
        /// <summary>
        /// Gets an instruction assembler
        /// </summary>
        /// <returns>An object implementing the IAssembler interface</returns>
        IAssembler GetAssembler();

        /// <summary>
        /// Gets an instruction disassembler
        /// </summary>
        /// <returns>An object implementing the IDisassembler interface</returns>
        IDisassembler GetDisassembler();
    }
    //// End architecture
}
//// End namespace