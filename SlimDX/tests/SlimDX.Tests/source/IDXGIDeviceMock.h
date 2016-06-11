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
#pragma once

#include "CommonMocks.h"

struct IDXGIDeviceMock : IDXGIDevice {
	MOCK_IUNKNOWN;
	MOCK_IDXGIOBJECT;

	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetAdapter, HRESULT( IDXGIAdapter** ) );
	MOCK_METHOD5_WITH_CALLTYPE( STDMETHODCALLTYPE, CreateSurface, HRESULT( const DXGI_SURFACE_DESC *, UINT, DXGI_USAGE, const DXGI_SHARED_RESOURCE*, IDXGISurface** ) );
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, QueryResourceResidency, HRESULT( IUnknown* const*, DXGI_RESIDENCY*, UINT ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetGPUThreadPriority, HRESULT( INT ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetGPUThreadPriority, HRESULT( INT* ) );
};