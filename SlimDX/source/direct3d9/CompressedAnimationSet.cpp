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
#include "CompressedAnimationSet.h"
#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	CompressedAnimationSet::CompressedAnimationSet( String^ name, double ticksPerSecond,
		SlimDX::Direct3D9::PlaybackType playbackType, DataStream^ compressedData,
		array<CallbackKey>^ callbackKeys )
	{
		LPD3DXCOMPRESSEDANIMATIONSET pointer;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];
		int count = callbackKeys->Length;

		stack_array<D3DXKEY_CALLBACK> keys = stackalloc( D3DXKEY_CALLBACK, count );
		for( int i = 0; i < count; i++ )
		{
			keys[i].Time = callbackKeys[i].Time;
			keys[i].pCallbackData = callbackKeys[i].Data.ToPointer();
		}

		HRESULT hr = D3DXCreateCompressedAnimationSet( reinterpret_cast<LPCSTR>( pinnedName ), ticksPerSecond,
			static_cast<D3DXPLAYBACK_TYPE>( playbackType ), compressedData->GetD3DBuffer(), count,
			&keys[0], &pointer );

		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(pointer);
	}

	array<CallbackKey>^ CompressedAnimationSet::GetCallbackKeys()
	{
		int count = CallbackKeyCount;
		stack_array<D3DXKEY_CALLBACK> keys = stackalloc( D3DXKEY_CALLBACK, count );

		HRESULT hr = InternalPointer->GetCallbackKeys( &keys[0] );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		array<CallbackKey>^ results = gcnew array<CallbackKey>( count );
		for( int i = 0; i < count; i++ )
			results[i] = CallbackKey( keys[i] );

		return results;
	}

	DataStream^ CompressedAnimationSet::GetCompressedData()
	{
		LPD3DXBUFFER buffer;

		HRESULT hr = InternalPointer->GetCompressedData( &buffer );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( buffer );
	}

	int CompressedAnimationSet::CallbackKeyCount::get()
	{
		return InternalPointer->GetNumCallbackKeys();
	}

	SlimDX::Direct3D9::PlaybackType CompressedAnimationSet::PlaybackType::get()
	{
		return static_cast<SlimDX::Direct3D9::PlaybackType>( InternalPointer->GetPlaybackType() );
	}

	double CompressedAnimationSet::SourceTicksPerSecond::get()
	{
		return InternalPointer->GetSourceTicksPerSecond();
	}
}
}