using SharpDX.DirectInput;

namespace Anathema.Source.Engine.InputCapture.Keyboard
{
    public interface IKeyboardSubject : IInputCapture
    {
        void Subscribe(IKeyboardObserver Subject);

        void Unsubscribe(IKeyboardObserver Subject);

        void NotifyKeyDown(Key Key);

        void NotifyKeyPress(Key Key);

        void NotifyKeyUp(Key Key);

    } // End interface

} // End namespace