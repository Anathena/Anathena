using Anathema.Source.SystemInternals.Graphics.DirectX.Hook;
using Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX10;
using Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX11;
using Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX9;
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

namespace Anathema.Source.SystemInternals.Graphics.DirectX
{
    public class HookEntry : IEntryPoint
    {
        private ClientCaptureInterfaceEventProxy ClientEventProxy;
        private IpcServerChannel ClientServerChannel;
        private ClientInterface ClientInterface;

        private IDXHook DirectXHook;
        private ManualResetEvent TaskRunning;

        private CancellationTokenSource CancelRequest;

        public HookEntry(RemoteHooking.IContext Context, String ChannelName, String ProjectDirectory)
        {
            ClientEventProxy = new ClientCaptureInterfaceEventProxy();
            this.ClientServerChannel = null;
            DirectXHook = null;

            // Get reference to IPC to host application
            ClientInterface = RemoteHooking.IpcConnectClient<ClientInterface>(ChannelName);

            // We try to ping immediately, if it fails then injection fails
            ClientInterface.Ping();

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

            ClientInterface.Message(MessageType.Information, "Injected into process Id:{0}.", EasyHook.RemoteHooking.GetCurrentProcessId());

            TaskRunning = new ManualResetEvent(false);
            TaskRunning.Reset();

            try
            {
                // Initialize the Hook
                if (!InitializeDirectXHook())
                    return;

                ClientInterface.Disconnected += ClientEventProxy.DisconnectedProxyHandler;

                // Important Note:
                // accessing the _interface from within a _clientEventProxy event handler must always 
                // be done on a different thread otherwise it will cause a deadlock

                ClientEventProxy.Disconnected += () =>
                {
                    // We can now signal the exit of the Run method
                    TaskRunning.Set();
                };

                // We start a thread here to periodically check if the host is still running
                // If the host process stops then we will automatically uninstall the hooks
                MaintainConnection();
            }
            catch (Exception Ex)
            {
                ClientInterface.Message(MessageType.Error, "An unexpected error occured: {0}", Ex.ToString());
            }
            finally
            {
                try
                {
                    ClientInterface.Message(MessageType.Information, "Disconnecting from process {0}", EasyHook.RemoteHooking.GetCurrentProcessId());
                }
                catch { }

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

            try
            {
                ClientInterface.Message(MessageType.Debug, "Disposing of hooks...");
            }
            catch { }

            DirectXHook.Dispose();
        }

        private Boolean InitializeDirectXHook()
        {
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
                {
                    ClientInterface.Message(MessageType.Error, "Unsupported Direct3D version, or DLL not loaded");
                    return false;
                }

                switch (Version)
                {
                    case Direct3DVersionEnum.Direct3D9:
                        DirectXHook = new DXHookD3D9(ClientInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D10:
                        DirectXHook = new DXHookD3D10(ClientInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D10_1:
                        DirectXHook = new DXHookD3D10_1(ClientInterface);
                        break;
                    case Direct3DVersionEnum.Direct3D11:
                        DirectXHook = new DXHookD3D11(ClientInterface);
                        break;
                    //case Direct3DVersion.Direct3D11_1:
                    //    DirectXHook = new DXHookD3D11_1(ClientConnection);
                    //    return;
                    default:
                        ClientInterface.Message(MessageType.Error, "Unsupported Direct3D version: {0}", Version);
                        return false;
                }

                DirectXHook.Hook();
                return true;

            }
            catch (Exception Ex)
            {
                // Notify the host/server application about this error
                ClientInterface.Message(MessageType.Error, "Error in InitialiseHook: {0}", Ex.ToString());
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
                        ClientInterface.Ping();
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
        public static extern IntPtr GetModuleHandle(string lpModuleName);

    } // End class

} // End namespace