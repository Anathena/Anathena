using Ana.Source.Engine.Hook.Graphics.DirectX.Interface;
using System;

namespace Ana.Source.Engine.Hook.Graphics
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