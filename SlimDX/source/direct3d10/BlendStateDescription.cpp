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

#include "../Utilities.h"

#include "BlendStateDescription.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace SlimDX
{
namespace Direct3D10
{ 
	BlendStateDescription::BlendStateDescription( const D3D10_BLEND_DESC& native )
	{
		m_AlphaToCoverageEnable = native.AlphaToCoverageEnable ? true : false;
		m_SrcBlend = static_cast<BlendOption>( native.SrcBlend );
		m_DestBlend = static_cast<BlendOption>( native.DestBlend );
		m_BlendOp = static_cast<Direct3D10::BlendOperation>( native.BlendOp );
		m_SrcBlendAlpha = static_cast<BlendOption>( native.SrcBlendAlpha );
		m_DestBlendAlpha = static_cast<BlendOption>( native.DestBlendAlpha );
		m_BlendOpAlpha = static_cast<Direct3D10::BlendOperation>( native.BlendOpAlpha );
		
		ConstructLazyProperties();
		for(int index = 0; index < 8; ++index)
		{
			m_BlendEnable[ index ] = (native.BlendEnable[ index ] > 0);
			m_RenderTargetWriteMask[ index ] = static_cast<ColorWriteMaskFlags>( native.RenderTargetWriteMask[ index ] );
		}
	}
	
	D3D10_BLEND_DESC BlendStateDescription::CreateNativeVersion()
	{
		D3D10_BLEND_DESC native;
		native.AlphaToCoverageEnable = m_AlphaToCoverageEnable;
		native.SrcBlend = static_cast<D3D10_BLEND>( m_SrcBlend );
		native.DestBlend = static_cast<D3D10_BLEND>( m_DestBlend );
		native.BlendOp = static_cast<D3D10_BLEND_OP>( m_BlendOp );
		native.SrcBlendAlpha = static_cast<D3D10_BLEND>( m_SrcBlendAlpha );
		native.DestBlendAlpha = static_cast<D3D10_BLEND>( m_DestBlendAlpha );
		native.BlendOpAlpha = static_cast<D3D10_BLEND_OP>( m_BlendOpAlpha );
		
		ConstructLazyProperties();
		for(int index = 0; index < 8; ++index)
		{
			native.BlendEnable[ index ] = m_BlendEnable[ index ];
			native.RenderTargetWriteMask[ index ] = static_cast<UINT8>( m_RenderTargetWriteMask[ index ] );
		}
		
		return native;
	}
	
	bool BlendStateDescription::IsAlphaToCoverageEnabled::get()
	{
		return m_AlphaToCoverageEnable;
	}
	
	void BlendStateDescription::IsAlphaToCoverageEnabled::set( bool value )
	{
		m_AlphaToCoverageEnable = value;
	}
	
	BlendOption BlendStateDescription::SourceBlend::get()
	{
		return m_SrcBlend;
	}
	
	void BlendStateDescription::SourceBlend::set( BlendOption value )
	{
		m_SrcBlend = value;
	}
	
	BlendOption BlendStateDescription::DestinationBlend::get()
	{
		return m_DestBlend;
	}
	
	void BlendStateDescription::DestinationBlend::set( BlendOption value )
	{
		m_DestBlend = value;
	}

	Direct3D10::BlendOperation BlendStateDescription::BlendOperation::get()
	{
		return m_BlendOp;
	}

	void BlendStateDescription::BlendOperation::set( Direct3D10::BlendOperation value )
	{
		m_BlendOp = value;
	}
	
	BlendOption BlendStateDescription::SourceAlphaBlend::get()
	{
		return m_SrcBlendAlpha;
	}
	
	void BlendStateDescription::SourceAlphaBlend::set( BlendOption value )
	{
		m_SrcBlendAlpha = value;
	}

	BlendOption BlendStateDescription::DestinationAlphaBlend::get()
	{
		return m_DestBlendAlpha;
	}
	
	void BlendStateDescription::DestinationAlphaBlend::set( BlendOption value )
	{
		m_DestBlendAlpha = value;
	}
	
	Direct3D10::BlendOperation BlendStateDescription::AlphaBlendOperation::get()
	{
		return m_BlendOpAlpha;
	}
	
	void BlendStateDescription::AlphaBlendOperation::set( Direct3D10::BlendOperation value )
	{
		m_BlendOpAlpha = value;
	}

	bool BlendStateDescription::GetBlendEnable( UInt32 index )
	{
		ConstructLazyProperties();
		return m_BlendEnable[ index ];
	}
	
	void BlendStateDescription::SetBlendEnable( UInt32 index, bool value )
	{
		ConstructLazyProperties();
		m_BlendEnable[ index ] = value;
	}
	
	ColorWriteMaskFlags BlendStateDescription::GetWriteMask( UInt32 index )
	{
		ConstructLazyProperties();
		return m_RenderTargetWriteMask[ index ];
	}
	
	void BlendStateDescription::SetWriteMask( UInt32 index, ColorWriteMaskFlags value )
	{
		ConstructLazyProperties();
		m_RenderTargetWriteMask[ index ] = value;
	}

	void BlendStateDescription::ConstructLazyProperties()
	{
		if( m_BlendEnable == nullptr )
		{
			m_BlendEnable = gcnew array<bool>(8);
			m_RenderTargetWriteMask = gcnew array<ColorWriteMaskFlags>(8);
			for(int index = 0; index < 8; ++index)
			{
				m_BlendEnable[ index ] = false;
				m_RenderTargetWriteMask[ index ] = ColorWriteMaskFlags::All;
			}
		}
	}

	bool BlendStateDescription::operator == ( BlendStateDescription left, BlendStateDescription right )
	{
		return BlendStateDescription::Equals( left, right );
	}

	bool BlendStateDescription::operator != ( BlendStateDescription left, BlendStateDescription right )
	{
		return !BlendStateDescription::Equals( left, right );
	}

	int BlendStateDescription::GetHashCode()
	{
		ConstructLazyProperties();
		return (
			m_AlphaToCoverageEnable.GetHashCode() +
			Utilities::GetElementHashCode( m_BlendEnable ) +
			m_SrcBlend.GetHashCode() +
			m_DestBlend.GetHashCode() +
			m_BlendOp.GetHashCode() +
			m_SrcBlendAlpha.GetHashCode() +
			m_DestBlendAlpha.GetHashCode() +
			m_BlendOpAlpha.GetHashCode() +
			m_RenderTargetWriteMask->GetHashCode()
		);
	}

	bool BlendStateDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<BlendStateDescription>( value ) );
	}

	bool BlendStateDescription::Equals( BlendStateDescription value )
	{
		ConstructLazyProperties();
		value.ConstructLazyProperties();
		return (
			m_AlphaToCoverageEnable == value.m_AlphaToCoverageEnable &&
			Utilities::CheckElementEquality( m_BlendEnable, value.m_BlendEnable ) &&
			m_SrcBlend == value.m_SrcBlend &&
			m_DestBlend == value.m_DestBlend &&
			m_BlendOp == value.m_BlendOp &&
			m_SrcBlendAlpha == value.m_SrcBlendAlpha &&
			m_DestBlendAlpha == value.m_DestBlendAlpha &&
			m_BlendOpAlpha == value.m_BlendOpAlpha &&
			Utilities::CheckElementEquality( m_RenderTargetWriteMask, value.m_RenderTargetWriteMask )
		);
	}

	bool BlendStateDescription::Equals( BlendStateDescription% value1, BlendStateDescription% value2 )
	{
		value1.ConstructLazyProperties();
		value2.ConstructLazyProperties();
		return (
			value1.m_AlphaToCoverageEnable == value2.m_AlphaToCoverageEnable &&
			Utilities::CheckElementEquality( value1.m_BlendEnable, value2.m_BlendEnable ) &&
			value1.m_SrcBlend == value2.m_SrcBlend &&
			value1.m_DestBlend == value2.m_DestBlend &&
			value1.m_BlendOp == value2.m_BlendOp &&
			value1.m_SrcBlendAlpha == value2.m_SrcBlendAlpha &&
			value1.m_DestBlendAlpha == value2.m_DestBlendAlpha &&
			value1.m_BlendOpAlpha == value2.m_BlendOpAlpha &&
			Utilities::CheckElementEquality( value1.m_RenderTargetWriteMask, value2.m_RenderTargetWriteMask )
		);
	}
}
}
