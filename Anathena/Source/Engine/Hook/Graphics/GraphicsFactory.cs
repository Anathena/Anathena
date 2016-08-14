using Anathena.Source.Engine.Hook.Graphics.DirectX.Interface;
using System;

namespace Anathena.Source.Engine.Hook.Graphics
{
    public class GraphicsFactory
    {
        public static IGraphicsInterface GetGraphicsInterface(String ProjectDirectory)
        {
            DirextXGraphicsInterface GraphicsInterface = new DirextXGraphicsInterface(ProjectDirectory);

            return GraphicsInterface;
        }

    } // End interface

} // End namespace