using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics
{
    public class GraphicsFactory
    {
        public static IGraphicsInterface GetGraphicsInterface(Process TargetProcess, String ProjectDirectory)
        {
            DirextXGraphicsInterface GraphicsInterface = new DirextXGraphicsInterface();
            GraphicsInterface.ProcessId = TargetProcess.Id;
            GraphicsInterface.ProjectDirectory = ProjectDirectory;

            return GraphicsInterface;
        }

    } // End interface

} // End namespace