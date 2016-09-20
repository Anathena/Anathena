using SharpDX.DirectInput;
using System;
using System.Collections.Generic;

namespace Ana.Source.Engine.InputCapture.Controller
{
    class ControllerCapture : IControllerSubject
    {
        private DirectInput DirectInput;
        private Guid JoystickGuid;
        private Joystick Joystick;

        public ControllerCapture()
        {
            // Initialize DirectInput
            DirectInput = new DirectInput();

            // Find a Joystick Guid
            JoystickGuid = Guid.Empty;
            foreach (DeviceInstance DeviceInstance in DirectInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                JoystickGuid = DeviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (JoystickGuid == Guid.Empty)
                foreach (DeviceInstance DeviceInstance in DirectInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    JoystickGuid = DeviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (JoystickGuid == Guid.Empty)
            {
                // TODO: Resort to like xInput or something here??? IDK
                return;
            }

            // Instantiate the joystick
            Joystick = new Joystick(DirectInput, JoystickGuid);

            // Query all suported ForceFeedback effects
            IList<EffectInfo> AllEffects = Joystick.GetEffects();
            foreach (EffectInfo EffectInfo in AllEffects)
                Console.WriteLine("Effect available {0}", EffectInfo.Name);

            Joystick.Properties.BufferSize = 128;
            Joystick.Acquire();
        }

        public void Update()
        {
            // TODO: maybe use this object
            JoystickState JoystickState = Joystick.GetCurrentState();

            // Poll events from joystick
            Joystick.Poll();
            JoystickUpdate[] Data = Joystick.GetBufferedData();
            foreach (JoystickUpdate State in Data)
                Console.WriteLine(State);
        }

        public void Subscribe(IControllerObserver Subject)
        {

        }

        public void Unsubscribe(IControllerObserver Subject)
        {

        }

    } // End class

} // End namespace