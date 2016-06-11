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

#include "ITextRenderer.h"

const IID IID_IDWriteTextRenderer = __uuidof(IDWriteTextRenderer);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	ITextRendererShim *ITextRendererShim::CreateInstance(ITextRenderer ^wrappedInterface)
	{
		if (wrappedInterface == nullptr)
			return NULL;

		return new ITextRendererShim(wrappedInterface);
	}

	ITextRendererShim::ITextRendererShim(ITextRenderer ^wrappedInterface)
		: m_WrappedInterface(wrappedInterface),
		m_refCount(1)
	{
	}

	HRESULT ITextRendererShim::QueryInterface(const IID &iid, LPVOID *ppv)
	{
		if (iid == IID_IDWriteTextRenderer)
		{
			AddRef();
			*reinterpret_cast<IDWriteTextRenderer**>(ppv) = this;
			return S_OK;
		}

		return E_NOTIMPL;
	}

	ULONG ITextRendererShim::AddRef()
	{
		return ++m_refCount;
	}

	ULONG ITextRendererShim::Release()
	{
		if (--m_refCount == 0)
		{
			delete this;
		}
		return m_refCount;
	}

	HRESULT ITextRendererShim::IsPixelSnappingDisabled(void* clientDrawingContext, BOOL* isDisabled)
	{
		try
		{
			*isDisabled = m_WrappedInterface->IsPixelSnappingDisabled(IntPtr(clientDrawingContext));
			return S_OK;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::GetCurrentTransform(void* clientDrawingContext, DWRITE_MATRIX* transform)
	{
		try
		{
			Matrix3x2 result = m_WrappedInterface->GetCurrentTransform(IntPtr(clientDrawingContext));
			memcpy(transform, &result, sizeof(DWRITE_MATRIX));
			return S_OK;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::GetPixelsPerDip(void* clientDrawingContext, FLOAT* pixelsPerDip)
	{
		try
		{
			*pixelsPerDip = m_WrappedInterface->GetPixelsPerDip(IntPtr(clientDrawingContext));
			return S_OK;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::DrawGlyphRun(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_MEASURING_MODE measuringMode, DWRITE_GLYPH_RUN const* glyphRun,
		DWRITE_GLYPH_RUN_DESCRIPTION const* glyphRunDescription, IUnknown* clientDrawingEffect)
	{
		try
		{
			Result result = m_WrappedInterface->DrawGlyphRun(IntPtr(clientDrawingContext), baselineOriginX, baselineOriginY, static_cast<MeasuringMode>(measuringMode),
				gcnew GlyphRun(*glyphRun), gcnew GlyphRunDescription(*glyphRunDescription), IntPtr(clientDrawingEffect));

			return result.Code;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::DrawUnderline(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_UNDERLINE const* underline, IUnknown* clientDrawingEffect)
	{
		try
		{
			Result result = m_WrappedInterface->DrawUnderline(IntPtr(clientDrawingContext), baselineOriginX, baselineOriginY, gcnew Underline(*underline), IntPtr(clientDrawingEffect));
			return result.Code;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::DrawStrikethrough(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_STRIKETHROUGH const* strikethrough, IUnknown* clientDrawingEffect)
	{
		try
		{
			Result result = m_WrappedInterface->DrawStrikethrough(IntPtr(clientDrawingContext), baselineOriginX, baselineOriginY, gcnew Strikethrough(*strikethrough), IntPtr(clientDrawingEffect));
			return result.Code;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}

	HRESULT ITextRendererShim::DrawInlineObject(void* clientDrawingContext, FLOAT originX, FLOAT originY, IDWriteInlineObject* inlineObject, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect)
	{
		try
		{
			Result result = m_WrappedInterface->DrawInlineObject(IntPtr(clientDrawingContext), originX, originY, InlineObject::FromPointer(inlineObject), isSideways != 0, isRightToLeft != 0, IntPtr(clientDrawingEffect));
			return result.Code;
		}
		catch(SlimDXException^ e)
		{
			return e->ResultCode.Code;
		}
		catch(...)
		{
			return E_FAIL;
		}
	}
}
}
