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

#include "../DataStream.h"

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "ImageLoadInformation.h"
#include "Resource10.h"
#include "ShaderResourceView.h"
#include "ShaderResourceViewDescription.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Direct3D10
{ 
	ShaderResourceView::ShaderResourceView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10ShaderResourceView *view = 0;
		if( RECORD_D3D10( device->InternalPointer->CreateShaderResourceView( resource->InternalPointer, 0, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
			
		Construct( view );
	}
	
	ShaderResourceView::ShaderResourceView( SlimDX::Direct3D10::Device^ device, SlimDX::Direct3D10::Resource^ resource, ShaderResourceViewDescription description )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( resource == nullptr )
			throw gcnew ArgumentNullException( "resource" );
		
		ID3D10ShaderResourceView *view = 0;
		D3D10_SHADER_RESOURCE_VIEW_DESC nativeDescription = description.CreateNativeVersion();
		if( RECORD_D3D10( device->InternalPointer->CreateShaderResourceView( resource->InternalPointer, &nativeDescription, &view ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
			
		Construct( view );
	}

	ShaderResourceViewDescription ShaderResourceView::Description::get()
	{
		D3D10_SHADER_RESOURCE_VIEW_DESC nativeDescription;
		InternalPointer->GetDesc( &nativeDescription );
		return ShaderResourceViewDescription( nativeDescription );
	}

	ShaderResourceView^ ShaderResourceView::FromFile(SlimDX::Direct3D10::Device^ device, System::String^ fileName)
	{
		ID3D10ShaderResourceView* resource = ConstructFromFile( device, fileName, 0 );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ShaderResourceView^ ShaderResourceView::FromFile(SlimDX::Direct3D10::Device^ device, System::String^ fileName, ImageLoadInformation loadInformation)
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInformation.CreateNativeVersion();
		ID3D10ShaderResourceView* resource = ConstructFromFile( device, fileName, &info );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ShaderResourceView^ ShaderResourceView::FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory )
	{
		ID3D10ShaderResourceView* resource = ConstructFromMemory( device, memory, NULL );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ShaderResourceView^ ShaderResourceView::FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, ImageLoadInformation loadInfo )
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D10ShaderResourceView* resource = ConstructFromMemory( device, memory, &info );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ShaderResourceView^ ShaderResourceView::FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes )
	{
		ID3D10ShaderResourceView* resource = ConstructFromStream( device, stream, sizeInBytes, NULL );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ShaderResourceView^ ShaderResourceView::FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo )
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D10ShaderResourceView* resource = ConstructFromStream( device, stream, sizeInBytes, &info );
		if( resource == 0 )
			return nullptr;

		return ShaderResourceView::FromPointer( static_cast<ID3D10ShaderResourceView*>( resource ) );
	}

	ID3D10ShaderResourceView* ShaderResourceView::ConstructFromFile(SlimDX::Direct3D10::Device^ device, System::String^ fileName, D3DX10_IMAGE_LOAD_INFO* loadInformation)
	{
		ID3D10ShaderResourceView* resource = 0;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		HRESULT hr = D3DX10CreateShaderResourceViewFromFile( device->InternalPointer, pinnedName, loadInformation, 0, &resource, 0 );
		RECORD_D3D10( hr );
		
		return resource;
	}

	ID3D10ShaderResourceView* ShaderResourceView::ConstructFromMemory(SlimDX::Direct3D10::Device^ device, array<Byte>^ memory, D3DX10_IMAGE_LOAD_INFO* loadInformation)
	{
		ID3D10ShaderResourceView* resource = 0;
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		HRESULT hr = D3DX10CreateShaderResourceViewFromMemory( device->InternalPointer, pinnedMemory, memory->Length, loadInformation, 0, &resource, 0 );
		RECORD_D3D10( hr );
		
		return resource;
	}

	ID3D10ShaderResourceView* ShaderResourceView::ConstructFromStream( SlimDX::Direct3D10::Device^ device, Stream^ stream, int sizeInBytes, D3DX10_IMAGE_LOAD_INFO* info )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ memory = SlimDX::Utilities::ReadStream( stream, sizeInBytes, &ds );
		
		if( memory == nullptr )
		{
			ID3D10ShaderResourceView* resource = NULL;
			SIZE_T size = static_cast<SIZE_T>( ds->RemainingLength );

			HRESULT hr = D3DX10CreateShaderResourceViewFromMemory( device->InternalPointer, ds->SeekToEnd(), size, info, NULL, &resource, NULL );
			RECORD_D3D10( hr );

			return resource;
		}

		return ConstructFromMemory( device, memory, info );
	}
}
}
