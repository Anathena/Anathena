using DirectXShell.Hook;
using DirectXShell.Interface;
using EasyHook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;
using System.Threading.Tasks;

namespace DirectXShell
{
    public class EntryPoint : IEntryPoint
    {
        private List<IDXHook> DirectXHooks = new List<IDXHook>();
        private IDXHook DirectXHook = null;
        private CaptureInterface CaptureInterface;
        private ManualResetEvent RunWait;
        private ClientCaptureInterfaceEventProxy ClientEventProxy = new ClientCaptureInterfaceEventProxy();
        private IpcServerChannel ClientServerChannel = null;

        public EntryPoint(RemoteHooking.IContext Context, String ChannelName, CaptureConfig Config)
        {
            // Get reference to IPC to host application
            // Note: any methods called or events triggered against _interface will execute in the host process.
            CaptureInterface = RemoteHooking.IpcConnectClient<CaptureInterface>(ChannelName);

            // We try to ping immediately, if it fails then injection fails
            CaptureInterface.Ping();

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            IDictionary Properties = new Hashtable();
            Properties["name"] = ChannelName;
            Properties["portName"] = ChannelName + Guid.NewGuid().ToString("N");

            BinaryServerFormatterSinkProvider binaryProv = new BinaryServerFormatterSinkProvider();
            binaryProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            IpcServerChannel _clientServerChannel = new IpcServerChannel(Properties, binaryProv);
            ChannelServices.RegisterChannel(_clientServerChannel, false);
        }

