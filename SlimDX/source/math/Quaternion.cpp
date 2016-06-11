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
#include "Vector3.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Quaternion::Quaternion(float x, float y, float z, float w)
	{
		X = x;
		Y = y;
		Z = z;
		W = w;
	}

	Quaternion::Quaternion( Vector3 value, float w )
	{
		X = value.X;
		Y = value.Y;
		Z = value.Z;
		W = w;
	}

	Quaternion Quaternion::Identity::get()
	{
		Quaternion result;
		result.X = 0.0f;
		result.Y = 0.0f;
		result.Z = 0.0f;
		result.W = 1.0f;
		return result;
	}

	bool Quaternion::IsIdentity::get()
	{
		if( X != 0.0f || Y != 0.0f || Z != 0.0f )
			return false;

		return (W == 1.0f);
	}

	Vector3 Quaternion::Axis::get()
	{
		pin_ptr<Quaternion> pinThis = this;
		float angle;
		Vector3 axis;

		D3DXQuaternionToAxisAngle( (D3DXQUATERNION*) pinThis, (D3DXVECTOR3*) &axis, &angle );
		return axis;
	}

	float Quaternion::Angle::get()
	{
		pin_ptr<Quaternion> pinThis = this;
		float angle;
		Vector3 axis;

		D3DXQuaternionToAxisAngle( (D3DXQUATERNION*) pinThis, (D3DXVECTOR3*) &axis, &angle );
		return angle;
	}

	float Quaternion::Length()
	{
		return static_cast<float>( Math::Sqrt( (X * X) + (Y * Y) + (Z * Z) + (W * W) ) );
	}
	
	float Quaternion::LengthSquared()
	{
		return (X * X) + (Y * Y) + (Z * Z) + (W * W);
	}

	void Quaternion::Normalize()
	{
		float length = 1.0f / Length();
		X *= length;
		Y *= length;
		Z *= length;
		W *= length;
	}

	void Quaternion::Conjugate()
	{
		X = -X;
		Y = -Y;
		Z = -Z;
	}

	void Quaternion::Invert()
	{
		float lengthSq = 1.0f / ( (X * X) + (Y * Y) + (Z * Z) + (W * W) );
		X = -X * lengthSq;
		Y = -Y * lengthSq;
		Z = -Z * lengthSq;
		W = W * lengthSq;
	}

	Quaternion Quaternion::Add( Quaternion left, Quaternion right )
	{
		Quaternion result;
		result.X = left.X + right.X;
		result.Y = left.Y + right.Y;
		result.Z = left.Z + right.Z;
		result.W = left.W + right.W;
		return result;
	}

	void Quaternion::Add( Quaternion% left, Quaternion% right, [Out] Quaternion% result )
	{
		Quaternion r;
		r.X = left.X + right.X;
		r.Y = left.Y + right.Y;
		r.Z = left.Z + right.Z;
		r.W = left.W + right.W;

		result = r;
	}

	Quaternion Quaternion::Barycentric( Quaternion q1, Quaternion q2, Quaternion q3, float f, float g )
	{
		Quaternion result;

		D3DXQuaternionBaryCentric( (D3DXQUATERNION*) &result, (D3DXQUATERNION*) &q1,
			(D3DXQUATERNION*) &q2, (D3DXQUATERNION*) &q3, f, g );

		return result;
	}

	void Quaternion::Barycentric( Quaternion% q1, Quaternion% q2, Quaternion% q3, float f, float g, [Out] Quaternion% result )
	{
		pin_ptr<Quaternion> pin1 = &q1;
		pin_ptr<Quaternion> pin2 = &q2;
		pin_ptr<Quaternion> pin3 = &q3;
		pin_ptr<Quaternion> pinResult = &result;

		D3DXQuaternionBaryCentric( (D3DXQUATERNION*) pinResult, (D3DXQUATERNION*) pin1,
			(D3DXQUATERNION*) pin2, (D3DXQUATERNION*) pin3, f, g );
	}

	Quaternion Quaternion::Conjugate( Quaternion quat )
	{
		Quaternion result;
		result.X = -quat.X;
		result.Y = -quat.Y;
		result.Z = -quat.Z;
		result.W = quat.W;
		return result;
	}

	void Quaternion::Conjugate( Quaternion% quat, [Out] Quaternion% result )
	{
		result.X = -quat.X;
		result.Y = -quat.Y;
		result.Z = -quat.Z;
		result.W = quat.W;
	}

	Quaternion Quaternion::Divide( Quaternion left, Quaternion right )
	{
		Quaternion result;
		result.X = left.X / right.X;
		result.Y = left.Y / right.Y;
		result.Z = left.Z / right.Z;
		result.W = left.W / right.W;
		return result;
	}

	void Quaternion::Divide( Quaternion% left, Quaternion% right, [Out] Quaternion% result )
	{
		result.X = left.X / right.X;
		result.Y = left.Y / right.Y;
		result.Z = left.Z / right.Z;
		result.W = left.W / right.W;
	}
	
	float Quaternion::Dot( Quaternion left, Quaternion right )
	{
		return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W); 
	}

	Quaternion Quaternion::Exponential( Quaternion quat )
	{
		Quaternion result;
		D3DXQuaternionExp( (D3DXQUATERNION*) &result, (D3DXQUATERNION*) &quat );
		return result;
	}

	void Quaternion::Exponential( Quaternion% quat, [Out] Quaternion% result )
	{
		pin_ptr<Quaternion> pinQuat = &quat;
		pin_ptr<Quaternion> pinResult = &result;

		D3DXQuaternionExp( (D3DXQUATERNION*) pinResult, (D3DXQUATERNION*) pinQuat );
	}

	Quaternion Quaternion::Invert( Quaternion quaternion )
	{
		Quaternion result;
		float lengthSq = 1.0f / ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W) );

		result.X = -quaternion.X * lengthSq;
		result.Y = -quaternion.Y * lengthSq;
		result.Z = -quaternion.Z * lengthSq;
		result.W = quaternion.W * lengthSq;

		return result;
	}

	void Quaternion::Invert( Quaternion% quaternion, [Out] Quaternion% result )
	{
		float lengthSq = 1.0f / ( (quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W) );

		result.X = -quaternion.X * lengthSq;
		result.Y = -quaternion.Y * lengthSq;
		result.Z = -quaternion.Z * lengthSq;
		result.W = quaternion.W * lengthSq;
	}

	Quaternion Quaternion::Lerp( Quaternion left, Quaternion right, float amount )
	{
		Quaternion result;
		float inverse = 1.0f - amount;
		float dot = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W); 

		if( dot >= 0.0f )
		{
			result.X = (inverse * left.X) + (amount * right.X);
			result.Y = (inverse * left.Y) + (amount * right.Y);
			result.Z = (inverse * left.Z) + (amount * right.Z);
			result.W = (inverse * left.W) + (amount * right.W);
		}
		else
		{
			result.X = (inverse * left.X) - (amount * right.X);
			result.Y = (inverse * left.Y) - (amount * right.Y);
			result.Z = (inverse * left.Z) - (amount * right.Z);
			result.W = (inverse * left.W) - (amount * right.W);
		}

		float invLength = 1.0f / result.Length();

		result.X *= invLength;
		result.Y *= invLength;
		result.Z *= invLength;
		result.W *= invLength;

		return result;
	}

	void Quaternion::Lerp( Quaternion% left, Quaternion% right, float amount, [Out] Quaternion% result )
	{
		float inverse = 1.0f - amount;
		float dot = (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z) + (left.W * right.W); 

		if( dot >= 0.0f )
		{
			result.X = (inverse * left.X) + (amount * right.X);
			result.Y = (inverse * left.Y) + (amount * right.Y);
			result.Z = (inverse * left.Z) + (amount * right.Z);
			result.W = (inverse * left.W) + (amount * right.W);
		}
		else
		{
			result.X = (inverse * left.X) - (amount * right.X);
			result.Y = (inverse * left.Y) - (amount * right.Y);
			result.Z = (inverse * left.Z) - (amount * right.Z);
			result.W = (inverse * left.W) - (amount * right.W);
		}

		float invLength = 1.0f / result.Length();

		result.X *= invLength;
		result.Y *= invLength;
		result.Z *= invLength;
		result.W *= invLength;
	}

	Quaternion Quaternion::Logarithm( Quaternion quat )
	{
		Quaternion result;
		D3DXQuaternionLn( (D3DXQUATERNION*) &result, (D3DXQUATERNION*) &quat );
		return result;
	}

	void Quaternion::Logarithm( Quaternion% quat, [Out] Quaternion% result )
	{
		pin_ptr<Quaternion> pinQuat = &quat;
		pin_ptr<Quaternion> pinResult = &result;

		D3DXQuaternionLn( (D3DXQUATERNION*) pinResult, (D3DXQUATERNION*) pinQuat );
	}

	Quaternion Quaternion::Multiply( Quaternion left, Quaternion right )
	{
		Quaternion quaternion;
		float lx = left.X;
        float ly = left.Y;
        float lz = left.Z;
        float lw = left.W;
        float rx = right.X;
        float ry = right.Y;
        float rz = right.Z;
        float rw = right.W;

        quaternion.X = (rx * lw + lx * rw + ry * lz) - (rz * ly);
        quaternion.Y = (ry * lw + ly * rw + rz * lx) - (rx * lz);
        quaternion.Z = (rz * lw + lz * rw + rx * ly) - (ry * lx);
        quaternion.W = (rw * lw) - (rx * lx + ry * ly + rz * lz);

		return quaternion;
	}

	void Quaternion::Multiply( Quaternion% left, Quaternion% right, [Out] Quaternion% result )
	{
		float lx = left.X;
        float ly = left.Y;
        float lz = left.Z;
        float lw = left.W;
        float rx = right.X;
        float ry = right.Y;
        float rz = right.Z;
        float rw = right.W;

        result.X = (rx * lw + lx * rw + ry * lz) - (rz * ly);
        result.Y = (ry * lw + ly * rw + rz * lx) - (rx * lz);
        result.Z = (rz * lw + lz * rw + rx * ly) - (ry * lx);
        result.W = (rw * lw) - (rx * lx + ry * ly + rz * lz);
	}

	Quaternion Quaternion::Multiply( Quaternion quaternion, float scale )
	{
		Quaternion result;
		result.X = quaternion.X * scale;
		result.Y = quaternion.Y * scale;
		result.Z = quaternion.Z * scale;
		result.W = quaternion.W * scale;
		return result;
	}

	void Quaternion::Multiply( Quaternion% quaternion, float scale, [Out] Quaternion% result )
	{
		result.X = quaternion.X * scale;
		result.Y = quaternion.Y * scale;
		result.Z = quaternion.Z * scale;
		result.W = quaternion.W * scale;
	}

	Quaternion Quaternion::Negate( Quaternion quat )
	{
		Quaternion result;
		result.X = -quat.X;
		result.Y = -quat.Y;
		result.Z = -quat.Z;
		result.W = -quat.W;
		return result;
	}

	void Quaternion::Negate( Quaternion% quat, [Out] Quaternion% result )
	{
		result.X = -quat.X;
		result.Y = -quat.Y;
		result.Z = -quat.Z;
		result.W = -quat.W;
	}

	Quaternion Quaternion::Normalize( Quaternion quat )
	{
		quat.Normalize();
		return quat;
	}

	void Quaternion::Normalize( Quaternion% quat, [Out] Quaternion% result )
	{
		float length = 1.0f / quat.Length();
		result.X = quat.X * length;
		result.Y = quat.Y * length;
		result.Z = quat.Z * length;
		result.W = quat.W * length;
	}

	Quaternion Quaternion::RotationAxis( Vector3 axis, float angle )
	{
		Quaternion result;

		Vector3::Normalize( axis, axis );

		float half = angle * 0.5f;
		float sin = static_cast<float>( Math::Sin( static_cast<double>( half ) ) );
		float cos = static_cast<float>( Math::Cos( static_cast<double>( half ) ) );

		result.X = axis.X * sin;
		result.Y = axis.Y * sin;
		result.Z = axis.Z * sin;
		result.W = cos;

		return result;
	}

	void Quaternion::RotationAxis( Vector3% axis, float angle, [Out] Quaternion% result )
	{
		Vector3::Normalize( axis, axis );

		float half = angle * 0.5f;
		float sin = static_cast<float>( Math::Sin( static_cast<double>( half ) ) );
		float cos = static_cast<float>( Math::Cos( static_cast<double>( half ) ) );

		result.X = axis.X * sin;
		result.Y = axis.Y * sin;
		result.Z = axis.Z * sin;
		result.W = cos;
	}
	
	Quaternion Quaternion::RotationMatrix( Matrix matrix )
	{
		Quaternion result;
		float scale = matrix.M11 + matrix.M22 + matrix.M33;

		if( scale > 0.0f )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(scale + 1.0f) ) );

			result.W = sqrt * 0.5f;
			sqrt = 0.5f / sqrt;

			result.X = (matrix.M23 - matrix.M32) * sqrt;
			result.Y = (matrix.M31 - matrix.M13) * sqrt;
			result.Z = (matrix.M12 - matrix.M21) * sqrt;

			return result;
		}

		if( (matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33) )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M11 - matrix.M22 - matrix.M33) ) );
			float half = 0.5f / sqrt;

			result.X = 0.5f * sqrt;
			result.Y = (matrix.M12 + matrix.M21) * half;
			result.Z = (matrix.M13 + matrix.M31) * half;
			result.W = (matrix.M23 - matrix.M32) * half;

			return result;
		}

		if( matrix.M22 > matrix.M33 )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M22 - matrix.M11 - matrix.M33) ) );
			float half = 0.5f / sqrt;

			result.X = (matrix.M21 + matrix.M12) * half;
			result.Y = 0.5f * sqrt;
			result.Z = (matrix.M32 + matrix.M23) * half;
			result.W = (matrix.M31 - matrix.M13) * half;

			return result;
		}

		float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M33 - matrix.M11 - matrix.M22) ) );
		float half = 0.5f / sqrt;

		result.X = (matrix.M31 + matrix.M13) * half;
		result.Y = (matrix.M32 + matrix.M23) * half;
		result.Z = 0.5f * sqrt;
		result.W = (matrix.M12 - matrix.M21) * half;

		return result;
	}

	void Quaternion::RotationMatrix( Matrix% matrix, [Out] Quaternion% result )
	{
		float scale = matrix.M11 + matrix.M22 + matrix.M33;

		if( scale > 0.0f )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(scale + 1.0f) ) );

			result.W = sqrt * 0.5f;
			sqrt = 0.5f / sqrt;

			result.X = (matrix.M23 - matrix.M32) * sqrt;
			result.Y = (matrix.M31 - matrix.M13) * sqrt;
			result.Z = (matrix.M12 - matrix.M21) * sqrt;
			return;
		}

		if( (matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33) )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M11 - matrix.M22 - matrix.M33) ) );
			float half = 0.5f / sqrt;

			result.X = 0.5f * sqrt;
			result.Y = (matrix.M12 + matrix.M21) * half;
			result.Z = (matrix.M13 + matrix.M31) * half;
			result.W = (matrix.M23 - matrix.M32) * half;
			return;
		}

		if( matrix.M22 > matrix.M33 )
		{
			float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M22 - matrix.M11 - matrix.M33) ) );
			float half = 0.5f / sqrt;

			result.X = (matrix.M21 + matrix.M12) * half;
			result.Y = 0.5f * sqrt;
			result.Z = (matrix.M32 + matrix.M23) * half;
			result.W = (matrix.M31 - matrix.M13) * half;
			return;
		}

		float sqrt = static_cast<float>( Math::Sqrt( static_cast<double>(1.0f + matrix.M33 - matrix.M11 - matrix.M22) ) );
		float half = 0.5f / sqrt;

		result.X = (matrix.M31 + matrix.M13) * half;
		result.Y = (matrix.M32 + matrix.M23) * half;
		result.Z = 0.5f * sqrt;
		result.W = (matrix.M12 - matrix.M21) * half;
	}

	Quaternion Quaternion::RotationYawPitchRoll( float yaw, float pitch, float roll )
	{
		Quaternion result;

		float halfRoll = roll * 0.5f;
		float sinRoll = static_cast<float>( Math::Sin( static_cast<double>( halfRoll ) ) );
		float cosRoll = static_cast<float>( Math::Cos( static_cast<double>( halfRoll ) ) );
		float halfPitch = pitch * 0.5f;
		float sinPitch = static_cast<float>( Math::Sin( static_cast<double>( halfPitch ) ) );
		float cosPitch = static_cast<float>( Math::Cos( static_cast<double>( halfPitch ) ) );
		float halfYaw = yaw * 0.5f;
		float sinYaw = static_cast<float>( Math::Sin( static_cast<double>( halfYaw ) ) );
		float cosYaw = static_cast<float>( Math::Cos( static_cast<double>( halfYaw ) ) );

		result.X = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
		result.Y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
		result.Z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
		result.W = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);

		return result;
	}

	void Quaternion::RotationYawPitchRoll( float yaw, float pitch, float roll, [Out] Quaternion% result )
	{
		float halfRoll = roll * 0.5f;
		float sinRoll = static_cast<float>( Math::Sin( static_cast<double>( halfRoll ) ) );
		float cosRoll = static_cast<float>( Math::Cos( static_cast<double>( halfRoll ) ) );
		float halfPitch = pitch * 0.5f;
		float sinPitch = static_cast<float>( Math::Sin( static_cast<double>( halfPitch ) ) );
		float cosPitch = static_cast<float>( Math::Cos( static_cast<double>( halfPitch ) ) );
		float halfYaw = yaw * 0.5f;
		float sinYaw = static_cast<float>( Math::Sin( static_cast<double>( halfYaw ) ) );
		float cosYaw = static_cast<float>( Math::Cos( static_cast<double>( halfYaw ) ) );

		result.X = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
		result.Y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
		result.Z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
		result.W = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);
	}

	Quaternion Quaternion::Slerp( Quaternion q1, Quaternion q2, float t )
	{
		Quaternion result;

		float opposite;
		float inverse;
		float dot = (q1.X * q2.X) + (q1.Y * q2.Y) + (q1.Z * q2.Z) + (q1.W * q2.W);
		bool flag = false;

		if( dot < 0.0f )
		{
			flag = true;
			dot = -dot;
		}

		if( dot > 0.999999f )
		{
			inverse = 1.0f - t;
			opposite = flag ? -t : t;
		}
		else
		{
			float acos = static_cast<float>( Math::Acos( static_cast<double>( dot ) ) );
			float invSin = static_cast<float>( ( 1.0f / Math::Sin( static_cast<double>( acos ) ) ) );

			inverse = ( static_cast<float>( Math::Sin( static_cast<double>( (1.0f - t) * acos ) ) ) ) * invSin;
			opposite = flag ? ( ( static_cast<float>( -Math::Sin( static_cast<double>( t * acos ) ) ) ) * invSin ) : ( ( static_cast<float>( Math::Sin( static_cast<double>( t * acos ) ) ) ) * invSin );
		}

		result.X = (inverse * q1.X) + (opposite * q2.X);
		result.Y = (inverse * q1.Y) + (opposite * q2.Y);
		result.Z = (inverse * q1.Z) + (opposite * q2.Z);
		result.W = (inverse * q1.W) + (opposite * q2.W);

		return result;
	}

	void Quaternion::Slerp( Quaternion% q1, Quaternion% q2, float t, [Out] Quaternion% result )
	{
		float opposite;
		float inverse;
		float dot = (q1.X * q2.X) + (q1.Y * q2.Y) + (q1.Z * q2.Z) + (q1.W * q2.W);
		bool flag = false;

		if( dot < 0.0f )
		{
			flag = true;
			dot = -dot;
		}

		if( dot > 0.999999f )
		{
			inverse = 1.0f - t;
			opposite = flag ? -t : t;
		}
		else
		{
			float acos = static_cast<float>( Math::Acos( static_cast<double>( dot ) ) );
			float invSin = static_cast<float>( ( 1.0f / Math::Sin( static_cast<double>( acos ) ) ) );

			inverse = ( static_cast<float>( Math::Sin( static_cast<double>( (1.0f - t) * acos ) ) ) ) * invSin;
			opposite = flag ? ( ( static_cast<float>( -Math::Sin( static_cast<double>( t * acos ) ) ) ) * invSin ) : ( ( static_cast<float>( Math::Sin( static_cast<double>( t * acos ) ) ) ) * invSin );
		}

		result.X = (inverse * q1.X) + (opposite * q2.X);
		result.Y = (inverse * q1.Y) + (opposite * q2.Y);
		result.Z = (inverse * q1.Z) + (opposite * q2.Z);
		result.W = (inverse * q1.W) + (opposite * q2.W);
	}

	Quaternion Quaternion::Squad( Quaternion q1, Quaternion a, Quaternion b, Quaternion c, float t )
	{
		Quaternion result;

		D3DXQuaternionSquad( (D3DXQUATERNION*) &result, (D3DXQUATERNION*) &q1, (D3DXQUATERNION*) &a,
			(D3DXQUATERNION*) &b, (D3DXQUATERNION*) &c, t );

		return result;
	}

	void Quaternion::Squad( Quaternion% q1, Quaternion% a, Quaternion% b, Quaternion% c, float t, [Out] Quaternion% result )
	{
		pin_ptr<Quaternion> pin1 = &q1;
		pin_ptr<Quaternion> pinA = &a;
		pin_ptr<Quaternion> pinB = &b;
		pin_ptr<Quaternion> pinC = &c;
		pin_ptr<Quaternion> pinResult = &result;

		D3DXQuaternionSquad( (D3DXQUATERNION*) pinResult, (D3DXQUATERNION*) pin1, (D3DXQUATERNION*) pinA,
			(D3DXQUATERNION*) pinB, (D3DXQUATERNION*) pinC, t );
	}

	array<Quaternion>^ Quaternion::SquadSetup( Quaternion source1, Quaternion source2, Quaternion source3, Quaternion source4 )
	{
		Quaternion result1;
		Quaternion result2;
		Quaternion result3;
		array<Quaternion>^ results = gcnew array<Quaternion>( 3 );

		D3DXQuaternionSquadSetup( (D3DXQUATERNION*) &result1, (D3DXQUATERNION*) &result2, (D3DXQUATERNION*) &result3,
			(D3DXQUATERNION*) &source1, (D3DXQUATERNION*) &source2, (D3DXQUATERNION*) &source3, (D3DXQUATERNION*) &source4 );

		results[0] = result1;
		results[1] = result2;
		results[2] = result3;
		return results;
	}

	Quaternion Quaternion::Subtract( Quaternion left, Quaternion right )
	{
		Quaternion result;
		result.X = left.X - right.X;
		result.Y = left.Y - right.Y;
		result.Z = left.Z - right.Z;
		result.W = left.W - right.W;
		return result;
	}

	void Quaternion::Subtract( Quaternion% left, Quaternion% right, [Out] Quaternion% result )
	{
		result.X = left.X - right.X;
		result.Y = left.Y - right.Y;
		result.Z = left.Z - right.Z;
		result.W = left.W - right.W;
	}

	Quaternion Quaternion::operator * (Quaternion left, Quaternion right)
	{
		Quaternion quaternion;
		float lx = left.X;
        float ly = left.Y;
        float lz = left.Z;
        float lw = left.W;
        float rx = right.X;
        float ry = right.Y;
        float rz = right.Z;
        float rw = right.W;

        quaternion.X = (rx * lw + lx * rw + ry * lz) - (rz * ly);
        quaternion.Y = (ry * lw + ly * rw + rz * lx) - (rx * lz);
        quaternion.Z = (rz * lw + lz * rw + rx * ly) - (ry * lx);
        quaternion.W = (rw * lw) - (rx * lx + ry * ly + rz * lz);

		return quaternion;
	}

	Quaternion Quaternion::operator * (Quaternion quaternion, float scale)
	{
		Quaternion result;
		result.X = quaternion.X * scale;
		result.Y = quaternion.Y * scale;
		result.Z = quaternion.Z * scale;
		result.W = quaternion.W * scale;
		return result;
	}

	Quaternion Quaternion::operator * (float scale, Quaternion quaternion)
	{
		Quaternion result;
		result.X = quaternion.X * scale;
		result.Y = quaternion.Y * scale;
		result.Z = quaternion.Z * scale;
		result.W = quaternion.W * scale;
		return result;
	}

	Quaternion Quaternion::operator / (Quaternion lhs, float rhs)
	{
		Quaternion result;
		result.X = lhs.X / rhs;
		result.Y = lhs.Y / rhs;
		result.Z = lhs.Z / rhs;
		result.W = lhs.W / rhs;
		return result;
	}

	Quaternion Quaternion::operator + (Quaternion lhs, Quaternion rhs)
	{
		Quaternion result;
		result.X = lhs.X + rhs.X;
		result.Y = lhs.Y + rhs.Y;
		result.Z = lhs.Z + rhs.Z;
		result.W = lhs.W + rhs.W;
		return result;
	}

	Quaternion Quaternion::operator - (Quaternion lhs, Quaternion rhs)
	{
		Quaternion result;
		result.X = lhs.X - rhs.X;
		result.Y = lhs.Y - rhs.Y;
		result.Z = lhs.Z - rhs.Z;
		result.W = lhs.W - rhs.W;
		return result;
	}

	Quaternion Quaternion::operator - (Quaternion quaternion)
	{
		Quaternion result;
		result.X = -quaternion.X;
		result.Y = -quaternion.Y;
		result.Z = -quaternion.Z;
		result.W = -quaternion.W;
		return result;
	}

	bool Quaternion::operator == ( Quaternion left, Quaternion right )
	{
		return Quaternion::Equals( left, right );
	}

	bool Quaternion::operator != ( Quaternion left, Quaternion right )
	{
		return !Quaternion::Equals( left, right );
	}

	String^ Quaternion::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", X.ToString(CultureInfo::CurrentCulture), 
			Y.ToString(CultureInfo::CurrentCulture), Z.ToString(CultureInfo::CurrentCulture),
			W.ToString(CultureInfo::CurrentCulture) );
	}

	int Quaternion::GetHashCode()
	{
		return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
	}

	bool Quaternion::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Quaternion>( value ) );
	}

	bool Quaternion::Equals( Quaternion value )
	{
		return ( X == value.X && Y == value.Y && Z == value.Z && W == value.W );
	}

	bool Quaternion::Equals( Quaternion% value1, Quaternion% value2 )
	{
		return ( value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W );
	}
}
