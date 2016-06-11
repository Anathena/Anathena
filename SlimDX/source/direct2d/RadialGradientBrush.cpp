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
#include "RadialGradientBrush.h"

const IID IID_ID2D1RadialGradientBrush = __uuidof(ID2D1RadialGradientBrush);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	RadialGradientBrush::RadialGradientBrush( RenderTarget^ renderTarget, GradientStopCollection^ gradientStops, RadialGradientBrushProperties radialGradientBrushProperties )
	{
		ID2D1RadialGradientBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateRadialGradientBrush( reinterpret_cast<D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES*>( &radialGradientBrushProperties ), 
			NULL, gradientStops->InternalPointer, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	RadialGradientBrush::RadialGradientBrush( RenderTarget^ renderTarget, GradientStopCollection^ gradientStops, RadialGradientBrushProperties radialGradientBrushProperties, BrushProperties properties )
	{
		ID2D1RadialGradientBrush *brush = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateRadialGradientBrush( reinterpret_cast<D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES*>( &radialGradientBrushProperties ), 
			reinterpret_cast<D2D1_BRUSH_PROPERTIES*>( &properties ), gradientStops->InternalPointer, &brush );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( brush );
	}

	System::Drawing::PointF RadialGradientBrush::CenterPoint::get()
	{
		D2D1_POINT_2F result = InternalPointer->GetCenter();

		return System::Drawing::PointF( result.x, result.y );
	}

	void RadialGradientBrush::CenterPoint::set( System::Drawing::PointF value )
	{
		D2D1_POINT_2F point = D2D1::Point2F( value.X, value.Y );

		InternalPointer->SetCenter( point );
	}

	System::Drawing::PointF RadialGradientBrush::GradientOriginOffset::get()
	{
		D2D1_POINT_2F result = InternalPointer->GetGradientOriginOffset();

		return System::Drawing::PointF( result.x, result.y );
	}

	void RadialGradientBrush::GradientOriginOffset::set( System::Drawing::PointF value )
	{
		D2D1_POINT_2F point = D2D1::Point2F( value.X, value.Y );

		InternalPointer->SetGradientOriginOffset( point );
	}

	float RadialGradientBrush::HorizontalRadius::get()
	{
		return InternalPointer->GetRadiusX();
	}

	void RadialGradientBrush::HorizontalRadius::set( float value )
	{
		InternalPointer->SetRadiusX( value );
	}

	float RadialGradientBrush::VerticalRadius::get()
	{
		return InternalPointer->GetRadiusY();
	}

	void RadialGradientBrush::VerticalRadius::set( float value )
	{
		InternalPointer->SetRadiusY( value );
	}

	GradientStopCollection^ RadialGradientBrush::GradientStops::get()
	{
		ID2D1GradientStopCollection *stops = NULL;
		InternalPointer->GetGradientStopCollection( &stops );

		return GradientStopCollection::FromPointer( stops );
	}
}
}