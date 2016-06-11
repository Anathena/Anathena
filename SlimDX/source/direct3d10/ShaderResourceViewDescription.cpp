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

#include "ShaderResourceViewDescription.h"

namespace SlimDX
{
namespace Direct3D10
{
	ShaderResourceViewDescription::ShaderResourceViewDescription( const D3D10_SHADER_RESOURCE_VIEW_DESC& native )
	: m_ElementOffset( 0 ), m_ElementWidth( 0 ), m_MipLevels( 0 ), m_MostDetailedMip( 0 ), m_ArraySize( 0 ), m_FirstArraySlice( 0 )
	{
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_ViewDimension = static_cast<ShaderResourceViewDimension>( native.ViewDimension );
		switch( m_ViewDimension )
		{
			case ShaderResourceViewDimension::Buffer:
				m_ElementOffset = native.Buffer.ElementOffset;
				m_ElementWidth = native.Buffer.ElementWidth;
				break;
			case ShaderResourceViewDimension::Texture1D:
				m_MipLevels = native.Texture1D.MipLevels;
				m_MostDetailedMip = native.Texture1D.MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture1DArray:
				m_ArraySize = native.Texture1DArray.ArraySize;
				m_FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				m_MipLevels = native.Texture1DArray.MipLevels;
				m_MostDetailedMip = native.Texture1DArray.MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2D:
				m_MipLevels = native.Texture2D.MipLevels;
				m_MostDetailedMip = native.Texture2D.MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2DArray:
				m_ArraySize = native.Texture2DArray.ArraySize;
				m_FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				m_MipLevels = native.Texture2DArray.MipLevels;
				m_MostDetailedMip = native.Texture2DArray.MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case ShaderResourceViewDimension::Texture2DMultisampledArray:
				m_ArraySize = native.Texture2DMSArray.ArraySize;
				m_FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;
			case ShaderResourceViewDimension::Texture3D:
				m_MipLevels = native.Texture3D.MipLevels;
				m_MostDetailedMip = native.Texture3D.MostDetailedMip;
				break;
			case ShaderResourceViewDimension::TextureCube:
				m_MipLevels = native.TextureCube.MipLevels;
				m_MostDetailedMip = native.TextureCube.MostDetailedMip;
				break;
			default:
				break;
		}
	}
	
	D3D10_SHADER_RESOURCE_VIEW_DESC ShaderResourceViewDescription::CreateNativeVersion()
	{
		D3D10_SHADER_RESOURCE_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.ViewDimension = static_cast<D3D10_SRV_DIMENSION>( m_ViewDimension );
		switch( m_ViewDimension )
		{
			case ShaderResourceViewDimension::Buffer:
				native.Buffer.ElementOffset = m_ElementOffset;
				native.Buffer.ElementWidth = m_ElementWidth;
				break;
			case ShaderResourceViewDimension::Texture1D:
				native.Texture1D.MipLevels = m_MipLevels;
				native.Texture1D.MostDetailedMip = m_MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = m_ArraySize;
				native.Texture1DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture1DArray.MipLevels = m_MipLevels;
				native.Texture1DArray.MostDetailedMip = m_MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2D:
				native.Texture2D.MipLevels = m_MipLevels;
				native.Texture2D.MostDetailedMip = m_MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = m_ArraySize;
				native.Texture2DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture2DArray.MipLevels = m_MipLevels;
				native.Texture2DArray.MostDetailedMip = m_MostDetailedMip;
				break;
			case ShaderResourceViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case ShaderResourceViewDimension::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = m_ArraySize;
				native.Texture2DMSArray.FirstArraySlice = m_FirstArraySlice;
				break;
			case ShaderResourceViewDimension::Texture3D:
				native.Texture3D.MipLevels = m_MipLevels;
				native.Texture3D.MostDetailedMip = m_MostDetailedMip;
				break;
			case ShaderResourceViewDimension::TextureCube:
				native.TextureCube.MipLevels = m_MipLevels;
				native.TextureCube.MostDetailedMip = m_MostDetailedMip;
				break;
			default:
				break;
		}
		
		return native;
	}

