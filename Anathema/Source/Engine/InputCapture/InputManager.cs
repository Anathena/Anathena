using Anathema.Source.Engine.InputCapture.ControllerHook;
using Anathema.Source.Engine.InputCapture.MouseKeyHook;
using System;
using System.Windows.Forms;

namespace Anathema.Source.Engine.InputCapture
{
    public class InputManager : IInputManager
    {
        private IKeyboardMouseEvents KeyboardMouseEvents;
        private IControllerEvents ControllerEvents;

        public InputManager()
        {
            ControllerEvents = new ControllerEvents();
            KeyboardMouseEvents = MouseKeyCapture.GlobalEvents();
        }

        public IKeyboardMouseEvents GetMouseKeyHook()
        {

            KeyboardMouseEvents.KeyUp += GlobalHookKeyUp;
            KeyboardMouseEvents.KeyDown += GlobalHookKeyDown;

            return KeyboardMouseEvents;
        }

        public IControllerEvents GetControllerHook()
        {
            throw new NotImplementedException();
        }

        private void GlobalHookKeyUp(Object Sender, KeyEventArgs E)
        {

        }

        private void GlobalHookKeyDown(Object Sender, KeyEventArgs E)
        {

        }

    } // End class

} // End namespace