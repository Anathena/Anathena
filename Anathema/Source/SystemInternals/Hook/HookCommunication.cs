using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;

namespace Anathema.Source.SystemInternals.Hook
{

    [Serializable]
    public delegate void DisconnectedEvent();

    [Serializable]
    public class HookCommunication : MarshalByRefObject
    {
        /// <summary>
        /// The client process Id
        /// </summary>
        private Int32 ProcessId { get; set; }

        private String ProjectDirectory { get; set; }

        public DirextXGraphicsInterface GraphicsInterface { get; set; }

        public HookCommunication(Int32 ProcessId, String ProjectDirectory)
        {
            GraphicsInterface = new DirextXGraphicsInterface();
            GraphicsInterface.ProcessId = ProcessId;
            GraphicsInterface.ProjectDirectory = ProjectDirectory;
        }

        /// <summary>
        /// Empty method to ensure we can call the client without crashing
        /// </summary>
        public void Ping() { }

    } // End class

} // End namespace