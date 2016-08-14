using Anathena.Source.Engine.Hook.Graphics;
using Anathena.Source.Engine.Hook.SpeedHack;
using System.Diagnostics;

namespace Anathena.Source.Engine.Hook.Client
{
    public interface IHookClient
    {
        void Inject(Process Target);

        IGraphicsInterface GetGraphicsInterface();

        ISpeedHackInterface GetSpeedHackInterface();

        void Ping();

        void Uninject();

    } // End interface

} // End namespace