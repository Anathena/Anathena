/*
* Copyright (c) 2007-2010 SlimDX Group
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
	[System::Runtime::InteropServices::StructLayout( System::Runtime::InteropServices::LayoutKind::Sequential, Pack = 4 )]
	public value class Matrix3x2 : System::IEquatable<Matrix3x2>
	{
	public:
		float M11;
		float M12;
		float M21;
		float M22;
		float M31;
		float M32;

		static property Matrix3x2 Identity
		{
			Matrix3x2 get();
		}

		property bool IsIdentity { bool get(); }
		property bool IsInvertible { bool get(); }

		static Matrix3x2 Invert( Matrix3x2 mat );
		static void Invert( Matrix3x2% mat, [Out] Matrix3x2% result );

		static Matrix3x2 Multiply ( Matrix3x2 left, Matrix3x2 right );
		static void Multiply ( Matrix3x2% left, Matrix3x2% right, [Out] Matrix3x2% result );

		static Matrix3x2 Rotation( float angle, System::Drawing::PointF centerPoint );
		static Matrix3x2 Rotation( float angle );

		static void Rotation( float angle, [Out] Matrix3x2% result );
		static void Rotation( float angle, System::Drawing::PointF% centerPoint, [Out] Matrix3x2% result );

		static Matrix3x2 Scale( System::Drawing::SizeF size);
		static void Scale( System::Drawing::SizeF% size, [Out] Matrix3x2% result);

		static Matrix3x2 Scale( float x, float y );
		static void Scale( float x, float y, [Out] Matrix3x2% result);

		static Matrix3x2 Scale( System::Drawing::SizeF size, System::Drawing::PointF centerPoint );
		static void Scale( System::Drawing::SizeF% size, System::Drawing::PointF% centerPoint, [Out] Matrix3x2% result );

		static Matrix3x2 Scale( float x, float y, System::Drawing::PointF centerPoint );
		static void Scale( float x, float y, System::Drawing::PointF% centerPoint, [Out] Matrix3x2% result );

		static Matrix3x2 Skew( float angleX, float angleY, System::Drawing::PointF centerPoint );
		static void Skew( float angleX, float angleY, System::Drawing::PointF% centerPoint, [Out] Matrix3x2% result );

		static Matrix3x2 Translation( System::Drawing::PointF point );
		static void Translation( System::Drawing::PointF% point, [Out] Matrix3x2% result );

		static Matrix3x2 Translation( float x, float y );
		static void Translation( float x, float y, [Out] Matrix3x2% result );

		static System::Drawing::PointF TransformPoint( Matrix3x2 mat, System::Drawing::PointF point);
		static void TransformPoint( Matrix3x2% mat, System::Drawing::PointF% point, [Out] System::Drawing::PointF% result);

		float Determinant();
		bool Invert();

		static Matrix3x2 operator * ( Matrix3x2 left, Matrix3x2 right );
		static bool operator == ( Matrix3x2 left, Matrix3x2 right );
		static bool operator != ( Matrix3x2 left, Matrix3x2 right );

		virtual System::String^ ToString() override;

		virtual int GetHashCode() override;

		virtual bool Equals( System::Object^ obj ) override;
		virtual bool Equals( Matrix3x2 other );
		static bool Equals( Matrix3x2% value1, Matrix3x2% value2 );
	};
}