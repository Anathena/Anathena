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

#include "../ComObject.h"
#include "../DataStream.h"
#include "XFile.h"

#include "Direct3D9Exception.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	XFileData^ XFileEnumerationObject::GetChild( int id )
	{
		ID3DXFileData *result;

		HRESULT hr = InternalPointer->GetChild( id, &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFileData::FromPointer( result );
	}

	XFileData^ XFileEnumerationObject::GetDataObject( Guid id )
	{
		ID3DXFileData *result;

		HRESULT hr = InternalPointer->GetDataObjectById( Utilities::ConvertManagedGuid( id ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFileData::FromPointer( result );
	}

	XFileData^ XFileEnumerationObject::GetDataObject( String^ name )
	{
		ID3DXFileData *result;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->GetDataObjectByName( reinterpret_cast<LPCSTR>( pinnedName ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFileData::FromPointer( result );
	}

	XFile^ XFileEnumerationObject::File::get()
	{
		ID3DXFile *result;

		HRESULT hr = InternalPointer->GetFile( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return XFile::FromPointer( result );
	}

	System::Int64 XFileEnumerationObject::ChildCount::get()
	{
		SIZE_T result;

		HRESULT hr = InternalPointer->GetChildren( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return 0;

		return result;
	}
}
}