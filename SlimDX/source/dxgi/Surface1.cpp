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

#include "../Utilities.h"

#include "DXGIException.h"

#include "Surface1.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{
	Surface1::Surface1( IComObject^ surface )
	{
		IDXGISurface1* result = 0;

		IUnknown *ptr = reinterpret_cast<IUnknown*>(surface->ComPointer.ToPointer());
		if( RECORD_DXGI( ptr->QueryInterface( IID_IDXGISurface1, reinterpret_cast<void**>( &result ) ) ).IsFailure )
			throw gcnew DXGIException( Result::Last );

		ComObject^ other = ObjectTable::Find(IntPtr(result));
		Construct(result, other, other == nullptr);
	}

	IntPtr Surface1::GetDC(bool discard)
	{
		HDC hdc;

		HRESULT hr = InternalPointer->GetDC(discard, &hdc);
		if (RECORD_DXGI(hr).IsFailure)
			return IntPtr::Zero;

		return IntPtr(hdc);
	}

	Result Surface1::ReleaseDC(System::Drawing::Rectangle dirtyRegion)
	{
		RECT rect;
		Utilities::ConvertRect(dirtyRegion, rect);

		HRESULT hr = InternalPointer->ReleaseDC(&rect);
		return RECORD_DXGI(hr);
	}

	Result Surface1::ReleaseDC()
	{
		HRESULT hr = InternalPointer->ReleaseDC(NULL);
		return RECORD_DXGI(hr);
	}
}
}
