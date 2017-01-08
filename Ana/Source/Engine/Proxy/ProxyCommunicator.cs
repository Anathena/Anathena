namespace Ana.Source.Engine.Proxy
{
    using AnathenaProxy;
    using Output;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.ServiceModel;
    using System.Threading;

    /// <summary>
    /// Communicates with proxy services. These issue commands that require 32 or 64 bit specifically.
    /// </summary>
    internal class ProxyCommunicator
    {
        /// <summary>
        /// The 32 bit proxy service executable
        /// </summary>
        private const String AnathenaProxy32Executable = "AnathenaProxy32.exe";

        /// <summary>
        /// The 64 bit proxy service executable
        /// </summary>
        private const String AnathenaProxy64Executable = "AnathenaProxy64.exe";

        /// <summary>
        /// The event name for a wait event, which allows us to wait for a proxy service to start
        /// </summary>
        private const String WaitEventName = @"Global\Anathena";

        /// <summary>
        /// Uri prefix for IPC channel names
        /// </summary>
        private const String UriPrefix = "net.pipe://localhost/";

        /// <summary>
        /// Singleton instance of the <see cref="ProxyCommunicator" /> class
        /// </summary>
        private static Lazy<ProxyCommunicator> proxyCommunicatorInstance = new Lazy<ProxyCommunicator>(
            () => { return new ProxyCommunicator(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="ProxyCommunicator" /> class from being created
        /// </summary>
        private ProxyCommunicator()
        {
            // Initialize channel names
            String anathenaProxy32ServerName = ProxyCommunicator.UriPrefix + Guid.NewGuid().ToString();
            String anathenaProxy64ServerName = ProxyCommunicator.UriPrefix + Guid.NewGuid().ToString();

            // Start 32 and 64 bit proxy services
            this.AnathenaProxy32 = this.StartProxyService(ProxyCommunicator.AnathenaProxy32Executable, anathenaProxy32ServerName);
            this.AnathenaProxy64 = this.StartProxyService(ProxyCommunicator.AnathenaProxy64Executable, anathenaProxy64ServerName);
        }

        /// <summary>
        /// Gets or sets the 32 bit proxy service
        /// </summary>
        private IProxyService AnathenaProxy32 { get; set; }

        /// <summary>
        /// Gets or sets the 64 bit proxy service
        /// </summary>
        private IProxyService AnathenaProxy64 { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProxyCommunicator"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ProxyCommunicator GetInstance()
        {
            return proxyCommunicatorInstance.Value;
        }

        /// <summary>
        /// Gets the proxy service based on provided parameters
        /// </summary>
        /// <param name="is32Bit">Whether or not to get the 32 or 64 bit service</param>
        /// <returns>The 32 or 64 bit service</returns>
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

        /// <summary>
        /// Starts a proxy service
        /// </summary>
        /// <param name="executableName">The executable name of the service to start</param>
        /// <param name="channelServerName">The channel name for IPC</param>
        /// <returns>The proxy service that is created</returns>
        private IProxyService StartProxyService(String executableName, String channelServerName)
        {
            try
            {
                // Start the proxy service
                EventWaitHandle processStartEvent = new EventWaitHandle(false, EventResetMode.ManualReset, ProxyCommunicator.WaitEventName);
                ProcessStartInfo processInfo = new ProcessStartInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), executableName));
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

                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Started proxy service: " + executableName + " over channel " + channelServerName);

                return proxyService;
            }
            catch
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Fatal, "Failed to start proxy service: " + executableName + ". This may impact Scripts and .NET explorer");
                return null;
            }
        }
    }
    //// End class
}
//// End namespace