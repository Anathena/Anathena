using Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation
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