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
#include "InputMessageFilter.h"
#include "KeyboardInputEventArgs.h"
#include "MouseInputEventArgs.h"
#include "RawInputEventArgs.h"
#include "DeviceInfo.h"

namespace SlimDX
{
	namespace RawInput
	{
		public ref class Device sealed
		{
		private:
			Device() { }

			static InputMessageFilter^ filter;

		internal:
			static void OnWmInput( HRAWINPUT input );

		public:
			static void RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags );
			static void RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags, System::IntPtr target );

			static void RegisterDevice( SlimDX::Multimedia::UsagePage usagePage, SlimDX::Multimedia::UsageId usageId, DeviceFlags flags, System::IntPtr target, bool addThreadFilter );
			static void HandleMessage( System::IntPtr message );

			static System::Collections::ObjectModel::ReadOnlyCollection<DeviceInfo^>^ GetDevices();

			static event System::EventHandler<KeyboardInputEventArgs^>^ KeyboardInput;
			static event System::EventHandler<MouseInputEventArgs^>^ MouseInput;
			static event System::EventHandler<RawInputEventArgs^>^ RawInput;
		};
	}
}