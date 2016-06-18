using Anathema.Source.Controller;
using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;

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
            // Skip if the process is already hooked, or if there is no main window
            if (GraphicsInterface != null || Process.MainWindowHandle == IntPtr.Zero)
                return;

            String ProjectDirectory = Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath());
            String ChannelName = null;

            GraphicsInterface = new DirextXGraphicsInterface();
            GraphicsInterface.ProcessId = Process.Id;
            GraphicsInterface.ProjectDirectory = ProjectDirectory;

            // Initialize the IPC server
            RemoteHooking.IpcCreateServer<DirextXGraphicsInterface>(ref ChannelName, WellKnownObjectMode.Singleton, GraphicsInterface);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(DirextXGraphicsInterface).Assembly.Location,
                    typeof(DirextXGraphicsInterface).Assembly.Location, ChannelName, ProjectDirectory);
            }
            catch (Exception Ex)
            {
                throw new Exception("Unable to Inject" + Ex);
            }
        }

        public IGraphicsInterface GetGraphicsInterface()
        {
            return GraphicsInterface;
        }

        public void Uninject()
        {
            GraphicsInterface = null;
        }

    } // End class

} // End namespace