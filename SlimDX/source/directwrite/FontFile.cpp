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

#include "../DataStream.h"

#include "DirectWriteException.h"
#include "FontFile.h"

const IID IID_IDWriteFontFile = __uuidof(IDWriteFontFile);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	FontFileAnalysis FontFile::Analyze()
	{
		BOOL isSupported;
		DWRITE_FONT_FILE_TYPE fileType;
		DWRITE_FONT_FACE_TYPE faceType;
		UINT32 faceCount;

		HRESULT hr = InternalPointer->Analyze(&isSupported, &fileType, &faceType, &faceCount);
		RECORD_DW(hr);

		return FontFileAnalysis(isSupported > 0, static_cast<FontFileType>(fileType), static_cast<FontFaceType>(faceType), faceCount);
	}

	DataStream^ FontFile::GetReferenceKey()
	{
		const void *data;
		UINT32 size;

		HRESULT hr = InternalPointer->GetReferenceKey(&data, &size);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return gcnew DataStream(data, size, true, false);
	}

	FontFileLoader^ FontFile::Loader::get()
	{
		IDWriteFontFileLoader *loader;

		HRESULT hr = InternalPointer->GetLoader(&loader);
		if (RECORD_DW(hr).IsFailure)
		{
			return nullptr;
		}

		return FontFileLoader::FromPointer(loader);
	}
}
}