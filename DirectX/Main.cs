using DirectXShell.Hook;
using DirectXShell.Interface;
using EasyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;

namespace DirectXShell
{
    public class Main : IEntryPoint
    {
        private ClientCaptureInterfaceEventProxy ClientEventProxy;
        private IpcServerChannel ClientServerChannel;
        private ClientInterface ClientConnection;

        private List<IDXHook> DirectXHooks;
        private IDXHook DirectXHook;
        private ManualResetEvent RunWait;

        private Int64 StopCheckAlive;

        public Main(RemoteHooking.IContext Context, String ChannelName, CaptureConfig Config)
        {
            ClientEventProxy = new ClientCaptureInterfaceEventProxy();
            this.ClientServerChannel = null;

            DirectXHooks = new List<IDXHook>();
            DirectXHook = null;

            StopCheckAlive = 0;

            // Get reference to IPC to host application
            ClientConnection = RemoteHooking.IpcConnectClient<ClientInterface>(ChannelName);

            // We try to ping immediately, if it fails then injection fails
            ClientConnection.Ping();

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            IDictionary Properties = new Hashtable();
            Properties["name"] = ChannelName;
            Properties["portName"] = ChannelName + Guid.NewGuid().ToString("N");

            BinaryServerFormatterSinkProvider BinaryProv = new BinaryServerFormatterSinkProvider();
            BinaryProv.TypeFilterLevel = TypeFilterLevel.Full;

            IpcServerChannel ClientServerChannel = new IpcServerChannel(Properties, BinaryProv);
            ChannelServices.RegisterChannel(ClientServerChannel, false);
        }

        public void Run(RemoteHooking.IContext Context, String ChannelName, CaptureConfig Config)
        {
            // When not using GAC there can be issues with remoting assemblies resolving correctly
            // this is a workaround that ensures that the current assembly is correctly associated
            AppDomain CurrentDomain = AppDomain.CurrentDomain;
            CurrentDomain.AssemblyResolve += (Sender, Args) =>
            {
                return this.GetType().Assembly.FullName == Args.Name ? this.GetType().Assembly : null;
            };

            // NOTE: This is running in the target process
            ClientConnection.Message(MessageType.Information, "Injected into process Id:{0}.", EasyHook.RemoteHooking.GetCurrentProcessId());

            RunWait = new ManualResetEvent(false);
            RunWait.Reset();
            try
            {
                // Initialise the Hook
                if (!InitializeDirectXHook(Config))
                {
                    return;
                }

                ClientConnection.Disconnected += ClientEventProxy.DisconnectedProxyHandler;

                // Important Note:
                // accessing the _interface from within a _clientEventProxy event handler must always 
                // be done on a different thread otherwise it will cause a deadlock

                ClientEventProxy.Disconnected += () =>
                {
                    // We can now signal the exit of the Run method
                    RunWait.Set();
                };

                // We start a thread here to periodically check if the host is still running
                // If the host process stops then we will automatically uninstall the hooks
                StartCheckHostIsAliveThread();

                // Wait until signaled for exit either when a Disconnect message from the host 
                // or if the the check is alive has failed to Ping the host.
                RunWait.WaitOne();

                // we need to tell the check host thread to exit (if it hasn't already)
                StopCheckHostIsAliveThread();

                // Dispose of the DXHook so any installed hooks are removed correctly
                DisposeDirectXHook();
            }
            catch (Exception Ex)
            {
                ClientConnection.Message(MessageType.Error, "An unexpected error occured: {0}", Ex.ToString());
            }
            finally
            {
                try
                {
                    ClientConnection.Message(MessageType.Information, "Disconnecting from process {0}", EasyHook.RemoteHooking.GetCurrentProcessId());
                }
                catch
                {

                }

                // Remove the client server channel (that allows client event handlers)
                ChannelServices.UnregisterChannel(ClientServerChannel);

                // Always sleep long enough for any remaining messages to complete sending
                Thread.Sleep(100);
            }
        }

        private void DisposeDirectXHook()
        {
            if (DirectXHooks != null)
            {
                try
                {
                    ClientConnection.Message(MessageType.Debug, "Disposing of hooks...");
                }
                catch (RemotingException) { } // Ignore channel remoting errors

                // Dispose of the hooks so they are removed
                foreach (IDXHook DXHook in DirectXHooks)
                {
                    DXHook.Dispose();
                }

                DirectXHooks.Clear();
            }
        }

