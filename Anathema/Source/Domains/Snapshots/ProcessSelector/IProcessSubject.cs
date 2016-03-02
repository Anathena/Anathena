using System.Diagnostics;

namespace Anathema
{
    interface IProcessSubject
    {
        void Subscribe(IProcessObserver Observer);
        void Unsubscribe(IProcessObserver Observer);
        void Notify(Process Process);
    }
}