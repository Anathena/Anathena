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
		/// Specifies the subresources from a resource that are accessible using a render-target view.
		/// </summary>
		/// <unmanaged>D3D11_RENDER_TARGET_VIEW_DESC</unmanaged>
		public value class RenderTargetViewDescription : System::IEquatable<RenderTargetViewDescription>
		{
		private:
			DXGI::Format m_Format;
			RenderTargetViewDimension m_ViewDimension;
			int m_ElementOffset;
			int m_ElementWidth;
			int m_MipSlice;
			int m_FirstArraySlice;
			int m_ArraySize;
			int m_FirstDepthSlice;
			int m_DepthSliceCount;
				
		internal:
			RenderTargetViewDescription( const D3D11_RENDER_TARGET_VIEW_DESC& native );
			
			D3D11_RENDER_TARGET_VIEW_DESC CreateNativeVersion();
		
		public:
			/// <summary>
			/// The data format of the render target.
			/// </summary>
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}
			
			/// <summary>
			/// The resource type, which specifies how the render-target resource will be accessed.
			/// </summary>
			property RenderTargetViewDimension Dimension
			{
				RenderTargetViewDimension get();
				void set( RenderTargetViewDimension value );
			}
			
			/// <summary>
			/// Number of bytes between the beginning of the buffer and the first element to access.
			/// </summary>
			property int ElementOffset
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// The width of each element (in bytes). This can be determined from the format stored in the render-target-view description.
			/// </summary>
			property int ElementWidth
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// The index of the mipmap level to use mip slice.
			/// </summary>
			property int MipSlice
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// The index of the first texture to use in an array of textures.
			/// </summary>
			property int FirstArraySlice
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Number of textures to use.
			/// </summary>
			property int ArraySize
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// First depth level to use.
			/// </summary>
			property int FirstDepthSlice
			{
				int get();
				void set( int value );
			}
			
			/// <summary>
			/// Number of depth levels to use in the render-target view, starting from FirstDepthSlice. A value of -1 indicates all of the slices along the w axis, starting from FirstDepthSlice.
			/// </summary>
			property int DepthSliceCount
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( RenderTargetViewDescription left, RenderTargetViewDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( RenderTargetViewDescription left, RenderTargetViewDescription right );

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
			virtual bool Equals( RenderTargetViewDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( RenderTargetViewDescription% value1, RenderTargetViewDescription% value2 );
		};
	}
};