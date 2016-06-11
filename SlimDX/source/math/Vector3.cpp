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

#include "../Utilities.h"

#include "Quaternion.h"
#include "Matrix.h"
#include "Vector2.h"
#include "Vector3.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Vector3::Vector3( float value )
	{
		X = value;
		Y = value;
		Z = value;
	}
	
	Vector3::Vector3( Vector2 value, float z )
	{
		X = value.X;
		Y = value.Y;
		Z = z;
	}
	
	Vector3::Vector3( float x, float y, float z )
	{
		X = x;
		Y = y;
		Z = z;
	}
	
	float Vector3::default::get( int index )
	{
		switch( index )
		{
		case 0:
			return X;

		case 1:
			return Y;

		case 2:
			return Z;

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector3 run from 0 to 2, inclusive." );
		}
	}
	
	void Vector3::default::set( int index, float value )
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

		default:
			throw gcnew ArgumentOutOfRangeException( "index", "Indices for Vector3 run from 0 to 2, inclusive." );
		}
	}
	
	float Vector3::Length()
	{
		return static_cast<float>( Math::Sqrt( (X * X) + (Y * Y) + (Z * Z) ) );
	}
	
	float Vector3::LengthSquared()
	{
		return (X * X) + (Y * Y) + (Z * Z);
	}

	void Vector3::Normalize()
	{
		float length = Length();
		if( length == 0 )
			return;
		float num = 1 / length;
		X *= num;
		Y *= num;
		Z *= num;
	}
	
	Vector3 Vector3::Add( Vector3 left, Vector3 right )
	{
		return Vector3( left.X + right.X, left.Y + right.Y, left.Z + right.Z );
	}
	
	void Vector3::Add( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		result = Vector3( left.X + right.X, left.Y + right.Y, left.Z + right.Z );
	}
	
	Vector3 Vector3::Subtract( Vector3 left, Vector3 right )
	{
		return Vector3( left.X - right.X, left.Y - right.Y, left.Z - right.Z );
	}
	
	void Vector3::Subtract( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		result = Vector3( left.X - right.X, left.Y - right.Y, left.Z - right.Z );
	}
	
	Vector3 Vector3::Modulate( Vector3 left, Vector3 right )
	{
		return Vector3( left.X * right.X, left.Y * right.Y, left.Z * right.Z );
	}
	
	void Vector3::Modulate( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		result = Vector3( left.X * right.X, left.Y * right.Y, left.Z * right.Z );
	}
	
	Vector3 Vector3::Multiply( Vector3 value, float scale )
	{
		return Vector3( value.X * scale, value.Y * scale, value.Z * scale );
	}
	
	void Vector3::Multiply( Vector3% value, float scale, [Out] Vector3% result )
	{
		result = Vector3( value.X * scale, value.Y * scale, value.Z * scale );
	}
	
	Vector3 Vector3::Divide( Vector3 value, float scale )
	{
		return Vector3( value.X / scale, value.Y / scale, value.Z / scale );
	}
	
	void Vector3::Divide( Vector3% value, float scale, [Out] Vector3% result )
	{
		result = Vector3( value.X / scale, value.Y / scale, value.Z / scale );
	}
	
	Vector3 Vector3::Negate( Vector3 value )
	{
		return Vector3( -value.X, -value.Y, -value.Z );
	}
	
	void Vector3::Negate( Vector3% value, [Out] Vector3% result )
	{
		result = Vector3( -value.X, -value.Y, -value.Z );
	}
	
	Vector3 Vector3::Barycentric( Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2 )
	{
		Vector3 vector;
		vector.X = (value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X));
		vector.Y = (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y));
		vector.Z = (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z));
		return vector;
	}
	
	void Vector3::Barycentric( Vector3% value1, Vector3% value2, Vector3% value3, float amount1, float amount2, [Out] Vector3% result )
	{
		result = Vector3((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)),
			(value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)),
			(value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z)) );
	}
	
	Vector3 Vector3::CatmullRom( Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount )
	{
		Vector3 vector;
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

		return vector;
	}
	
	void Vector3::CatmullRom( Vector3% value1, Vector3% value2, Vector3% value3, Vector3% value4, float amount, [Out] Vector3% result )
	{
		float squared = amount * amount;
		float cubed = amount * squared;
		
		Vector3 r;

		r.X = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) + 
			(((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) + 
			((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));

		r.Y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + 
			(((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) + 
			((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));

		r.Z = 0.5f * ((((2.0f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + 
			(((((2.0f * value1.Z) - (5.0f * value2.Z)) + (4.0f * value3.Z)) - value4.Z) * squared)) + 
			((((-value1.Z + (3.0f * value2.Z)) - (3.0f * value3.Z)) + value4.Z) * cubed));

		result = r;
	}
	
	Vector3 Vector3::Clamp( Vector3 value, Vector3 min, Vector3 max )
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

		return Vector3( x, y, z );
	}
	
	void Vector3::Clamp( Vector3% value, Vector3% min, Vector3% max, [Out] Vector3% result )
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

		result = Vector3( x, y, z );
	}
	
	Vector3 Vector3::Hermite( Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount )
	{
		Vector3 vector;
		float squared = amount * amount;
		float cubed = amount * squared;
		float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
		float part2 = (-2.0f * cubed) + (3.0f * squared);
		float part3 = (cubed - (2.0f * squared)) + amount;
		float part4 = cubed - squared;

		vector.X = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
		vector.Y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);
		vector.Z = (((value1.Z * part1) + (value2.Z * part2)) + (tangent1.Z * part3)) + (tangent2.Z * part4);

		return vector;
	}
	
	void Vector3::Hermite( Vector3% value1, Vector3% tangent1, Vector3% value2, Vector3% tangent2, float amount, [Out] Vector3% result )
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
	}
	
	Vector3 Vector3::Lerp( Vector3 start, Vector3 end, float factor )
	{
		Vector3 vector;

		vector.X = start.X + ((end.X - start.X) * factor);
		vector.Y = start.Y + ((end.Y - start.Y) * factor);
		vector.Z = start.Z + ((end.Z - start.Z) * factor);

		return vector;
	}
	
	void Vector3::Lerp( Vector3% start, Vector3% end, float factor, [Out] Vector3% result )
	{
		result.X = start.X + ((end.X - start.X) * factor);
		result.Y = start.Y + ((end.Y - start.Y) * factor);
		result.Z = start.Z + ((end.Z - start.Z) * factor);
	}
	
	Vector3 Vector3::SmoothStep( Vector3 start, Vector3 end, float amount )
	{
		Vector3 vector;

		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		vector.X = start.X + ((end.X - start.X) * amount);
		vector.Y = start.Y + ((end.Y - start.Y) * amount);
		vector.Z = start.Z + ((end.Z - start.Z) * amount);

		return vector;
	}
	
	void Vector3::SmoothStep( Vector3% start, Vector3% end, float amount, [Out] Vector3% result )
	{
		amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
		amount = (amount * amount) * (3.0f - (2.0f * amount));

		result.X = start.X + ((end.X - start.X) * amount);
		result.Y = start.Y + ((end.Y - start.Y) * amount);
		result.Z = start.Z + ((end.Z - start.Z) * amount);
	}
	
	float Vector3::Distance( Vector3 value1, Vector3 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;
		float z = value1.Z - value2.Z;

		return static_cast<float>( Math::Sqrt( (x * x) + (y * y) + (z * z) ) );
	}
	
	float Vector3::DistanceSquared( Vector3 value1, Vector3 value2 )
	{
		float x = value1.X - value2.X;
		float y = value1.Y - value2.Y;
		float z = value1.Z - value2.Z;

		return (x * x) + (y * y) + (z * z);
	}
	
	float Vector3::Dot( Vector3 left, Vector3 right )
	{
		return (left.X * right.X + left.Y * right.Y + left.Z * right.Z);
	}
	
	Vector3 Vector3::Cross( Vector3 left, Vector3 right )
	{
		Vector3 result;
		result.X = left.Y * right.Z - left.Z * right.Y;
		result.Y = left.Z * right.X - left.X * right.Z;
		result.Z = left.X * right.Y - left.Y * right.X;
		return result;
	}
	
	void Vector3::Cross( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		Vector3 r;
		r.X = left.Y * right.Z - left.Z * right.Y;
		r.Y = left.Z * right.X - left.X * right.Z;
		r.Z = left.X * right.Y - left.Y * right.X; 

		result = r;
	}
	
	Vector3 Vector3::Reflect( Vector3 vector, Vector3 normal )
	{
		Vector3 result;
		float dot = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);

		result.X = vector.X - ((2.0f * dot) * normal.X);
		result.Y = vector.Y - ((2.0f * dot) * normal.Y);
		result.Z = vector.Z - ((2.0f * dot) * normal.Z);

		return result;
	}
	
	void Vector3::Reflect( Vector3% vector, Vector3% normal, [Out] Vector3% result )
	{
		float dot = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);

		result.X = vector.X - ((2.0f * dot) * normal.X);
		result.Y = vector.Y - ((2.0f * dot) * normal.Y);
		result.Z = vector.Z - ((2.0f * dot) * normal.Z);
	}
	
	Vector3 Vector3::Normalize( Vector3 vector )
	{
		vector.Normalize();
		return vector;
	}
	
	void Vector3::Normalize( Vector3% vector, [Out] Vector3% result )
	{
		result = Vector3(vector);
		result.Normalize();
	}
	
	Vector4 Vector3::Transform( Vector3 vector, Matrix transform )
	{
		Vector4 result;

		result.X = (((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + transform.M41;
		result.Y = (((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + transform.M42;
		result.Z = (((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + transform.M43;
		result.W = (((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + transform.M44;

		return result;
	}
	
	void Vector3::Transform( Vector3% vector, Matrix% transform, [Out] Vector4% result )
	{
		result = Vector4();
		result.X = (((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + transform.M41;
		result.Y = (((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + transform.M42;
		result.Z = (((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + transform.M43;
		result.W = (((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + transform.M44;
	}
	
	void Vector3::Transform( Vector3* vectorsIn, int inputStride, Matrix* transformation, Vector4* vectorsOut, int outputStride, int count )
	{
		D3DXVec3TransformArray( reinterpret_cast<D3DXVECTOR4*>( vectorsOut ), outputStride,
			reinterpret_cast<const D3DXVECTOR3*>( vectorsIn ), inputStride,
			reinterpret_cast<const D3DXMATRIX*>( transformation ), count );
	}

	void Vector3::Transform( array<Vector3>^ vectorsIn, Matrix% transformation, array<Vector4>^ vectorsOut, int offset, int count )
	{
		if(vectorsIn->Length != vectorsOut->Length)
			throw gcnew ArgumentException( "Input and output arrays must be the same size.", "vectorsOut" );
		Utilities::CheckArrayBounds( vectorsIn, offset, count );

		pin_ptr<Vector3> pinnedIn = &vectorsIn[offset];
		pin_ptr<Matrix> pinnedMatrix = &transformation;
		pin_ptr<Vector4> pinnedOut = &vectorsOut[offset];

		Transform( pinnedIn, pinnedMatrix, pinnedOut, count );
	}

	array<Vector4>^ Vector3::Transform( array<Vector3>^ vectors, Matrix% transform )
	{
		int count = vectors->Length;
		array<Vector4>^ results = gcnew array<Vector4>( count );

		/*for( int i = 0; i < count; i++ )
		{
			Vector4 r;
			r.X = (((vectors[i].X * transform.M11) + (vectors[i].Y * transform.M21)) + (vectors[i].Z * transform.M31)) + transform.M41;
			r.Y = (((vectors[i].X * transform.M12) + (vectors[i].Y * transform.M22)) + (vectors[i].Z * transform.M32)) + transform.M42;
			r.Z = (((vectors[i].X * transform.M13) + (vectors[i].Y * transform.M23)) + (vectors[i].Z * transform.M33)) + transform.M43;
			r.W = (((vectors[i].X * transform.M14) + (vectors[i].Y * transform.M24)) + (vectors[i].Z * transform.M34)) + transform.M44;
		
			results[i] = r;
		}*/
		Transform( vectors, transform, results );
		return results;
	}
	
	Vector4 Vector3::Transform( Vector3 value, Quaternion rotation )
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
		vector.W = 1.0f;

		return vector;
	}
	
	void Vector3::Transform( Vector3% value, Quaternion% rotation, [Out] Vector4% result )
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

		result = Vector4();
		result.X = ((value.X * ((1.0f - yy) - zz)) + (value.Y * (xy - wz))) + (value.Z * (xz + wy));
		result.Y = ((value.X * (xy + wz)) + (value.Y * ((1.0f - xx) - zz))) + (value.Z * (yz - wx));
		result.Z = ((value.X * (xz - wy)) + (value.Y * (yz + wx))) + (value.Z * ((1.0f - xx) - yy));
		result.W = 1.0f;
	}
	
	array<Vector4>^ Vector3::Transform( array<Vector3>^ vectors, Quaternion% rotation )
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
			r.W = 1.0f;

			results[i] = r;
		}

		return results;
	}
	
	Vector3 Vector3::TransformCoordinate( Vector3 coord, Matrix transform )
	{
		Vector4 vector;

		vector.X = (((coord.X * transform.M11) + (coord.Y * transform.M21)) + (coord.Z * transform.M31)) + transform.M41;
		vector.Y = (((coord.X * transform.M12) + (coord.Y * transform.M22)) + (coord.Z * transform.M32)) + transform.M42;
		vector.Z = (((coord.X * transform.M13) + (coord.Y * transform.M23)) + (coord.Z * transform.M33)) + transform.M43;
		vector.W = 1 / ((((coord.X * transform.M14) + (coord.Y * transform.M24)) + (coord.Z * transform.M34)) + transform.M44);

		return Vector3( vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W );
	}
	
	void Vector3::TransformCoordinate( Vector3% coord, Matrix% transform, [Out] Vector3% result )
	{
		Vector4 vector;

		vector.X = (((coord.X * transform.M11) + (coord.Y * transform.M21)) + (coord.Z * transform.M31)) + transform.M41;
		vector.Y = (((coord.X * transform.M12) + (coord.Y * transform.M22)) + (coord.Z * transform.M32)) + transform.M42;
		vector.Z = (((coord.X * transform.M13) + (coord.Y * transform.M23)) + (coord.Z * transform.M33)) + transform.M43;
		vector.W = 1 / ((((coord.X * transform.M14) + (coord.Y * transform.M24)) + (coord.Z * transform.M34)) + transform.M44);

		result = Vector3( vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W );
	}
	
	void Vector3::TransformCoordinate( Vector3* coordsIn, int inputStride, Matrix* transformation, Vector3* coordsOut, int outputStride, int count )
	{
		D3DXVec3TransformCoordArray( reinterpret_cast<D3DXVECTOR3*>( coordsOut ), outputStride,
			reinterpret_cast<const D3DXVECTOR3*>( coordsIn ), inputStride,
			reinterpret_cast<const D3DXMATRIX*>( transformation ), count );
	}

	void Vector3::TransformCoordinate( array<Vector3>^ coordsIn, Matrix% transformation, array<Vector3>^ coordsOut, int offset, int count )
	{
		if(coordsIn->Length != coordsOut->Length)
			throw gcnew ArgumentException( "Input and output arrays must be the same size.", "coordinatesOut" );
		Utilities::CheckArrayBounds( coordsIn, offset, count );

		pin_ptr<Vector3> pinnedIn = &coordsIn[offset];
		pin_ptr<Matrix> pinnedMatrix = &transformation;
		pin_ptr<Vector3> pinnedOut = &coordsOut[offset];

		TransformCoordinate( pinnedIn, pinnedMatrix, pinnedOut, count );
	}
	
	array<Vector3>^ Vector3::TransformCoordinate( array<Vector3>^ coords, Matrix% transform )
	{
		if( coords == nullptr )
			throw gcnew ArgumentNullException( "coordinates" );

		Vector4 vector;
		int count = coords->Length;
		array<Vector3>^ results = gcnew array<Vector3>( count );

		/*for( int i = 0; i < count; i++ )
		{
			vector.X = (((coords[i].X * transform.M11) + (coords[i].Y * transform.M21)) + (coords[i].Z * transform.M31)) + transform.M41;
			vector.Y = (((coords[i].X * transform.M12) + (coords[i].Y * transform.M22)) + (coords[i].Z * transform.M32)) + transform.M42;
			vector.Z = (((coords[i].X * transform.M13) + (coords[i].Y * transform.M23)) + (coords[i].Z * transform.M33)) + transform.M43;
			vector.W = 1 / ((((coords[i].X * transform.M14) + (coords[i].Y * transform.M24)) + (coords[i].Z * transform.M34)) + transform.M44);
			results[i] = Vector3( vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W );
		}*/
		TransformCoordinate( coords, transform, results );
		return results;
	}
	
	Vector3 Vector3::TransformNormal( Vector3 normal, Matrix transform )
	{
		Vector3 vector;

		vector.X = ((normal.X * transform.M11) + (normal.Y * transform.M21)) + (normal.Z * transform.M31);
		vector.Y = ((normal.X * transform.M12) + (normal.Y * transform.M22)) + (normal.Z * transform.M32);
		vector.Z = ((normal.X * transform.M13) + (normal.Y * transform.M23)) + (normal.Z * transform.M33);

		return vector;
	}
	
	void Vector3::TransformNormal( Vector3% normal, Matrix% transform, [Out] Vector3% result )
	{
		result.X = ((normal.X * transform.M11) + (normal.Y * transform.M21)) + (normal.Z * transform.M31);
		result.Y = ((normal.X * transform.M12) + (normal.Y * transform.M22)) + (normal.Z * transform.M32);
		result.Z = ((normal.X * transform.M13) + (normal.Y * transform.M23)) + (normal.Z * transform.M33);
	}
	
	void Vector3::TransformNormal( Vector3* normalsIn, int inputStride, Matrix* transformation, Vector3* normalsOut, int outputStride, int count )
	{
		D3DXVec3TransformNormalArray( reinterpret_cast<D3DXVECTOR3*>( normalsOut ), outputStride,
			reinterpret_cast<const D3DXVECTOR3*>( normalsIn ), inputStride,
			reinterpret_cast<const D3DXMATRIX*>( transformation ), count );
	}

	void Vector3::TransformNormal( array<Vector3>^ normalsIn, Matrix% transformation, array<Vector3>^ normalsOut, int offset, int count )
	{
		if(normalsIn->Length != normalsOut->Length)
			throw gcnew ArgumentException( "Input and output arrays must be the same size.", "normalsOut" );
		Utilities::CheckArrayBounds( normalsOut, offset, count );

		pin_ptr<Vector3> pinnedIn = &normalsIn[offset];
		pin_ptr<Matrix> pinnedMatrix = &transformation;
		pin_ptr<Vector3> pinnedOut = &normalsOut[offset];

		TransformNormal( pinnedIn, pinnedMatrix, pinnedOut, count );
	}
	
	array<Vector3>^ Vector3::TransformNormal( array<Vector3>^ normals, Matrix% transform )
	{
		if( normals == nullptr )
			throw gcnew ArgumentNullException( "normals" );

		int count = normals->Length;
		array<Vector3>^ results = gcnew array<Vector3>( count );

		/*for( int i = 0; i < count; i++ )
		{
			Vector3 r;
			r.X = ((normals[i].X * transform.M11) + (normals[i].Y * transform.M21)) + (normals[i].Z * transform.M31);
			r.Y = ((normals[i].X * transform.M12) + (normals[i].Y * transform.M22)) + (normals[i].Z * transform.M32);
			r.Z = ((normals[i].X * transform.M13) + (normals[i].Y * transform.M23)) + (normals[i].Z * transform.M33);
		
			results[i] = r;
		}*/
		TransformNormal( normals, transform, results );
		return results;
	}
	
	Vector3 Vector3::Project( Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection )
	{
		Vector3::TransformCoordinate( vector, worldViewProjection, vector );
		return Vector3( ( ( 1.0f + vector.X ) * 0.5f * width ) + x, ( ( 1.0f - vector.Y ) * 0.5f * height ) + y, ( vector.Z * ( maxZ - minZ ) ) + minZ );
	}
	
	void Vector3::Project( Vector3% vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix% worldViewProjection, [Out] Vector3% result )
	{
		Vector3 v;
		Vector3::TransformCoordinate( vector, worldViewProjection, v );

		result = Vector3( ( ( 1.0f + v.X ) * 0.5f * width ) + x, ( ( 1.0f - v.Y ) * 0.5f * height ) + y, ( v.Z * ( maxZ - minZ ) ) + minZ );
	}
	
	Vector3 Vector3::Unproject( Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection )
	{
		Vector3 v;
		Matrix matrix;
		Matrix::Invert( worldViewProjection, matrix );

		v.X = ( ( ( vector.X - x ) / width ) * 2.0f ) - 1.0f;
		v.Y = -( ( ( ( vector.Y - y ) / height ) * 2.0f ) - 1.0f );
		v.Z = ( vector.Z - minZ ) / ( maxZ - minZ );

		Vector3::TransformCoordinate( v, matrix, v );
		return v;
	}
	
	void Vector3::Unproject( Vector3% vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix% worldViewProjection, [Out] Vector3% result )
	{
		Vector3 v;
		Matrix matrix;
		Matrix::Invert( worldViewProjection, matrix );

		v.X = ( ( ( vector.X - x ) / width ) * 2.0f ) - 1.0f;
		v.Y = -( ( ( ( vector.Y - y ) / height ) * 2.0f ) - 1.0f );
		v.Z = ( vector.Z - minZ ) / ( maxZ - minZ );

		Vector3::TransformCoordinate( v, matrix, v );
		result = v;
	}
	
	Vector3 Vector3::Minimize( Vector3 left, Vector3 right )
	{
		Vector3 vector;
		vector.X = (left.X < right.X) ? left.X : right.X;
		vector.Y = (left.Y < right.Y) ? left.Y : right.Y;
		vector.Z = (left.Z < right.Z) ? left.Z : right.Z;
		return vector;
	}
	
	void Vector3::Minimize( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		result.X = (left.X < right.X) ? left.X : right.X;
		result.Y = (left.Y < right.Y) ? left.Y : right.Y;
		result.Z = (left.Z < right.Z) ? left.Z : right.Z;
	}
	
	Vector3 Vector3::Maximize( Vector3 left, Vector3 right )
	{
		Vector3 vector;
		vector.X = (left.X > right.X) ? left.X : right.X;
		vector.Y = (left.Y > right.Y) ? left.Y : right.Y;
		vector.Z = (left.Z > right.Z) ? left.Z : right.Z;
		return vector;
	}
	
	void Vector3::Maximize( Vector3% left, Vector3% right, [Out] Vector3% result )
	{
		result.X = (left.X > right.X) ? left.X : right.X;
		result.Y = (left.Y > right.Y) ? left.Y : right.Y;
		result.Z = (left.Z > right.Z) ? left.Z : right.Z;
	}
	
	Vector3 Vector3::operator + ( Vector3 left, Vector3 right )
	{
		return Vector3( left.X + right.X, left.Y + right.Y, left.Z + right.Z );
	}
	
	Vector3 Vector3::operator - ( Vector3 left, Vector3 right )
	{
		return Vector3( left.X - right.X, left.Y - right.Y, left.Z - right.Z );
	}
	
	Vector3 Vector3::operator - ( Vector3 value )
	{
		return Vector3( -value.X, -value.Y, -value.Z );
	}

	Vector3 Vector3::operator * ( Vector3 value, float scale )
	{
		return Vector3( value.X * scale, value.Y * scale, value.Z * scale );
	}

	Vector3 Vector3::operator * ( float scale, Vector3 vec )
	{
		return vec * scale;
	}
	
	Vector3 Vector3::operator / ( Vector3 value, float scale )
	{
		return Vector3( value.X / scale, value.Y / scale, value.Z / scale );
	}
	
	bool Vector3::operator == ( Vector3 left, Vector3 right )
	{
		return Vector3::Equals( left, right );
	}
	
	bool Vector3::operator != ( Vector3 left, Vector3 right )
	{
		return !Vector3::Equals( left, right );
	}

	String^ Vector3::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "X:{0} Y:{1} Z:{2}", X.ToString(CultureInfo::CurrentCulture), Y.ToString(CultureInfo::CurrentCulture), Z.ToString(CultureInfo::CurrentCulture) );
	}

	int Vector3::GetHashCode()
	{
		return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
	}

	bool Vector3::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Vector3>( value ) );
	}

	bool Vector3::Equals( Vector3 value )
	{
		return ( X == value.X && Y == value.Y && Z == value.Z );
	}

	bool Vector3::Equals( Vector3% value1, Vector3% value2 )
	{
		return ( value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z );
	}
}