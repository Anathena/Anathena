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
#include <vcclr.h>
#include <dxgi.h>

#include "../DataStream.h"

#include "Direct2DException.h"

#include "RenderTarget.h"
#include "ConversionMethods.h"
#include "Bitmap.h"
#include "Layer.h"
#include "BitmapRenderTarget.h"
#include "MeshD2D.h"

const IID IID_ID2D1RenderTarget = __uuidof(ID2D1RenderTarget);

using namespace System;
using namespace System::Drawing;

namespace SlimDX
{
namespace Direct2D
{
	RenderTarget^ RenderTarget::FromDXGI( SlimDX::Direct2D::Factory^ factory, SlimDX::DXGI::Surface^ surface, RenderTargetProperties properties )
	{
		ID2D1RenderTarget *target;

		HRESULT hr = factory->InternalPointer->CreateDxgiSurfaceRenderTarget( surface->InternalPointer, reinterpret_cast<D2D1_RENDER_TARGET_PROPERTIES*>( &properties ), &target );
		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return RenderTarget::FromPointer( target );
	}

	void RenderTarget::BeginDraw()
	{
		InternalPointer->BeginDraw();
	}

	Result RenderTarget::EndDraw()
	{
		return RECORD_D2D( InternalPointer->EndDraw() );
	}

	Result RenderTarget::EndDraw( [Out] Int64% tag1, [Out] Int64% tag2 )
	{
		pin_ptr<Int64> pt1 = &tag1;
		pin_ptr<Int64> pt2 = &tag2;

		HRESULT hr = InternalPointer->EndDraw( reinterpret_cast<D2D1_TAG*>( pt1 ), reinterpret_cast<D2D1_TAG*>( pt2 ) );
		return RECORD_D2D( hr );
	}

	Result RenderTarget::Flush()
	{
		return RECORD_D2D( InternalPointer->Flush() );
	}

	Result RenderTarget::Flush( [Out] Int64% tag1, [Out] Int64% tag2 )
	{
		pin_ptr<Int64> pt1 = &tag1;
		pin_ptr<Int64> pt2 = &tag2;

		HRESULT hr = InternalPointer->Flush( reinterpret_cast<D2D1_TAG*>( pt1 ), reinterpret_cast<D2D1_TAG*>( pt2 ) );
		return RECORD_D2D( hr );
	}

	void RenderTarget::SetTags( Int64 tag1, Int64 tag2 )
	{
		InternalPointer->SetTags( tag1, tag2 );
	}

	void RenderTarget::GetTags( [Out] Int64% tag1, [Out] Int64% tag2 )
	{
		pin_ptr<Int64> pt1 = &tag1;
		pin_ptr<Int64> pt2 = &tag2;

		InternalPointer->GetTags( reinterpret_cast<D2D1_TAG*>( pt1 ), reinterpret_cast<D2D1_TAG*>( pt2 ) );
	}

	BitmapRenderTarget^ RenderTarget::CreateCompatibleRenderTarget()
	{
		ID2D1BitmapRenderTarget *result = NULL;

		HRESULT hr = InternalPointer->CreateCompatibleRenderTarget( &result );

		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return BitmapRenderTarget::FromPointer( result );
	}

	BitmapRenderTarget^ RenderTarget::CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize )
	{
		ID2D1BitmapRenderTarget *result = NULL;

		HRESULT hr = InternalPointer->CreateCompatibleRenderTarget( D2D1::SizeF( desiredSize.Width, desiredSize.Height ), &result );

		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return BitmapRenderTarget::FromPointer( result );
	}

