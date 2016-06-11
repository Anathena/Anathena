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

#include "../ComObject.h"

#include "XAudio2Exception.h"

#include "XAudio2.h"
#include "MasteringVoice.h"

using namespace System;

namespace SlimDX
{
namespace XAudio2
{
	MasteringVoice::MasteringVoice( XAudio2^ device, int inputChannels, int inputSampleRate, int deviceIndex )
	{
		IXAudio2MasteringVoice *pointer;

		HRESULT hr = device->InternalPointer->CreateMasteringVoice( &pointer, inputChannels, inputSampleRate, 0, deviceIndex );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	MasteringVoice::MasteringVoice( XAudio2^ device, int inputChannels, int inputSampleRate )
	{
		IXAudio2MasteringVoice *pointer;

		HRESULT hr = device->InternalPointer->CreateMasteringVoice( &pointer, inputChannels, inputSampleRate, 0, 0 );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	MasteringVoice::MasteringVoice( XAudio2^ device, int inputChannels )
	{
		IXAudio2MasteringVoice *pointer;

		HRESULT hr = device->InternalPointer->CreateMasteringVoice( &pointer, inputChannels, XAUDIO2_DEFAULT_SAMPLERATE, 0, 0 );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	MasteringVoice::MasteringVoice( XAudio2^ device )
	{
		IXAudio2MasteringVoice *pointer;

		HRESULT hr = device->InternalPointer->CreateMasteringVoice( &pointer, XAUDIO2_DEFAULT_CHANNELS, XAUDIO2_DEFAULT_SAMPLERATE, 0, 0 );

		if( RECORD_XAUDIO2( hr ).IsFailure )
			throw gcnew XAudio2Exception( Result::Last );

		InternalPointer = pointer;
	}

	MasteringVoice::~MasteringVoice()
	{
		if( InternalPointer != NULL )
			InternalPointer->DestroyVoice();
		InternalPointer = NULL;
	}
}
}