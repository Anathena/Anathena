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
#include "LinearGradientBrush.h"

const IID IID_ID2D1LinearGradientBrush = __uuidof(ID2D1LinearGradientBrush);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	LinearGradientBrush::LinearGradientBrush( RenderTarget^ renderTarget, GradientStopCollection^ gradientStops, LinearGradientBrushProperties linearGradientBrushProperties )
	{
		ID2D1LinearGradientBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateLinearGradientBrush( reinterpret_cast<D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES*>( &linearGradientBrushProperties ), 
			NULL, gradientStops->InternalPointer, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	LinearGradientBrush::LinearGradientBrush( RenderTarget^ renderTarget, GradientStopCollection^ gradientStops, LinearGradientBrushProperties linearGradientBrushProperties, BrushProperties properties )
	{
		ID2D1LinearGradientBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateLinearGradientBrush( reinterpret_cast<D2D1_LINEAR_GRADIENT_BRUSH_PROPERTIES*>( &linearGradientBrushProperties ), 
			reinterpret_cast<D2D1_BRUSH_PROPERTIES*>( &properties ), gradientStops->InternalPointer, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	System::Drawing::PointF LinearGradientBrush::StartPoint::get()
	{
		D2D1_POINT_2F result = InternalPointer->GetStartPoint();

		return System::Drawing::PointF( result.x, result.y );
	}

	void LinearGradientBrush::StartPoint::set( System::Drawing::PointF value )
	{
		D2D1_POINT_2F point = D2D1::Point2F( value.X, value.Y );

		InternalPointer->SetStartPoint( point );
	}

	System::Drawing::PointF LinearGradientBrush::EndPoint::get()
	{
		D2D1_POINT_2F result = InternalPointer->GetEndPoint();

		return System::Drawing::PointF( result.x, result.y );
	}

	void LinearGradientBrush::EndPoint::set( System::Drawing::PointF value )
	{
		D2D1_POINT_2F point = D2D1::Point2F( value.X, value.Y );

		InternalPointer->SetEndPoint( point );
	}

	GradientStopCollection^ LinearGradientBrush::GradientStops::get()
	{
		ID2D1GradientStopCollection *stops = NULL;
		InternalPointer->GetGradientStopCollection( &stops );

		return GradientStopCollection::FromPointer( stops );
	}
}
}