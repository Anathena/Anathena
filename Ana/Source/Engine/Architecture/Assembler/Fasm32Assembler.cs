namespace Ana.Source.Engine.Architecture.Assembler
{
    using Proxy;
    using System;
    using Utils.Extensions;

    /// <summary>
    /// The Fasm assembler for x86/64
    /// </summary>
    internal class Fasm32Assembler : IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code
        /// </summary>
        /// <param name="isProcess32Bit">Whether or not the assembly is in the context of a 32 bit program</param>
        /// <param name="assembly">The assembly code</param>
        /// <returns>An array of bytes containing the assembly code</returns>
        public Byte[] Assemble(Boolean isProcess32Bit, String assembly)
        {
            // Assemble and return the code
            return this.Assemble(isProcess32Bit, assembly, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address
        /// </summary>
        /// <param name="isProcess32Bit">Whether or not the assembly is in the context of a 32 bit program</param>
        /// <param name="assembly">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(Boolean isProcess32Bit, String assembly, IntPtr baseAddress)
        {
            // Call proxy service, which simply passes the asm code to Fasm.net to assemble the instructions
            return ProxyCommunicator.GetInstance().GetProxyService(isProcess32Bit).Assemble(isProcess32Bit, assembly, baseAddress.ToUInt64());
        }
    }
    //// End class
}
//// End namespace