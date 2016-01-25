using System;
using System.Linq;
using Binarysharp.Assemblers.Fasm;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using FASMSharedInterface;

namespace FASMHelper
{
    class Program
    {
        static void Main(String[] args)
        {
            // IPC port name
            IpcChannel IpcChannel = new IpcChannel("FASMChannel");
            ChannelServices.RegisterChannel(IpcChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FASMAssembler), "FASMObj", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Anathema FASM helper service to assemble x86/x64 instructions.");
            Console.ReadLine();
        }
        
        public class FASMAssembler : MarshalByRefObject, ISharedAssemblyInterface
        {
            public FASMAssembler() {  }

            public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, Int64 BaseAddress)
            {
                // Add header information about process
                Assembly = String.Format( (IsProcess32Bit ? "use32\n" : "use64\n") + "org 0x{0:X8}\n", BaseAddress) + Assembly;

                // Print fully assembly to console
                Console.WriteLine(Assembly);

                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                Byte[] Result = FasmNet.Assemble(Assembly);

                // Print bytes to console
                Result.ToList().ForEach(x => Console.Write(x.ToString() + " "));

                return Result;
            }
        }

    } // End class

} // End namespace