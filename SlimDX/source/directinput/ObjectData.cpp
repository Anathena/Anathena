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
#include "stdafx.h"
#include <dinput.h>

#include "ObjectData.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	bool ObjectData::operator == ( ObjectData left, ObjectData right )
	{
		return ObjectData::Equals( left, right );
	}

	bool ObjectData::operator != ( ObjectData left, ObjectData right )
	{
		return !ObjectData::Equals( left, right );
	}

	int ObjectData::GetHashCode()
	{
		return Instance.GetHashCode() + Data.GetHashCode();
	}

	bool ObjectData::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ObjectData>( value ) );
	}

	bool ObjectData::Equals( ObjectData value )
	{
		return ( Instance == value.Instance && Data == value.Data );
	}

	bool ObjectData::Equals( ObjectData% value1, ObjectData% value2 )
	{
		return ( value1.Instance == value2.Instance && value1.Data == value2.Data );
	}
}
}