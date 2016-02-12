using Anathema.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateMemoryEditor(MemoryEditor MemoryEditor);

    } // End interface

} // End namespace