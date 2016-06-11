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

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"

#include "DirectSoundException.h"

#include "SecondarySoundBuffer.h"

using namespace System;
using namespace System::Reflection;

namespace SlimDX
{
namespace DirectSound
{
	SecondarySoundBuffer::SecondarySoundBuffer( DirectSound^ dsound, SoundBufferDescription description )
	{
		IDirectSoundBuffer* buffer;
		DSBUFFERDESC nativeDesc = description.ToUnmanaged();
		
		HRESULT hr = dsound->InternalPointer->CreateSoundBuffer( &nativeDesc, &buffer, NULL );

		delete[] nativeDesc.lpwfxFormat;

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		IDirectSoundBuffer8* result;
		hr = buffer->QueryInterface( IID_IDirectSoundBuffer8, reinterpret_cast<void**>( &result ) );
		
		buffer->Release();

		if( RECORD_DSOUND( hr ).IsFailure )
			throw gcnew DirectSoundException( Result::Last );

		Construct( result );
	}

	array<SoundEffectResult>^ SecondarySoundBuffer::SetEffects( array<Guid>^ effects )
	{
		DWORD count = effects->Length;
		LPDWORD dwResults = NULL;
		LPDSEFFECTDESC dsEffects = NULL;

		stack_array<DWORD> outputs = stackalloc( DWORD, count );
		stack_array<DSEFFECTDESC> inputs = stackalloc( DSEFFECTDESC, count );

		if( effects != nullptr && count > 0 )
		{
			for( unsigned int i = 0; i < count; i++ )
			{
				DSEFFECTDESC desc;
				ZeroMemory( &desc, sizeof( DSEFFECTDESC ) );
				desc.dwSize = sizeof( DSEFFECTDESC );
				desc.guidDSFXClass = Utilities::ConvertManagedGuid( effects[i] );

				inputs[i] = desc;
			}

			dwResults = &outputs[0];
			dsEffects = &inputs[0];
		}

		HRESULT hr = InternalPointer->SetFX( count, dsEffects, dwResults );
		if( RECORD_DSOUND( hr ).IsFailure || count == 0 )
			return nullptr;

		array<SoundEffectResult>^ results = gcnew array<SoundEffectResult>( count );

		for( unsigned int i = 0; i < count; i++ )
			results[i] = static_cast<SoundEffectResult>( outputs[i] );

		return results;
	}

	generic<typename T>
	T SecondarySoundBuffer::GetEffect( int index )
	{
		GUID guid = Utilities::GetNativeGuidForType( T::typeid );
		void *resultPointer;

		HRESULT hr = InternalPointer->GetObjectInPath( GUID_All_Objects, index, guid, &resultPointer );
		if( RECORD_DSOUND( hr ).IsFailure )
			return T();

		MethodInfo^ method = T::typeid->GetMethod( "FromPointer", BindingFlags::Public | BindingFlags::Static );
		return safe_cast<T>( method->Invoke( nullptr, gcnew array<Object^> { IntPtr( resultPointer ) } ) );
	}
}
}