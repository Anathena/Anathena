using Binarysharp.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateMemoryEditor(MemorySharp MemoryEditor);
    }
}
