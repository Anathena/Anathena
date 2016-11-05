namespace Ana.Source.Engine.Input.Keyboard
{
    using SharpDX.DirectInput;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for an object which will observe changes in keyboard input
    /// </summary>
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