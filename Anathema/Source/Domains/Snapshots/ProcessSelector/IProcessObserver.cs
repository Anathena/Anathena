using Anathema.MemoryManagement;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateOSInterface(OSInterface OSInterface);

    } // End interface

} // End namespace