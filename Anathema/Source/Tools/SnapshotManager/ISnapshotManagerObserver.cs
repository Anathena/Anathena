using Binarysharp.MemoryManagement;

namespace Anathema
{
    interface ISnapshotManagerObserver
    {
        void InitializeSnapshotManagerObserver();
        void SnapshotUpdated();
    }
}
