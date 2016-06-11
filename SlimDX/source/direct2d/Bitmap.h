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

#include "Enums.h"
#include "ResourceD2D.h"
#include "PixelFormat.h"
#include "RenderTarget.h"
#include "BitmapProperties.h"

namespace SlimDX
{
	namespace Direct2D
	{
		public ref class Bitmap : Resource
		{
			COMOBJECT(ID2D1Bitmap, Bitmap);
			
		public:
			Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, BitmapProperties properties );
			Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, DataStream^ data, int pitch, BitmapProperties properties );
			Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size );
			Bitmap( RenderTarget^ renderTarget, System::Drawing::Size size, DataStream^ data, int pitch );
			Bitmap( RenderTarget^ renderTarget, SlimDX::DXGI::Surface^ surface);
			Bitmap( RenderTarget^ renderTarget, SlimDX::DXGI::Surface^ surface, BitmapProperties properties);

			Result FromBitmap( Bitmap^ sourceBitmap );
			Result FromBitmap( Bitmap^ sourceBitmap, System::Drawing::Point destinationPoint );
			Result FromBitmap( Bitmap^ sourceBitmap, System::Drawing::Point destinationPoint, System::Drawing::Rectangle sourceArea );

			Result FromRenderTarget( RenderTarget^ renderTarget );
			Result FromRenderTarget( RenderTarget^ renderTarget, System::Drawing::Point destinationPoint );
			Result FromRenderTarget( RenderTarget^ renderTarget, System::Drawing::Point destinationPoint, System::Drawing::Rectangle sourceArea );

			Result FromMemory( array<System::Byte>^ memory, int pitch );
			Result FromMemory( array<System::Byte>^ memory, int pitch, System::Drawing::Rectangle destinationArea );

			Result FromMemory( System::IntPtr pointer, int pitch );
			Result FromMemory( System::IntPtr pointer, int pitch, System::Drawing::Rectangle destinationArea );

			Result FromStream( System::IO::Stream^ stream, int pitch, int length );
			Result FromStream( System::IO::Stream^ stream, int pitch, int length, System::Drawing::Rectangle destinationArea );

			property System::Drawing::SizeF DotsPerInch
			{
				System::Drawing::SizeF get();
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
		};
	}
}