	BitmapRenderTarget^ RenderTarget::CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize )
	{
		ID2D1BitmapRenderTarget *result = NULL;

		HRESULT hr = InternalPointer->CreateCompatibleRenderTarget( D2D1::SizeF( desiredSize.Width, desiredSize.Height ), D2D1::SizeU( desiredPixelSize.Width, desiredPixelSize.Height ), &result );

		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return BitmapRenderTarget::FromPointer( result );
	}

	BitmapRenderTarget^ RenderTarget::CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize, SlimDX::Direct2D::PixelFormat desiredPixelFormat )
	{
		ID2D1BitmapRenderTarget *result = NULL;

		HRESULT hr = InternalPointer->CreateCompatibleRenderTarget( D2D1::SizeF( desiredSize.Width, desiredSize.Height ), D2D1::SizeU( desiredPixelSize.Width, desiredPixelSize.Height ),
			D2D1::PixelFormat( static_cast<DXGI_FORMAT>( desiredPixelFormat.Format ), static_cast<D2D1_ALPHA_MODE>( desiredPixelFormat.AlphaMode ) ), &result );

		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return BitmapRenderTarget::FromPointer( result );
	}

	BitmapRenderTarget^ RenderTarget::CreateCompatibleRenderTarget( System::Drawing::SizeF desiredSize, System::Drawing::Size desiredPixelSize, SlimDX::Direct2D::PixelFormat desiredPixelFormat, CompatibleRenderTargetOptions options )
	{
		ID2D1BitmapRenderTarget *result = NULL;

		HRESULT hr = InternalPointer->CreateCompatibleRenderTarget( D2D1::SizeF( desiredSize.Width, desiredSize.Height ), D2D1::SizeU( desiredPixelSize.Width, desiredPixelSize.Height ),
			D2D1::PixelFormat( static_cast<DXGI_FORMAT>( desiredPixelFormat.Format ), static_cast<D2D1_ALPHA_MODE>( desiredPixelFormat.AlphaMode ) ), 
			static_cast<D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS>( options ), &result );

		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return BitmapRenderTarget::FromPointer( result );
	}

	void RenderTarget::Clear()
	{
		InternalPointer->Clear( NULL );
	}

	void RenderTarget::Clear( Color4 color )
	{
		InternalPointer->Clear( reinterpret_cast<D2D1_COLOR_F*>( &color ) );
	}

	void RenderTarget::DrawLine( Brush^ brush, PointF point1, PointF point2 )
	{
		D2D1_POINT_2F point1_n = D2D1::Point2F( point1.X, point1.Y );
		D2D1_POINT_2F point2_n = D2D1::Point2F( point2.X, point2.Y );

		InternalPointer->DrawLine( point1_n, point2_n, brush->InternalPointer );
	}

	void RenderTarget::DrawLine( Brush^ brush, PointF point1, PointF point2, float strokeWidth )
	{
		D2D1_POINT_2F point1_n = D2D1::Point2F( point1.X, point1.Y );
		D2D1_POINT_2F point2_n = D2D1::Point2F( point2.X, point2.Y );

		InternalPointer->DrawLine( point1_n, point2_n, brush->InternalPointer, strokeWidth );
	}

	void RenderTarget::DrawLine( Brush^ brush, PointF point1, PointF point2, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *style = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_POINT_2F point1_n = D2D1::Point2F( point1.X, point1.Y );
		D2D1_POINT_2F point2_n = D2D1::Point2F( point2.X, point2.Y );

		InternalPointer->DrawLine( point1_n, point2_n, brush->InternalPointer, strokeWidth, style );
	}

	void RenderTarget::DrawLine( Brush^ brush, float x1, float y1, float x2, float y2 )
	{
		DrawLine( brush, PointF( x1, y1 ), PointF( x2, y2 ) );
	}

	void RenderTarget::DrawLine( Brush^ brush, float x1, float y1, float x2, float y2, float strokeWidth )
	{
		DrawLine( brush, PointF( x1, y1 ), PointF( x2, y2 ), strokeWidth );
	}

	void RenderTarget::DrawLine( Brush^ brush, float x1, float y1, float x2, float y2, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		DrawLine( brush, PointF( x1, y1 ), PointF( x2, y2 ), strokeWidth, strokeStyle );
	}

	void RenderTarget::FillRectangle( Brush^ brush, System::Drawing::Rectangle rectangle )
	{
		FillRectangle( brush, RectangleF( static_cast<FLOAT>( rectangle.X ), static_cast<FLOAT>( rectangle.Y ), 
			static_cast<FLOAT>( rectangle.Width ), static_cast<FLOAT>( rectangle.Height ) ) );
	}

	void RenderTarget::FillRectangle( Brush^ brush, RectangleF rectangle )
	{
		D2D1_RECT_F rect = D2D1::RectF( rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom );
		InternalPointer->FillRectangle( rect, brush->InternalPointer );
	}

	void RenderTarget::FillRoundedRectangle( Brush^ brush, RoundedRectangle rectangle )
	{
		InternalPointer->FillRoundedRectangle( reinterpret_cast<D2D1_ROUNDED_RECT*>( &rectangle ), brush->InternalPointer );
	}

	void RenderTarget::FillEllipse( Brush^ brush, Ellipse ellipse )
	{
		InternalPointer->FillEllipse( reinterpret_cast<D2D1_ELLIPSE*>( &ellipse ), brush->InternalPointer );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle )
	{
		DrawRectangle( brush, CastRectangle( rectangle ) );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle, float strokeWidth )
	{
		DrawRectangle( brush, CastRectangle( rectangle ), strokeWidth );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, System::Drawing::Rectangle rectangle, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		DrawRectangle( brush, CastRectangle( rectangle ), strokeWidth, strokeStyle );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, RectangleF rectangle )
	{
		D2D1_RECT_F rect = D2D1::RectF( rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom );
		InternalPointer->DrawRectangle( rect, brush->InternalPointer );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, RectangleF rectangle, float strokeWidth )
	{
		D2D1_RECT_F rect = D2D1::RectF( rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom );
		InternalPointer->DrawRectangle( rect, brush->InternalPointer, strokeWidth );
	}

	void RenderTarget::DrawRectangle( Brush^ brush, RectangleF rectangle, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *style = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;
		D2D1_RECT_F rect = D2D1::RectF( rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom );

		InternalPointer->DrawRectangle( rect, brush->InternalPointer, strokeWidth, style );
	}

	void RenderTarget::DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle )
	{
		InternalPointer->DrawRoundedRectangle( reinterpret_cast<D2D1_ROUNDED_RECT*>( &rectangle ), brush->InternalPointer );
	}

	void RenderTarget::DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle, float strokeWidth )
	{
		InternalPointer->DrawRoundedRectangle( reinterpret_cast<D2D1_ROUNDED_RECT*>( &rectangle ), brush->InternalPointer, strokeWidth );
	}

	void RenderTarget::DrawRoundedRectangle( Brush^ brush, RoundedRectangle rectangle, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *style = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		InternalPointer->DrawRoundedRectangle( reinterpret_cast<D2D1_ROUNDED_RECT*>( &rectangle ), brush->InternalPointer, strokeWidth, style );
	}

	void RenderTarget::DrawEllipse( Brush^ brush, Ellipse ellipse )
	{
		InternalPointer->DrawEllipse( reinterpret_cast<D2D1_ELLIPSE*>( &ellipse ), brush->InternalPointer );
	}

	void RenderTarget::DrawEllipse( Brush^ brush, Ellipse ellipse, float strokeWidth )
	{
		InternalPointer->DrawEllipse( reinterpret_cast<D2D1_ELLIPSE*>( &ellipse ), brush->InternalPointer, strokeWidth );
	}

	void RenderTarget::DrawEllipse( Brush^ brush, Ellipse ellipse, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *style = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		InternalPointer->DrawEllipse( reinterpret_cast<D2D1_ELLIPSE*>( &ellipse ), brush->InternalPointer, strokeWidth, style );
	}

	void RenderTarget::DrawTextLayout( Point origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush )
	{
		DrawTextLayout( origin, textLayout, defaultBrush, DrawTextOptions::None );
	}

	void RenderTarget::DrawTextLayout( Point origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush, DrawTextOptions options )
	{
		D2D1_POINT_2F po = D2D1::Point2F( static_cast<FLOAT>( origin.X ), static_cast<FLOAT>( origin.Y ) );

		InternalPointer->DrawTextLayout( po, textLayout->InternalPointer, defaultBrush->InternalPointer, static_cast<D2D1_DRAW_TEXT_OPTIONS>( options ) );
	}

	void RenderTarget::DrawTextLayout( PointF origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush )
	{
		DrawTextLayout( origin, textLayout, defaultBrush, DrawTextOptions::None );
	}

	void RenderTarget::DrawTextLayout( PointF origin, SlimDX::DirectWrite::TextLayout^ textLayout, Brush^ defaultBrush, DrawTextOptions options )
	{
		D2D1_POINT_2F po = D2D1::Point2F( origin.X, origin.Y );

		InternalPointer->DrawTextLayout( po, textLayout->InternalPointer, defaultBrush->InternalPointer, static_cast<D2D1_DRAW_TEXT_OPTIONS>( options ) );
	}

