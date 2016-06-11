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
#include "IClientDrawingEffect.h"
#include "InlineObject.h"
#include "ITextRenderer.h"

const IID IID_IDWriteInlineObject = __uuidof(IDWriteInlineObject);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Result InlineObject::Draw(IntPtr clientDrawingContext, ITextRenderer ^renderer,
		float originX, float originY, bool isSideways, bool isRightToLeft,
		IClientDrawingEffect ^clientDrawingEffect)
	{
		IUnknown *nativeClientDrawingEffect = clientDrawingEffect == nullptr ? 0 : reinterpret_cast<IUnknown*>(clientDrawingEffect->ComPointer.ToPointer());
		void *nativeClientDrawingContext = static_cast<void *>(clientDrawingContext);

		ITextRendererShim *shim = ITextRendererShim::CreateInstance(renderer);

		HRESULT hr = InternalPointer->Draw(nativeClientDrawingContext, shim,
			originX, originY, isSideways ? TRUE : FALSE, isRightToLeft ? TRUE : FALSE, nativeClientDrawingEffect);

		shim->Release();
		return RECORD_DW(hr);
	}

	Result InlineObject::GetBreakConditions([Out] BreakCondition %before, [Out] BreakCondition %after)
	{
		DWRITE_BREAK_CONDITION beforeNative, afterNative;
		Result result = RECORD_DW(InternalPointer->GetBreakConditions(&beforeNative, &afterNative));
		before = static_cast<BreakCondition>(beforeNative);
		after = static_cast<BreakCondition>(afterNative);
		return result;
	}

	InlineObjectMetrics InlineObject::Metrics::get()
	{
		DWRITE_INLINE_OBJECT_METRICS metrics;
		if (RECORD_DW(InternalPointer->GetMetrics(&metrics)).IsFailure)
		{
			DWRITE_INLINE_OBJECT_METRICS zero = { 0 };
			metrics = zero;
		}
		return InlineObjectMetrics(metrics.width, metrics.height, metrics.baseline, metrics.supportsSideways == TRUE);
	}
	SlimDX::DirectWrite::OverhangMetrics InlineObject::OverhangMetrics::get()
	{
		DWRITE_OVERHANG_METRICS metrics;
		if (RECORD_DW(InternalPointer->GetOverhangMetrics(&metrics)).IsFailure)
		{
			DWRITE_OVERHANG_METRICS zero = { 0 };
			metrics = zero;
		}
		return SlimDX::DirectWrite::OverhangMetrics(metrics.left, metrics.top, metrics.right, metrics.bottom);
	}
}
}
