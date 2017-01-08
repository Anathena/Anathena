namespace Ana.Source.Engine.Input.Keyboard
{
    using SharpDX.DirectInput;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for an object which will observe changes in keyboard input.
    /// </summary>
    internal interface IKeyboardObserver
    {
        /// <summary>
        /// Event received when a key is pressed.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        void OnKeyPress(Key key);

        /// <summary>
        /// Event received when a key is released.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        void OnKeyRelease(Key key);

        /// <summary>
        /// Event received when a key is down.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        void OnKeyDown(Key key);

        /// <summary>
        /// Event received when a set of keys are down.
        /// </summary>
        /// <param name="pressedKeys">The down keys.</param>
        void OnUpdateAllDownKeys(HashSet<Key> pressedKeys);
    }
    //// End interface
}
//// End namespace