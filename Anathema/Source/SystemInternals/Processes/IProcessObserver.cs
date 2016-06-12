using Anathema.Source.SystemInternals.OperatingSystems;

namespace Anathema.Source.SystemInternals.Processes
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateEngine(Engine Engine);

    } // End interface

} // End namespace