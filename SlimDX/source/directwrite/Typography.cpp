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

#include "Typography.h"

const IID IID_IDWriteTypography = __uuidof(IDWriteTypography);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Typography::Typography( Factory^ factory )
	{
		IDWriteTypography *typo = NULL;

		HRESULT hr = factory->InternalPointer->CreateTypography( &typo );
		if( RECORD_DW( hr ).IsFailure )
			throw gcnew DirectWriteException( Result::Last );

		Construct( typo );
	}

	Result Typography::AddFeature( FontFeature feature )
	{
		DWRITE_FONT_FEATURE ff;
		ff.nameTag = static_cast<DWRITE_FONT_FEATURE_TAG>( feature.NameTag );
		ff.parameter = feature.Value;

		HRESULT hr = InternalPointer->AddFontFeature( ff );
		return RECORD_DW( hr );
	}

	FontFeature Typography::GetFeature( int featureIndex )
	{
		FontFeature result;

		HRESULT hr = InternalPointer->GetFontFeature( featureIndex, reinterpret_cast<DWRITE_FONT_FEATURE*>( &result ) );
		RECORD_DW( hr );

		return result;
	}

	int Typography::FeatureCount::get()
	{
		return InternalPointer->GetFontFeatureCount();
	}
}
}