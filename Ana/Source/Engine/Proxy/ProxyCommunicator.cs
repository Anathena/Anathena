namespace Ana.Source.Engine.Proxy
{
    using AnathenaProxy;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Communicates with proxy services. These issue commands that require 32 or 64 bit specifically.
    /// </summary>
    internal class ProxyCommunicator
    {
        private const String AnathenaProxy32Executable = "AnathenaProxy32.exe";

        private const String AnathenaProxy64Executable = "AnathenaProxy64.exe";

        private const String WaitEventName = @"Global\Anathena";

        private const String UriPrefix = "net.pipe://localhost/";

        private static Lazy<ProxyCommunicator> proxyCommunicatorInstance = new Lazy<ProxyCommunicator>(
            () => { return new ProxyCommunicator(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="ProxyCommunicator" /> class from being created
        /// </summary>
        private ProxyCommunicator()
        {
        }

        private IProxyService AnathenaProxy32 { get; set; }

        private IProxyService AnathenaProxy64 { get; set; }

        public static ProxyCommunicator GetInstance()
        {
            return proxyCommunicatorInstance.Value;
        }

        public void InitializeServices()
        {
            // Initialize channel names
            String anathenaProxy32ServerName = ProxyCommunicator.UriPrefix + Guid.NewGuid().ToString();
            String anathenaProxy64ServerName = ProxyCommunicator.UriPrefix + Guid.NewGuid().ToString();

            // Start 32 and 64 bit proxy services
            this.AnathenaProxy32 = this.StartProxyService(ProxyCommunicator.AnathenaProxy32Executable, anathenaProxy32ServerName);
            this.AnathenaProxy64 = this.StartProxyService(ProxyCommunicator.AnathenaProxy64Executable, anathenaProxy64ServerName);
        }

        public IProxyService GetProxyService(Boolean is32Bit)
        {
            if (is32Bit)
            {
                return this.AnathenaProxy32;
            }
            else
            {
                return this.AnathenaProxy64;
            }
        }

        private IProxyService StartProxyService(String executableName, String channelServerName)
        {
            // Start the proxy service
            EventWaitHandle processStartEvent = new EventWaitHandle(false, EventResetMode.ManualReset, ProxyCommunicator.WaitEventName);
            ProcessStartInfo processInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), executableName));
            processInfo.Arguments = Process.GetCurrentProcess().Id.ToString() + " " + channelServerName + " " + ProxyCommunicator.WaitEventName;
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            Process.Start(processInfo);
            processStartEvent.WaitOne();

            // Create connection
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;

            EndpointAddress endpoint = new EndpointAddress(channelServerName);
            IProxyService proxyService = ChannelFactory<IProxyService>.CreateChannel(binding, endpoint);

            return proxyService;
        }
    }
    //// End class
}
//// End namespace