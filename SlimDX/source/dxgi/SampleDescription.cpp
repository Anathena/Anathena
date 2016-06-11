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

#include <dxgi.h>

#include "SampleDescription.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace DXGI
{ 	
	SampleDescription::SampleDescription( const DXGI_SAMPLE_DESC& native )
	{
		m_Count = native.Count;
		m_Quality = native.Quality;
	}
	
	SampleDescription::SampleDescription( int count, int quality )
	: m_Count( count ), m_Quality( quality )
	{
	}
	
	DXGI_SAMPLE_DESC SampleDescription::CreateNativeVersion()
	{
		DXGI_SAMPLE_DESC native;
		native.Count = m_Count;
		native.Quality = m_Quality;
		
		return native;
	}
	
	int SampleDescription::Count::get()
	{
		return m_Count;
	}

	void SampleDescription::Count::set( int value )
	{
		m_Count = value;
	}

	int SampleDescription::Quality::get()
	{
		return m_Quality;
	}

	void SampleDescription::Quality::set( int value )
	{
		m_Quality = value;
	}
	
	String^ SampleDescription::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "{0}:{1}", m_Count, m_Quality );
	}

	bool SampleDescription::operator == ( SampleDescription left, SampleDescription right )
	{
		return SampleDescription::Equals( left, right );
	}

	bool SampleDescription::operator != ( SampleDescription left, SampleDescription right )
	{
		return !SampleDescription::Equals( left, right );
	}

	int SampleDescription::GetHashCode()
	{
		return m_Count.GetHashCode() + m_Quality.GetHashCode();
	}

	bool SampleDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<SampleDescription>( value ) );
	}

	bool SampleDescription::Equals( SampleDescription value )
	{
		return ( m_Count == value.m_Count && m_Quality == value.m_Quality );
	}

	bool SampleDescription::Equals( SampleDescription% value1, SampleDescription% value2 )
	{
		return ( value1.m_Count == value2.m_Count && value1.m_Quality == value2.m_Quality );
	}
}
}
