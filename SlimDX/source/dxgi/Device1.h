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

#include "DeviceDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Represents a base for objects that produces image data.
		/// </summary>
		/// <unmanaged>IDXGIDevice1</unmanaged>
		public ref class Device1 : Device
		{
			COMOBJECT(IDXGIDevice1, Device1);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Device1"/> class.
			/// </summary>
			/// <param name="device">The COM object implementing the IDXGIDevice1 interface.</param>
			Device1( IComObject^ device );

			/// <summary>
			/// Gets or sets the number of frames that the system is allowed to queue for rendering. This value defaults to 3, but can range from 1 to 16.
			/// </summary>
			property int MaximumFrameLatency
			{
				int get();
				void set(int value);
			}
		};
	}
}
