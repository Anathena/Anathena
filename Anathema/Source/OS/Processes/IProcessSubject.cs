using System.Diagnostics;

namespace Anathema.Source.OS.Processes
{
    interface IProcessSubject
    {
        void Subscribe(IProcessObserver Observer);
        void Unsubscribe(IProcessObserver Observer);
        void Notify(Process Process);

    } // End interface

} // End namespace