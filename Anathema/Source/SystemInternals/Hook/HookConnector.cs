using Anathema.Source.Controller;
using Anathema.Source.SystemInternals.Hook;
using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;

namespace Anathema.Source.SystemInternals.Graphics
{
    /// <summary>
    /// Provides capability to access objects in the target process
    /// </summary>
    class HookConnector : IHookConnector
    {
        private HookCommunication HookCommunication;

        public HookConnector() { }

        public void Inject(Process Process)
        {
            // Skip if the process is already hooked, or if there is no main window
            if (HookCommunication != null || Process.MainWindowHandle == IntPtr.Zero)
                return;

            String ProjectDirectory = Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath());
            String ChannelName = null;


            HookCommunication = new HookCommunication(Process.Id, ProjectDirectory);

            // Initialize the IPC server
            RemoteHooking.IpcCreateServer<HookCommunication>(ref ChannelName, WellKnownObjectMode.Singleton, HookCommunication);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(HookCommunication).Assembly.Location,
                    typeof(HookCommunication).Assembly.Location, ChannelName, ProjectDirectory);
            }
            catch (Exception Ex)
            {
                throw new Exception("Unable to Inject" + Ex);
            }
        }

        public IGraphicsInterface GetGraphicsInterface()
        {
            return HookCommunication?.GraphicsInterface;
        }

        public void Uninject()
        {
            HookCommunication.GraphicsInterface = null;
        }

    } // End class

} // End namespace