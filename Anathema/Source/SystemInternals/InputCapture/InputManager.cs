using Anathema.Source.SystemInternals.InputCapture.ControllerHook;
using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook;
using System;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture
{
    public class InputManager : IInputManager
    {
        private IKeyboardMouseEvents CaptureEvents;

        public IKeyboardMouseEvents GetMouseKeyHook()
        {
            CaptureEvents = MouseKeyCapture.GlobalEvents();

            CaptureEvents.KeyUp += GlobalHookKeyUp;
            CaptureEvents.KeyDown += GlobalHookKeyDown;

            return CaptureEvents;
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