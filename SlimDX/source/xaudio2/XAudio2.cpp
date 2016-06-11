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

#define XAUDIO2_HELPER_FUNCTIONS

#include <xaudio2.h>
#include <vcclr.h>

#include "../ComObject.h"
#include "../ObjectTable.h"
#include "../Utilities.h"

#include "XAudio2Exception.h"

#include "XAudio2.h"

using namespace System;

namespace SlimDX
{
namespace XAudio2
{
	XAudio2::XAudio2()
	{
		IXAudio2 *pointer;

		HRESULT hr = XAudio2Create( &pointer, 0, XAUDIO2_DEFAULT_PROCESSOR );
		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		Construct( pointer );

		// Manual Allocation: cleaned up in the finalizer / destructor
		callback = new EngineCallbackShim( this );
		hr = pointer->RegisterForCallbacks( callback );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );
	}
	
	XAudio2::XAudio2( XAudio2Flags flags, ProcessorSpecifier processor )
	{
		IXAudio2 *pointer;

		HRESULT hr = XAudio2Create( &pointer, static_cast<UINT32>( flags ), static_cast<XAUDIO2_PROCESSOR>( processor ) );
		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		Construct( pointer );

		// Manual Allocation: cleaned up in the finalizer / destructor
		callback = new EngineCallbackShim( this );
		hr = pointer->RegisterForCallbacks( callback );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );
	}

	float XAudio2::AmplitudeRatioToDecibels( float volume )
	{
		return XAudio2AmplitudeRatioToDecibels( volume );
	}

	float XAudio2::CutoffFrequencyToRadians( float cutoffFrequency, int sampleRate )
	{
		return XAudio2CutoffFrequencyToRadians( cutoffFrequency, sampleRate );
	}

	float XAudio2::DecibelsToAmplitudeRatio( float decibels )
	{
		return XAudio2DecibelsToAmplitudeRatio( decibels );
	}

	float XAudio2::FrequencyRatioToSemitones( float frequencyRatio )
	{
		return XAudio2FrequencyRatioToSemitones( frequencyRatio );
	}

	float XAudio2::RadiansToCutoffFrequency( float radians, float sampleRate )
	{
		return XAudio2RadiansToCutoffFrequency( radians, sampleRate );
	}

	float XAudio2::SemitonesToFrequencyRatio( float semitones )
	{
		return XAudio2SemitonesToFrequencyRatio( semitones );
	}

	Result XAudio2::StartEngine()
	{
		HRESULT hr = InternalPointer->StartEngine();
		return RECORD_XAUDIO2( hr );
	}

	void XAudio2::StopEngine()
	{
		InternalPointer->StopEngine();
	}

	Result XAudio2::CommitChanges()
	{
		HRESULT hr = InternalPointer->CommitChanges( XAUDIO2_COMMIT_ALL );
		return RECORD_XAUDIO2( hr );
	}

	Result XAudio2::CommitChanges( int operationSet )
	{
		HRESULT hr = InternalPointer->CommitChanges( operationSet );
		return RECORD_XAUDIO2( hr );
	}

	DeviceDetails^ XAudio2::GetDeviceDetails( int index )
	{
		XAUDIO2_DEVICE_DETAILS deviceDetails;

		HRESULT hr = InternalPointer->GetDeviceDetails( index, &deviceDetails );
		if( RECORD_XAUDIO2( hr ).IsFailure )
			return nullptr;

		return gcnew DeviceDetails( deviceDetails );
	}

	void XAudio2::SetDebugConfiguration( DebugConfiguration configuration )
	{
		InternalPointer->SetDebugConfiguration( reinterpret_cast<XAUDIO2_DEBUG_CONFIGURATION*>( &configuration ) );
	}

	int XAudio2::DeviceCount::get()
	{
		UINT32 count = 0;
		HRESULT hr = InternalPointer->GetDeviceCount( &count );
		RECORD_XAUDIO2( hr );

		return count;
	}

	SlimDX::XAudio2::PerformanceData^ XAudio2::PerformanceData::get()
	{
		XAUDIO2_PERFORMANCE_DATA data;
		InternalPointer->GetPerformanceData( &data );

		return gcnew SlimDX::XAudio2::PerformanceData( data );
	}

	void XAudio2::OnCriticalError(ErrorEventArgs^ e)
	{
		if( &XAudio2::CriticalError != nullptr )
			CriticalError( this, e );
	}

	void XAudio2::OnProcessingPassEnd(EventArgs^ e)
	{
		if( &XAudio2::ProcessingPassEnd != nullptr )
			ProcessingPassEnd( this, e );
	}

	void XAudio2::OnProcessingPassStart(EventArgs^ e)
	{
		if( &XAudio2::ProcessingPassStart != nullptr )
			ProcessingPassStart( this, e );
	}

	Guid XAudio2::FormatSubtypePcm::get()
	{
		return Utilities::ConvertNativeGuid( KSDATAFORMAT_SUBTYPE_PCM );
	}

	Guid XAudio2::FormatSubtypeAdpcm::get()
	{
		return Utilities::ConvertNativeGuid( KSDATAFORMAT_SUBTYPE_ADPCM );
	}

	Guid XAudio2::FormatSubtypeIeee::get()
	{
		return Utilities::ConvertNativeGuid( KSDATAFORMAT_SUBTYPE_IEEE_FLOAT );
	}
}
}