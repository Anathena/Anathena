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

#include <d3d11.h>

#include "../stack_array.h"
#include "../DataBox.h"
#include "../DataStream.h"

#include "Direct3D11Exception.h"

#include "Device11.h"
#include "DeviceContext11.h"
#include "Texture3D11.h"
#include "Texture3DDescription11.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Direct3D11
{ 
	Texture3D::Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description )
	{
		Construct( Build( device, description, 0 ) );
	}

	Texture3D::Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, DataBox^ data )
	{
		if( data != nullptr )
		{
			D3D11_SUBRESOURCE_DATA initialData;
			initialData.pSysMem = data->Data->PositionPointer;
			initialData.SysMemPitch = data->RowPitch;
			initialData.SysMemSlicePitch = data->SlicePitch;
			Construct( Build( device, description, &initialData ) );	
		}
		else 
		{
			Construct( Build( device, description, 0 ) );	
		}
	}
	
	Texture3D::Texture3D( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, array<DataBox^>^ data )
	{
		if( data != nullptr )
		{
			stack_array<D3D11_SUBRESOURCE_DATA> initialData = stackalloc( D3D11_SUBRESOURCE_DATA, data->Length );
			for( size_t dataIndex = 0; dataIndex < initialData.size(); ++dataIndex ) 
			{
				initialData[dataIndex].pSysMem = data[static_cast<int>( dataIndex )]->Data->PositionPointer;
				initialData[dataIndex].SysMemPitch = data[static_cast<int>( dataIndex )]->RowPitch;
				initialData[dataIndex].SysMemSlicePitch = data[static_cast<int>( dataIndex )]->SlicePitch;
			}
			
			Construct( Build( device, description, &initialData[0] ) );	
		} 
		else
		{
			Construct( Build( device, description, 0 ) );	
		}
	}
	
	ID3D11Texture3D* Texture3D::Build( SlimDX::Direct3D11::Device^ device, Texture3DDescription description, D3D11_SUBRESOURCE_DATA* data )
	{
		ID3D11Texture3D* texture = 0;
		D3D11_TEXTURE3D_DESC nativeDescription = description.CreateNativeVersion();
		
		if( RECORD_D3D11( device->InternalPointer->CreateTexture3D( &nativeDescription, data, &texture ) ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );
		
		return texture;
	} 
	
	Texture3DDescription Texture3D::Description::get()
	{
		D3D11_TEXTURE3D_DESC nativeDescription;
		InternalPointer->GetDesc( &nativeDescription );
		return Texture3DDescription( nativeDescription );
	}
	
	Texture3D^ Texture3D::FromFile( SlimDX::Direct3D11::Device^ device, String^ fileName )
	{
		ID3D11Resource* resource = Resource::ConstructFromFile( device, fileName, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}
	
	Texture3D^ Texture3D::FromMemory( SlimDX::Direct3D11::Device^ device, array<Byte>^ memory )
	{
		ID3D11Resource* resource = Resource::ConstructFromMemory( device, memory, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}
	
	Texture3D^ Texture3D::FromStream( SlimDX::Direct3D11::Device^ device, Stream^ stream, int sizeInBytes )
	{
		ID3D11Resource* resource = Resource::ConstructFromStream( device, stream, sizeInBytes, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}
	
	Texture3D^ Texture3D::FromFile( SlimDX::Direct3D11::Device^ device, String^ fileName, ImageLoadInformation loadInfo )
	{
		D3DX11_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D11Resource* resource = Resource::ConstructFromFile( device, fileName, &info );
		if( resource == 0 )
			return nullptr;

		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}

	Texture3D^ Texture3D::FromMemory( SlimDX::Direct3D11::Device^ device, array<Byte>^ memory, ImageLoadInformation loadInfo )
	{
		D3DX11_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D11Resource* resource = Resource::ConstructFromMemory( device, memory, &info );
		if( resource == 0 )
			return nullptr;

		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}

	Texture3D^ Texture3D::FromStream( SlimDX::Direct3D11::Device^ device, Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo )
	{
		D3DX11_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D11Resource* resource = Resource::ConstructFromStream( device, stream, sizeInBytes, &info );
		if( resource == 0 )
			return nullptr;

		D3D11_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D11_RESOURCE_DIMENSION_TEXTURE3D )
			throw gcnew InvalidOperationException( "Could not load file as 3D texture." );

		return Texture3D::FromPointer( static_cast<ID3D11Texture3D*>( resource ) );
	}

	Result Texture3D::ToFile( DeviceContext^ context, Texture3D^ texture, ImageFileFormat format, String^ fileName )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DX11SaveTextureToFile( context->InternalPointer, texture->InternalPointer, static_cast<D3DX11_IMAGE_FILE_FORMAT>( format ), pinnedName );
		return RECORD_D3D11( hr );
	}

	Result Texture3D::ToStream( DeviceContext^ context, Texture3D^ texture, ImageFileFormat format, Stream^ stream )
	{
		ID3D10Blob* blob = 0;
		HRESULT hr = D3DX11SaveTextureToMemory( context->InternalPointer, texture->InternalPointer, static_cast<D3DX11_IMAGE_FILE_FORMAT>( format ), &blob, 0 );
		if( RECORD_D3D11( hr ).IsFailure )
			return Result::Last;
		
		// Write byte-by-byte to avoid allocating a managed byte[] that will wastefully persist.
		unsigned char* bytes = reinterpret_cast<unsigned char*>( blob->GetBufferPointer() );
		for(SIZE_T byteIndex = 0; byteIndex < blob->GetBufferSize(); ++byteIndex)
			stream->WriteByte( bytes[byteIndex] );
		
		blob->Release();
		return Result::Last;
	}
}
}
