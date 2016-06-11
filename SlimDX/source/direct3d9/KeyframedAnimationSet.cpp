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
#include <d3d9.h>
#include <d3dx9.h>
#include <vcclr.h>

#include "../stack_array.h"
#include "../ComObject.h"
#include "../DataStream.h"

#include "../Math/Vector3.h"

#include "Device.h"
#include "Mesh.h"
#include "ProgressiveMesh.h"
#include "PatchMesh.h"
#include "SkinInfo.h"
#include "AnimationFrame.h"
#include "KeyframedAnimationSet.h"
#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	KeyframedAnimationSet::KeyframedAnimationSet( String^ name, double ticksPerSecond, SlimDX::Direct3D9::PlaybackType playbackType,
		int animationCount, array<CallbackKey>^ callbackKeys )
	{
		LPD3DXKEYFRAMEDANIMATIONSET pointer;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		int count = callbackKeys->Length;

		stack_array<D3DXKEY_CALLBACK> keys = stackalloc( D3DXKEY_CALLBACK, count );
		for( int i = 0; i < count; i++ )
		{
			keys[i].Time = callbackKeys[i].Time;
			keys[i].pCallbackData = callbackKeys[i].Data.ToPointer();
		}

		HRESULT hr = D3DXCreateKeyframedAnimationSet( reinterpret_cast<LPCSTR>( pinnedName ), ticksPerSecond, static_cast<D3DXPLAYBACK_TYPE>( playbackType ),
			animationCount, count, &keys[0], &pointer );

		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(pointer);
	}

	DataStream^ KeyframedAnimationSet::Compress( float lossiness )
	{
		LPD3DXBUFFER data;

		HRESULT hr = InternalPointer->Compress( D3DXCOMPRESS_DEFAULT, lossiness, NULL, &data);
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( data );
	}

	DataStream^ KeyframedAnimationSet::Compress( float lossiness, Frame^ frame )
	{
		LPD3DXBUFFER data;

		HRESULT hr = InternalPointer->Compress( D3DXCOMPRESS_DEFAULT, lossiness, frame->Pointer, &data);
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( data );
	}

	CallbackKey KeyframedAnimationSet::GetCallbackKey( int animation )
	{
		D3DXKEY_CALLBACK key;

		HRESULT hr = InternalPointer->GetCallbackKey( animation, &key );
		RECORD_D3D9( hr );

		return CallbackKey( key );
	}

	array<CallbackKey>^ KeyframedAnimationSet::GetCallbackKeys()
	{
		int count = CallbackKeyCount;
		stack_array<D3DXKEY_CALLBACK> keys = stackalloc( D3DXKEY_CALLBACK, count );

		HRESULT hr = InternalPointer->GetCallbackKeys( &keys[0] );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			return nullptr;
		}

		array<CallbackKey>^ results = gcnew array<CallbackKey>( count );
		for( int i = 0; i < count; i++ )
			results[i] = CallbackKey( keys[i] );

		return results;
	}

	Result KeyframedAnimationSet::SetCallbackKey( int animation, CallbackKey callbackKey )
	{
		D3DXKEY_CALLBACK key;
		key.Time = callbackKey.Time;
		key.pCallbackData = callbackKey.Data.ToPointer();

		HRESULT hr = InternalPointer->SetCallbackKey( animation, &key );
		return RECORD_D3D9( hr );
	}

	RotationKey KeyframedAnimationSet::GetRotationKey( int animation, int key )
	{
		D3DXKEY_QUATERNION qkey;

		HRESULT hr = InternalPointer->GetRotationKey( animation, key, &qkey );
		RECORD_D3D9( hr );

		return RotationKey( qkey );
	}

	array<RotationKey>^ KeyframedAnimationSet::GetRotationKeys( int animation )
	{
		int count = GetRotationKeyCount( animation );
		stack_array<D3DXKEY_QUATERNION> keys = stackalloc( D3DXKEY_QUATERNION, count );

		HRESULT hr = InternalPointer->GetRotationKeys( animation, &keys[0] );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			return nullptr;
		}

		array<RotationKey>^ results = gcnew array<RotationKey>( count );
		for( int i = 0; i < count; i++ )
			results[i] = RotationKey( keys[i] );

		return results;
	}

	Result KeyframedAnimationSet::SetRotationKey( int animation, int key, RotationKey callbackKey )
	{
		D3DXKEY_QUATERNION qkey;
		qkey.Time = callbackKey.Time;
		qkey.Value = D3DXQUATERNION( callbackKey.Value.X, callbackKey.Value.Y, callbackKey.Value.Z, callbackKey.Value.W );

		HRESULT hr = InternalPointer->SetRotationKey( animation, key, &qkey );
		return RECORD_D3D9( hr );
	}

	Result KeyframedAnimationSet::UnregisterRotationKey( int animation, int key )
	{
		HRESULT hr = InternalPointer->UnregisterRotationKey( animation, key );
		return RECORD_D3D9( hr );
	}

	int KeyframedAnimationSet::GetRotationKeyCount( int animation )
	{
		return InternalPointer->GetNumRotationKeys( animation );
	}

	ScaleKey KeyframedAnimationSet::GetScaleKey( int animation, int key )
	{
		D3DXKEY_VECTOR3 qkey;

		HRESULT hr = InternalPointer->GetScaleKey( animation, key, &qkey );
		RECORD_D3D9( hr );

		return ScaleKey( qkey );
	}

	array<ScaleKey>^ KeyframedAnimationSet::GetScaleKeys( int animation )
	{
		int count = GetScaleKeyCount( animation );
		stack_array<D3DXKEY_VECTOR3> keys = stackalloc( D3DXKEY_VECTOR3, count );

		HRESULT hr = InternalPointer->GetScaleKeys( animation, &keys[0] );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		array<ScaleKey>^ results = gcnew array<ScaleKey>( count );
		for( int i = 0; i < count; i++ )
			results[i] = ScaleKey( keys[i] );

		return results;
	}

	Result KeyframedAnimationSet::SetScaleKey( int animation, int key, ScaleKey callbackKey )
	{
		D3DXKEY_VECTOR3 qkey;
		qkey.Time = callbackKey.Time;
		qkey.Value = D3DXVECTOR3( callbackKey.Value.X, callbackKey.Value.Y, callbackKey.Value.Z );

		HRESULT hr = InternalPointer->SetScaleKey( animation, key, &qkey );
		return RECORD_D3D9( hr );
	}

	Result KeyframedAnimationSet::UnregisterScaleKey( int animation, int key )
	{
		HRESULT hr = InternalPointer->UnregisterScaleKey( animation, key );
		return RECORD_D3D9( hr );
	}

	int KeyframedAnimationSet::GetScaleKeyCount( int animation )
	{
		return InternalPointer->GetNumScaleKeys( animation );
	}

	TranslationKey KeyframedAnimationSet::GetTranslationKey( int animation, int key )
	{
		D3DXKEY_VECTOR3 qkey;

		HRESULT hr = InternalPointer->GetTranslationKey( animation, key, &qkey );
		RECORD_D3D9( hr );

		return TranslationKey( qkey );
	}

	array<TranslationKey>^ KeyframedAnimationSet::GetTranslationKeys( int animation )
	{
		int count = GetTranslationKeyCount( animation );
		stack_array<D3DXKEY_VECTOR3> keys = stackalloc( D3DXKEY_VECTOR3, count );

		HRESULT hr = InternalPointer->GetTranslationKeys( animation, &keys[0] );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			return nullptr;
		}

		array<TranslationKey>^ results = gcnew array<TranslationKey>( count );
		for( int i = 0; i < count; i++ )
			results[i] = TranslationKey( keys[i] );

		return results;
	}

	Result KeyframedAnimationSet::SetTranslationKey( int animation, int key, TranslationKey callbackKey )
	{
		D3DXKEY_VECTOR3 qkey;
		qkey.Time = callbackKey.Time;
		qkey.Value = D3DXVECTOR3( callbackKey.Value.X, callbackKey.Value.Y, callbackKey.Value.Z );

		HRESULT hr = InternalPointer->SetTranslationKey( animation, key, &qkey );
		return RECORD_D3D9( hr );
	}

	Result KeyframedAnimationSet::UnregisterTranslationKey( int animation, int key )
	{
		HRESULT hr = InternalPointer->UnregisterTranslationKey( animation, key );
		return RECORD_D3D9( hr );
	}

	int KeyframedAnimationSet::GetTranslationKeyCount( int animation )
	{
		return InternalPointer->GetNumTranslationKeys( animation );
	}

	int KeyframedAnimationSet::RegisterAnimationKeys( String^ name, array<ScaleKey>^ scaleKeys,
		array<RotationKey>^ rotationKeys, array<TranslationKey>^ translationKeys )
	{
		DWORD result;

		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(name);
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		pin_ptr<ScaleKey> scales = &scaleKeys[0];
		pin_ptr<RotationKey> rotations = &rotationKeys[0];
		pin_ptr<TranslationKey> translations = &translationKeys[0];

		HRESULT hr = InternalPointer->RegisterAnimationSRTKeys(
			reinterpret_cast<LPCSTR>(pinnedName),
			scaleKeys->Length,
			rotationKeys->Length,
			translationKeys->Length,
			reinterpret_cast<D3DXKEY_VECTOR3*>(scales),
			reinterpret_cast<D3DXKEY_QUATERNION*>(rotations),
			reinterpret_cast<D3DXKEY_VECTOR3*>(translations),
			&result
		);

		if(RECORD_D3D9(hr).IsFailure)
			return 0;

		return result;
	}

	Result KeyframedAnimationSet::UnregisterAnimation( int animation )
	{
		HRESULT hr = InternalPointer->UnregisterAnimation( animation );
		return RECORD_D3D9( hr );
	}

	int KeyframedAnimationSet::CallbackKeyCount::get()
	{
		return InternalPointer->GetNumCallbackKeys();
	}

	SlimDX::Direct3D9::PlaybackType KeyframedAnimationSet::PlaybackType::get()
	{
		return static_cast<SlimDX::Direct3D9::PlaybackType>( InternalPointer->GetPlaybackType() );
	}

	double KeyframedAnimationSet::SourceTicksPerSecond::get()
	{
		return InternalPointer->GetSourceTicksPerSecond();
	}
}
}