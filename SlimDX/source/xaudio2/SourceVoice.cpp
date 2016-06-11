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

#include <xaudio2.h>
#include <vcclr.h>

#include "../VersionConfig.h"
#include "../ComObject.h"
#include "../DataStream.h"
#include "../multimedia/WaveFormat.h"
#include "../multimedia/WaveFormatExtensible.h"

#include "XAudio2Exception.h"

#include "XAudio2.h"
#include "SourceVoice.h"

using namespace System;
using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace XAudio2
{
	SourceVoice::SourceVoice( XAudio2^ device, WaveFormat^ sourceFormat, VoiceFlags flags, float maximumFrequencyRatio )
	{
		IXAudio2SourceVoice *pointer;
		auto_array<WAVEFORMATEX> format = WaveFormat::ToUnmanaged( sourceFormat );

		// Manual Allocation: cleaned up in the destructor / finalizer
		callback = new VoiceCallbackShim( this );

		HRESULT hr = device->InternalPointer->CreateSourceVoice( &pointer, format.get(), static_cast<UINT32>( flags ), maximumFrequencyRatio, callback );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	SourceVoice::SourceVoice( XAudio2^ device, WaveFormat^ sourceFormat, VoiceFlags flags )
	{
		IXAudio2SourceVoice *pointer;
		auto_array<WAVEFORMATEX> format = WaveFormat::ToUnmanaged( sourceFormat );

		// Manual Allocation: cleaned up in the destructor / finalizer
		callback = new VoiceCallbackShim( this );

		HRESULT hr = device->InternalPointer->CreateSourceVoice( &pointer, format.get(), static_cast<UINT32>( flags ), XAUDIO2_DEFAULT_FREQ_RATIO, callback );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	SourceVoice::SourceVoice( XAudio2^ device, WaveFormat^ sourceFormat )
	{
		IXAudio2SourceVoice *pointer;
		auto_array<WAVEFORMATEX> format = WaveFormat::ToUnmanaged( sourceFormat );

		// Manual Allocation: cleaned up in the destructor / finalizer
		callback = new VoiceCallbackShim( this );

		HRESULT hr = device->InternalPointer->CreateSourceVoice( &pointer, format.get(), 0, XAUDIO2_DEFAULT_FREQ_RATIO, callback );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	SourceVoice::~SourceVoice()
	{
		if( InternalPointer != NULL )
			InternalPointer->DestroyVoice();
		InternalPointer = NULL;

		if( callback != NULL )
			delete callback;
		callback = NULL;
	}

	void SourceVoice::OnBufferEnd( ContextEventArgs^ e )
	{
		if( &SourceVoice::BufferEnd != nullptr )
			BufferEnd( this, e );
	}

	void SourceVoice::OnBufferStart( ContextEventArgs^ e )
	{
		if( &SourceVoice::BufferStart != nullptr )
			BufferStart( this, e );
	}

	void SourceVoice::OnLoopEnd( ContextEventArgs^ e )
	{
		if( &SourceVoice::LoopEnd != nullptr )
			LoopEnd( this, e );
	}

	void SourceVoice::OnStreamEnd( EventArgs^ e )
	{
		if( &SourceVoice::StreamEnd != nullptr )
			StreamEnd( this, e );
	}

	void SourceVoice::OnVoiceError( ErrorEventArgs^ e )
	{
		if( &SourceVoice::VoiceError != nullptr )
			VoiceError( this, e );
	}

	void SourceVoice::OnVoiceProcessingPassEnd( EventArgs^ e )
	{
		if( &SourceVoice::VoiceProcessingPassEnd != nullptr )
			VoiceProcessingPassEnd( this, e );
	}

	void SourceVoice::OnVoiceProcessingPassStart( int bytesRequired )
	{
		if( &SourceVoice::VoiceProcessingPassStart != nullptr )
			VoiceProcessingPassStart( this, gcnew StartProcessingEventArgs( bytesRequired )  );
	}

	Result SourceVoice::Discontinuity()
	{
		HRESULT hr = SourcePointer->Discontinuity();
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::FlushSourceBuffers()
	{
		HRESULT hr = SourcePointer->FlushSourceBuffers();
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::ExitLoop()
	{
		HRESULT hr = SourcePointer->ExitLoop();
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::ExitLoop( int operationSet )
	{
		HRESULT hr = SourcePointer->ExitLoop( operationSet );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Start()
	{
		HRESULT hr = SourcePointer->Start( 0 );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Start( PlayFlags flags )
	{
		HRESULT hr = SourcePointer->Start( static_cast<UINT32>( flags ) );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Start( PlayFlags flags, int operationSet )
	{
		HRESULT hr = SourcePointer->Start( static_cast<UINT32>( flags ), operationSet );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Stop()
	{
		HRESULT hr = SourcePointer->Stop( 0 );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Stop( PlayFlags flags )
	{
		HRESULT hr = SourcePointer->Stop( static_cast<UINT32>( flags ) );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::Stop( PlayFlags flags, int operationSet )
	{
		HRESULT hr = SourcePointer->Stop( static_cast<UINT32>( flags ), operationSet );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::SubmitSourceBuffer( AudioBuffer^ buffer )
	{
		XAUDIO2_BUFFER input = buffer->ToUnmanaged();

		HRESULT hr = SourcePointer->SubmitSourceBuffer( &input );
		return RECORD_XAUDIO2( hr );
	}

	Result SourceVoice::SubmitSourceBuffer( AudioBuffer^ buffer, array<int>^ decodedPacketCumulativeBytes )
	{
		XAUDIO2_BUFFER input = buffer->ToUnmanaged();
		pin_ptr<int> pinnedData = &decodedPacketCumulativeBytes[0];

		XAUDIO2_BUFFER_WMA wma;
		wma.PacketCount = decodedPacketCumulativeBytes->Length;
#if SLIMDX_XAUDIO2_VERSION < 23
		wma.pDecodedPacketCumulativeBytes = reinterpret_cast<const UINT32*>( pinnedData );
#else
		wma.pDecodedPacketCumulativeBytes = reinterpret_cast<UINT32*>( pinnedData );
#endif

		HRESULT hr = SourcePointer->SubmitSourceBuffer( &input, &wma );
		return RECORD_XAUDIO2( hr );
	}

#if SLIMDX_XAUDIO2_VERSION >= 24
	Result SourceVoice::SetSourceSampleRate( int sampleRate ) 
	{
		HRESULT hr = SourcePointer->SetSourceSampleRate( sampleRate );
		return RECORD_XAUDIO2( hr );
	}
#endif

	VoiceState SourceVoice::State::get()
	{
		XAUDIO2_VOICE_STATE state;

		SourcePointer->GetState( &state );

		VoiceState result;
		result.Context = IntPtr( state.pCurrentBufferContext );
		result.BuffersQueued = state.BuffersQueued;
		result.SamplesPlayed = static_cast<Int64>( state.SamplesPlayed );

		return result;
	}

	float SourceVoice::FrequencyRatio::get()
	{
		float result;
		SourcePointer->GetFrequencyRatio( &result );

		return result;
	}

	void SourceVoice::FrequencyRatio::set( float value )
	{
		HRESULT hr = SourcePointer->SetFrequencyRatio( value );
		RECORD_XAUDIO2( hr );
	}
}
}