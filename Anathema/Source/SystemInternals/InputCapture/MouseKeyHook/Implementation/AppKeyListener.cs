using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;
using System.Collections.Generic;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal class AppKeyListener : KeyListener
    {
        public AppKeyListener() : base(HookHelper.HookAppKeyboard) { }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData Data)
        {
            return KeyPressEventArgsExt.FromRawDataApp(Data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData Data)
        {
            return KeyEventArgsExt.FromRawDataApp(Data);
        }

    } // End class

} // End namespace