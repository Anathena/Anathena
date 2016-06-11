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

#include "../multimedia/Enums.h"
#include "Enums.h"

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>
		/// Contains information about a device's identity.
		/// </summary>
		public ref class DeviceInstance
		{
		private:
			System::Guid instanceGuid;
			System::Guid productGuid;
			System::Guid ffDriverGuid;
			DeviceType type;
			int subType;
			System::String^ instanceName;
			System::String^ productName;
			SlimDX::Multimedia::UsageId usage;
			SlimDX::Multimedia::UsagePage usagePage;
			bool hid;

		internal:
			DeviceInstance( const DIDEVICEINSTANCE &deviceInstance );

		public:
			/// <summary>
			/// Gets the unique identifier for the instance of the device. Instance Guids are
			/// specific to a particular computer.
			/// </summary>
			property System::Guid InstanceGuid
			{
				System::Guid get() { return instanceGuid; }
			}

			/// <summary>
			/// Gets the unique identifier for the product. This identifier is established
			/// by the manufacturer of the device.
			/// </summary>
			property System::Guid ProductGuid
			{
				System::Guid get() { return productGuid; }
			}

			/// <summary>
			/// Gets the unique identifier for the driver being used for force-feedback. The
			/// driver's manufacturer establishes this identifier.
			/// </summary>
			property System::Guid ForceFeedbackDriverGuid
			{
				System::Guid get() { return ffDriverGuid; }
			}

			/// <summary>
			/// Gets the main device type specifier.
			/// </summary>
			property DeviceType Type
			{
				DeviceType get() { return type; }
			}

			/// <summary>
			/// Gets the secondary device type specifier.
			/// </summary>
			property int Subtype
			{
				int get() { return subType; }
			}

			/// <summary>
			/// Gets the friendly name for the instance.
			/// </summary>
			property System::String^ InstanceName
			{
				System::String^ get() { return instanceName; }
			}

			/// <summary>
			/// Gets the friendly name for the product.
			/// </summary>
			property System::String^ ProductName
			{
				System::String^ get() { return productName; }
			}

			/// <summary>
			/// If the device is a Human Interface Device, this member contains
			/// the HID usage code.
			/// </summary>
			property SlimDX::Multimedia::UsageId UsageId
			{
				SlimDX::Multimedia::UsageId get() { return usage; }
			}

			/// <summary>
			/// If the device is a Human Interface Device, this member contains
			/// the HID usage page code.
			/// </summary>
			property SlimDX::Multimedia::UsagePage UsagePage
			{
				SlimDX::Multimedia::UsagePage get() { return usagePage; }
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