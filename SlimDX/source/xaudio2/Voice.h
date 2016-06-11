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

#include "../Result.h"

#include "FilterParameters.h"
#include "VoiceDetails.h"
#include "VoiceSendDescriptor.h"
#include "EffectDescriptor.h"

namespace SlimDX
{
	namespace XAudio2
	{
		public ref class Voice abstract
		{
		private:
			IXAudio2Voice *m_Pointer;

		internal:
			property IXAudio2Voice *InternalPointer
			{ 
				IXAudio2Voice *get() { return m_Pointer; }
				void set( IXAudio2Voice *value ) { m_Pointer = value; }
			}

		protected:
			Voice() { }

		public:
			Result EnableEffect( int effectIndex );
			Result EnableEffect( int effectIndex, int operationSet );

			Result DisableEffect( int effectIndex );
			Result DisableEffect( int effectIndex, int operationSet );

			array<float>^ GetChannelVolumes( int channels );

			Result SetChannelVolumes( int channels, array<float>^ volumes );
			Result SetChannelVolumes( int channels, array<float>^ volumes, int operationSet );

			Result GetEffectParameters( int effectIndex, array<System::Byte>^ parameters );

			generic<typename T> where T : value class
			T GetEffectParameters( int effectIndex );

			Result SetEffectParameters( int effectIndex, array<System::Byte>^ parameters );
			Result SetEffectParameters( int effectIndex, array<System::Byte>^ parameters, int operationSet );

			generic<typename T> where T : value class
			Result SetEffectParameters( int effectIndex, T parameters );
			generic<typename T> where T : value class
			Result SetEffectParameters( int effectIndex, T parameters, int operationSet );

			bool IsEffectEnabled( int effectIndex );

			array<float>^ GetOutputMatrix( int sourceChannels, int destinationChannels );
			array<float>^ GetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels );

			Result SetOutputMatrix( int sourceChannels, int destinationChannels, array<float>^ matrix );
			Result SetOutputMatrix( int sourceChannels, int destinationChannels, array<float>^ matrix, int operationSet );

			Result SetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels, array<float>^ matrix );
			Result SetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels, array<float>^ matrix, int operationSet );

			Result SetEffectChain( array<EffectDescriptor>^ effects );
			
#if SLIMDX_XAUDIO2_VERSION < 24
			Result SetOutputVoices( array<Voice^>^ outputVoices );
#else
			Result SetOutputVoices( array<VoiceSendDescriptor>^ outputVoices );
#endif
			property FilterParameters FilterParameters
			{
				SlimDX::XAudio2::FilterParameters get();
				void set( SlimDX::XAudio2::FilterParameters value );
			}

			property VoiceDetails VoiceDetails
			{
				SlimDX::XAudio2::VoiceDetails get();
			}

			property float Volume
			{
				float get();
				void set( float value );
			}
		};
	}
}