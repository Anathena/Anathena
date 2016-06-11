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

#include "../DataStream.h"

#include "Direct2DException.h"

#include "ConversionMethods.h"
#include "Bitmap.h"

const IID IID_ID2D1Bitmap = __uuidof(ID2D1Bitmap);

using namespace System;
using namespace System::Drawing;

namespace SlimDX
{
namespace Direct2D
{
	Bitmap::Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size )
	{
		ID2D1Bitmap *bitmap = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmap( D2D1::SizeU( size.Width, size.Height ), D2D1::BitmapProperties(), &bitmap );
		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( bitmap );
	}

	Bitmap::Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, BitmapProperties properties )
	{
		ID2D1Bitmap *bitmap = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmap( D2D1::SizeU( size.Width, size.Height ), BitmapProperties::ToUnmanaged( properties ), &bitmap );
		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( bitmap );
	}

	Bitmap::Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, DataStream^ data, int pitch )
	{
		ID2D1Bitmap *bitmap = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmap( D2D1::SizeU( size.Width, size.Height ), data->DataPointer.ToPointer(), pitch,
			D2D1::BitmapProperties(), &bitmap );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( bitmap );
	}

	Bitmap::Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, DataStream^ data, int pitch, BitmapProperties properties )
	{
		ID2D1Bitmap *bitmap = NULL;

		HRESULT hr = renderTarget->InternalPointer->CreateBitmap( D2D1::SizeU( size.Width, size.Height ), data->DataPointer.ToPointer(), pitch,
			BitmapProperties::ToUnmanaged( properties ), &bitmap );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( bitmap );
	}

	Bitmap::Bitmap(RenderTarget^ renderTarget, SlimDX::DXGI::Surface^ surface)
	{
		ID2D1Bitmap *bitmap = 0;
		HRESULT hr = renderTarget->InternalPointer->CreateSharedBitmap(IID_IDXGISurface, surface->InternalPointer, NULL, &bitmap);

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );
		Construct( bitmap );
	}

	Bitmap::Bitmap(RenderTarget^ renderTarget, SlimDX::DXGI::Surface^ surface, BitmapProperties properties)
	{
		ID2D1Bitmap *bitmap = 0;
		D2D1_BITMAP_PROPERTIES bitmapProperties = BitmapProperties::ToUnmanaged(properties);
		HRESULT hr = renderTarget->InternalPointer->CreateSharedBitmap(IID_IDXGISurface, surface->InternalPointer, &bitmapProperties, &bitmap);

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );
		Construct( bitmap );
	}

	Result Bitmap::FromBitmap( Bitmap^ sourceBitmap )
	{
		HRESULT hr = InternalPointer->CopyFromBitmap( NULL, sourceBitmap->InternalPointer, NULL );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromBitmap( Bitmap^ sourceBitmap, Point destinationPoint )
	{
		HRESULT hr = InternalPointer->CopyFromBitmap( reinterpret_cast<D2D1_POINT_2U*>( &destinationPoint ), sourceBitmap->InternalPointer, NULL );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromBitmap( Bitmap^ sourceBitmap, Point destinationPoint, System::Drawing::Rectangle sourceArea )
	{
		D2D1_RECT_U rect = ConvertRectangle( sourceArea );

		HRESULT hr = InternalPointer->CopyFromBitmap( reinterpret_cast<D2D1_POINT_2U*>( &destinationPoint ), sourceBitmap->InternalPointer, &rect );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromRenderTarget( RenderTarget^ renderTarget )
	{
		HRESULT hr = InternalPointer->CopyFromRenderTarget( NULL, renderTarget->InternalPointer, NULL );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromRenderTarget( RenderTarget^ renderTarget, System::Drawing::Point destinationPoint )
	{
		HRESULT hr = InternalPointer->CopyFromRenderTarget( reinterpret_cast<D2D1_POINT_2U*>( &destinationPoint ), renderTarget->InternalPointer, NULL );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromRenderTarget( RenderTarget^ renderTarget, System::Drawing::Point destinationPoint, System::Drawing::Rectangle sourceArea )
	{
		D2D1_RECT_U rect = ConvertRectangle( sourceArea );

		HRESULT hr = InternalPointer->CopyFromRenderTarget( reinterpret_cast<D2D1_POINT_2U*>( &destinationPoint ), renderTarget->InternalPointer, &rect );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromMemory( array<Byte>^ memory, int pitch )
	{
		pin_ptr<Byte> pinnedMemory = &memory[0];

		HRESULT hr = InternalPointer->CopyFromMemory( NULL, pinnedMemory, pitch );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromMemory( array<Byte>^ memory, int pitch, System::Drawing::Rectangle destinationArea )
	{
		pin_ptr<Byte> pinnedMemory = &memory[0];
		D2D1_RECT_U rect = ConvertRectangle( destinationArea );

		HRESULT hr = InternalPointer->CopyFromMemory( &rect, pinnedMemory, pitch );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromMemory( IntPtr pointer, int pitch )
	{
		HRESULT hr = InternalPointer->CopyFromMemory( NULL, pointer.ToPointer(), pitch );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromMemory( IntPtr pointer, int pitch, System::Drawing::Rectangle destinationArea )
	{
		D2D1_RECT_U rect = ConvertRectangle( destinationArea );

		HRESULT hr = InternalPointer->CopyFromMemory( &rect, pointer.ToPointer(), pitch );
		return RECORD_D2D( hr );
	}

	Result Bitmap::FromStream( System::IO::Stream^ stream, int pitch, int length )
	{
		DataStream^ dataStream;
		array<Byte>^ bytes = Utilities::ReadStream( stream, length, &dataStream );

		if( bytes == nullptr )
			return FromMemory( dataStream->DataPointer, pitch );
		else
			return FromMemory( bytes, pitch );
	}

	Result Bitmap::FromStream( System::IO::Stream^ stream, int pitch, int length, System::Drawing::Rectangle destinationArea )
	{
		DataStream^ dataStream;
		array<Byte>^ bytes = Utilities::ReadStream( stream, length, &dataStream );

		if( bytes == nullptr )
			return FromMemory( dataStream->DataPointer, pitch, destinationArea );
		else
			return FromMemory( bytes, pitch, destinationArea );
	}

	SlimDX::Direct2D::PixelFormat Bitmap::PixelFormat::get()
	{
		D2D1_PIXEL_FORMAT pf = InternalPointer->GetPixelFormat();
		return SlimDX::Direct2D::PixelFormat( static_cast<SlimDX::DXGI::Format>( pf.format ), static_cast<AlphaMode>( pf.alphaMode ) );
	}

	System::Drawing::SizeF Bitmap::Size::get()
	{
		D2D1_SIZE_F size = InternalPointer->GetSize();
		return System::Drawing::SizeF( size.width, size.height );
	}

	System::Drawing::Size Bitmap::PixelSize::get()
	{
		D2D1_SIZE_U size = InternalPointer->GetPixelSize();
		return System::Drawing::Size( size.width, size.height );
	}

	System::Drawing::SizeF Bitmap::DotsPerInch::get()
	{
		float x, y;
		InternalPointer->GetDpi( &x, &y );
		return System::Drawing::SizeF( x, y );
	}
}
}