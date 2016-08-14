using Anathena.Source.Controller;
using Anathena.Source.Engine.Hook.Graphics;
using Anathena.Source.Engine.Hook.SpeedHack;
using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;

namespace Anathena.Source.Engine.Hook.Client
{
    /// <summary>
    /// Provides capability to access objects in the target process
    /// </summary>
    [Serializable]
    class HookClient : MarshalByRefObject, IHookClient
    {
        /// <summary>
        /// Graphics interface shared between client and hook
        /// </summary>
        public IGraphicsInterface GraphicsInterface { get; set; }

        /// <summary>
        /// Speedhack interface shared between client and hook
        /// </summary>
        public ISpeedHackInterface SpeedHackInterface { get; set; }

        public HookClient() { }

        public void Inject(Process Process)
        {
            // Skip if the process is already hooked, or if there is no main window
            if (GraphicsInterface != null || SpeedHackInterface != null || (Process == null || Process.MainWindowHandle == IntPtr.Zero))
                return;

            String ProjectDirectory = Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath());
            String ChannelName = null;

            GraphicsInterface = GraphicsFactory.GetGraphicsInterface(ProjectDirectory);
            SpeedHackInterface = new SpeedHackInterface();

            // Initialize the IPC server, giving the server access to the interfaces defined here
            RemoteHooking.IpcCreateServer<HookClient>(ref ChannelName, WellKnownObjectMode.Singleton, this);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(HookClient).Assembly.Location,
                    typeof(HookClient).Assembly.Location, ChannelName, ProjectDirectory);
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

        public ISpeedHackInterface GetSpeedHackInterface()
        {
            return SpeedHackInterface;
        }

        public void Ping() { }

        public void Uninject()
        {
            GraphicsInterface = null;
            SpeedHackInterface = null;
        }

    } // End class

} // End namespace