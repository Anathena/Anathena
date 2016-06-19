using Anathema.Source.SystemInternals.Graphics;
using Anathema.Source.SystemInternals.SpeedHack;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Hook.Client
{
    public interface IHookCreator
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        ISpeedHackInterface GetSpeedHackInterface();

        void Uninject();

    } // End interface

} // End namespace