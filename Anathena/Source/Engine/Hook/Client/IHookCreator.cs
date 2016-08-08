using Anathena.Source.Engine.Graphics;
using Anathena.Source.Engine.Hook.SpeedHack;
using System.Diagnostics;

namespace Anathena.Source.Engine.Hook.Client
{
    public interface IHookCreator
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        ISpeedHackInterface GetSpeedHackInterface();

        void Uninject();

    } // End interface

} // End namespace