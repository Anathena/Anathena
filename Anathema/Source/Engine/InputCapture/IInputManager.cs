using Anathema.Source.Engine.InputCapture.ControllerCapture;
using Anathema.Source.Engine.InputCapture.KeyboardCapture;
using Anathema.Source.Engine.InputCapture.MouseCapture;

namespace Anathema.Source.Engine.InputCapture
{
    public interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();

    } // End class

} // End namespace