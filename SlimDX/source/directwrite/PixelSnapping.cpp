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

#include "PixelSnapping.h"

const IID IID_IDWritePixelSnapping = __uuidof(IDWritePixelSnapping);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	bool PixelSnapping::IsPixelSnappingDisabled(IntPtr clientDrawingContext)
	{
		BOOL val;
		if (RECORD_DW(InternalPointer->IsPixelSnappingDisabled(static_cast<void *>(clientDrawingContext), &val)).IsFailure)
		{
			val = FALSE;
		}
		return val == TRUE;
	}

	Matrix3x2 PixelSnapping::GetCurrentTransform(IntPtr clientDrawingContext)
	{
		Matrix3x2 val;
		if (RECORD_DW(InternalPointer->GetCurrentTransform(static_cast<void *>(clientDrawingContext),
				reinterpret_cast<DWRITE_MATRIX *>(&val))).IsFailure)
		{
			Matrix3x2 zero;
			val = zero;
		}
		return val;
	}

	float PixelSnapping::GetPixelsPerDip(IntPtr clientDrawingContext)
	{
		float val;
		if (RECORD_DW(InternalPointer->GetPixelsPerDip(static_cast<void *>(clientDrawingContext), &val)).IsFailure)
		{
			val = 0.0f;
		}
		return val;
	}
}
}
