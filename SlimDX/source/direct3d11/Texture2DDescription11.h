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
#include "../dxgi/SampleDescription.h"

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Describes a two-dimensional texture.
		/// </summary>
		/// <unmanaged>D3D11_TEXTURE2D_DESC</unmanaged>
		public value class Texture2DDescription : System::IEquatable<Texture2DDescription>
		{
		private:
			int m_Width;
			int m_Height;
			int m_MipLevels;
			int m_ArraySize;
			DXGI::Format m_Format;
			DXGI::SampleDescription m_SampleDesc;
			ResourceUsage m_Usage;
			BindFlags m_BindFlags;
			CpuAccessFlags m_CPUAccessFlags;
			ResourceOptionFlags m_MiscFlags;

		internal:
			Texture2DDescription( const D3D11_TEXTURE2D_DESC& native );
			
			D3D11_TEXTURE2D_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Texture width (in texels).
			/// </summary>
			property int Width
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Texture height (in texels).
			/// </summary>
			property int Height
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// The maximum number of mipmap levels in the texture. Use 1 for a multisampled texture; or 0 to generate a full set of subtextures.
			/// </summary>
			property int MipLevels
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Number of textures in the array.
			/// </summary>
			property int ArraySize
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Format of the data in the texture.
			/// </summary>
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}
			
			/// <summary>
			/// Gets or sets multisampling parameters for the texture.
			/// </summary>
			property DXGI::SampleDescription SampleDescription
			{
				DXGI::SampleDescription get();
				void set( DXGI::SampleDescription value );
			}
			
			/// <summary>
			/// Value that identifies how the texture is to be read from and written to.
			/// </summary>
			property ResourceUsage Usage
			{
				ResourceUsage get();
				void set( ResourceUsage value );
			}
			
			/// <summary>
			/// Flags that describe how the texture can be bound to the pipeline.
			/// </summary>
			property Direct3D11::BindFlags BindFlags
			{
				Direct3D11::BindFlags get();
				void set( Direct3D11::BindFlags value );
			}
			
			/// <summary>
			/// Flags that specify how the CPU may access the texture.
			/// </summary>
			property Direct3D11::CpuAccessFlags CpuAccessFlags
			{
				Direct3D11::CpuAccessFlags get();
				void set( Direct3D11::CpuAccessFlags value );
			}
			
			/// <summary>
			/// Flags that identifies other, less common resource options.
			/// </summary>
			property ResourceOptionFlags OptionFlags
			{
				ResourceOptionFlags get();
				void set( ResourceOptionFlags value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Texture2DDescription left, Texture2DDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Texture2DDescription left, Texture2DDescription right );

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
			virtual bool Equals( Texture2DDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Texture2DDescription% value1, Texture2DDescription% value2 );
		};
	}
};