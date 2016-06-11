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
#include <d3d9.h>
#include <d3dx9.h>

#include "PresentParameters.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	D3DPRESENT_PARAMETERS PresentParameters::ToUnmanaged()
	{
		D3DPRESENT_PARAMETERS d3dpp;

		d3dpp.AutoDepthStencilFormat = static_cast<D3DFORMAT>( AutoDepthStencilFormat );
		d3dpp.BackBufferCount = BackBufferCount;
		d3dpp.BackBufferFormat = static_cast<D3DFORMAT>( BackBufferFormat );
		d3dpp.BackBufferHeight = BackBufferHeight;
		d3dpp.BackBufferWidth = BackBufferWidth;
		d3dpp.EnableAutoDepthStencil = EnableAutoDepthStencil;
		d3dpp.Flags = static_cast<DWORD>( PresentFlags );
		d3dpp.FullScreen_RefreshRateInHz = FullScreenRefreshRateInHertz;
		d3dpp.hDeviceWindow = static_cast<HWND>( DeviceWindowHandle.ToPointer() );
		d3dpp.MultiSampleQuality = MultisampleQuality;
		d3dpp.MultiSampleType = static_cast<D3DMULTISAMPLE_TYPE>( Multisample );
		d3dpp.PresentationInterval = static_cast<UINT>( PresentationInterval );
		d3dpp.SwapEffect = static_cast<D3DSWAPEFFECT>( SwapEffect );
		d3dpp.Windowed = Windowed;

		return d3dpp;
	}

	PresentParameters::PresentParameters( const D3DPRESENT_PARAMETERS& d3dpp )
	{
		AutoDepthStencilFormat = static_cast<Format>( d3dpp.AutoDepthStencilFormat );
		BackBufferCount = d3dpp.BackBufferCount;
		BackBufferFormat = static_cast<Format>( d3dpp.BackBufferFormat );
		BackBufferHeight = d3dpp.BackBufferHeight;
		BackBufferWidth = d3dpp.BackBufferWidth;
		EnableAutoDepthStencil = d3dpp.EnableAutoDepthStencil == TRUE;
		PresentFlags = static_cast<SlimDX::Direct3D9::PresentFlags>( d3dpp.Flags );
		FullScreenRefreshRateInHertz = d3dpp.FullScreen_RefreshRateInHz;
		DeviceWindowHandle = IntPtr( d3dpp.hDeviceWindow );
		MultisampleQuality = d3dpp.MultiSampleQuality;
		Multisample = static_cast<MultisampleType>( d3dpp.MultiSampleType );
		PresentationInterval = static_cast<PresentInterval>( d3dpp.PresentationInterval );
		SwapEffect = static_cast<SlimDX::Direct3D9::SwapEffect>( d3dpp.SwapEffect );
		Windowed = d3dpp.Windowed == TRUE;
	}

	PresentParameters::PresentParameters()
	{
		BackBufferWidth = 640;
		BackBufferHeight = 480;
		BackBufferFormat = Format::X8R8G8B8;
		BackBufferCount = 1;

		Multisample = MultisampleType::None;

		SwapEffect = SlimDX::Direct3D9::SwapEffect::Discard;
		DeviceWindowHandle = IntPtr::Zero;
		Windowed = true;
		EnableAutoDepthStencil = true;
		AutoDepthStencilFormat = Format::D24X8;
		PresentFlags = SlimDX::Direct3D9::PresentFlags::None;

		PresentationInterval = PresentInterval::Immediate;
	}

	PresentParameters^ PresentParameters::Clone()
	{
		PresentParameters^ result = gcnew PresentParameters();

		result->BackBufferWidth = BackBufferWidth;
		result->BackBufferHeight = BackBufferHeight;
		result->BackBufferFormat = BackBufferFormat;
		result->BackBufferCount = BackBufferCount;

		result->Multisample = Multisample;
		result->MultisampleQuality = MultisampleQuality;

		result->SwapEffect = SwapEffect;
		result->DeviceWindowHandle = DeviceWindowHandle;
		result->Windowed = Windowed;
		result->EnableAutoDepthStencil = EnableAutoDepthStencil;
		result->AutoDepthStencilFormat = AutoDepthStencilFormat;
		result->PresentFlags = PresentFlags;

		result->FullScreenRefreshRateInHertz = FullScreenRefreshRateInHertz;
		result->PresentationInterval = PresentationInterval;

		return result;
	}

	bool PresentParameters::operator == ( PresentParameters^ left, PresentParameters^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return PresentParameters::Equals( left, right );
	}

	bool PresentParameters::operator != ( PresentParameters^ left, PresentParameters^ right )
	{
		return !( left == right );
	}

	int PresentParameters::GetHashCode()
	{		
		return BackBufferWidth.GetHashCode() + BackBufferHeight.GetHashCode() + BackBufferFormat.GetHashCode() +
			BackBufferCount.GetHashCode() + Multisample.GetHashCode() + MultisampleQuality.GetHashCode() +
			SwapEffect.GetHashCode() + DeviceWindowHandle.GetHashCode() + Windowed.GetHashCode() +
			EnableAutoDepthStencil.GetHashCode() + AutoDepthStencilFormat.GetHashCode() + PresentFlags.GetHashCode() +
			FullScreenRefreshRateInHertz.GetHashCode() + PresentationInterval.GetHashCode();
	}

	bool PresentParameters::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PresentParameters^>( value ) );
	}

	bool PresentParameters::Equals( PresentParameters^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( value->BackBufferWidth == BackBufferWidth && value->BackBufferHeight == BackBufferHeight && value->BackBufferFormat == BackBufferFormat &&
			value->BackBufferCount == BackBufferCount && value->Multisample == Multisample && value->MultisampleQuality == MultisampleQuality &&
			value->SwapEffect == SwapEffect && value->DeviceWindowHandle == DeviceWindowHandle && value->Windowed == Windowed &&
			value->EnableAutoDepthStencil == EnableAutoDepthStencil && value->AutoDepthStencilFormat == AutoDepthStencilFormat && value->PresentFlags == PresentFlags &&
			value->FullScreenRefreshRateInHertz == FullScreenRefreshRateInHertz && value->PresentationInterval == PresentationInterval );
	}

	bool PresentParameters::Equals( PresentParameters^ value1, PresentParameters^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}