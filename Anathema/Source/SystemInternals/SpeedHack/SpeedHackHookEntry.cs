using EasyHook;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Source.SystemInternals.SpeedHack
{
    public class SpeedHackHookEntry : IEntryPoint
    {
        private IpcServerChannel ClientServerChannel;
        private ManualResetEvent TaskRunning;
        private CancellationTokenSource CancelRequest;

        private SpeedHackInterface SpeedHackInterface;

        public SpeedHackHookEntry(RemoteHooking.IContext Context, String ChannelName)
        {
            this.ClientServerChannel = null;

            // Get reference to IPC to host application
            SpeedHackInterface = RemoteHooking.IpcConnectClient<SpeedHackInterface>(ChannelName);

            // We try to ping immediately, if it fails then injection fails
            SpeedHackInterface.Ping();

            // Attempt to create a IpcServerChannel so that any event handlers on the client will function correctly
            IDictionary Properties = new Hashtable();
            Properties["name"] = ChannelName;
            Properties["portName"] = ChannelName + Guid.NewGuid().ToString("N");

            BinaryServerFormatterSinkProvider BinaryProv = new BinaryServerFormatterSinkProvider();
            BinaryProv.TypeFilterLevel = TypeFilterLevel.Full;

            IpcServerChannel ClientServerChannel = new IpcServerChannel(Properties, BinaryProv);
            ChannelServices.RegisterChannel(ClientServerChannel, false);
        }

        public void Run(RemoteHooking.IContext Context, String ChannelName)
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
                // GraphicsInterface.Disconnected += ClientEventProxy.DisconnectedProxyHandler;

                // ClientEventProxy.Disconnected += () =>
                {
                    // We can now signal the exit of the Run method
                    // TaskRunning.Set();
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
                        // GraphicsInterface.Ping();
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
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(String LPModuleName);

    } // End class

} // End namespace