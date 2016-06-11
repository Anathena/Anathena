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

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"

#include "RenderTargetProperties.h"
#include "FactoryD2D.h"
#include "RenderTarget.h"
#include "GdiInteropRenderTarget.h"

const IID IID_ID2D1GdiInteropRenderTarget = __uuidof(ID2D1GdiInteropRenderTarget);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	GdiInteropRenderTarget::GdiInteropRenderTarget( RenderTarget^ renderTarget )
	{
		ID2D1GdiInteropRenderTarget *rt = NULL;

		HRESULT hr = renderTarget->InternalPointer->QueryInterface( IID_ID2D1GdiInteropRenderTarget, reinterpret_cast<void**>( &rt ) );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( rt );
	}

	IntPtr GdiInteropRenderTarget::GetDC( DeviceContextInitializeMode mode )
	{
		HDC hdc;

		HRESULT hr = InternalPointer->GetDC( static_cast<D2D1_DC_INITIALIZE_MODE>( mode ), &hdc );
		if( RECORD_D2D( hr ).IsFailure )
			return IntPtr::Zero;

		return IntPtr( hdc );
	}

	Result GdiInteropRenderTarget::ReleaseDC()
	{
		HRESULT hr = InternalPointer->ReleaseDC( NULL );
		return RECORD_D2D( hr );
	}

	Result GdiInteropRenderTarget::ReleaseDC( System::Drawing::Rectangle updateRegion )
	{
		RECT rect;
		rect.left = updateRegion.Left;
		rect.right = updateRegion.Right;
		rect.top = updateRegion.Top;
		rect.bottom = updateRegion.Bottom;

		HRESULT hr = InternalPointer->ReleaseDC( &rect );
		return RECORD_D2D( hr );
	}
}
}