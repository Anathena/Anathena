using Anathema.Source.Engine.InputCapture.ControllerHook;
using Anathema.Source.Engine.InputCapture.MouseKeyHook;

namespace Anathema.Source.Engine.InputCapture
{
    public interface IInputManager
    {

        IKeyboardMouseEvents GetMouseKeyHook();

        IControllerEvents GetControllerHook();

    } // End class

} // End namespace