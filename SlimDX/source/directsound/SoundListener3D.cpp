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
#include "../math/Vector3.h"

#include "DirectSoundException.h"

#include "DirectSound.h"

#include "SoundBuffer.h"
#include "SoundListener3D.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	SoundListener3D::SoundListener3D( SoundBuffer^ soundBuffer )
	{
		IDirectSound3DListener8* listener;

		HRESULT hr = soundBuffer->InternalPointer->QueryInterface( IID_IDirectSound3DListener8, reinterpret_cast<void**>( &listener ) );
		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );
		
		Construct( listener );
	}

	Result SoundListener3D::CommitDeferredSettings()
	{
		HRESULT hr = InternalPointer->CommitDeferredSettings();
		return RECORD_DSOUND( hr );
	}

	float SoundListener3D::DistanceFactor::get()
	{
		float distance = 0.0f;
		HRESULT hr = InternalPointer->GetDistanceFactor( &distance );
		RECORD_DSOUND( hr );

		return distance;
	}

	void SoundListener3D::DistanceFactor::set( float value )
	{
		HRESULT hr = InternalPointer->SetDistanceFactor( value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	float SoundListener3D::DopplerFactor::get()
	{
		float doppler = 0.0f;
		HRESULT hr = InternalPointer->GetDopplerFactor( &doppler );
		RECORD_DSOUND( hr );

		return doppler;
	}

	void SoundListener3D::DopplerFactor::set( float value )
	{
		HRESULT hr = InternalPointer->SetDopplerFactor( value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	float SoundListener3D::RolloffFactor::get()
	{
		float rolloff = 0.0f;
		HRESULT hr = InternalPointer->GetRolloffFactor( &rolloff );
		RECORD_DSOUND( hr );

		return rolloff;
	}

	void SoundListener3D::RolloffFactor::set( float value )
	{
		HRESULT hr = InternalPointer->SetRolloffFactor( value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundListener3D::FrontOrientation::get()
	{
		D3DVECTOR front = D3DVECTOR();
		D3DVECTOR top = D3DVECTOR();
		HRESULT hr = InternalPointer->GetOrientation( &front, &top );

		if( RECORD_DSOUND( hr ).IsFailure )
			return Vector3::Zero;

		return Vector3( front.x, front.y, front.z );
	}

	Vector3 SoundListener3D::TopOrientation::get()
	{
		D3DVECTOR front = D3DVECTOR();
		D3DVECTOR top = D3DVECTOR();
		HRESULT hr = InternalPointer->GetOrientation( &front, &top );

		if( RECORD_DSOUND( hr ).IsFailure )
			return Vector3::Zero;

		return Vector3( top.x, top.y, top.z );
	}

	void SoundListener3D::FrontOrientation::set( Vector3 value )
	{
		D3DVECTOR front = D3DVECTOR();
		D3DVECTOR top = D3DVECTOR();
		HRESULT hr = InternalPointer->GetOrientation( &front, &top );

		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		hr = InternalPointer->SetOrientation( value.X, value.Y, value.Z, top.x, top.y, top.z,
			Deferred ? DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	void SoundListener3D::TopOrientation::set( Vector3 value )
	{
		D3DVECTOR front = D3DVECTOR();
		D3DVECTOR top = D3DVECTOR();
		HRESULT hr = InternalPointer->GetOrientation( &front, &top );

		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		hr = InternalPointer->SetOrientation( front.x, front.y, front.z, value.X, value.Y, value.Z,
			Deferred ? DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundListener3D::Position::get()
	{
		D3DVECTOR position = D3DVECTOR();
		HRESULT hr = InternalPointer->GetPosition( &position );
		RECORD_DSOUND( hr );

		return Vector3( position.x, position.y, position.z );
	}

	void SoundListener3D::Position::set( Vector3 value )
	{
		HRESULT hr = InternalPointer->SetPosition( value.X, value.Y, value.Z, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundListener3D::Velocity::get()
	{
		D3DVECTOR velocity = D3DVECTOR();
		HRESULT hr = InternalPointer->GetVelocity( &velocity );
		RECORD_DSOUND( hr );

		return Vector3( velocity.x, velocity.y, velocity.z );
	}

	void SoundListener3D::Velocity::set( Vector3 value )
	{
		HRESULT hr = InternalPointer->SetVelocity( value.X, value.Y, value.Z, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}
}
}