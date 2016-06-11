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

#include "Enums.h"

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>
		/// Specifies the sub type of a DirectInput device.
		/// </summary>
		public ref class DeviceSubtype sealed
		{
		private:
			DeviceSubtype() { }

		public:
			/// <summary>
			/// A device that does not provide the minimum number of device objects for
			/// action mapping.
			/// </summary>
			literal int Limited = DI8DEVTYPE_LIMITEDGAMESUBTYPE;

			/// <summary>
			/// Unknown subtype for a first-person device.
			/// </summary>
			literal int FirstPersonUnknown = DI8DEVTYPE1STPERSON_UNKNOWN;

			/// <summary>
			/// Device designed for first-person person shooter games.
			/// </summary>
			literal int FirstPersonShooter = DI8DEVTYPE1STPERSON_SHOOTER;

			/// <summary>
			/// Device with six degrees of freedom; that is, three lateral axes and three rotation axes.
			/// </summary>
			literal int SixDegreesOfFreedom = DI8DEVTYPE1STPERSON_SIXDOF;

			/// <summary>
			/// Control used to make communications selections.
			/// </summary>
			literal int CommunicationSelections = DI8DEVTYPEDEVICECTRL_COMMSSELECTION;

			/// <summary>
			/// Device that must use its default configuration and cannot be remapped.
			/// </summary>
			literal int Hardwired = DI8DEVTYPEDEVICECTRL_COMMSSELECTION_HARDWIRED;

			/// <summary>
			/// Unknown subtype for a control device.
			/// </summary>
			literal int ControlDeviceUnknown = DI8DEVTYPEDEVICECTRL_UNKNOWN;

			/// <summary>
			/// Steering device that reports accelerations and break pedal values from a single axis.
			/// </summary>
			literal int CombinedPedals = DI8DEVTYPEDRIVING_COMBINEDPEDALS;

			/// <summary>
			/// Steering device that reports accelerations and break pedal values from separate axes.
			/// </summary>
			literal int DualPedals = DI8DEVTYPEDRIVING_DUALPEDALS;

			/// <summary>
			/// Hand-held steering device.
			/// </summary>
			literal int DrivingHandheld = DI8DEVTYPEDRIVING_HANDHELD;

			/// <summary>
			/// Steering device that reports acceleration, break, and clutch pedal values from
			/// separate axes.
			/// </summary>
			literal int ThreePedals = DI8DEVTYPEDRIVING_THREEPEDALS;

			/// <summary>
			/// Flight device based upon a remote control for model aircraft.
			/// </summary>
			literal int RemoteControlledFlight = DI8DEVTYPEFLIGHT_RC;

			/// <summary>
			/// A joystick for flight simulators.
			/// </summary>
			literal int FlightStick = DI8DEVTYPEFLIGHT_STICK;

			/// <summary>
			/// A yoke for flight simulators.
			/// </summary>
			literal int FlightYoke = DI8DEVTYPEFLIGHT_YOKE;

			/// <summary>
			/// Standard game pad that provides the minimum number of objects for
			/// action mapping.
			/// </summary>
			literal int StandardGamepad = DI8DEVTYPEGAMEPAD_STANDARD;

			/// <summary>
			/// IBM PC/XT 83-key keyboard.
			/// </summary>
			literal int PCXTKeyboard = DI8DEVTYPEKEYBOARD_PCXT;

			/// <summary>
			/// Olivetti 102-key keyboard.
			/// </summary>
			literal int OlivettiKeyboard = DI8DEVTYPEKEYBOARD_OLIVETTI;

			/// <summary>
			/// IBM PC/AT 84-key keyboard.
			/// </summary>
			literal int PCATKeyboard = DI8DEVTYPEKEYBOARD_PCAT;

			/// <summary>
			/// IBM PC Enhanced 101/102-key keyboard or Microsoft Natural keyboard.
			/// </summary>
			literal int StandardKeyboard = DI8DEVTYPEKEYBOARD_PCENH;

			/// <summary>
			/// Nokia 1050 keyboard.
			/// </summary>
			literal int Nokia1050Keyboard = DI8DEVTYPEKEYBOARD_NOKIA1050;

			/// <summary>
			/// Nokia 9140 keyboard.
			/// </summary>
			literal int Nokia9140Keyboard = DI8DEVTYPEKEYBOARD_NOKIA9140;

			/// <summary>
			/// Japanse NEC PC98 keyboard.
			/// </summary>
			literal int JapaneseNecKeyboard = DI8DEVTYPEKEYBOARD_NEC98;

			/// <summary>
			/// Japanese NEC PC98 laptop keyboard.
			/// </summary>
			literal int JapaneseNecLaptopKeyboard = DI8DEVTYPEKEYBOARD_NEC98LAPTOP;

			/// <summary>
			/// Japanese NEC PC98 106-key keyboard.
			/// </summary>
			literal int JapaneseNec106Keyboard = DI8DEVTYPEKEYBOARD_NEC98106;

			/// <summary>
			/// Japanese 106-key keyboard.
			/// </summary>
			literal int Japanese106Keyboard = DI8DEVTYPEKEYBOARD_JAPAN106;

			/// <summary>
			/// Japanese AX keyboard.
			/// </summary>
			literal int JapaneseAXKeyboard = DI8DEVTYPEKEYBOARD_JAPANAX;

			/// <summary>
			/// Japanese J3100 keyboard.
			/// </summary>
			literal int JapaneseJ3100Keyboard = DI8DEVTYPEKEYBOARD_J3100;
		};
	}
}