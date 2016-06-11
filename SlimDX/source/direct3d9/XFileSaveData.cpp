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
#include "../ComObject.h"
#include "../DataStream.h"

#include "Direct3D9Exception.h"

#include "XFile.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	XFileSaveData^ XFileSaveData::AddDataObject_Internal( Guid dataTemplate, String^ name, Guid id, const void* data, SIZE_T size )
	{
		ID3DXFileSaveData *result;
		GUID nativeId = Utilities::ConvertManagedGuid( id );
		GUID *pointer = &nativeId;
		if( id == Guid::Empty )
			pointer = NULL;

		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->AddDataObject( Utilities::ConvertManagedGuid( dataTemplate ),
			reinterpret_cast<LPCSTR>( pinnedName ), pointer, size, data, &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew XFileSaveData( result, nullptr );
	}

	XFileSaveData^ XFileSaveData::AddDataObject( Guid dataTemplate, String^ name, Guid id, array<Byte>^ data )
	{
		pin_ptr<unsigned char> pinnedMemory = &data[0];
		return AddDataObject_Internal( dataTemplate, name, id, pinnedMemory, static_cast<SIZE_T>( data->Length ) );
	}

	XFileSaveData^ XFileSaveData::AddDataObject( Guid dataTemplate, String^ name, Guid id, Stream^ data )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ memory = Utilities::ReadStream( data, &ds );

		if( memory == nullptr )
		{
			SIZE_T size = static_cast<SIZE_T>( ds->RemainingLength );
			return AddDataObject_Internal( dataTemplate, name, id, ds->SeekToEnd(), size );
		}

		return AddDataObject( dataTemplate, name, id, memory );
	}

	Result XFileSaveData::AddDataReference( String^ name, Guid id )
	{
		GUID nativeId = Utilities::ConvertManagedGuid( id );
		GUID *pointer = &nativeId;
		if( id == Guid::Empty )
			pointer = NULL;

		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->AddDataReference( reinterpret_cast<LPCSTR>( pinnedName ), pointer );
		return RECORD_D3D9( hr );
	}

	Guid XFileSaveData::Id::get()
	{
		GUID guid;

		HRESULT hr = InternalPointer->GetId( &guid );

		if( RECORD_D3D9(hr).IsFailure || guid == GUID_NULL )
			return Guid::Empty;

		return Utilities::ConvertNativeGuid( guid );
	}

	String^ XFileSaveData::Name::get()
	{
		SIZE_T size = 0;

		HRESULT hr = InternalPointer->GetName( NULL, &size );
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		stack_array<char> name = stackalloc( char, static_cast<size_t>( size ) );

		hr = InternalPointer->GetName( &name[0], &size );
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew String( &name[0] );
	}

	XFileSaveObject^ XFileSaveData::SaveObject::get()
	{
		ID3DXFileSaveObject *result;

		HRESULT hr = InternalPointer->GetSave( &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return XFileSaveObject::FromPointer( result );
	}

	Guid XFileSaveData::Type::get()
	{
		GUID result;

		HRESULT hr = InternalPointer->GetType( &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return Guid::Empty;

		return Utilities::ConvertNativeGuid( result );
	}
}
}