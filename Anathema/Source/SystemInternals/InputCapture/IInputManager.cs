using Anathema.Source.SystemInternals.InputCapture.ControllerHook;
using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook;

namespace Anathema.Source.SystemInternals.InputCapture
{
    public interface IInputManager
    {

        IKeyboardMouseEvents GetMouseKeyHook();

        IControllerEvents GetControllerHook();

    } // End class

} // End namespace