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

#include "DepthStencilOperationDescription11.h"
#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for depth-stencil state objects.
		/// </summary>
		/// <unmanaged>D3D11_DEPTH_STENCIL_DESC</unmanaged>
		public value class DepthStencilStateDescription : System::IEquatable<DepthStencilStateDescription>
		{
		private:
			bool m_DepthEnable;
			Direct3D11::DepthWriteMask m_DepthWriteMask;
			Comparison m_DepthFunc;
			bool m_StencilEnable;
			System::Byte m_StencilReadMask;
			System::Byte m_StencilWriteMask;
			DepthStencilOperationDescription m_FrontFace;
			DepthStencilOperationDescription m_BackFace;

		internal:
			DepthStencilStateDescription( const D3D11_DEPTH_STENCIL_DESC& description );
			
			D3D11_DEPTH_STENCIL_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets a value indicating whether depth testing is enabled.
			/// </summary>
			property bool IsDepthEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Gets or sets a write mask identifying the portion of the depth-stencil buffer that can be modified by depth data.
			/// </summary>
			property Direct3D11::DepthWriteMask DepthWriteMask
			{
				Direct3D11::DepthWriteMask get();
				void set( Direct3D11::DepthWriteMask value );
			}

			/// <summary>
			/// A function that compares depth data against existing data in the buffer.
			/// </summary>
			property Comparison DepthComparison
			{
				Comparison get();
				void set( Comparison value );
			}

			/// <summary>
			/// Gets or sets a value indicating whether stencil testing is enabled.
			/// </summary>
			property bool IsStencilEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Gets or sets a read mask identifying the portion of the depth-stencil buffer that can be read for stencil data.
			/// </summary>
			property System::Byte StencilReadMask
			{
				System::Byte get();
				void set( System::Byte value );
			}

			/// <summary>
			/// Gets or sets a write mask identifying the portion of the depth-stencil buffer that can be modified by stencil data.
			/// </summary>
			property System::Byte StencilWriteMask
			{
				System::Byte get();
				void set( System::Byte value );
			}

			/// <summary>
			/// Specifies how to use the results of the depth test and the stencil test for pixels whose surface normal is facing towards the camera.
			/// </summary>
			property DepthStencilOperationDescription FrontFace
			{
				DepthStencilOperationDescription get();
				void set( DepthStencilOperationDescription value );
			}

			/// <summary>
			/// Specifies how to use the results of the depth test and the stencil test for pixels whose surface normal is facing away from camera.
			/// </summary>
			property DepthStencilOperationDescription BackFace
			{
				DepthStencilOperationDescription get();
				void set( DepthStencilOperationDescription value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( DepthStencilStateDescription left, DepthStencilStateDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( DepthStencilStateDescription left, DepthStencilStateDescription right );

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
			virtual bool Equals( DepthStencilStateDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( DepthStencilStateDescription% value1, DepthStencilStateDescription% value2 );
		};
	}
};