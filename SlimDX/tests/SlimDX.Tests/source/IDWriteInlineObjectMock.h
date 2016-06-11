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
#include <dwrite.h>

class IDWriteInlineObjectMock : public IDWriteInlineObject
{
public:
	MOCK_IUNKNOWN;

	MOCK_METHOD7_WITH_CALLTYPE( STDMETHODCALLTYPE, Draw, HRESULT(void* clientDrawingContext, IDWriteTextRenderer* renderer, FLOAT originX, FLOAT originY, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetMetrics, HRESULT(DWRITE_INLINE_OBJECT_METRICS*) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetOverhangMetrics, HRESULT(DWRITE_OVERHANG_METRICS* overhangs) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetBreakConditions, HRESULT(DWRITE_BREAK_CONDITION*, DWRITE_BREAK_CONDITION*) );
};
