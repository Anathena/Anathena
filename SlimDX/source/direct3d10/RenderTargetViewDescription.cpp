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

#include "RenderTargetViewDescription.h"

namespace SlimDX
{
namespace Direct3D10
{
	RenderTargetViewDescription::RenderTargetViewDescription( const D3D10_RENDER_TARGET_VIEW_DESC& native )
	: m_ElementOffset( 0 ), m_ElementWidth( 0 ), m_MipSlice( 0 ), m_ArraySize( 0 ), m_FirstArraySlice( 0 )
	{
		m_Format = static_cast<DXGI::Format>( native.Format );
		m_ViewDimension = static_cast<RenderTargetViewDimension>( native.ViewDimension );
		switch( m_ViewDimension )
		{
			case RenderTargetViewDimension::Buffer:
				m_ElementOffset = native.Buffer.ElementOffset;
				m_ElementWidth = native.Buffer.ElementWidth;
				break;
			case RenderTargetViewDimension::Texture1D:
				m_MipSlice = native.Texture1D.MipSlice;
				break;
			case RenderTargetViewDimension::Texture1DArray:
				m_ArraySize = native.Texture1DArray.ArraySize;
				m_FirstArraySlice = native.Texture1DArray.FirstArraySlice;
				m_MipSlice = native.Texture1DArray.MipSlice;
				break;
			case RenderTargetViewDimension::Texture2D:
				m_MipSlice = native.Texture2D.MipSlice;
				break;
			case RenderTargetViewDimension::Texture2DArray:
				m_ArraySize = native.Texture2DArray.ArraySize;
				m_FirstArraySlice = native.Texture2DArray.FirstArraySlice;
				m_MipSlice = native.Texture2DArray.MipSlice;
				break;
			case RenderTargetViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case RenderTargetViewDimension::Texture2DMultisampledArray:
				m_ArraySize = native.Texture2DMSArray.ArraySize;
				m_FirstArraySlice = native.Texture2DMSArray.FirstArraySlice;
				break;
			case RenderTargetViewDimension::Texture3D:
				m_MipSlice = native.Texture3D.MipSlice;
				m_FirstDepthSlice = native.Texture3D.FirstWSlice;
				m_DepthSliceCount = native.Texture3D.WSize;
				break;
			default:
				break;
		}
	}
	
	D3D10_RENDER_TARGET_VIEW_DESC RenderTargetViewDescription::CreateNativeVersion()
	{
		D3D10_RENDER_TARGET_VIEW_DESC native;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.ViewDimension = static_cast<D3D10_RTV_DIMENSION>( m_ViewDimension );
		switch( m_ViewDimension )
		{
			case RenderTargetViewDimension::Buffer:
				native.Buffer.ElementOffset = m_ElementOffset;
				native.Buffer.ElementWidth = m_ElementWidth;
				break;
			case RenderTargetViewDimension::Texture1D:
				native.Texture1D.MipSlice = m_MipSlice;
				break;
			case RenderTargetViewDimension::Texture1DArray:
				native.Texture1DArray.ArraySize = m_ArraySize;
				native.Texture1DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture1DArray.MipSlice = m_MipSlice;
				break;
			case RenderTargetViewDimension::Texture2D:
				native.Texture2D.MipSlice = m_MipSlice;
				break;
			case RenderTargetViewDimension::Texture2DArray:
				native.Texture2DArray.ArraySize = m_ArraySize;
				native.Texture2DArray.FirstArraySlice = m_FirstArraySlice;
				native.Texture2DArray.MipSlice = m_MipSlice;
				break;
			case RenderTargetViewDimension::Texture2DMultisampled:
				// Nothing to do here.
				break;
			case RenderTargetViewDimension::Texture2DMultisampledArray:
				native.Texture2DMSArray.ArraySize = m_ArraySize;
				native.Texture2DMSArray.FirstArraySlice = m_FirstArraySlice;
				break;
			case RenderTargetViewDimension::Texture3D:
				native.Texture3D.MipSlice = m_MipSlice;
				native.Texture3D.FirstWSlice = m_FirstDepthSlice;
				native.Texture3D.WSize = m_DepthSliceCount;
				break;
			default:
				break;
		}
		
		return native;
	}

