using Ana.Source.Engine.InputCapture.Controller;
using Ana.Source.Engine.InputCapture.Keyboard;
using Ana.Source.Engine.InputCapture.Mouse;

namespace Ana.Source.Engine.InputCapture
{
    public interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();

    } // End class

} // End namespace