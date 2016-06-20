using Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation
{
    internal class GlobalKeyListener : KeyListener
    {
        public GlobalKeyListener() : base(HookHelper.HookGlobalKeyboard) { }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData Data)
        {
            return KeyPressEventArgsExt.FromRawDataGlobal(Data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData Data)
        {
            return KeyEventArgsExt.FromRawDataGlobal(Data);
        }

    } // End class

} // End namespace