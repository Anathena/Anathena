using Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation
{
    internal abstract class KeyListener : BaseListener, IKeyboardEvents
    {
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;

        protected KeyListener(Subscribe Subscribe) : base(Subscribe) { }

        public void InvokeKeyDown(KeyEventArgsExt E)
        {
            KeyEventHandler Handler = KeyDown;

            if (Handler == null || E.Handled || !E.IsKeyDown)
                return;

            Handler(this, E);
        }

        public void InvokeKeyPress(KeyPressEventArgsExt E)
        {
            KeyPressEventHandler Handler = KeyPress;

            if (Handler == null || E.Handled || E.IsNonChar)
                return;

            Handler(this, E);
        }

        public void InvokeKeyUp(KeyEventArgsExt E)
        {
            KeyEventHandler Handler = KeyUp;

            if (Handler == null || E.Handled || !E.IsKeyUp)
                return;

            Handler(this, E);
        }

        protected override Boolean Callback(CallbackData Data)
        {
            IEnumerable<KeyPressEventArgsExt> PressEventArgs = GetPressEventArgs(Data);
            KeyEventArgsExt KeyDownUp = GetDownUpEventArgs(Data);

            InvokeKeyDown(KeyDownUp);

            foreach (KeyPressEventArgsExt PressEventArg in PressEventArgs)
                InvokeKeyPress(PressEventArg);

            InvokeKeyUp(KeyDownUp);

            return !KeyDownUp.Handled;
        }

        protected abstract IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData Data);

        protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData Data);

    } // End class

} // End namespace