	DXGI::Format RenderTargetViewDescription::Format::get()
	{
		return m_Format;
	}

	void RenderTargetViewDescription::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	RenderTargetViewDimension RenderTargetViewDescription::Dimension::get()
	{
		return m_ViewDimension;
	}

	void RenderTargetViewDescription::Dimension::set( RenderTargetViewDimension value )
	{
		m_ViewDimension = value;
	}
	
	int RenderTargetViewDescription::ElementOffset::get()
	{
		return m_ElementOffset;
	}

	void RenderTargetViewDescription::ElementOffset::set( int value )
	{
		m_ElementOffset = value;
	}
	
	int RenderTargetViewDescription::ElementWidth::get()
	{
		return m_ElementWidth;
	}

	void RenderTargetViewDescription::ElementWidth::set( int value )
	{
		m_ElementWidth = value;
	}
	
	int RenderTargetViewDescription::MipSlice::get()
	{
		return m_MipSlice;
	}

	void RenderTargetViewDescription::MipSlice::set( int value )
	{
		m_MipSlice = value;
	}
	
	int RenderTargetViewDescription::ArraySize::get()
	{
		return m_ArraySize;
	}

	void RenderTargetViewDescription::ArraySize::set( int value )
	{
		m_ArraySize = value;
	}
	
	int RenderTargetViewDescription::FirstArraySlice::get()
	{
		return m_FirstArraySlice;
	}

	void RenderTargetViewDescription::FirstArraySlice::set( int value )
	{
		m_FirstArraySlice = value;
	}
	
	int RenderTargetViewDescription::FirstDepthSlice::get()
	{
		return m_FirstDepthSlice;
	}

	void RenderTargetViewDescription::FirstDepthSlice::set( int value )
	{
		m_FirstDepthSlice = value;
	}
	
	int RenderTargetViewDescription::DepthSliceCount::get()
	{
		return m_DepthSliceCount;
	}

	void RenderTargetViewDescription::DepthSliceCount::set( int value )
	{
		m_DepthSliceCount = value;
	}

	bool RenderTargetViewDescription::operator == ( RenderTargetViewDescription left, RenderTargetViewDescription right )
	{
		return RenderTargetViewDescription::Equals( left, right );
	}

	bool RenderTargetViewDescription::operator != ( RenderTargetViewDescription left, RenderTargetViewDescription right )
	{
		return !RenderTargetViewDescription::Equals( left, right );
	}

	int RenderTargetViewDescription::GetHashCode()
	{
		return m_Format.GetHashCode() + m_ViewDimension.GetHashCode() + m_ElementOffset.GetHashCode()
			 + m_ElementWidth.GetHashCode() + m_MipSlice.GetHashCode() + m_FirstArraySlice.GetHashCode()
			 + m_ArraySize.GetHashCode() + m_FirstDepthSlice.GetHashCode() + m_DepthSliceCount.GetHashCode();
	}

	bool RenderTargetViewDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RenderTargetViewDescription>( value ) );
	}

	bool RenderTargetViewDescription::Equals( RenderTargetViewDescription value )
	{
		return ( m_Format == value.m_Format && m_ViewDimension == value.m_ViewDimension && m_ElementOffset == value.m_ElementOffset
			 && m_ElementWidth == value.m_ElementWidth && m_MipSlice == value.m_MipSlice && m_FirstArraySlice == value.m_FirstArraySlice
			 && m_ArraySize == value.m_ArraySize && m_FirstDepthSlice == value.m_FirstDepthSlice && m_DepthSliceCount == value.m_DepthSliceCount );
	}

	bool RenderTargetViewDescription::Equals( RenderTargetViewDescription% value1, RenderTargetViewDescription% value2 )
	{
		return ( value1.m_Format == value2.m_Format && value1.m_ViewDimension == value2.m_ViewDimension && value1.m_ElementOffset == value2.m_ElementOffset
			 && value1.m_ElementWidth == value2.m_ElementWidth && value1.m_MipSlice == value2.m_MipSlice && value1.m_FirstArraySlice == value2.m_FirstArraySlice
			 && value1.m_ArraySize == value2.m_ArraySize && value1.m_FirstDepthSlice == value2.m_FirstDepthSlice && value1.m_DepthSliceCount == value2.m_DepthSliceCount );
	}
}
}
