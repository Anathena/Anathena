using Anathema.Source.Controller;
using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectX;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;
using System.Diagnostics;
using System.IO;

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

            // Skip if the process is already hooked (and we want to hook multiple applications)
            if (Process.MainWindowHandle == IntPtr.Zero)
                return;

            ClientInterface CaptureInterface = new ClientInterface();
            CaptureInterface.RemoteMessage += new MessageReceivedEvent(CaptureInterfaceRemoteMessage);
            GraphicsInterface = new DirextXGraphicsInterface(Process, CaptureInterface, Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath()));
        }

        public IGraphicsInterface GetGraphicsInterface()
        {
            return GraphicsInterface;
        }

        public void Uninject()
        {
            GraphicsInterface?.Disconnect();
            GraphicsInterface = null;
        }

        /// <summary>
        /// Display messages from the target process
        /// </summary>
        /// <param name="Message"></param>
        public void CaptureInterfaceRemoteMessage(MessageReceivedEventArgs Message)
        {
            // Process messages that come from the injected hook
        }

    } // End class

} // End namespace