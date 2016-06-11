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
#include "Resource11.h"
#include "ImageLoadInformation11.h"

namespace SlimDX
{
	ref class DataBox;
	
	namespace Direct3D11
	{
		ref class Device;
		ref class DeviceContext;
		value class Texture3DDescription;

		/// <summary>
		/// Represents a three-dimensional texture.
		/// </summary>
		/// <unmanaged>ID3D11Texture3D</unmanaged>
		public ref class Texture3D : public Resource
		{
			COMOBJECT(ID3D11Texture3D, Texture3D);
			
		private:
				static ID3D11Texture3D* Build( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, D3D11_SUBRESOURCE_DATA* data ); 
		
		public:
			/// <summary>
			/// Gets the texture description.
			/// </summary>
			property Texture3DDescription Description
			{
				Texture3DDescription get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture3D"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the texture.</param>
			/// <param name="description">The description of the texture.</param>
			Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture3D"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the texture.</param>
			/// <param name="description">The description of the texture.</param>
			/// <param name="data">The initial texture data.</param>
			Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, DataBox^ data );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture3D"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the texture.</param>
			/// <param name="description">The description of the texture.</param>
			/// <param name="data">An array of initial texture data for each subresource.</param>
			Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, array<DataBox^>^ data );
			
			/// <summary>
			/// Loads a texture from an image file.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="fileName">Path to the file on disk.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromFile( SlimDX::Direct3D11::Device^ device, System::String^ fileName );

			/// <summary>
			/// Loads a texture from an image file.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="fileName">Path to the file on disk.</param>
			/// <param name="loadInfo">Specifies information used to load the texture.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromFile( SlimDX::Direct3D11::Device^ device, System::String^ fileName, ImageLoadInformation loadInfo );

			/// <summary>
			/// Loads a texture from an image in memory.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="memory">Array of memory containing the image data to load.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromMemory( SlimDX::Direct3D11::Device^ device, array<System::Byte>^ memory );

			/// <summary>
			/// Loads a texture from an image in memory.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="memory">Array of memory containing the image data to load.</param>
			/// <param name="loadInfo">Specifies information used to load the texture.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromMemory( SlimDX::Direct3D11::Device^ device, array<System::Byte>^ memory, ImageLoadInformation loadInfo );

			/// <summary>
			/// Loads a texture from a stream of data.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="stream">A stream containing the image data to load.</param>
			/// <param name="sizeInBytes">Size of the image to load.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromStream( SlimDX::Direct3D11::Device^ device, System::IO::Stream^ stream, int sizeInBytes );

			/// <summary>
			/// Loads a texture from a stream of data.
			/// </summary>
			/// <param name="device">The device used to load the texture.</param>
			/// <param name="stream">A stream containing the image data to load.</param>
			/// <param name="sizeInBytes">Size of the image to load.</param>
			/// <param name="loadInfo">Specifies information used to load the texture.</param>
			/// <returns>The loaded texture object.</returns>
			static Texture3D^ FromStream( SlimDX::Direct3D11::Device^ device, System::IO::Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo );

			/// <summary>
			/// Saves a texture to file.
			/// </summary>
			/// <param name="context">The device used to save the texture.</param>
			/// <param name="texture">The texture to save.</param>
			/// <param name="format">The format the texture will be saved as.</param>
			/// <param name="fileName">Name of the destination output file where the texture will be saved.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ToFile( DeviceContext^ context, Texture3D^ texture, ImageFileFormat format, System::String^ fileName );

			/// <summary>
			/// Saves a texture to a stream.
			/// </summary>
			/// <param name="context">The device used to save the texture.</param>
			/// <param name="texture">The texture to save.</param>
			/// <param name="format">The format the texture will be saved as.</param>
			/// <param name="stream">Destination memory stream where the image will be saved.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result ToStream( DeviceContext^ context, Texture3D^ texture, ImageFileFormat format, System::IO::Stream^ stream );
		};
	}
};