using Anathema.Assemblers.Fasm;
using System;

namespace AnathenaProxy
{
    public class FasmService : MarshalByRefObject, IFasmServiceInterface
    {
        public FasmService() { }

        public Byte[] Assemble(String Assembly)
        {
            Byte[] Result;
            try
            {
                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                Result = FasmNet.Assemble(Assembly);
            }
            catch
            {
                Result = null;
            }
            return Result;
        }

    } // End class

    public class FASMAssembler : MarshalByRefObject, ISharedAssemblyInterface
    {
        public FASMAssembler() { }

        public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress)
        {
            if (Assembly == null)
                return null;

            // Add header information about process
            if (IsProcess32Bit)
                Assembly = String.Format("use32\n" + "org 0x{0:X8}\n", BaseAddress) + Assembly;
            else
                Assembly = String.Format("use64\n" + "org 0x{0:X16}\n", BaseAddress) + Assembly;

            // Print fully assembly to console
            Console.WriteLine("\n" + Assembly + "\n");

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