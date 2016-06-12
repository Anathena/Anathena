using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics.DirectX;

namespace Anathema.Source.SystemInternals.Graphics
{
    class GraphicsFactory
    {

        public static IGraphicsInterface GetGraphicsInterface()
        {
            return new GraphicsDirextX();
        }

    } // End class

} // End namespace