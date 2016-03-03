using Anathema.Utils.OS;

namespace Anathema.Services.ProcessManager
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateOSInterface(OSInterface OSInterface);

    } // End interface

} // End namespace