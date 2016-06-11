#include "stdafx.h"
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

#include <d3dx9.h>

#include "Matrix.h"
#include "Quaternion.h"
#include "Vector2.h"
#include "Vector3.h"
#include "Vector4.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Vector4::Vector4( float value )
	{
		X = value;
		Y = value;
		Z = value;
		W = value;
	}
	
	Vector4::Vector4( Vector2 value, float z, float w )
	{
		X = value.X;
		Y = value.Y;
		Z = z;
		W = w;
	}
	
	Vector4::Vector4( Vector3 value, float w )
	{
		X = value.X;
		Y = value.Y;
		Z = value.Z;
		W = w;
	}
	
	Vector4::Vector4( float x, float y, float z, float w )
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	float Vector4::default::get( int index )
	{
		switch( index )
		{
		case 0:
			return X;

		case 1:
			return Y;

		case 2:
			return Z;

		case 3:
			return W;

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector4 run from 0 to 3, inclusive." );
		}
	}
	
	void Vector4::default::set( int index, float value )
	{
		switch( index )
		{
		case 0:
			X = value;
			break;

		case 1:
			Y = value;
			break;

		case 2:
			Z = value;
			break;

		case 3:
			W = value;
			break;

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector4 run from 0 to 3, inclusive." );
		}
	}
	
	float Vector4::Length()
	{
		return static_cast<float>( Math::Sqrt( (X * X) + (Y * Y) + (Z * Z) + (W * W) ) );
	}
	
	float Vector4::LengthSquared()
	{
		return (X * X) + (Y * Y) + (Z * Z) + (W * W);
	}
	
	void Vector4::Normalize()
	{
		float length = Length();
		if( length == 0 )
			return;
		float num = 1 / length;
		X *= num;
		Y *= num;
		Z *= num;
		W *= num;
	}
	
	Vector4 Vector4::Add( Vector4 left, Vector4 right )
	{
		return Vector4( left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W );
	}
	
	void Vector4::Add( Vector4% left, Vector4% right, [Out] Vector4% result )
	{
		result = Vector4( left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W );
	}
	
	Vector4 Vector4::Subtract( Vector4 left, Vector4 right )
	{
		return Vector4( left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W );
	}
	
	void Vector4::Subtract( Vector4% left, Vector4% right, [Out] Vector4% result )
	{
		result = Vector4( left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W );
	}
	
	Vector4 Vector4::Modulate( Vector4 left, Vector4 right )
	{
		return Vector4( left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W );
	}
	
	void Vector4::Modulate( Vector4% left, Vector4% right, [Out] Vector4% result )
	{
		result = Vector4( left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W );
	}
	
	Vector4 Vector4::Multiply( Vector4 value, float scale )
	{
		return Vector4( value.X * scale, value.Y * scale, value.Z * scale, value.W * scale );
	}
	
	void Vector4::Multiply( Vector4% value, float scale, [Out] Vector4% result )
	{
		result = Vector4( value.X * scale, value.Y * scale, value.Z * scale, value.W * scale );
	}
	
	Vector4 Vector4::Divide( Vector4 value, float scale )
	{
		return Vector4( value.X / scale, value.Y / scale, value.Z / scale, value.W / scale );
	}
	
	void Vector4::Divide( Vector4% value, float scale, [Out] Vector4% result )
	{
		result = Vector4( value.X / scale, value.Y / scale, value.Z / scale, value.W / scale );
	}
	
	Vector4 Vector4::Negate( Vector4 value )
	{
		return Vector4( -value.X, -value.Y, -value.Z, -value.W );
	}
	
	void Vector4::Negate( Vector4% value, [Out] Vector4% result )
	{
		result = Vector4( -value.X, -value.Y, -value.Z, -value.W );
	}
	
	Vector4 Vector4::Barycentric( Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2 )
	{
		Vector4 vector;
		vector.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
		vector.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		vector.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		vector.W = (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W));
		return vector;
	}
	
	void Vector4::Barycentric( Vector4% value1, Vector4% value2, Vector4% value3, float amount1, float amount2, [Out] Vector4% result )
	{
		result = Vector4((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)),
			(value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)),
			(value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z)),
			(value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W)) );
	}
	
	Vector4 Vector4::CatmullRom( Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount )
	{
		Vector4 vector;
		float squared = amount * amount;
		float cubed = amount * squared;

		vector.X = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) + 
			(((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) + 
			((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));

		vector.Y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + 
			(((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) + 
			((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));

		vector.Z = 0.5f * ((((2.0f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + 
			(((((2.0f * value1.Z) - (5.0f * value2.Z)) + (4.0f * value3.Z)) - value4.Z) * squared)) + 
			((((-value1.Z + (3.0f * value2.Z)) - (3.0f * value3.Z)) + value4.Z) * cubed));

		vector.W = 0.5f * ((((2.0f * value2.W) + ((-value1.W + value3.W) * amount)) + 
			(((((2.0f * value1.W) - (5.0f * value2.W)) + (4.0f * value3.W)) - value4.W) * squared)) + 
			((((-value1.W + (3.0f * value2.W)) - (3.0f * value3.W)) + value4.W) * cubed));

		return vector;
	}
	
	void Vector4::CatmullRom( Vector4% value1, Vector4% value2, Vector4% value3, Vector4% value4, float amount, [Out] Vector4% result )
	{
		float squared = amount * amount;
		float cubed = amount * squared;
		
		Vector4 r;

		r.X = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) + 
			(((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) + 
			((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));

		r.Y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + 
			(((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) + 
			((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));

		r.Z = 0.5f * ((((2.0f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + 
			(((((2.0f * value1.Z) - (5.0f * value2.Z)) + (4.0f * value3.Z)) - value4.Z) * squared)) + 
			((((-value1.Z + (3.0f * value2.Z)) - (3.0f * value3.Z)) + value4.Z) * cubed));

		r.W = 0.5f * ((((2.0f * value2.W) + ((-value1.W + value3.W) * amount)) + 
			(((((2.0f * value1.W) - (5.0f * value2.W)) + (4.0f * value3.W)) - value4.W) * squared)) + 
			((((-value1.W + (3.0f * value2.W)) - (3.0f * value3.W)) + value4.W) * cubed));

		result = r;
	}
	
	Vector4 Vector4::Clamp( Vector4 value, Vector4 min, Vector4 max )
	{
		float x = value.X;
		x = (x > max.X) ? max.X : x;
		x = (x < min.X) ? min.X : x;

		float y = value.Y;
		y = (y > max.Y) ? max.Y : y;
		y = (y < min.Y) ? min.Y : y;

		float z = value.Z;
		z = (z > max.Z) ? max.Z : z;
		z = (z < min.Z) ? min.Z : z;

		float w = value.W;
		w = (w > max.W) ? max.W : w;
		w = (w < min.W) ? min.W : w;

		return Vector4( x, y, z, w );
	}
	
	void Vector4::Clamp( Vector4% value, Vector4% min, Vector4% max, [Out] Vector4% result )
	{
		float x = value.X;
		x = (x > max.X) ? max.X : x;
		x = (x < min.X) ? min.X : x;

		float y = value.Y;
		y = (y > max.Y) ? max.Y : y;
		y = (y < min.Y) ? min.Y : y;

		float z = value.Z;
		z = (z > max.Z) ? max.Z : z;
		z = (z < min.Z) ? min.Z : z;

		float w = value.W;
		w = (w > max.W) ? max.W : w;
		w = (w < min.W) ? min.W : w;

		result = Vector4( x, y, z, w );
	}
	
	Vector4 Vector4::Hermite( Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount )
	{
		Vector4 vector;
		float squared = amount * amount;
		float cubed = amount * squared;
		float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
		float part2 = (-2.0f * cubed) + (3.0f * squared);
		float part3 = (cubed - (2.0f * squared)) + amount;
		float part4 = cubed - squared;

		vector.X = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
		vector.Y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);
		vector.Z = (((value1.Z * part1) + (value2.Z * part2)) + (tangent1.Z * part3)) + (tangent2.Z * part4);
		vector.W = (((value1.W * part1) + (value2.W * part2)) + (tangent1.W * part3)) + (tangent2.W * part4);

		return vector;
	}	
	
	void Vector4::Hermite( Vector4% value1, Vector4% tangent1, Vector4% value2, Vector4% tangent2, float amount, [Out] Vector4% result )
	{
		float squared = amount * amount;
		float cubed = amount * squared;
		float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
		float part2 = (-2.0f * cubed) + (3.0f * squared);
		float part3 = (cubed - (2.0f * squared)) + amount;
		float part4 = cubed - squared;

		result.X = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
		result.Y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);
		result.Z = (((value1.Z * part1) + (value2.Z * part2)) + (tangent1.Z * part3)) + (tangent2.Z * part4);
		result.W = (((value1.W * part1) + (value2.W * part2)) + (tangent1.W * part3)) + (tangent2.W * part4);
	}
	
	Vector4 Vector4::Lerp( Vector4 start, Vector4 end, float factor )
	{
		Vector4 vector;

		vector.X = start.X + ((end.X - start.X) * factor);
		vector.Y = start.Y + ((end.Y - start.Y) * factor);
		vector.Z = start.Z + ((end.Z - start.Z) * factor);
		vector.W = start.W + ((end.W - start.W) * factor);

		return vector;
	}
	
	void Vector4::Lerp( Vector4% start, Vector4% end, float factor, [Out] Vector4% result )
	{
		result.X = start.X + ((end.X - start.X) * factor);
		result.Y = start.Y + ((end.Y - start.Y) * factor);
		result.Z = start.Z + ((end.Z - start.Z) * factor);
		result.W = start.W + ((end.W - start.W) * factor);
	}
	
	Vector4 Vector4::SmoothStep( Vector4 start, Vector4 end, float amount )
	{
		Vector4 vector;

		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		vector.X = start.X + ((end.X - start.X) * amount);
		vector.Y = start.Y + ((end.Y - start.Y) * amount);
		vector.Z = start.Z + ((end.Z - start.Z) * amount);
		vector.W = start.W + ((end.W - start.W) * amount);

		return vector;
	}
	
	void Vector4::SmoothStep( Vector4% start, Vector4% end, float amount, [Out] Vector4% result )
	{
		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		result.X = start.X + ((end.X - start.X) * amount);
		result.Y = start.Y + ((end.Y - start.Y) * amount);
		result.Z = start.Z + ((end.Z - start.Z) * amount);
		result.W = start.W + ((end.W - start.W) * amount);
	}
	
	float Vector4::Distance( Vector4 value1, Vector4 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;
		float z = value1.Z - value2.Z;
		float w = value1.W - value2.W;

		return static_cast<float>( Math::Sqrt( (x * x) + (y * y) + (z * z) + (w * w) ) );
	}
	
	float Vector4::DistanceSquared( Vector4 value1, Vector4 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;
		float z = value1.Z - value2.Z;
		float w = value1.W - value2.W;

		return (x * x) + (y * y) + (z * z) + (w * w);
	}
	
	float Vector4::Dot( Vector4 left, Vector4 right )
	{
		return (left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W);
	}

	Vector4 Vector4::Normalize( Vector4 vector )
	{
		vector.Normalize();
		return vector;
	}
	
	void Vector4::Normalize( Vector4% vector, [Out] Vector4% result )
	{
		result = Vector4(vector);
		result.Normalize();
	}
	
	Vector4 Vector4::Transform( Vector4 vector, Matrix transform )
	{
		Vector4 result;

		result.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + (vector.Z * transform.M31) + (vector.W * transform.M41);
		result.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + (vector.Z * transform.M32) + (vector.W * transform.M42);
		result.Z = (vector.X * transform.M13) + (vector.Y * transform.M23) + (vector.Z * transform.M33) + (vector.W * transform.M43);
		result.W = (vector.X * transform.M14) + (vector.Y * transform.M24) + (vector.Z * transform.M34) + (vector.W * transform.M44);

		return result;
	}
	
	void Vector4::Transform( Vector4% vector, Matrix% transform, [Out] Vector4% result )
	{
		Vector4 r;
		r.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + (vector.Z * transform.M31) + (vector.W * transform.M41);
		r.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + (vector.Z * transform.M32) + (vector.W * transform.M42);
		r.Z = (vector.X * transform.M13) + (vector.Y * transform.M23) + (vector.Z * transform.M33) + (vector.W * transform.M43);
		r.W = (vector.X * transform.M14) + (vector.Y * transform.M24) + (vector.Z * transform.M34) + (vector.W * transform.M44);
	
		result = r;
	}
	
	array<Vector4>^ Vector4::Transform( array<Vector4>^ vectors, Matrix% transform )
	{
		if( vectors == nullptr )
			throw gcnew ArgumentNullException( "vectors" );

		int count = vectors->Length;
		array<Vector4>^ results = gcnew array<Vector4>( count );

		for( int i = 0; i < count; i++ )
		{
			Vector4 r;
			r.X = (vectors[i].X * transform.M11) + (vectors[i].Y * transform.M21) + (vectors[i].Z * transform.M31) + (vectors[i].W * transform.M41);
			r.Y = (vectors[i].X * transform.M12) + (vectors[i].Y * transform.M22) + (vectors[i].Z * transform.M32) + (vectors[i].W * transform.M42);
			r.Z = (vectors[i].X * transform.M13) + (vectors[i].Y * transform.M23) + (vectors[i].Z * transform.M33) + (vectors[i].W * transform.M43);
			r.W = (vectors[i].X * transform.M14) + (vectors[i].Y * transform.M24) + (vectors[i].Z * transform.M34) + (vectors[i].W * transform.M44);
		
			results[i] = r;
		}

		return results;
	}
	
	Vector4 Vector4::Transform( Vector4 value, Quaternion rotation )
	{
		Vector4 vector;
		float x = rotation.X + rotation.X;
		float y = rotation.Y + rotation.Y;
		float z = rotation.Z + rotation.Z;
		float wx = rotation.W * x;
		float wy = rotation.W * y;
		float wz = rotation.W * z;
		float xx = rotation.X * x;
		float xy = rotation.X * y;
		float xz = rotation.X * z;
		float yy = rotation.Y * y;
		float yz = rotation.Y * z;
		float zz = rotation.Z * z;

		vector.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz))) + (value.Z * (xz + wy));
		vector.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz))) + (value.Z * (yz - wx));
		vector.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx))) + (value.Z * ((1.0f - xx) - yy));
		vector.W = value.W;

		return vector;
	}
	
	void Vector4::Transform( Vector4% value, Quaternion% rotation, [Out] Vector4% result )
	{
		float x = rotation.X + rotation.X;
		float y = rotation.Y + rotation.Y;
		float z = rotation.Z + rotation.Z;
		float wx = rotation.W * x;
		float wy = rotation.W * y;
		float wz = rotation.W * z;
		float xx = rotation.X * x;
		float xy = rotation.X * y;
		float xz = rotation.X * z;
		float yy = rotation.Y * y;
		float yz = rotation.Y * z;
		float zz = rotation.Z * z;

		Vector4 r;
		r.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz))) + (value.Z * (xz + wy));
		r.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz))) + (value.Z * (yz - wx));
		r.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx))) + (value.Z * ((1.0f - xx) - yy));
		r.W = value.W;

		result = r;
	}
	
	array<Vector4>^ Vector4::Transform( array<Vector4>^ vectors, Quaternion% rotation )
	{
		if( vectors == nullptr )
			throw gcnew ArgumentNullException( "vectors" );

		int count = vectors->Length;
		array<Vector4>^ results = gcnew array<Vector4>( count );

		float x = rotation.X + rotation.X;
		float y = rotation.Y + rotation.Y;
		float z = rotation.Z + rotation.Z;
		float wx = rotation.W * x;
		float wy = rotation.W * y;
		float wz = rotation.W * z;
		float xx = rotation.X * x;
		float xy = rotation.X * y;
		float xz = rotation.X * z;
		float yy = rotation.Y * y;
		float yz = rotation.Y * z;
		float zz = rotation.Z * z;

		for( int i = 0; i < count; i++ )
		{
			Vector4 r;
			r.X = ((vectors[i].X * ((1.0f - yy) - zz)) + (vectors[i].Y * (xy - wz))) + (vectors[i].Z * (xz + wy));
			r.Y = ((vectors[i].X * (xy + wz)) + (vectors[i].Y * ((1.0f - xx) - zz))) + (vectors[i].Z * (yz - wx));
			r.Z = ((vectors[i].X * (xz - wy)) + (vectors[i].Y * (yz + wx))) + (vectors[i].Z * ((1.0f - xx) - yy));
			r.W = vectors[i].W;

			results[i] = r;
		}

		return results;
	}

	Vector4 Vector4::Minimize( Vector4 left, Vector4 right )
	{
		Vector4 vector;
		vector.X = (left.X < right.X) ? left.X : right.X;
		vector.Y = (left.Y < right.Y) ? left.Y : right.Y;
		vector.Z = (left.Z < right.Z) ? left.Z : right.Z;
		vector.W = (left.W < right.W) ? left.W : right.W;
		return vector;
	}
	
	void Vector4::Minimize( Vector4% left, Vector4% right, [Out] Vector4% result )
	{
		result.X = (left.X < right.X) ? left.X : right.X;
		result.Y = (left.Y < right.Y) ? left.Y : right.Y;
		result.Z = (left.Z < right.Z) ? left.Z : right.Z;
		result.W = (left.W < right.W) ? left.W : right.W;
	}

	Vector4 Vector4::Maximize( Vector4 left, Vector4 right )
	{
		Vector4 vector;
		vector.X = (left.X > right.X) ? left.X : right.X;
		vector.Y = (left.Y > right.Y) ? left.Y : right.Y;
		vector.Z = (left.Z > right.Z) ? left.Z : right.Z;
		vector.W = (left.W > right.W) ? left.W : right.W;
		return vector;
	}
	
	void Vector4::Maximize( Vector4% left, Vector4% right, [Out] Vector4% result )
	{
		result.X = (left.X > right.X) ? left.X : right.X;
		result.Y = (left.Y > right.Y) ? left.Y : right.Y;
		result.Z = (left.Z > right.Z) ? left.Z : right.Z;
		result.W = (left.W > right.W) ? left.W : right.W;
	}

	Vector4 Vector4::operator + ( Vector4 left, Vector4 right )
	{
		return Vector4( left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W );
	}
	
	Vector4 Vector4::operator - ( Vector4 left, Vector4 right )
	{
		return Vector4( left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W );
	}
	
	Vector4 Vector4::operator - ( Vector4 value )
	{
		return Vector4( -value.X, -value.Y, -value.Z, -value.W );
	}
	
	Vector4 Vector4::operator * ( Vector4 value, float scale )
	{
		return Vector4( value.X * scale, value.Y * scale, value.Z * scale, value.W * scale );
	}
	
	Vector4 Vector4::operator * ( float scale, Vector4 vec )
	{
		return vec * scale;
	}
	
	Vector4 Vector4::operator / ( Vector4 value, float scale )
	{
		return Vector4( value.X / scale, value.Y / scale, value.Z / scale, value.W / scale );
	}

	bool Vector4::operator == ( Vector4 left, Vector4 right )
	{
		return Vector4::Equals( left, right );
	}

	bool Vector4::operator != ( Vector4 left, Vector4 right )
	{
		return !Vector4::Equals( left, right );
	}

	String^ Vector4::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", X.ToString(CultureInfo::CurrentCulture), 
			Y.ToString(CultureInfo::CurrentCulture), Z.ToString(CultureInfo::CurrentCulture),
			W.ToString(CultureInfo::CurrentCulture) );
	}

	int Vector4::GetHashCode()
	{
		return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
	}

	bool Vector4::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Vector4>( value ) );
	}

	bool Vector4::Equals( Vector4 value )
	{
		return ( X == value.X && Y == value.Y && Z == value.Z && W == value.W );
	}

	bool Vector4::Equals( Vector4% value1, Vector4% value2 )
	{
		return ( value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W );
	}
}