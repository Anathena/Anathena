using AnathenaProxy;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;
using System.Windows.Forms;

namespace Anathema.Source.Engine.Proxy
{
    class ProxyCommunicator
    {
        private static Lazy<ProxyCommunicator> ProxyCommunicatorInstance = new Lazy<ProxyCommunicator>(() => { return new ProxyCommunicator(); }, LazyThreadSafetyMode.PublicationOnly);

        private const String AnathenaProxy32Executable = "AnathenaProxy32.exe";
        private const String AnathenaProxy64Executable = "AnathenaProxy64.exe";

        private String AnathenaProxy32ChannelClient;
        private String AnathenaProxy32ChannelServer;
        private String AnathenaProxy64ChannelClient;
        private String AnathenaProxy64ChannelServer;

        private ProxyCommunicator() { }

        public static ProxyCommunicator GetInstance()
        {
            return ProxyCommunicatorInstance.Value;
        }

        public void InitializeServices()
        {
            // Initialize channel names
            AnathenaProxy32ChannelClient = Guid.NewGuid().ToString();
            AnathenaProxy32ChannelServer = Guid.NewGuid().ToString();
            AnathenaProxy64ChannelClient = Guid.NewGuid().ToString();
            AnathenaProxy64ChannelServer = Guid.NewGuid().ToString();

            // Start 32 and 64 bit proxy services
            StartProxyService(AnathenaProxy32Executable, AnathenaProxy32ChannelClient, AnathenaProxy32ChannelServer);
            // StartProxyService(AnathenaProxy64Executable, AnathenaProxy64ChannelClient, AnathenaProxy64ChannelServer);
        }

        private void StartProxyService(String ExecutableName, String ChannelNameClient, String ChannelNameServer)
        {
            EventWaitHandle ProcessStartEvent = new EventWaitHandle(false, EventResetMode.ManualReset, @"Global\Anathena");
            ProcessStartInfo ProcessInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ExecutableName));
            ProcessInfo.Arguments = ChannelNameServer;
            // ProcessInfo.RedirectStandardInput = true;
            ProcessInfo.UseShellExecute = false;
            // ProcessInfo.CreateNoWindow = true;
            Process.Start(ProcessInfo);
            ProcessStartEvent.WaitOne();

            // Create client connection to service
            IpcChannel IpcChannel = new IpcChannel(ChannelNameClient);
            ChannelServices.RegisterChannel(IpcChannel, true);
        }

        public IFasmServiceInterface GetFasmService()
        {
            // Fasm service exclusively runs on the 32 bit executable, this library has no 64 bit version
            String ObjectUri = String.Format("ipc://{0}/{1}", AnathenaProxy32ChannelServer, typeof(FasmService).Name);
            return (IFasmServiceInterface)Activator.GetObject(typeof(IFasmServiceInterface), ObjectUri);
        }

        public ISharedAssemblyInterface GetFasmWhatever()
        {
            // Fasm service exclusively runs on the 32 bit executable, this library has no 64 bit version
            String ObjectUri = String.Format("ipc://{0}/{1}", AnathenaProxy32ChannelServer, typeof(FASMAssembler).Name);
            return (ISharedAssemblyInterface)Activator.GetObject(typeof(ISharedAssemblyInterface), ObjectUri);
        }

        public IClrServiceInterface GetClrService(Boolean Is32Bit)
        {
            if (Is32Bit)
            {
                String ObjectUri = String.Format("ipc://{0}/{1}", AnathenaProxy32ChannelServer, typeof(ClrService).Name);
                return (IClrServiceInterface)Activator.GetObject(typeof(IClrServiceInterface), ObjectUri);
            }
            else
            {
                String ObjectUri = String.Format("ipc://{0}/{1}", AnathenaProxy64ChannelServer, typeof(ClrService).Name);
                return (IClrServiceInterface)Activator.GetObject(typeof(IClrServiceInterface), ObjectUri);
            }
        }

    } // End class

} // End namespace