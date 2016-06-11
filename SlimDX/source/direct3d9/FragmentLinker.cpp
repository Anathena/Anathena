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
#include "../VersionConfig.h"
#if SLIMDX_D3DX_VERSION < 42

#include <d3d9.h>
#include <d3dx9.h>

#include "../stack_array.h"
#include "../DataStream.h"
#include "../ComObject.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "VertexShader9.h"
#include "PixelShader9.h"
#include "FragmentLinker.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	FragmentLinker::FragmentLinker( SlimDX::Direct3D9::Device^ device, int cacheSize )
	{
		IDirect3DDevice9* devicePointer = device->InternalPointer;
		ID3DXFragmentLinker* linker;

		HRESULT hr = D3DXCreateFragmentLinker( devicePointer, cacheSize, &linker );
		RECORD_D3D9( hr );
		if( FAILED( hr ) )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(linker);
	}
	
	DataStream^ FragmentLinker::Gather( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* fragmentBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const Byte> pinnedData = &sourceData[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

#pragma warning(push)
#pragma warning(disable:4996)
		HRESULT hr = D3DXGatherFragments( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &fragmentBuffer, &errorBuffer );
#pragma warning(pop)

		//clean up after marshaling macros
		Macro::Unmarshal( handles );
		//marshal errors if necessary
		errors = Utilities::BufferToString( errorBuffer );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( fragmentBuffer );
	}

// We know that Gather is obsolete, we declared it ourselves
#pragma warning(disable:4947)
	DataStream^ FragmentLinker::Gather( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		array<Byte>^ sourceBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Gather( sourceBytes, defines, includeFile, flags, errors );
	}
#pragma warning(default:4947)

	DataStream^ FragmentLinker::GatherFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* fragmentBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedFile = PtrToStringChars( fileName );

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

#pragma warning(push)
#pragma warning(disable:4996)
		HRESULT hr = D3DXGatherFragmentsFromFile( reinterpret_cast<LPCTSTR>( pinnedFile ), macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &fragmentBuffer, &errorBuffer );
#pragma warning(pop)

		//clean up after marshaling macros
		Macro::Unmarshal( handles );
		//marshal errors if necessary
		errors = Utilities::BufferToString( errorBuffer );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( fragmentBuffer );
	}

	Result FragmentLinker::AddFragments( array<int>^ fragments )
	{
		pin_ptr<int> pinnedFragments = &fragments[0];
		HRESULT hr = InternalPointer->AddFragments( reinterpret_cast<const DWORD*>( pinnedFragments ) );
		return RECORD_D3D9( hr );
	}

	Result FragmentLinker::AddFragments( DataStream^ fragments )
	{
		HRESULT hr = InternalPointer->AddFragments( reinterpret_cast<const DWORD*>( fragments->PositionPointer ) );
		return RECORD_D3D9( hr );
	}

	DataStream^ FragmentLinker::GetFragment( EffectHandle^ name )
	{
		D3DXHANDLE handle = name != nullptr ? name->InternalHandle : NULL;
		ID3DXBuffer* fragment;

		HRESULT hr = InternalPointer->GetFragment( handle, &fragment );
		GC::KeepAlive( name );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( fragment );
	}

	DataStream^ FragmentLinker::Fragments::get()
	{
		ID3DXBuffer* fragments;

		HRESULT hr = InternalPointer->GetAllFragments( &fragments );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( fragments );
	}

	SlimDX::Direct3D9::Device^ FragmentLinker::Device::get()
	{
		IDirect3DDevice9* device;
		//This method always returns the value S_OK.
		InternalPointer->GetDevice( &device );

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	FragmentDescription FragmentLinker::GetFragmentDescription( EffectHandle^ name )
	{
		D3DXHANDLE handle = name != nullptr ? name->InternalHandle : NULL;
		D3DXFRAGMENT_DESC description;

		HRESULT hr = InternalPointer->GetFragmentDesc( handle, &description );
		GC::KeepAlive( name );
		if( RECORD_D3D9( hr ).IsFailure )
			return FragmentDescription();

		FragmentDescription outDesc;
		outDesc.Name = gcnew String( description.Name );
		outDesc.Target = description.Target;

		return outDesc;
	}

	EffectHandle^ FragmentLinker::GetFragmentHandle( int index )
	{
		D3DXHANDLE handle = InternalPointer->GetFragmentHandleByIndex( index );
		if( handle == NULL )
			return nullptr;

		return gcnew EffectHandle( handle );
	}

	EffectHandle^ FragmentLinker::GetFragmentHandle( String^ name )
	{
		array<Byte>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<const Byte> pinnedName = &nameBytes[0];

		D3DXHANDLE handle = InternalPointer->GetFragmentHandleByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( handle == NULL )
			return nullptr;

		return gcnew EffectHandle( handle );
	}

	void FragmentLinker::ClearCache()
	{
		//This method always returns the value S_OK.
		InternalPointer->ClearCache();
	}

	ShaderBytecode^ FragmentLinker::LinkShader( String^ profile, ShaderFlags flags, array<EffectHandle^>^ fragmentHandles, [Out] String^% errors )
	{
		ID3DXBuffer* bytecode;
		ID3DXBuffer* errorBuffer;
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<const Byte> pinnedProfile = &profileBytes[0];

		stack_array<D3DXHANDLE> handles = stackalloc( D3DXHANDLE, fragmentHandles->Length );
		for( int i = 0; i < fragmentHandles->Length; ++i )
			handles[i] = fragmentHandles[i] != nullptr ? fragmentHandles[i]->InternalHandle : NULL;

		HRESULT hr = InternalPointer->LinkShader( reinterpret_cast<LPCSTR>( pinnedProfile ), static_cast<DWORD>( flags ), &handles[0], fragmentHandles->Length, &bytecode, &errorBuffer );
		GC::KeepAlive( fragmentHandles );
		RECORD_D3D9( hr );
		
		//marshal errors if necessary
		errors = Utilities::BufferToString( errorBuffer );
		if( Result::Last.IsFailure )
			return nullptr;

		return ShaderBytecode::FromPointer( bytecode );
	}

	VertexShader^ FragmentLinker::LinkVertexShader( String^ profile, ShaderFlags flags, array<EffectHandle^>^ fragmentHandles, [Out] String^% errors )
	{
		IDirect3DVertexShader9* shader;
		ID3DXBuffer* errorBuffer;
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<const Byte> pinnedProfile = &profileBytes[0];

		stack_array<D3DXHANDLE> handles = stackalloc( D3DXHANDLE, fragmentHandles->Length );
		for( int i = 0; i < fragmentHandles->Length; ++i )
			handles[i] = fragmentHandles[i] != nullptr ? fragmentHandles[i]->InternalHandle : NULL;

		HRESULT hr = InternalPointer->LinkVertexShader( reinterpret_cast<LPCSTR>( pinnedProfile ), static_cast<DWORD>( flags ), &handles[0], fragmentHandles->Length, &shader, &errorBuffer );
		GC::KeepAlive( fragmentHandles );
		RECORD_D3D9( hr );
		
		//marshal errors if necessary
		errors = Utilities::BufferToString( errorBuffer );
		if( Result::Last.IsFailure )
			return nullptr;

		return VertexShader::FromPointer( shader );
	}

	PixelShader^ FragmentLinker::LinkPixelShader( String^ profile, ShaderFlags flags, array<EffectHandle^>^ fragmentHandles, [Out] String^% errors )
	{
		IDirect3DPixelShader9* shader;
		ID3DXBuffer* errorBuffer;
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<const Byte> pinnedProfile = &profileBytes[0];

		stack_array<D3DXHANDLE> handles = stackalloc( D3DXHANDLE, fragmentHandles->Length);
		for( int i = 0; i < fragmentHandles->Length; ++i )
			handles[i] = fragmentHandles[i] != nullptr ? fragmentHandles[i]->InternalHandle : NULL;

		HRESULT hr = InternalPointer->LinkPixelShader( reinterpret_cast<LPCSTR>( pinnedProfile ), static_cast<DWORD>( flags ), &handles[0], fragmentHandles->Length, &shader, &errorBuffer );
		GC::KeepAlive( fragmentHandles );
		RECORD_D3D9( hr );
		
		//marshal errors if necessary
		errors = Utilities::BufferToString( errorBuffer );
		if( Result::Last.IsFailure )
			return nullptr;

		return PixelShader::FromPointer( shader );
	}

	int FragmentLinker::FragmentCount::get()
	{
		return InternalPointer->GetNumberOfFragments();
	}
}
}
#endif
