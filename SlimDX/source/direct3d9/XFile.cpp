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

// rmxftmpl.h defines a global unsigned char array and can only be included once
// per project, or multiple-definition errors will occur at link time.
#include <rmxftmpl.h>

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
	XFile::XFile()
	{
		ID3DXFile *result;

		HRESULT hr = D3DXFileCreate( &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	XFileEnumerationObject^ XFile::CreateEnumerationObject( String^ fileName, CharSet charSet )
	{
		ID3DXFileEnumObject *result;
		array<unsigned char>^ nameBytes = System::Text::Encoding::Unicode->GetBytes( fileName );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		DWORD flag;
		if( charSet == CharSet::Unicode )
			flag = D3DXF_FILELOAD_FROMWFILE;
		else
			flag = D3DXF_FILELOAD_FROMFILE;

		HRESULT hr = InternalPointer->CreateEnumObject( reinterpret_cast<LPCVOID>( pinnedName ), flag, &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return XFileEnumerationObject::FromPointer( result );
	}

	XFileEnumerationObject^ XFile::CreateEnumerationObject_Internal( const void* memory, SIZE_T size )
	{
		ID3DXFileEnumObject *result;
		D3DXF_FILELOADMEMORY mem;
		mem.dSize = size;

		HRESULT hr = InternalPointer->CreateEnumObject( memory, D3DXF_FILELOAD_FROMMEMORY, &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return XFileEnumerationObject::FromPointer( result );
	}

	XFileEnumerationObject^ XFile::CreateEnumerationObject( array<Byte>^ memory )
	{
		pin_ptr<Byte> pinnedMemory = &memory[0];
		return CreateEnumerationObject_Internal( pinnedMemory, static_cast<SIZE_T>( memory->Length ) );
	}

	XFileEnumerationObject^ XFile::CreateEnumerationObject( Stream^ memory )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( memory, &ds );

		if( data == nullptr )
		{
			SIZE_T size = static_cast<SIZE_T>( ds->RemainingLength );
			return CreateEnumerationObject_Internal( ds->SeekToEnd(), size );
		}

		return CreateEnumerationObject( data );
	}

	XFileSaveObject^ XFile::CreateSaveObject( String^ fileName, CharSet charSet, XFileFormat format )
	{
		ID3DXFileSaveObject *result;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( fileName );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		DWORD flag;
		if( charSet == CharSet::Unicode )
			flag = D3DXF_FILESAVE_TOWFILE;
		else
			flag = D3DXF_FILESAVE_TOFILE;

		HRESULT hr = InternalPointer->CreateSaveObject( reinterpret_cast<LPCVOID>( pinnedName ), flag, static_cast<D3DXF_FILEFORMAT>( format ), &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return XFileSaveObject::FromPointer( result );
	}

	Result XFile::RegisterEnumerationTemplates( XFileEnumerationObject^ enumerationObject )
	{
		HRESULT hr = InternalPointer->RegisterEnumTemplates( enumerationObject->InternalPointer );
		return RECORD_D3D9( hr );
	}

	Result XFile::RegisterTemplates_Internal( const void* memory, SIZE_T size )
	{
		HRESULT hr = InternalPointer->RegisterTemplates( memory, size );
		return RECORD_D3D9( hr );
	}

	Result XFile::RegisterTemplates( array<Byte>^ memory )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return RegisterTemplates_Internal( pinnedMemory, static_cast<SIZE_T>( memory->Length ) );
	}

	Result XFile::RegisterTemplates( Stream^ stream )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			SIZE_T size = static_cast<SIZE_T>( ds->RemainingLength );
			return RegisterTemplates_Internal( ds->SeekToEnd(), size );
		}

		return RegisterTemplates( data );
	}

	Result XFile::RegisterTemplates( String^ name )
	{
		array<Byte>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		return RegisterTemplates( nameBytes );
	}

	String^ XFile::DefaultTemplates::get()
	{
		return gcnew String( reinterpret_cast<char*>( D3DRM_XTEMPLATES ) );
	}

	String^ XFile::SkinTemplates::get()
	{
		return XSKINEXP_TEMPLATES;
	}

	String^ XFile::ExtensionTemplates::get()
	{
		return XEXTENSIONS_TEMPLATES;
	}
}
}