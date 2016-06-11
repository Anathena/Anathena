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

#include "../Result.h"
#include "../ComObject.h"

#include "DirectSoundException.h"

#include "SoundBufferDescription.h"
#include "CaptureBufferDescription.h"
#include "CaptureBuffer.h"
#include "SecondarySoundBuffer.h"
#include "FullDuplex.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	FullDuplex::FullDuplex( System::Guid captureDevice, System::Guid playbackDevice, SlimDX::DirectSound::CaptureBufferDescription captureDesc, SlimDX::DirectSound::SoundBufferDescription bufferDesc, System::IntPtr windowHandle, SlimDX::DirectSound::CooperativeLevel level, [Out] SlimDX::DirectSound::CaptureBuffer ^%captureBuffer, [Out] SlimDX::DirectSound::SecondarySoundBuffer ^%secondaryBuffer )
	{
		DSCBUFFERDESC cbDesc = captureDesc.ToUnmanaged();
		DSBUFFERDESC bDesc = bufferDesc.ToUnmanaged();

		IDirectSoundFullDuplex8* duplex;
		IDirectSoundCaptureBuffer8* capture;
		IDirectSoundBuffer8* buffer;
		HRESULT hr = DirectSoundFullDuplexCreate( reinterpret_cast<GUID*>( &captureDevice ), reinterpret_cast<GUID*>( &playbackDevice ), &cbDesc, &bDesc, static_cast<HWND>( windowHandle.ToPointer() ), static_cast<DWORD>( level ), &duplex, &capture, &buffer, NULL );
		
		delete[] cbDesc.lpwfxFormat;
		delete[] bDesc.lpwfxFormat;

		if( RECORD_DSOUND( hr ).IsFailure )
		{
			captureBuffer = nullptr;
			secondaryBuffer = nullptr;
			throw gcnew DirectSoundException( Result::Last );
		}
		else
		{
			Construct( duplex );
			captureBuffer = CaptureBuffer::FromPointer( capture );
			secondaryBuffer = static_cast<SecondarySoundBuffer^>( SecondarySoundBuffer::FromPointer( buffer ) );
		}
	}
}
}