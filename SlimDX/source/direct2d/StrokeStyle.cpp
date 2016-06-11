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

#include "StrokeStyle.h"

const IID IID_ID2D1StrokeStyle = __uuidof(ID2D1StrokeStyle);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	StrokeStyle::StrokeStyle( SlimDX::Direct2D::Factory^ factory )
	{
		ID2D1StrokeStyle *strokeStyle = NULL;

		HRESULT hr = factory->InternalPointer->CreateStrokeStyle( D2D1::StrokeStyleProperties(), NULL, 0, &strokeStyle );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( strokeStyle );
	}

	StrokeStyle::StrokeStyle( SlimDX::Direct2D::Factory^ factory, StrokeStyleProperties properties )
	{
		ID2D1StrokeStyle *strokeStyle = NULL;

		HRESULT hr = factory->InternalPointer->CreateStrokeStyle( reinterpret_cast<D2D1_STROKE_STYLE_PROPERTIES*>( &properties ), 
			NULL, 0, &strokeStyle );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( strokeStyle );
	}

	StrokeStyle::StrokeStyle( SlimDX::Direct2D::Factory^ factory, StrokeStyleProperties properties, array<float>^ dashes )
	{
		ID2D1StrokeStyle *strokeStyle = NULL;
		pin_ptr<float> pinnedDashes = &dashes[0];

		HRESULT hr = factory->InternalPointer->CreateStrokeStyle( reinterpret_cast<D2D1_STROKE_STYLE_PROPERTIES*>( &properties ), 
			pinnedDashes, dashes->Length, &strokeStyle );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( strokeStyle );
	}

	array<float>^ StrokeStyle::GetDashes()
	{
		array<float>^ results = gcnew array<float>( InternalPointer->GetDashesCount() );
		pin_ptr<float> pinnedResults = &results[0];

		InternalPointer->GetDashes( pinnedResults, InternalPointer->GetDashesCount() );

		return results;
	}

	CapStyle StrokeStyle::StartCap::get()
	{
		return static_cast<CapStyle>( InternalPointer->GetStartCap() );
	}

	CapStyle StrokeStyle::EndCap::get()
	{
		return static_cast<CapStyle>( InternalPointer->GetEndCap() );
	}

	CapStyle StrokeStyle::DashCap::get()
	{
		return static_cast<CapStyle>( InternalPointer->GetDashCap() );
	}

	SlimDX::Direct2D::LineJoin StrokeStyle::LineJoin::get()
	{
		return static_cast<SlimDX::Direct2D::LineJoin>( InternalPointer->GetLineJoin() );
	}

	float StrokeStyle::MiterLimit::get()
	{
		return InternalPointer->GetMiterLimit();
	}

	SlimDX::Direct2D::DashStyle StrokeStyle::DashStyle::get()
	{
		return static_cast<SlimDX::Direct2D::DashStyle>( InternalPointer->GetDashStyle() );
	}

	float StrokeStyle::DashOffset::get()
	{
		return InternalPointer->GetDashOffset();
	}
}
}