using System;
using System.Linq;
using Binarysharp.Assemblers.Fasm;
using System.Runtime.Remoting.Channels.Ipc;    //Importing IPC
                                               //channel
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace FASMHelper
{
    class Program
    {
        static void Main(String[] args)
        {
            // IPC port name
            IpcChannel IpcChannel = new IpcChannel("IPChannelName");
            ChannelServices.RegisterChannel(IpcChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(MyRemoteObject), "SreeniRemoteObj", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Please enter to stop the server");
            Console.ReadLine();
        }
        
        public interface ISharedAssemblyInterface
        {
            Int32 Addition(Int32 a, Int32 b);
            Int32 Multipliation(Int32 a, Int32 b);
        }

        public class MyRemoteObject : MarshalByRefObject, ISharedAssemblyInterface
        {
            public MyRemoteObject()
            {
            /*
                String[] mnemonics = new String[]
                {
                    "use64",
                    "push rax"
                };
                FasmNet.Assemble(mnemonics).ToList().ForEach(x => Console.Write(x.ToString() + " "));
            */
            }

            public Int32 Addition(Int32 a, Int32 b)
            {
                return a + b;
            }

            public int Multipliation(Int32 a, Int32 b)
            {
                return a * b;
            }
        }

    } // End class

} // End namespace