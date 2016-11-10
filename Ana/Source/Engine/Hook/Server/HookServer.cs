namespace Ana.Source.Engine.Hook.Server
{
    using Client;
    using EasyHook;
    using Graphics.DirectX.Interface;
    using Graphics.DirectX.Interface.DX11;
    using Graphics.DirectX.Interface.DX9;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Ipc;
    using System.Runtime.Serialization.Formatters;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Entry point for a hook in the target process. Automatically loads when RemoteHooking.Inject() is called.
    /// </summary>
    public class HookServer : IEntryPoint
    {
        public HookServer(RemoteHooking.IContext context, String channelName, String projectDirectory)
        {
            this.IpcServerChannel = null;
            this.DirectXHook = null;

            // Get reference to IPC to host application
            this.HookClient = RemoteHooking.IpcConnectClient<HookClient>(channelName);

            // We try to ping immediately, if it fails then injection fails
            this.HookClient.Ping();

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            IDictionary properties = new Hashtable();
            properties["name"] = channelName;
            properties["portName"] = channelName + Guid.NewGuid().ToString("N");

            BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
            binaryServerFormatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;

            IpcServerChannel clientServerChannel = new IpcServerChannel(properties, binaryServerFormatterSinkProvider);
            ChannelServices.RegisterChannel(clientServerChannel, false);
        }

        /// <summary>
        /// Gets or sets the hook to DirectX
        /// TODO: Move this out, hide it under graphics interface or something
        /// </summary>
        private BaseDXHook DirectXHook { get; set; }

        private IpcServerChannel IpcServerChannel { get; set; }

        private IHookClient HookClient { get; set; }

        private CancellationTokenSource CancelRequest { get; set; }

        private ManualResetEvent TaskRunning { get; set; }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(String moduleName);

        public void Run(RemoteHooking.IContext context, String channelName, String projectDirectory)
        {
            // When not using GAC there can be issues with remoting assemblies resolving correctly
            // this is a workaround that ensures that the current assembly is correctly associated
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += (sender, args) =>
            {
                return this.GetType().Assembly.FullName == args.Name ? this.GetType().Assembly : null;
            };

            this.TaskRunning = new ManualResetEvent(false);
            this.TaskRunning.Reset();

            try
            {
                // Initialize the Hook
                if (!this.InitializeDirectXHook())
                {
                    return;
                }

                // We start a thread here to periodically check if the host is still running
                // If the host process stops then we will automatically uninstall the hooks
                this.MaintainConnection();
            }
            catch
            {
            }
            finally
            {
                // Remove the client server channel (that allows client event handlers)
                ChannelServices.UnregisterChannel(this.IpcServerChannel);

                // Always sleep long enough for any remaining messages to complete sending
                Thread.Sleep(100);
            }
        }

        private void DisposeDirectXHook()
        {
            if (this.DirectXHook == null)
            {
                return;
            }

            //// this.DirectXHook.Dispose();
        }

        private Boolean InitializeDirectXHook()
        {
            DirextXGraphicsInterface dirextXGraphicsInterface = (DirextXGraphicsInterface)this.HookClient.GetGraphicsInterface();
            DirectXFlags.Direct3DVersionEnum version = DirectXFlags.Direct3DVersionEnum.Unknown;

            Dictionary<DirectXFlags.Direct3DVersionEnum, String> directXModules = new Dictionary<DirectXFlags.Direct3DVersionEnum, String>
            {
                { DirectXFlags.Direct3DVersionEnum.Direct3D9, "d3d9.dll" },
                { DirectXFlags.Direct3DVersionEnum.Direct3D10, "d3d10.dll" },
                { DirectXFlags.Direct3DVersionEnum.Direct3D10_1, "d3d10_1.dll" },
                { DirectXFlags.Direct3DVersionEnum.Direct3D11, "d3d11.dll" },
                { DirectXFlags.Direct3DVersionEnum.Direct3D11_1, "d3d11_1.dll" },
            };

            try
            {
                IntPtr handle = IntPtr.Zero;

                foreach (KeyValuePair<DirectXFlags.Direct3DVersionEnum, String> module in directXModules)
                {
                    handle = GetModuleHandle(module.Value);

                    if (handle != IntPtr.Zero)
                    {
                        version = module.Key;
                        break;
                    }
                }

                if (handle == IntPtr.Zero)
                {
                    return false;
                }

                switch (version)
                {
                    case DirectXFlags.Direct3DVersionEnum.Direct3D9:
                        this.DirectXHook = new DXHookD3D9(dirextXGraphicsInterface);
                        break;
                    case DirectXFlags.Direct3DVersionEnum.Direct3D10:
                        //// this.DirectXHook = new DXHookD3D10(DirextXGraphicsInterface);
                        break;
                    case DirectXFlags.Direct3DVersionEnum.Direct3D10_1:
                        //// this.DirectXHook = new DXHookD3D10_1(DirextXGraphicsInterface);
                        break;
                    case DirectXFlags.Direct3DVersionEnum.Direct3D11:
                        this.DirectXHook = new DXHookD3D11(dirextXGraphicsInterface);
                        break;
                    //// case Direct3DVersion.Direct3D11_1:
                    ////    this.DirectXHook = new DXHookD3D11_1(this.ClientConnection);
                    ////    return;
                    default:
                        return false;
                }

                this.DirectXHook.Hook();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Begin a background thread to check periodically that the host process is still accessible on its IPC channel
        /// </summary>
        private void MaintainConnection()
        {
            this.CancelRequest = new CancellationTokenSource();

            Task.Run(
            async () =>
            {
                while (true)
                {
                    try
                    {
                        this.HookClient.Ping();
                    }
                    catch
                    {
                        this.TaskRunning.Set();
                    }

                    // Await with cancellation
                    await Task.Delay(1000, this.CancelRequest.Token);
                }
            },
            this.CancelRequest.Token);

            // Wait until task is no longer running
            this.TaskRunning.WaitOne();

            // Cancel task to ensure it ends
            this.CancelRequest?.Cancel();

            // Clean up
            this.DisposeDirectXHook();
        }
    }
    //// End class
}
//// End namespace