namespace Ana.Source.Engine.Input.Keyboard
{
    using Output;
    using SharpDX;
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class to capture keyboard input.
    /// </summary>
    internal class KeyboardCapture : IKeyboardSubject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardCapture" /> class.
        /// </summary>
        public KeyboardCapture()
        {
            this.Subjects = new List<IKeyboardObserver>();
            this.FindKeyboard();
        }

        /// <summary>
        /// Gets or sets the DirectX input object to collect system input.
        /// </summary>
        private DirectInput DirectInput { get; set; }

        /// <summary>
        /// Gets or sets the keyboard input object to capture input.
        /// </summary>
        private Keyboard Keyboard { get; set; }

        /// <summary>
        /// Gets or sets the current state of the keyboard.
        /// </summary>
        private KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        /// Gets or sets the previous state of the keyboard.
        /// </summary>
        private KeyboardState PreviousKeyboardState { get; set; }

        /// <summary>
        /// Gets or sets the subjects that are observing keyboard events.
        /// </summary>
        private List<IKeyboardObserver> Subjects { get; set; }

        /// <summary>
        /// Subscribes to keyboard capture events.
        /// </summary>
        /// <param name="subject">The observer to subscribe.</param>
        public void Subscribe(IKeyboardObserver subject)
        {
            if (this.Subjects.Contains(subject))
            {
                return;
            }

            this.Subjects.Add(subject);
        }

        /// <summary>
        /// Unsubscribes from keyboard capture events.
        /// </summary>
        /// <param name="subject">The observer to unsubscribe.</param>
        public void Unsubscribe(IKeyboardObserver subject)
        {
            if (!this.Subjects.Contains(subject))
            {
                return;
            }

            this.Subjects.Remove(subject);
        }

        /// <summary>
        /// Updates keyboard capture, gathering the input state and raising necessary events.
        /// </summary>
        public void Update()
        {
            try
            {
                this.CurrentKeyboardState = this.Keyboard.GetCurrentState();

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
                try
                {
                    this.Keyboard.Acquire();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Notifies observers of a key press event.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        public void NotifyKeyPress(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyPress(key);
            }
        }

        /// <summary>
        /// Notifies observers of a key release event.
        /// </summary>
        /// <param name="key">The key that was released.</param>
        public void NotifyKeyRelease(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyRelease(key);
            }
        }

        /// <summary>
        /// Notifies observers of a key down event.
        /// </summary>
        /// <param name="key">The key that is down.</param>
        public void NotifyKeyDown(Key key)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnKeyDown(key);
            }
        }

        /// <summary>
        /// Notifies observers of a set of key down events.
        /// </summary>
        /// <param name="downKeys">The keys that are down.</param>
        public void NotifyAllDownKeys(HashSet<Key> downKeys)
        {
            foreach (IKeyboardObserver keySubject in this.Subjects)
            {
                keySubject.OnUpdateAllDownKeys(downKeys);
            }
        }

        /// <summary>
        /// Finds any connected keyboard devices.
        /// </summary>
        private void FindKeyboard()
        {
            try
            {
                this.DirectInput = new DirectInput();
                this.Keyboard = new Keyboard(this.DirectInput);
                this.Keyboard.Acquire();

                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Info, "Keyboard device found");
            }
            catch (Exception ex)
            {
                OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Error, "Unable to acquire keyboard device: " + ex.ToString());
            }
        }
    }
    //// End class
}
//// End namespace