using Anathema.Source.Engine.Graphics;
using Anathema.Source.Engine.Hook.SpeedHack;
using System.Diagnostics;

namespace Anathema.Source.Engine.Hook.Client
{
    public interface IHookCreator
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        ISpeedHackInterface GetSpeedHackInterface();

        void Uninject();

    } // End interface

} // End namespace