#pragma push_macro("DrawText")
#undef DrawText
	void RenderTarget::DrawText( String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::Rectangle layoutRectangle, Brush^ defaultBrush )
	{
		RectangleF rect = CastRectangle( layoutRectangle );

		DrawText( text, textFormat, rect, defaultBrush );
	}

	void RenderTarget::DrawText( String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::Rectangle layoutRectangle, Brush^ defaultBrush, DrawTextOptions options, SlimDX::DirectWrite::MeasuringMode measuringMethod )
	{
		RectangleF rect = CastRectangle( layoutRectangle );

		DrawText( text, textFormat, rect, defaultBrush, options, measuringMethod );
	}

	void RenderTarget::DrawText( String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::RectangleF layoutRectangle, Brush^ defaultBrush )
	{
		D2D1_RECT_F rect = D2D1::RectF( layoutRectangle.Left, layoutRectangle.Top, layoutRectangle.Right, layoutRectangle.Bottom );
		pin_ptr<const wchar_t> pinnedText = PtrToStringChars( text );

		InternalPointer->DrawTextW( pinnedText, text->Length, textFormat->InternalPointer, rect, defaultBrush->InternalPointer );
	}

	void RenderTarget::DrawText( String^ text, SlimDX::DirectWrite::TextFormat^ textFormat, System::Drawing::RectangleF layoutRectangle, Brush^ defaultBrush, DrawTextOptions options, SlimDX::DirectWrite::MeasuringMode measuringMethod )
	{
		D2D1_RECT_F rect = D2D1::RectF( layoutRectangle.Left, layoutRectangle.Top, layoutRectangle.Right, layoutRectangle.Bottom );
		pin_ptr<const wchar_t> pinnedText = PtrToStringChars( text );

		InternalPointer->DrawTextW( pinnedText, text->Length, textFormat->InternalPointer, rect, defaultBrush->InternalPointer,
			static_cast<D2D1_DRAW_TEXT_OPTIONS>( options ), static_cast<DWRITE_MEASURING_MODE>( measuringMethod ) );
	}
