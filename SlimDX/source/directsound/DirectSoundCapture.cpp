#include "stdafx.h"
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
#include <windows.h>
#include <dsound.h>
#include <memory>

#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectSoundException.h"

#include "CallbacksDS.h"
#include "DirectSoundCapture.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace DirectSound
{
	DirectSoundCapture::DirectSoundCapture()
	{
		IDirectSoundCapture8* capture;

		HRESULT hr = DirectSoundCaptureCreate8( NULL, &capture, NULL );

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		Construct( capture );
	}

	DirectSoundCapture::DirectSoundCapture( System::Guid device )
	{
		IDirectSoundCapture8* capture;

		GUID guid = Utilities::ConvertManagedGuid( device );
		HRESULT hr = DirectSoundCaptureCreate8( &guid, &capture, NULL );

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		Construct( capture );
	}

	CaptureCapabilities DirectSoundCapture::Capabilities::get()
	{
		DSCCAPS caps;
		caps.dwSize = sizeof( DSCCAPS );
		HRESULT hr = InternalPointer->GetCaps( &caps );
		RECORD_DSOUND( hr );

		return CaptureCapabilities( caps );
	}

	DeviceCollection^ DirectSoundCapture::GetDevices()
	{
		DeviceCollection^ results = gcnew DeviceCollection();
		std::auto_ptr<DeviceCollectionShim> shim( new DeviceCollectionShim( results ) );

		HRESULT hr = DirectSoundCaptureEnumerate( EnumerateDevices, shim.get() );

		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		return results;
	}
}
}