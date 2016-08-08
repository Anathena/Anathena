using Anathena.Source.Engine.Graphics.DirectX.Interface;
using System;
using System.Diagnostics;

namespace Anathena.Source.Engine.Graphics
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