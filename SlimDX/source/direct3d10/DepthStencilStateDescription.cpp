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

#include "DepthStencilOperationDescription.h"
#include "DepthStencilStateDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	DepthStencilStateDescription::DepthStencilStateDescription( const D3D10_DEPTH_STENCIL_DESC& native )
	{
		m_DepthEnable = native.DepthEnable ? true : false;
		m_DepthWriteMask = static_cast<Direct3D10::DepthWriteMask>( native.DepthWriteMask );
		m_DepthFunc = static_cast<Comparison>( native.DepthFunc );
		m_StencilEnable = native.StencilEnable ? true : false;
		m_StencilReadMask = native.StencilReadMask;
		m_StencilWriteMask = native.StencilWriteMask;
		m_FrontFace = DepthStencilOperationDescription( native.FrontFace );
		m_BackFace = DepthStencilOperationDescription( native.BackFace );
	}
	
	D3D10_DEPTH_STENCIL_DESC DepthStencilStateDescription::CreateNativeVersion()
	{
		D3D10_DEPTH_STENCIL_DESC native;
		native.DepthEnable = m_DepthEnable;
		native.DepthWriteMask = static_cast<D3D10_DEPTH_WRITE_MASK>( m_DepthWriteMask );
		native.DepthFunc = static_cast<D3D10_COMPARISON_FUNC>( m_DepthFunc );
		native.StencilEnable = m_StencilEnable;
		native.StencilReadMask = m_StencilReadMask;
		native.StencilWriteMask = m_StencilWriteMask;
		native.FrontFace = m_FrontFace.CreateNativeVersion();
		native.BackFace = m_BackFace.CreateNativeVersion();
		
		return native;
	}
	
	bool DepthStencilStateDescription::IsDepthEnabled::get()
	{
		return m_DepthEnable;
	}
	
	void DepthStencilStateDescription::IsDepthEnabled::set( bool value )
	{
		m_DepthEnable = value;
	}
	
	Direct3D10::DepthWriteMask DepthStencilStateDescription::DepthWriteMask::get()
	{
		return m_DepthWriteMask;
	}
	
	void DepthStencilStateDescription::DepthWriteMask::set( Direct3D10::DepthWriteMask value )
	{
		m_DepthWriteMask = value;
	}
	
	Comparison DepthStencilStateDescription::DepthComparison::get()
	{
		return m_DepthFunc;
	}
	
	void DepthStencilStateDescription::DepthComparison::set( Comparison value )
	{
		m_DepthFunc = value;
	}
	
	bool DepthStencilStateDescription::IsStencilEnabled::get()
	{
		return m_StencilEnable;
	}
	
	void DepthStencilStateDescription::IsStencilEnabled::set( bool value )
	{
		m_StencilEnable = value;
	}
	
	Byte DepthStencilStateDescription::StencilReadMask::get()
	{
		return m_StencilReadMask;
	}
	
	void DepthStencilStateDescription::StencilReadMask::set( Byte value )
	{
		m_StencilReadMask = value;
	}
	
	Byte DepthStencilStateDescription::StencilWriteMask::get()
	{
		return m_StencilWriteMask;
	}
	
	void DepthStencilStateDescription::StencilWriteMask::set( Byte value )
	{
		m_StencilWriteMask = value;
	}
	
	DepthStencilOperationDescription DepthStencilStateDescription::FrontFace::get()
	{
		return m_FrontFace;
	}
	
	void DepthStencilStateDescription::FrontFace::set( DepthStencilOperationDescription value )
	{
		m_FrontFace = value;
	}
	
	DepthStencilOperationDescription DepthStencilStateDescription::BackFace::get()
	{
		return m_BackFace;
	}
	
	void DepthStencilStateDescription::BackFace::set( DepthStencilOperationDescription value )
	{
		m_BackFace = value;
	}

	bool DepthStencilStateDescription::operator == ( DepthStencilStateDescription left, DepthStencilStateDescription right )
	{
		return DepthStencilStateDescription::Equals( left, right );
	}

	bool DepthStencilStateDescription::operator != ( DepthStencilStateDescription left, DepthStencilStateDescription right )
	{
		return !DepthStencilStateDescription::Equals( left, right );
	}

	int DepthStencilStateDescription::GetHashCode()
	{
		return m_DepthEnable.GetHashCode() + m_DepthWriteMask.GetHashCode() + m_DepthFunc.GetHashCode()
			 + m_StencilEnable.GetHashCode() + m_StencilReadMask.GetHashCode() + m_StencilWriteMask.GetHashCode()
			 + m_FrontFace.GetHashCode() + m_BackFace.GetHashCode();
	}

	bool DepthStencilStateDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DepthStencilStateDescription>( value ) );
	}

	bool DepthStencilStateDescription::Equals( DepthStencilStateDescription value )
	{
		return ( m_DepthEnable == value.m_DepthEnable && m_DepthWriteMask == value.m_DepthWriteMask && m_DepthFunc == value.m_DepthFunc
			 && m_StencilEnable == value.m_StencilEnable && m_StencilReadMask == value.m_StencilReadMask && m_StencilWriteMask == value.m_StencilWriteMask
			 && m_FrontFace == value.m_FrontFace && m_BackFace == value.m_BackFace );
	}

	bool DepthStencilStateDescription::Equals( DepthStencilStateDescription% value1, DepthStencilStateDescription% value2 )
	{
		return ( value1.m_DepthEnable == value2.m_DepthEnable && value1.m_DepthWriteMask == value2.m_DepthWriteMask && value1.m_DepthFunc == value2.m_DepthFunc
			 && value1.m_StencilEnable == value2.m_StencilEnable && value1.m_StencilReadMask == value2.m_StencilReadMask && value1.m_StencilWriteMask == value2.m_StencilWriteMask
			 && value1.m_FrontFace == value2.m_FrontFace && value1.m_BackFace == value2.m_BackFace );
	}
}
}
