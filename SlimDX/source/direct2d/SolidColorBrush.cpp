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

#include <d2d1.h>
#include <d2d1helper.h>

#include "Direct2DException.h"

#include "RenderTarget.h"
#include "SolidColorBrush.h"

const IID IID_ID2D1SolidColorBrush = __uuidof(ID2D1SolidColorBrush);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	SolidColorBrush::SolidColorBrush( RenderTarget^ renderTarget, Color4 color )
	{
		ID2D1SolidColorBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateSolidColorBrush( reinterpret_cast<D2D1_COLOR_F*>( &color ),NULL, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	SolidColorBrush::SolidColorBrush( RenderTarget^ renderTarget, Color4 color, BrushProperties properties )
	{
		ID2D1SolidColorBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateSolidColorBrush( reinterpret_cast<D2D1_COLOR_F*>( &color ),
			reinterpret_cast<D2D1_BRUSH_PROPERTIES*>( &properties ), &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	Color4 SolidColorBrush::Color::get()
	{
		D2D1_COLOR_F color = InternalPointer->GetColor();
		return Color4( color.a, color.r, color.g, color.b );
	}

	void SolidColorBrush::Color::set( Color4 value )
	{
		InternalPointer->SetColor( reinterpret_cast<D2D1_COLOR_F*>( &value ) );
	}
}
}