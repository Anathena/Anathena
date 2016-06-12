using Capture.Hook;
using Capture.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading.Tasks;

namespace Capture
{
    public class EntryPoint : EasyHook.IEntryPoint
    {
        List<IDXHook> _directXHooks = new List<IDXHook>();
        IDXHook _directXHook = null;
        private CaptureInterface _interface;
        private System.Threading.ManualResetEvent _runWait;
        ClientCaptureInterfaceEventProxy _clientEventProxy = new ClientCaptureInterfaceEventProxy();
        IpcServerChannel _clientServerChannel = null;

        public EntryPoint(
            EasyHook.RemoteHooking.IContext context,
            String channelName,
            CaptureConfig config)
        {
            // Get reference to IPC to host application
            // Note: any methods called or events triggered against _interface will execute in the host process.
            _interface = EasyHook.RemoteHooking.IpcConnectClient<CaptureInterface>(channelName);

            // We try to ping immediately, if it fails then injection fails
            _interface.Ping();

            #region Allow client event handlers (bi-directional IPC)

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            System.Collections.IDictionary properties = new System.Collections.Hashtable();
            properties["name"] = channelName;
            properties["portName"] = channelName + Guid.NewGuid().ToString("N"); // random portName so no conflict with existing channels of channelName

            System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider binaryProv = new System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider();
            binaryProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            System.Runtime.Remoting.Channels.Ipc.IpcServerChannel _clientServerChannel = new System.Runtime.Remoting.Channels.Ipc.IpcServerChannel(properties, binaryProv);
            System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(_clientServerChannel, false);

            #endregion
        }

        public void Run(
            EasyHook.RemoteHooking.IContext context,
            String channelName,
            CaptureConfig config)
        {
            // When not using GAC there can be issues with remoting assemblies resolving correctly
            // this is a workaround that ensures that the current assembly is correctly associated
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += (sender, args) =>
            {
                return this.GetType().Assembly.FullName == args.Name ? this.GetType().Assembly : null;
            };

            // NOTE: This is running in the target process
            _interface.Message(MessageType.Information, "Injected into process Id:{0}.", EasyHook.RemoteHooking.GetCurrentProcessId());

            _runWait = new System.Threading.ManualResetEvent(false);
            _runWait.Reset();
            try
            {
                // Initialise the Hook
                if (!InitialiseDirectXHook(config))
                {
                    return;
                }
                _interface.Disconnected += _clientEventProxy.DisconnectedProxyHandler;

                // Important Note:
                // accessing the _interface from within a _clientEventProxy event handler must always 
                // be done on a different thread otherwise it will cause a deadlock

                _clientEventProxy.Disconnected += () =>
                {
                    // We can now signal the exit of the Run method
                    _runWait.Set();
                };

                // We start a thread here to periodically check if the host is still running
                // If the host process stops then we will automatically uninstall the hooks
                StartCheckHostIsAliveThread();

                // Wait until signaled for exit either when a Disconnect message from the host 
                // or if the the check is alive has failed to Ping the host.
                _runWait.WaitOne();

                // we need to tell the check host thread to exit (if it hasn't already)
                StopCheckHostIsAliveThread();

                // Dispose of the DXHook so any installed hooks are removed correctly
                DisposeDirectXHook();
            }
            catch (Exception e)
            {
                _interface.Message(MessageType.Error, "An unexpected error occured: {0}", e.ToString());
            }
            finally
            {
                try
                {
                    _interface.Message(MessageType.Information, "Disconnecting from process {0}", EasyHook.RemoteHooking.GetCurrentProcessId());
                }
                catch
                {
                }

                // Remove the client server channel (that allows client event handlers)
                System.Runtime.Remoting.Channels.ChannelServices.UnregisterChannel(_clientServerChannel);

                // Always sleep long enough for any remaining messages to complete sending
                System.Threading.Thread.Sleep(100);
            }
        }

        private void DisposeDirectXHook()
        {
            if (_directXHooks != null)
            {
                try
                {
                    _interface.Message(MessageType.Debug, "Disposing of hooks...");
                }
                catch (System.Runtime.Remoting.RemotingException) { } // Ignore channel remoting errors

                // Dispose of the hooks so they are removed
                foreach (var dxHook in _directXHooks)
                    dxHook.Dispose();

                _directXHooks.Clear();
            }
        }

