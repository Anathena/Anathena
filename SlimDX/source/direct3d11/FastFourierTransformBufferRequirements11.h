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

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Contains buffer requirements for an FFT.
		/// </summary>
		/// <unmanaged>D3DX11_FFT_BUFFER_INFO</unmanaged>
		public value class FastFourierTransformBufferRequirements
		{
		internal:
			FastFourierTransformBufferRequirements( const D3DX11_FFT_BUFFER_INFO &bufferInfo );

		public:
			/// <summary>
			/// Number of temporary buffers needed.
			/// </summary>
			property int TemporaryBufferCount;

			/// <summary>
			/// Minimum sizes (in floats) of temporary buffers.
			/// </summary>
			property array<int>^ TemporaryBufferSizes;

			/// <summary>
			/// Number of precompute buffers required.
			/// </summary>
			property int PrecomputeBufferCount;

			/// <summary>
			/// Minimum sizes (in floats) for precompute buffers.
			/// </summary>
			property array<int>^ PrecomputeBufferSizes;
		};
	}
};