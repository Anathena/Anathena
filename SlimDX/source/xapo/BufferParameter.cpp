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

#include "BufferParameter.h"

namespace SlimDX
{
namespace XAPO
{
	bool BufferParameter::operator == ( BufferParameter left, BufferParameter right )
	{
		return BufferParameter::Equals( left, right );
	}

	bool BufferParameter::operator != ( BufferParameter left, BufferParameter right )
	{
		return !BufferParameter::Equals( left, right );
	}

	int BufferParameter::GetHashCode()
	{
		return Buffer.GetHashCode() + ValidFrameCount.GetHashCode() + Flags.GetHashCode();
	}

	bool BufferParameter::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<BufferParameter>( value ) );
	}

	bool BufferParameter::Equals( BufferParameter value )
	{
		return ( Buffer == value.Buffer && ValidFrameCount == value.ValidFrameCount && Flags == value.Flags );
	}

	bool BufferParameter::Equals( BufferParameter% value1, BufferParameter% value2 )
	{
		return ( value1.Buffer == value2.Buffer && value1.ValidFrameCount == value2.ValidFrameCount && value1.Flags == value2.Flags );
	}
}
}