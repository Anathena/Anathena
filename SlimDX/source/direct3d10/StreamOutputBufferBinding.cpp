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

#include "Buffer.h"
#include "StreamOutputBufferBinding.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	StreamOutputBufferBinding::StreamOutputBufferBinding( Direct3D10::Buffer^ buffer, int offset )
	: m_Buffer( buffer ), m_Offset( offset )
	{
	}
	
	Direct3D10::Buffer^ StreamOutputBufferBinding::Buffer::get()
	{
		return m_Buffer;
	}
	
	void StreamOutputBufferBinding::Buffer::set( Direct3D10::Buffer^ value )
	{
		m_Buffer = value;
	}
	
	int StreamOutputBufferBinding::Offset::get()
	{
		return m_Offset;
	}
	
	void StreamOutputBufferBinding::Offset::set( int value )
	{
		m_Offset = value;
	}

	bool StreamOutputBufferBinding::operator == ( StreamOutputBufferBinding left, StreamOutputBufferBinding right )
	{
		return StreamOutputBufferBinding::Equals( left, right );
	}

	bool StreamOutputBufferBinding::operator != ( StreamOutputBufferBinding left, StreamOutputBufferBinding right )
	{
		return !StreamOutputBufferBinding::Equals( left, right );
	}

	int StreamOutputBufferBinding::GetHashCode()
	{
		return m_Buffer->GetHashCode() + m_Offset.GetHashCode();
	}

	bool StreamOutputBufferBinding::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<StreamOutputBufferBinding>( value ) );
	}

	bool StreamOutputBufferBinding::Equals( StreamOutputBufferBinding value )
	{
		return ( m_Buffer == value.m_Buffer && m_Offset == value.m_Offset );
	}

	bool StreamOutputBufferBinding::Equals( StreamOutputBufferBinding% value1, StreamOutputBufferBinding% value2 )
	{
		return ( value1.m_Buffer == value2.m_Buffer && value1.m_Offset == value2.m_Offset );
	}
}
}
