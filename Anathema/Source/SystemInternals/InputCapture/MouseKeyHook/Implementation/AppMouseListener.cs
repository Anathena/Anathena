using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal class AppMouseListener : MouseListener
    {
        public AppMouseListener() : base(HookHelper.HookAppMouse) { }

        protected override MouseEventExtArgs GetEventArgs(CallbackData Data)
        {
            return MouseEventExtArgs.FromRawDataApp(Data);
        }

    } // End class

} // End namespace