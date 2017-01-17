namespace Ana.Source.Engine.Input
{
    using Controller;
    using Keyboard;
    using Mouse;
    using System;
    using Utils.Tasks;

    /// <summary>
    /// Manages all input devices and is responsible for updating them.
    /// </summary>
    internal class InputManager : ScheduledTask, IInputManager
    {
        /// <summary>
        /// The rate at which to collect input in ms. Currently ~60 times per second.
        /// </summary>
        private const Int32 InputCollectionIntervalMs = 1000 / 60;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager" /> class.
        /// </summary>
        public InputManager() : base(isRepeated: true)
        {
            this.ControllerSubject = new ControllerCapture();
            this.KeyboardSubject = new KeyboardCapture();
            this.MouseSubject = new MouseCapture();

            this.Begin();
        }

        /// <summary>
        /// Gets or sets the controller capture interface.
        /// </summary>
        private IControllerSubject ControllerSubject { get; set; }

        /// <summary>
        /// Gets or sets the keyboard capture interface.
        /// </summary>
        private IKeyboardSubject KeyboardSubject { get; set; }

        /// <summary>
        /// Gets or sets the mouse capture interface.
        /// </summary>
        private IMouseSubject MouseSubject { get; set; }

        /// <summary>
        /// Gets the keyboard capture interface.
        /// </summary>
        /// <returns>The keyboard capture interface.</returns>
        public IKeyboardSubject GetKeyboardCapture()
        {
            return this.KeyboardSubject;
        }

        /// <summary>
        /// Gets the mouse capture interface.
        /// </summary>
        /// <returns>The mouse capture interface.</returns>
        public IControllerSubject GetControllerCapture()
        {
            return this.ControllerSubject;
        }

        /// <summary>
        /// Gets the controller capture interface.
        /// </summary>
        /// <returns>The controller capture interface.</returns>
        public IMouseSubject GetMouseCapture()
        {
            return this.MouseSubject;
        }

        /// <summary>
        /// Begins input capture.
        /// </summary>
        protected override void OnBegin()
        {
            this.UpdateInterval = InputManager.InputCollectionIntervalMs;
        }

        /// <summary>
        /// Updates the input capture devices, polling the system for changes on each device.
        /// </summary>
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