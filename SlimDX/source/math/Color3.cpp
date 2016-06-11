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

#include "Color3.h"

using namespace System;

namespace SlimDX
{
	Color3::Color3( float red, float green, float blue )
	: Red( red ), Green( green ), Blue( blue )
	{
	}

	bool Color3::operator == ( Color3 left, Color3 right )
	{
		return Color3::Equals( left, right );
	}

	bool Color3::operator != ( Color3 left, Color3 right )
	{
		return !Color3::Equals( left, right );
	}

	int Color3::GetHashCode()
	{
		return Red.GetHashCode() + Green.GetHashCode() + Blue.GetHashCode();
	}

	bool Color3::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Color3>( value ) );
	}

	bool Color3::Equals( Color3 value )
	{
		return ( Red == value.Red && Green == value.Green && Blue == value.Blue );
	}

	bool Color3::Equals( Color3% value1, Color3% value2 )
	{
		return ( value1.Red == value2.Red && value1.Green == value2.Green && value1.Blue == value2.Blue );
	}
}