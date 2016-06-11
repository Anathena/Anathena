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

#include "RawBufferedData.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	RawBufferedData RawBufferedData::FromUnmanaged(const DIDEVICEOBJECTDATA &data)
	{
		RawBufferedData result;
		result.Offset = data.dwOfs;
		result.Data = data.dwData;
		result.Timestamp = data.dwTimeStamp;
		result.Sequence = data.dwSequence;
		result.AppData = System::IntPtr::IntPtr(const_cast<void*>(reinterpret_cast<const void*>(data.uAppData)));

		return result;
	}

	bool RawBufferedData::operator == ( RawBufferedData left, RawBufferedData right )
	{
		return RawBufferedData::Equals( left, right );
	}

	bool RawBufferedData::operator != ( RawBufferedData left, RawBufferedData right )
	{
		return !RawBufferedData::Equals( left, right );
	}

	int RawBufferedData::GetHashCode()
	{
		return Offset.GetHashCode() + Data.GetHashCode() + Timestamp.GetHashCode() + Sequence.GetHashCode() + AppData.GetHashCode();
	}

	bool RawBufferedData::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RawBufferedData>( value ) );
	}

	bool RawBufferedData::Equals( RawBufferedData value )
	{
		return ( Offset == value.Offset && Data == value.Data && Timestamp == value.Timestamp
			 && Sequence == value.Sequence && AppData == value.AppData );
	}

	bool RawBufferedData::Equals( RawBufferedData% value1, RawBufferedData% value2 )
	{
		return ( value1.Offset == value2.Offset && value1.Data == value2.Data && value1.Timestamp == value2.Timestamp
			 && value1.Sequence == value2.Sequence && value1.AppData == value2.AppData );
	}
}
}