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

#include <dxgi.h>

#include "../DataStream.h"
#include "../DataRectangle.h"

#include "DXGIException.h"

#include "SwapChainDxgi.h"
#include "SurfaceDxgi.h"
#include "SurfaceDescription.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	SurfaceDescription Surface::Description::get()
	{
		DXGI_SURFACE_DESC nativeDescription;
		if (RECORD_DXGI( InternalPointer->GetDesc( &nativeDescription ) ).IsFailure)
			return SurfaceDescription();

		return SurfaceDescription( nativeDescription );
	}
	
	DataRectangle^ Surface::Map( MapFlags flags )
	{
		DXGI_MAPPED_RECT mappedRect;
		if( RECORD_DXGI( InternalPointer->Map( &mappedRect, static_cast<UINT>( flags ) ) ).IsFailure )
			return nullptr;
		
		int size = Description.Width * Description.Height * Utilities::SizeOfFormatElement( static_cast<DXGI_FORMAT>( Description.Format ) );
		bool canRead = ( static_cast<UINT>( flags ) & DXGI_MAP_READ ) != 0;
		bool canWrite = ( static_cast<UINT>( flags ) & DXGI_MAP_WRITE ) != 0;
		return gcnew DataRectangle( mappedRect.Pitch, gcnew DataStream( mappedRect.pBits, size, canRead, canWrite, false ) );
	}
	
	Result Surface::Unmap()
	{
		return RECORD_DXGI( InternalPointer->Unmap() );
	}

	Surface^ Surface::FromSwapChain( SlimDX::DXGI::SwapChain^ swapChain, int index )
	{
		IDXGISurface *buffer;
		GUID guid = Utilities::GetNativeGuidForType( Surface::typeid );
		
		if (RECORD_DXGI( swapChain->InternalPointer->GetBuffer(index, guid, reinterpret_cast<void**>(&buffer)) ).IsFailure)
			return nullptr;

		return Surface::FromPointer( buffer );
	}
}
}
