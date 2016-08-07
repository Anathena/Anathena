using Anathema.Source.Engine.InputCapture.Controller;
using Anathema.Source.Engine.InputCapture.Keyboard;
using Anathema.Source.Engine.InputCapture.Mouse;
using Anathema.Source.Utils;
using System;

namespace Anathema.Source.Engine.InputCapture
{
    public class InputManager : RepeatedTask, IInputManager
    {
        private IControllerSubject ControllerSubject;
        private IKeyboardSubject KeyboardSubject;
        private IMouseSubject MouseSubject;

        private const Int32 InputCollectionInterval = 200;

        public InputManager()
        {
            ControllerSubject = new ControllerCapture();
            KeyboardSubject = new KeyboardCapture();
            MouseSubject = new MouseCapture();

            Begin();
        }

        public override void Begin()
        {
            this.UpdateInterval = InputCollectionInterval;

            base.Begin();
        }

        protected override void Update()
        {
            ControllerSubject.Update();
            KeyboardSubject.Update();
            MouseSubject.Update();
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

        public void RegisterHotkey(HotKeys HotKey)
        {

        }

    } // End class

} // End namespace