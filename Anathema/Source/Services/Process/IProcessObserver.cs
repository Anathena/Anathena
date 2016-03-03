using Anathema.Utils.OS;

namespace Anathema.Services.ProcessSelector
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateOSInterface(OSInterface OSInterface);

    } // End interface

} // End namespace