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

#include "FontFamily.h"
#include "LocalizedStrings.h"
#include "Font.h"

const IID IID_IDWriteFontFamily = __uuidof(IDWriteFontFamily);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Font^ FontFamily::GetFirstMatchingFont(FontWeight weight, FontStretch stretch, FontStyle style)
	{
		IDWriteFont *font;

		HRESULT hr = InternalPointer->GetFirstMatchingFont(static_cast<DWRITE_FONT_WEIGHT>(weight), static_cast<DWRITE_FONT_STRETCH>(stretch), 
			static_cast<DWRITE_FONT_STYLE>(style), &font);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return Font::FromPointer(font);
	}

	FontList^ FontFamily::GetMatchingFonts(FontWeight weight, FontStretch stretch, FontStyle style)
	{
		IDWriteFontList *list;

		HRESULT hr = InternalPointer->GetMatchingFonts(static_cast<DWRITE_FONT_WEIGHT>(weight), static_cast<DWRITE_FONT_STRETCH>(stretch), 
			static_cast<DWRITE_FONT_STYLE>(style), &list);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return FontList::FromPointer(list);
	}

	LocalizedStrings^ FontFamily::FamilyNames::get()
	{
		IDWriteLocalizedStrings* strings;

		HRESULT hr = InternalPointer->GetFamilyNames(&strings);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return LocalizedStrings::FromPointer(strings);
	}
}
}