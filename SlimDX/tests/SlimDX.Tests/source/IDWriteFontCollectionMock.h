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

struct IDWriteFontCollectionMock : public IDWriteFontCollection
{
	MOCK_IUNKNOWN;

	STDMETHOD_(UINT32, GetFontFamilyCount)() { return UINT32(0); }
	STDMETHOD(GetFontFamily)(UINT32 index, IDWriteFontFamily** fontFamily) { return E_NOTIMPL; }
	STDMETHOD(FindFamilyName)(WCHAR const* familyName, UINT32* index, BOOL* exists) { return E_NOTIMPL; }
	STDMETHOD(GetFontFromFontFace)(IDWriteFontFace* fontFace, IDWriteFont** font) { return E_NOTIMPL; }
};
