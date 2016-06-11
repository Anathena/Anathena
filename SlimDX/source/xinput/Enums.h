/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#pragma once

#include <xinput.h>

namespace SlimDX
{
	namespace XInput
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.
		
		/// <summary>
		/// Specifies possible XInput devices that can use batteries.
		/// </summary>
		public enum class BatteryDeviceType : System::Int32
		{
			/// <summary>
			/// An XInput gamepad.
			/// </summary>
			Gamepad = BATTERY_DEVTYPE_GAMEPAD,

			/// <summary>
			/// An XInput headset.
			/// </summary>
			Headset = BATTERY_DEVTYPE_HEADSET
		};
		
		/// <summary>
		/// Specifies possible values indicating the amount of charge remaining in a battery.
		/// </summary>
		/// <remarks>
		/// These are only valid for wireless, connected devices, with known battery types. The amount of use time remaining depends on the type of device.
		/// </remarks>
		public enum class BatteryLevel : System::Int32
		{
			/// <summary>
			/// The battery is completely empty.
			/// </summary>
			Empty = BATTERY_LEVEL_EMPTY,

			/// <summary>
			/// The battery is running low.
			/// </summary>
			Low = BATTERY_LEVEL_LOW,

			/// <summary>
			/// The battery is at a medium charge.
			/// </summary>
			Medium = BATTERY_LEVEL_MEDIUM,

			/// <summary>
			/// The battery is fully charged.
			/// </summary>
			Full = BATTERY_LEVEL_FULL
		};
		
		/// <summary>
		/// Specifies possible battery types.
		/// </summary>
		public enum class BatteryType : System::Int32
		{
			/// <summary>
			/// The device is disconnected, and therefore the battery type cannot be determined.
			/// </summary>
			Disconnected = BATTERY_TYPE_DISCONNECTED,

			/// <summary>
			/// The device is wired, and therefore does not have a battery.
			/// </summary>
			Wired = BATTERY_TYPE_WIRED,

			/// <summary>
			/// The device is using an Alkaline battery.
			/// </summary>
			Alkaline = BATTERY_TYPE_ALKALINE,

			/// <summary>
			/// The device is using a Nickel Metal Hydride battery.
			/// </summary>
			NickelMetalHydride = BATTERY_TYPE_NIMH,

			/// <summary>
			/// The device's battery type is unknown.
			/// </summary>
			Unknown = BATTERY_TYPE_UNKNOWN
		};
		
		/// <summary>
		/// Specifies capabilities for XInput devices.
		/// </summary>
		[System::Flags]
		public enum class CapabilityFlags : System::Int32
		{
			/// <summary>
			/// No extra capabilities specfied.
			/// </summary>
			None = 0,

			/// <summary>
			/// The device has an integrated voice device.
			/// </summary>
			VoiceSupported = XINPUT_CAPS_VOICE_SUPPORTED
		};
		
		/// <summary>
		/// Specifies possible controller types.
		/// </summary>
		public enum class DeviceType : System::Int32
		{
			/// <summary>
			/// The device is a game controller.
			/// </summary>
			Gamepad = XINPUT_DEVTYPE_GAMEPAD
		};
		
		/// <summary>
		/// Defines the type of devices that will be returned by a query.
		/// </summary>
		public enum class DeviceQueryType : System::Int32
		{
			/// <summary>
			/// Return any and all devices.
			/// </summary>
			Any = 0,

			/// <summary>
			/// Query for gamepads only.
			/// </summary>
			Gamepad = XINPUT_FLAG_GAMEPAD
		};
		
		/// <summary>
		/// Specifies possible sub-types for XInput devices.
		/// </summary>
		public enum class DeviceSubtype : System::Int32
		{
			/// <summary>
			/// The device is an arcade stick.
			/// </summary>
			ArcadeStick = XINPUT_DEVSUBTYPE_ARCADE_STICK,

			/// <summary>
			/// The device is a generic gamepad.
			/// </summary>
			Gamepad = XINPUT_DEVSUBTYPE_GAMEPAD,

			/// <summary>
			/// The device is a racing wheel.
			/// </summary>
			Wheel = XINPUT_DEVSUBTYPE_WHEEL,

			/// <summary>
			/// The device is a dancing pad.
			/// </summary>
			DancePad = XINPUT_DEVSUBTYPE_DANCE_PAD,

