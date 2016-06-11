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

#include "ResourceView11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class Resource;
		value class UnorderedAccessViewDescription;
		
		/// <summary>Specifies the parts of a resource the pipeline can access during rendering.</summary>
		/// <unmanaged>ID3D11UnorderedAccessView</unmanaged>
		public ref class UnorderedAccessView : public ResourceView
		{
			COMOBJECT(ID3D11UnorderedAccessView, UnorderedAccessView);
		
		public:
			/// <summary>Gets a structure describing this <see cref="UnorderedAccessView" />.</summary>
			/// <unmanaged>ID3D11UnorderedAccessView::GetDesc</unmanaged>
			property UnorderedAccessViewDescription Description
			{
				UnorderedAccessViewDescription get();
			}
			
			/// <summary>Creates a <see cref="UnorderedAccessView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="UnorderedAccessView" />.</param>
			/// <param name="resource">The resource that will serve as input to a shader.</param>
			/// <unmanaged>ID3D11Device::CreateUnorderedAccessView</unmanaged>
			UnorderedAccessView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource );

			/// <summary>Creates a <see cref="UnorderedAccessView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="UnorderedAccessView" />.</param>
			/// <param name="resource">The resource that will serve as input to a shader.</param>
			/// <param name="description">A structure describing the <see cref="UnorderedAccessView" /> to be created.</param>
			/// <unmanaged>ID3D11Device::CreateUnorderedAccessView</unmanaged>
			UnorderedAccessView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource, UnorderedAccessViewDescription description );
		};
	}
};
