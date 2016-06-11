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

#include "CounterDescription.h"

namespace SlimDX
{
namespace Direct3D10
{ 	
	CounterDescription::CounterDescription( Direct3D10::CounterKind kind )
	: m_Counter( kind )
	{
	}
	
	CounterDescription::CounterDescription( const D3D10_COUNTER_DESC& native )
	{
		m_Counter = static_cast<Direct3D10::CounterKind>( native.Counter );
	}
	
	D3D10_COUNTER_DESC CounterDescription::CreateNativeVersion()
	{
		D3D10_COUNTER_DESC native;
		native.Counter = static_cast<D3D10_COUNTER>( m_Counter );
		native.MiscFlags = 0;
		
		return native;
	}

	Direct3D10::CounterKind CounterDescription::CounterKind::get()
	{
		return m_Counter;
	}

	void CounterDescription::CounterKind::set( Direct3D10::CounterKind value )
	{
		m_Counter = value;
	}

	bool CounterDescription::operator == ( CounterDescription left, CounterDescription right )
	{
		return CounterDescription::Equals( left, right );
	}

	bool CounterDescription::operator != ( CounterDescription left, CounterDescription right )
	{
		return !CounterDescription::Equals( left, right );
	}

	int CounterDescription::GetHashCode()
	{
		return m_Counter.GetHashCode();
	}

	bool CounterDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CounterDescription>( value ) );
	}

	bool CounterDescription::Equals( CounterDescription value )
	{
		return ( m_Counter == value.m_Counter );
	}

	bool CounterDescription::Equals( CounterDescription% value1, CounterDescription% value2 )
	{
		return ( value1.m_Counter == value2.m_Counter );
	}
}
}
