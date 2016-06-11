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

namespace SlimDX
{
	namespace RawInput
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		[System::Flags]
		public enum class DeviceFlags : System::Int32
		{
			None = 0,
			ApplicationKeys = RIDEV_APPKEYS,
			CaptureMouse = RIDEV_CAPTUREMOUSE,
			InputSink = RIDEV_INPUTSINK,
			NoHotkeys = RIDEV_NOHOTKEYS,
			NoLegacy = RIDEV_NOLEGACY,
		};

		public enum class DeviceType : System::Int32
		{
			Keyboard = RIM_TYPEKEYBOARD,
			Mouse = RIM_TYPEMOUSE,
			HumanInputDevice = RIM_TYPEHID
		};

		[System::Flags]
		public enum class ScanCodeFlags : System::Int32
		{
			Make = RI_KEY_MAKE,
			Break = RI_KEY_BREAK,
			E0 = RI_KEY_E0,
			E1 = RI_KEY_E1,
			TerminalServerSetLed = RI_KEY_TERMSRV_SET_LED,
			TerminalServerShadow = RI_KEY_TERMSRV_SHADOW
		};

		public enum class KeyState
		{
			Pressed = WM_KEYDOWN,
			Released = WM_KEYUP,
			SystemKeyReleased = WM_SYSKEYUP,
			SystemKeyPressed = WM_SYSKEYDOWN,
			Hotkey = WM_HOTKEY,
		};

		[System::Flags]
		public enum class MouseButtonFlags : System::Int32
		{
			None = 0,
			LeftDown = RI_MOUSE_LEFT_BUTTON_DOWN,
			LeftUp = RI_MOUSE_LEFT_BUTTON_UP,
			RightDown = RI_MOUSE_RIGHT_BUTTON_DOWN,
			RightUp = RI_MOUSE_RIGHT_BUTTON_UP,
			MiddleDown = RI_MOUSE_MIDDLE_BUTTON_DOWN,
			MiddleUp = RI_MOUSE_MIDDLE_BUTTON_UP,
			Button4Down = RI_MOUSE_BUTTON_4_DOWN,
			Button4Up = RI_MOUSE_BUTTON_4_UP,
			Button5Down = RI_MOUSE_BUTTON_5_DOWN,
			Button5Up = RI_MOUSE_BUTTON_5_UP,
			MouseWheel = RI_MOUSE_WHEEL
		};

		[System::Flags]
		public enum class MouseMode : System::Int32
		{
			AbsoluteMovement = MOUSE_MOVE_ABSOLUTE,
			AttributesChanged = MOUSE_ATTRIBUTES_CHANGED,
			RelativeMovement = MOUSE_MOVE_RELATIVE,
			VirtualDesktopMovement = MOUSE_VIRTUAL_DESKTOP
		};
	}
}