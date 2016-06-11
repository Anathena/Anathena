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

TEST( BoundingSphereTests, ConstructCenterRadius )
{
    BoundingSphere sphere( Vector3( 1.0f, 2.0f, 3.0f ), 5.0f );

	ASSERT_EQ( 1.0f, sphere.Center.X );
	ASSERT_EQ( 2.0f, sphere.Center.Y );
	ASSERT_EQ( 3.0f, sphere.Center.Z );
	ASSERT_EQ( 5.0f, sphere.Radius );
}

TEST( BoundingSphereTests, DisjointTest )
{
	BoundingSphere sphere1( Vector3::Zero, 300 );
	BoundingSphere sphere2( Vector3( 301, 0, 0 ), 0.5f );

	ASSERT_EQ( (int)ContainmentType::Disjoint, (int)BoundingSphere::Contains( sphere1, sphere2 ) );
}