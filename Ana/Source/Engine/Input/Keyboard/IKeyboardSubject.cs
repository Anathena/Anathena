namespace Ana.Source.Engine.InputCapture.Keyboard
{
    using SharpDX.DirectInput;
    using System.Collections.Generic;

    internal interface IKeyboardSubject : IInputCapture
    {
        void Subscribe(IKeyboardObserver subject);

        void Unsubscribe(IKeyboardObserver subject);

        void NotifyKeyRelease(Key key);

        void NotifyKeyPress(Key key);

        void NotifyKeyDown(Key key);

        void NotifyAllDownKeys(HashSet<Key> downKeys);
    }
    //// End interface
}
//// End namespace