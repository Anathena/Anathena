using Anathena.Source.Engine.Graphics;
using Anathena.Source.Engine.Hook.SpeedHack;
using System;
using System.Diagnostics;

namespace Anathena.Source.Engine.Hook
{
    [Serializable]
    public class HookCommunicator : MarshalByRefObject
    {
        public IGraphicsInterface GraphicsInterface { get; set; }
        public ISpeedHackInterface SpeedHackInterface { get; set; }

        public HookCommunicator(Process Process, String ProjectDirectory)
        {
            GraphicsInterface = GraphicsFactory.GetGraphicsInterface(Process, ProjectDirectory);
            SpeedHackInterface = new SpeedHackInterface();
        }

        /// <summary>
        /// Empty method to ensure we can call the client without crashing
        /// </summary>
        public void Ping() { }

    } // End class

} // End namespace