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

#include <d3d10.h>

#include "BufferDescription.h"

namespace SlimDX
{
namespace Direct3D10
{ 	
	BufferDescription::BufferDescription( int sizeInBytes, ResourceUsage usage, Direct3D10::BindFlags bindFlags, Direct3D10::CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags )
	: m_ByteWidth( sizeInBytes), m_Usage( usage ), m_BindFlags( bindFlags ), m_CPUAccessFlags( accessFlags ), m_MiscFlags( optionFlags )
	{
	}
	
	BufferDescription::BufferDescription( const D3D10_BUFFER_DESC& native )
	{
		m_ByteWidth = native.ByteWidth;
		m_Usage = static_cast<ResourceUsage>( native.Usage );
		m_BindFlags = static_cast<Direct3D10::BindFlags>( native.BindFlags );
		m_CPUAccessFlags = static_cast<Direct3D10::CpuAccessFlags>( native.CPUAccessFlags );
		m_MiscFlags = static_cast<ResourceOptionFlags>( native.MiscFlags );
	}

	int BufferDescription::SizeInBytes::get()
	{
		return m_ByteWidth;
	}

	void BufferDescription::SizeInBytes::set( int value )
	{
		m_ByteWidth = value;
	}
	
	ResourceUsage BufferDescription::Usage::get()
	{
		return m_Usage;
	}

	void BufferDescription::Usage::set( ResourceUsage value )
	{
		m_Usage = value;
	}
	
	Direct3D10::BindFlags BufferDescription::BindFlags::get()
	{
		return m_BindFlags;
	}

	void BufferDescription::BindFlags::set( Direct3D10::BindFlags value )
	{
		m_BindFlags = value;
	}
	
	Direct3D10::CpuAccessFlags BufferDescription::CpuAccessFlags::get()
	{
		return m_CPUAccessFlags;
	}

	void BufferDescription::CpuAccessFlags::set( Direct3D10::CpuAccessFlags value )
	{
		m_CPUAccessFlags = value;
	}
	
	ResourceOptionFlags BufferDescription::OptionFlags::get()
	{
		return m_MiscFlags;
	}

	void BufferDescription::OptionFlags::set( ResourceOptionFlags value )
	{
		m_MiscFlags = value;
	}

	bool BufferDescription::operator == ( BufferDescription left, BufferDescription right )
	{
		return BufferDescription::Equals( left, right );
	}

	bool BufferDescription::operator != ( BufferDescription left, BufferDescription right )
	{
		return !BufferDescription::Equals( left, right );
	}

	int BufferDescription::GetHashCode()
	{
		return m_ByteWidth.GetHashCode() + m_Usage.GetHashCode() + m_BindFlags.GetHashCode()
			 + m_CPUAccessFlags.GetHashCode() + m_MiscFlags.GetHashCode();
	}

	bool BufferDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<BufferDescription>( value ) );
	}

	bool BufferDescription::Equals( BufferDescription value )
	{
		return ( m_ByteWidth == value.m_ByteWidth && m_Usage == value.m_Usage && m_BindFlags == value.m_BindFlags
			 && m_CPUAccessFlags == value.m_CPUAccessFlags && m_MiscFlags == value.m_MiscFlags );
	}

	bool BufferDescription::Equals( BufferDescription% value1, BufferDescription% value2 )
	{
		return ( value1.m_ByteWidth == value2.m_ByteWidth && value1.m_Usage == value2.m_Usage && value1.m_BindFlags == value2.m_BindFlags
			 && value1.m_CPUAccessFlags == value2.m_CPUAccessFlags && value1.m_MiscFlags == value2.m_MiscFlags );
	}
}
}
