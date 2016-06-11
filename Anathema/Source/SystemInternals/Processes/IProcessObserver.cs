using Anathema.Source.SystemInternals.OperatingSystems;

namespace Anathema.Source.SystemInternals.Processes
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateOSInterface(Engine OSInterface);

    } // End interface

} // End namespace