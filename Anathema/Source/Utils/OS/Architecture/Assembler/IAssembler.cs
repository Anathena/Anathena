using System;

namespace Anathema.Utils.OS.Architecture.Assembler
{
    /// <summary>
    /// Interface defining an assembler.
    /// </summary>
    public interface IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="Asm">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(Boolean IsProcess32Bit, String Asm);
        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="Asm">The assembly code.</param>
        /// <param name="BaseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        Byte[] Assemble(Boolean IsProcess32Bit, String Asm, IntPtr BaseAddress);

    } // End interface

} // End namespace