        private bool InitialiseDirectXHook(CaptureConfig config)
        {
            Direct3DVersion version = config.Direct3DVersion;

            List<Direct3DVersion> loadedVersions = new List<Direct3DVersion>();

            bool isX64Process = EasyHook.RemoteHooking.IsX64Process(EasyHook.RemoteHooking.GetCurrentProcessId());
            _interface.Message(MessageType.Information, "Remote process is a {0}-bit process.", isX64Process ? "64" : "32");

            try
            {
                if (version == Direct3DVersion.AutoDetect || version == Direct3DVersion.Unknown)
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
                            _interface.Message(MessageType.Error, "Unsupported Direct3D version, or Direct3D DLL not loaded within 5 seconds.");
                            return false;
                        }
                    }

                    version = Direct3DVersion.Unknown;
                    if (d3D11_1Loaded != IntPtr.Zero)
                    {
                        _interface.Message(MessageType.Debug, "Autodetect found Direct3D 11.1");
                        version = Direct3DVersion.Direct3D11_1;
                        loadedVersions.Add(version);
                    }
                    if (d3D11Loaded != IntPtr.Zero)
                    {
                        _interface.Message(MessageType.Debug, "Autodetect found Direct3D 11");
                        version = Direct3DVersion.Direct3D11;
                        loadedVersions.Add(version);
                    }
                    if (d3D10_1Loaded != IntPtr.Zero)
                    {
                        _interface.Message(MessageType.Debug, "Autodetect found Direct3D 10.1");
                        version = Direct3DVersion.Direct3D10_1;
                        loadedVersions.Add(version);
                    }
                    if (d3D10Loaded != IntPtr.Zero)
                    {
                        _interface.Message(MessageType.Debug, "Autodetect found Direct3D 10");
                        version = Direct3DVersion.Direct3D10;
                        loadedVersions.Add(version);
                    }
                    if (d3D9Loaded != IntPtr.Zero)
                    {
                        _interface.Message(MessageType.Debug, "Autodetect found Direct3D 9");
                        version = Direct3DVersion.Direct3D9;
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
                        case Direct3DVersion.Direct3D9:
                            _directXHook = new DXHookD3D9(_interface);
                            break;
                        case Direct3DVersion.Direct3D10:
                            _directXHook = new DXHookD3D10(_interface);
                            break;
                        case Direct3DVersion.Direct3D10_1:
                            _directXHook = new DXHookD3D10_1(_interface);
                            break;
                        case Direct3DVersion.Direct3D11:
                            _directXHook = new DXHookD3D11(_interface);
                            break;
                        //case Direct3DVersion.Direct3D11_1:
                        //    _directXHook = new DXHookD3D11_1(_interface);
                        //    return;
                        default:
                            _interface.Message(MessageType.Error, "Unsupported Direct3D version: {0}", version);
                            return false;
                    }

                    _directXHook.Config = config;
                    _directXHook.Hook();

                    _directXHooks.Add(_directXHook);
                }

                return true;

            }
            catch (Exception e)
            {
                // Notify the host/server application about this error
                _interface.Message(MessageType.Error, "Error in InitialiseHook: {0}", e.ToString());
                return false;
            }
        }

        #region Check Host Is Alive

        Task _checkAlive;
        long _stopCheckAlive = 0;

        /// <summary>
        /// Begin a background thread to check periodically that the host process is still accessible on its IPC channel
        /// </summary>
        private void StartCheckHostIsAliveThread()
        {
            _checkAlive = new Task(() =>
            {
                try
                {
                    while (System.Threading.Interlocked.Read(ref _stopCheckAlive) == 0)
                    {
                        System.Threading.Thread.Sleep(1000);

                        // .NET Remoting exceptions will throw RemotingException
                        _interface.Ping();
                    }
                }
                catch // We will assume that any exception means that the hooks need to be removed. 
                {
                    // Signal the Run method so that it can exit
                    _runWait.Set();
                }
            });

            _checkAlive.Start();
        }

        /// <summary>
        /// Tell the _checkAlive thread that it can exit if it hasn't already
        /// </summary>
        private void StopCheckHostIsAliveThread()
        {
            System.Threading.Interlocked.Increment(ref _stopCheckAlive);
        }

        #endregion
    }
}
