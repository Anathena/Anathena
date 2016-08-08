using Anathena.Source.Engine.InputCapture.Controller;
using Anathena.Source.Engine.InputCapture.Keyboard;
using Anathena.Source.Engine.InputCapture.Mouse;

namespace Anathena.Source.Engine.InputCapture
{
    public interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();

    } // End class

} // End namespace