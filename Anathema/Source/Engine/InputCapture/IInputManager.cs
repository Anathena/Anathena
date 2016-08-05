using Anathema.Source.Engine.InputCapture.Controller;
using Anathema.Source.Engine.InputCapture.Keyboard;
using Anathema.Source.Engine.InputCapture.Mouse;

namespace Anathema.Source.Engine.InputCapture
{
    public interface IInputManager
    {
        IKeyboardSubject GetKeyboardCapture();

        IMouseSubject GetMouseCapture();

        IControllerSubject GetControllerCapture();

        void RegisterHotkey(InputBinding HotKey);

    } // End class

} // End namespace