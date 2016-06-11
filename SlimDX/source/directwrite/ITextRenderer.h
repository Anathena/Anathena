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

extern const IID IID_IDWriteTextRenderer;

#include "../ComObject.h"
#include "../math/Matrix3x2.h"

#include "InlineObject.h"
#include "Enums.h"
#include "GlyphRunDW.h"
#include "GlyphRunDescription.h"
#include "Underline.h"
#include "Strikethrough.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		public interface struct ITextRenderer
		{
			Matrix3x2 GetCurrentTransform(System::IntPtr drawingContext);
			float GetPixelsPerDip(System::IntPtr drawingContext);
			bool IsPixelSnappingDisabled(System::IntPtr drawingContext);

			Result DrawGlyphRun(System::IntPtr drawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun^ glyphRun, GlyphRunDescription^ glyphRunDescription, System::IntPtr clientDrawingEffect);
			Result DrawInlineObject(System::IntPtr drawingContext, float baselineOriginX, float baselineOriginY, InlineObject^ inlineObject, bool isSideways, bool isRightToLeft, System::IntPtr clientDrawingEffect);
			Result DrawStrikethrough(System::IntPtr drawingContext, float baselineOriginX, float baselineOriginY, Strikethrough^ strikethrough, System::IntPtr clientDrawingEffect);
			Result DrawUnderline(System::IntPtr drawingContext, float baselineOriginX, float baselineOriginY, Underline^ underline, System::IntPtr clientDrawingEffect);
		};

		class ITextRendererShim : public IDWriteTextRenderer
		{
		public:
			static ITextRendererShim *CreateInstance(ITextRenderer ^wrappedInterface);

			STDMETHOD(QueryInterface)(REFIID riid, void **ppvObject);
			STDMETHOD_(ULONG, AddRef)();
			STDMETHOD_(ULONG, Release)();

			STDMETHOD(IsPixelSnappingDisabled)(
				void* clientDrawingContext,
				BOOL* isDisabled
				);

			STDMETHOD(GetCurrentTransform)(
				void* clientDrawingContext,
				DWRITE_MATRIX* transform
				);

			STDMETHOD(GetPixelsPerDip)(
				void* clientDrawingContext,
				FLOAT* pixelsPerDip
				);

			STDMETHOD(DrawGlyphRun)(
				void* clientDrawingContext,
				FLOAT baselineOriginX,
				FLOAT baselineOriginY,
				DWRITE_MEASURING_MODE measuringMode,
				DWRITE_GLYPH_RUN const* glyphRun,
				DWRITE_GLYPH_RUN_DESCRIPTION const* glyphRunDescription,
				IUnknown* clientDrawingEffect
				);

			STDMETHOD(DrawUnderline)(
				void* clientDrawingContext,
				FLOAT baselineOriginX,
				FLOAT baselineOriginY,
				DWRITE_UNDERLINE const* underline,
				IUnknown* clientDrawingEffect
				);

			STDMETHOD(DrawStrikethrough)(
				void* clientDrawingContext,
				FLOAT baselineOriginX,
				FLOAT baselineOriginY,
				DWRITE_STRIKETHROUGH const* strikethrough,
				IUnknown* clientDrawingEffect
				);

			STDMETHOD(DrawInlineObject)(
				void* clientDrawingContext,
				FLOAT originX,
				FLOAT originY,
				IDWriteInlineObject* inlineObject,
				BOOL isSideways,
				BOOL isRightToLeft,
				IUnknown* clientDrawingEffect
				);

		private:
			ITextRendererShim(ITextRenderer ^wrappedInterface);

			int m_refCount;
			gcroot<ITextRenderer ^> m_WrappedInterface;
		};
	}
}
