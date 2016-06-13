using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectX;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics
{
    class GraphicsConnectorFactory
    {

        public static IGraphicsConnector GetGraphicsConnector(Process TargetProcess)
        {
            // TODO: Once OpenGL/Ogre API hooking is done, we need logic here to decide which is the correct one to return
            return new ConnectorDirextX();
        }

    } // End class

} // End namespace