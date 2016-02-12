using Anathema.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateMemoryEditor(WindowsOSInterface MemoryEditor);

    } // End interface

} // End namespace