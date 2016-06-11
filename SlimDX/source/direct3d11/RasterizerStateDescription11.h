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

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for rasterizer state objects.
		/// </summary>
		/// <unmanaged>D3D11_RASTERIZER_DESC</unmanaged>
		public value class RasterizerStateDescription : System::IEquatable<RasterizerStateDescription>
		{
		private:
			Direct3D11::FillMode m_FillMode;
			Direct3D11::CullMode m_CullMode;
			bool m_FrontCounterClockwise;
			int m_DepthBias;
			float m_DepthBiasClamp;
			float m_SlopeScaledDepthBias;
			bool m_DepthClipEnable;
			bool m_ScissorEnable;
			bool m_MultisampleEnable;
			bool m_AntialiasedLineEnable;

		internal:
			RasterizerStateDescription( const D3D11_RASTERIZER_DESC& description );
			
			D3D11_RASTERIZER_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Gets or sets the fill mode to use when rendering.
			/// </summary>
			property Direct3D11::FillMode FillMode
			{
				Direct3D11::FillMode get();
				void set( Direct3D11::FillMode value );
			}

			/// <summary>
			/// Gets or sets which triangles are to be culled.
			/// </summary>
			property Direct3D11::CullMode CullMode
			{
				Direct3D11::CullMode get();
				void set( Direct3D11::CullMode value );
			}

			/// <summary>
			/// Determines if a triangle is front- or back-facing. If this parameter is true, then a triangle will be considered front-facing if its vertices are counter-clockwise on the render target and considered back-facing if they are clockwise. If this parameter is false then the opposite is true.
			/// </summary>
			property bool IsFrontCounterclockwise
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Depth value added to a given pixel.
			/// </summary>
			property int DepthBias
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Maximum depth bias of a pixel.
			/// </summary>
			property float DepthBiasClamp
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Scalar on a given pixel's slope.
			/// </summary>
			property float SlopeScaledDepthBias
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Determines whether to enable clipping based on distance.
			/// </summary>
			property bool IsDepthClipEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Determines whether to enable scissor-rectangle culling. All pixels ouside an active scissor rectangle are culled.
			/// </summary>
			property bool IsScissorEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Determines whether to enable multisample antialiasing.
			/// </summary>
			property bool IsMultisampleEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Determines whether to enable line antialiasing; only applies if doing line drawing and IsMultisampleEnabled is false.
			/// </summary>
			property bool IsAntialiasedLineEnabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( RasterizerStateDescription left, RasterizerStateDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( RasterizerStateDescription left, RasterizerStateDescription right );

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
			virtual bool Equals( RasterizerStateDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( RasterizerStateDescription% value1, RasterizerStateDescription% value2 );
		};
	}
};