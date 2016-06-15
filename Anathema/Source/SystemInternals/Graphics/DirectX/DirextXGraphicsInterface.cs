using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using EasyHook;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;

namespace Anathema.Source.SystemInternals.Graphics.DirectX
{
    /// <summary>
    /// Primary class that provides support for direct X manipulations over IPC
    /// </summary>
    public class DirextXGraphicsInterface : IGraphicsInterface, IDisposable
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
        /// <exception cref="InjectionFailedException">Thrown if the injection failed - see the InnerException for more details.</exception>
        /// <remarks>The target process will have its main window brought to the foreground after successful injection.</remarks>
        public DirextXGraphicsInterface(Process Process, ClientInterface CaptureInterface)
        {
            // If the process doesn't have a mainwindowhandle yet, skip it (we need to be able to get the hwnd to set foreground etc)
            if (Process.MainWindowHandle == IntPtr.Zero)
                throw new ProcessHasNoWindowHandleException();

            CaptureInterface.ProcessId = Process.Id;
            ServerInterface = CaptureInterface;

            // Initialize the IPC server (with our instance of ServerInterface)
            ScreenshotServer = RemoteHooking.IpcCreateServer<ClientInterface>(ref ChannelName, WellKnownObjectMode.Singleton, ServerInterface);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(ClientInterface).Assembly.Location,
                    typeof(ClientInterface).Assembly.Location, ChannelName);
            }
            catch (Exception Ex)
            {
                throw new InjectionFailedException(Ex);
            }

            this.Process = Process;
        }

        ~DirextXGraphicsInterface()
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

    public class ProcessHasNoWindowHandleException : Exception
    {
        public ProcessHasNoWindowHandleException() : base("The process does not have a window handle.") { }
    }

    public class InjectionFailedException : Exception
    {
        public InjectionFailedException(Exception innerException) : base("Injection failed. See InnerException for more detail.", innerException) { }

    } // End class

} // End namespace