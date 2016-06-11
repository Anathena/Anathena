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
#pragma once

#include "../math/Vector2.h"
#include "../math/Matrix3x2.h"

#include "ResourceD2D.h"
#include "SimplifiedGeometrySink.h"
#include "TessellationSink.h"
#include "Enums.h"
#include "StrokeStyle.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct2D
	{
		public ref class Geometry : Resource
		{
			COMOBJECT(ID2D1Geometry, Geometry);

		protected:
			Geometry() { };
			
		public:
			literal float DefaultFlatteningTolerance = D2D1_DEFAULT_FLATTENING_TOLERANCE;

			static Result Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, SimplifiedGeometrySink^ geometrySink );
			static Result Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink );
			static Result Combine( Geometry^ geometry1, Geometry^ geometry2, CombineMode combineMode, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink );

			static GeometryRelation Compare( Geometry^ geometry1, Geometry^ geometry2 );
			static GeometryRelation Compare( Geometry^ geometry1, Geometry^ geometry2, Matrix3x2 transform );
			static GeometryRelation Compare( Geometry^ geometry1, Geometry^ geometry2, Matrix3x2 transform, float flatteningTolerance );

			static float ComputeArea( Geometry^ geometry );
			static float ComputeArea( Geometry^ geometry, Matrix3x2 transform );
			static float ComputeArea( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance );

			static float ComputeLength( Geometry^ geometry );
			static float ComputeLength( Geometry^ geometry, Matrix3x2 transform );
			static float ComputeLength( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance );

			static System::Drawing::PointF ComputePointAtLength( Geometry^ geometry, float length );
			static System::Drawing::PointF ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform );
			static System::Drawing::PointF ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform, float flatteningTolerance );
			static System::Drawing::PointF ComputePointAtLength( Geometry^ geometry, float length, Matrix3x2 transform, float flatteningTolerance, [Out] Vector2% unitTangentVector );

			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point );
			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point, Matrix3x2 transform );
			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::PointF point, Matrix3x2 transform, float flatteningTolerance );

			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::Point point );
			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::Point point, Matrix3x2 transform );
			static bool FillContainsPoint( Geometry^ geometry, System::Drawing::Point point, Matrix3x2 transform, float flatteningTolerance );

			static System::Drawing::RectangleF GetBounds( Geometry^ geometry );
			static System::Drawing::RectangleF GetBounds( Geometry^ geometry, Matrix3x2 transform );

			static System::Drawing::RectangleF GetWidenedBounds( Geometry^ geometry, float strokeWidth );
			static System::Drawing::RectangleF GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle );
			static System::Drawing::RectangleF GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform );
			static System::Drawing::RectangleF GetWidenedBounds( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance );

			static Result Outline( Geometry^ geometry, SimplifiedGeometrySink^ geometrySink );
			static Result Outline( Geometry^ geometry, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink );
			static Result Outline( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink );

			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::PointF point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance );

			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform );
			static bool StrokeContainsPoint( Geometry^ geometry, System::Drawing::Point point, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance );

			static Result Simplify( Geometry^ geometry, SimplificationType type, SimplifiedGeometrySink^ geometrySink );
			static Result Simplify( Geometry^ geometry, SimplificationType type, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink );
			static Result Simplify( Geometry^ geometry, SimplificationType type, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink );

			static Result Tessellate( Geometry^ geometry, TessellationSink^ tessellationSink );
			static Result Tessellate( Geometry^ geometry, Matrix3x2 transform, TessellationSink^ tessellationSink );
			static Result Tessellate( Geometry^ geometry, Matrix3x2 transform, float flatteningTolerance, TessellationSink^ tessellationSink );

			static Result Widen( Geometry^ geometry, float strokeWidth, SimplifiedGeometrySink^ geometrySink );
			static Result Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, SimplifiedGeometrySink^ geometrySink );
			static Result Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, SimplifiedGeometrySink^ geometrySink );
			static Result Widen( Geometry^ geometry, float strokeWidth, StrokeStyle^ strokeStyle, Matrix3x2 transform, float flatteningTolerance, SimplifiedGeometrySink^ geometrySink );
		};
	}
}