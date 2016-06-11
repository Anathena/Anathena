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
		/// Contains properties that define device behavior. These
		/// properties include input buffer size and axis mode.
		/// </summary>
		public ref class DeviceProperties
		{
		private:
			IDirectInputDevice8W *pointer;

		internal:
			DeviceProperties( IDirectInputDevice8W *pointer );

		public:
			/// <summary>
			/// Retrieves the localized key name for the specified keyboard key.
			/// </summary>
			System::String^ GetKeyName( Key key );

			/// <summary>
			/// Retrieves the scan code for the specified keyboard key.
			/// </summary>
			int GetKeyCode( Key key );

			/// <summary>
			/// Sets the data range for the device.
			/// </summary>
			void SetRange( int lowerRange, int upperRange );

			/// <summary>
			/// Gets or sets an application-defined value associated with the device.
			/// </summary>
			property System::Object^ ApplicationData
			{
				[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
				System::Object^ get();

				[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
				void set( System::Object^ value );
			}

			/// <summary>
			/// Gets or sets a value indicating whether device objects are self centering.
			/// </summary>
			property bool AutoCenter
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Gets or sets the axis mode for the device.
			/// </summary>
			property DeviceAxisMode AxisMode
			{
				DeviceAxisMode get();
				void set( DeviceAxisMode value );
			}

			/// <summary>
			/// Gets or sets the buffer input size.
			/// </summary>
			property int BufferSize
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the dead zone for a joystick device.
			/// </summary>
			property int DeadZone
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the gain of the device for force-feedback effects.
			/// </summary>
			property int ForceFeedbackGain
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets the memory load for the device.
			/// </summary>
			property int MemoryLoad
			{
				int get();
			}

			/// <summary>
			/// Gets the human-readable display name of the port to which the device is connected.
			/// </summary>
			property System::String^ PortDisplayName
			{
				System::String^ get();
			}

			/// <summary>
			/// Gets the input granularity for the device.
			/// </summary>
			property int Granularity
			{
				int get();
			}

			/// <summary>
			/// Gets the class identifier for the device.
			/// </summary>
			property System::Guid ClassGuid
			{
				System::Guid get();
			}

			/// <summary>
			/// Gets the physical interface path to which the device is connected.
			/// </summary>
			property System::String^ InterfacePath
			{
				System::String^ get();
			}

			/// <summary>
			/// Gets or sets the friendly instance name of the device.
			/// </summary>
			property System::String^ InstanceName
			{
				System::String^ get();
				void set( System::String^ value );
			}

			/// <summary>
			/// Gets the instance number of a joystick device.
			/// </summary>
			property int JoystickId
			{
				int get();
			}

			/// <summary>
			/// Gets or sets the friendly product name of the device.
			/// </summary>
			property System::String^ ProductName
			{
				System::String^ get();
				void set( System::String^ value );
			}

			/// <summary>
			/// Gets or sets the upper range of values that the device can possibly report.
			/// </summary>
			property int UpperRange
			{
				int get();
			}

			/// <summary>
			/// Gets or sets the lower range of values that the device can possibly report.
			/// </summary>
			property int LowerRange
			{
				int get();
			}

			/// <summary>
			/// Gets or sets the saturation zone of a joystick device.
			/// </summary>
			property int Saturation
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets the type name of the device.
			/// </summary>
			property System::String^ TypeName
			{
				System::String^ get();
			}

			/// <summary>
			/// Gets the user name for a user currently assigned to the device.
			/// </summary>
			property System::String^ UserName
			{
				System::String^ get();
			}

			/// <summary>
			/// Gets the vendor identifier.
			/// </summary>
			property int VendorId
			{
				int get();
			}

			/// <summary>
			/// Gets the product identifer.
			/// </summary>
			property int ProductId
			{
				int get();
			}
		};
	}
}