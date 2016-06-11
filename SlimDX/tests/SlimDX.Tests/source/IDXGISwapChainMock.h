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

struct IDXGISwapChainMock : IDXGISwapChain {
	MOCK_IUNKNOWN;
	MOCK_IDXGIOBJECT;
	MOCK_IDXGIDEVICESUBOBJECT;

	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, Present, HRESULT( UINT, UINT ) );   
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, GetBuffer, HRESULT( UINT, REFIID, void**) );   
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, SetFullscreenState, HRESULT( BOOL, IDXGIOutput* ) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFullscreenState, HRESULT( BOOL*, IDXGIOutput** ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetDesc, HRESULT( DXGI_SWAP_CHAIN_DESC* ) );
	MOCK_METHOD5_WITH_CALLTYPE( STDMETHODCALLTYPE, ResizeBuffers, HRESULT( UINT, UINT, UINT, DXGI_FORMAT, UINT ) );   
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, ResizeTarget, HRESULT( const DXGI_MODE_DESC* ) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetContainingOutput, HRESULT( IDXGIOutput** ) );   
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFrameStatistics, HRESULT( DXGI_FRAME_STATISTICS* ) );    
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetLastPresentCount, HRESULT( UINT* ) );
};