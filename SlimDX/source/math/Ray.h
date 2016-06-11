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

#include "../design/RayConverter.h"

#include "Vector3.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	value class BoundingBox;
	value class BoundingSphere;
	value class Plane;

	/// <summary>
	/// Defines a ray in three dimensions, specified by a starting position and a direction.
	/// </summary>
	[System::Serializable]
	[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
	[System::ComponentModel::TypeConverter( SlimDX::Design::RayConverter::typeid )]
	public value class Ray : System::IEquatable<Ray>
	{
	public:
		/// <summary>
		/// Specifies the location of the ray's origin.
		/// </summary>
		Vector3 Position;

		/// <summary>
		/// A unit vector specifying the direction in which the ray is pointing.
		/// </summary>
		Vector3 Direction;

		/// <summary>
		/// Initializes a new instance of the <see cref="Ray"/> structure.
		/// </summary>
		/// <param name="position">The location of the ray's origin.</param>
		/// <param name="direction">A unit vector specifying the direction in which the ray is pointing.</param>
		Ray( Vector3 position, Vector3 direction );

		/// <summary>
		/// Determines whether a ray intersects the specified object.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="plane">A plane that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance at which the ray intersected the plane.</param>
		/// <returns><c>true</c> if the ray intersects the plane; otherwise, <c>false</c>.</returns>
		static bool Intersects( Ray ray, Plane plane, [Out] float% distance );
		
		/// <summary>
		/// Determines whether a ray intersects the specified object.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="vertex1">The first vertex of a triangle that will be tested for intersection.</param>
		/// <param name="vertex2">The second vertex of a triangle that will be tested for intersection.</param>
		/// <param name="vertex3">The third vertex of a triangle that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance at which the ray intersected the plane.</param>
		/// <returns><c>true</c> if the ray intersects the plane; otherwise, <c>false</c>.</returns>
		static bool Intersects( Ray ray, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, [Out] float% distance );

		/// <summary>
		/// Determines whether a ray intersects the specified object.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="vertex1">The first vertex of a triangle that will be tested for intersection.</param>
		/// <param name="vertex2">The second vertex of a triangle that will be tested for intersection.</param>
		/// <param name="vertex3">The third vertex of a triangle that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance at which the ray intersected the plane.</param>
		/// <param name="barycentricU">When the method completes, contains the U component of the barycentric hit coordinates.</param>
		/// <param name="barycentricV">When the method completes, contains the V component of the barycentric hit coordinates.</param>
		/// <returns><c>true</c> if the ray intersects the plane; otherwise, <c>false</c>.</returns>
		static bool Intersects( Ray ray, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, [Out] float% distance, [Out] float% barycentricU, [Out] float% barycentricV );
		
		/// <summary>
		/// Determines whether a ray intersects the specified object.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="box">A box that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance at which the ray intersected the plane.</param>
		/// <returns><c>true</c> if the ray intersects the plane; otherwise, <c>false</c>.</returns>
		static bool Intersects( Ray ray, BoundingBox box, [Out] float% distance );
		
		/// <summary>
		/// Determines whether a ray intersects the specified object.
		/// </summary>
		/// <param name="ray">The ray which will be tested for intersection.</param>
		/// <param name="sphere">A sphere that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance at which the ray intersected the plane.</param>
		/// <returns><c>true</c> if the ray intersects the plane; otherwise, <c>false</c>.</returns>
		static bool Intersects( Ray ray, BoundingSphere sphere, [Out] float% distance );

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator == ( Ray left, Ray right );

		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator != ( Ray left, Ray right );

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
		virtual bool Equals( Ray other );

		/// <summary>
		/// Determines whether the specified object instances are considered equal. 
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
		/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
		static bool Equals( Ray% value1, Ray% value2 );
	};
}
