using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook;
using Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface;
using EasyHook;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook
{
    /// <summary>
    /// Primary class that provides support for direct X manipulations over IPC
    /// </summary>
    public class GraphicsInterface : IGraphicsInterface, IDisposable
    {
        private IpcServerChannel ScreenshotServer;
        private ClientInterface ServerInterface;
        public Process Process { get; set; }

        private Boolean Disposed = false;
        private String ChannelName = null;

        public ClientInterface CaptureInterface
        {
            get { return ServerInterface; }
        }

        /// <summary>
        /// Prepares capturing in the target process. Note that the process must not already be hooked, and must have a <see cref="Process.MainWindowHandle"/>.
        /// </summary>
        /// <param name="Process">The process to inject into</param>
        /// <exception cref="ProcessHasNoWindowHandleException">Thrown if the <paramref name="Process"/> does not have a window handle. This could mean that the process does not have a UI, or that the process has not yet finished starting.</exception>
        /// <exception cref="ProcessAlreadyHookedException">Thrown if the <paramref name="Process"/> is already hooked</exception>
        /// <exception cref="InjectionFailedException">Thrown if the injection failed - see the InnerException for more details.</exception>
        /// <remarks>The target process will have its main window brought to the foreground after successful injection.</remarks>
        public GraphicsInterface(Process Process, CaptureConfig Config, ClientInterface CaptureInterface)
        {
            // If the process doesn't have a mainwindowhandle yet, skip it (we need to be able to get the hwnd to set foreground etc)
            if (Process.MainWindowHandle == IntPtr.Zero)
            {
                throw new ProcessHasNoWindowHandleException();
            }

            // Skip if the process is already hooked (and we want to hook multiple applications)
            if (HookManager.IsHooked(Process.Id))
            {
                throw new ProcessAlreadyHookedException();
            }

            CaptureInterface.ProcessId = Process.Id;
            ServerInterface = CaptureInterface;
            // ServerInterface = new CaptureInterface() { ProcessId = Process.Id };

            // Initialize the IPC server (with our instance of ServerInterface)
            ScreenshotServer = RemoteHooking.IpcCreateServer<ClientInterface>(ref ChannelName, WellKnownObjectMode.Singleton, ServerInterface);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(ClientInterface).Assembly.Location,
                    typeof(ClientInterface).Assembly.Location, ChannelName, Config);
            }
            catch (Exception Ex)
            {
                throw new InjectionFailedException(Ex);
            }

            HookManager.AddHookedProcess(Process.Id);

            this.Process = Process;
        }

        ~GraphicsInterface()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean Disposing)
        {
            if (Disposed)
                return;

            if (Disposing)
                ServerInterface.Disconnect();

            Disposed = true;
        }

        public void DrawString(String Text, Int32 LocationX, Int32 LocationY)
        {

        }

    } // End class

} // End namespace