namespace Ana.Source.Engine.InputCapture
{
    using Controller;
    using Keyboard;
    using Mouse;

    internal interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();
    }
    //// End class
}
//// End namespace