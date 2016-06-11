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

#include <d3d11.h>

#include "CounterMetadata11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 	
	CounterMetadata::CounterMetadata( CounterType type, int count, System::String^ name, System::String^ units, System::String^ description )
	: m_Type( type ), m_ActiveCounters( count ), m_Name( name ), m_Units( units ), m_Description( description )
	{
	}

	CounterType CounterMetadata::Type::get()
	{
		return m_Type;
	}

	int CounterMetadata::HardwareCounterCount::get()
	{
		return m_ActiveCounters;
	}
	
	String^ CounterMetadata::Name::get()
	{
		return m_Name;
	}
	
	String^ CounterMetadata::Units::get()
	{
		return m_Units;
	}
	
	String^ CounterMetadata::Description::get()
	{
		return m_Description;
	}

	bool CounterMetadata::operator == ( CounterMetadata left, CounterMetadata right )
	{
		return CounterMetadata::Equals( left, right );
	}

	bool CounterMetadata::operator != ( CounterMetadata left, CounterMetadata right )
	{
		return !CounterMetadata::Equals( left, right );
	}

	int CounterMetadata::GetHashCode()
	{
		return m_Type.GetHashCode() + m_ActiveCounters.GetHashCode() + m_Name->GetHashCode()
			 + m_Units->GetHashCode() + m_Description->GetHashCode();
	}

	bool CounterMetadata::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CounterMetadata>( value ) );
	}

	bool CounterMetadata::Equals( CounterMetadata value )
	{
		return ( m_Type == value.m_Type && m_ActiveCounters == value.m_ActiveCounters && m_Name == value.m_Name
			 && m_Units == value.m_Units && m_Description == value.m_Description );
	}

	bool CounterMetadata::Equals( CounterMetadata% value1, CounterMetadata% value2 )
	{
		return ( value1.m_Type == value2.m_Type && value1.m_ActiveCounters == value2.m_ActiveCounters && value1.m_Name == value2.m_Name
			 && value1.m_Units == value2.m_Units && value1.m_Description == value2.m_Description );
	}

}
}
