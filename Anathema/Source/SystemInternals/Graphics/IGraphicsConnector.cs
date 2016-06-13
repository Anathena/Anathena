using System.Diagnostics;

namespace Anathema.Source.Graphics
{
    public interface IGraphicsConnector
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        void Uninject();

    } // End interface

} // End namespace