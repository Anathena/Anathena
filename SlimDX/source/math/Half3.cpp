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

#include "Half.h"
#include "Half3.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Half3::Half3( Half value )
	{
		X = value;
		Y = value;
		Z = value;
	}

	Half3::Half3( Half x, Half y, Half z )
	{
		X = x;
		Y = y;
		Z = z;
	}

	bool Half3::operator == ( Half3 left, Half3 right )
	{
		return Half3::Equals( left, right );
	}

	bool Half3::operator != ( Half3 left, Half3 right )
	{
		return !Half3::Equals( left, right );
	}

	int Half3::GetHashCode()
	{
		return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
	}

	bool Half3::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Half3>( value ) );
	}

	bool Half3::Equals( Half3 value )
	{
		return ( X == value.X && Y == value.Y && Z == value.Z );
	}

	bool Half3::Equals( Half3% value1, Half3% value2 )
	{
		return ( value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z );
	}
}