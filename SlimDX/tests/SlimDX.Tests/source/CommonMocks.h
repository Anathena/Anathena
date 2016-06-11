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

#define MOCK_IUNKNOWN \
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, QueryInterface, HRESULT( REFIID, void**) ); \
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, AddRef, ULONG() ); \
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, Release, ULONG() )

#define MOCK_IDXGIOBJECT \
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, SetPrivateData, HRESULT( REFGUID, UINT, const void* ) ); \
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, SetPrivateDataInterface, HRESULT( REFGUID, const IUnknown* ) ); \
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, GetPrivateData, HRESULT( REFGUID, UINT*, void* ) ); \
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetParent, HRESULT( REFIID, void** ) )

#define MOCK_IDXGIDEVICESUBOBJECT \
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetDevice, HRESULT( REFIID, void** ) )

#define MOCK_IDXGIRESOURCE \
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetSharedHandle, HRESULT( HANDLE* ) ); \
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetUsage, HRESULT( DXGI_USAGE* ) ); \
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetEvictionPriority, HRESULT( UINT ) ); \
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetEvictionPriority, HRESULT( UINT* ) )