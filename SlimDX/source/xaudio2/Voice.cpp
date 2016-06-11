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

#include "../stack_array.h"

#include "XAudio2Exception.h"
#include "Voice.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace XAudio2
{
	Result Voice::DisableEffect( int effectIndex )
	{
		HRESULT hr = InternalPointer->DisableEffect( effectIndex );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::DisableEffect( int effectIndex, int operationSet )
	{
		HRESULT hr = InternalPointer->DisableEffect( effectIndex, operationSet );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::EnableEffect( int effectIndex )
	{
		HRESULT hr = InternalPointer->EnableEffect( effectIndex );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::EnableEffect( int effectIndex, int operationSet )
	{
		HRESULT hr = InternalPointer->EnableEffect( effectIndex, operationSet );
		return RECORD_XAUDIO2( hr );
	}

	array<float>^ Voice::GetChannelVolumes( int channels )
	{
		array<float>^ results = gcnew array<float>( channels );
		pin_ptr<float> pinResults = &results[0];

		InternalPointer->GetChannelVolumes( channels, reinterpret_cast<float*>( pinResults ) );

		return results;
	}

	Result Voice::SetChannelVolumes( int channels, array<float>^ volumes )
	{
		pin_ptr<float> pinVolumes = &volumes[0];

		HRESULT hr = InternalPointer->SetChannelVolumes( channels, reinterpret_cast<const float*>( pinVolumes ) );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::SetChannelVolumes( int channels, array<float>^ volumes, int operationSet )
	{
		pin_ptr<float> pinVolumes = &volumes[0];

		HRESULT hr = InternalPointer->SetChannelVolumes( channels, reinterpret_cast<const float*>( pinVolumes ), operationSet );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::GetEffectParameters( int effectIndex, array<Byte>^ parameters )
	{
		pin_ptr<Byte> pinParams = &parameters[0];

		HRESULT hr = InternalPointer->GetEffectParameters( effectIndex, pinParams, parameters->Length );
		return RECORD_XAUDIO2( hr );
	}

	generic<typename T>
	T Voice::GetEffectParameters( int effectIndex )
	{
		T results;
		HRESULT hr = InternalPointer->GetEffectParameters( effectIndex, &results, Marshal::SizeOf( T::typeid ) );
		RECORD_XAUDIO2( hr );

		return results;
	}

	Result Voice::SetEffectParameters( int effectIndex, array<Byte>^ parameters )
	{
		pin_ptr<Byte> pinParams = &parameters[0];

		HRESULT hr = InternalPointer->SetEffectParameters( effectIndex, pinParams, parameters->Length );
		return RECORD_XAUDIO2( hr );
	}

	Result Voice::SetEffectParameters( int effectIndex, array<Byte>^ parameters, int operationSet )
	{
		pin_ptr<Byte> pinParams = &parameters[0];

		HRESULT hr = InternalPointer->SetEffectParameters( effectIndex, pinParams, parameters->Length, operationSet );
		return RECORD_XAUDIO2( hr );
	}

	generic<typename T>
	Result Voice::SetEffectParameters( int effectIndex, T parameters )
	{
		HRESULT hr = InternalPointer->SetEffectParameters( effectIndex, &parameters, Marshal::SizeOf( T::typeid ) );
		return RECORD_XAUDIO2( hr );
	}

	generic<typename T>
	Result Voice::SetEffectParameters( int effectIndex, T parameters, int operationSet )
	{
		HRESULT hr = InternalPointer->SetEffectParameters( effectIndex, &parameters, Marshal::SizeOf( T::typeid ), operationSet );
		return RECORD_XAUDIO2( hr );
	}

	bool Voice::IsEffectEnabled( int effectIndex )
	{
		BOOL result = FALSE;

		InternalPointer->GetEffectState( effectIndex, &result );

		return result > 0;
	}

	array<float>^ Voice::GetOutputMatrix( int sourceChannels, int destinationChannels )
	{
		return GetOutputMatrix( nullptr, sourceChannels, destinationChannels );
	}

	array<float>^ Voice::GetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels )
	{
		array<float>^ results = gcnew array<float>( sourceChannels * destinationChannels );
		pin_ptr<float> pinResults = &results[0];

		IXAudio2Voice *voice = destinationVoice == nullptr ? NULL : destinationVoice->InternalPointer;

		InternalPointer->GetOutputMatrix( voice, sourceChannels,
			destinationChannels, reinterpret_cast<float*>( pinResults ) );

		return results;
	}

	Result Voice::SetEffectChain( array<EffectDescriptor>^ effects )
	{
		stack_array<XAUDIO2_EFFECT_DESCRIPTOR> descriptors = stackalloc(XAUDIO2_EFFECT_DESCRIPTOR, effects->Length);
		for (int i = 0; i < effects->Length; i++)
			descriptors[i] = effects[i].ToUnmanaged();

		XAUDIO2_EFFECT_CHAIN chain;
		chain.EffectCount = effects->Length;
		chain.pEffectDescriptors = &descriptors[0];

		HRESULT hr = InternalPointer->SetEffectChain( &chain );
		
		for (int i = 0; i < effects->Length; i++)
			effects[i].Free();

		return RECORD_XAUDIO2( hr );
	}

	Result Voice::SetOutputMatrix( int sourceChannels, int destinationChannels, array<float>^ matrix )
	{
		return SetOutputMatrix( nullptr, sourceChannels, destinationChannels, matrix );
	}

	Result Voice::SetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels, array<float>^ matrix )
	{
		pin_ptr<float> pinMatrix = &matrix[0];

		IXAudio2Voice *voice = destinationVoice == nullptr ? NULL : destinationVoice->InternalPointer;

		HRESULT hr = InternalPointer->SetOutputMatrix( voice, sourceChannels,
			destinationChannels, reinterpret_cast<const float*>( pinMatrix ) );

		return RECORD_XAUDIO2( hr );
	}

	Result Voice::SetOutputMatrix( int sourceChannels, int destinationChannels, array<float>^ matrix, int operationSet )
	{
		return SetOutputMatrix( nullptr, sourceChannels, destinationChannels, matrix, operationSet );
	}

	Result Voice::SetOutputMatrix( Voice^ destinationVoice, int sourceChannels, int destinationChannels, array<float>^ matrix, int operationSet )
	{
		pin_ptr<float> pinMatrix = &matrix[0];

		IXAudio2Voice *voice = destinationVoice == nullptr ? NULL : destinationVoice->InternalPointer;

		HRESULT hr = InternalPointer->SetOutputMatrix( voice, sourceChannels,
			destinationChannels, reinterpret_cast<const float*>( pinMatrix ), operationSet );

		return RECORD_XAUDIO2( hr );
	}

#if SLIMDX_XAUDIO2_VERSION < 24
	Result Voice::SetOutputVoices( array<Voice^>^ outputVoices )
#else
	Result Voice::SetOutputVoices( array<VoiceSendDescriptor>^ outputVoices )
#endif
	{
		HRESULT hr;

		if( outputVoices == nullptr || outputVoices->Length == 0 )
			hr = InternalPointer->SetOutputVoices( NULL );
		else
		{
#if SLIMDX_XAUDIO2_VERSION < 24
			stack_array<IXAudio2Voice*> voices = stackalloc( IXAudio2Voice*, outputVoices->Length );
			for( int i = 0; i < outputVoices->Length; i++ )
				voices[i] = outputVoices[i]->InternalPointer;

			XAUDIO2_VOICE_SENDS sendList;
			sendList.OutputCount = outputVoices->Length;
			sendList.pOutputVoices = &voices[0];
#else
			stack_array<XAUDIO2_SEND_DESCRIPTOR> voices = stackalloc( XAUDIO2_SEND_DESCRIPTOR, outputVoices->Length );
			for( int i = 0; i < outputVoices->Length; i++ )
				voices[i] = outputVoices[i].CreateNativeVersion();

			XAUDIO2_VOICE_SENDS sendList;
			sendList.SendCount = outputVoices->Length;
			sendList.pSends = &voices[0];
#endif

			hr = InternalPointer->SetOutputVoices( &sendList );
		}

		return RECORD_XAUDIO2( hr );
	}

	SlimDX::XAudio2::FilterParameters Voice::FilterParameters::get()
	{
		XAUDIO2_FILTER_PARAMETERS parameters;

		InternalPointer->GetFilterParameters( &parameters );

		SlimDX::XAudio2::FilterParameters result;
		result.Frequency = parameters.Frequency;
		result.OneOverQ = parameters.OneOverQ;
		result.Type = static_cast<FilterType>( parameters.Type );

		return result;
	}

	void Voice::FilterParameters::set( SlimDX::XAudio2::FilterParameters value )
	{
		pin_ptr<SlimDX::XAudio2::FilterParameters> pinValue = &value;

		HRESULT hr = InternalPointer->SetFilterParameters( reinterpret_cast<const XAUDIO2_FILTER_PARAMETERS*>( pinValue ) );
		RECORD_XAUDIO2( hr );
	}

	SlimDX::XAudio2::VoiceDetails Voice::VoiceDetails::get()
	{
		XAUDIO2_VOICE_DETAILS details;

		InternalPointer->GetVoiceDetails( &details );

		SlimDX::XAudio2::VoiceDetails result;
		result.CreationFlags = static_cast<VoiceFlags>( details.CreationFlags );
		result.InputChannels = details.InputChannels;
		result.InputSampleRate = details.InputSampleRate;

		return result;
	}

	float Voice::Volume::get()
	{
		float result;
		InternalPointer->GetVolume( &result );

		return result;
	}

	void Voice::Volume::set( float value )
	{
		HRESULT hr = InternalPointer->SetVolume( value );
		RECORD_XAUDIO2( hr );
	}
}
}