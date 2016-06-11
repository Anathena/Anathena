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

namespace SlimDX
{
	namespace Direct3D10_1
	{
		public value class RenderTargetBlendDescription1
		{
		internal:
			RenderTargetBlendDescription1( const D3D10_RENDER_TARGET_BLEND_DESC1& native );
			
			D3D10_RENDER_TARGET_BLEND_DESC1 CreateNativeVersion();

		public:
			/// <summary>
			/// Enables or disables blending on this render target.
			/// </summary>
			property bool BlendEnable;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOption" /> specifies the first RGB data source and includes an optional pre-blend operation. 
			/// </summary>
			property SlimDX::Direct3D10::BlendOption SourceBlend;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOption" /> specifies the second RGB data source and includes an optional pre-blend operation. 
			/// </summary>
			property SlimDX::Direct3D10::BlendOption DestinationBlend;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOperation" /> defines how to combine the RGB data sources.
			/// </summary>
			property SlimDX::Direct3D10::BlendOperation BlendOperation;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOption" /> specifies the first alpha data source and includes an optional pre-blend operation. Blend options that end in "Color" are not allowed.
			/// </summary>
			property SlimDX::Direct3D10::BlendOption SourceBlendAlpha;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOption" /> specifies the second alpha data source and includes an optional pre-blend operation. Blend options that end in "Color" are not allowed.
			/// </summary>
			property SlimDX::Direct3D10::BlendOption DestinationBlendAlpha;

			/// <summary>
			/// This <see cref="SlimDX::Direct3D10::BlendOperation" /> defines how to combine the alpha data sources.
			/// </summary>
			property SlimDX::Direct3D10::BlendOperation BlendOperationAlpha;

			/// <summary>
			/// A write mask.
			/// </summary>
			property System::Byte RenderTargetWriteMask;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( RenderTargetBlendDescription1 left, RenderTargetBlendDescription1 right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( RenderTargetBlendDescription1 left, RenderTargetBlendDescription1 right );

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
			virtual bool Equals( RenderTargetBlendDescription1 other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( RenderTargetBlendDescription1% value1, RenderTargetBlendDescription1% value2 );	
		};
	}
}
