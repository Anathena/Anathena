using AnathenaProxy;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;

namespace Anathema.Source.Engine.Proxy
{
    class ProxyCommunicator
    {
        private static Lazy<ProxyCommunicator> ProxyCommunicatorInstance = new Lazy<ProxyCommunicator>(() => { return new ProxyCommunicator(); }, LazyThreadSafetyMode.PublicationOnly);

        private const String AnathenaProxy32Executable = "AnathenaProxy32.exe";
        private const String AnathenaProxy64Executable = "AnathenaProxy64.exe";
        private const String WaitEventName = @"Global\Anathena";
        private const String UriPrefix = "net.pipe://localhost/";

        private IProxyService AnathenaProxy32;
        private IProxyService AnathenaProxy64;

        private ProxyCommunicator() { }

        public static ProxyCommunicator GetInstance()
        {
            return ProxyCommunicatorInstance.Value;
        }

        public void InitializeServices()
        {
            // Initialize channel names
            String AnathenaProxy32ServerName = UriPrefix + Guid.NewGuid().ToString();
            String AnathenaProxy64ServerName = UriPrefix + Guid.NewGuid().ToString();

            // Start 32 and 64 bit proxy services
            AnathenaProxy32 = StartProxyService(AnathenaProxy32Executable, AnathenaProxy32ServerName);
            AnathenaProxy64 = StartProxyService(AnathenaProxy64Executable, AnathenaProxy64ServerName);
        }

        private IProxyService StartProxyService(String ExecutableName, String ChannelServerName)
        {
            // Start the proxy service
            EventWaitHandle ProcessStartEvent = new EventWaitHandle(false, EventResetMode.ManualReset, WaitEventName);
            ProcessStartInfo ProcessInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ExecutableName));
            ProcessInfo.Arguments = Process.GetCurrentProcess().Id.ToString() + " " + ChannelServerName + " " + WaitEventName;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.CreateNoWindow = true;
            Process.Start(ProcessInfo);
            ProcessStartEvent.WaitOne();

            // Create connection
            NetNamedPipeBinding Binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress Endpoint = new EndpointAddress(ChannelServerName);
            IProxyService ProxyService = ChannelFactory<IProxyService>.CreateChannel(Binding, Endpoint);

            return ProxyService;
        }

        public IProxyService GetProxyService(Boolean Is32Bit)
        {
            if (Is32Bit)
                return AnathenaProxy32;
            else
                return AnathenaProxy64;
        }

    } // End class

} // End namespace