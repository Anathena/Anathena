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

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Vector2::Vector2( float value )
	{
		X = value;
		Y = value;
	}
	
	Vector2::Vector2( float x, float y )
	{
		X = x;
		Y = y;
	}
	
	float Vector2::default::get( int index )
	{
		switch( index )
		{
		case 0:
			return X;

		case 1:
			return Y;

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector2 run from 0 to 1, inclusive." );
		}
	}
	
	void Vector2::default::set( int index, float value )
	{
		switch( index )
		{
		case 0:
			X = value;
			break;

		case 1:
			Y = value;
			break;

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector2 run from 0 to 1, inclusive." );
		}
	}
	
	float Vector2::Length()
	{
		return static_cast<float>( Math::Sqrt( (X * X) + (Y * Y) ) );
	}
	
	float Vector2::LengthSquared()
	{
		return (X * X) + (Y * Y);
	}
	
	void Vector2::Normalize()
	{
		float length = Length();
		if( length == 0 )
			return;
		float num = 1 / length;
		X *= num;
		Y *= num;
	}

	Vector2 Vector2::Add( Vector2 left, Vector2 right )
	{
		return Vector2( left.X + right.X, left.Y + right.Y );
	}
	
	void Vector2::Add( Vector2% left, Vector2% right, [Out] Vector2% result )
	{
		result = Vector2( left.X + right.X, left.Y + right.Y );
	}
	
	Vector2 Vector2::Subtract( Vector2 left, Vector2 right )
	{
		return Vector2( left.X - right.X, left.Y - right.Y );
	}
	
	void Vector2::Subtract( Vector2% left, Vector2% right, [Out] Vector2% result )
	{
		result = Vector2( left.X - right.X, left.Y - right.Y );
	}
	
	Vector2 Vector2::Modulate( Vector2 left, Vector2 right )
	{
		return Vector2( left.X * right.X, left.Y * right.Y );
	}
	
	void Vector2::Modulate( Vector2% left, Vector2% right, [Out] Vector2% result )
	{
		result = Vector2( left.X * right.X, left.Y * right.Y );
	}
	
	Vector2 Vector2::Multiply( Vector2 value, float scale )
	{
		return Vector2( value.X * scale, value.Y * scale );
	}
	
	void Vector2::Multiply( Vector2% value, float scale, [Out] Vector2% result )
	{
		result = Vector2( value.X * scale, value.Y * scale );
	}
	
	Vector2 Vector2::Divide( Vector2 value, float scale )
	{
		return Vector2( value.X / scale, value.Y / scale );
	}

	void Vector2::Divide( Vector2% value, float scale, [Out] Vector2% result )
	{
		result = Vector2( value.X / scale, value.Y / scale );
	}

	Vector2 Vector2::Negate( Vector2 value )
	{
		return Vector2( -value.X, -value.Y );
	}
	
	void Vector2::Negate( Vector2% value, [Out] Vector2% result )
	{
		result = Vector2( -value.X, -value.Y );
	}
	
	Vector2 Vector2::Barycentric( Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2 )
	{
		Vector2 vector;
		vector.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
		vector.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		return vector;
	}
	
	void Vector2::Barycentric( Vector2% value1, Vector2% value2, Vector2% value3, float amount1, float amount2, [Out] Vector2% result )
	{
		result = Vector2((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)),
			(value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)) );
	}
	
	Vector2 Vector2::CatmullRom( Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount )
	{
		Vector2 vector;
		float squared = amount * amount;
		float cubed = amount * squared;

		vector.X = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) + 
			(((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) + 
			((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));

		vector.Y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + 
			(((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) + 
			((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));

		return vector;
	}
	
	void Vector2::CatmullRom( Vector2% value1, Vector2% value2, Vector2% value3, Vector2% value4, float amount, [Out] Vector2% result )
	{
		float squared = amount * amount;
		float cubed = amount * squared;
		Vector2 r;

		r.X = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) + 
			(((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) + 
			((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));

		r.Y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + 
			(((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) + 
			((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));

		result = r;
	}
	
	Vector2 Vector2::Clamp( Vector2 value, Vector2 min, Vector2 max )
	{
		float x = value.X;
		x = (x > max.X) ? max.X : x;
		x = (x < min.X) ? min.X : x;

		float y = value.Y;
		y = (y > max.Y) ? max.Y : y;
		y = (y < min.Y) ? min.Y : y;

		return Vector2( x, y );
	}
	
	void Vector2::Clamp( Vector2% value, Vector2% min, Vector2% max, [Out] Vector2% result )
	{
		float x = value.X;
		x = (x > max.X) ? max.X : x;
		x = (x < min.X) ? min.X : x;

		float y = value.Y;
		y = (y > max.Y) ? max.Y : y;
		y = (y < min.Y) ? min.Y : y;

		result = Vector2( x, y );
	}
	
	Vector2 Vector2::Hermite( Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount )
	{
		Vector2 vector;
		float squared = amount * amount;
		float cubed = amount * squared;
		float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
		float part2 = (-2.0f * cubed) + (3.0f * squared);
		float part3 = (cubed - (2.0f * squared)) + amount;
		float part4 = cubed - squared;

		vector.X = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
		vector.Y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);

		return vector;
	}	
	
	void Vector2::Hermite( Vector2% value1, Vector2% tangent1, Vector2% value2, Vector2% tangent2, float amount, [Out] Vector2% result )
	{
		float squared = amount * amount;
		float cubed = amount * squared;
		float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
		float part2 = (-2.0f * cubed) + (3.0f * squared);
		float part3 = (cubed - (2.0f * squared)) + amount;
		float part4 = cubed - squared;

		Vector2 r;
		r.X = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
		r.Y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);

		result = r;
	}
	
	Vector2 Vector2::Lerp( Vector2 start, Vector2 end, float factor )
	{
		Vector2 vector;

		vector.X = start.X + ((end.X - start.X) * factor);
		vector.Y = start.Y + ((end.Y - start.Y) * factor);

		return vector;
	}
	
	void Vector2::Lerp( Vector2% start, Vector2% end, float factor, [Out] Vector2% result )
	{
		Vector2 r;
		r.X = start.X + ((end.X - start.X) * factor);
		r.Y = start.Y + ((end.Y - start.Y) * factor);

		result = r;
	}
	
	Vector2 Vector2::SmoothStep( Vector2 start, Vector2 end, float amount )
	{
		Vector2 vector;

		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		vector.X = start.X + ((end.X - start.X) * amount);
		vector.Y = start.Y + ((end.Y - start.Y) * amount);

		return vector;
	}
	
	void Vector2::SmoothStep( Vector2% start, Vector2% end, float amount, [Out] Vector2% result )
	{
		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		Vector2 r;
		r.X = start.X + ((end.X - start.X) * amount);
		r.Y = start.Y + ((end.Y - start.Y) * amount);

		result = r;
	}
	
	float Vector2::Distance( Vector2 value1, Vector2 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;

		return static_cast<float>( Math::Sqrt( (x * x) + (y * y) ) );
	}

	float Vector2::DistanceSquared( Vector2 value1, Vector2 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;

		return (x * x) + (y * y);
	}
	
	float Vector2::Dot( Vector2 left, Vector2 right )
	{
		return (left.X * right.X + left.Y * right.Y);
	}
	
	Vector2 Vector2::Normalize( Vector2 vector )
	{
		vector.Normalize();
		return vector;
	}
	
	void Vector2::Normalize( Vector2% vector, [Out] Vector2% result )
	{
		result = Vector2::Normalize(vector);
	}
	
	Vector4 Vector2::Transform( Vector2 vector, Matrix transform )
	{
		Vector4 result;

		result.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + transform.M41;
		result.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + transform.M42;
		result.Z = (vector.X * transform.M13) + (vector.Y * transform.M23) + transform.M43;
		result.W = (vector.X * transform.M14) + (vector.Y * transform.M24) + transform.M44;

		return result;
	}
	
	void Vector2::Transform( Vector2% vector, Matrix% transform, [Out] Vector4% result )
	{
		Vector4 r;
		r.X = (vector.X * transform.M11) + (vector.Y * transform.M21) + transform.M41;
		r.Y = (vector.X * transform.M12) + (vector.Y * transform.M22) + transform.M42;
		r.Z = (vector.X * transform.M13) + (vector.Y * transform.M23) + transform.M43;
		r.W = (vector.X * transform.M14) + (vector.Y * transform.M24) + transform.M44;

		result = r;
	}
	
	array<Vector4>^ Vector2::Transform( array<Vector2>^ vectors, Matrix% transform )
	{
		if( vectors == nullptr )
			throw gcnew ArgumentNullException( "vectors" );

		int count = vectors->Length;
		array<Vector4>^ results = gcnew array<Vector4>( count );

		for( int i = 0; i < count; i++ )
		{
			Vector4 r;
			r.X = (vectors[i].X * transform.M11) + (vectors[i].Y * transform.M21) + transform.M41;
			r.Y = (vectors[i].X * transform.M12) + (vectors[i].Y * transform.M22) + transform.M42;
			r.Z = (vectors[i].X * transform.M13) + (vectors[i].Y * transform.M23) + transform.M43;
			r.W = (vectors[i].X * transform.M14) + (vectors[i].Y * transform.M24) + transform.M44;

			results[i] = r;
		}

		return results;
	}
	
	Vector4 Vector2::Transform( Vector2 value, Quaternion rotation )
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

		vector.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz)));
		vector.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz)));
		vector.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx)));
		vector.W = 1.0f;

		return vector;
	}
	
	void Vector2::Transform( Vector2% value, Quaternion% rotation, [Out] Vector4% result )
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
		r.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz)));
		r.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz)));
		r.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx)));
		r.W = 1.0f;

		result = r;
	}

	array<Vector4>^ Vector2::Transform( array<Vector2>^ vectors, Quaternion% rotation )
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
			r.X = ((vectors[i].X * ((1.0f - yy) - zz)) + (vectors[i].Y * (xy - wz)));
			r.Y = ((vectors[i].X * (xy + wz)) + (vectors[i].Y * ((1.0f - xx) - zz)));
			r.Z = ((vectors[i].X * (xz - wy)) + (vectors[i].Y * (yz + wx)));
			r.W = 1.0f;

			results[i] = r;
		}

		return results;
	}
	
	Vector2 Vector2::TransformCoordinate( Vector2 coord, Matrix transform )
	{
		Vector4 vector;

		vector.X = (coord.X * transform.M11) + (coord.Y * transform.M21) + transform.M41;
		vector.Y = (coord.X * transform.M12) + (coord.Y * transform.M22) + transform.M42;
		vector.Z = (coord.X * transform.M13) + (coord.Y * transform.M23) + transform.M43;
		vector.W = 1 / ((coord.X * transform.M14) + (coord.Y * transform.M24) + transform.M44);

		return Vector2( vector.X * vector.W, vector.Y * vector.W );
	}
	
	void Vector2::TransformCoordinate( Vector2% coord, Matrix% transform, [Out] Vector2% result )
	{
		Vector4 vector;

		vector.X = (coord.X * transform.M11) + (coord.Y * transform.M21) + transform.M41;
		vector.Y = (coord.X * transform.M12) + (coord.Y * transform.M22) + transform.M42;
		vector.Z = (coord.X * transform.M13) + (coord.Y * transform.M23) + transform.M43;
		vector.W = 1 / ((coord.X * transform.M14) + (coord.Y * transform.M24) + transform.M44);

		result = Vector2( vector.X * vector.W, vector.Y * vector.W );
	}
	
	array<Vector2>^ Vector2::TransformCoordinate( array<Vector2>^ coords, Matrix% transform )
	{
		if( coords == nullptr )
			throw gcnew ArgumentNullException( "coordinates" );

		Vector4 vector;
		int count = coords->Length;
		array<Vector2>^ results = gcnew array<Vector2>( count );

		for( int i = 0; i < count; i++ )
		{
			vector.X = (coords[i].X * transform.M11) + (coords[i].Y * transform.M21) + transform.M41;
			vector.Y = (coords[i].X * transform.M12) + (coords[i].Y * transform.M22) + transform.M42;
			vector.Z = (coords[i].X * transform.M13) + (coords[i].Y * transform.M23) + transform.M43;
			vector.W = 1 / ((coords[i].X * transform.M14) + (coords[i].Y * transform.M24) + transform.M44);
			results[i] = Vector2( vector.X * vector.W, vector.Y * vector.W );
		}

		return results;
	}

	Vector2 Vector2::TransformNormal( Vector2 normal, Matrix transform )
	{
		Vector2 vector;

		vector.X = (normal.X * transform.M11) + (normal.Y * transform.M21);
		vector.Y = (normal.X * transform.M12) + (normal.Y * transform.M22);

		return vector;
	}
	
	void Vector2::TransformNormal( Vector2% normal, Matrix% transform, [Out] Vector2% result )
	{
		Vector2 r;
		r.X = (normal.X * transform.M11) + (normal.Y * transform.M21);
		r.Y = (normal.X * transform.M12) + (normal.Y * transform.M22);

		result = r;
	}
	
	array<Vector2>^ Vector2::TransformNormal( array<Vector2>^ normals, Matrix% transform )
	{
		if( normals == nullptr )
			throw gcnew ArgumentNullException( "normals" );

		int count = normals->Length;
		array<Vector2>^ results = gcnew array<Vector2>( count );

		for( int i = 0; i < count; i++ )
		{
			Vector2 r;
			r.X = (normals[i].X * transform.M11) + (normals[i].Y * transform.M21);
			r.Y = (normals[i].X * transform.M12) + (normals[i].Y * transform.M22);

			results[i] = r;
		}

		return results;
	}
	
	Vector2 Vector2::Minimize( Vector2 left, Vector2 right )
	{
		Vector2 vector;
		vector.X = (left.X < right.X) ? left.X : right.X;
		vector.Y = (left.Y < right.Y) ? left.Y : right.Y;
		return vector;
	}
	
	void Vector2::Minimize( Vector2% left, Vector2% right, [Out] Vector2% result )
	{
		Vector2 r;
		r.X = (left.X < right.X) ? left.X : right.X;
		r.Y = (left.Y < right.Y) ? left.Y : right.Y;

		result = r;
	}
	
	Vector2 Vector2::Maximize( Vector2 left, Vector2 right )
	{
		Vector2 vector;
		vector.X = (left.X > right.X) ? left.X : right.X;
		vector.Y = (left.Y > right.Y) ? left.Y : right.Y;
		return vector;
	}

	void Vector2::Maximize( Vector2% left, Vector2% right, [Out] Vector2% result )
	{
		Vector2 r;
		r.X = (left.X > right.X) ? left.X : right.X;
		r.Y = (left.Y > right.Y) ? left.Y : right.Y;

		result = r;
	}
	
	Vector2 Vector2::operator + ( Vector2 left, Vector2 right )
	{
		return Vector2( left.X + right.X, left.Y + right.Y );
	}

	Vector2 Vector2::operator - ( Vector2 left, Vector2 right )
	{
		return Vector2( left.X - right.X, left.Y - right.Y );
	}
	
	Vector2 Vector2::operator - ( Vector2 value )
	{
		return Vector2( -value.X, -value.Y );
	}
	
	Vector2 Vector2::operator * ( Vector2 value, float scale )
	{
		return Vector2( value.X * scale, value.Y * scale );
	}
	
	Vector2 Vector2::operator * ( float scale, Vector2 vec )
	{
		return vec * scale;
	}
	
	Vector2 Vector2::operator / ( Vector2 value, float scale )
	{
		return Vector2( value.X / scale, value.Y / scale );
	}
	
	bool Vector2::operator == ( Vector2 left, Vector2 right )
	{
		return Vector2::Equals( left, right );
	}
	
	bool Vector2::operator != ( Vector2 left, Vector2 right )
	{
		return !Vector2::Equals( left, right );
	}
	
	String^ Vector2::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "X:{0} Y:{1}", X.ToString(CultureInfo::CurrentCulture), Y.ToString(CultureInfo::CurrentCulture) );
	}

	int Vector2::GetHashCode()
	{
		return X.GetHashCode() + Y.GetHashCode();
	}

	bool Vector2::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Vector2>( value ) );
	}

	bool Vector2::Equals( Vector2 value )
	{
		return ( X == value.X && Y == value.Y );
	}

	bool Vector2::Equals( Vector2% value1, Vector2% value2 )
	{
		return ( value1.X == value2.X && value1.Y == value2.Y );
	}
}