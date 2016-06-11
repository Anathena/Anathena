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

#include "PerformanceData.h"

namespace SlimDX
{
namespace XAudio2
{
	PerformanceData::PerformanceData( const XAUDIO2_PERFORMANCE_DATA &data )
	{
		AudioCyclesSinceLastQuery = data.AudioCyclesSinceLastQuery;
		TotalCyclesSinceLastQuery = data.TotalCyclesSinceLastQuery;
		MinimumCyclesPerQuantum = data.MinimumCyclesPerQuantum;
		MaximumCyclesPerQuantum = data.MaximumCyclesPerQuantum;
		MemoryUsageInBytes = data.MemoryUsageInBytes;
		CurrentLatencyInSamples = data.CurrentLatencyInSamples;
		GlitchesSinceEngineStarted = data.GlitchesSinceEngineStarted;
		ActiveSourceVoiceCount = data.ActiveSourceVoiceCount;
		TotalSourceVoiceCount = data.TotalSourceVoiceCount;
		ActiveSubmixVoiceCount = data.ActiveSubmixVoiceCount;

#if SLIMDX_XAUDIO2_VERSION < 23
		TotalSubmixVoiceCount = data.TotalSubmixVoiceCount;
#else
		ActiveResamplerCount = data.ActiveResamplerCount;
		ActiveMatrixMixCount = data.ActiveMatrixMixCount;
#endif

		ActiveXmaSourceVoices = data.ActiveXmaSourceVoices;
		ActiveXmaStreams = data.ActiveXmaStreams;
	}

	PerformanceData^ PerformanceData::Clone()
	{
		PerformanceData^ result = gcnew PerformanceData();

		result->AudioCyclesSinceLastQuery = AudioCyclesSinceLastQuery;
		result->TotalCyclesSinceLastQuery = TotalCyclesSinceLastQuery;
		result->MinimumCyclesPerQuantum = MinimumCyclesPerQuantum;
		result->MaximumCyclesPerQuantum = MaximumCyclesPerQuantum;
		result->MemoryUsageInBytes = MemoryUsageInBytes;
		result->CurrentLatencyInSamples = CurrentLatencyInSamples;
		result->GlitchesSinceEngineStarted = GlitchesSinceEngineStarted;
		result->ActiveSourceVoiceCount = ActiveSourceVoiceCount;
		result->TotalSourceVoiceCount = TotalSourceVoiceCount;
		result->ActiveSubmixVoiceCount = ActiveSubmixVoiceCount;

#if SLIMDX_XAUDIO2_VERSION < 23
		result->TotalSubmixVoiceCount = TotalSubmixVoiceCount;
#else
		result->ActiveResamplerCount = ActiveResamplerCount;
		result->ActiveMatrixMixCount = ActiveMatrixMixCount;
#endif

		result->ActiveXmaSourceVoices = ActiveXmaSourceVoices;
		result->ActiveXmaStreams = ActiveXmaStreams;

		return result;
	}

	bool PerformanceData::operator == ( PerformanceData^ left, PerformanceData^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return PerformanceData::Equals( left, right );
	}

	bool PerformanceData::operator != ( PerformanceData^ left, PerformanceData^ right )
	{
		return !( left == right );
	}

	int PerformanceData::GetHashCode()
	{		
		return AudioCyclesSinceLastQuery.GetHashCode() + TotalCyclesSinceLastQuery.GetHashCode() + MinimumCyclesPerQuantum.GetHashCode() +
			MaximumCyclesPerQuantum.GetHashCode() + MemoryUsageInBytes.GetHashCode() + CurrentLatencyInSamples.GetHashCode() +
			GlitchesSinceEngineStarted.GetHashCode() + ActiveSubmixVoiceCount.GetHashCode() + 
#if SLIMDX_XAUDIO2_VERSION < 23
			TotalSubmixVoiceCount.GetHashCode() +
#else
			ActiveResamplerCount.GetHashCode() + ActiveMatrixMixCount.GetHashCode() +
#endif
			ActiveSourceVoiceCount.GetHashCode() + TotalSourceVoiceCount.GetHashCode() + ActiveXmaSourceVoices.GetHashCode() + ActiveXmaStreams.GetHashCode();
	}

	bool PerformanceData::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PerformanceData^>( value ) );
	}

	bool PerformanceData::Equals( PerformanceData^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( value->AudioCyclesSinceLastQuery == AudioCyclesSinceLastQuery && value->TotalCyclesSinceLastQuery == TotalCyclesSinceLastQuery && value->MinimumCyclesPerQuantum == MinimumCyclesPerQuantum &&
			value->MaximumCyclesPerQuantum == MaximumCyclesPerQuantum && value->MemoryUsageInBytes == MemoryUsageInBytes && value->CurrentLatencyInSamples == CurrentLatencyInSamples &&
			value->GlitchesSinceEngineStarted == GlitchesSinceEngineStarted && value->ActiveSubmixVoiceCount == ActiveSubmixVoiceCount &&
#if SLIMDX_XAUDIO2_VERSION < 23
			value->TotalSubmixVoiceCount == TotalSubmixVoiceCount &&
#else
			value->ActiveResamplerCount == ActiveResamplerCount && value->ActiveMatrixMixCount == ActiveMatrixMixCount &&
#endif
			value->ActiveSourceVoiceCount == ActiveSourceVoiceCount && value->TotalSourceVoiceCount == TotalSourceVoiceCount && value->ActiveXmaSourceVoices == ActiveXmaSourceVoices &&
			value->ActiveXmaStreams == ActiveXmaStreams);
	}

	bool PerformanceData::Equals( PerformanceData^ value1, PerformanceData^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}