			/// <summary>
			/// The device is a flight stick.
			/// </summary>
			FlightStick = XINPUT_DEVSUBTYPE_FLIGHT_SICK
		};
		
	#pragma warning(disable: 4369)
		/// <summary>
		/// Describes a set of pressed buttons on a gamepad.
		/// </summary>
		[System::Flags]
		public enum class GamepadButtonFlags : System::Int16
		{
			/// <summary>
			/// No buttons are pressed.
			/// </summary>
			None = 0,

			/// <summary>
			/// The Up button on the D-Pad is pressed.
			/// </summary>
			DPadUp = XINPUT_GAMEPAD_DPAD_UP,

			/// <summary>
			/// The Down button on the D-Pad is pressed.
			/// </summary>
			DPadDown = XINPUT_GAMEPAD_DPAD_DOWN,

			/// <summary>
			/// The Left button on the D-Pad is pressed.
			/// </summary>
			DPadLeft = XINPUT_GAMEPAD_DPAD_LEFT,

			/// <summary>
			/// The Right button on the D-Pad is pressed.
			/// </summary>
			DPadRight = XINPUT_GAMEPAD_DPAD_RIGHT,

			/// <summary>
			/// The start button is pressed.
			/// </summary>
			Start = XINPUT_GAMEPAD_START,

			/// <summary>
			/// The back button is pressed.
			/// </summary>
			Back = XINPUT_GAMEPAD_BACK,

			/// <summary>
			/// The left thumb button is pressed.
			/// </summary>
			LeftThumb = XINPUT_GAMEPAD_LEFT_THUMB,  

			/// <summary>
			/// The right thumb button is pressed.
			/// </summary>
			RightThumb = XINPUT_GAMEPAD_RIGHT_THUMB,   

			/// <summary>
			/// The left shoulder button is pressed.
			/// </summary>
			LeftShoulder = XINPUT_GAMEPAD_LEFT_SHOULDER,

			/// <summary>
			/// The right shoulder button is pressed.
			/// </summary>
			RightShoulder = XINPUT_GAMEPAD_RIGHT_SHOULDER,

			/// <summary>
			/// The A button is pressed.
			/// </summary>
			A = XINPUT_GAMEPAD_A,

			/// <summary>
			/// The B button is pressed.
			/// </summary>
			B = XINPUT_GAMEPAD_B,

			/// <summary>
			/// The X button is pressed.
			/// </summary>
			X = XINPUT_GAMEPAD_X,

			/// <summary>
			/// The Y button is pressed.
			/// </summary>
			Y = XINPUT_GAMEPAD_Y
		};
	#pragma warning(default: 4369)
		
		/// <summary>
		/// Specifies possible key codes for gamepad buttons.
		/// </summary>
		public enum class GamepadKeyCode : System::Int32
		{
			/// <summary>
			/// The A button.
			/// </summary>
			A = VK_PAD_A,

			/// <summary>
			/// The B button.
			/// </summary>
			B = VK_PAD_B,

			/// <summary>
			/// The X button.
			/// </summary>
			X = VK_PAD_X,

			/// <summary>
			/// The Y button.
			/// </summary>
			Y = VK_PAD_Y,

			/// <summary>
			/// The right shoulder button.
			/// </summary>
			RightShoulder = VK_PAD_RSHOULDER,

			/// <summary>
			/// The left shoulder button.
			/// </summary>
			LeftShoulder = VK_PAD_LSHOULDER,

			/// <summary>
			/// The left trigger button.
			/// </summary>
			LeftTrigger = VK_PAD_LTRIGGER,

			/// <summary>
			/// The right trigger button.
			/// </summary>
			RightTrigger = VK_PAD_RTRIGGER,

			/// <summary>
			/// The Up button on the D-Pad.
			/// </summary>
			DPadUp = VK_PAD_DPAD_UP,

			/// <summary>
			/// The Down button on the D-Pad.
			/// </summary>
			DPadDown = VK_PAD_DPAD_DOWN,

			/// <summary>
			/// The Left button on the D-Pad.
			/// </summary>
			DPadLeft = VK_PAD_DPAD_LEFT,

			/// <summary>
			/// The Right button on the D-Pad.
			/// </summary>
			DPadRight = VK_PAD_DPAD_RIGHT,

			/// <summary>
			/// The start button.
			/// </summary>
			Start = VK_PAD_START,

