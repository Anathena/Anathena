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

#include <d3dx9.h>

#include "Half.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Half::Half( float value )
	{
		Half temp;
		D3DXFloat32To16Array( reinterpret_cast<D3DXFLOAT16*>( &temp ), &value, 1 );
		m_Value = temp.m_Value;
	}

	UInt16 Half::RawValue::get()
	{
		return m_Value;
	}

	void Half::RawValue::set( UInt16 value )
	{
		m_Value = value;
	}

	array<float>^ Half::ConvertToFloat( array<Half>^ values )
	{
		array<float>^ results = gcnew array<float>( values->Length );
		pin_ptr<float> pinnedResults = &results[0];
		pin_ptr<Half> pinnedValues = &values[0];

		D3DXFloat16To32Array( reinterpret_cast<FLOAT*>( pinnedResults ), 
			reinterpret_cast<const D3DXFLOAT16*>( pinnedValues ), values->Length );

		return results;
	}

	array<Half>^ Half::ConvertToHalf( array<float>^ values )
	{
		array<Half>^ results = gcnew array<Half>( values->Length );
		pin_ptr<Half> pinnedResults = &results[0];
		pin_ptr<float> pinnedValues = &values[0];

		D3DXFloat32To16Array( reinterpret_cast<D3DXFLOAT16*>( pinnedResults ), 
			reinterpret_cast<const FLOAT*>( pinnedValues ), values->Length );

		return results;
	}

	Half::operator Half( float value )
	{
		return Half( value );
	}

	Half::operator float( Half value )
	{
		float result;
		D3DXFloat16To32Array( &result, reinterpret_cast<D3DXFLOAT16*>( &value ), 1 );
		return result;
	}

	bool Half::operator == ( Half left, Half right )
	{
		return Half::Equals( left, right );
	}

	bool Half::operator != ( Half left, Half right )
	{
		return !Half::Equals( left, right );
	}

	String^ Half::ToString()
	{
		Half temp = Half( m_Value );
		return ( static_cast<float>( temp ) ).ToString( CultureInfo::CurrentCulture );
	}

	int Half::GetHashCode()
	{
		return (m_Value * 3 / 2) ^ m_Value;
	}

	bool Half::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Half>( value ) );
	}

	bool Half::Equals( Half value )
	{
		return ( value.m_Value == m_Value );
	}

	bool Half::Equals( Half% value1, Half% value2 )
	{
		return ( value1.m_Value == value2.m_Value );
	}
}