        private Boolean InitializeDirectXHook(CaptureConfig Config)
        {
            Direct3DVersionEnum Version = Config.Direct3DVersion;

            List<Direct3DVersionEnum> LoadedVersions = new List<Direct3DVersionEnum>();

            Boolean Is64BitProcess = RemoteHooking.IsX64Process(RemoteHooking.GetCurrentProcessId());
            ClientConnection.Message(MessageType.Information, "Remote process is a {0}-bit process.", Is64BitProcess ? "64" : "32");

            try
            {
                if (Version == Direct3DVersionEnum.AutoDetect || Version == Direct3DVersionEnum.Unknown)
                {
                    // Attempt to determine the correct version based on loaded module.
                    // In most cases this will work fine, however it is perfectly ok for an application to use a D3D10 device along with D3D11 devices
                    // so the version might matched might not be the one you want to use
                    IntPtr D3D9Loaded = IntPtr.Zero;
                    IntPtr D3D10Loaded = IntPtr.Zero;
                    IntPtr D3D10_1Loaded = IntPtr.Zero;
                    IntPtr D3D11Loaded = IntPtr.Zero;
                    IntPtr D3D11_1Loaded = IntPtr.Zero;

                    Int32 DelayTime = 100;
                    Int32 RetryCount = 0;

                    while (D3D9Loaded == IntPtr.Zero && D3D10Loaded == IntPtr.Zero && D3D10_1Loaded == IntPtr.Zero && D3D11Loaded == IntPtr.Zero && D3D11_1Loaded == IntPtr.Zero)
                    {
                        RetryCount++;
                        D3D9Loaded = GetModuleHandle("d3d9.dll");
                        D3D10Loaded = GetModuleHandle("d3d10.dll");
                        D3D10_1Loaded = GetModuleHandle("d3d10_1.dll");
                        D3D11Loaded = GetModuleHandle("d3d11.dll");
                        D3D11_1Loaded = GetModuleHandle("d3d11_1.dll");
                        Thread.Sleep(DelayTime);

                        if (RetryCount * DelayTime > 5000)
                        {
                            ClientConnection.Message(MessageType.Error, "Unsupported Direct3D version, or Direct3D DLL not loaded within 5 seconds.");
                            return false;
                        }
                    }

                    Version = Direct3DVersionEnum.Unknown;

                    if (D3D11_1Loaded != IntPtr.Zero)
                    {
                        ClientConnection.Message(MessageType.Debug, "Autodetect found Direct3D 11.1");
                        Version = Direct3DVersionEnum.Direct3D11_1;
                        LoadedVersions.Add(Version);
                    }
                    if (D3D11Loaded != IntPtr.Zero)
                    {
                        ClientConnection.Message(MessageType.Debug, "Autodetect found Direct3D 11");
                        Version = Direct3DVersionEnum.Direct3D11;
                        LoadedVersions.Add(Version);
                    }
                    if (D3D10_1Loaded != IntPtr.Zero)
                    {
                        ClientConnection.Message(MessageType.Debug, "Autodetect found Direct3D 10.1");
                        Version = Direct3DVersionEnum.Direct3D10_1;
                        LoadedVersions.Add(Version);
                    }
                    if (D3D10Loaded != IntPtr.Zero)
                    {
                        ClientConnection.Message(MessageType.Debug, "Autodetect found Direct3D 10");
                        Version = Direct3DVersionEnum.Direct3D10;
                        LoadedVersions.Add(Version);
                    }
                    if (D3D9Loaded != IntPtr.Zero)
                    {
                        ClientConnection.Message(MessageType.Debug, "Autodetect found Direct3D 9");
                        Version = Direct3DVersionEnum.Direct3D9;
                        LoadedVersions.Add(Version);
                    }
                }
                else
                {
                    // If not autodetect, assume specified version is loaded
                    LoadedVersions.Add(Version);
                }

                foreach (Direct3DVersionEnum DXVersion in LoadedVersions)
                {
                    Version = DXVersion;
                    switch (Version)
                    {
                        case Direct3DVersionEnum.Direct3D9:
                            DirectXHook = new DXHookD3D9(ClientConnection);
                            break;
                        case Direct3DVersionEnum.Direct3D10:
                            DirectXHook = new DXHookD3D10(ClientConnection);
                            break;
                        case Direct3DVersionEnum.Direct3D10_1:
                            DirectXHook = new DXHookD3D10_1(ClientConnection);
                            break;
                        case Direct3DVersionEnum.Direct3D11:
                            DirectXHook = new DXHookD3D11(ClientConnection);
                            break;
                        //case Direct3DVersion.Direct3D11_1:
                        //    DirectXHook = new DXHookD3D11_1(ClientConnection);
                        //    return;
                        default:
                            ClientConnection.Message(MessageType.Error, "Unsupported Direct3D version: {0}", Version);
                            return false;
                    }

                    DirectXHook.Config = Config;
                    DirectXHook.Hook();

                    DirectXHooks.Add(DirectXHook);
                }

                return true;

            }
            catch (Exception Ex)
            {
                // Notify the host/server application about this error
                ClientConnection.Message(MessageType.Error, "Error in InitialiseHook: {0}", Ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Begin a background thread to check periodically that the host process is still accessible on its IPC channel
        /// </summary>
        private void StartCheckHostIsAliveThread()
        {
            Task.Run(() =>
            {
                try
                {
                    while (Interlocked.Read(ref StopCheckAlive) == 0)
                    {
                        Thread.Sleep(1000);

                        // .NET Remoting exceptions will throw RemotingException
                        ClientConnection.Ping();
                    }
                }
                catch // We will assume that any exception means that the hooks need to be removed. 
                {
                    // Signal the Run method so that it can exit
                    RunWait.Set();
                }
            });
        }

        /// <summary>
        /// Tell the _checkAlive thread that it can exit if it hasn't already
        /// </summary>
        private void StopCheckHostIsAliveThread()
        {
            Interlocked.Increment(ref StopCheckAlive);
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

    } // End class

} // End namespace