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

#include "../dxgi/Enums.h"

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Contains the description of the contents of an image file.
		/// </summary>
		/// <unmanaged>D3DX11_IMAGE_LOAD_INFO</unmanaged>
		[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
		public value class ImageLoadInformation : System::IEquatable<ImageLoadInformation>
		{
		private:
			int m_Width;
			int m_Height;
			int m_Depth;
			int m_FirstMipLevel;
			int m_MipLevels;
			ResourceUsage m_Usage;
			BindFlags m_BindFlags;
			CpuAccessFlags m_CPUAccessFlags;
			ResourceOptionFlags m_MiscFlags;
			DXGI::Format m_Format;
			FilterFlags m_Filter;
			FilterFlags m_MipFilter;

		internal:
			ImageLoadInformation( const D3DX11_IMAGE_LOAD_INFO& native );

			D3DX11_IMAGE_LOAD_INFO CreateNativeVersion();

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ImageLoadInformation"/> struct using default values.
			/// </summary>
			/// <returns>The default image load information.</returns>
			static ImageLoadInformation FromDefaults();

			/// <summary>
			/// The default value for load options.
			/// </summary>
			property static int FileDefaultValue
			{
				int get();
			}
			
			/// <summary>
			/// Width of the original image, in pixels.
			/// </summary>
			property int Width
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Height of the original image, in pixels.
			/// </summary>
			property int Height
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Depth of the original image, in pixels.
			/// </summary>
			property int Depth
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// The highest resolution mipmap level of the texture; if greater than zero,
			/// this mipmap level will be mapped to level 0 in the loaded texture.
			/// </summary>
			property int FirstMipLevel
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Number of mipmap levels in the original image.
			/// </summary>
			property int MipLevels
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Gets or sets the intended usage pattern of the loaded texture.
			/// </summary>
			property ResourceUsage Usage
			{
				ResourceUsage get();
				void set( ResourceUsage value );
			}
			
			/// <summary>
			/// Gets or sets the flags specifying how the loaded texture is bound to the pipeline.
			/// </summary>
			property Direct3D11::BindFlags BindFlags
			{
				Direct3D11::BindFlags get();
				void set( Direct3D11::BindFlags value );
			}
			
			/// <summary>
			/// Gets or sets the flags specifying how the CPU will be allowed to access the loaded texture.
			/// </summary>
			property Direct3D11::CpuAccessFlags CpuAccessFlags
			{
				Direct3D11::CpuAccessFlags get();
				void set( Direct3D11::CpuAccessFlags value );
			}
			
			/// <summary>
			/// Gets or sets the flags specifying miscellaneous resource options.
			/// </summary>
			property ResourceOptionFlags OptionFlags
			{
				ResourceOptionFlags get();
				void set( ResourceOptionFlags value );
			}

			/// <summary>
			/// The format of the loaded texture.
			/// </summary>
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}

			/// <summary>
			/// The filter used when resampling the texture.
			/// </summary>
			property Direct3D11::FilterFlags FilterFlags
			{
				Direct3D11::FilterFlags get();
				void set( Direct3D11::FilterFlags value );
			}

			/// <summary>
			/// The filter used when resampling mipmap levels of the texture.
			/// </summary>
			property Direct3D11::FilterFlags MipFilterFlags
			{
				Direct3D11::FilterFlags get();
				void set( Direct3D11::FilterFlags value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ImageLoadInformation left, ImageLoadInformation right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ImageLoadInformation left, ImageLoadInformation right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( ImageLoadInformation other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( ImageLoadInformation% value1, ImageLoadInformation% value2 );
		};
	}
}