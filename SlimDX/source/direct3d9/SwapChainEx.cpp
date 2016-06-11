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

#include "Direct3D9Exception.h"

#include "SwapChainEx.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	SwapChainEx::SwapChainEx( SwapChain^ source )
	{
		IDirect3DSwapChain9Ex* result = 0;

		IUnknown *ptr = reinterpret_cast<IUnknown*>(source->ComPointer.ToPointer());
		if( RECORD_D3D9( ptr->QueryInterface( IID_IDirect3DSwapChain9Ex, reinterpret_cast<void**>( &result ) ) ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		ComObject^ other = ObjectTable::Find(IntPtr(result));
		Construct(result, other, other == nullptr);
	}

	DisplayModeEx SwapChainEx::GetDisplayModeEx()
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		HRESULT hr = InternalPointer->GetDisplayModeEx( &nativeMode, NULL );
		RECORD_D3D9( hr );

		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		return mode;
	}

	DisplayModeEx SwapChainEx::GetDisplayModeEx( [Out] DisplayRotation% rotation )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		D3DDISPLAYROTATION nativeRotation = D3DDISPLAYROTATION_IDENTITY;

		HRESULT hr = InternalPointer->GetDisplayModeEx( &nativeMode, &nativeRotation );
		RECORD_D3D9( hr );

		rotation = static_cast<DisplayRotation>( nativeRotation );
		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		return mode;
	}

	Direct3D9::PresentStatistics SwapChainEx::PresentStatistics::get()
	{
		D3DPRESENTSTATS stats;
		if (RECORD_D3D9( InternalPointer->GetPresentStats( &stats ) ).IsFailure )
			return Direct3D9::PresentStatistics();

		return Direct3D9::PresentStatistics( stats );
	}
	
	int SwapChainEx::LastPresentCount::get()
	{
		UINT result = 0;
		RECORD_D3D9( InternalPointer->GetLastPresentCount( &result ) );
		
		return result;
	}
}
}