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

#include "../ComObject.h"

#include "DirectSoundException.h"

#include "Enums.h"
#include "WavesReverb.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	float WavesReverb::InGain::get()
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fInGain;
	}

	void WavesReverb::InGain::set( float value )
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fInGain = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float WavesReverb::ReverbMix::get()
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fReverbMix;
	}

	void WavesReverb::ReverbMix::set( float value )
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fReverbMix = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float WavesReverb::ReverbTime::get()
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fReverbTime;
	}

	void WavesReverb::ReverbTime::set( float value )
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fReverbTime = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float WavesReverb::HighFrequencyRTRatio::get()
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fHighFreqRTRatio;
	}

	void WavesReverb::HighFrequencyRTRatio::set( float value )
	{
		DSFXWavesReverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fHighFreqRTRatio = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}
}
}