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

#include "../stack_array.h"
#include "../DataStream.h"
#include "../CompilationException.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Effect9.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	Effect::Effect( ID3DXEffect* pointer, ComObject^ owner )
	{
		Construct( pointer, owner );
	}

	Effect::Effect( IntPtr pointer )
	{
		Construct( pointer, NativeInterface );
	}
	
	Effect^ Effect::FromPointer( ID3DXEffect* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ComObject::ConstructFromPointer<Effect, ID3DXEffect>( pointer, owner, flags );
	}
	
	Effect^ Effect::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<Effect>( pointer );
	}
	
	Effect^ Effect::FromMemory_Internal( SlimDX::Direct3D9::Device^ device, const void* memory, UINT size,
		array<Macro>^ preprocessorDefines, Include ^includeFile, String^ skipConstants, ShaderFlags flags,
		EffectPool^ pool, String^* compilationErrors, Exception^* exception )
	{
		ID3DXEffect* effect;
		ID3DXBuffer* errorBuffer;

		LPCSTR skipString = NULL;
		pin_ptr<Byte> pinnedSkip;
		if( skipConstants != nullptr )
		{
			array<Byte>^ skipBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( skipConstants );
			pinnedSkip = &skipBytes[0];
			skipString = reinterpret_cast<LPCSTR>( pinnedSkip );
		}

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		ID3DXEffectPool* effectPool = pool != nullptr ? pool->InternalPointer : NULL;
		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( preprocessorDefines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCreateEffectEx( device->InternalPointer, memory, size, macrosPtr, includePtr,
			skipString, static_cast<DWORD>( flags ), effectPool, &effect, &errorBuffer );
		
		Macro::Unmarshal( handles );

		String^ compilationErrorsLocal = Utilities::BufferToString( errorBuffer );
		if( compilationErrors != NULL )
			*compilationErrors = compilationErrorsLocal;

		Exception^ e = CompilationException::Check<Direct3D9Exception^>( hr, compilationErrorsLocal );
		if( e != nullptr )
		{
			if( compilationErrors == NULL || exception == NULL )
				throw e;

			*exception = e;
			return nullptr;
		}

		return gcnew Effect( effect, nullptr );
	}

	Effect^ Effect::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, array<Macro>^ preprocessorDefines,
		Include ^includeFile, String^ skipConstants, ShaderFlags flags, EffectPool^ pool,
		[Out] String^% compilationErrors )
	{
		pin_ptr<unsigned char> pinnedData = &memory[0];
		String^ compileErrorsLocal;
		Exception^ e = nullptr;

		Effect^ effect = FromMemory_Internal( device, pinnedData, static_cast<UINT>( memory->Length ), preprocessorDefines,
			includeFile, skipConstants, flags, pool, &compileErrorsLocal, &e );
		compilationErrors = compileErrorsLocal;

		if( e != nullptr )
			throw e;

		return effect;
	}

	Effect^ Effect::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, array<Macro>^ preprocessorDefines,
		Include ^includeFile, String^ skipConstants, ShaderFlags flags, EffectPool^ pool )
	{
		pin_ptr<unsigned char> pinnedData = &memory[0];

		return FromMemory_Internal( device, pinnedData, static_cast<UINT>( memory->Length ), preprocessorDefines,
			includeFile, skipConstants, flags, pool, NULL, NULL );
	}

	Effect^ Effect::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, array<Macro>^ preprocessorDefines,
		Include ^includeFile, String^ skipConstants, ShaderFlags flags )
	{
		pin_ptr<unsigned char> pinnedData = &memory[0];

		return FromMemory_Internal( device, pinnedData, static_cast<UINT>( memory->Length ), preprocessorDefines,
			includeFile, skipConstants, flags, nullptr, NULL, NULL );
	}

	Effect^ Effect::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, ShaderFlags flags )
	{
		pin_ptr<unsigned char> pinnedData = &memory[0];

		return FromMemory_Internal( device, pinnedData, static_cast<UINT>( memory->Length ), nullptr,
			nullptr, nullptr, flags, nullptr, NULL, NULL );
	}

	Effect^ Effect::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] String^ %compilationErrors )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );
		Exception^ e = nullptr;

		if( data == nullptr )
		{
			String^ compilationErrorsLocal;
			UINT size = static_cast<UINT>( ds->RemainingLength );

			Effect^ effect = FromMemory_Internal( device, ds->SeekToEnd(), size, preprocessorDefines, includeFile,
				skipConstants, flags, pool, &compilationErrorsLocal, &e );

			compilationErrors = compilationErrorsLocal;
			if( e != nullptr )
				throw e;

			return effect;
		}

		return FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags, pool, compilationErrors );
	}

	Effect^ Effect::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );
		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromMemory_Internal( device, ds->SeekToEnd(), size, preprocessorDefines, includeFile,
				skipConstants, flags, pool, NULL, NULL );
		}

		return FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags, pool );
	}

	Effect^ Effect::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );
		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromMemory_Internal( device, ds->SeekToEnd(), size, preprocessorDefines, includeFile,
				skipConstants, flags, nullptr, NULL, NULL );
		}

		return Effect::FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags );
	}

	Effect^ Effect::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, ShaderFlags flags )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );
		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromMemory_Internal( device, ds->SeekToEnd(), size, nullptr, nullptr,
				nullptr, flags, nullptr, NULL, NULL );
		}

		return FromMemory( device, data, flags );
	}

	Effect^ Effect::FromString( SlimDX::Direct3D9::Device^ device, String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] String^ %compilationErrors )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );

		return FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags, pool, compilationErrors );
	}

	Effect^ Effect::FromString( SlimDX::Direct3D9::Device^ device, String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );

		return FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags, pool );
	}

	Effect^ Effect::FromString( SlimDX::Direct3D9::Device^ device, String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );

		return FromMemory( device, data, preprocessorDefines, includeFile,
			skipConstants, flags );
	}

	Effect^ Effect::FromString( SlimDX::Direct3D9::Device^ device, String^ sourceData, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );

		return FromMemory( device, data, flags );
	}

	Effect^ Effect::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] String^ %compilationErrors )
	{
		ID3DXEffect* effect;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		LPCSTR skipString = NULL;
		pin_ptr<Byte> pinnedSkip;
		if( skipConstants != nullptr )
		{
			array<Byte>^ skipBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( skipConstants );
			pinnedSkip = &skipBytes[0];
			skipString = reinterpret_cast<LPCSTR>( pinnedSkip );
		}

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		ID3DXEffectPool* effectPool = pool != nullptr ? pool->InternalPointer : NULL;
		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( preprocessorDefines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCreateEffectFromFileEx( device->InternalPointer, pinnedName, macrosPtr, includePtr,
			skipString, static_cast<DWORD>( flags ), effectPool, &effect, &errorBuffer );
		
		Macro::Unmarshal( handles );
		compilationErrors = Utilities::BufferToString( errorBuffer );

		Exception^ e = CompilationException::Check<Direct3D9Exception^>( hr, compilationErrors );
		if( e != nullptr )
			throw e;

		return gcnew Effect( effect, nullptr );
	}

	Effect^ Effect::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags, EffectPool^ pool )
	{
		ID3DXEffect* effect;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		LPCSTR skipString = NULL;
		pin_ptr<Byte> pinnedSkip;
		if( skipConstants != nullptr )
		{
			array<Byte>^ skipBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( skipConstants );
			pinnedSkip = &skipBytes[0];
			skipString = reinterpret_cast<LPCSTR>( pinnedSkip );
		}

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		ID3DXEffectPool* effectPool = pool != nullptr ? pool->InternalPointer : NULL;
		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( preprocessorDefines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCreateEffectFromFileEx( device->InternalPointer, pinnedName, macrosPtr, includePtr,
			skipString, static_cast<DWORD>( flags ), effectPool, &effect, &errorBuffer );
		
		Macro::Unmarshal( handles );
		String^ compilationErrors = Utilities::BufferToString( errorBuffer );

		Exception^ e = CompilationException::Check<Direct3D9Exception^>( hr, compilationErrors );
		if( e != nullptr )
			throw e;

		return gcnew Effect( effect, nullptr );
	}

	Effect^ Effect::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile,
		String^ skipConstants, ShaderFlags flags )
	{
		ID3DXEffect* effect;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		LPCSTR skipString = NULL;
		pin_ptr<Byte> pinnedSkip;
		if( skipConstants != nullptr )
		{
			array<Byte>^ skipBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( skipConstants );
			pinnedSkip = &skipBytes[0];
			skipString = reinterpret_cast<LPCSTR>( pinnedSkip );
		}

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( preprocessorDefines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCreateEffectFromFileEx( device->InternalPointer, pinnedName, macrosPtr, includePtr,
			skipString, static_cast<DWORD>( flags ), NULL, &effect, &errorBuffer );
		
		Macro::Unmarshal( handles );
		String^ compilationErrors = Utilities::BufferToString( errorBuffer );

		Exception^ e = CompilationException::Check<Direct3D9Exception^>( hr, compilationErrors );
		if( e != nullptr )
			throw e;

		return gcnew Effect( effect, nullptr );
	}

	Effect^ Effect::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, ShaderFlags flags )
	{
		ID3DXEffect* effect;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXCreateEffectFromFileEx( device->InternalPointer, pinnedName, NULL, NULL,
			NULL, static_cast<DWORD>( flags ), NULL, &effect, &errorBuffer );
		
		String^ compilationErrors = Utilities::BufferToString( errorBuffer );

		Exception^ e = CompilationException::Check<Direct3D9Exception^>( hr, compilationErrors );
		if( e != nullptr )
			throw e;

		return gcnew Effect( effect, nullptr );
	}

	Effect^ Effect::Clone( SlimDX::Direct3D9::Device^ device )
	{
		ID3DXEffect *result;

		HRESULT hr = InternalPointer->CloneEffect( device->InternalPointer, &result );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew Effect( result, nullptr );
	}

	int Effect::Begin( FX flags )
	{
		unsigned int passCount;

		HRESULT hr = InternalPointer->Begin( &passCount, static_cast<DWORD>( flags ) );
		RECORD_D3D9( hr );

		return passCount;
	}

	int Effect::Begin()
	{
		unsigned int passCount;

		HRESULT hr = InternalPointer->Begin( &passCount, 0 );
		RECORD_D3D9( hr );

		return passCount;
	}

	Result Effect::End()
	{
		HRESULT hr = InternalPointer->End();
		return RECORD_D3D9( hr );
	}

	Result Effect::BeginPass( int pass )
	{
		HRESULT hr = InternalPointer->BeginPass( pass );
		return RECORD_D3D9( hr );
	}

	Result Effect::EndPass()
	{
		HRESULT hr = InternalPointer->EndPass();
		return RECORD_D3D9( hr );
	}

	Result Effect::BeginParameterBlock()
	{
		HRESULT hr = InternalPointer->BeginParameterBlock();
		return RECORD_D3D9( hr );
	}

	EffectHandle^ Effect::EndParameterBlock()
	{
		D3DXHANDLE handle = InternalPointer->EndParameterBlock();
		if( handle == NULL )
			return nullptr;
		return gcnew EffectHandle( handle );
	}

	Result Effect::ApplyParameterBlock( EffectHandle^ parameterBlock )
	{
		D3DXHANDLE handle = parameterBlock != nullptr ? parameterBlock->InternalHandle : NULL;
		HRESULT hr = InternalPointer->ApplyParameterBlock( handle );
		GC::KeepAlive( parameterBlock );
		return RECORD_D3D9( hr );
	}

	Result Effect::DeleteParameterBlock( EffectHandle^ parameterBlock )
	{
		D3DXHANDLE handle = parameterBlock != nullptr ? parameterBlock->InternalHandle : NULL;
		HRESULT hr = InternalPointer->DeleteParameterBlock( handle );
		GC::KeepAlive( parameterBlock );
		return RECORD_D3D9( hr );
	}

	bool Effect::IsParameterUsed( EffectHandle^ parameter, EffectHandle^ technique )
	{
		D3DXHANDLE paramHandle = parameter != nullptr ? parameter->InternalHandle : NULL;
		D3DXHANDLE techHandle = technique != nullptr ? technique->InternalHandle : NULL;
		BOOL used = InternalPointer->IsParameterUsed( paramHandle, techHandle );
		GC::KeepAlive( parameter );
		return used > 0;
	}

	Result Effect::CommitChanges()
	{
		HRESULT hr = InternalPointer->CommitChanges();
		return RECORD_D3D9( hr );
	}

	EffectHandle^ Effect::FindNextValidTechnique( EffectHandle^ technique )
	{
		D3DXHANDLE handle;
		D3DXHANDLE parentHandle = technique != nullptr ? technique->InternalHandle : NULL;

		HRESULT hr = InternalPointer->FindNextValidTechnique( parentHandle, &handle );
		GC::KeepAlive( technique );
		if( RECORD_D3D9( hr ).IsFailure || handle == NULL )
			return nullptr;

		return gcnew EffectHandle( handle );
	}

	bool Effect::ValidateTechnique( EffectHandle^ technique )
	{
		D3DXHANDLE handle = technique != nullptr ? technique->InternalHandle : NULL;
		bool result = SUCCEEDED( InternalPointer->ValidateTechnique( handle ) );
		GC::KeepAlive( technique );
		return result;
	}

	EffectHandle^ Effect::Technique::get()
	{
		D3DXHANDLE handle = InternalPointer->GetCurrentTechnique();
		if( handle == NULL )
			return nullptr;
		return gcnew EffectHandle( handle );
	}

	void Effect::Technique::set( EffectHandle^ value )
	{
		D3DXHANDLE handle = value != nullptr ? value->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetTechnique( handle );
		GC::KeepAlive( value );
		RECORD_D3D9( hr );
	}

	Result Effect::OnLostDevice()
	{
		HRESULT hr = InternalPointer->OnLostDevice();
		return RECORD_D3D9( hr );
	}

	Result Effect::OnResetDevice()
	{
		HRESULT hr = InternalPointer->OnResetDevice();
		return RECORD_D3D9( hr );
	}

	Result Effect::SetRawValue( EffectHandle^ handle, DataStream^ data )
	{
		return SetRawValue( handle, data, 0, static_cast<int>( data->Length ) );
	}

	Result Effect::SetRawValue( EffectHandle^ handle, DataStream^ data, int offset, int count )
	{
		D3DXHANDLE value = handle != nullptr ? handle->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetRawValue( value, data->PositionPointer, offset, count );
		GC::KeepAlive( handle );
		return RECORD_D3D9( hr );
	}

	Result Effect::SetRawValue( EffectHandle^ handle, array<float>^ data )
	{
		return SetRawValue( handle, data, 0, data->Length );
	}

	// only float[] and not T[] due to SDK docs:
	// "All values are expected to be either matrix4x4s or float4s and all matrices are expected to be 
	// in column-major order. Int or float values are cast into a float4; therefore, it is highly recommended 
	// that you use SetRawValue with only float4 or matrix4x4 data."
	Result Effect::SetRawValue( EffectHandle^ handle, array<float>^ data, int startIndex, int count )
	{
		D3DXHANDLE value = handle != nullptr ? handle->InternalHandle : NULL;
		pin_ptr<float> pinnedData = &data[startIndex];
		HRESULT hr = InternalPointer->SetRawValue( value, pinnedData, 0, count * sizeof(float) );
		GC::KeepAlive( handle );
		return RECORD_D3D9( hr );
	}

	DataStream^ Effect::Disassemble( bool enableColorCode )
	{
		ID3DXBuffer *result = NULL;

		HRESULT hr = D3DXDisassembleEffect( InternalPointer, enableColorCode, &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	void Effect::StateManager::set( IEffectStateManager^ manager )
	{
		if( shim != NULL )
			shim->Release();

		// Manual Allocation: Handled properly
		// the class needs to keep this pointer around
		if (manager == nullptr)
			shim = 0;
		else
			shim = new IEffectStateManagerShim(manager, this);

		HRESULT hr = InternalPointer->SetStateManager( shim );
		RECORD_D3D9( hr );
	}

	IEffectStateManager^ Effect::StateManager::get()
	{
		if( shim != NULL )
			return shim->GetManager();
		else
			return nullptr;
	}

	SlimDX::Direct3D9::Device^ Effect::Device::get()
	{
		IDirect3DDevice9* device;

		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	EffectPool^ Effect::Pool::get()
	{
		ID3DXEffectPool *pool;

		HRESULT hr = InternalPointer->GetPool( &pool );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return EffectPool::FromPointer( pool );
	}
}
}
