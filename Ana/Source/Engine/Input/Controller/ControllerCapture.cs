namespace Ana.Source.Engine.Input.Controller
{
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;

    internal class ControllerCapture : IControllerSubject
    {
        public ControllerCapture()
        {
            // Initialize DirectInput
            this.DirectInput = new DirectInput();

            // Find a Joystick Guid
            this.JoystickGuid = Guid.Empty;
            foreach (DeviceInstance deviceInstance in this.DirectInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                this.JoystickGuid = deviceInstance.InstanceGuid;
            }

            // If Gamepad not found, look for a Joystick
            if (this.JoystickGuid == Guid.Empty)
            {
                foreach (DeviceInstance deviceInstance in this.DirectInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                {
                    this.JoystickGuid = deviceInstance.InstanceGuid;
                }
            }

            // If Joystick not found, throws an error
            if (this.JoystickGuid == Guid.Empty)
            {
                // TODO: Resort to like xInput or something here??? IDK
                return;
            }

            // Instantiate the joystick
            this.Joystick = new Joystick(this.DirectInput, this.JoystickGuid);

            // Query all suported ForceFeedback effects
            IList<EffectInfo> allEffects = this.Joystick.GetEffects();

            if (allEffects != null)
            {
                foreach (EffectInfo effectInfo in allEffects)
                {
                    Console.WriteLine("Effect available {0}", effectInfo.Name);
                }
            }

            this.Joystick.Properties.BufferSize = 128;
            this.Joystick.Acquire();
        }

        private DirectInput DirectInput { get; set; }

        private Guid JoystickGuid { get; set; }

        private Joystick Joystick { get; set; }

        public void Update()
        {
            // TODO: maybe use this object
            JoystickState joystickState = this.Joystick.GetCurrentState();

            // Poll events from joystick
            this.Joystick.Poll();
            JoystickUpdate[] data = this.Joystick.GetBufferedData();

            if (data == null)
            {
                return;
            }

            foreach (JoystickUpdate state in data)
            {
                Console.WriteLine(state);
            }
        }

        public void Subscribe(IControllerObserver subject)
        {
        }

        public void Unsubscribe(IControllerObserver subject)
        {
        }
    }
    //// End class
}
//// End namespace