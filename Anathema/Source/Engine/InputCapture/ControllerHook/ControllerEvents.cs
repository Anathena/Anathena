using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anathema.Source.Engine.InputCapture.ControllerHook
{
    class ControllerEvents : IControllerEvents
    {
        public ControllerEvents()
        {
            // Initialize DirectInput
            DirectInput DirectInput = new DirectInput();

            // Find a Joystick Guid
            Guid JoystickGuid = Guid.Empty;

            foreach (DeviceInstance DeviceInstance in DirectInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                JoystickGuid = DeviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (JoystickGuid == Guid.Empty)
                foreach (DeviceInstance DeviceInstance in DirectInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    JoystickGuid = DeviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (JoystickGuid == Guid.Empty)
            {

            }

            // Instantiate the joystick
            Joystick Joystick = new Joystick(DirectInput, JoystickGuid);

            Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", JoystickGuid);

            // Query all suported ForceFeedback effects
            IList<EffectInfo> AllEffects = Joystick.GetEffects();
            foreach (EffectInfo EffectInfo in AllEffects)
                Console.WriteLine("Effect available {0}", EffectInfo.Name);

            // Set BufferSize in order to use buffered data.
            Joystick.Properties.BufferSize = 128;

            // Acquire the joystick
            Joystick.Acquire();

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