﻿using Anathema.Source.Controller;
using Anathema.Source.SystemInternals.Graphics;
using EasyHook;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;

namespace Anathema.Source.SystemInternals.Hook.Client
{
    /// <summary>
    /// Provides capability to access objects in the target process
    /// </summary>
    class HookCreator : IHookCreator
    {
        private HookCommunicator HookCommunicator;

        public HookCreator() { }

        public void Inject(Process Process)
        {
            // Skip if the process is already hooked, or if there is no main window
            if (HookCommunicator != null || Process.MainWindowHandle == IntPtr.Zero)
                return;

            String ProjectDirectory = Path.GetDirectoryName(Main.GetInstance().GetProjectFilePath());
            String ChannelName = null;

            HookCommunicator = new HookCommunicator(Process.Id, ProjectDirectory);

            // Initialize the IPC server
            RemoteHooking.IpcCreateServer<HookCommunicator>(ref ChannelName, WellKnownObjectMode.Singleton, HookCommunicator);

            try
            {
                // Inject DLL into target process
                RemoteHooking.Inject(Process.Id, InjectionOptions.Default, typeof(HookCommunicator).Assembly.Location,
                    typeof(HookCommunicator).Assembly.Location, ChannelName, ProjectDirectory);
            }
            catch (Exception Ex)
            {
                throw new Exception("Unable to Inject" + Ex);
            }
        }

        public IGraphicsInterface GetGraphicsInterface()
        {
            return HookCommunicator?.GraphicsInterface;
        }

        public void Uninject()
        {
            HookCommunicator.GraphicsInterface = null;
        }

    } // End class

} // End namespace