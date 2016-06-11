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

#include "ClipStatus.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool ClipStatus::operator == ( ClipStatus left, ClipStatus right )
	{
		return ClipStatus::Equals( left, right );
	}

	bool ClipStatus::operator != ( ClipStatus left, ClipStatus right )
	{
		return !ClipStatus::Equals( left, right );
	}

	int ClipStatus::GetHashCode()
	{
		return ClipUnion.GetHashCode() + ClipIntersection.GetHashCode();
	}

	bool ClipStatus::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ClipStatus>( value ) );
	}

	bool ClipStatus::Equals( ClipStatus value )
	{
		return ( ClipUnion == value.ClipUnion && ClipIntersection == value.ClipIntersection );
	}

	bool ClipStatus::Equals( ClipStatus% value1, ClipStatus% value2 )
	{
		return ( value1.ClipUnion == value2.ClipUnion && value1.ClipIntersection == value2.ClipIntersection );
	}
}
}