using Anathema.Source.Engine.InputCapture.ControllerCapture;
using Anathema.Source.Engine.InputCapture.KeyboardCapture;
using Anathema.Source.Engine.InputCapture.MouseCapture;

namespace Anathema.Source.Engine.InputCapture
{
    public class InputManager : IInputManager
    {
        private IControllerSubject ControllerSubject;
        private IKeyboardSubject KeyboardSubject;
        private IMouseSubject MouseSubject;

        public InputManager()
        {
            return;
            ControllerSubject = new ControllerCapture.ControllerCapture();
            KeyboardSubject = new KeyboardCapture.KeyboardCapture();
            MouseSubject = new MouseCapture.MouseCapture();

            Update();
        }

        public IControllerSubject GetControllerCapture()
        {
            return ControllerSubject;
        }

        public IKeyboardSubject GetKeyboardCapture()
        {
            return KeyboardSubject;
        }

        public IMouseSubject GetMouseCapture()
        {
            return MouseSubject;
        }

        private void Update()
        {
            return;

            ControllerSubject.Update();
            KeyboardSubject.Update();
            MouseSubject.Update();
        }

    } // End class

} // End namespace