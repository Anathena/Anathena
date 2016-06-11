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

#include "DepthStencilViewDescription.h"

namespace SlimDX
{
namespace Direct3D10
{
	DepthStencilViewDescription::DepthStencilViewDescription( const D3D10_DEPTH_STENCIL_VIEW_DESC& native )
	: m_MipSlice( 0 ), m_ArraySize( 0 ), m_FirstArraySlice( 0 )
	{
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_ViewDimension = static_cast<DepthStencilViewDimension>( native.ViewDimension );
		switch( m_ViewDimension )
		{
			case DepthStencilViewDimension::Texture1D:
				m_MipSlice = native.Texture1D.MipSlice;
				break;
			case DepthStencilViewDimension::Texture1DArray:
				m_ArraySize = native.Texture1DArray.ArraySize;
				m_FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				m_MipSlice = native.Texture1DArray.MipSlice;
				break;
			case DepthStencilViewDimension::Texture2D:
				m_MipSlice = native.Texture2D.MipSlice;
				break;
			case DepthStencilViewDimension::Texture2DArray:
				m_ArraySize = native.Texture2DArray.ArraySize;
				m_FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				m_MipSlice = native.Texture2DArray.MipSlice;
				break;
			case DepthStencilViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case DepthStencilViewDimension::Texture2DMultisampledArray:
				m_ArraySize = native.Texture2DMSArray.ArraySize;
				m_FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;
			default:
				break;
		}
	}
	
	D3D10_DEPTH_STENCIL_VIEW_DESC DepthStencilViewDescription::CreateNativeVersion()
	{
		D3D10_DEPTH_STENCIL_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.ViewDimension = static_cast<D3D10_DSV_DIMENSION>( m_ViewDimension );
		switch( m_ViewDimension )
		{
			case DepthStencilViewDimension::Texture1D:
				native.Texture1D.MipSlice = m_MipSlice;
				break;
			case DepthStencilViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = m_ArraySize;
				native.Texture1DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture1DArray.MipSlice = m_MipSlice;
				break;
			case DepthStencilViewDimension::Texture2D:
				native.Texture2D.MipSlice = m_MipSlice;
				break;
			case DepthStencilViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = m_ArraySize;
				native.Texture2DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture2DArray.MipSlice = m_MipSlice;
				break;
			case DepthStencilViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case DepthStencilViewDimension::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = m_ArraySize;
				native.Texture2DMSArray.FirstArraySlice = m_FirstArraySlice;
				break;
			default:
				break;
		}
		
		return native;
	}

	DXGI::Format DepthStencilViewDescription::Format::get()
	{
		return m_Format;
	}

	void DepthStencilViewDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	DepthStencilViewDimension DepthStencilViewDescription::Dimension::get()
	{
		return m_ViewDimension;
	}

	void DepthStencilViewDescription::Dimension::set( DepthStencilViewDimension value )
	{
		m_ViewDimension = value;
	}

	int DepthStencilViewDescription::MipSlice::get()
	{
		return m_MipSlice;
	}

	void DepthStencilViewDescription::MipSlice::set( int value )
	{
		m_MipSlice = value;
	}
	
	int DepthStencilViewDescription::ArraySize::get()
	{
		return m_ArraySize;
	}

	void DepthStencilViewDescription::ArraySize::set( int value )
	{
		m_ArraySize = value;
	}
	
	int DepthStencilViewDescription::FirstArraySlice::get()
	{
		return m_FirstArraySlice;
	}

	void DepthStencilViewDescription::FirstArraySlice::set( int value )
	{
		m_FirstArraySlice = value;
	}

	bool DepthStencilViewDescription::operator == ( DepthStencilViewDescription left, DepthStencilViewDescription right )
	{
		return DepthStencilViewDescription::Equals( left, right );
	}

	bool DepthStencilViewDescription::operator != ( DepthStencilViewDescription left, DepthStencilViewDescription right )
	{
		return !DepthStencilViewDescription::Equals( left, right );
	}

	int DepthStencilViewDescription::GetHashCode()
	{
		return m_Format.GetHashCode() + m_ViewDimension.GetHashCode() + m_MipSlice.GetHashCode()
			 + m_FirstArraySlice.GetHashCode() + m_ArraySize.GetHashCode();
	}

	bool DepthStencilViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DepthStencilViewDescription>( value ) );
	}

	bool DepthStencilViewDescription::Equals( DepthStencilViewDescription value )
	{
		return ( m_Format == value.m_Format && m_ViewDimension == value.m_ViewDimension && m_MipSlice == value.m_MipSlice
			 && m_FirstArraySlice == value.m_FirstArraySlice && m_ArraySize == value.m_ArraySize );
	}

	bool DepthStencilViewDescription::Equals( DepthStencilViewDescription% value1, DepthStencilViewDescription% value2 )
	{
		return ( value1.m_Format == value2.m_Format && value1.m_ViewDimension == value2.m_ViewDimension && value1.m_MipSlice == value2.m_MipSlice
			 && value1.m_FirstArraySlice == value2.m_FirstArraySlice && value1.m_ArraySize == value2.m_ArraySize );
	}
}
}
