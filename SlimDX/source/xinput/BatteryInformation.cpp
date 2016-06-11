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

#include <windows.h>
#include <xinput.h>

#include "BatteryInformation.h"

namespace SlimDX
{
namespace XInput
{
	BatteryInformation::BatteryInformation( const XINPUT_BATTERY_INFORMATION& native )
	{
		m_Type = static_cast<BatteryType>( native.BatteryType );
		m_Level = static_cast<BatteryLevel>( native.BatteryLevel );
	}
	
	BatteryType BatteryInformation::Type::get()
	{
		return m_Type;
	}

	BatteryLevel BatteryInformation::Level::get()
	{
		return m_Level;
	}

	bool BatteryInformation::operator == ( BatteryInformation left, BatteryInformation right )
	{
		return BatteryInformation::Equals( left, right );
	}

	bool BatteryInformation::operator != ( BatteryInformation left, BatteryInformation right )
	{
		return !BatteryInformation::Equals( left, right );
	}

	int BatteryInformation::GetHashCode()
	{
		return m_Type.GetHashCode() + m_Level.GetHashCode();
	}

	bool BatteryInformation::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<BatteryInformation>( value ) );
	}

	bool BatteryInformation::Equals( BatteryInformation value )
	{
		return ( m_Type == value.m_Type && m_Level == value.m_Level );
	}

	bool BatteryInformation::Equals( BatteryInformation% value1, BatteryInformation% value2 )
	{
		return ( value1.m_Type == value2.m_Type && value1.m_Level == value2.m_Level );
	}
}
}