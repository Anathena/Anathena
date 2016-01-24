using System;
using System.Linq;
using Binarysharp.Assemblers.Fasm;

namespace FASMHelper
{
    class Program
    {
        static void Main(String[] args)
        {
            var mnemonics = new[]
                {
                    "use64",
                    "push rax"
                };

            FasmNet.Assemble(mnemonics).ToList().ForEach(x => Console.Write(x.ToString() + " "));

            IntPtr Llo = IntPtr.Zero;
            

            Console.Read();
        }

    } // End class

} // End namespace