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
		/// Describes a DirectInput device's capabilities.
		/// </summary>
		public ref class Capabilities
		{
		private:
			DeviceFlags flags;
			DeviceType type;
			int subType;
			int axesCount;
			int buttonCount;
			int povCount;
			int ffSamplePeriod;
			int ffMinTimeResolution;
			int ffDriverVersion;
			int firmwareRevision;
			int hardwareRevision;
			bool hid;

		internal:
			Capabilities( const DIDEVCAPS &caps );

		public:
			/// <summary>
			/// Gets the flags associated with the device.
			/// </summary>
			property DeviceFlags Flags
			{
				DeviceFlags get() { return flags; }
			}

			/// <summary>
			/// Gets the device type specifier.
			/// </summary>
			property DeviceType Type
			{
				DeviceType get() { return type; }
			}

			/// <summary>
			/// Gets the device sub-type specifier.
			/// </summary>
			property int Subtype
			{
				int get() { return subType; }
			}

			/// <summary>
			/// Gets the number of axes available on the device.
			/// </summary>
			property int AxesCount
			{
				int get() { return axesCount; }
			}

			/// <summary>
			/// Gets the number of buttons available on the device.
			/// </summary>
			property int ButtonCount
			{
				int get() { return buttonCount; }
			}

			/// <summary>
			/// Gets the number of Point-Of-View controllers available on the device.
			/// </summary>
			property int PovCount
			{
				int get() { return povCount; }
			}

			/// <summary>
			/// Gets the minimum time between playback of consecutive raw force commands, in microseconds.
			/// </summary>
			property int ForceFeedbackSamplePeriod
			{
				int get() { return ffSamplePeriod; }
			}

			/// <summary>
			/// Gets the minimum time, in microseconds, that the device can resolve. The device
			/// rounds any times to the nearest supported increment.
			/// </summary>
			property int ForceFeedbackMinimumTimeResolution
			{
				int get() { return ffMinTimeResolution; }
			}

			/// <summary>
			/// Gets the driver version number.
			/// </summary>
			property int DriverVersion
			{
				int get() { return ffDriverVersion; }
			}

			/// <summary>
			/// Gets the firmware revision number.
			/// </summary>
			property int FirmwareRevision
			{
				int get() { return firmwareRevision; }
			}

			/// <summary>
			/// Gets the hardware revision number.
			/// </summary>
			property int HardwareRevision
			{
				int get() { return hardwareRevision; }
			}

			/// <summary>
			/// Gets a value indicating whether the device is a Human Interface Device.
			/// </summary>
			property bool HumanInterfaceDevice
			{
				bool get() { return hid; }
			}
		};
	}
}