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

#include "Asynchronous11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		
		/// <summary>Encapsulates methods for measuring GPU performance.</summary>
		/// <unmanaged>ID3D11Counter</unmanaged>
		public ref class Counter : public Asynchronous
		{
			COMOBJECT(ID3D11Counter, Counter);
		
		public:
			/// <summary>Gets the kind of this counter.</summary>
			/// <unmanaged>ID3D11Counter::GetDesc</unmanaged>
			property Direct3D11::CounterKind CounterKind
			{
				Direct3D11::CounterKind get();
			}
			
			/// <summary>Initializes a new instance of the <see cref="Counter"/> class.</summary>
			/// <param name="device">The device to use when creating the counter.</param>
			/// <param name="counterKind">The kind of counter to create.</param>
			/// <unmanaged>ID3D11Device::CreateCounter</unmanaged>
			Counter( SlimDX::Direct3D11::Device^ device, Direct3D11::CounterKind counterKind );
		};
	}
};