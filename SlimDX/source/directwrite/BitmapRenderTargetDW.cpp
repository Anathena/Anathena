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

#include "../Utilities.h"
#include "../stack_array.h"

#include "DirectWriteException.h"
#include "BitmapRenderTargetDW.h"

const IID IID_IDWriteBitmapRenderTarget = __uuidof(IDWriteBitmapRenderTarget);

using namespace System;
using namespace System::Drawing;
using namespace SlimDX::Direct2D;

namespace SlimDX
{
namespace DirectWrite
{
	BitmapRenderTarget::BitmapRenderTarget(GdiInterop ^interop, IntPtr hdc, int width, int height)
	{
		IDWriteBitmapRenderTarget *pointer;

		HRESULT hr = interop->InternalPointer->CreateBitmapRenderTarget(reinterpret_cast<HDC>(hdc.ToPointer()), width, height, &pointer);
		if (RECORD_DW(hr).IsFailure)
			throw gcnew DirectWriteException(Result::Last);

		Construct(pointer);
	}

	Result BitmapRenderTarget::DrawGlyphRun(float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun^ glyphRun, RenderingParameters^ renderingParameters, Color4 color)
	{
		System::Drawing::Rectangle unused;
		return DrawGlyphRun(baselineOriginX, baselineOriginY, measuringMode, glyphRun, renderingParameters, color, unused);
	}

	Result BitmapRenderTarget::DrawGlyphRun(float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun^ glyphRun, RenderingParameters^ renderingParameters, Color4 color, [Out] System::Drawing::Rectangle% blackBoxBounds)
	{
		int indexCount = (glyphRun->GlyphIndices != nullptr) ? glyphRun->GlyphIndices->Length : 0;
		int advanceCount = (glyphRun->GlyphAdvances != nullptr) ? glyphRun->GlyphAdvances->Length : 0;
		int offsetCount = (glyphRun->GlyphOffsets != nullptr) ? glyphRun->GlyphOffsets->Length : 0;

		stack_array<UINT16> indices = stackalloc(UINT16, indexCount);
		stack_array<FLOAT> advances = stackalloc(FLOAT, advanceCount);
		stack_array<DWRITE_GLYPH_OFFSET> offsets = stackalloc(DWRITE_GLYPH_OFFSET, offsetCount);

		DWRITE_GLYPH_RUN glyphs = glyphRun->ToUnmanaged(indices, advances, offsets);
		Color c = color.ToColor();
		RECT blackBoxRect;

		HRESULT hr = InternalPointer->DrawGlyphRun(baselineOriginX, baselineOriginY, static_cast<DWRITE_MEASURING_MODE>(measuringMode), &glyphs,
			renderingParameters->InternalPointer, RGB(c.R, c.G, c.B), &blackBoxRect);
		
		blackBoxBounds = Utilities::ConvertRect(blackBoxRect);
		return RECORD_DW(hr);
	}

	Result BitmapRenderTarget::Resize(int width, int height)
	{
		HRESULT hr = InternalPointer->Resize(width, height);
		return RECORD_DW(hr);
	}

	Matrix3x2 BitmapRenderTarget::Transform::get()
	{
		Matrix3x2 result;
		
		HRESULT hr = InternalPointer->GetCurrentTransform(reinterpret_cast<DWRITE_MATRIX*>(&result));
		RECORD_DW(hr);

		return result;
	}

	void BitmapRenderTarget::Transform::set(Matrix3x2 value)
	{
		HRESULT hr = InternalPointer->SetCurrentTransform(reinterpret_cast<DWRITE_MATRIX*>(&value));
		RECORD_DW(hr);
	}

	float BitmapRenderTarget::PixelsPerDip::get()
	{
		return InternalPointer->GetPixelsPerDip();
	}

	void BitmapRenderTarget::PixelsPerDip::set(float value)
	{
		InternalPointer->SetPixelsPerDip(value);
	}

	System::Drawing::Size BitmapRenderTarget::Size::get()
	{
		System::Drawing::Size size;

		HRESULT hr = InternalPointer->GetSize(reinterpret_cast<SIZE*>(&size));
		RECORD_DW(hr);

		return size;
	}

	IntPtr BitmapRenderTarget::MemoryDC::get()
	{
		return IntPtr(InternalPointer->GetMemoryDC());
	}
}
}
