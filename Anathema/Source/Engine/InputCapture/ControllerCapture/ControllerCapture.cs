using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anathema.Source.Engine.InputCapture.ControllerCapture
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
            return;
            // TODO: maybe use this object
            var k = Joystick.GetCurrentState();

            // Poll events from joystick
            Task.Run(() =>
            {
                while (true)
                {
                    Joystick.Poll();
                    JoystickUpdate[] Data = Joystick.GetBufferedData();
                    foreach (JoystickUpdate State in Data)
                        Console.WriteLine(State);
                }
            });
        }

    } // End class

} // End namespace