	DXGI::Format ShaderResourceViewDescription::Format::get()
	{
		return m_Format;
	}

	void ShaderResourceViewDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	ShaderResourceViewDimension ShaderResourceViewDescription::Dimension::get()
	{
		return m_ViewDimension;
	}

	void ShaderResourceViewDescription::Dimension::set( ShaderResourceViewDimension value )
	{
		m_ViewDimension = value;
	}
	
	int ShaderResourceViewDescription::ElementOffset::get()
	{
		return m_ElementOffset;
	}

	void ShaderResourceViewDescription::ElementOffset::set( int value )
	{
		m_ElementOffset = value;
	}
	
	int ShaderResourceViewDescription::ElementWidth::get()
	{
		return m_ElementWidth;
	}

	void ShaderResourceViewDescription::ElementWidth::set( int value )
	{
		m_ElementWidth = value;
	}
	
	int ShaderResourceViewDescription::MipLevels::get()
	{
		return m_MipLevels;
	}

	void ShaderResourceViewDescription::MipLevels::set( int value )
	{
		m_MipLevels = value;
	}
	
	int ShaderResourceViewDescription::MostDetailedMip::get()
	{
		return m_MostDetailedMip;
	}

	void ShaderResourceViewDescription::MostDetailedMip::set( int value )
	{
		m_MostDetailedMip = value;
	}
	
	int ShaderResourceViewDescription::ArraySize::get()
	{
		return m_ArraySize;
	}

	void ShaderResourceViewDescription::ArraySize::set( int value )
	{
		m_ArraySize = value;
	}
	
	int ShaderResourceViewDescription::FirstArraySlice::get()
	{
		return m_FirstArraySlice;
	}

	void ShaderResourceViewDescription::FirstArraySlice::set( int value )
	{
		m_FirstArraySlice = value;
	}

	
	bool ShaderResourceViewDescription::operator == ( ShaderResourceViewDescription left, ShaderResourceViewDescription right )
	{
		return ShaderResourceViewDescription::Equals( left, right );
	}

	bool ShaderResourceViewDescription::operator != ( ShaderResourceViewDescription left, ShaderResourceViewDescription right )
	{
		return !ShaderResourceViewDescription::Equals( left, right );
	}

	int ShaderResourceViewDescription::GetHashCode()
	{
		return m_Format.GetHashCode() + m_ViewDimension.GetHashCode() + m_ElementOffset.GetHashCode()
			 + m_ElementWidth.GetHashCode() + m_MostDetailedMip.GetHashCode() + m_MipLevels.GetHashCode()
			 + m_FirstArraySlice.GetHashCode() + m_ArraySize.GetHashCode();
	}

	bool ShaderResourceViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderResourceViewDescription>( value ) );
	}

	bool ShaderResourceViewDescription::Equals( ShaderResourceViewDescription value )
	{
		return ( m_Format == value.m_Format && m_ViewDimension == value.m_ViewDimension && m_ElementOffset == value.m_ElementOffset
			 && m_ElementWidth == value.m_ElementWidth && m_MostDetailedMip == value.m_MostDetailedMip && m_MipLevels == value.m_MipLevels
			 && m_FirstArraySlice == value.m_FirstArraySlice && m_ArraySize == value.m_ArraySize );
	}

	bool ShaderResourceViewDescription::Equals( ShaderResourceViewDescription% value1, ShaderResourceViewDescription% value2 )
	{
		return ( value1.m_Format == value2.m_Format && value1.m_ViewDimension == value2.m_ViewDimension && value1.m_ElementOffset == value2.m_ElementOffset
			 && value1.m_ElementWidth == value2.m_ElementWidth && value1.m_MostDetailedMip == value2.m_MostDetailedMip && value1.m_MipLevels == value2.m_MipLevels
			 && value1.m_FirstArraySlice == value2.m_FirstArraySlice && value1.m_ArraySize == value2.m_ArraySize );
	}
}
}
