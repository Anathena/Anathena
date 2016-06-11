using Anathema.Source.Utils.LUA.Graphics3;

namespace Anathema.Source.Utils.LUA
{
    class GraphicsFactory
    {
        public IGraphicsCore GetGraphicsObject()
        {
            return new GraphicsDX9();
        }

    } // End class

} // End namespace