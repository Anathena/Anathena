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

#include "ConversionMethods.h"
#include "SimplifiedGeometrySink.h"

const IID IID_ID2D1SimplifiedGeometrySink = __uuidof(ID2D1SimplifiedGeometrySink);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	void SimplifiedGeometrySink::BeginFigure( System::Drawing::Point startPoint, FigureBegin style )
	{
		D2D1_POINT_2F point = D2D1::Point2F( static_cast<FLOAT>( startPoint.X ), static_cast<FLOAT>( startPoint.Y ) );

		InternalPointer->BeginFigure( point, static_cast<D2D1_FIGURE_BEGIN>( style ) );
	}

	void SimplifiedGeometrySink::BeginFigure( System::Drawing::PointF startPoint, FigureBegin style )
	{
		D2D1_POINT_2F point = D2D1::Point2F( startPoint.X, startPoint.Y );

		InternalPointer->BeginFigure( point, static_cast<D2D1_FIGURE_BEGIN>( style ) );
	}

	void SimplifiedGeometrySink::EndFigure( FigureEnd style )
	{
		InternalPointer->EndFigure( static_cast<D2D1_FIGURE_END>( style ) );
	}

	Result SimplifiedGeometrySink::Close()
	{
		return RECORD_D2D( InternalPointer->Close() );
	}

	void SimplifiedGeometrySink::SetFillMode( FillMode fillMode )
	{
		InternalPointer->SetFillMode( static_cast<D2D1_FILL_MODE>( fillMode ) );
	}

	void SimplifiedGeometrySink::SetSegmentFlags( PathSegment vertexFlags )
	{
		InternalPointer->SetSegmentFlags( static_cast<D2D1_PATH_SEGMENT>( vertexFlags ) );
	}

	void SimplifiedGeometrySink::AddLines( array<System::Drawing::PointF>^ points )
	{
		pin_ptr<System::Drawing::PointF> pinnedPoints = &points[0];

		InternalPointer->AddLines( reinterpret_cast<D2D1_POINT_2F*>( pinnedPoints ), points->Length );
	}

	void SimplifiedGeometrySink::AddBeziers( array<BezierSegment>^ beziers )
	{
		pin_ptr<BezierSegment> pinnedBeziers = &beziers[0];

		InternalPointer->AddBeziers( reinterpret_cast<D2D1_BEZIER_SEGMENT*>( pinnedBeziers ), beziers->Length );
	}
}
}