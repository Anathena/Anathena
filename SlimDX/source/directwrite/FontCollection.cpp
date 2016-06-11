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

#include "DirectWriteException.h"

#include "FontCollection.h"
#include "FontFamily.h"
#include "FontFace.h"
#include "Font.h"

const IID IID_IDWriteFontCollection = __uuidof(IDWriteFontCollection);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	int FontCollection::FontFamilyCount::get()
	{
		return InternalPointer->GetFontFamilyCount();
	}

	FontFamily^ FontCollection::default::get(int index)
	{
		IDWriteFontFamily* fontFamily = NULL;
		HRESULT hr = InternalPointer->GetFontFamily( index, &fontFamily );
		RECORD_DW(hr);

		return FontFamily::FromPointer( fontFamily );
	}

	int FontCollection::FindFamilyName( System::String^ familyName, [Out] bool% exists )
	{
		BOOL existsNative = FALSE;
		UINT32 index = 0;
		pin_ptr<const wchar_t> nameChars = PtrToStringChars( familyName );

		HRESULT hr = InternalPointer->FindFamilyName( nameChars, &index, &existsNative );
		exists = existsNative == TRUE;
		RECORD_DW(hr);

		return index;
	}
	
	int FontCollection::FindFamilyName( System::String^ familyName )
	{
		bool exists;
		return FindFamilyName( familyName, exists );
	}

	Font^ FontCollection::GetFontFromFontFace( FontFace^ fontFace )
	{
		IDWriteFont* font = NULL;

		HRESULT hr = InternalPointer->GetFontFromFontFace( fontFace->InternalPointer, &font );
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return Font::FromPointer(font);
	}
}
}