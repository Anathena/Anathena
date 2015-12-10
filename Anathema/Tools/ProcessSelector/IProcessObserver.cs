using Binarysharp.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void NotifyProcessSelectorCreated();
        void UpdateMemoryEditor(MemorySharp MemoryEditor);
    }
}
