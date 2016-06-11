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
#include "EchoEffect.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	float EchoEffect::LeftDelay::get()
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fLeftDelay;
	}

	void EchoEffect::LeftDelay::set( float value )
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fLeftDelay = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float EchoEffect::RightDelay::get()
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fRightDelay;
	}

	void EchoEffect::RightDelay::set( float value )
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fRightDelay = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float EchoEffect::Feedback::get()
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fFeedback;
	}

	void EchoEffect::Feedback::set( float value )
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fFeedback = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float EchoEffect::WetDryMix::get()
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.fWetDryMix;
	}

	void EchoEffect::WetDryMix::set( float value )
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fWetDryMix = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	int EchoEffect::PanDelay::get()
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return param.lPanDelay;
	}

	void EchoEffect::PanDelay::set( int value )
	{
		DSFXEcho param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.lPanDelay = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}
}
}