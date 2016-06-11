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
#include "DistortionEffect.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	float DistortionEffect::Gain::get()
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fGain;
	}

	void DistortionEffect::Gain::set( float value )
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fGain = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float DistortionEffect::Edge::get()
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fEdge;
	}

	void DistortionEffect::Edge::set( float value )
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fEdge = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float DistortionEffect::PostEQCenterFrequency::get()
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fPostEQCenterFrequency;
	}

	void DistortionEffect::PostEQCenterFrequency::set( float value )
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fPostEQCenterFrequency = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float DistortionEffect::PostEQBandwidth::get()
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fPostEQBandwidth;
	}

	void DistortionEffect::PostEQBandwidth::set( float value )
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fPostEQBandwidth = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float DistortionEffect::PreLowPassCutoff::get()
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fPreLowpassCutoff;
	}

	void DistortionEffect::PreLowPassCutoff::set( float value )
	{
		DSFXDistortion param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fPreLowpassCutoff = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}
}
}