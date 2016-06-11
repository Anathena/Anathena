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
#include <dinput.h>

#include "../InternalHelpers.h"

#include "DeviceImage.h"

using namespace System;
using namespace System::Drawing;
using namespace msclr::interop;

namespace SlimDX
{
namespace DirectInput
{
	DeviceImage::DeviceImage( const DIDEVICEIMAGEINFO &image )
	{
		path = gcnew String( image.tszImagePath );
		usage = static_cast<ImageUsage>( image.dwFlags );
		viewId = image.dwViewID;
		overlay = marshal_as<System::Drawing::Rectangle>( image.rcOverlay );
		instance = DIDFT_GETINSTANCE( image.dwObjID );
		type = static_cast<ObjectDeviceType>( DIDFT_GETTYPE( image.dwObjID ) );
		calloutRectangle = marshal_as<System::Drawing::Rectangle>( image.rcCalloutRect );
		alignment = static_cast<TextAlignment>( image.dwTextAlign );

		calloutLine = gcnew array<Point>( image.dwcValidPts );
		for( UINT i = 0; i < image.dwcValidPts; i++ )
			calloutLine[0] = marshal_as<Point>( image.rgptCalloutLine[i] );
	}
}
}