using System.Diagnostics;

namespace Anathema
{
    interface ISnapshotManagerSubject
    {
        void Subscribe(ISnapshotManagerObserver Observer);
        void Unsubscribe(ISnapshotManagerObserver Observer);
        void Notify();
    }
}