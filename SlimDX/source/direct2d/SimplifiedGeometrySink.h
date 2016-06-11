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

#include "../ComObject.h"

#include "Enums.h"
#include "BezierSegment.h"

namespace SlimDX
{
	namespace Direct2D
	{
		public ref class SimplifiedGeometrySink : ComObject
		{
			COMOBJECT(ID2D1SimplifiedGeometrySink, SimplifiedGeometrySink);

		protected:
			SimplifiedGeometrySink() { };
			
		public:
			void BeginFigure( System::Drawing::Point startPoint, FigureBegin style );
			void BeginFigure( System::Drawing::PointF startPoint, FigureBegin style );
			void EndFigure( FigureEnd style );

			Result Close();

			void SetFillMode( FillMode fillMode );
			void SetSegmentFlags( PathSegment vertexFlags );

			void AddLines( array<System::Drawing::PointF>^ points );
			void AddBeziers( array<BezierSegment>^ beziers );
		};
	}
}