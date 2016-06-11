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

#include "../Utilities.h"
#include "Device.h"
#include "IndexBuffer.h"
#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	IndexBuffer::IndexBuffer( SlimDX::Direct3D9::Device^ device, int sizeInBytes, SlimDX::Direct3D9::Usage usage, SlimDX::Direct3D9::Pool pool, bool sixteenBit )
	{
		IDirect3DIndexBuffer9* ib;
		D3DFORMAT format = sixteenBit ? D3DFMT_INDEX16 : D3DFMT_INDEX32;

		HRESULT hr = device->InternalPointer->CreateIndexBuffer( sizeInBytes, static_cast<DWORD>( usage ), format, static_cast<D3DPOOL>( pool ), &ib, NULL );
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct( ib );

		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}
	
	IndexBuffer::IndexBuffer( SlimDX::Direct3D9::Device^ device, int sizeInBytes, SlimDX::Direct3D9::Usage usage, SlimDX::Direct3D9::Pool pool, bool sixteenBit, [Out] IntPtr% sharedHandle )
	{
		IDirect3DIndexBuffer9* ib;
		D3DFORMAT format = sixteenBit ? D3DFMT_INDEX16 : D3DFMT_INDEX32;
		HANDLE sharedHandleNative = NULL;

		HRESULT hr = device->InternalPointer->CreateIndexBuffer( sizeInBytes, static_cast<DWORD>( usage ), format, static_cast<D3DPOOL>( pool ), &ib, &sharedHandleNative );
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct( ib );
		sharedHandle = IntPtr(sharedHandleNative);

		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}
	
	DataStream^ IndexBuffer::Lock( int offset, int size, LockFlags flags )
	{
		void* lockedPtr;
		HRESULT hr = InternalPointer->Lock( offset, size, &lockedPtr, static_cast<DWORD>( flags ) );
		RECORD_D3D9( hr );
		
		int lockedSize = size == 0 ? Description.SizeInBytes : size;
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataStream^ stream = gcnew DataStream( lockedPtr, lockedSize, true, !readOnly, false );
		return stream;
	}

	Result IndexBuffer::Unlock()
	{
		return RECORD_D3D9( InternalPointer->Unlock() );
	}

	IndexBufferDescription IndexBuffer::Description::get()
	{
		IndexBufferDescription desc;
		HRESULT hr = InternalPointer->GetDesc( reinterpret_cast<D3DINDEXBUFFER_DESC*>( &desc ) );
		RECORD_D3D9( hr );

		return desc;
	}
}
}