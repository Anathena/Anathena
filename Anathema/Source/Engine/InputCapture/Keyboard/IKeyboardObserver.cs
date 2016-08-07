using SharpDX.DirectInput;

namespace Anathema.Source.Engine.InputCapture.Keyboard
{
    public interface IKeyboardObserver
    {
        void OnKeyPress(Key Key);

        void OnKeyRelease(Key Key);

        void OnKeyDown(Key Key);

    } // End interface

} // End namespace