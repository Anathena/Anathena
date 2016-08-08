using Anathena.Source.Engine.InputCapture.Controller;
using Anathena.Source.Engine.InputCapture.Keyboard;
using Anathena.Source.Engine.InputCapture.Mouse;
using Anathena.Source.Utils;
using System;

namespace Anathena.Source.Engine.InputCapture
{
    public class InputManager : RepeatedTask, IInputManager
    {
        private IControllerSubject ControllerSubject;
        private IKeyboardSubject KeyboardSubject;
        private IMouseSubject MouseSubject;

        // Collect input ~60 times per second
        private const Int32 InputCollectionIntervalMs = 17;

        public InputManager()
        {
            ControllerSubject = new ControllerCapture();
            KeyboardSubject = new KeyboardCapture();
            MouseSubject = new MouseCapture();

            Begin();
        }

        public override void Begin()
        {
            this.UpdateInterval = InputCollectionIntervalMs;

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

    } // End class

} // End namespace