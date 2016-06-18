using Anathema.Source.SystemInternals.Graphics;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Hook.Client
{
    public interface IHookCreator
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        void Uninject();

    } // End interface

} // End namespace