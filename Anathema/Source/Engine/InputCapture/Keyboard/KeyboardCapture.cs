using SharpDX;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.Keyboard
{
    class KeyboardCapture : IKeyboardSubject
    {
        private DirectInput DirectInput;
        private SharpDX.DirectInput.Keyboard Keyboard;

        private KeyboardState CurrentKeyboardState;
        private KeyboardState PreviousKeyboardState;

        private List<IKeyboardObserver> Subjects;

        public KeyboardCapture()
        {
            SharpDX.RawInput.Device.RegisterDevice(SharpDX.Multimedia.UsagePage.Generic, SharpDX.Multimedia.UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
            Subjects = new List<IKeyboardObserver>();
            DirectInput = new DirectInput();
            Keyboard = new SharpDX.DirectInput.Keyboard(DirectInput);
            Keyboard.Acquire();
        }

        public void Subscribe(IKeyboardObserver Subject)
        {
            if (Subjects.Contains(Subject))
                return;

            Subjects.Add(Subject);
        }

        public void Unsubscribe(IKeyboardObserver Subject)
        {
            if (!Subjects.Contains(Subject))
                return;

            Subjects.Remove(Subject);
        }

        public void Update()
        {
            try
            {
                CurrentKeyboardState = Keyboard.GetCurrentState();

                if (PreviousKeyboardState == null)
                {
                    PreviousKeyboardState = CurrentKeyboardState;
                    return;
                }

                if (CurrentKeyboardState == null || PreviousKeyboardState == null)
                    return;

                HashSet<Key> PressedKeys = new HashSet<Key>(CurrentKeyboardState.PressedKeys);
                HashSet<Key> ReleasedKeys = new HashSet<Key>(PreviousKeyboardState.PressedKeys);

                PressedKeys.ExceptWith(PreviousKeyboardState.PressedKeys);
                ReleasedKeys.ExceptWith(CurrentKeyboardState.PressedKeys);

                foreach (Key Key in PressedKeys)
                    NotifyKeyPress(Key);

                foreach (Key Key in ReleasedKeys)
                    NotifyKeyRelease(Key);

                foreach (Key Key in CurrentKeyboardState.PressedKeys)
                    NotifyKeyDown(Key);

                PreviousKeyboardState = CurrentKeyboardState;
            }
            catch (SharpDXException)
            {
                Keyboard.Acquire();
            }
        }

        #region Events

        public void NotifyKeyPress(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyPress(Key);
        }

        public void NotifyKeyRelease(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyRelease(Key);
        }

        public void NotifyKeyDown(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyDown(Key);
        }

        #endregion

    } // End class

} // End namespace