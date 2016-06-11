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

using namespace testing;
using namespace System;
using namespace SlimDX;

TEST( Vector2Tests, ConstructDefault )
{
	Vector2 vector = Vector2();

	ASSERT_EQ( 0.0f, vector.X );
	ASSERT_EQ( 0.0f, vector.Y );
}

TEST( Vector2Tests, ConstructWithX )
{
	Vector2 vector = Vector2( 0.75f );

	ASSERT_EQ( 0.75f, vector.X );
	ASSERT_EQ( 0.75f, vector.Y );
}

TEST( Vector2Tests, ConstructWithXY )
{
	Vector2 vector = Vector2( 0.75f, 1.25f );

	ASSERT_EQ( 0.75f, vector.X );
	ASSERT_EQ( 1.25f, vector.Y );
}

TEST( Vector2Tests, Length )
{
	Vector2 vector = Vector2( 1.25f, 2.75f );

	ASSERT_EQ( 3.02076149f, vector.Length() );
}

TEST( Vector2Tests, LengthSquared )
{
	Vector2 vector = Vector2( 1.25f, 2.75f );

	ASSERT_EQ( 9.125f, vector.LengthSquared() );
}

TEST( Vector2Tests, Normalize )
{
	Vector2 vector = Vector2( 3.5f, 1.75f );
	vector.Normalize();

	ASSERT_EQ( 0.89442719f, vector.X );
	ASSERT_EQ( 0.44721359f, vector.Y );
}

TEST( Vector2Tests, NormalizeZeroVector )
{
	Vector2 vector = Vector2( 0.0f );
	vector.Normalize();

	ASSERT_EQ( 0.0f, vector.X );
	ASSERT_EQ( 0.0f, vector.Y );
}

TEST( Vector2Tests, StaticNormalize )
{
	Vector2 vector = Vector2( 3.5f, 1.75f );
	Vector2 result = Vector2::Normalize(vector);

	ASSERT_EQ( 0.89442719f, result.X );
	ASSERT_EQ( 0.44721359f, result.Y );
}

TEST( Vector2Tests, StaticNormalizeByRef )
{
	Vector2 vector = Vector2( 3.5f, 1.75f );
	Vector2 result;
	
	Vector2::Normalize( vector, result );

	ASSERT_EQ( 0.89442719f, result.X );
	ASSERT_EQ( 0.44721359f, result.Y );
}

TEST( Vector2Tests, Add )
{
	Vector2 vector0 = Vector2( 1.0f, 2.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result = Vector2::Add( vector0, vector1 );

	ASSERT_EQ( 4.0f, result.X );
	ASSERT_EQ( 6.0f, result.Y );
}

TEST( Vector2Tests, AddByRef )
{
	Vector2 vector0 = Vector2( 1.0f, 2.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result;
	Vector2::Add( vector0, vector1, result );

	ASSERT_EQ( 4.0f, result.X );
	ASSERT_EQ( 6.0f, result.Y );
}

TEST( Vector2Tests, AddOperator )
{
	Vector2 vector0 = Vector2( 1.0f, 2.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result = vector0 + vector1;

	ASSERT_EQ( 4.0f, result.X );
	ASSERT_EQ( 6.0f, result.Y );
}

TEST( Vector2Tests, Subtract )
{
	Vector2 vector0 = Vector2( 1.0f, 8.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result = Vector2::Subtract( vector0, vector1 );

	ASSERT_EQ( -2.0f, result.X );
	ASSERT_EQ( 4.0f, result.Y );
}

TEST( Vector2Tests, SubtractByRef )
{
	Vector2 vector0 = Vector2( 1.0f, 8.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result;
	Vector2::Subtract( vector0, vector1, result );

	ASSERT_EQ( -2.0f, result.X );
	ASSERT_EQ( 4.0f, result.Y );
}

TEST( Vector2Tests, SubtractOperator )
{
	Vector2 vector0 = Vector2( 1.0f, 8.0f );
	Vector2 vector1 = Vector2( 3.0f, 4.0f );
	Vector2 result = vector0 - vector1;

	ASSERT_EQ( -2.0f, result.X );
	ASSERT_EQ( 4.0f, result.Y );
}

TEST( Vector2Tests, Multiply )
{
	Vector2 vector = Vector2( 1.0f, 8.0f );
	Vector2 result = Vector2::Multiply( vector, 2.0f );

	ASSERT_EQ( 2.0f, result.X );
	ASSERT_EQ( 16.0f, result.Y );
}

TEST( Vector2Tests, MultiplyByRef )
{
	Vector2 vector = Vector2( 1.0f, 8.0f );
	Vector2 result;
	Vector2::Multiply( vector, 2.0f, result);

	ASSERT_EQ( 2.0f, result.X );
	ASSERT_EQ( 16.0f, result.Y );
}

TEST( Vector2Tests, MultiplyOperatorByScalarOnLeft )
{
	Vector2 vector = Vector2( 1.0f, 8.0f );
	Vector2 result = 2.0f * vector;

	ASSERT_EQ( 2.0f, result.X );
	ASSERT_EQ( 16.0f, result.Y );
}

TEST( Vector2Tests, MultiplyOperatorByScalarOnRight )
{
	Vector2 vector = Vector2( 1.0f, 8.0f );
	Vector2 result = vector * 2.0f;

	ASSERT_EQ( 2.0f, result.X );
	ASSERT_EQ( 16.0f, result.Y );
}

TEST( Vector2Tests, Modulate )
{
	Vector2 vector0 = Vector2( 2.0f, 3.0f );
	Vector2 vector1 = Vector2( 4.0f, 5.0f );
	Vector2 result = Vector2::Modulate( vector0, vector1 );

	ASSERT_EQ( 8.0f, result.X );
	ASSERT_EQ( 15.0f, result.Y );
}

TEST( Vector2Tests, ModulateByRef )
{
	Vector2 vector0 = Vector2( 2.0f, 3.0f );
	Vector2 vector1 = Vector2( 4.0f, 5.0f );
	Vector2 result;
	Vector2::Modulate( vector0, vector1, result );

	ASSERT_EQ( 8.0f, result.X );
	ASSERT_EQ( 15.0f, result.Y );
}