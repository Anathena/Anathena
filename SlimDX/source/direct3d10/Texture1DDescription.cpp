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

#include "Texture1DDescription.h"

namespace SlimDX
{
namespace Direct3D10
{ 	
	Texture1DDescription::Texture1DDescription( const D3D10_TEXTURE1D_DESC& native )
	{
		m_Width = native.Width;
		m_MipLevels = native.MipLevels;
		m_ArraySize = native.ArraySize;
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_Usage = static_cast<ResourceUsage>( native.Usage );
		m_BindFlags = static_cast<Direct3D10::BindFlags>( native.BindFlags );
		m_CPUAccessFlags = static_cast<Direct3D10::CpuAccessFlags>( native.CPUAccessFlags );
		m_MiscFlags = static_cast<ResourceOptionFlags>( native.MiscFlags );
	}
	
	D3D10_TEXTURE1D_DESC Texture1DDescription::CreateNativeVersion()
	{
		D3D10_TEXTURE1D_DESC native;
		native.Width = m_Width;
		native.MipLevels = m_MipLevels;
		native.ArraySize = m_ArraySize;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.Usage = static_cast<D3D10_USAGE>( m_Usage );
		native.BindFlags = static_cast<UINT>( m_BindFlags );
		native.CPUAccessFlags = static_cast<UINT>( m_CPUAccessFlags );
		native.MiscFlags = static_cast<UINT>( m_MiscFlags );
		
		return native;
	}

	int Texture1DDescription::Width::get()
	{
		return m_Width;
	}

	void Texture1DDescription::Width::set( int value )
	{
		m_Width = value;
	}
	
	int Texture1DDescription::MipLevels::get()
	{
		return m_MipLevels;
	}

	void Texture1DDescription::MipLevels::set( int value )
	{
		m_MipLevels = value;
	}
	
	int Texture1DDescription::ArraySize::get()
	{
		return m_ArraySize;
	}

	void Texture1DDescription::ArraySize::set( int value )
	{
		m_ArraySize = value;
	}
	
	DXGI::Format Texture1DDescription::Format::get()
	{
		return m_Format;
	}

	void Texture1DDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	ResourceUsage Texture1DDescription::Usage::get()
	{
		return m_Usage;
	}

	void Texture1DDescription::Usage::set( ResourceUsage value )
	{
		m_Usage = value;
	}
	
	Direct3D10::BindFlags Texture1DDescription::BindFlags::get()
	{
		return m_BindFlags;
	}

	void Texture1DDescription::BindFlags::set( Direct3D10::BindFlags value )
	{
		m_BindFlags = value;
	}
	
	Direct3D10::CpuAccessFlags Texture1DDescription::CpuAccessFlags::get()
	{
		return m_CPUAccessFlags;
	}

	void Texture1DDescription::CpuAccessFlags::set( Direct3D10::CpuAccessFlags value )
	{
		m_CPUAccessFlags = value;
	}
	
	ResourceOptionFlags Texture1DDescription::OptionFlags::get()
	{
		return m_MiscFlags;
	}

	void Texture1DDescription::OptionFlags::set( ResourceOptionFlags value )
	{
		m_MiscFlags = value;
	}

	bool Texture1DDescription::operator == ( Texture1DDescription left, Texture1DDescription right )
	{
		return Texture1DDescription::Equals( left, right );
	}

	bool Texture1DDescription::operator != ( Texture1DDescription left, Texture1DDescription right )
	{
		return !Texture1DDescription::Equals( left, right );
	}

	int Texture1DDescription::GetHashCode()
	{
		return m_Width.GetHashCode() + m_MipLevels.GetHashCode() + m_ArraySize.GetHashCode()
			 + m_Format.GetHashCode() + m_Usage.GetHashCode() + m_BindFlags.GetHashCode()
			 + m_CPUAccessFlags.GetHashCode() + m_MiscFlags.GetHashCode();
	}

	bool Texture1DDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Texture1DDescription>( value ) );
	}

	bool Texture1DDescription::Equals( Texture1DDescription value )
	{
		return ( m_Width == value.m_Width && m_MipLevels == value.m_MipLevels && m_ArraySize == value.m_ArraySize
			 && m_Format == value.m_Format && m_Usage == value.m_Usage && m_BindFlags == value.m_BindFlags
			 && m_CPUAccessFlags == value.m_CPUAccessFlags && m_MiscFlags == value.m_MiscFlags );
	}

	bool Texture1DDescription::Equals( Texture1DDescription% value1, Texture1DDescription% value2 )
	{
		return ( value1.m_Width == value2.m_Width && value1.m_MipLevels == value2.m_MipLevels && value1.m_ArraySize == value2.m_ArraySize
			 && value1.m_Format == value2.m_Format && value1.m_Usage == value2.m_Usage && value1.m_BindFlags == value2.m_BindFlags
			 && value1.m_CPUAccessFlags == value2.m_CPUAccessFlags && value1.m_MiscFlags == value2.m_MiscFlags );
	}
}
}
