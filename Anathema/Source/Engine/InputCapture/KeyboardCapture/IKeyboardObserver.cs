using SharpDX.DirectInput;

namespace Anathema.Source.Engine.InputCapture.KeyboardCapture
{
    public interface IKeyboardObserver
    {
        void OnKeyPress(Key Key);

        void OnKeyDown(Key Key);

        void OnKeyUp(Key Key);

    } // End interface

} // End namespace