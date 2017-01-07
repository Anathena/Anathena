﻿namespace Ana.Source.Engine.Architecture.Assembler
{
    using System;

    /// <summary>
    /// Interface defining an assembler
    /// </summary>
    internal interface IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code
        /// </summary>
        /// <param name="isProcess32Bit">Whether or not the assembly is in the context of a 32 bit program</param>
        /// <param name="assembly">The assembly code</param>
        /// <returns>An array of bytes containing the assembly code</returns>
        Byte[] Assemble(Boolean isProcess32Bit, String assembly, out String logs);

        /// <summary>
        /// Assemble the specified assembly code at a base address
        /// </summary>
        /// <param name="isProcess32Bit">Whether or not the assembly is in the context of a 32 bit program</param>
        /// <param name="assembly">The assembly code</param>
        /// <param name="baseAddress">The address where the code is rebased</param>
        /// <returns>An array of bytes containing the assembly code</returns>
        Byte[] Assemble(Boolean isProcess32Bit, String assembly, IntPtr baseAddress, out String logs);
    }
    //// End interface
}
//// End namespace