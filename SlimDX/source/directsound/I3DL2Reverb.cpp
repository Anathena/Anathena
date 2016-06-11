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

#include "I3DL2Reverb.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	int I3DL2Reverb::Room::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return param.lRoom;
	}

	void I3DL2Reverb::Room::set( int value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.lRoom = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	int I3DL2Reverb::RoomHF::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return param.lRoomHF;
	}

	void I3DL2Reverb::RoomHF::set( int value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.lRoomHF = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	int I3DL2Reverb::Reflections::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return param.lReflections;
	}

	void I3DL2Reverb::Reflections::set( int value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.lReflections = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	int I3DL2Reverb::Reverb::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0;

		return param.lReverb;
	}

	void I3DL2Reverb::Reverb::set( int value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.lReverb = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::RoomRolloffFactor::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flRoomRolloffFactor;
	}

	void I3DL2Reverb::RoomRolloffFactor::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flRoomRolloffFactor = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::DecayTime::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flDecayTime;
	}

	void I3DL2Reverb::DecayTime::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flDecayTime = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::DecayHFRatio::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flDecayHFRatio;
	}

	void I3DL2Reverb::DecayHFRatio::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flDecayHFRatio = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::ReflectionsDelay::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flReflectionsDelay;
	}

	void I3DL2Reverb::ReflectionsDelay::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flReflectionsDelay = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::ReverbDelay::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flReverbDelay;
	}

	void I3DL2Reverb::ReverbDelay::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flReverbDelay = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::Diffusion::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flDiffusion;
	}

	void I3DL2Reverb::Diffusion::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flDiffusion = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::Density::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flDensity;
	}

	void I3DL2Reverb::Density::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flDensity = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	float I3DL2Reverb::HFReference::get()
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return 0.0f;

		return param.flHFReference;
	}

	void I3DL2Reverb::HFReference::set( float value )
	{
		DSFXI3DL2Reverb param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.flHFReference = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	int I3DL2Reverb::Quality::get()
	{
		LONG result;
		HRESULT hr = InternalPointer->GetQuality( &result );
		RECORD_DSOUND( hr );

		return static_cast<int>( result );
	}

	void I3DL2Reverb::Quality::set( int value )
	{
		HRESULT hr = InternalPointer->SetQuality( static_cast<LONG>( value ) );
		RECORD_DSOUND( hr );
	}

	I3DL2ReverbPreset I3DL2Reverb::Preset::get()
	{
		DWORD result;
		HRESULT hr = InternalPointer->GetPreset( &result );
		RECORD_DSOUND( hr );

		return static_cast<I3DL2ReverbPreset>( result );
	}

	void I3DL2Reverb::Preset::set( I3DL2ReverbPreset value )
	{
		HRESULT hr = InternalPointer->SetPreset( static_cast<DWORD>( value ) );
		RECORD_DSOUND( hr );
	}
}
}