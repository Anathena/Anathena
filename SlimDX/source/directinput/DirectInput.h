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

#include "DeviceInstance.h"

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>
		/// Provides an interface to DirectInput.
		/// </summary>
		/// <unmanaged>IDirectInput8W</unmanaged>
		public ref class DirectInput : ComObject
		{
			COMOBJECT(IDirectInput8W, DirectInput);

		public:
			DirectInput();

			/// <summary>
			/// Runs Control Panel to enable the user to install a new
			/// input device or modify configurations.
			/// </summary>
			Result RunControlPanel();

			/// <summary>
			/// Runs Control Panel to enable the user to install a new
			/// input device or modify configurations.
			/// </summary>
			/// <param name="parent">The parent control.</param>
			Result RunControlPanel( System::IntPtr parent );

			/// <summary>
			/// Gets a value indicating whether the specified device is
			/// attached to the user's system.
			/// </summary>
			bool IsDeviceAttached( System::Guid device );

			/// <summary>
			/// Retrieves the instance identifier of a device that
			/// has been newly attached to the system.
			/// </summary>
			System::Guid FindDevice( System::Guid deviceClass, System::String^ name );

			System::Collections::Generic::IList<DeviceInstance^>^ GetDevices();
			System::Collections::Generic::IList<DeviceInstance^>^ GetDevices( DeviceClass deviceClass, DeviceEnumerationFlags enumerationFlags );
			System::Collections::Generic::IList<DeviceInstance^>^ GetDevices( DeviceType deviceType, DeviceEnumerationFlags enumerationFlags );
		};
	}
}
