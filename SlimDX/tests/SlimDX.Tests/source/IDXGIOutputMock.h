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

struct IDXGIOutputMock : IDXGIOutput {
	MOCK_IUNKNOWN;
	MOCK_IDXGIOBJECT;

	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetDesc, HRESULT( DXGI_OUTPUT_DESC* ) );
	MOCK_METHOD4_WITH_CALLTYPE( STDMETHODCALLTYPE, GetDisplayModeList, HRESULT( DXGI_FORMAT, UINT, UINT*, DXGI_MODE_DESC* ) );
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, FindClosestMatchingMode, HRESULT( const DXGI_MODE_DESC*, DXGI_MODE_DESC*, IUnknown* ) );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, WaitForVBlank, HRESULT( void ) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, TakeOwnership, HRESULT( IUnknown*, BOOL ) );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, ReleaseOwnership, void( void ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetGammaControlCapabilities, HRESULT( DXGI_GAMMA_CONTROL_CAPABILITIES* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetGammaControl, HRESULT( const DXGI_GAMMA_CONTROL* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetGammaControl, HRESULT( DXGI_GAMMA_CONTROL* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetDisplaySurface, HRESULT( IDXGISurface* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetDisplaySurfaceData, HRESULT( IDXGISurface* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFrameStatistics, HRESULT( DXGI_FRAME_STATISTICS* ) );
};
