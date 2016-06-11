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

#include "../design/BoundingSphereConverter.h"

#include "Enums.h"
#include "Vector3.h"

namespace SlimDX
{
	value class BoundingBox;
	value class Plane;
	value class Ray;
	
	/// <summary>
	/// A bounding sphere, specified by a center vector and a radius.
	/// </summary>
	/// <unmanaged>None</unmanaged>
	[System::Serializable]
	[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
	[System::ComponentModel::TypeConverter( SlimDX::Design::BoundingSphereConverter::typeid )]
	public value class BoundingSphere : System::IEquatable<BoundingSphere>
	{
	public:
		/// <summary>
		/// Specifies the center point of the sphere.
		/// </summary>
		Vector3 Center;

		/// <summary>
		/// The radius of the sphere.
		/// </summary>
		float Radius;

		/// <summary>
		/// Initializes a new instance of the <see cref="BoundingSphere"/> structure.
		/// </summary>
		/// <param name="center">The center of the bounding sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		BoundingSphere( Vector3 center, float radius );

		/// <summary>
		/// Determines whether the sphere contains the specified box.
		/// </summary>
		/// <param name="sphere">The sphere that will be checked for containment.</param>
		/// <param name="box">The box that will be checked for containment.</param>
		/// <returns>A member of the <see cref="ContainmentType"/> enumeration indicating whether the two objects intersect, are contained, or don't meet at all.</returns>
		static ContainmentType Contains( BoundingSphere sphere, BoundingBox box );
		
		/// <summary>
		/// Determines whether the sphere contains the specified sphere.
		/// </summary>
		/// <param name="sphere1">The first sphere that will be checked for containment.</param>
		/// <param name="sphere2">The second sphere that will be checked for containment.</param>
		/// <returns>A member of the <see cref="ContainmentType"/> enumeration indicating whether the two objects intersect, are contained, or don't meet at all.</returns>
		static ContainmentType Contains( BoundingSphere sphere1, BoundingSphere sphere2 );
		
		/// <summary>
		/// Determines whether the sphere contains the specified point.
		/// </summary>
		/// <param name="sphere">The sphere that will be checked for containment.</param>
		/// <param name="vector">The point that will be checked for containment.</param>
		/// <returns>A member of the <see cref="ContainmentType"/> enumeration indicating whether the two objects intersect, are contained, or don't meet at all.</returns>
		static ContainmentType Contains( BoundingSphere sphere, Vector3 vector );

		/// <summary>
		/// Constructs a <see cref="BoundingSphere"/> from a given box.
		/// </summary>
		/// <param name="box">The box that will designate the extents of the sphere.</param>
		/// <returns>The newly constructed bounding sphere.</returns>
		static BoundingSphere FromBox( BoundingBox box );

		/// <summary>
		/// Constructs a <see cref="BoundingSphere"/> that fully contains the given points.
		/// </summary>
		/// <param name="points">The points that will be contained by the sphere.</param>
		/// <returns>The newly constructed bounding sphere.</returns>
		static BoundingSphere FromPoints( array<Vector3>^ points );

		/// <summary>
		/// Constructs a <see cref="BoundingSphere"/> that is the as large as the total combined area of the two specified spheres.
		/// </summary>
		/// <param name="sphere1">The first sphere to merge.</param>
		/// <param name="sphere2">The second sphere to merge.</param>
		/// <returns>The newly constructed bounding sphere.</returns>
		static BoundingSphere Merge( BoundingSphere sphere1, BoundingSphere sphere2 );

		/// <summary>
		/// Determines whether a sphere intersects the specified object.
		/// </summary>
		/// <param name="sphere">The sphere which will be tested for intersection.</param>
		/// <param name="box">The box that will be tested for intersection.</param>
		/// <returns><c>true</c> if the two objects are intersecting; otherwise, <c>false</c>.</returns>
		static bool Intersects( BoundingSphere sphere, BoundingBox box );

		/// <summary>
		/// Determines whether a sphere intersects the specified object.
		/// </summary>
		/// <param name="sphere1">The first sphere which will be tested for intersection.</param>
		/// <param name="sphere2">The second sphere that will be tested for intersection.</param>
		/// <returns><c>true</c> if the two objects are intersecting; otherwise, <c>false</c>.</returns>
		static bool Intersects( BoundingSphere sphere1, BoundingSphere sphere2 );

		/// <summary>
		/// Determines whether a sphere intersects the specified object.
		/// </summary>
		/// <param name="sphere">The sphere which will be tested for intersection.</param>
		/// <param name="ray">The ray that will be tested for intersection.</param>
		/// <param name="distance">When the method completes, contains the distance from the ray's origin in which the intersection with the sphere occured.</param>
		/// <returns><c>true</c> if the two objects are intersecting; otherwise, <c>false</c>.</returns>
		static bool Intersects( BoundingSphere sphere, Ray ray, [Out] float% distance );

		/// <summary>
		/// Finds the intersection between a plane and a sphere.
		/// </summary>
		/// <param name="sphere">The sphere to check for intersection.</param>
		/// <param name="plane">The source plane.</param>
		/// <returns>A value from the <see cref="PlaneIntersectionType"/> enumeration describing the result of the intersection test.</returns>
		static PlaneIntersectionType Intersects( BoundingSphere sphere, Plane plane );

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator == ( BoundingSphere left, BoundingSphere right );

		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator != ( BoundingSphere left, BoundingSphere right );

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
		virtual bool Equals( BoundingSphere other );

		/// <summary>
		/// Determines whether the specified object instances are considered equal. 
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
		/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
		static bool Equals( BoundingSphere% value1, BoundingSphere% value2 );
	};
}