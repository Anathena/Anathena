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

#include <d3d10.h>
#include <d3dx10.h>
#include <vcclr.h>

#include "Direct3D10Exception.h"

#include "../DataStream.h"
#include "EffectPool.h"
#include "Device10.h"
#include "Effect10.h"

using namespace System;
using namespace System::IO;

using namespace SlimDX::D3DCompiler;

namespace SlimDX
{
namespace Direct3D10
{ 
	Effect^ EffectPool::AsEffect()
	{
		ID3D10Effect* effect = InternalPointer->AsEffect();
		if( effect == 0 )
			return nullptr;
	  return Effect::FromPointer( effect, this, ComObjectFlags::IsAncillary );
	}

	EffectPool^ EffectPool::FromFile( Device^ device, String^ fileName, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return (FromFile( device, fileName, profile, shaderFlags, effectFlags, compilationErrors ));
	}
	
	EffectPool^ EffectPool::FromFile( Device^ device, String^ fileName, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, [Out] String^ %compilationErrors  )
	{
		pin_ptr<const wchar_t> pinnedFileName = PtrToStringChars( fileName );
		array<unsigned char>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<unsigned char> pinnedProfile = &profileBytes[0];
		ID3D10EffectPool* effectPool;
		ID3D10Blob* errorBlob;

		HRESULT hr = D3DX10CreateEffectPoolFromFile( pinnedFileName, NULL, NULL, reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<UINT>(shaderFlags), static_cast<UINT>(effectFlags ), device->InternalPointer,
			NULL, &effectPool, &errorBlob, NULL );

		if( errorBlob != 0 )
		{
		  compilationErrors = gcnew String( reinterpret_cast<const char*>( errorBlob->GetBufferPointer() ) );
		  errorBlob->Release();
		}
		else
		{
			compilationErrors = String::Empty;
		}
		
		RECORD_D3D10( hr );
		if( effectPool == NULL)
			return nullptr;
		return gcnew EffectPool( effectPool, nullptr );
	}
	
	EffectPool^ EffectPool::FromStream( Device^ device, Stream^ stream, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return FromStream( device, stream, profile, shaderFlags, effectFlags, compilationErrors );
	}
	
	EffectPool^ EffectPool::FromStream( Device^ device, Stream^ stream, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, [Out] String^ %compilationErrors  )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ memory = Utilities::ReadStream( stream, &ds );

		if( memory == nullptr )
		{
			SIZE_T size = static_cast<SIZE_T>( ds->RemainingLength );
			return FromMemory_Internal( device, ds->SeekToEnd(), size,
				profile, shaderFlags, effectFlags, compilationErrors );
		}

		return FromMemory( device, memory, profile, shaderFlags, effectFlags, compilationErrors );
	}
	
	EffectPool^ EffectPool::FromMemory( Device^ device, array<Byte>^ memory, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return FromMemory( device, memory, profile, shaderFlags, effectFlags, compilationErrors );
	}

	EffectPool^ EffectPool::FromMemory_Internal( Device^ device, const void* data, SIZE_T size, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, [Out] String^ %compilationErrors )
	{
		ID3D10EffectPool* effectPool;
		ID3D10Blob* errorBlob;
	
		array<unsigned char>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<unsigned char> pinnedProfile = &profileBytes[0];

		HRESULT hr = D3DX10CreateEffectPoolFromMemory( data, size, "<no file name>", NULL, NULL,
			reinterpret_cast<LPCSTR>( pinnedProfile ), static_cast<UINT>(shaderFlags), static_cast< UINT>(effectFlags ), device->InternalPointer,
			NULL, &effectPool, &errorBlob, NULL );

		if( errorBlob != 0 )
		{
		  compilationErrors = gcnew String( reinterpret_cast<const char*>( errorBlob->GetBufferPointer() ) );
		  errorBlob->Release();
		}
		else
		{
			compilationErrors = String::Empty;
		}
		
		RECORD_D3D10( hr );
		if( effectPool == NULL)
			return nullptr;
		return gcnew EffectPool( effectPool, nullptr );
	}
	
	EffectPool^ EffectPool::FromMemory( Device^ device, array<Byte>^ memory, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, [Out] String^ %compilationErrors )
	{
		pin_ptr<unsigned char> pinnedData = &memory[0];
		return FromMemory_Internal( device, pinnedData, static_cast<SIZE_T>( memory->Length ), profile,
			shaderFlags, effectFlags, compilationErrors );
	}
}
}
