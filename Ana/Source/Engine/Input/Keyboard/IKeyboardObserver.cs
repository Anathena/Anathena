namespace Ana.Source.Engine.InputCapture.Keyboard
{
    using SharpDX.DirectInput;
    using System.Collections.Generic;

    internal interface IKeyboardObserver
    {
        void OnKeyPress(Key key);

        void OnKeyRelease(Key key);

        void OnKeyDown(Key key);

        void OnUpdateAllDownKeys(HashSet<Key> pressedKeys);
    }
    //// End interface
}
//// End namespace