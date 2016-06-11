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

#include "../dxgi/SurfaceDxgi.h"
#include "../math/Color4.h"
#include "../math/Matrix3x2.h"
#include "../directwrite/TextLayout.h"
#include "../directwrite/RenderingParameters.h"

#include "ResourceD2D.h"
#include "StrokeStyle.h"
#include "Brush.h"
#include "PixelFormat.h"
#include "Ellipse.h"
#include "RoundedRectangle.h"
#include "StateBlockD2D.h"
#include "LayerParameters.h"
#include "RenderTargetProperties.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct2D
	{
		ref class Bitmap;
		ref class Layer;
		ref class BitmapRenderTarget;
		ref class Mesh;

		public ref class RenderTarget : Resource
		{
			COMOBJECT(ID2D1RenderTarget, RenderTarget);

		protected:
			RenderTarget() { }

		public:
			static RenderTarget^ FromDXGI( SlimDX::Direct2D::Factory^ factory, SlimDX::DXGI::Surface^ surface, RenderTargetProperties properties );

			void BeginDraw();
			Result EndDraw();
			Result EndDraw( [Out] System::Int64% tag1, [Out] System::Int64% tag2 );
			Result Flush();
			Result Flush( [Out] System::Int64% tag1, [Out] System::Int64% tag2 );

			BitmapRenderTarget^ CreateCompatibleRenderTarget();
			BitmapRenderTarget^ CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize );
			BitmapRenderTarget^ CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize );
			BitmapRenderTarget^ CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize, PixelFormat desiredPixelFormat );
			BitmapRenderTarget^ CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize, PixelFormat desiredPixelFormat, CompatibleRenderTargetOptions options );

			void Clear();
			void Clear( Color4 color );

			void SetTags( System::Int64 tag1, System::Int64 tag2 );
			void GetTags( [Out] System::Int64% tag1, [Out] System::Int64% tag2 );

			void DrawLine( Brush^ brush, System::Drawing::PointF point1, System::Drawing::PointF point2 );
			void DrawLine( Brush^ brush, System::Drawing::PointF point1, System::Drawing::PointF point2, float strokeWidth );
			void DrawLine( Brush^ brush, System::Drawing::PointF point1, System::Drawing::PointF point2, float strokeWidth, StrokeStyle^ strokeStyle );

			void DrawLine( Brush^ brush, float x1, float y1, float x2, float y2 );
			void DrawLine( Brush^ brush, float x1, float y1, float x2, float y2, float strokeWidth );
			void DrawLine( Brush^ brush, float x1, float y1, float x2, float y2, float strokeWidth, StrokeStyle^ style );

			void FillRectangle( Brush^ brush, System::Drawing::Rectangle rectangle );
			void FillRectangle( Brush^ brush, System::Drawing::RectangleF rectangle );

			void FillRoundedRectangle( Brush^ brush, RoundedRectangle rectangle );
			void FillEllipse( Brush^ brush, Ellipse ellipse );

			void DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle );
			void DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle, float strokeWidth );
			void DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle, float strokeWidth, StrokeStyle^ strokeStyle );

			void DrawRectangle( Brush^ brush, System::Drawing::RectangleF rectangle );
			void DrawRectangle( Brush^ brush, System::Drawing::RectangleF rectangle, float strokeWidth );
			void DrawRectangle( Brush^ brush, System::Drawing::RectangleF rectangle, float strokeWidth, StrokeStyle^ strokeStyle );

			void DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle );
			void DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle, float strokeWidth );
			void DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle, float strokeWidth, StrokeStyle^ strokeStyle );

			void DrawEllipse( Brush^ brush, Ellipse ellipse );
			void DrawEllipse( Brush^ brush, Ellipse ellipse, float strokeWidth );
			void DrawEllipse( Brush^ brush, Ellipse ellipse, float strokeWidth, StrokeStyle^ strokeStyle );

			void DrawTextLayout( System::Drawing::Point origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush );
			void DrawTextLayout( System::Drawing::PointF origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush );
			void DrawTextLayout( System::Drawing::Point origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush, DrawTextOptions options );
			void DrawTextLayout( System::Drawing::PointF origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush, DrawTextOptions options );

