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
	XFileData^ XFileData::GetChild( int id )
	{
		ID3DXFileData *result;

		HRESULT hr = InternalPointer->GetChild( id, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFileData::FromPointer( result );
	}

	XFileEnumerationObject^ XFileData::EnumerationObject::get()
	{
		ID3DXFileEnumObject *result;

		HRESULT hr = InternalPointer->GetEnum( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFileEnumerationObject::FromPointer( result );
	}

	DataStream^ XFileData::Lock()
	{
		SIZE_T size;
		const void *data;

		HRESULT hr = InternalPointer->Lock( &size, &data );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( data, size, true, true );
	}

	Result XFileData::Unlock()
	{
		HRESULT hr = InternalPointer->Unlock();
		return RECORD_D3D9( hr );
	}

	System::Int64 XFileData::ChildCount::get()
	{
		SIZE_T result;

		HRESULT hr = InternalPointer->GetChildren( &result );
		RECORD_D3D9( hr );

		if( FAILED( hr ) )
			return 0;

		return result;
	}

	Guid XFileData::Id::get()
	{
		GUID guid;

		HRESULT hr = InternalPointer->GetId( &guid );

		if( RECORD_D3D9( hr ).IsFailure || guid == GUID_NULL )
			return Guid::Empty;

		return Utilities::ConvertNativeGuid( guid );
	}

	String^ XFileData::Name::get()
	{
		SIZE_T size = 0;

		HRESULT hr = InternalPointer->GetName( NULL, &size );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		stack_array<char> name = stackalloc( char, size );

		hr = InternalPointer->GetName( &name[0], &size );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew String( &name[0] );
	}

	Guid XFileData::Type::get()
	{
		GUID result;

		HRESULT hr = InternalPointer->GetType( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return Guid::Empty;

		return Utilities::ConvertNativeGuid( result );
	}

	bool XFileData::IsReference::get()
	{
		if( InternalPointer->IsReference() )
			return true;
		else
			return false;
	}
}
}