#pragma pop_macro("DrawText")

	void RenderTarget::DrawBitmap( Bitmap^ bitmap )
	{
		InternalPointer->DrawBitmap( bitmap->InternalPointer );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle )
	{
		D2D1_RECT_F destRect = D2D1::RectF( destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom );

		InternalPointer->DrawBitmap( bitmap->InternalPointer, &destRect );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity )
	{
		D2D1_RECT_F destRect = D2D1::RectF( destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom );

		InternalPointer->DrawBitmap( bitmap->InternalPointer, &destRect, opacity );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity, InterpolationMode interpolationMode )
	{
		D2D1_RECT_F destRect = D2D1::RectF( destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom );

		InternalPointer->DrawBitmap( bitmap->InternalPointer, &destRect, opacity, static_cast<D2D1_BITMAP_INTERPOLATION_MODE>( interpolationMode ) );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::RectangleF destinationRectangle, float opacity, InterpolationMode interpolationMode, System::Drawing::RectangleF sourceRectangle )
	{
		D2D1_RECT_F sourceRect = D2D1::RectF( sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom );
		D2D1_RECT_F destRect = D2D1::RectF( destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom );

		InternalPointer->DrawBitmap( bitmap->InternalPointer, &destRect, opacity, static_cast<D2D1_BITMAP_INTERPOLATION_MODE>( interpolationMode ), &sourceRect );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle )
	{
		DrawBitmap( bitmap, CastRectangle( destinationRectangle ) );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity )
	{
		DrawBitmap( bitmap, CastRectangle( destinationRectangle ), opacity );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity, InterpolationMode interpolationMode )
	{
		DrawBitmap( bitmap, CastRectangle( destinationRectangle ), opacity, interpolationMode );
	}

	void RenderTarget::DrawBitmap( Bitmap^ bitmap, System::Drawing::Rectangle destinationRectangle, float opacity, InterpolationMode interpolationMode, System::Drawing::Rectangle sourceRectangle )
	{
		DrawBitmap( bitmap, CastRectangle( destinationRectangle ), opacity, interpolationMode, CastRectangle( sourceRectangle ) );
	}

	void RenderTarget::DrawGeometry( Geometry^ geometry, Brush^ brush )
	{
		InternalPointer->DrawGeometry( geometry->InternalPointer, brush->InternalPointer );
	}

	void RenderTarget::DrawGeometry( Geometry^ geometry, Brush^ brush, float strokeWidth )
	{
		InternalPointer->DrawGeometry( geometry->InternalPointer, brush->InternalPointer, strokeWidth );
	}

	void RenderTarget::DrawGeometry( Geometry^ geometry, Brush^ brush, float strokeWidth, StrokeStyle^ strokeStyle )
	{
		ID2D1StrokeStyle *style = strokeStyle == nullptr ? NULL : strokeStyle->InternalPointer;

		InternalPointer->DrawGeometry( geometry->InternalPointer, brush->InternalPointer, strokeWidth, style );
	}

	void RenderTarget::FillGeometry( Geometry^ geometry, Brush^ brush )
	{
		FillGeometry( geometry, brush, nullptr );
	}

	void RenderTarget::FillGeometry( Geometry^ geometry, Brush^ brush, Brush^ opacityBrush )
	{
		ID2D1Brush *ob = opacityBrush == nullptr ? NULL : opacityBrush->InternalPointer;
		InternalPointer->FillGeometry( geometry->InternalPointer, brush->InternalPointer, ob );
	}

	void RenderTarget::FillOpacityMask( Bitmap^ mask, Brush^ brush, OpacityMaskContent content, Nullable<RectangleF> sourceRectangle, Nullable<RectangleF> destinationRectangle )
	{
		D2D1_RECT_F sourceRect;
		D2D1_RECT_F destRect;

		D2D1_RECT_F *sourceRectPtr = NULL;
		D2D1_RECT_F *destRectPtr = NULL;

		if( sourceRectangle.HasValue )
		{
			sourceRect = D2D1::RectF( sourceRectangle.Value.Left, sourceRectangle.Value.Top, sourceRectangle.Value.Right, sourceRectangle.Value.Bottom );
			sourceRectPtr = &sourceRect;
		}

		if( destinationRectangle.HasValue )
		{
			destRect = D2D1::RectF( destinationRectangle.Value.Left, destinationRectangle.Value.Top, destinationRectangle.Value.Right, destinationRectangle.Value.Bottom );
			destRectPtr = &destRect;
		}

		InternalPointer->FillOpacityMask( mask->InternalPointer, brush->InternalPointer, static_cast<D2D1_OPACITY_MASK_CONTENT>( content ), 
			destRectPtr, sourceRectPtr );
	}

	void RenderTarget::FillMesh( Mesh^ mesh, Brush^ brush )
	{
		InternalPointer->FillMesh( mesh->InternalPointer, brush->InternalPointer );
	}

	void RenderTarget::PopAxisAlignedClip()
	{
		InternalPointer->PopAxisAlignedClip();
	}

	void RenderTarget::PushAxisAlignedClip( System::Drawing::RectangleF clippingArea, SlimDX::Direct2D::AntialiasMode antialiasMode )
	{
		D2D1_RECT_F rect = D2D1::RectF( clippingArea.Left, clippingArea.Top, clippingArea.Right, clippingArea.Bottom );
		InternalPointer->PushAxisAlignedClip( rect, static_cast<D2D1_ANTIALIAS_MODE>( antialiasMode ) );
	}

	void RenderTarget::PushAxisAlignedClip( System::Drawing::Rectangle clippingArea, SlimDX::Direct2D::AntialiasMode antialiasMode )
	{
		PushAxisAlignedClip( RectangleF( static_cast<float>( clippingArea.X ), static_cast<float>( clippingArea.Y ), 
			static_cast<float>( clippingArea.Width ), static_cast<float>( clippingArea.Height ) ), antialiasMode );
	}

	Bitmap^ RenderTarget::CreateSharedBitmap( Guid guid, DataStream^ data )
	{
		ID2D1Bitmap *bitmap = NULL;

		HRESULT hr = InternalPointer->CreateSharedBitmap( Utilities::ConvertManagedGuid( guid ), data->PositionPointer, NULL, &bitmap );
		if( RECORD_D2D( hr ).IsFailure )
			return nullptr;

		return Bitmap::FromPointer( bitmap );
	}

	void RenderTarget::PopLayer()
	{
		InternalPointer->PopLayer();
	}

	void RenderTarget::PushLayer( Layer^ layer, LayerParameters^ parameters )
	{
		D2D1_LAYER_PARAMETERS params = parameters == nullptr ? D2D1::LayerParameters() : parameters->ToUnmanaged();
		InternalPointer->PushLayer( params, layer->InternalPointer );
	}

	void RenderTarget::SaveDrawingState( StateBlock^ stateBlock )
	{
		InternalPointer->SaveDrawingState( stateBlock->InternalPointer );
	}

	void RenderTarget::RestoreDrawingState( StateBlock^ stateBlock )
	{
		InternalPointer->RestoreDrawingState( stateBlock->InternalPointer );
	}

	SlimDX::DirectWrite::RenderingParameters^ RenderTarget::TextRenderingParameters::get()
	{
		IDWriteRenderingParams *pointer = NULL;
		InternalPointer->GetTextRenderingParams( &pointer );

		return SlimDX::DirectWrite::RenderingParameters::FromPointer( pointer );
	}

	void RenderTarget::TextRenderingParameters::set( SlimDX::DirectWrite::RenderingParameters^ value )
	{
		IDWriteRenderingParams *pointer = value == nullptr ? NULL : value->InternalPointer;
		InternalPointer->SetTextRenderingParams( pointer );
	}

	SlimDX::Direct2D::PixelFormat RenderTarget::PixelFormat::get()
	{
		D2D1_PIXEL_FORMAT pf = InternalPointer->GetPixelFormat();
		return SlimDX::Direct2D::PixelFormat( static_cast<SlimDX::DXGI::Format>( pf.format ), static_cast<AlphaMode>( pf.alphaMode ) );
	}

	Matrix3x2 RenderTarget::Transform::get()
	{
		Matrix3x2 result;
		InternalPointer->GetTransform( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &result ) );

		return result;
	}

	void RenderTarget::Transform::set( Matrix3x2 value )
	{
		InternalPointer->SetTransform( reinterpret_cast<D2D1_MATRIX_3X2_F*>( &value ) );
	}

	System::Drawing::SizeF RenderTarget::Size::get()
	{
		D2D1_SIZE_F size = InternalPointer->GetSize();
		return System::Drawing::SizeF( size.width, size.height );
	}

	System::Drawing::Size RenderTarget::PixelSize::get()
	{
		D2D1_SIZE_U size = InternalPointer->GetPixelSize();
		return System::Drawing::Size( size.width, size.height );
	}

	System::Drawing::SizeF RenderTarget::DotsPerInch::get()
	{
		float x, y;
		InternalPointer->GetDpi( &x, &y );
		return System::Drawing::SizeF( x, y );
	}

	void RenderTarget::DotsPerInch::set( SizeF value )
	{
		InternalPointer->SetDpi( value.Width, value.Height );
	}

	SlimDX::Direct2D::AntialiasMode RenderTarget::AntialiasMode::get()
	{
		return static_cast<SlimDX::Direct2D::AntialiasMode>( InternalPointer->GetAntialiasMode() );
	}

	void RenderTarget::AntialiasMode::set( SlimDX::Direct2D::AntialiasMode value )
	{
		InternalPointer->SetAntialiasMode( static_cast<D2D1_ANTIALIAS_MODE>( value ) );
	}

	SlimDX::Direct2D::TextAntialiasMode RenderTarget::TextAntialiasMode::get()
	{
		return static_cast<SlimDX::Direct2D::TextAntialiasMode>( InternalPointer->GetTextAntialiasMode() );
	}

	void RenderTarget::TextAntialiasMode::set( SlimDX::Direct2D::TextAntialiasMode value )
	{
		InternalPointer->SetTextAntialiasMode( static_cast<D2D1_TEXT_ANTIALIAS_MODE>( value ) );
	}
}
}