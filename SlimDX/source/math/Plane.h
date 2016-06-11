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

#include "../design/PlaneConverter.h"

#include "Enums.h"
#include "Vector3.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	value class BoundingBox;
	value class BoundingSphere;
	value class Quaternion;

	/// <summary>
	/// Defines a plane in three dimensions.
	/// </summary>
	/// <unmanaged>D3DXPLANE</unmanaged>
	[System::Serializable]
	[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential )]
	[System::ComponentModel::TypeConverter( SlimDX::Design::PlaneConverter::typeid )]
	public value class Plane : System::IEquatable<Plane>
	{
	public:
		/// <summary>
		/// The normal vector of the plane.
		/// </summary>
		Vector3 Normal;

		/// <summary>
		/// The distance of the plane along its normal from the origin.
		/// </summary>
		float D;

		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDX::Plane"/> class.
		/// </summary>
		/// <param name="a">X component of the normal defining the plane.</param>
		/// <param name="b">Y component of the normal defining the plane.</param>
		/// <param name="c">Z component of the normal defining the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		Plane( float a, float b, float c, float d );

		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDX::Plane"/> class.
		/// </summary>
		/// <param name="normal">The normal vector to the plane.</param>
		/// <param name="d">Distance of the plane along its normal from the origin.</param>
		Plane( Vector3 normal, float d );

		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDX::Plane"/> class.
		/// </summary>
		/// <param name="point">Any point that lies along the plane.</param>
		/// <param name="normal">The normal vector to the plane.</param>
		Plane( Vector3 point, Vector3 normal );

		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDX::Plane"/> class.
		/// </summary>
		/// <param name="point1">First point of a triangle defining the plane.</param>
		/// <param name="point2">Second point of a triangle defining the plane.</param>
		/// <param name="point3">Third point of a triangle defining the plane.</param>
		Plane( Vector3 point1, Vector3 point2, Vector3 point3 );

		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDX::Plane"/> class.
		/// </summary>
		/// <param name="value">
		/// A vector with the X, Y, and Z components defining the normal to the plane.
		/// The W component defines the distance of the plane along its normal from the origin.
		/// </param>
		Plane( Vector4 value );

		/// <summary>
		/// Calculates the dot product of the specified vector and plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="point">The source vector.</param>
		/// <returns>The dot product of the specified vector and plane.</returns>
		static float Dot( Plane plane, Vector4 point );

		/// <summary>
		/// Calculates the dot product of a specified vector and the normal of the plane plus the distance value of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="point">The source vector.</param>
		/// <returns>The dot product of a specified vector and the normal of the Plane plus the distance value of the plane.</returns>
		static float DotCoordinate( Plane plane, Vector3 point );

		/// <summary>
		/// Calculates the dot product of the specified vector and the normal of the plane.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="point">The source vector.</param>
		/// <returns>The dot product of the specified vector and the normal of the plane.</returns>
		static float DotNormal( Plane plane, Vector3 point );

		/// <summary>
		/// Changes the coefficients of the normal vector of the plane to make it of unit length.
		/// </summary>
		void Normalize();
		
		/// <summary>
		/// Changes the coefficients of the normal vector of the plane to make it of unit length.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <returns>The normalized plane.</returns>
		static Plane Normalize( Plane plane );
		
		/// <summary>
		/// Changes the coefficients of the normal vector of the plane to make it of unit length.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="result">When the method completes, contains the normalized plane.</param>
		static void Normalize( Plane% plane, [Out] Plane% result );

		/// <summary>
		/// Transforms a normalized plane by a matrix.
		/// </summary>
		/// <param name="plane">The normalized source plane.</param>
		/// <param name="transformation">The transformation matrix.</param>
		/// <returns>The transformed plane.</returns>
		static Plane Transform( Plane plane, Matrix transformation );

		/// <summary>
		/// Transforms a normalized plane by a matrix.
		/// </summary>
		/// <param name="plane">The normalized source plane.</param>
		/// <param name="transformation">The transformation matrix.</param>
		/// <param name="result">When the method completes, contains the transformed plane.</param>
		static void Transform( Plane% plane, Matrix% transformation, [Out] Plane% result );

		/// <summary>
		/// Transforms an array of normalized planes by a matrix.
		/// </summary>
		/// <param name="planes">The normalized source planes.</param>
		/// <param name="transformation">The transformation matrix.</param>
		/// <returns>The transformed planes.</returns>
		static array<Plane>^ Transform( array<Plane>^ planes, Matrix% transformation );

		/// <summary>
		/// Transforms a normalized plane by a quaternion rotation.
		/// </summary>
		/// <param name="plane">The normalized source plane.</param>
		/// <param name="rotation">The quaternion rotation.</param>
		/// <returns>The transformed plane.</returns>
		static Plane Transform( Plane plane, Quaternion rotation );

		/// <summary>
		/// Transforms a normalized plane by a quaternion rotation.
		/// </summary>
		/// <param name="plane">The normalized source plane.</param>
		/// <param name="rotation">The quaternion rotation.</param>
		/// <param name="result">When the method completes, contains the transformed plane.</param>
		static void Transform( Plane% plane, Quaternion% rotation, [Out] Plane% result );

		/// <summary>
		/// Transforms an array of normalized planes by a quaternion rotation.
		/// </summary>
		/// <param name="planes">The normalized source planes.</param>
		/// <param name="rotation">The quaternion rotation.</param>
		/// <returns>The transformed planes.</returns>
		static array<Plane>^ Transform( array<Plane>^ planes, Quaternion% rotation );

		/// <summary>
		/// Finds the intersection between a plane and a line.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="start">The start point of the line.</param>
		/// <param name="end">The end point of the line.</param>
		/// <param name="intersectPoint">If an intersection is found, contains the intersection point between the line and the plane.</param>
		/// <returns><c>true</c> if an intersection is found; <c>false</c> otherwise.</returns>
		static bool Intersects( Plane plane, Vector3 start, Vector3 end, [Out] Vector3% intersectPoint );

		/// <summary>
		/// Finds the intersection between a plane and a box.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="box">The box to check for intersection.</param>
		/// <returns>A value from the <see cref="PlaneIntersectionType"/> enumeration describing the result of the intersection test.</returns>
		static PlaneIntersectionType Intersects( Plane plane, BoundingBox box );
		
		/// <summary>
		/// Finds the intersection between a plane and a sphere.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="sphere">The sphere to check for intersection.</param>
		/// <returns>A value from the <see cref="PlaneIntersectionType"/> enumeration describing the result of the intersection test.</returns>
		static PlaneIntersectionType Intersects( Plane plane, BoundingSphere sphere );

		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		static Plane Multiply( Plane plane, float scale );

		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <param name="result">When the method completes, contains the scaled plane.</param>
		static void  Multiply( Plane% plane, float scale, [Out] Plane% result );

		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		static Plane operator * ( Plane plane, float scale );

		/// <summary>
		/// Scales the plane by the given scaling factor.
		/// </summary>
		/// <param name="plane">The source plane.</param>
		/// <param name="scale">The scaling factor.</param>
		/// <returns>The scaled plane.</returns>
		static Plane operator * ( float scale, Plane plane );

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator == ( Plane left, Plane right );

		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		static bool operator != ( Plane left, Plane right );

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
		virtual bool Equals( Plane other );

		/// <summary>
		/// Determines whether the specified object instances are considered equal. 
		/// </summary>
		/// <param name="value1"></param>
		/// <param name="value2"></param>
		/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
		/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
		static bool Equals( Plane% value1, Plane% value2 );
	};
}
