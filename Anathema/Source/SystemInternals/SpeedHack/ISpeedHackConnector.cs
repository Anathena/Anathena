using System.Diagnostics;

namespace Anathema.Source.SystemInternals.SpeedHack
{
    public interface ISpeedHackConnector
    {
        void Inject(Process Target);

        ISpeedHackInterface GetSpeedHackInterface();

        void Uninject();

    } // End interface

} // End namespace