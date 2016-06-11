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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		/// <summary>
		/// Contains the description of the contents of an image file.
		/// </summary>
		[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
		public value class ImageInformation : System::IEquatable<ImageInformation>
		{
		private:
			int m_Width;
			int m_Height;
			int m_Depth;
			int m_ArraySize;
			int m_MipLevels;
			ResourceOptionFlags m_MiscFlags;
			DXGI::Format m_Format;
			ResourceDimension m_Dimension;
			ImageFileFormat m_FileFormat;
		
		internal:
			ImageInformation( const D3DX10_IMAGE_INFO& native );
			
			D3DX10_IMAGE_INFO CreateNativeVersion();
			
		public:
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
			/// Size of the image, in bytes.
			/// </summary>
			property int ArraySize
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
			
			property ResourceOptionFlags OptionFlags
			{
				ResourceOptionFlags get();
				void set( ResourceOptionFlags value );
			}

			/// <summary>
			/// The original format of the image.
			/// </summary>
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}

			/// <summary>
			/// The type of the texture stored in the file.
			/// </summary>
			property ResourceDimension Dimension
			{
				ResourceDimension get();
				void set( ResourceDimension value );
			}

			/// <summary>
			/// The format of the image file.
			/// </summary>
			property ImageFileFormat FileFormat
			{
				ImageFileFormat get();
				void set( ImageFileFormat value );
			}

			static System::Nullable<ImageInformation> FromFile( System::String^ fileName );
			static System::Nullable<ImageInformation> FromMemory( array<System::Byte>^ memory );

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ImageInformation left, ImageInformation right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ImageInformation left, ImageInformation right );

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
			virtual bool Equals( ImageInformation other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( ImageInformation% value1, ImageInformation% value2 );
		};
	}
}