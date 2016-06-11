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
#include "Geometry.h"

const IID IID_ID2D1Geometry = __uuidof(ID2D1Geometry);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	Result Geometry::Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry1->InternalPointer->CombineWithGeometry( geometry2->InternalPointer, static_cast<D2D1_COMBINE_MODE>( combineMode ),
			NULL, geometrySink->InternalPointer );

		return RECORD_D2D( hr );
	}

	Result Geometry::Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry1->InternalPointer->CombineWithGeometry( geometry2->InternalPointer, static_cast<D2D1_COMBINE_MODE>( combineMode ),
			reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), geometrySink->InternalPointer );

		return RECORD_D2D( hr );
	}

	Result Geometry::Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry1->InternalPointer->CombineWithGeometry( geometry2->InternalPointer, static_cast<D2D1_COMBINE_MODE>( combineMode ),
			reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, geometrySink->InternalPointer );

		return RECORD_D2D( hr );
	}

	GeometryRelation Geometry::Compare( Geometry^ geometry1, Geometry^ geometry2 )
	{
		D2D1_GEOMETRY_RELATION result;

		HRESULT hr = geometry1->InternalPointer->CompareWithGeometry( geometry2->InternalPointer, NULL, &result );
		RECORD_D2D( hr );

		return static_cast<GeometryRelation>( result );
	}

	GeometryRelation Geometry::Compare( Geometry^ geometry1, Geometry^ geometry2, Matrix3x2 transform )
	{
		D2D1_GEOMETRY_RELATION result;

		HRESULT hr = geometry1->InternalPointer->CompareWithGeometry( geometry2->InternalPointer, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &result );
		RECORD_D2D( hr );

		return static_cast<GeometryRelation>( result );
	}

	GeometryRelation Geometry::Compare( Geometry^ geometry1, Geometry^ geometry2, Matrix3x2 transform, float flatteningTolerance )
	{
		D2D1_GEOMETRY_RELATION result;

		HRESULT hr = geometry1->InternalPointer->CompareWithGeometry( geometry2->InternalPointer, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &result );
		RECORD_D2D( hr );

		return static_cast<GeometryRelation>( result );
	}

	float Geometry::ComputeArea( Geometry^ geometry )
	{
		float area = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeArea( NULL, &area );
		RECORD_D2D( hr );

		return area;
	}

	float Geometry::ComputeArea( Geometry^ geometry, Matrix3x2 transform )
	{
		float area = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeArea( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &area );
		RECORD_D2D( hr );

		return area;
	}

	float Geometry::ComputeArea( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance )
	{
		float area = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeArea( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &area );
		RECORD_D2D( hr );

		return area;
	}

	float Geometry::ComputeLength( Geometry^ geometry )
	{
		float length = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeLength( NULL, &length );
		RECORD_D2D( hr );

		return length;
	}

	float Geometry::ComputeLength( Geometry^ geometry, Matrix3x2 transform )
	{
		float length = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeLength( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &length );
		RECORD_D2D( hr );

		return length;
	}

	float Geometry::ComputeLength( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance )
	{
		float length = 0.0f;

		HRESULT hr = geometry->InternalPointer->ComputeLength( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &length );
		RECORD_D2D( hr );

		return length;
	}

	System::Drawing::PointF Geometry::ComputePointAtLength( Geometry^ geometry, float length )
	{
		D2D1_POINT_2F point;

		HRESULT hr = geometry->InternalPointer->ComputePointAtLength( length, NULL, &point, NULL );
		RECORD_D2D( hr );

		return System::Drawing::PointF( point.x, point.y );
	}

	System::Drawing::PointF Geometry::ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform )
	{
		D2D1_POINT_2F point;

		HRESULT hr = geometry->InternalPointer->ComputePointAtLength( length, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &point, NULL );
		RECORD_D2D( hr );

		return System::Drawing::PointF( point.x, point.y );
	}

	System::Drawing::PointF Geometry::ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform, float flatteningTolerance )
	{
		D2D1_POINT_2F point;

		HRESULT hr = geometry->InternalPointer->ComputePointAtLength( length, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &point, NULL );
		RECORD_D2D( hr );

		return System::Drawing::PointF( point.x, point.y );
	}

	System::Drawing::PointF Geometry::ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform, float flatteningTolerance, [Out] Vector2% unitTangentVector )
	{
		D2D1_POINT_2F point;
		D2D1_POINT_2F tangent;

		HRESULT hr = geometry->InternalPointer->ComputePointAtLength( length, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &point, &tangent );
		RECORD_D2D( hr );

		unitTangentVector = Vector2( tangent.x, tangent.y );
		return System::Drawing::PointF( point.x, point.y );
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::Point point )
	{
		return FillContainsPoint( geometry, CastPoint( point ) );
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::Point point, Matrix3x2 transform )
	{
		return FillContainsPoint( geometry, CastPoint( point ), transform );
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::Point point, Matrix3x2 transform, float flatteningTolerance )
	{
		return FillContainsPoint( geometry, CastPoint( point ), transform, flatteningTolerance );
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point )
	{
		BOOL result;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->FillContainsPoint( p, NULL, &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point, Matrix3x2 transform )
	{
		BOOL result;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->FillContainsPoint( p, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	bool Geometry::FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point, Matrix3x2 transform, float flatteningTolerance )
	{
		BOOL result;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->FillContainsPoint( p, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	System::Drawing::RectangleF Geometry::GetBounds( Geometry^ geometry )
	{
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetBounds( NULL, &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	System::Drawing::RectangleF Geometry::GetBounds( Geometry^ geometry, Matrix3x2 transform )
	{
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetBounds( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	System::Drawing::RectangleF Geometry::GetWidenedBounds( Geometry^ geometry, float strokeWidth )
	{
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetWidenedBounds( strokeWidth, NULL, NULL, &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	System::Drawing::RectangleF Geometry::GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetWidenedBounds( strokeWidth, ss, NULL, &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	System::Drawing::RectangleF Geometry::GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetWidenedBounds( strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	System::Drawing::RectangleF Geometry::GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_RECT_F bounds;

		HRESULT hr = geometry->InternalPointer->GetWidenedBounds( strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &bounds );
		RECORD_D2D( hr );

		return System::Drawing::RectangleF::FromLTRB( bounds.left, bounds.top, bounds.right, bounds.bottom );
	}

	Result Geometry::Outline( Geometry^ geometry, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Outline( NULL, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Outline( Geometry^ geometry, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Outline( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Outline( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Outline( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth )
	{
		return StrokeContainsPoint( geometry, CastPoint( point ), strokeWidth );
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		return StrokeContainsPoint( geometry, CastPoint( point ), strokeWidth, strokeStyle );
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform )
	{
		return StrokeContainsPoint( geometry, CastPoint( point ), strokeWidth, strokeStyle, transform );
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance )
	{
		return StrokeContainsPoint( geometry, CastPoint( point ), strokeWidth, strokeStyle, transform, flatteningTolerance );
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth )
	{
		BOOL result;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->StrokeContainsPoint( p, strokeWidth, NULL, NULL, &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		BOOL result;
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->StrokeContainsPoint( p, strokeWidth, ss, NULL, &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform )
	{
		BOOL result;
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->StrokeContainsPoint( p, strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	bool Geometry::StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance )
	{
		BOOL result;
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_POINT_2F p = D2D1::Point2F( point.X, point.Y );

		HRESULT hr = geometry->InternalPointer->StrokeContainsPoint( p, strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, &result );
		RECORD_D2D( hr );

		return result > 0;
	}

	Result Geometry::Simplify( Geometry^ geometry, SimplificationType type, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Simplify( static_cast<D2D1_GEOMETRY_SIMPLIFICATION_OPTION>( type ), NULL, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Simplify( Geometry^ geometry, SimplificationType type, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Simplify( static_cast<D2D1_GEOMETRY_SIMPLIFICATION_OPTION>( type ),
			reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Simplify( Geometry^ geometry, SimplificationType type, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Simplify( static_cast<D2D1_GEOMETRY_SIMPLIFICATION_OPTION>( type ),
			reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Tessellate( Geometry^ geometry, TessellationSink^ tessellationSink )
	{
		HRESULT hr = geometry->InternalPointer->Tessellate( NULL, tessellationSink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Tessellate( Geometry^ geometry, Matrix3x2 transform, TessellationSink^ tessellationSink )
	{
		HRESULT hr = geometry->InternalPointer->Tessellate( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), tessellationSink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Tessellate( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance, TessellationSink^ tessellationSink )
	{
		HRESULT hr = geometry->InternalPointer->Tessellate( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, tessellationSink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Widen( Geometry^ geometry, float strokeWidth, SimplifiedGeometrySink^ geometrySink )
	{
		HRESULT hr = geometry->InternalPointer->Widen( strokeWidth, NULL, NULL, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, SimplifiedGeometrySink^ geometrySink )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		HRESULT hr = geometry->InternalPointer->Widen( strokeWidth, ss, NULL, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		HRESULT hr = geometry->InternalPointer->Widen( strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}

	Result Geometry::Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink )
	{
		ID2D1StrokeStyle *ss = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		HRESULT hr = geometry->InternalPointer->Widen( strokeWidth, ss, reinterpret_cast<D2D1_MATRIX_3X2_F*>( &transform ), flatteningTolerance, geometrySink->InternalPointer );
		return RECORD_D2D( hr );
	}
}
}