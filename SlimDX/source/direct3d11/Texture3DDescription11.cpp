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

#include "Texture3DDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{ 	
	Texture3DDescription::Texture3DDescription( const D3D11_TEXTURE3D_DESC& native )
	{
		m_Width = native.Width;
		m_Height = native.Height;
		m_Depth = native.Depth;
		m_MipLevels = native.MipLevels;
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_Usage = static_cast<ResourceUsage>( native.Usage );
		m_BindFlags = static_cast<Direct3D11::BindFlags>( native.BindFlags );
		m_CPUAccessFlags = static_cast<Direct3D11::CpuAccessFlags>( native.CPUAccessFlags );
		m_MiscFlags = static_cast<ResourceOptionFlags>( native.MiscFlags );
	}

	D3D11_TEXTURE3D_DESC Texture3DDescription::CreateNativeVersion()
	{
		D3D11_TEXTURE3D_DESC native;
		native.Width = m_Width;
		native.Height = m_Height;
		native.Depth = m_Depth;
		native.MipLevels = m_MipLevels;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.Usage = static_cast<D3D11_USAGE>( m_Usage );
		native.BindFlags = static_cast<UINT>( m_BindFlags );
		native.CPUAccessFlags = static_cast<UINT>( m_CPUAccessFlags );
		native.MiscFlags = static_cast<UINT>( m_MiscFlags );
		
		return native;
	}

	int Texture3DDescription::Width::get()
	{
		return m_Width;
	}

	void Texture3DDescription::Width::set( int value )
	{
		m_Width = value;
	}
	
	int Texture3DDescription::Height::get()
	{
		return m_Height;
	}

	void Texture3DDescription::Height::set( int value )
	{
		m_Height = value;
	}
	
	int Texture3DDescription::Depth::get()
	{
		return m_Depth;
	}

	void Texture3DDescription::Depth::set( int value )
	{
		m_Depth = value;
	}
	
	int Texture3DDescription::MipLevels::get()
	{
		return m_MipLevels;
	}

	void Texture3DDescription::MipLevels::set( int value )
	{
		m_MipLevels = value;
	}

	DXGI::Format Texture3DDescription::Format::get()
	{
		return m_Format;
	}

	void Texture3DDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	ResourceUsage Texture3DDescription::Usage::get()
	{
		return m_Usage;
	}

	void Texture3DDescription::Usage::set( ResourceUsage value )
	{
		m_Usage = value;
	}
	
	Direct3D11::BindFlags Texture3DDescription::BindFlags::get()
	{
		return m_BindFlags;
	}

	void Texture3DDescription::BindFlags::set( Direct3D11::BindFlags value )
	{
		m_BindFlags = value;
	}
	
	Direct3D11::CpuAccessFlags Texture3DDescription::CpuAccessFlags::get()
	{
		return m_CPUAccessFlags;
	}

	void Texture3DDescription::CpuAccessFlags::set( Direct3D11::CpuAccessFlags value )
	{
		m_CPUAccessFlags = value;
	}
	
	ResourceOptionFlags Texture3DDescription::OptionFlags::get()
	{
		return m_MiscFlags;
	}

	void Texture3DDescription::OptionFlags::set( ResourceOptionFlags value )
	{
		m_MiscFlags = value;
	}

	bool Texture3DDescription::operator == ( Texture3DDescription left, Texture3DDescription right )
	{
		return Texture3DDescription::Equals( left, right );
	}

	bool Texture3DDescription::operator != ( Texture3DDescription left, Texture3DDescription right )
	{
		return !Texture3DDescription::Equals( left, right );
	}

	int Texture3DDescription::GetHashCode()
	{
		return m_Width.GetHashCode() + m_Height.GetHashCode() + m_Depth.GetHashCode()
			 + m_MipLevels.GetHashCode() + m_Format.GetHashCode() + m_Usage.GetHashCode()
			 + m_BindFlags.GetHashCode() + m_CPUAccessFlags.GetHashCode() + m_MiscFlags.GetHashCode();
	}

	bool Texture3DDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Texture3DDescription>( value ) );
	}

	bool Texture3DDescription::Equals( Texture3DDescription value )
	{
		return ( m_Width == value.m_Width && m_Height == value.m_Height && m_Depth == value.m_Depth
			 && m_MipLevels == value.m_MipLevels && m_Format == value.m_Format && m_Usage == value.m_Usage
			 && m_BindFlags == value.m_BindFlags && m_CPUAccessFlags == value.m_CPUAccessFlags && m_MiscFlags == value.m_MiscFlags );
	}

	bool Texture3DDescription::Equals( Texture3DDescription% value1, Texture3DDescription% value2 )
	{
		return ( value1.m_Width == value2.m_Width && value1.m_Height == value2.m_Height && value1.m_Depth == value2.m_Depth
			 && value1.m_MipLevels == value2.m_MipLevels && value1.m_Format == value2.m_Format && value1.m_Usage == value2.m_Usage
			 && value1.m_BindFlags == value2.m_BindFlags && value1.m_CPUAccessFlags == value2.m_CPUAccessFlags && value1.m_MiscFlags == value2.m_MiscFlags );
	}
}
}
