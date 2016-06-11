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
#include <d3d9.h>
#include <d3dx9.h>

#include "Box.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool Box::operator == ( Box left, Box right )
	{
		return Box::Equals( left, right );
	}

	bool Box::operator != ( Box left, Box right )
	{
		return !Box::Equals( left, right );
	}

	int Box::GetHashCode()
	{
		return Left.GetHashCode() + Top.GetHashCode() + Right.GetHashCode()
			 + Bottom.GetHashCode() + Front.GetHashCode() + Back.GetHashCode();
	}

	bool Box::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Box>( value ) );
	}

	bool Box::Equals( Box value )
	{
		return ( Left == value.Left && Top == value.Top && Right == value.Right
			 && Bottom == value.Bottom && Front == value.Front && Back == value.Back );
	}

	bool Box::Equals( Box% value1, Box% value2 )
	{
		return ( value1.Left == value2.Left && value1.Top == value2.Top && value1.Right == value2.Right
			 && value1.Bottom == value2.Bottom && value1.Front == value2.Front && value1.Back == value2.Back );
	}
}
}