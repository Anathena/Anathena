namespace Ana.Source.Engine.Architecture.Assembler
{
    using System;

    /// <summary>
    /// Interface defining an assembler.
    /// </summary>
    public interface IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(Boolean isProcess32Bit, String asm);

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="asm">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(Boolean isProcess32Bit, String asm, IntPtr baseAddress);
    }
    //// End interface
}
//// End namespace