        public void Run(RemoteHooking.IContext Context, String ChannelName, CaptureConfig Config)
        {
            // When not using GAC there can be issues with remoting assemblies resolving correctly
            // this is a workaround that ensures that the current assembly is correctly associated
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += (sender, args) =>
            {
                return this.GetType().Assembly.FullName == args.Name ? this.GetType().Assembly : null;
            };

            // NOTE: This is running in the target process
            CaptureInterface.Message(MessageType.Information, "Injected into process Id:{0}.", EasyHook.RemoteHooking.GetCurrentProcessId());

            RunWait = new ManualResetEvent(false);
            RunWait.Reset();
            try
            {
                // Initialise the Hook
                if (!InitializeDirectXHook(Config))
                {
                    return;
                }

                CaptureInterface.Disconnected += ClientEventProxy.DisconnectedProxyHandler;

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
                CaptureInterface.Message(MessageType.Error, "An unexpected error occured: {0}", Ex.ToString());
            }
            finally
            {
                try
                {
                    CaptureInterface.Message(MessageType.Information, "Disconnecting from process {0}", EasyHook.RemoteHooking.GetCurrentProcessId());
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
                    CaptureInterface.Message(MessageType.Debug, "Disposing of hooks...");
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

        private bool InitializeDirectXHook(CaptureConfig config)
        {
            Direct3DVersionEnum version = config.Direct3DVersion;

            List<Direct3DVersionEnum> loadedVersions = new List<Direct3DVersionEnum>();

            bool isX64Process = EasyHook.RemoteHooking.IsX64Process(EasyHook.RemoteHooking.GetCurrentProcessId());
            CaptureInterface.Message(MessageType.Information, "Remote process is a {0}-bit process.", isX64Process ? "64" : "32");

            try
            {
                if (version == Direct3DVersionEnum.AutoDetect || version == Direct3DVersionEnum.Unknown)
                {
                    // Attempt to determine the correct version based on loaded module.
                    // In most cases this will work fine, however it is perfectly ok for an application to use a D3D10 device along with D3D11 devices
                    // so the version might matched might not be the one you want to use
                    IntPtr d3D9Loaded = IntPtr.Zero;
                    IntPtr d3D10Loaded = IntPtr.Zero;
                    IntPtr d3D10_1Loaded = IntPtr.Zero;
                    IntPtr d3D11Loaded = IntPtr.Zero;
                    IntPtr d3D11_1Loaded = IntPtr.Zero;

                    int delayTime = 100;
                    int retryCount = 0;
                    while (d3D9Loaded == IntPtr.Zero && d3D10Loaded == IntPtr.Zero && d3D10_1Loaded == IntPtr.Zero && d3D11Loaded == IntPtr.Zero && d3D11_1Loaded == IntPtr.Zero)
                    {
                        retryCount++;
                        d3D9Loaded = NativeMethods.GetModuleHandle("d3d9.dll");
                        d3D10Loaded = NativeMethods.GetModuleHandle("d3d10.dll");
                        d3D10_1Loaded = NativeMethods.GetModuleHandle("d3d10_1.dll");
                        d3D11Loaded = NativeMethods.GetModuleHandle("d3d11.dll");
                        d3D11_1Loaded = NativeMethods.GetModuleHandle("d3d11_1.dll");
                        System.Threading.Thread.Sleep(delayTime);

                        if (retryCount * delayTime > 5000)
                        {
                            CaptureInterface.Message(MessageType.Error, "Unsupported Direct3D version, or Direct3D DLL not loaded within 5 seconds.");
                            return false;
                        }
                    }

                    version = Direct3DVersionEnum.Unknown;
                    if (d3D11_1Loaded != IntPtr.Zero)
                    {
                        CaptureInterface.Message(MessageType.Debug, "Autodetect found Direct3D 11.1");
                        version = Direct3DVersionEnum.Direct3D11_1;
                        loadedVersions.Add(version);
                    }
                    if (d3D11Loaded != IntPtr.Zero)
                    {
                        CaptureInterface.Message(MessageType.Debug, "Autodetect found Direct3D 11");
                        version = Direct3DVersionEnum.Direct3D11;
                        loadedVersions.Add(version);
                    }
                    if (d3D10_1Loaded != IntPtr.Zero)
                    {
                        CaptureInterface.Message(MessageType.Debug, "Autodetect found Direct3D 10.1");
                        version = Direct3DVersionEnum.Direct3D10_1;
                        loadedVersions.Add(version);
                    }
                    if (d3D10Loaded != IntPtr.Zero)
                    {
                        CaptureInterface.Message(MessageType.Debug, "Autodetect found Direct3D 10");
                        version = Direct3DVersionEnum.Direct3D10;
                        loadedVersions.Add(version);
                    }
                    if (d3D9Loaded != IntPtr.Zero)
                    {
                        CaptureInterface.Message(MessageType.Debug, "Autodetect found Direct3D 9");
                        version = Direct3DVersionEnum.Direct3D9;
                        loadedVersions.Add(version);
                    }
                }
                else
                {
                    // If not autodetect, assume specified version is loaded
                    loadedVersions.Add(version);
                }

                foreach (var dxVersion in loadedVersions)
                {
                    version = dxVersion;
                    switch (version)
                    {
                        case Direct3DVersionEnum.Direct3D9:
                            DirectXHook = new DXHookD3D9(CaptureInterface);
                            break;
                        case Direct3DVersionEnum.Direct3D10:
                            DirectXHook = new DXHookD3D10(CaptureInterface);
                            break;
                        case Direct3DVersionEnum.Direct3D10_1:
                            DirectXHook = new DXHookD3D10_1(CaptureInterface);
                            break;
                        case Direct3DVersionEnum.Direct3D11:
                            DirectXHook = new DXHookD3D11(CaptureInterface);
                            break;
                        //case Direct3DVersion.Direct3D11_1:
                        //    _directXHook = new DXHookD3D11_1(_interface);
                        //    return;
                        default:
                            CaptureInterface.Message(MessageType.Error, "Unsupported Direct3D version: {0}", version);
                            return false;
                    }

                    DirectXHook.Config = config;
                    DirectXHook.Hook();

                    DirectXHooks.Add(DirectXHook);
                }

                return true;

            }
            catch (Exception e)
            {
                // Notify the host/server application about this error
                CaptureInterface.Message(MessageType.Error, "Error in InitialiseHook: {0}", e.ToString());
                return false;
            }
        }

        #region Check Host Is Alive

        Int64 StopCheckAlive = 0;

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
                        CaptureInterface.Ping();
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

        #endregion

    } // End class

} // End namespace