			/// <summary>
			/// The back button.
			/// </summary>
			Back = VK_PAD_BACK,

			/// <summary>
			/// A complete press of the left thumb stick.
			/// </summary>
			LeftThumbPress = VK_PAD_LTHUMB_PRESS,

			/// <summary>
			/// A complete press of the right thumb stick.
			/// </summary>
			RightThumbPress = VK_PAD_RTHUMB_PRESS,

			/// <summary>
			/// Pushing the left thumb stick up.
			/// </summary>
			LeftThumbUp = VK_PAD_LTHUMB_UP,

			/// <summary>
			/// Pushing the left thumb stick down.
			/// </summary>
			LeftThumbDown = VK_PAD_LTHUMB_DOWN,

			/// <summary>
			/// Pushing the left thumb stick left.
			/// </summary>
			LeftThumbLeft = VK_PAD_LTHUMB_LEFT,

			/// <summary>
			/// Pushing the left thumb stick right.
			/// </summary>
			LeftThumbRight = VK_PAD_LTHUMB_RIGHT,

			/// <summary>
			/// Pushing the left thumb stick up and to the left.
			/// </summary>
			LeftThumbUpLeft = VK_PAD_LTHUMB_UPLEFT,

			/// <summary>
			/// Pushing the left thumb stick up and to the right.
			/// </summary>
			LeftThumbUpRight = VK_PAD_LTHUMB_UPRIGHT,

			/// <summary>
			/// Pushing the left thumb stick down and to the left.
			/// </summary>
			LeftThumbDownLeft = VK_PAD_LTHUMB_DOWNLEFT,

			/// <summary>
			/// Pushing the left thumb stick down and to the right.
			/// </summary>
			LeftThumbDownRight = VK_PAD_LTHUMB_DOWNRIGHT,

			/// <summary>
			/// Pushing the right thumb stick up.
			/// </summary>
			RightThumbUp = VK_PAD_RTHUMB_UP,

			/// <summary>
			/// Pushing the right thumb stick down.
			/// </summary>
			RightThumbDown = VK_PAD_RTHUMB_DOWN,

			/// <summary>
			/// Pushing the right thumb stick left.
			/// </summary>
			RightThumpLeft = VK_PAD_RTHUMB_LEFT,

			/// <summary>
			/// Pushing the right thumb stick right.
			/// </summary>
			RightThumbRight = VK_PAD_RTHUMB_RIGHT,

			/// <summary>
			/// Pushing the right thumb stick up and to the left.
			/// </summary>
			RightThumbUpLeft = VK_PAD_RTHUMB_UPLEFT,

			/// <summary>
			/// Pushing the right thumb stick up and to the right.
			/// </summary>
			RightThumbUpRight = VK_PAD_RTHUMB_UPRIGHT,

			/// <summary>
			/// Pushing the right thumb stick down and to the left.
			/// </summary>
			RightThumbDownLeft = VK_PAD_RTHUMB_DOWNLEFT,

			/// <summary>
			/// Pushing the right thumb stick down and to the right.
			/// </summary>
			RightThumbDownRight = VK_PAD_RTHUMB_DOWNRIGHT
		};
		
		/// <summary>
		/// Defines flags that indicate the keyboard state at the time of an input event.
		/// </summary>
		[System::Flags]
		public enum class KeystrokeFlags : System::Int32
		{
			/// <summary>
			/// No extra keystroke flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// The key was pressed.
			/// </summary>
			KeyDown = XINPUT_KEYSTROKE_KEYDOWN,

			/// <summary>
			/// The key was released.
			/// </summary>
			KeyUp = XINPUT_KEYSTROKE_KEYUP,

			/// <summary>
			/// A repeat of a held key.
			/// </summary>
			Repeat = XINPUT_KEYSTROKE_REPEAT
		};

		/// <summary>
		/// Specifies the index of a user's controller.
		/// </summary>
		public enum class UserIndex : System::Int32
		{
			/// <summary>
			/// The index of the user is unimportant.
			/// </summary>
			Any = XUSER_INDEX_ANY,

			/// <summary>
			/// Player one.
			/// </summary>
			One = 0,

			/// <summary>
			/// Player two.
			/// </summary>
			Two = 1,

			/// <summary>
			/// Player three.
			/// </summary>
			Three = 2,

			/// <summary>
			/// Player four.
			/// </summary>
			Four = 3
		};
	}
}