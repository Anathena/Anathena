using Anathema.Source.Engine.Proxy;
using Anathema.Source.Utils.Extensions;
using System;

namespace Anathema.Source.Engine.Architecture.Assembler
{
    public class Fasm32Assembler : IAssembler
    {
        /// <summary>
        /// Assemble the specified assembly code.
        /// </summary>
        /// <param name="Assembly">The assembly code.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly)
        {
            // Assemble and return the code
            return Assemble(IsProcess32Bit, Assembly, IntPtr.Zero);
        }

        /// <summary>
        /// Assemble the specified assembly code at a base address.
        /// </summary>
        /// <param name="Assembly">The assembly code.</param>
        /// <param name="BaseAddress">The address where the code is rebased.</param>
        /// <returns>An array of bytes containing the assembly code.</returns>
        public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, IntPtr BaseAddress)
        {
            // Call proxy service, which simply passes the asm code to Fasm.net to assemble the instructions
            return ProxyCommunicator.GetInstance().GetFasmService().Assemble(IsProcess32Bit, Assembly, BaseAddress.ToUInt64());
        }

    } // End class

} // End namespace