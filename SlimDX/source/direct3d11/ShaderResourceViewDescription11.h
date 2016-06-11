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
		/// Provides a description for a shader resource view.
		/// </summary>
		/// <unmanaged>D3D11_SHADER_RESOURCE_VIEW_DESC</unmanaged>
		public value class ShaderResourceViewDescription : System::IEquatable<ShaderResourceViewDescription>
		{
		private:
			DXGI::Format m_Format;
			ShaderResourceViewDimension m_ViewDimension;
			int m_ElementOffset;
			int m_ElementWidth;
			int m_MostDetailedMip;
			int m_MipLevels;
			int m_FirstArraySlice;
			int m_ArraySize;
				
		internal:
			ShaderResourceViewDescription( const D3D11_SHADER_RESOURCE_VIEW_DESC& native );
			
			D3D11_SHADER_RESOURCE_VIEW_DESC CreateNativeVersion();
		
		public:
			/// <summary>
			/// The format of the data in the shader resource.
			/// </summary>
			property DXGI::Format Format;

			/// <summary>
			/// The resource type of the view. This should be the same as the resource type of the underlying resource. This parameter also determines which other properties are valid.
			/// </summary>
			property ShaderResourceViewDimension Dimension;

			/// <summary>
			/// The offset of the first element in the view to access, relative to element 0.
			/// </summary>
			property int ElementOffset;

			/// <summary>
			/// The total number of elements in the view.
			/// </summary>
			property int ElementWidth;

			/// <summary>
			/// Index of the most detailed mipmap level to use; this number is between 0 and MipLevels - 1.
			/// </summary>
			property int MostDetailedMip;

			/// <summary>
			/// The maximum number of mipmap levels in the texture.
			/// </summary>
			property int MipLevels;

			/// <summary>
			/// The index of the first texture to use in an array of textures.
			/// </summary>
			property int FirstArraySlice;

			/// <summary>
			/// Number of textures in the array.
			/// </summary>
			property int ArraySize;

			/// <summary>
			/// Index of the first 2D texture to use.
			/// </summary>
			property int First2DArrayFace;

			/// <summary>
			/// Number of cube textures in the array.
			/// </summary>
			property int CubeCount;

			/// <summary>
			/// The index of the first element to be accessed by the view.
			/// </summary>
			property int FirstElement;

			/// <summary>
			/// The number of elements in the resource.
			/// </summary>
			property int ElementCount;

			/// <summary>
			/// Options for binding a raw buffer.
			/// </summary>
			property ShaderResourceViewExtendedBufferFlags Flags;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( ShaderResourceViewDescription left, ShaderResourceViewDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( ShaderResourceViewDescription left, ShaderResourceViewDescription right );

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
			virtual bool Equals( ShaderResourceViewDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( ShaderResourceViewDescription% value1, ShaderResourceViewDescription% value2 );
		};
	}
};