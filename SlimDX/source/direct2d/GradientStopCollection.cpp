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

#include "GradientStopCollection.h"

const IID IID_ID2D1GradientStopCollection = __uuidof(ID2D1GradientStopCollection);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	GradientStopCollection::GradientStopCollection( RenderTarget^ renderTarget, array<GradientStop>^ stops )
	{
		ID2D1GradientStopCollection *collection = NULL;
		pin_ptr<GradientStop> pinnedStops = &stops[0];

		HRESULT hr = renderTarget->InternalPointer->CreateGradientStopCollection( reinterpret_cast<D2D1_GRADIENT_STOP*>( pinnedStops ), stops->Length, &collection );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( collection );
	}

	GradientStopCollection::GradientStopCollection( RenderTarget^ renderTarget, array<GradientStop>^ stops, Gamma gamma, SlimDX::Direct2D::ExtendMode extendMode )
	{
		ID2D1GradientStopCollection *collection = NULL;
		pin_ptr<GradientStop> pinnedStops = &stops[0];

		HRESULT hr = renderTarget->InternalPointer->CreateGradientStopCollection( reinterpret_cast<D2D1_GRADIENT_STOP*>( pinnedStops ), stops->Length,
			static_cast<D2D1_GAMMA>( gamma ), static_cast<D2D1_EXTEND_MODE>( extendMode ), &collection );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( collection );
	}

	array<GradientStop>^ GradientStopCollection::GetStops()
	{
		int count = InternalPointer->GetGradientStopCount();
		array<GradientStop>^ results = gcnew array<GradientStop>( count );
		pin_ptr<GradientStop> pinnedResults = &results[0];

		InternalPointer->GetGradientStops( reinterpret_cast<D2D1_GRADIENT_STOP*>( pinnedResults ), count );

		return results;
	}

	Gamma GradientStopCollection::InterpolationGamma::get()
	{
		return static_cast<Gamma>( InternalPointer->GetColorInterpolationGamma() );
	}

	SlimDX::Direct2D::ExtendMode GradientStopCollection::ExtendMode::get()
	{
		return static_cast<SlimDX::Direct2D::ExtendMode>( InternalPointer->GetExtendMode() );
	}
}
}