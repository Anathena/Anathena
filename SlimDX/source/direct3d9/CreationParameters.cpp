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

#include "CreationParameters.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool CreationParameters::operator == ( CreationParameters left, CreationParameters right )
	{
		return CreationParameters::Equals( left, right );
	}

	bool CreationParameters::operator != ( CreationParameters left, CreationParameters right )
	{
		return !CreationParameters::Equals( left, right );
	}

	int CreationParameters::GetHashCode()
	{
		return AdapterOrdinal.GetHashCode() + DeviceType.GetHashCode() + Window.GetHashCode()
			 + BehaviorFlags.GetHashCode();
	}

	bool CreationParameters::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CreationParameters>( value ) );
	}

	bool CreationParameters::Equals( CreationParameters value )
	{
		return ( AdapterOrdinal == value.AdapterOrdinal && DeviceType == value.DeviceType && Window == value.Window
			 && BehaviorFlags == value.BehaviorFlags );
	}

	bool CreationParameters::Equals( CreationParameters% value1, CreationParameters% value2 )
	{
		return ( value1.AdapterOrdinal == value2.AdapterOrdinal && value1.DeviceType == value2.DeviceType && value1.Window == value2.Window
			 && value1.BehaviorFlags == value2.BehaviorFlags );
	}
}
}