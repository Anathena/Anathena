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

#include "DeviceChildDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Represents a keyed mutex, which allows exclusive access to a shared resource that is used by multiple devices.
		/// </summary>
		/// <unmanaged>IDXGIKeyedMutex</unmanaged>
		public ref class KeyedMutex : public DeviceChild
		{
			COMOBJECT(IDXGIKeyedMutex, KeyedMutex);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="KeyedMutex"/> class.
			/// </summary>
			/// <param name="resource">The COM object implementing the IDXGIKeyedMutex interface.</param>
			KeyedMutex( IComObject^ resource );

			/// <summary>
			/// Using a key, acquires exclusive rendering access to a shared resource.
			/// </summary>
			/// <param name="key">A value that indicates which device to give access to. This method will succeed when the device that currently owns the surface
			/// calls the KeyedMutex.Release method using the same value. This key can be any arbitrary value.</param>
			/// <param name="milliseconds">The time-out interval, in milliseconds. This method will return if the interval elapses, and the keyed mutex
			/// has not been released using the specified key. If this value is set to zero, the Acquire method will test to see if the keyed mutex has been
			/// released and returns immediately. If this value is set to the maximum integral value, the time-out interval will never elapse.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Acquire(System::Int64 key, int milliseconds);

			/// <summary>
			/// Using a key, releases exclusive rendering access to a shared resource.
			/// </summary>
			/// <param name="key">A value that indicates which device to give access to. This method will succeed when the device that currently owns the surface
			/// calls the KeyedMutex.ReleaseSync method using the same value. This key can be any arbitrary value.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Release(System::Int64 key);
		};
	}
}
