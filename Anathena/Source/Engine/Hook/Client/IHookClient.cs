using Ana.Source.Engine.Hook.Graphics;
using Ana.Source.Engine.Hook.SpeedHack;
using System.Diagnostics;

namespace Ana.Source.Engine.Hook.Client
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