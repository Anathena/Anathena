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
#include "stdafx.h"

#include "Direct3D11Exception.h"
#include "Debug11.h"
#include "Device11.h"
#include "DeviceContext11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	Debug::Debug( Device^ device )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
	
		void *result = 0;
		if( RECORD_D3D11( device->InternalPointer->QueryInterface( IID_ID3D11Debug, &result ) ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );
		
		Construct( reinterpret_cast<ID3D11Debug*>( result ) );
	}
	
	DebugFeatureFlags Debug::FeatureFlags::get()
	{
		UINT flags = InternalPointer->GetFeatureMask();
		return static_cast<DebugFeatureFlags>( flags );
	}
	
	void Debug::FeatureFlags::set( DebugFeatureFlags value )
	{
		RECORD_D3D11( InternalPointer->SetFeatureMask( static_cast<UINT>( value ) ) );
	}
	
	int Debug::PresentDelay::get()
	{
		return InternalPointer->GetPresentPerRenderOpDelay();
	}
	
	void Debug::PresentDelay::set( int value )
	{
		RECORD_D3D11( InternalPointer->SetPresentPerRenderOpDelay( value ) );
	}
	
	DXGI::SwapChain^ Debug::SwapChain::get()
	{
		IDXGISwapChain* swapChain = 0;
		if( RECORD_D3D11( InternalPointer->GetSwapChain( &swapChain ) ).IsFailure )
			return nullptr;
		
		return DXGI::SwapChain::FromPointer( swapChain );
	}
	
	void Debug::SwapChain::set( DXGI::SwapChain^ value )
	{
		RECORD_D3D11( InternalPointer->SetSwapChain( value->InternalPointer ) );
	}

	Result Debug::ValidateContext(DeviceContext^ context)
	{
		HRESULT hr = InternalPointer->ValidateContext(context->InternalPointer);
		return RECORD_D3D11(hr);
	}

	Result Debug::ValidateContextForDispatch(DeviceContext^ context)
	{
		HRESULT hr = InternalPointer->ValidateContextForDispatch(context->InternalPointer);
		return RECORD_D3D11(hr);
	}

	Result Debug::ReportLiveDeviceObjects(ReportingLevel level)
	{
		HRESULT hr = InternalPointer->ReportLiveDeviceObjects(static_cast<D3D11_RLDO_FLAGS>(level));
		return RECORD_D3D11(hr);
	}
}
}
