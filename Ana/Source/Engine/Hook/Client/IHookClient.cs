namespace Ana.Source.Engine.Hook.Client
{
    using Graphics;
    using SpeedHack;
    using System.Diagnostics;

    internal interface IHookClient
    {
        void Inject(Process target);

        IGraphicsInterface GetGraphicsInterface();

        ISpeedHackInterface GetSpeedHackInterface();

        void Ping();

        void Uninject();
    }
    //// End interface
}
//// End namespace