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
#include "ResourceView11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class Resource;
		value class DepthStencilViewDescription;
		
		/// <summary>Accesses a texture resource during depth/stencil testing.</summary>
		/// <unmanaged>ID3D11DepthStencilView</unmanaged>
		public ref class DepthStencilView : public ResourceView
		{
			COMOBJECT(ID3D11DepthStencilView, DepthStencilView);
		
		public:
			/// <summary>Gets a structure describing this <see cref="DepthStencilView" />.</summary>
			/// <unmanaged>ID3D11DepthStencilView::GetDesc</unmanaged>
			property DepthStencilViewDescription Description
			{
				DepthStencilViewDescription get();
			}
			
			/// <summary>Creates a <see cref="DepthStencilView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="DepthStencilView" />.</param>
			/// <param name="resource">The resource that will serve as the depth-stencil surface. This surface must have been created with the <see cref="BindFlags">DepthStencil</see> flag.</param>
			/// <unmanaged>ID3D11Device::CreateDepthStencilView</unmanaged>
			DepthStencilView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource );

			/// <summary>Creates a <see cref="DepthStencilView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="DepthStencilView" />.</param>
			/// <param name="resource">The resource that will serve as the depth-stencil surface. This surface must have been created with the <see cref="BindFlags">DepthStencil</see> flag.</param>
			/// <param name="description">A structure describing the <see cref="DepthStencilView" /> to be created.</param>
			/// <unmanaged>ID3D11Device::CreateDepthStencilView</unmanaged>
			DepthStencilView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource, DepthStencilViewDescription description );
		};
	}
};