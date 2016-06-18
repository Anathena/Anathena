using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics
{
    public interface IHookConnector
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        void Uninject();

    } // End interface

} // End namespace