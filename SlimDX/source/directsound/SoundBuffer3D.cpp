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
#include "SoundBuffer3D.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	SoundBuffer3D::SoundBuffer3D( SoundBuffer^ soundBuffer )
	{
		IDirectSound3DBuffer8* buffer;

		HRESULT hr = soundBuffer->InternalPointer->QueryInterface( IID_IDirectSound3DBuffer8, reinterpret_cast<void**>( &buffer ) );
		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );
		
		Construct( buffer );
	}

	Buffer3DSettings SoundBuffer3D::AllParameters::get()
	{
		DS3DBUFFER result;
		result.dwSize = sizeof( DS3DBUFFER );
		HRESULT hr = InternalPointer->GetAllParameters( &result );
		RECORD_DSOUND( hr );

		return Buffer3DSettings( result );
	}

	void SoundBuffer3D::AllParameters::set( Buffer3DSettings value )
	{
		DS3DBUFFER result = value.ToUnmanaged();
		HRESULT hr = InternalPointer->SetAllParameters( &result, Deferred ? DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer3D::InsideConeAngle::get()
	{
		DWORD inside = 0;
		HRESULT hr = InternalPointer->GetConeAngles( &inside, NULL );
		RECORD_DSOUND( hr );

		return inside; 
	}

	void SoundBuffer3D::InsideConeAngle::set( int value )
	{
		HRESULT hr = InternalPointer->SetConeAngles( value, NULL, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer3D::OutsideConeAngle::get()
	{
		DWORD outside = 0;
		HRESULT hr = InternalPointer->GetConeAngles( NULL, &outside );
		RECORD_DSOUND( hr );

		return outside; 
	}

	void SoundBuffer3D::OutsideConeAngle::set( int value )
	{
		HRESULT hr = InternalPointer->SetConeAngles( NULL, value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundBuffer3D::ConeOrientation::get()
	{
		D3DVECTOR orientation = D3DVECTOR();
		HRESULT hr = InternalPointer->GetConeOrientation( &orientation );
		RECORD_DSOUND( hr );

		return Vector3( orientation.x, orientation.y, orientation.z );
	}

	void SoundBuffer3D::ConeOrientation::set( Vector3 value )
	{
		HRESULT hr = InternalPointer->SetConeOrientation( value.X, value.Y, value.Z, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	int SoundBuffer3D::ConeOutsideVolume::get()
	{
		LONG volume = 0;
		HRESULT hr = InternalPointer->GetConeOutsideVolume( &volume );
		RECORD_DSOUND( hr );

		return static_cast<int>( volume );
	}

	void SoundBuffer3D::ConeOutsideVolume::set( int value )
	{
		HRESULT hr = InternalPointer->SetConeOutsideVolume( static_cast<LONG>( value ), Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	float SoundBuffer3D::MaxDistance::get()
	{
		float maxDistance = 0.0f;
		HRESULT hr = InternalPointer->GetMaxDistance( &maxDistance );
		RECORD_DSOUND( hr );

		return maxDistance;
	}

	void SoundBuffer3D::MaxDistance::set( float value )
	{
		HRESULT hr = InternalPointer->SetMaxDistance( value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	float SoundBuffer3D::MinDistance::get()
	{
		float minDistance = 0.0f;
		HRESULT hr = InternalPointer->GetMinDistance( &minDistance );
		RECORD_DSOUND( hr );

		return minDistance;
	}

	void SoundBuffer3D::MinDistance::set( float value )
	{
		HRESULT hr = InternalPointer->SetMinDistance( value, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Mode3D SoundBuffer3D::Mode::get()
	{
		DWORD mode = 0;
		HRESULT hr = InternalPointer->GetMode( &mode );
		RECORD_DSOUND( hr );
	
		return static_cast<Mode3D>( mode );
	}

	void SoundBuffer3D::Mode::set( Mode3D value )
	{
		HRESULT hr = InternalPointer->SetMode( static_cast<DWORD>( value ), Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundBuffer3D::Position::get()
	{
		D3DVECTOR position = D3DVECTOR();
		HRESULT hr = InternalPointer->GetPosition( &position );
		RECORD_DSOUND( hr );

		return Vector3( position.x, position.y, position.z );
	}

	void SoundBuffer3D::Position::set( Vector3 value )
	{
		HRESULT hr = InternalPointer->SetPosition( value.X, value.Y, value.Z, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}

	Vector3 SoundBuffer3D::Velocity::get()
	{
		D3DVECTOR velocity = D3DVECTOR();
		HRESULT hr = InternalPointer->GetVelocity( &velocity );
		RECORD_DSOUND( hr );

		return Vector3( velocity.x, velocity.y, velocity.z );
	}

	void SoundBuffer3D::Velocity::set( Vector3 value )
	{
		HRESULT hr = InternalPointer->SetVelocity( value.X, value.Y, value.Z, Deferred ?  DS3D_DEFERRED : DS3D_IMMEDIATE );
		RECORD_DSOUND( hr );
	}
}
}