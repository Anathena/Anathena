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
#include "../Utilities.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Resource.h"

#include "Surface.h"
#include "Volume.h"
#include "Texture.h"
#include "VolumeTexture.h"
#include "CubeTexture.h"
#include "VertexBuffer.h"
#include "IndexBuffer.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	Resource^ Resource::FromPointer( System::IntPtr pointer )
	{
		if( pointer == System::IntPtr::Zero )
			throw gcnew System::ArgumentNullException( "pointer" );

		Resource^ tableEntry = safe_cast<Resource^>( SlimDX::ObjectTable::Find( static_cast<System::IntPtr>( pointer ) ) );
		if( tableEntry != nullptr )
		{
			return tableEntry;
		}

		void* result = 0;
		IUnknown* unknown = static_cast<IUnknown*>( pointer.ToPointer() );
		HRESULT hr = unknown->QueryInterface(IID_IDirect3DResource9, &result);
		if( FAILED( hr ) )
			throw gcnew InvalidCastException( "Failed to QueryInterface on user-supplied pointer." );

		//find out what this thing actually is
		IDirect3DResource9* resource = static_cast<IDirect3DResource9*>( unknown );
		D3DRESOURCETYPE type = resource->GetType();
		resource->Release();

		//chain to the correct ctor (not the fastest way to do this, but good enough for now)
		switch(type)
		{
		case D3DRTYPE_SURFACE:
			return Surface::FromPointer( pointer );
		case D3DRTYPE_VOLUME:
			return Volume::FromPointer( pointer );
		case D3DRTYPE_TEXTURE:
			return Texture::FromPointer( pointer );
		case D3DRTYPE_VOLUMETEXTURE:
			return VolumeTexture::FromPointer( pointer );
		case D3DRTYPE_CUBETEXTURE:
			return CubeTexture::FromPointer( pointer );
		case D3DRTYPE_VERTEXBUFFER:
			return VertexBuffer::FromPointer( pointer );
		case D3DRTYPE_INDEXBUFFER:
			return IndexBuffer::FromPointer( pointer );

		default:
			throw gcnew InvalidCastException( "Unrecognized resource type." );
		}
	}

	SlimDX::Direct3D9::Device^ Resource::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}
	
	void Resource::Preload()
	{
		InternalPointer->PreLoad();
	}

	System::String^ Resource::DebugName::get()
	{
		char name[1024];
		DWORD size = sizeof(name) - 1;

		if (FAILED(InternalPointer->GetPrivateData(WKPDID_D3DDebugObjectName, &name[0], &size)))
			return "";

		name[size] = 0;
		return gcnew System::String(name);
	}
	
	void Resource::DebugName::set(System::String^ value)
	{
		if (!String::IsNullOrEmpty(value))
		{
			array<Byte>^ valueBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(value);
			pin_ptr<Byte> pinnedValue = &valueBytes[0];

			InternalPointer->SetPrivateData(WKPDID_D3DDebugObjectName, pinnedValue, value->Length, 0);
		}
		else
		{
			InternalPointer->SetPrivateData(WKPDID_D3DDebugObjectName, 0, 0, 0);
		}
	}

	generic<typename T> where T : value class
	Result Resource::SetPrivateData(Guid guid, T data)
	{
		HRESULT hr = InternalPointer->SetPrivateData(Utilities::ConvertManagedGuid(guid), &data, sizeof(T), 0);
		return RECORD_D3D9(hr);
	}

	generic<typename T> where T : value class
	T Resource::GetPrivateData(Guid guid)
	{
		T result;
		DWORD size = sizeof(T);

		HRESULT hr = InternalPointer->GetPrivateData(Utilities::ConvertManagedGuid(guid), &result, &size);
		RECORD_D3D9(hr);

		return result;
	}
}
}