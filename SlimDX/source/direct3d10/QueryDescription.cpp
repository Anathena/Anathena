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

#include "QueryDescription.h"

namespace SlimDX
{
namespace Direct3D10
{ 	
	QueryDescription::QueryDescription( QueryType type, QueryFlags flags )
	: m_Query( type ), m_MiscFlags( flags )
	{
	}
	
	QueryDescription::QueryDescription( const D3D10_QUERY_DESC& native )
	{
		m_Query = static_cast<QueryType>( native.Query );
		m_MiscFlags = static_cast<QueryFlags>( native.MiscFlags );
	}
	
	D3D10_QUERY_DESC QueryDescription::CreateNativeVersion()
	{
		D3D10_QUERY_DESC native;
		native.Query = static_cast<D3D10_QUERY>( m_Query );
		native.MiscFlags = static_cast<UINT>( m_MiscFlags );
	
		return native;
	}

	QueryType QueryDescription::Type::get()
	{
		return m_Query;
	}

	void QueryDescription::Type::set( QueryType value )
	{
		m_Query = value;
	}
	
	QueryFlags QueryDescription::Flags::get()
	{
		return m_MiscFlags;
	}

	void QueryDescription::Flags::set( QueryFlags value )
	{
		m_MiscFlags = value;
	}
	
	bool QueryDescription::operator == ( QueryDescription left, QueryDescription right )
	{
		return QueryDescription::Equals( left, right );
	}

	bool QueryDescription::operator != ( QueryDescription left, QueryDescription right )
	{
		return !QueryDescription::Equals( left, right );
	}

	int QueryDescription::GetHashCode()
	{
		return m_Query.GetHashCode() + m_MiscFlags.GetHashCode();
	}

	bool QueryDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<QueryDescription>( value ) );
	}

	bool QueryDescription::Equals( QueryDescription value )
	{
		return ( m_Query == value.m_Query && m_MiscFlags == value.m_MiscFlags );
	}

	bool QueryDescription::Equals( QueryDescription% value1, QueryDescription% value2 )
	{
		return ( value1.m_Query == value2.m_Query && value1.m_MiscFlags == value2.m_MiscFlags );
	}
}
}
