namespace Ana.Source.Engine.Input
{
    using Controller;
    using Keyboard;
    using Mouse;
    using System;
    using Utils;

    internal class InputManager : RepeatedTask, IInputManager
    {
        // Collect input ~60 times per second
        private const Int32 InputCollectionIntervalMs = 1000 / 60;

        public InputManager()
        {
            this.ControllerSubject = new ControllerCapture();
            this.KeyboardSubject = new KeyboardCapture();
            this.MouseSubject = new MouseCapture();

            this.Begin();
        }

        private IControllerSubject ControllerSubject { get; set; }

        private IKeyboardSubject KeyboardSubject { get; set; }

        private IMouseSubject MouseSubject { get; set; }

        public override void Begin()
        {
            this.UpdateInterval = InputManager.InputCollectionIntervalMs;

            base.Begin();
        }

        public IControllerSubject GetControllerCapture()
        {
            return this.ControllerSubject;
        }

        public IKeyboardSubject GetKeyboardCapture()
        {
            return this.KeyboardSubject;
        }

        public IMouseSubject GetMouseCapture()
        {
            return this.MouseSubject;
        }

        protected override void OnUpdate()
        {
            this.ControllerSubject.Update();
            this.KeyboardSubject.Update();
            this.MouseSubject.Update();
        }
    }
    //// End class
}
//// End namespace