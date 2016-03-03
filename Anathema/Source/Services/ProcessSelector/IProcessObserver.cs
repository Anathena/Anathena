using Anathema.MemoryManagement;
using Anathema.Utils.OS;

namespace Anathema
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateOSInterface(OSInterface OSInterface);

    } // End interface

} // End namespace