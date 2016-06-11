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

#include "DirectWriteException.h"
#include "GlyphRunAnalysis.h"
#include "RenderingParameters.h"

const IID IID_IDWriteGlyphRunAnalysis = __uuidof(IDWriteGlyphRunAnalysis);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Result GlyphRunAnalysis::CreateAlphaTexture(TextureType textureType, System::Drawing::Rectangle bounds, array<System::Byte>^ alphaValues)
	{
		RECT rect;
		Utilities::ConvertRect(bounds, rect);
		pin_ptr<BYTE> pinnedBytes = &alphaValues[0];

		HRESULT hr = InternalPointer->CreateAlphaTexture(static_cast<DWRITE_TEXTURE_TYPE>(textureType), &rect, pinnedBytes, alphaValues->Length);
		return RECORD_DW(hr);
	}

	Result GlyphRunAnalysis::GetAlphaBlendParameters(RenderingParameters^ renderingParameters, [Out] float% gamma, [Out] float% enhancedContrast, [Out] float% clearTypeLevel)
	{
		FLOAT blendGamma;
		FLOAT blendEnhancedContrast;
		FLOAT blendClearTypeLevel;

		HRESULT hr = InternalPointer->GetAlphaBlendParams(renderingParameters->InternalPointer, &blendGamma, &blendEnhancedContrast, &blendClearTypeLevel);
		if (RECORD_DW(hr).IsFailure)
		{
			gamma = 0.0f;
			enhancedContrast = 0.0f;
			clearTypeLevel = 0.0f;
		}
		else
		{
			gamma = blendGamma;
			enhancedContrast = blendEnhancedContrast;
			clearTypeLevel = blendClearTypeLevel;
		}

		return Result::Last;
	}

	System::Drawing::Rectangle GlyphRunAnalysis::GetAlphaTextureBounds(TextureType textureType)
	{
		RECT bounds;

		HRESULT hr = InternalPointer->GetAlphaTextureBounds(static_cast<DWRITE_TEXTURE_TYPE>(textureType), &bounds);
		RECORD_DW(hr);

		return Utilities::ConvertRect(bounds);
	}
}
}
