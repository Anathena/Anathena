using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX10;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX11;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX9;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using EasyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Source.SystemInternals.Hook
{
    /// <summary>
    /// Entry point for a hook in the target process. Automatically loads when RemoteHook.Inject() is called.
    /// </summary>
    public class HookEntry : IEntryPoint
    {
        private ClientCaptureInterfaceEventProxy ClientEventProxy;
        private IpcServerChannel ClientServerChannel;
        private HookCommunication HookCommunication;

        private BaseDXHook DirectXHook;
        private ManualResetEvent TaskRunning;

        private CancellationTokenSource CancelRequest;

        public HookEntry(RemoteHooking.IContext Context, String ChannelName, String ProjectDirectory)
        {
            ClientEventProxy = new ClientCaptureInterfaceEventProxy();
            this.ClientServerChannel = null;
            DirectXHook = null;

            // Get reference to IPC to host application
            HookCommunication = RemoteHooking.IpcConnectClient<HookCommunication>(ChannelName);

            // We try to ping immediately, if it fails then injection fails
            HookCommunication.Ping();

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            IDictionary Properties = new Hashtable();
            Properties["name"] = ChannelName;
            Properties["portName"] = ChannelName + Guid.NewGuid().ToString("N");

            BinaryServerFormatterSinkProvider BinaryProv = new BinaryServerFormatterSinkProvider();
            BinaryProv.TypeFilterLevel = TypeFilterLevel.Full;

            IpcServerChannel ClientServerChannel = new IpcServerChannel(Properties, BinaryProv);
            ChannelServices.RegisterChannel(ClientServerChannel, false);
        }

        public void Run(RemoteHooking.IContext Context, String ChannelName, String ProjectDirectory)
        {
            // When not using GAC there can be issues with remoting assemblies resolving correctly
            // this is a workaround that ensures that the current assembly is correctly associated
            AppDomain CurrentDomain = AppDomain.CurrentDomain;
            CurrentDomain.AssemblyResolve += (Sender, Args) =>
            {
                return this.GetType().Assembly.FullName == Args.Name ? this.GetType().Assembly : null;
            };

            TaskRunning = new ManualResetEvent(false);
            TaskRunning.Reset();

            try
            {
                // Initialize the Hook
                if (!InitializeDirectXHook())
                    return;

                HookCommunication.Disconnected += ClientEventProxy.DisconnectedProxyHandler;

                ClientEventProxy.Disconnected += () =>
                {
                    // We can now signal the exit of the Run method
                    TaskRunning.Set();
                };

                // We start a thread here to periodically check if the host is still running
                // If the host process stops then we will automatically uninstall the hooks
                MaintainConnection();
            }
            catch
            {

            }
            finally
            {
                // Remove the client server channel (that allows client event handlers)
                ChannelServices.UnregisterChannel(ClientServerChannel);

                // Always sleep long enough for any remaining messages to complete sending
                Thread.Sleep(100);
            }
        }

        private void DisposeDirectXHook()
        {
            if (DirectXHook == null)
                return;

            DirectXHook.Dispose();
        }

        private Boolean InitializeDirectXHook()
        {
            DirextXGraphicsInterface DirextXGraphicsInterface = new DirextXGraphicsInterface();
            Direct3DVersionEnum Version = Direct3DVersionEnum.Unknown;

            Dictionary<Direct3DVersionEnum, String> DXModules = new Dictionary<Direct3DVersionEnum, String>
            {
                { Direct3DVersionEnum.Direct3D9, "d3d9.dll" },
                { Direct3DVersionEnum.Direct3D10, "d3d10.dll" },
                { Direct3DVersionEnum.Direct3D10_1, "d3d10_1.dll" },
                { Direct3DVersionEnum.Direct3D11, "d3d11.dll" },
                { Direct3DVersionEnum.Direct3D11_1, "d3d11_1.dll" },
            };

            try
            {
                IntPtr Handle = IntPtr.Zero;

                foreach (KeyValuePair<Direct3DVersionEnum, String> Module in DXModules)
                {
                    Handle = GetModuleHandle(Module.Value);

                    if (Handle != IntPtr.Zero)
                    {
                        Version = Module.Key;
                        break;
                    }
                }

                if (Handle == IntPtr.Zero)
                    return false;

                switch (Version)
                {
                    case Direct3DVersionEnum.Direct3D9:
                        DirectXHook = new DXHookD3D9(DirextXGraphicsInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D10:
                        DirectXHook = new DXHookD3D10(DirextXGraphicsInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D10_1:
                        DirectXHook = new DXHookD3D10_1(DirextXGraphicsInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D11:
                        DirectXHook = new DXHookD3D11(DirextXGraphicsInterface);
                        break;
                    //case Direct3DVersion.Direct3D11_1:
                    //    DirectXHook = new DXHookD3D11_1(ClientConnection);
                    //    return;
                    default:
                        return false;
                }

                DirectXHook.Hook();
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
            CancelRequest = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        HookCommunication.Ping();
                    }
                    catch
                    {
                        TaskRunning.Set();
                    }

                    // Await with cancellation
                    await Task.Delay(1000, CancelRequest.Token);
                }

            }, CancelRequest.Token);

            // Wait until task is no longer running
            TaskRunning.WaitOne();

            // Cancel task to ensure it ends
            CancelRequest?.Cancel();

            // Clean up
            DisposeDirectXHook();
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(String LPModuleName);

    } // End class

} // End namespace