namespace Ana.Source.Engine.Hook.Graphics
{
    using DirectX.Interface;
    using System;

    internal class GraphicsFactory
    {
        public static IGraphicsInterface GetGraphicsInterface(String projectDirectory)
        {
            DirextXGraphicsInterface graphicsInterface = new DirextXGraphicsInterface(projectDirectory);
            return graphicsInterface;
        }
    }
    //// End interface
}
//// End namespace