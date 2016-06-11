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

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	[System::Serializable]
	public ref class SHVector : System::IEquatable<SHVector^>
	{
	private:
		array<float>^ coefficients;
		int order;

	public:
		literal int MinimumOrder = D3DXSH_MINORDER;
		literal int MaximumOrder = D3DXSH_MAXORDER;

		SHVector( int order );
		SHVector( int order, array<float>^ coefficients );

		property array<float>^ Coefficients { array<float>^ get() { return coefficients; } }
		property int Order { int get() { return order; } }
		property int Degree { int get() { return order - 1; } }

		property float default[int]
		{
			float get( int index ) { return coefficients[index]; }
			void set( int index, float value ) { coefficients[index] = value; }
		}

		static SHVector^ Add( SHVector^ left, SHVector^ right );
		static SHVector^ Rotate( SHVector^ vector, Matrix rotationMatrix );
		static SHVector^ RotateZ( SHVector^ vector, float angle );
		static SHVector^ Scale( SHVector^ vector, float scale );
		static float Dot( SHVector^ left, SHVector^ right );

		static SHVector^ EvaluateDirection( int order, Vector3 direction );
		static Result EvaluateDirectionalLight( int order, Vector3 direction, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue );
		static Result EvaluateConeLight( int order, Vector3 direction, float radius, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue );
		static Result EvaluateSphericalLight( int order, Vector3 direction, float radius, Color3 color, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue );
		static Result EvaluateHemisphereLight( int order, Vector3 direction, Color4 top, Color4 bottom, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue );

		static Result ProjectCubeMap( int order, SlimDX::Direct3D9::CubeTexture^ cubeMap, [Out] SHVector^% red, [Out] SHVector^% green, [Out] SHVector^% blue );

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The sum of the two vectors.</returns>
		static SHVector^ operator + ( SHVector^ left, SHVector^ right );

		/// <summary>
		/// Scales a vector by the given value.
		/// </summary>
		/// <param name="vector">The vector to scale.</param>
		/// <param name="scale">The amount by which to scale the vector.</param>
		/// <returns>The scaled vector.</returns>
		static SHVector^ operator * ( SHVector^ vector, float scale );

		/// <summary>
		/// Scales a vector by the given value.
		/// </summary>
		/// <param name="vector">The vector to scale.</param>
		/// <param name="scale">The amount by which to scale the vector.</param>
		/// <returns>The scaled vector.</returns>
		static SHVector^ operator * ( float scale, SHVector^ vector );

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator == ( SHVector^ left, SHVector^ right );

		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator != ( SHVector^ left, SHVector^ right );

		/// <summary>
		/// Converts the value of the object to its equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the value of this instance.</returns>
		virtual System::String^ ToString() override;

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
		virtual bool Equals( SHVector^ other );

		/// <summary>
		/// Determines whether the specified object instances are considered equal. 
		/// </summary>
		/// <param name="value1">The first value to compare.</param>
		/// <param name="value2">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
		/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
		static bool Equals( SHVector^ value1, SHVector^ value2 );
	};
}