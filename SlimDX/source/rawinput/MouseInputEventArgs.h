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
	namespace RawInput
	{
		public ref class MouseInputEventArgs : System::EventArgs
		{
		public:
			MouseInputEventArgs(MouseMode mode, MouseButtonFlags buttonFlags, int wheelDelta, long rawButtons, long x, long y, long extraInformation, System::IntPtr device)
			{
				Mode = mode;
				ButtonFlags = buttonFlags;
				WheelDelta = wheelDelta;
				RawButtons = rawButtons;
				X = x;
				Y = y;
				ExtraInformation = extraInformation;
				Device = device;
			}

			property MouseMode Mode;
			property MouseButtonFlags ButtonFlags;
			property int WheelDelta;
			property long RawButtons;
			property long X;
			property long Y;
			property long ExtraInformation;
			property System::IntPtr Device;
		};
	}
}