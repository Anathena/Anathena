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

#include "RasterizerStateDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{ 
	RasterizerStateDescription::RasterizerStateDescription( const D3D11_RASTERIZER_DESC& native )
	{
		m_FillMode = static_cast<Direct3D11::FillMode>( native.FillMode );
		m_CullMode = static_cast<Direct3D11::CullMode>( native.CullMode );
		m_FrontCounterClockwise = native.FrontCounterClockwise ? true : false;
		m_DepthBias = native.DepthBias;
		m_DepthBiasClamp = native.DepthBiasClamp;
		m_SlopeScaledDepthBias = native.SlopeScaledDepthBias;
		m_DepthClipEnable = native.DepthClipEnable ? true : false;
		m_ScissorEnable = native.ScissorEnable ? true : false;
		m_MultisampleEnable = native.MultisampleEnable ? true : false;
		m_AntialiasedLineEnable = native.AntialiasedLineEnable ? true : false;
	}
	
	D3D11_RASTERIZER_DESC RasterizerStateDescription::CreateNativeVersion()
	{
		D3D11_RASTERIZER_DESC native;
		native.FillMode = static_cast<D3D11_FILL_MODE>( m_FillMode );
		native.CullMode = static_cast<D3D11_CULL_MODE>( m_CullMode );
		native.FrontCounterClockwise = m_FrontCounterClockwise;
		native.DepthBias = m_DepthBias;
		native.DepthBiasClamp = m_DepthBiasClamp;
		native.SlopeScaledDepthBias = m_SlopeScaledDepthBias;
		native.DepthClipEnable = m_DepthClipEnable;
		native.ScissorEnable = m_ScissorEnable;
		native.MultisampleEnable = m_MultisampleEnable;
		native.AntialiasedLineEnable = m_AntialiasedLineEnable;
		
		return native;
	}
	
	Direct3D11::FillMode RasterizerStateDescription::FillMode::get()
	{
		return m_FillMode;
	}
	
	void RasterizerStateDescription::FillMode::set( Direct3D11::FillMode value )
	{
		m_FillMode = value;	
	}
	
	Direct3D11::CullMode RasterizerStateDescription::CullMode::get()
	{
		return m_CullMode;
	}
	
	void RasterizerStateDescription::CullMode::set( Direct3D11::CullMode value )
	{
		m_CullMode = value;	
	}
	
	bool RasterizerStateDescription::IsFrontCounterclockwise::get()
	{
		return m_FrontCounterClockwise;
	}
	
	void RasterizerStateDescription::IsFrontCounterclockwise::set( bool value )
	{
		m_FrontCounterClockwise = value;	
	}
	
	int RasterizerStateDescription::DepthBias::get()
	{
		return m_DepthBias;
	}
	
	void RasterizerStateDescription::DepthBias::set( int value )
	{
		m_DepthBias = value;	
	}
	
	float RasterizerStateDescription::DepthBiasClamp::get()
	{
		return m_DepthBiasClamp;
	}
	
	void RasterizerStateDescription::DepthBiasClamp::set( float value )
	{
		m_DepthBiasClamp = value;	
	}
	
	float RasterizerStateDescription::SlopeScaledDepthBias::get()
	{
		return m_SlopeScaledDepthBias;
	}
	
	void RasterizerStateDescription::SlopeScaledDepthBias::set( float value )
	{
		m_SlopeScaledDepthBias = value;	
	}
	
	bool RasterizerStateDescription::IsDepthClipEnabled::get()
	{
		return m_DepthClipEnable;
	}
	
	void RasterizerStateDescription::IsDepthClipEnabled::set( bool value )
	{
		m_DepthClipEnable = value;	
	}
	
	bool RasterizerStateDescription::IsScissorEnabled::get()
	{
		return m_ScissorEnable;
	}
	
	void RasterizerStateDescription::IsScissorEnabled::set( bool value )
	{
		m_ScissorEnable = value;	
	}
	
	bool RasterizerStateDescription::IsMultisampleEnabled::get()
	{
		return m_MultisampleEnable;
	}
	
	void RasterizerStateDescription::IsMultisampleEnabled::set( bool value )
	{
		m_MultisampleEnable = value;	
	}
	
	bool RasterizerStateDescription::IsAntialiasedLineEnabled::get()
	{
		return m_AntialiasedLineEnable;
	}
	
	void RasterizerStateDescription::IsAntialiasedLineEnabled::set( bool value )
	{
		m_AntialiasedLineEnable = value;	
	}

	bool RasterizerStateDescription::operator == ( RasterizerStateDescription left, RasterizerStateDescription right )
	{
		return RasterizerStateDescription::Equals( left, right );
	}

	bool RasterizerStateDescription::operator != ( RasterizerStateDescription left, RasterizerStateDescription right )
	{
		return !RasterizerStateDescription::Equals( left, right );
	}

	int RasterizerStateDescription::GetHashCode()
	{
		return m_FillMode.GetHashCode() + m_CullMode.GetHashCode() + m_FrontCounterClockwise.GetHashCode()
			 + m_DepthBias.GetHashCode() + m_DepthBiasClamp.GetHashCode() + m_SlopeScaledDepthBias.GetHashCode()
			 + m_DepthClipEnable.GetHashCode() + m_ScissorEnable.GetHashCode() + m_MultisampleEnable.GetHashCode()
			 + m_AntialiasedLineEnable.GetHashCode();
	}

	bool RasterizerStateDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RasterizerStateDescription>( value ) );
	}

	bool RasterizerStateDescription::Equals( RasterizerStateDescription value )
	{
		return ( m_FillMode == value.m_FillMode && m_CullMode == value.m_CullMode && m_FrontCounterClockwise == value.m_FrontCounterClockwise
			 && m_DepthBias == value.m_DepthBias && m_DepthBiasClamp == value.m_DepthBiasClamp && m_SlopeScaledDepthBias == value.m_SlopeScaledDepthBias
			 && m_DepthClipEnable == value.m_DepthClipEnable && m_ScissorEnable == value.m_ScissorEnable && m_MultisampleEnable == value.m_MultisampleEnable
			 && m_AntialiasedLineEnable == value.m_AntialiasedLineEnable );
	}

	bool RasterizerStateDescription::Equals( RasterizerStateDescription% value1, RasterizerStateDescription% value2 )
	{
		return ( value1.m_FillMode == value2.m_FillMode && value1.m_CullMode == value2.m_CullMode && value1.m_FrontCounterClockwise == value2.m_FrontCounterClockwise
			 && value1.m_DepthBias == value2.m_DepthBias && value1.m_DepthBiasClamp == value2.m_DepthBiasClamp && value1.m_SlopeScaledDepthBias == value2.m_SlopeScaledDepthBias
			 && value1.m_DepthClipEnable == value2.m_DepthClipEnable && value1.m_ScissorEnable == value2.m_ScissorEnable && value1.m_MultisampleEnable == value2.m_MultisampleEnable
			 && value1.m_AntialiasedLineEnable == value2.m_AntialiasedLineEnable );
	}
}
}
