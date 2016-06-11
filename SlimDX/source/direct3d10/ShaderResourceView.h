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

#include "ResourceView.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class Resource;
		value class ImageLoadInformation;
		value class ShaderResourceViewDescription;

		public ref class ShaderResourceView : public ResourceView
		{
			COMOBJECT(ID3D10ShaderResourceView, ShaderResourceView);
			
			static ID3D10ShaderResourceView* ConstructFromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, D3DX10_IMAGE_LOAD_INFO* loadInformation );
			static ID3D10ShaderResourceView* ConstructFromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, D3DX10_IMAGE_LOAD_INFO* loadInformation );
			static ID3D10ShaderResourceView* ConstructFromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes, D3DX10_IMAGE_LOAD_INFO* loadInformation );

		protected:
			//no-op ctor for use by ShaderResourceView1
			ShaderResourceView() { }

		public:
			property ShaderResourceViewDescription Description
			{
				ShaderResourceViewDescription get();
			}
			
			ShaderResourceView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource );
			ShaderResourceView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource, ShaderResourceViewDescription description );
			
			/// <summary>
			/// Creates a shader resource view from a file.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="fileName">The name of the file that contains the shader resource view.</param>
			static ShaderResourceView^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName );

			/// <summary>
			/// Creates a shader resource view from a file.
			/// </summary>
			/// <param name="device">The device that will own the resource.</param>
			/// <param name="fileName">The name of the file that contains the shader resource view.</param>
			static ShaderResourceView^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, ImageLoadInformation loadInformation );

			static ShaderResourceView^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory );
			static ShaderResourceView^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, ImageLoadInformation loadInfo );
			static ShaderResourceView^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes );
			static ShaderResourceView^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo );
		};
	}
};