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
#include "DXGIException.h"

#include "Factory.h"
#include "FrameStatistics.h"
#include "ModeDescription.h"
#include "Output.h"
#include "SwapChainDxgi.h"
#include "SwapChainDescription.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Globalization;

namespace SlimDX
{
namespace DXGI
{ 	
	SwapChain::SwapChain( Factory^ factory, ComObject^ device, SwapChainDescription description )
	{
		if( factory == nullptr )
			throw gcnew ArgumentNullException( "factory" );
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		
		IDXGISwapChain* swapChain = 0;
		DXGI_SWAP_CHAIN_DESC nativeDescription = description.CreateNativeVersion();
		RECORD_DXGI( factory->InternalPointer->CreateSwapChain( device->UnknownPointer, &nativeDescription, &swapChain ) );
		if( Result::Last.IsFailure )
			throw gcnew DXGIException( Result::Last );
		
		Construct( swapChain );
	}

	SwapChain::~SwapChain()
	{
		if (InternalPointer)
		{
			BOOL fullscreen = FALSE;
			HRESULT hr = InternalPointer->GetFullscreenState(&fullscreen, NULL);

			if (SUCCEEDED(hr) && fullscreen)
				throw gcnew InvalidOperationException("You may not release a swap chain in full-screen mode because doing so may create thread contention (which will cause DXGI to raise a non-continuable exception). Before releasing a swap chain, first switch to windowed mode.");
		}
	}
	
	SwapChainDescription SwapChain::Description::get()
	{
		DXGI_SWAP_CHAIN_DESC description;
		if (RECORD_DXGI( InternalPointer->GetDesc( &description ) ).IsFailure)
			return SwapChainDescription();

		return SwapChainDescription( description );
	}
	
	DXGI::FrameStatistics SwapChain::FrameStatistics::get()
	{
		DXGI_FRAME_STATISTICS stats;
		if (RECORD_DXGI( InternalPointer->GetFrameStatistics( &stats ) ).IsFailure)
			return DXGI::FrameStatistics();

		return DXGI::FrameStatistics( stats );
	}
	
	int SwapChain::PresentCount::get()
	{
		UINT result = 0;
		RECORD_DXGI( InternalPointer->GetLastPresentCount( &result ) );
		
		return result;
	}
	
	Output^ SwapChain::ContainingOutput::get()
	{
		IDXGIOutput* output = 0;
		if( RECORD_DXGI( InternalPointer->GetContainingOutput( &output ) ).IsFailure )
			return nullptr;
			
		return Output::FromPointer( output, this );
	}

	bool SwapChain::IsFullScreen::get()
	{
		BOOL result = FALSE;
		if (RECORD_DXGI(InternalPointer->GetFullscreenState(&result, NULL)).IsFailure)
			return false;

		return result > 0;
	}

	void SwapChain::IsFullScreen::set(bool value)
	{
		RECORD_DXGI(InternalPointer->SetFullscreenState(value, NULL));
	}

	Result SwapChain::GetFullScreenState( bool% isFullScreen, Output^% target )
	{
		BOOL result = false;
		IDXGIOutput* output = 0;
		if( RECORD_DXGI( InternalPointer->GetFullscreenState( &result, &output ) ).IsSuccess )
		{
			isFullScreen = result ? true : false;
			if( output == 0 )
				target = nullptr;
			else
				target = Output::FromPointer( output, this );
		}
		
		return Result::Last;
	}
	
	Result SwapChain::SetFullScreenState( bool isFullScreen, Output^ target )
	{
		IDXGIOutput* output = target == nullptr ? 0 : target->InternalPointer;
		return RECORD_DXGI( InternalPointer->SetFullscreenState( isFullScreen, output ) );
	}
	
	Result SwapChain::ResizeBuffers( int count, int width, int height, SlimDX::DXGI::Format format, SwapChainFlags flags )
	{
		return RECORD_DXGI( InternalPointer->ResizeBuffers( count, width, height, static_cast<DXGI_FORMAT>( format ), static_cast<UINT>( flags ) ) );
	}
	
	Result SwapChain::ResizeTarget( ModeDescription description )
	{
		return RECORD_DXGI( InternalPointer->ResizeTarget( reinterpret_cast<DXGI_MODE_DESC*>( &description ) ) );
	}

	Result SwapChain::Present( int syncInterval, PresentFlags flags )
	{
		return RECORD_DXGI( InternalPointer->Present( syncInterval, static_cast<UINT>( flags ) ) );
	}
}
}
