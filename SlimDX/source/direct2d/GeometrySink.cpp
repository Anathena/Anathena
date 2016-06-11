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
#include "GeometrySink.h"

const IID IID_ID2D1GeometrySink = __uuidof(ID2D1GeometrySink);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	void GeometrySink::AddLine( System::Drawing::Point point )
	{
		AddLine( CastPoint( point ) );
	}

	void GeometrySink::AddLine( System::Drawing::PointF point )
	{
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );
		InternalPointer->AddLine( p );
	}

	void GeometrySink::AddBezier( BezierSegment bezier )
	{
		InternalPointer->AddBezier( reinterpret_cast<D2D1_BEZIER_SEGMENT*>( &bezier ) );
	}

	void GeometrySink::AddQuadraticBezier( QuadraticBezierSegment bezier )
	{
		InternalPointer->AddQuadraticBezier( reinterpret_cast<D2D1_QUADRATIC_BEZIER_SEGMENT*>( &bezier ) );
	}

	void GeometrySink::AddQuadraticBeziers( array<QuadraticBezierSegment>^ beziers )
	{
		pin_ptr<QuadraticBezierSegment> pinnedBeziers = &beziers[0];
		InternalPointer->AddQuadraticBeziers( reinterpret_cast<D2D1_QUADRATIC_BEZIER_SEGMENT*>( pinnedBeziers ), beziers->Length );
	}

	void GeometrySink::AddArc( ArcSegment arc )
	{
		InternalPointer->AddArc( reinterpret_cast<D2D1_ARC_SEGMENT*>( &arc ) );
	}
}
}