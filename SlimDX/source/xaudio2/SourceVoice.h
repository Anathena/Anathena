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

#include "Voice.h"
#include "VoiceCallback.h"
#include "ContextEventArgs.h"
#include "ErrorEventArgs.h"
#include "StartProcessingEventArgs.h"
#include "AudioBuffer.h"
#include "VoiceState.h"

namespace SlimDX
{
	namespace Multimedia
	{
		ref class WaveFormat;
	}

	namespace XAudio2
	{
		ref class XAudio2;

		public ref class SourceVoice : Voice
		{
		private:
			VoiceCallbackShim *callback;

			property IXAudio2SourceVoice *SourcePointer
			{
				IXAudio2SourceVoice *get() { return reinterpret_cast<IXAudio2SourceVoice*>( InternalPointer ); }
			}

		internal:
			void InvokeBufferEnd( ContextEventArgs^ e ) { OnBufferEnd( e ); }
			void InvokeBufferStart( ContextEventArgs^ e ) { OnBufferStart( e ); }
			void InvokeLoopEnd( ContextEventArgs^ e ) { OnLoopEnd( e ); }
			void InvokeStreamEnd() { OnStreamEnd( System::EventArgs::Empty ); }
			void InvokeVoiceError( ErrorEventArgs^ e ) { OnVoiceError( e ); }
			void InvokeVoiceProcessingPassEnd() { OnVoiceProcessingPassEnd( System::EventArgs::Empty ); }
			void InvokeVoiceProcessingPassStart( int bytesRequired ) { OnVoiceProcessingPassStart( bytesRequired ); }

		protected:
			void OnBufferEnd( ContextEventArgs^ e );
			void OnBufferStart( ContextEventArgs^ e );
			void OnLoopEnd( ContextEventArgs^ e );
			void OnStreamEnd( System::EventArgs^ e );
			void OnVoiceError( ErrorEventArgs^ e );
			void OnVoiceProcessingPassStart( int bytesRequired );
			void OnVoiceProcessingPassEnd( System::EventArgs^ e );

		public:
			SourceVoice( XAudio2^ device, SlimDX::Multimedia::WaveFormat^ sourceFormat, VoiceFlags flags, float maximumFrequencyRatio );
			SourceVoice( XAudio2^ device, SlimDX::Multimedia::WaveFormat^ sourceFormat, VoiceFlags flags );
			SourceVoice( XAudio2^ device, SlimDX::Multimedia::WaveFormat^ sourceFormat );
			virtual ~SourceVoice();

			Result Discontinuity();
			Result FlushSourceBuffers();

			Result ExitLoop();
			Result ExitLoop( int operationSet );

			Result Start();
			Result Start( PlayFlags flags );
			Result Start( PlayFlags flag, int operationSet );

			Result Stop();
			Result Stop( PlayFlags flags );
			Result Stop( PlayFlags flags, int operationSet );

			Result SubmitSourceBuffer( AudioBuffer^ buffer );
			Result SubmitSourceBuffer( AudioBuffer^ buffer, array<int>^ decodedPacketCumulativeBytes );
			
#if SLIMDX_XAUDIO2_VERSION >= 24
			Result SetSourceSampleRate( int sampleRate );
#endif

			property VoiceState State
			{
				VoiceState get();
			}

			property float FrequencyRatio
			{
				float get();
				void set( float value );
			}

			event System::EventHandler<ContextEventArgs^>^ BufferEnd;
			event System::EventHandler<ContextEventArgs^>^ BufferStart;
			event System::EventHandler<ContextEventArgs^>^ LoopEnd;
			event System::EventHandler<StartProcessingEventArgs^>^ VoiceProcessingPassStart;

			event System::EventHandler<ErrorEventArgs^>^ VoiceError;

			event System::EventHandler^ StreamEnd;
			event System::EventHandler^ VoiceProcessingPassEnd;
		};
	}
}