#pragma push_macro("DrawText")
#undef DrawText
			void DrawText( System::String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::Rectangle layoutRectangle, Brush^ defaultBrush );
			void DrawText( System::String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::RectangleF layoutRectangle, Brush^ defaultBrush );
			void DrawText( System::String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::Rectangle layoutRectangle, Brush^ defaultBrush, DrawTextOptions options, SlimDX::DirectWrite::MeasuringMode measuringMethod );
			void DrawText( System::String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::RectangleF layoutRectangle, Brush^ defaultBrush, DrawTextOptions options, SlimDX::DirectWrite::MeasuringMode measuringMethod );
#pragma pop_macro("DrawText")

			void DrawBitmap( Bitmap^ bitmap );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity, InterpolationMode interpolationMode );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity, InterpolationMode interpolationMode, System::Drawing::RectangleF sourceRectangle );

			void DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity, InterpolationMode interpolationMode );
			void DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity, InterpolationMode interpolationMode, System::Drawing::Rectangle sourceRectangle );

			void DrawGeometry( Geometry^ geometry, Brush^ brush );
			void DrawGeometry( Geometry^ geometry, Brush^ brush, float strokeWidth );
			void DrawGeometry( Geometry^ geometry, Brush^ brush, float strokeWidth, StrokeStyle^ strokeStyle );

			void FillGeometry( Geometry^ geometry, Brush^ brush );
			void FillGeometry( Geometry^ geometry, Brush^ brush, Brush^ opacityBrush );

			void FillOpacityMask( Bitmap^ mask, Brush^ brush, OpacityMaskContent content, System::Nullable<System::Drawing::RectangleF> sourceRectangle, System::Nullable<System::Drawing::RectangleF> destinationRectangle );

			void FillMesh( Mesh^ mesh, Brush^ brush );

			void PopAxisAlignedClip();
			void PushAxisAlignedClip( System::Drawing::RectangleF clippingArea, AntialiasMode antialiasMode );
			void PushAxisAlignedClip( System::Drawing::Rectangle clippingArea, AntialiasMode antialiasMode );

			Bitmap^ CreateSharedBitmap( System::Guid guid, DataStream^ data );

			void PopLayer();
			void PushLayer( Layer^ layer, LayerParameters^ parameters );

			void SaveDrawingState( StateBlock^ stateBlock );
			void RestoreDrawingState( StateBlock^ stateBlock );

			property SlimDX::DirectWrite::RenderingParameters^ TextRenderingParameters
			{
				SlimDX::DirectWrite::RenderingParameters^ get();
				void set( SlimDX::DirectWrite::RenderingParameters^ value );
			}

			property PixelFormat PixelFormat
			{
				SlimDX::Direct2D::PixelFormat get();
			}

			property System::Drawing::SizeF Size
			{
				System::Drawing::SizeF get();
			}

			property System::Drawing::Size PixelSize
			{
				System::Drawing::Size get();
			}

			property Matrix3x2 Transform
			{
				Matrix3x2 get();
				void set( Matrix3x2 value );
			}

			property System::Drawing::SizeF DotsPerInch
			{
				System::Drawing::SizeF get();
				void set( System::Drawing::SizeF value );
			}

			property AntialiasMode AntialiasMode
			{
				SlimDX::Direct2D::AntialiasMode get();
				void set( SlimDX::Direct2D::AntialiasMode value );
			}

			property TextAntialiasMode TextAntialiasMode
			{
				SlimDX::Direct2D::TextAntialiasMode get();
				void set( SlimDX::Direct2D::TextAntialiasMode value );
			}
		};
	}
}