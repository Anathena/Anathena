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

#include "../math/color4.h"

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for sampler state objects.
		/// </summary>
		/// <unmanaged>D3D11_SAMPLER_DESC</unmanaged>
		public value class SamplerDescription : System::IEquatable<SamplerDescription>
		{
		internal:
			SamplerDescription( const D3D11_SAMPLER_DESC& native );
			
			D3D11_SAMPLER_DESC CreateNativeVersion();
			
		public:
			/// <summary>
			/// Filtering method to use when sampling a texture.
			/// </summary>
			property Filter Filter;

			/// <summary>
			/// Method to use for resolving a u texture coordinate that is outside the 0 to 1 range.
			/// </summary>
			property TextureAddressMode AddressU;

			/// <summary>
			/// Method to use for resolving a v texture coordinate that is outside the 0 to 1 range.
			/// </summary>
			property TextureAddressMode AddressV;

			/// <summary>
			/// Method to use for resolving a w texture coordinate that is outside the 0 to 1 range.
			/// </summary>
			property TextureAddressMode AddressW;

			/// <summary>
			/// Offset from the calculated mipmap level. For example, if Direct3D calculates that a texture should be sampled at mipmap level 3 and MipLODBias is 2, then the texture will be sampled at mipmap level 5.
			/// </summary>
			property float MipLodBias;

			/// <summary>
			/// Clamping value used if performing anisotropic filtering. Valid values are between 1 and 16.
			/// </summary>
			property int MaximumAnisotropy;

			/// <summary>
			/// A function that compares sampled data against existing sampled data.
			/// </summary>
			property Comparison ComparisonFunction;

			/// <summary>
			/// Border color to use if using border addressing mode.
			/// </summary>
			property Color4 BorderColor;

			/// <summary>
			/// Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.
			/// </summary>
			property float MinimumLod;

			/// <summary>
			/// Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinimumLod. To have no upper limit on LOD set this to a large value such as float.MaxValue.
			/// </summary>
			property float MaximumLod;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( SamplerDescription left, SamplerDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( SamplerDescription left, SamplerDescription right );

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
			virtual bool Equals( SamplerDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( SamplerDescription% value1, SamplerDescription% value2 );
		};
	}
};