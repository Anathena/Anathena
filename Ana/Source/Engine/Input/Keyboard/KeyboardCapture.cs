namespace Ana.Source.Engine.Input.Keyboard
{
    using SharpDX;
    using SharpDX.DirectInput;
    using System.Collections.Generic;

    internal class KeyboardCapture : IKeyboardSubject
    {
        public KeyboardCapture()
        {
            SharpDX.RawInput.Device.RegisterDevice(SharpDX.Multimedia.UsagePage.Generic, SharpDX.Multimedia.UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
            this.Subjects = new List<IKeyboardObserver>();
            this.DirectInput = new DirectInput();
            this.Keyboard = new Keyboard(this.DirectInput);
            this.Keyboard.Acquire();
        }

        private DirectInput DirectInput { get; set; }

        private Keyboard Keyboard { get; set; }

        private KeyboardState CurrentKeyboardState { get; set; }

        private KeyboardState PreviousKeyboardState { get; set; }

        private List<IKeyboardObserver> Subjects { get; set; }

        public void Subscribe(IKeyboardObserver subject)
        {
            if (this.Subjects.Contains(subject))
            {
                return;
            }

            this.Subjects.Add(subject);
        }

        public void Unsubscribe(IKeyboardObserver subject)
        {
            if (!this.Subjects.Contains(subject))
            {
                return;
            }

            this.Subjects.Remove(subject);
        }

        public void Update()
        {
            try
            {
                this.CurrentKeyboardState = Keyboard.GetCurrentState();

                if (this.PreviousKeyboardState == null)
                {
                    this.PreviousKeyboardState = this.CurrentKeyboardState;
                    return;
                }

                if (this.CurrentKeyboardState == null || this.PreviousKeyboardState == null)
                {
                    return;
                }

                HashSet<Key> pressedKeys = new HashSet<Key>(this.CurrentKeyboardState.PressedKeys);
                HashSet<Key> releasedKeys = new HashSet<Key>(this.PreviousKeyboardState.PressedKeys);

                this.NotifyAllDownKeys(pressedKeys);

                pressedKeys.ExceptWith(this.PreviousKeyboardState.PressedKeys);
                releasedKeys.ExceptWith(this.CurrentKeyboardState.PressedKeys);

                foreach (Key key in pressedKeys)
                {
                    this.NotifyKeyPress(key);
                }

                foreach (Key key in releasedKeys)
                {
                    this.NotifyKeyRelease(key);
                }

                foreach (Key key in this.CurrentKeyboardState.PressedKeys)
                {
                    this.NotifyKeyDown(key);
                }

                this.PreviousKeyboardState = this.CurrentKeyboardState;
            }
            catch (SharpDXException)
            {
                this.Keyboard.Acquire();
            }
        }

        public void NotifyKeyPress(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyPress(key);
            }
        }

        public void NotifyKeyRelease(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyRelease(key);
            }
        }

        public void NotifyKeyDown(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyDown(key);
            }
        }

        public void NotifyAllDownKeys(HashSet<Key> downKeys)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnUpdateAllDownKeys(downKeys);
            }
        }
    }
    //// End class
}
//// End namespace