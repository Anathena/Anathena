using SharpDX.DirectInput;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.Keyboard
{
    public interface IKeyboardObserver
    {
        void OnKeyPress(Key Key);

        void OnKeyRelease(Key Key);

        void OnKeyDown(Key Key);

        void OnUpdateAllDownKeys(HashSet<Key> PressedKeys);

    } // End interface

} // End namespace