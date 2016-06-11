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
#include "../DataStream.h"

#include "../multimedia/WaveFormatExtensible.h"

#include "DirectSoundException.h"

#include "SoundBuffer.h"

using namespace System;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace DirectSound
{
	Result SoundBuffer::Restore()
	{
		HRESULT hr = InternalPointer->Restore();
		return RECORD_DSOUND( hr );
	}

	Result SoundBuffer::Play( int priority, PlayFlags flags )
	{
		HRESULT hr = InternalPointer->Play( 0, priority, static_cast<DWORD>( flags ) );
		return RECORD_DSOUND( hr );
	}

	Result SoundBuffer::Stop()
	{
		HRESULT hr = InternalPointer->Stop();
		return RECORD_DSOUND( hr );
	}

	DataStream^ SoundBuffer::Lock( int offset, int sizeBytes, LockFlags flags, [Out] DataStream^% secondPart )
	{
		void* buffer1;
		void* buffer2;
		DWORD size1, size2;

		secondPart = nullptr;

		HRESULT hr = InternalPointer->Lock( offset, sizeBytes, &buffer1, &size1, &buffer2, &size2, static_cast<DWORD>( flags ) );
		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		DataStream^ stream1 = gcnew DataStream( buffer1, size1, false, true, false );
		if( buffer2 != NULL )
			secondPart = gcnew DataStream( buffer2, size2, false, true, false );

		return stream1;
	}

	Result SoundBuffer::Unlock( DataStream^ firstPart, DataStream^ secondPart )
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
	Result SoundBuffer::Write( array<T>^ data, int bufferOffset, SlimDX::DirectSound::LockFlags flags )
	{
		return Write( data, 0, 0, bufferOffset, flags );
	}

	generic<typename T>
	Result SoundBuffer::Write( array<T>^ data, int startIndex, int count, int bufferOffset, SlimDX::DirectSound::LockFlags flags )
	{
		Utilities::CheckArrayBounds( data, startIndex, count );
		int bytes = static_cast<int>(sizeof(T) * count);

		DataStream^ stream2;
		DataStream^ stream1 = Lock( bufferOffset, bytes, flags, stream2 );

		if( stream1 == nullptr )
			return Result::Last;

		int count1 = static_cast<int>( stream1->Length ) / sizeof(T);
		stream1->WriteRange( data, startIndex, count1 );

		if( stream2 != nullptr && count > count1 )
			stream2->WriteRange( data, count1 + startIndex, static_cast<int>( stream2->Length ) / sizeof(T) );

		return Unlock( stream1, stream2 );
	}

	Result SoundBuffer::SetNotificationPositions( array<NotificationPosition>^ positions )
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

	void SoundBuffer::Format::set( WaveFormat^ sourceFormat )
	{
		auto_array<WAVEFORMATEX> format = WaveFormat::ToUnmanaged( sourceFormat );

		HRESULT hr = InternalPointer->SetFormat( format.get() );
		RECORD_DSOUND( hr );
	}

	WaveFormat^ SoundBuffer::Format::get()
	{
		WaveFormat^ result;
		WAVEFORMATEX* format;
		DWORD size;

		HRESULT hr = InternalPointer->GetFormat( NULL, 0, &size );
		if( RECORD_DSOUND( hr ).IsFailure )
			return nullptr;

		// Manual Allocation: cleaned up properly by the try/finally clause below
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

	int SoundBuffer::CurrentPlayPosition::get()
	{
		DWORD position = 0;
		HRESULT hr = InternalPointer->GetCurrentPosition( &position, NULL );
		RECORD_DSOUND( hr );

		return position;
	}
	
	void SoundBuffer::CurrentPlayPosition::set( int value )
	{
		HRESULT hr = InternalPointer->SetCurrentPosition( value );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer::CurrentWritePosition::get()
	{
		DWORD position = 0;
		HRESULT hr = InternalPointer->GetCurrentPosition( NULL, &position );
		RECORD_DSOUND( hr );

		return position;
	}
	
	int SoundBuffer::Frequency::get()
	{
		DWORD freq = 0;
		HRESULT hr = InternalPointer->GetFrequency( &freq );
		RECORD_DSOUND( hr );

		return freq;
	}

	void SoundBuffer::Frequency::set( int value )
	{
		HRESULT hr = InternalPointer->SetFrequency( value );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer::Volume::get()
	{
		LONG volume = 0;
		HRESULT hr = InternalPointer->GetVolume( &volume );
		RECORD_DSOUND( hr );

		return volume;
	}

	void SoundBuffer::Volume::set( int value )
	{
		HRESULT hr = InternalPointer->SetVolume( value );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer::Pan::get()
	{
		LONG pan = 0;
		HRESULT hr = InternalPointer->GetPan( &pan );
		RECORD_DSOUND( hr );

		return pan;
	}

	void SoundBuffer::Pan::set( int value )
	{
		HRESULT hr = InternalPointer->SetPan( value );
		RECORD_DSOUND( hr );
	}

	BufferStatus SoundBuffer::Status::get()
	{
		DWORD status = 0;
		HRESULT hr = InternalPointer->GetStatus( &status );
		RECORD_DSOUND( hr );

		return static_cast<BufferStatus>( status );
	}

	BufferCapabilities SoundBuffer::Capabilities::get()
	{
		DSBCAPS caps;
		caps.dwSize = sizeof( DSBCAPS );
		HRESULT hr = InternalPointer->GetCaps( &caps );
		RECORD_DSOUND( hr );

		return BufferCapabilities( caps );
	}
}
}