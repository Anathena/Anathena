using Anathema.Source.Controller;
using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;

namespace Anathema.Source.SystemInternals.SpeedHack
{
    public class SpeedHackConnector : ISpeedHackConnector
    {
        private SpeedHackInterface SpeedHackInterface;

        public SpeedHackConnector() { }

        public void Inject(Process Process)
        {
            // Skip if the process is already hooked, or if there is no main window
            if (SpeedHackInterface != null || Process.MainWindowHandle == IntPtr.Zero)
                return;

            String ProjectDirectory = Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath());
            String ChannelName = null;

            SpeedHackInterface = new SpeedHackInterface();
            SpeedHackInterface.ProcessId = Process.Id;

            // Initialize the IPC server
            RemoteHooking.IpcCreateServer<SpeedHackInterface>(ref ChannelName, WellKnownObjectMode.Singleton, SpeedHackInterface);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(SpeedHackInterface).Assembly.Location,
                    typeof(SpeedHackInterface).Assembly.Location, ChannelName, ProjectDirectory);
            }
            catch (Exception Ex)
            {
                throw new Exception("Unable to Inject" + Ex);
            }
        }

        public ISpeedHackInterface GetSpeedHackInterface()
        {
            return SpeedHackInterface;
        }

        public void Uninject()
        {
            SpeedHackInterface = null;
        }

    } // End interface

} // End namespace