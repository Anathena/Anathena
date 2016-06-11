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

#include "../ComObject.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Direct3D.h"
#include "Surface.h"
#include "PresentParameters.h"
#include "SwapChain.h"

using namespace System;
using namespace System::Drawing;

namespace SlimDX
{
namespace Direct3D9
{
	SwapChain::SwapChain()
	{
		// this is called by SwapChainEx
	}

	SwapChain::SwapChain( SlimDX::Direct3D9::Device^ device, SlimDX::Direct3D9::PresentParameters^ presentParameters )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );

		if( presentParameters == nullptr )
			throw gcnew ArgumentNullException( "presentParameters" );

		D3DPRESENT_PARAMETERS d3dpp;
		d3dpp = presentParameters->ToUnmanaged();

		IDirect3DSwapChain9* swapChain;
		HRESULT hr = device->InternalPointer->CreateAdditionalSwapChain( &d3dpp, &swapChain );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( "Failed to create swap chain." );

		Construct(swapChain);
	}
	
	Surface^ SwapChain::GetBackBuffer( int index )
	{
		IDirect3DSurface9* surface;
		HRESULT hr = InternalPointer->GetBackBuffer( index, D3DBACKBUFFER_TYPE_MONO, &surface );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return Surface::FromPointer( surface );
	}

	Result SwapChain::GetFrontBufferData( Surface^ destinationSurface )
	{
		if( destinationSurface == nullptr )
			throw gcnew ArgumentNullException( "destinationSurface" );

		HRESULT hr = InternalPointer->GetFrontBufferData( destinationSurface->InternalPointer );
		return RECORD_D3D9( hr );
	}

	SlimDX::Direct3D9::Device^ SwapChain::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	SlimDX::Direct3D9::DisplayMode SwapChain::DisplayMode::get()
	{
		SlimDX::Direct3D9::DisplayMode mode;
		HRESULT hr = InternalPointer->GetDisplayMode( reinterpret_cast<D3DDISPLAYMODE*>( &mode ) );
		RECORD_D3D9( hr );

		return mode;
	}

	SlimDX::Direct3D9::RasterStatus SwapChain::RasterStatus::get()
	{
		D3DRASTER_STATUS status;
		HRESULT hr = InternalPointer->GetRasterStatus( &status );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return SlimDX::Direct3D9::RasterStatus();

		SlimDX::Direct3D9::RasterStatus result;
		result.InVBlank = status.InVBlank > 0;
		result.Scanline = status.ScanLine;
		return result;
	}

	SlimDX::Direct3D9::PresentParameters^ SwapChain::PresentParameters::get()
	{
		D3DPRESENT_PARAMETERS d3dpp;
		HRESULT hr = InternalPointer->GetPresentParameters( &d3dpp );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew SlimDX::Direct3D9::PresentParameters( d3dpp );
	}

	Result SwapChain::Present( SlimDX::Direct3D9::Present flags )
	{
		HRESULT hr = InternalPointer->Present( 0, 0, 0, 0, static_cast<DWORD>( flags ) );
		return RECORD_D3D9( hr );
	}

	Result SwapChain::Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->Present( sourcePtr, destPtr, NULL, NULL, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}

	Result SwapChain::Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->Present( sourcePtr, destPtr, static_cast<HWND>( windowOverride.ToPointer() ), NULL, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}

	Result SwapChain::Present( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride, System::Drawing::Region^ region )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };

		RGNDATA nativeRegion;
		Graphics^ graphics = Graphics::FromHwnd( windowOverride );
		int count = GetRegionData( static_cast<HRGN>( region->GetHrgn(graphics).ToPointer() ), 0, NULL );
		GetRegionData( static_cast<HRGN>( region->GetHrgn(graphics).ToPointer() ), count, &nativeRegion );
		delete graphics;
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->Present( sourcePtr, destPtr, static_cast<HWND>( windowOverride.ToPointer() ), &nativeRegion, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}
}
}
