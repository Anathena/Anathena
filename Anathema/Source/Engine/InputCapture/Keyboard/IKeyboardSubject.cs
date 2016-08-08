using SharpDX.DirectInput;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.Keyboard
{
    public interface IKeyboardSubject : IInputCapture
    {
        void Subscribe(IKeyboardObserver Subject);

        void Unsubscribe(IKeyboardObserver Subject);

        void NotifyKeyRelease(Key Key);

        void NotifyKeyPress(Key Key);

        void NotifyKeyDown(Key Key);

        void NotifyAllDownKeys(HashSet<Key> DownKeys);

    } // End interface

} // End namespace