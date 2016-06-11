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
#include "DeviceChildDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		/// <summary>
		/// Implements a base interface that allows resource sharing and identifies the memory that a resource resides in.
		/// </summary>
		/// <unmanaged></unmanaged>
		public ref class Resource : DeviceChild
		{
			COMOBJECT(IDXGIResource, Resource);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Resource"/> class.
			/// </summary>
			/// <param name="resource">The COM object implementing the IDXGIResource interface.</param>
			Resource( IComObject^ resource );

			/// <summary>
			/// Gets or sets the eviction priority for the resource.
			/// </summary>
			property ResourcePriority EvictionPriority
			{
				ResourcePriority get();
				void set( ResourcePriority value );
			}
			
			/// <summary>
			/// Gets the resource usage.
			/// </summary>
			property DXGI::Usage Usage
			{
				DXGI::Usage get();
			}

			/// <summary>
			/// Get the handle to a shared resource. The returned handle can be used to open the resource using different Direct3D devices.
			/// </summary>
			property System::IntPtr SharedHandle
			{
				System::IntPtr get();
			}
		};
	}
}