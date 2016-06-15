using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectX;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics
{
    /// <summary>
    /// Provides capability to connect to the graphics API of another process
    /// </summary>
    class DirextXConnector : IGraphicsConnector
    {
        private DirextXGraphicsInterface GraphicsInterface;

        public DirextXConnector() { }

        public void Inject(Process Process)
        {
            if (GraphicsInterface != null)
                return;

            // Must be running as Administrator to allow dynamic registration in GAC
            // Config.Register("Name", "Name.dll");

            // Skip if the process is already hooked (and we want to hook multiple applications)
            if (Process.MainWindowHandle == IntPtr.Zero)
                return;

            ClientInterface CaptureInterface = new ClientInterface();
            CaptureInterface.RemoteMessage += new MessageReceivedEvent(CaptureInterface_RemoteMessage);
            GraphicsInterface = new DirextXGraphicsInterface(Process, CaptureInterface);
        }

        public IGraphicsInterface GetGraphicsInterface()
        {
            return GraphicsInterface;
        }

        public void Uninject()
        {
            if (GraphicsInterface == null)
                return;

            GraphicsInterface.CaptureInterface.Disconnect();
            GraphicsInterface = null;
        }

        /// <summary>
        /// Display messages from the target process
        /// </summary>
        /// <param name="Message"></param>
        public void CaptureInterface_RemoteMessage(MessageReceivedEventArgs Message)
        {
            // Process messages that come from the injected hook
        }

    } // End class

} // End namespace