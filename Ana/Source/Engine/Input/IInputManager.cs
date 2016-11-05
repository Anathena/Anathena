namespace Ana.Source.Engine.Input
{
    using Controller;
    using Keyboard;
    using Mouse;

    /// <summary>
    /// An interface defining an object which is responsable for managing all input devices
    /// </summary>
    internal interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();
    }
    //// End class
}
//// End namespace