using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;

namespace Anathema.Source.SystemInternals.Hook
{
    [Serializable]
    public class HookCommunicator : MarshalByRefObject
    {
        public DirextXGraphicsInterface GraphicsInterface { get; set; }

        public HookCommunicator(Int32 ProcessId, String ProjectDirectory)
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