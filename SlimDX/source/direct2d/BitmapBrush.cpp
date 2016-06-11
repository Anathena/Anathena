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
#include "BitmapBrush.h"

const IID IID_ID2D1BitmapBrush = __uuidof(ID2D1BitmapBrush);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	BitmapBrush::BitmapBrush( RenderTarget^ renderTarget, SlimDX::Direct2D::Bitmap^ bitmap )
	{
		ID2D1BitmapBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmapBrush( bitmap->InternalPointer, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	BitmapBrush::BitmapBrush( RenderTarget^ renderTarget, SlimDX::Direct2D::Bitmap^ bitmap, BitmapBrushProperties bitmapBrushProperties )
	{
		ID2D1BitmapBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmapBrush( bitmap->InternalPointer, reinterpret_cast<D2D1_BITMAP_BRUSH_PROPERTIES*>( &bitmapBrushProperties ),
			NULL, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	BitmapBrush::BitmapBrush( RenderTarget^ renderTarget, SlimDX::Direct2D::Bitmap^ bitmap, BitmapBrushProperties bitmapBrushProperties, BrushProperties brushProperties )
	{
		ID2D1BitmapBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmapBrush( bitmap->InternalPointer, reinterpret_cast<D2D1_BITMAP_BRUSH_PROPERTIES*>( &bitmapBrushProperties ),
			reinterpret_cast<D2D1_BRUSH_PROPERTIES*>( &brushProperties ), &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	SlimDX::Direct2D::Bitmap^ BitmapBrush::Bitmap::get()
	{
		ID2D1Bitmap *bitmap = NULL;
		InternalPointer->GetBitmap( &bitmap );

		return SlimDX::Direct2D::Bitmap::FromPointer( bitmap );
	}

	void BitmapBrush::Bitmap::set( SlimDX::Direct2D::Bitmap^ value )
	{
		InternalPointer->SetBitmap( value->InternalPointer );
	}

	ExtendMode BitmapBrush::HorizontalExtendMode::get()
	{
		return static_cast<ExtendMode>( InternalPointer->GetExtendModeX() );
	}

	void BitmapBrush::HorizontalExtendMode::set( ExtendMode value )
	{
		InternalPointer->SetExtendModeX( static_cast<D2D1_EXTEND_MODE>( value ) );
	}

	ExtendMode BitmapBrush::VerticalExtendMode::get()
	{
		return static_cast<ExtendMode>( InternalPointer->GetExtendModeY() );
	}

	void BitmapBrush::VerticalExtendMode::set( ExtendMode value )
	{
		InternalPointer->SetExtendModeY( static_cast<D2D1_EXTEND_MODE>( value ) );
	}

	SlimDX::Direct2D::InterpolationMode BitmapBrush::InterpolationMode::get()
	{
		return static_cast<SlimDX::Direct2D::InterpolationMode>( InternalPointer->GetInterpolationMode() );
	}

	void BitmapBrush::InterpolationMode::set( SlimDX::Direct2D::InterpolationMode value )
	{
		InternalPointer->SetInterpolationMode( static_cast<D2D1_BITMAP_INTERPOLATION_MODE>( value ) );
	}
}
}