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

#include "WindowRenderTargetProperties.h"
#include "RenderTargetProperties.h"
#include "FactoryD2D.h"
#include "RenderTarget.h"
#include "WindowRenderTarget.h"

const IID IID_ID2D1HwndRenderTarget = __uuidof(ID2D1HwndRenderTarget);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	WindowRenderTarget::WindowRenderTarget( SlimDX::Direct2D::Factory^ factory, WindowRenderTargetProperties windowRenderTargetProperties )
	{
		Init( factory, RenderTargetProperties(), windowRenderTargetProperties );
	}

	WindowRenderTarget::WindowRenderTarget( SlimDX::Direct2D::Factory^ factory, RenderTargetProperties renderTargetProperties, WindowRenderTargetProperties windowRenderTargetProperties )
	{
		Init( factory, renderTargetProperties, windowRenderTargetProperties );
	}

	void WindowRenderTarget::Init( SlimDX::Direct2D::Factory^ factory, RenderTargetProperties renderTargetProperties, WindowRenderTargetProperties windowRenderTargetProperties )
	{
		ID2D1HwndRenderTarget *renderTarget = NULL;

		HRESULT hr = factory->InternalPointer->CreateHwndRenderTarget( reinterpret_cast<D2D1_RENDER_TARGET_PROPERTIES*>( &renderTargetProperties ),
			reinterpret_cast<D2D1_HWND_RENDER_TARGET_PROPERTIES*>( &windowRenderTargetProperties ), &renderTarget );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( renderTarget );
	}

	Result WindowRenderTarget::Resize( System::Drawing::Size size )
	{
		return RECORD_D2D( InternalPointer->Resize( reinterpret_cast<D2D1_SIZE_U*>( &size ) ) );
	}

	IntPtr WindowRenderTarget::Handle::get()
	{
		return IntPtr( InternalPointer->GetHwnd() );
	}

	bool WindowRenderTarget::IsOccluded::get()
	{
		return (InternalPointer->CheckWindowState() & D2D1_WINDOW_STATE_OCCLUDED) != 0;
	}
}
}