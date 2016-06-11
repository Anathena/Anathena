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

#include "ModeDescription.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace DXGI
{ 	
	ModeDescription::ModeDescription( int width, int height, Rational refreshRate, DXGI::Format format )
	: m_Width( width ), m_Height( height ), m_RefreshRate( refreshRate ), m_Format( format ),
		m_ScanlineOrdering( DisplayModeScanlineOrdering::Unspecified ), m_Scaling( DisplayModeScaling::Unspecified )
	{
	}
	
	ModeDescription::ModeDescription( const DXGI_MODE_DESC& native )
	{
		m_Width = native.Width;
		m_Height = native.Height;
		m_RefreshRate.Numerator = native.RefreshRate.Numerator;
		m_RefreshRate.Denominator = native.RefreshRate.Denominator;
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_ScanlineOrdering = static_cast<DisplayModeScanlineOrdering>( native.ScanlineOrdering );
		m_Scaling = static_cast<DisplayModeScaling>( native.Scaling );
	}

	DXGI_MODE_DESC ModeDescription::CreateNativeVersion()
	{
		DXGI_MODE_DESC native;
		native.Width = m_Width;
		native.Height = m_Height;
		native.RefreshRate.Numerator = m_RefreshRate.Numerator;
		native.RefreshRate.Denominator = m_RefreshRate.Denominator;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.ScanlineOrdering = static_cast<DXGI_MODE_SCANLINE_ORDER>( m_ScanlineOrdering );
		native.Scaling = static_cast<DXGI_MODE_SCALING>( m_Scaling );
		
		return native;
	}

	int ModeDescription::Width::get()
	{
		return m_Width;
	}

	void ModeDescription::Width::set( int value )
	{
		m_Width = value;
	}

	int ModeDescription::Height::get()
	{
		return m_Height;
	}
	
	void ModeDescription::Height::set( int value )
	{
		m_Height = value;
	}
	
	Rational ModeDescription::RefreshRate::get()
	{
		return m_RefreshRate;
	}
	
	void ModeDescription::RefreshRate::set( Rational value )
	{
		m_RefreshRate = value;
	}
	
	DXGI::Format ModeDescription::Format::get()
	{
		return m_Format;
	}
	
	void ModeDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	DisplayModeScanlineOrdering ModeDescription::ScanlineOrdering::get()
	{
		return m_ScanlineOrdering;
	}
	
	void ModeDescription::ScanlineOrdering::set( DisplayModeScanlineOrdering value )
	{
		m_ScanlineOrdering = value;
	}
	
	DisplayModeScaling ModeDescription::Scaling::get()
	{
		return m_Scaling;
	}
	
	void ModeDescription::Scaling::set( DisplayModeScaling value )
	{
		m_Scaling = value;
	}

	String^ ModeDescription::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "{0}x{1}x{2}", m_Width, m_Height, m_RefreshRate.Denominator == 0 ? 0 : m_RefreshRate.Numerator / m_RefreshRate.Denominator );
	}

	bool ModeDescription::operator == ( ModeDescription left, ModeDescription right )
	{
		return ModeDescription::Equals( left, right );
	}

	bool ModeDescription::operator != ( ModeDescription left, ModeDescription right )
	{
		return !ModeDescription::Equals( left, right );
	}

	int ModeDescription::GetHashCode()
	{
		return m_Width.GetHashCode() + m_Height.GetHashCode() + m_RefreshRate.GetHashCode()
			 + m_Format.GetHashCode() + m_ScanlineOrdering.GetHashCode() + m_Scaling.GetHashCode();
	}

	bool ModeDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ModeDescription>( value ) );
	}

	bool ModeDescription::Equals( ModeDescription value )
	{
		return ( m_Width == value.m_Width && m_Height == value.m_Height && m_RefreshRate == value.m_RefreshRate
			 && m_Format == value.m_Format && m_ScanlineOrdering == value.m_ScanlineOrdering && m_Scaling == value.m_Scaling );
	}

	bool ModeDescription::Equals( ModeDescription% value1, ModeDescription% value2 )
	{
		return ( value1.m_Width == value2.m_Width && value1.m_Height == value2.m_Height && value1.m_RefreshRate == value2.m_RefreshRate
			 && value1.m_Format == value2.m_Format && value1.m_ScanlineOrdering == value2.m_ScanlineOrdering && value1.m_Scaling == value2.m_Scaling );
	}
}
}
