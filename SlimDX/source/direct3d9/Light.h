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

#include "../math/Color4.h"
#include "../math/Vector3.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Defines a set of lighting properties.</summary>
		/// <unmanaged>D3DLIGHT9</unmanaged>
		[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
		public value class Light : System::IEquatable<Light>
		{
		public:
			/// <summary>
			/// Gets or sets the type of the light source.
			/// </summary>
			property LightType Type;

			/// <summary>
			/// Gets or sets the diffuse color of the light.
			/// </summary>
			property Color4 Diffuse;

			/// <summary>
			/// Gets or sets the specular color of the light.
			/// </summary>
			property Color4 Specular;

			/// <summary>
			/// Gets or sets the ambient color of the light.
			/// </summary>
			property Color4 Ambient;

			/// <summary>
			/// Gets or sets the position of the light in world space. This member does not affect directional lights.
			/// </summary>
			property Vector3 Position;

			/// <summary>
			/// Gets or sets the direction of the light. This vector need not be normalized, but should have a non-zero length.
			/// </summary>
			property Vector3 Direction;

			/// <summary>
			/// Gets or sets the distance beyond which the light has no effect. This member does not affect directional lights.
			/// </summary>
			property float Range;

			/// <summary>
			/// Gets or sets the decrease in illumination between a spotlight's inner cone and the outer edge of the outer cone.
			/// </summary>
			property float Falloff;

			/// <summary>
			/// Gets or sets the first value in the equation that determines how the light intensity changes over distance.
			/// </summary>
			property float Attenuation0;

			/// <summary>
			/// Gets or sets the second value in the equation that determines how the light intensity changes over distance.
			/// </summary>
			property float Attenuation1;

			/// <summary>
			/// Gets or sets the third value in the equation that determines how the light intensity changes over distance.
			/// </summary>
			property float Attenuation2;

			/// <summary>
			/// Gets or sets angle, in radians, of the spotlight's inner cone.
			/// </summary>
			property float Theta;

			/// <summary>
			/// Gets or sets the angle, in radians, of the spotlight's outer cone.
			/// </summary>
			property float Phi;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Light left, Light right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Light left, Light right );

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
			virtual bool Equals( Light other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Light% value1, Light% value2 );
		};
	}
}