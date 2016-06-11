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

#include "Enums11.h"
#include "RenderTargetBlendDescription11.h"

namespace SlimDX
{
namespace Direct3D11
{
	RenderTargetBlendDescription::RenderTargetBlendDescription( const D3D11_RENDER_TARGET_BLEND_DESC& native )
	{
		BlendEnable = native.BlendEnable != FALSE;
		SourceBlend = static_cast<SlimDX::Direct3D11::BlendOption>( native.SrcBlend );
		DestinationBlend = static_cast<SlimDX::Direct3D11::BlendOption>( native.DestBlend );
		BlendOperation = static_cast<SlimDX::Direct3D11::BlendOperation>( native.BlendOp );
		SourceBlendAlpha = static_cast<SlimDX::Direct3D11::BlendOption>( native.SrcBlendAlpha );
		DestinationBlendAlpha = static_cast<SlimDX::Direct3D11::BlendOption>( native.DestBlendAlpha );
		BlendOperationAlpha = static_cast<SlimDX::Direct3D11::BlendOperation>( native.BlendOpAlpha );
		RenderTargetWriteMask = static_cast<SlimDX::Direct3D11::ColorWriteMaskFlags>( native.RenderTargetWriteMask );
	}

	D3D11_RENDER_TARGET_BLEND_DESC RenderTargetBlendDescription::CreateNativeVersion()
	{
		D3D11_RENDER_TARGET_BLEND_DESC native;

		native.BlendEnable = BlendEnable;
		native.SrcBlend = static_cast<D3D11_BLEND>( SourceBlend );
		native.DestBlend = static_cast<D3D11_BLEND>( DestinationBlend );
		native.BlendOp = static_cast<D3D11_BLEND_OP>( BlendOperation );
		native.SrcBlendAlpha = static_cast<D3D11_BLEND>( SourceBlendAlpha );
		native.DestBlendAlpha = static_cast<D3D11_BLEND>( DestinationBlendAlpha );
		native.BlendOpAlpha = static_cast<D3D11_BLEND_OP>( BlendOperationAlpha );
		native.RenderTargetWriteMask = static_cast<UINT8>( RenderTargetWriteMask );

		return native;
	}

	bool RenderTargetBlendDescription::operator == ( RenderTargetBlendDescription left, RenderTargetBlendDescription right )
	{
		return RenderTargetBlendDescription::Equals( left, right );
	}

	bool RenderTargetBlendDescription::operator != ( RenderTargetBlendDescription left, RenderTargetBlendDescription right )
	{
		return !RenderTargetBlendDescription::Equals( left, right );
	}

	int RenderTargetBlendDescription::GetHashCode()
	{
		return RenderTargetWriteMask.GetHashCode() + BlendOperationAlpha.GetHashCode() + DestinationBlendAlpha.GetHashCode() + 
			SourceBlendAlpha.GetHashCode() + BlendOperation.GetHashCode() + DestinationBlend.GetHashCode() + SourceBlend.GetHashCode() + 
			BlendEnable.GetHashCode();
	}

	bool RenderTargetBlendDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RenderTargetBlendDescription>( value ) );
	}

	bool RenderTargetBlendDescription::Equals( RenderTargetBlendDescription value )
	{
		return ( RenderTargetWriteMask == value.RenderTargetWriteMask && BlendOperationAlpha == value.BlendOperationAlpha && 
			DestinationBlendAlpha == value.DestinationBlendAlpha && SourceBlendAlpha == value.SourceBlendAlpha && 
			BlendOperation == value.BlendOperation && DestinationBlend == value.DestinationBlend && SourceBlend == value.SourceBlend && 
			BlendEnable == value.BlendEnable );
	}

	bool RenderTargetBlendDescription::Equals( RenderTargetBlendDescription% value1, RenderTargetBlendDescription% value2 )
	{
		return value1.Equals( value2 );
	}
}
}