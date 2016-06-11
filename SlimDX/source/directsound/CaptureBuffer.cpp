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

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"
#include "../DataStream.h"

#include "../multimedia/WaveFormat.h"
#include "../multimedia/WaveFormatExtensible.h"

#include "DirectSoundException.h"

#include "CaptureBuffer.h"

using namespace System;
using namespace System::Reflection;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace DirectSound
{
	CaptureBuffer::CaptureBuffer( DirectSoundCapture^ capture, CaptureBufferDescription description )
	{
		DSCBUFFERDESC value = description.ToUnmanaged();
		IDirectSoundCaptureBuffer* buffer;

		HRESULT hr = capture->InternalPointer->CreateCaptureBuffer( &value, &buffer, NULL );

		delete[] value.lpDSCFXDesc;
		delete[] value.lpwfxFormat;

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		IDirectSoundCaptureBuffer8* dsCapture;
		hr = buffer->QueryInterface( IID_IDirectSoundCaptureBuffer8, reinterpret_cast<void**>( &dsCapture ) );

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		Construct( dsCapture );
	}
	
	Result CaptureBuffer::Start( bool looping )
	{
		HRESULT hr = InternalPointer->Start( looping ? DSCBSTART_LOOPING : 0 );
		return RECORD_DSOUND( hr );
	}

	Result CaptureBuffer::Stop()
	{
		HRESULT hr = InternalPointer->Stop();
		return RECORD_DSOUND( hr );
	}

	DataStream^ CaptureBuffer::Lock( int offset, int sizeBytes, bool lockEntireBuffer, [Out] DataStream^% secondPart )
	{
		void* buffer1;
		void* buffer2;
		DWORD size1, size2;

		secondPart = nullptr;

		HRESULT hr = InternalPointer->Lock( offset, sizeBytes, &buffer1, &size1, &buffer2, &size2, lockEntireBuffer ? DSCBLOCK_ENTIREBUFFER : 0 );
		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		DataStream^ stream1 = gcnew DataStream( buffer1, size1, true, false, false );
		if( buffer2 != NULL )
			secondPart = gcnew DataStream( buffer2, size2, true, false, false );

		return stream1;
	}

	Result CaptureBuffer::Unlock( DataStream^ firstPart, DataStream^ secondPart )
	{
		void* buffer2 = NULL;
		int size2 = 0;

		if( secondPart != nullptr )
		{
			buffer2 = secondPart->RawPointer;
			size2 = static_cast<int>( secondPart->Length );
		}

		HRESULT hr = InternalPointer->Unlock( firstPart->RawPointer, static_cast<int>( firstPart->Length ), buffer2, size2 );
		return RECORD_DSOUND( hr );
	}

	generic<typename T>
	Result CaptureBuffer::Read( array<T>^ data, int bufferOffset, bool lockEntireBuffer )
	{
		return InternalRead( data, 0, 0, bufferOffset, lockEntireBuffer );
	}

	generic<typename T>
	Result CaptureBuffer::Read( array<T>^ data, int startIndex, int count, int bufferOffset )
	{
		return InternalRead( data, startIndex, count, bufferOffset, false );
	}

	generic<typename T>
	Result CaptureBuffer::InternalRead( array<T>^ data, int startIndex, int count, int bufferOffset, bool lockEntireBuffer )
	{
		Utilities::CheckArrayBounds( data, startIndex, count );
		int bytes = static_cast<int>(sizeof(T) * count);

		DataStream^ stream2;
		DataStream^ stream1 = Lock( bufferOffset, bytes, lockEntireBuffer, stream2 );

		if( stream1 == nullptr )
			return Result::Last;

		int count1 = static_cast<int>( stream1->Length ) / sizeof(T);
		stream1->ReadRange( data, startIndex, count1 );

		if( stream2 != nullptr && count > count1 )
			stream2->ReadRange( data, count1 + startIndex, count - count1 );

		return Unlock( stream1, stream2 );
	}

	Result CaptureBuffer::SetNotificationPositions( array<NotificationPosition>^ positions )
	{
		IDirectSoundNotify *pointer;
		HRESULT hr = InternalPointer->QueryInterface( IID_IDirectSoundNotify, reinterpret_cast<void**>( &pointer ) );

		if( FAILED( hr ) )
			return RECORD_DSOUND( hr );

		stack_array<DSBPOSITIONNOTIFY> notifies = stackalloc( DSBPOSITIONNOTIFY, positions->Length );
		for( int i = 0; i < positions->Length; i++ )
		{
			notifies[i].dwOffset = positions[i].Offset;
			notifies[i].hEventNotify = positions[i].Event->SafeWaitHandle->DangerousGetHandle().ToPointer();
		}

		hr = pointer->SetNotificationPositions( positions->Length, &notifies[0] );
		pointer->Release();

		return RECORD_DSOUND( hr );
	}

	WaveFormat^ CaptureBuffer::Format::get()
	{
		WaveFormat^ result;
		WAVEFORMATEX* format;
		DWORD size;

		HRESULT hr = InternalPointer->GetFormat( NULL, 0, &size );
		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		// Manual Allocation: handled properly in the try/finally clause below
		format = reinterpret_cast<WAVEFORMATEX*>( new char[size] );

		try
		{
			hr = InternalPointer->GetFormat( format, size, NULL );

			if( RECORD_DSOUND( hr ).IsFailure )
				return nullptr;

			if( format->wFormatTag == WAVE_FORMAT_EXTENSIBLE )
				result = WaveFormatExtensible::FromBase( format );
			else 
				result = WaveFormat::FromUnmanaged( *format );

			return result;
		}
		finally
		{
			delete[] format;
		}
	}

	array<CaptureEffectResult>^ CaptureBuffer::GetEffectStatus( int effectCount )
	{
		stack_array<DWORD> results = stackalloc( DWORD, effectCount );

		HRESULT hr = InternalPointer->GetFXStatus( effectCount, &results[0] );
		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		array<CaptureEffectResult>^ output = gcnew array<CaptureEffectResult>( effectCount );

		for( int i = 0; i < effectCount; i++ )
			output[i] = static_cast<CaptureEffectResult>( results[i] );

		return output;
	}

	generic<typename T>
	T CaptureBuffer::GetEffect( int index )
	{
		GUID guid = Utilities::GetNativeGuidForType( T::typeid );
		void *resultPointer;

		HRESULT hr = InternalPointer->GetObjectInPath( GUID_All_Objects, index, guid, &resultPointer );
		if( RECORD_DSOUND( hr ).IsFailure )
			return T();

		MethodInfo^ method = T::typeid->GetMethod( "FromPointer", BindingFlags::Public | BindingFlags::Static );
		return safe_cast<T>( method->Invoke( nullptr, gcnew array<Object^> { IntPtr( resultPointer ) } ) );
	}

	bool CaptureBuffer::WaveMapped::get()
	{
		DSCBCAPS caps;
		caps.dwSize = sizeof( DSCBCAPS );
		HRESULT hr = InternalPointer->GetCaps( &caps );

		if( RECORD_DSOUND( hr ).IsFailure )
			return false;

		return caps.dwFlags == DSCBCAPS_WAVEMAPPED;
	}

	int CaptureBuffer::SizeInBytes::get()
	{
		DSCBCAPS caps;
		caps.dwSize = sizeof( DSCBCAPS );
		HRESULT hr = InternalPointer->GetCaps( &caps );

		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return caps.dwBufferBytes;
	}

	bool CaptureBuffer::Capturing::get()
	{
		DWORD status;
		HRESULT hr = InternalPointer->GetStatus( &status );
		
		if( RECORD_DSOUND( hr ).IsFailure )
			return false;

		return ( status & DSCBSTATUS_CAPTURING ) != 0;
	}

	bool CaptureBuffer::Looping::get()
	{
		DWORD status;
		HRESULT hr = InternalPointer->GetStatus( &status );
		
		if( RECORD_DSOUND( hr ).IsFailure )
			return false;

		return ( status & DSCBSTATUS_LOOPING ) != 0;
	}

	int CaptureBuffer::CurrentCapturePosition::get()
	{
		DWORD capturePosition;
		HRESULT hr = InternalPointer->GetCurrentPosition( &capturePosition, NULL );
		
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return capturePosition;
	}

	int CaptureBuffer::CurrentReadPosition::get()
	{
		DWORD readPosition;
		HRESULT hr = InternalPointer->GetCurrentPosition( NULL, &readPosition );
		
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return readPosition;
	}
}
}