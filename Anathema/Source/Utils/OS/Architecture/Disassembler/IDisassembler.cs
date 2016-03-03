using SharpDisasm;
using System;
using System.Collections.Generic;

namespace Anathema.Utils.OS.Architecture.Disassembler
{
    /// <summary>
    /// Interface defining a disassembler.
    /// </summary>
    public interface IDisassembler
    {
        /// <summary>
        /// Disassemble the specified assembly code.
        /// </summary>
        /// <param name="Bytes">The raw bytes.</param>
        /// <returns>A string containing the assembly.</returns>
        List<Instruction> Disassemble(Byte[] Bytes, Boolean Architecture32Bit, IntPtr Address);

    } // End class

} // End namespace