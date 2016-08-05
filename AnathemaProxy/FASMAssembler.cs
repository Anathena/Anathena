using Anathema.Assemblers.Fasm;
using System;

namespace AnathemaProxy
{
    public class FASMAssembler : MarshalByRefObject//, ISharedAssemblyInterface
    {
        public FASMAssembler() { }

        public Byte[] Assemble(String Assembly)
        {
            Byte[] Result;
            try
            {
                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                Result = FasmNet.Assemble(Assembly);

                // Print bytes to console
                Array.ForEach(Result, (X => Console.Write(X.ToString() + " ")));
            }
            catch
            {
                Result = null;
            }
            return Result;
        }

    } // End class

} // End namespace