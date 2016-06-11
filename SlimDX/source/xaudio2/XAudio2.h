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

#include "../ComObject.h"
#include "../multimedia/WaveFormat.h"
#include "../multimedia/WaveFormatExtensible.h"

#include "DeviceDetails.h"
#include "PerformanceData.h"
#include "DebugConfiguration.h"
#include "ErrorEventArgs.h"
#include "EngineCallback.h"

namespace SlimDX
{
	namespace XAudio2
	{
		public ref class XAudio2 : public ComObject
		{
			COMOBJECT(IXAudio2, XAudio2);

		private:
			EngineCallbackShim *callback;

		internal:
			void InvokeCriticalError( ErrorEventArgs^ e ) { OnCriticalError( e ); }
			void InvokeProcessingPassEnd() { OnProcessingPassEnd( System::EventArgs::Empty ); }
			void InvokeProcessingPassStart() { OnProcessingPassStart( System::EventArgs::Empty); }

		protected:
			void OnCriticalError( ErrorEventArgs^ e );
			void OnProcessingPassEnd( System::EventArgs^ e );
			void OnProcessingPassStart( System::EventArgs^ e );

		public:
			XAudio2();
			XAudio2( XAudio2Flags flags, ProcessorSpecifier processor );
			virtual ~XAudio2() { if( callback != NULL ) delete callback; callback = NULL; }

			static float AmplitudeRatioToDecibels( float volume );
			static float CutoffFrequencyToRadians( float cutoffFrequency, int sampleRate );
			static float DecibelsToAmplitudeRatio( float decibels );
			static float FrequencyRatioToSemitones( float frequencyRatio );
			static float RadiansToCutoffFrequency( float radians, float sampleRate );
			static float SemitonesToFrequencyRatio( float semitones );

			Result StartEngine();
			void StopEngine();

			Result CommitChanges();
			Result CommitChanges( int operationSet );

			DeviceDetails^ GetDeviceDetails( int index );

			void SetDebugConfiguration( DebugConfiguration configuration );

			property int DeviceCount
			{
				int get();
			}

			property PerformanceData^ PerformanceData
			{
				SlimDX::XAudio2::PerformanceData^ get();
			}

			event System::EventHandler<ErrorEventArgs^>^ CriticalError;
			event System::EventHandler^ ProcessingPassEnd;
			event System::EventHandler^ ProcessingPassStart;

			literal int MaximumBufferBytes = XAUDIO2_MAX_BUFFER_BYTES;
			literal int MaximumQueuedBuffers = XAUDIO2_MAX_QUEUED_BUFFERS;
			literal int MaximumAudioChannels = XAUDIO2_MAX_AUDIO_CHANNELS;
			literal int MinimumSampleRate = XAUDIO2_MIN_SAMPLE_RATE;
			literal int MaximumSampleRate = XAUDIO2_MAX_SAMPLE_RATE;
			literal float MaximumVolumeLevel = XAUDIO2_MAX_VOLUME_LEVEL;
			literal float MinimumFrequencyRatio = XAUDIO2_MIN_FREQ_RATIO;
			literal float MaximumFrequencyRatio = XAUDIO2_MAX_FREQ_RATIO;
			literal float DefaultFrequencyRatio = XAUDIO2_DEFAULT_FREQ_RATIO;
			literal float MaximumFilterOneOverQ = XAUDIO2_MAX_FILTER_ONEOVERQ;
			literal float MaximumFilterFrequency = XAUDIO2_MAX_FILTER_FREQUENCY;
			literal int MaximumLoopCount = XAUDIO2_MAX_LOOP_COUNT;

			literal int CommitNow = XAUDIO2_COMMIT_NOW;
			literal int CommitAll = XAUDIO2_COMMIT_ALL;
			literal int NoLoopRegion = XAUDIO2_NO_LOOP_REGION;
			literal int LoopInfinite = XAUDIO2_LOOP_INFINITE;
			literal int DefaultChannels = XAUDIO2_DEFAULT_CHANNELS;
			literal int DefaultSampleRate = XAUDIO2_DEFAULT_SAMPLERATE;

			static property System::Guid FormatSubtypePcm
			{
				System::Guid get();
			}

			static property System::Guid FormatSubtypeAdpcm
			{
				System::Guid get();
			}

			static property System::Guid FormatSubtypeIeee
			{
				System::Guid get();
			}
		};
	}
}