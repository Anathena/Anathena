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
#include "ImageLoadInformation11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class Resource;
		value class ShaderResourceViewDescription;

		/// <summary>Specifies the subresources that a shader can access during rendering.</summary>
		/// <unmanaged>ID3D11ShaderResourceView</unmanaged>
		public ref class ShaderResourceView : public ResourceView
		{
			COMOBJECT(ID3D11ShaderResourceView, ShaderResourceView);

			static ID3D11ShaderResourceView* ConstructFromFile( SlimDX::Direct3D11::Device^ device, System::String^ fileName, D3DX11_IMAGE_LOAD_INFO* loadInformation );
			static ID3D11ShaderResourceView* ConstructFromMemory( SlimDX::Direct3D11::Device^ device, array<System::Byte>^ memory, D3DX11_IMAGE_LOAD_INFO* loadInformation );
			static ID3D11ShaderResourceView* ConstructFromStream( SlimDX::Direct3D11::Device^ device, System::IO::Stream^ stream, int sizeInBytes, D3DX11_IMAGE_LOAD_INFO* loadInformation );
			
		public:
			/// <summary>Creates a <see cref="ShaderResourceView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="ShaderResourceView" />.</param>
			/// <param name="resource">The resource that will serve as input to a shader. This resource must have been created with the <see cref="BindFlags">ShaderResource</see> flag.</param>
			/// <unmanaged>ID3D11Device::CreateShaderResourceView</unmanaged>
			ShaderResourceView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource );

			/// <summary>Creates a <see cref="ShaderResourceView" /> for accessing resource data.</summary>
			/// <param name="device">The device to use when creating this <see cref="ShaderResourceView" />.</param>
			/// <param name="resource">The resource that will serve as input to a shader. This resource must have been created with the <see cref="BindFlags">ShaderResource</see> flag.</param>
			/// <param name="description">A structure describing the <see cref="ShaderResourceView" /> to be created.</param>
			/// <unmanaged>ID3D11Device::CreateShaderResourceView</unmanaged>
			ShaderResourceView( SlimDX::Direct3D11::Device^ device, SlimDX::Direct3D11::Resource^ resource, ShaderResourceViewDescription description );

			/// <summary>Gets a structure describing this <see cref="ShaderResourceView" />.</summary>
			/// <unmanaged>ID3D11ShaderResourceView::GetDesc</unmanaged>
			property ShaderResourceViewDescription Description
			{
				ShaderResourceViewDescription get();
			}

			/// <summary>
			/// Creates a shader resource view from a file.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="fileName">The name of the file that contains the shader resource view.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromFile( SlimDX::Direct3D11::Device^ device, System::String^ fileName );

			/// <summary>
			/// Creates a shader resource view from a file.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="fileName">The name of the file that contains the shader resource view.</param>
			/// <param name="loadInformation">Identifies characteristics of the texture to be loaded.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromFile( SlimDX::Direct3D11::Device^ device, System::String^ fileName, ImageLoadInformation loadInformation );

			/// <summary>
			/// Creates a shader resource view from memory.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="memory">The block of memory that contains the shader resource view.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromMemory( SlimDX::Direct3D11::Device^ device, array<System::Byte>^ memory );

			/// <summary>
			/// Creates a shader resource view from memory.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="memory">The block of memory that contains the shader resource view.</param>
			/// <param name="loadInfo">Identifies characteristics of the texture to be loaded.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromMemory( SlimDX::Direct3D11::Device^ device, array<System::Byte>^ memory, ImageLoadInformation loadInfo );

			/// <summary>
			/// Creates a shader resource view from a stream.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="stream">The stream that contains the shader resource view.</param>
			/// <param name="sizeInBytes">Size of the shader resource, in bytes.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromStream( SlimDX::Direct3D11::Device^ device, System::IO::Stream^ stream, int sizeInBytes );

			/// <summary>
			/// Creates a shader resource view from a stream.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="stream">The stream that contains the shader resource view.</param>
			/// <param name="sizeInBytes">Size of the shader resource, in bytes.</param>
			/// <param name="loadInfo">Identifies characteristics of the texture to be loaded.</param>
			/// <returns>The created resource view.</returns>
			static ShaderResourceView^ FromStream( SlimDX::Direct3D11::Device^ device, System::IO::Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo );
		};
	}
};