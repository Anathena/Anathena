using Binarysharp.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeObserver();
        void UpdateMemoryEditor(MemorySharp MemoryEditor);
    }
}
