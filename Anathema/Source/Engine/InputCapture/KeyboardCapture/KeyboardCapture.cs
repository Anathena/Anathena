using SharpDX.DirectInput;
using SharpDX.RawInput;
using System;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture.KeyboardCapture
{
    class KeyboardCapture : IKeyboardSubject
    {
        private DirectInput DirectInput;
        private Keyboard Keyboard;

        private KeyboardState CurrentKeyboardState;
        private KeyboardState PreviousKeyboardState;

        private List<IKeyboardObserver> Subjects;

        public KeyboardCapture()
        {
            // Initialize DirectInput
            SharpDX.RawInput.Device.RegisterDevice(SharpDX.Multimedia.UsagePage.Generic, SharpDX.Multimedia.UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
            SharpDX.RawInput.Device.KeyboardInput += new System.EventHandler<KeyboardInputEventArgs>(Device_KeyboardInput);

            /*
            DirectInput = new DirectInput();
            Subjects = new List<IKeyboardObserver>();

            Keyboard = new Keyboard(DirectInput);*/
        }

        private void Device_KeyboardInput(Object Sender, KeyboardInputEventArgs Args)
        {

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

        public void NotifyKeyPress(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyPress(Key);
        }

        public void NotifyKeyDown(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyDown(Key);
        }

        public void NotifyKeyUp(Key Key)
        {
            foreach (IKeyboardObserver KeySubject in Subjects)
                KeySubject.OnKeyUp(Key);
        }

        public void Update()
        {
            CurrentKeyboardState = Keyboard.GetCurrentState();



            PreviousKeyboardState = CurrentKeyboardState;
        }

    } // End class

} // End namespace