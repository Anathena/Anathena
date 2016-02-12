using Anathema.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateMemoryEditor(OSInterface MemoryEditor);

    } // End interface

} // End namespace