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

#include "IntersectInformation.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool IntersectInformation::operator == ( IntersectInformation left, IntersectInformation right )
	{
		return IntersectInformation::Equals( left, right );
	}

	bool IntersectInformation::operator != ( IntersectInformation left, IntersectInformation right )
	{
		return !IntersectInformation::Equals( left, right );
	}

	int IntersectInformation::GetHashCode()
	{
		return FaceIndex.GetHashCode() + U.GetHashCode() + V.GetHashCode()
			 + Distance.GetHashCode();
	}

	bool IntersectInformation::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<IntersectInformation>( value ) );
	}

	bool IntersectInformation::Equals( IntersectInformation value )
	{
		return ( FaceIndex == value.FaceIndex && U == value.U && V == value.V
			 && Distance == value.Distance );
	}

	bool IntersectInformation::Equals( IntersectInformation% value1, IntersectInformation% value2 )
	{
		return ( value1.FaceIndex == value2.FaceIndex && value1.U == value2.U && value1.V == value2.V
			 && value1.Distance == value2.Distance );
	}
}
}