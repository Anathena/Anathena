namespace Ana.Source.Engine.Architecture.Assembler
{
    using Proxy;
    using System;
    using Utils.Extensions;

    public class Fasm32Assembler : IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="assembly">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(Boolean isProcess32Bit, String assembly)
        {
            // Assemble and return the code
            return Assemble(isProcess32Bit, assembly, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="assembly">The assembly code.</param>
        /// <param name="baseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(Boolean isProcess32Bit, String assembly, IntPtr baseAddress)
        {
            // Call proxy service, which simply passes the asm code to Fasm.net to assemble the instructions
            // return ProxyCommunicator.GetInstance().GetFasmService().Assemble(IsProcess32Bit, Assembly, BaseAddress.ToUInt64());
            return ProxyCommunicator.GetInstance().GetProxyService(isProcess32Bit).Assemble(isProcess32Bit, assembly, baseAddress.ToUInt64());
        }
    }
    //// End class
}
//// End namespace