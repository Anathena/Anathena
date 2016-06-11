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
#include "stdafx.h"

#include "../DataStream.h"
#include "../ComObject.h"
#include "../Utilities.h"

#include "../math/Vector2.h"

#include "Device.h"
#include "DeviceEx.h"
#include "D3DX.h"
#include "CubeTexture.h"

#include "Direct3D9Exception.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	void WINAPI NativeD3DXFill2D(D3DXVECTOR4 *out, CONST D3DXVECTOR2 *pTexCoord, CONST D3DXVECTOR2 *pTexelSize, LPVOID data);
	void WINAPI NativeD3DXFill3D(D3DXVECTOR4 *out, CONST D3DXVECTOR3 *pTexCoord, CONST D3DXVECTOR3 *pTexelSize, LPVOID data);

	CubeTexture::CubeTexture( SlimDX::Direct3D9::Device^ device, int edgeLength, int numLevels, Usage usage, Format format, Pool pool )
	{
		IDirect3DCubeTexture9* texture = NULL;
		HRESULT hr = device->InternalPointer->CreateCubeTexture( edgeLength, numLevels, static_cast<DWORD>( usage ),
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &texture, NULL );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(texture);
		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}

	CubeTexture::CubeTexture( SlimDX::Direct3D9::Device^ device, int edgeLength, int numLevels, Usage usage, Format format, Pool pool, [Out] IntPtr% sharedHandle )
	{
		IDirect3DCubeTexture9* texture = NULL;
		HANDLE sharedHandleNative = NULL;
		HRESULT hr = device->InternalPointer->CreateCubeTexture( edgeLength, numLevels, static_cast<DWORD>( usage ),
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &texture, &sharedHandleNative );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(texture);
		sharedHandle = IntPtr(sharedHandleNative);
		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}

	CubeTextureRequirements CubeTexture::CheckRequirements(SlimDX::Direct3D9::Device^ device, int size,
		int numMipLevels, Usage usage, Format format, Pool pool)
	{
		CubeTextureRequirements result;					// Result.
		D3DFORMAT d3dFormat = static_cast<D3DFORMAT>( format );	// Format.
		HRESULT hr;									// Error code.

		// Get texture requirements.
		hr = D3DXCheckCubeTextureRequirements(device->InternalPointer, reinterpret_cast<UINT*>( &size ), 
			reinterpret_cast<UINT*>( &numMipLevels ), static_cast<DWORD>( usage ),
			reinterpret_cast<D3DFORMAT*>( &d3dFormat ), static_cast<D3DPOOL>( pool ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return CubeTextureRequirements();

		// Return proposed values.
		result.Size = size;
		result.Format = format;
		result.MipLevelCount = numMipLevels;

		return result;
	}

	CubeTexture^ CubeTexture::FromMemory_Internal( SlimDX::Direct3D9::Device^ device, const void* memory, UINT sizeBytes,
		int size, int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		ImageInformation* imageInformation, PaletteEntry* palette )
	{
		IDirect3DCubeTexture9* texture = NULL;

		HRESULT hr = D3DXCreateCubeTextureFromFileInMemoryEx( device->InternalPointer, memory, sizeBytes, size, numLevels,
			static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ), static_cast<DWORD>( mipFilter ),
			static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( imageInformation ), 
			reinterpret_cast<PALETTEENTRY*>( palette ), &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
		{
			palette = nullptr;
			return nullptr;
		}

		CubeTexture^ result = gcnew CubeTexture( texture, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	CubeTexture^ CubeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		imageInformation = ImageInformation();
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		palette = gcnew array<PaletteEntry>( 256 );
		pin_ptr<PaletteEntry> pinnedPalette = &palette[0];

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), size, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, pinnedPalette );
	}

	CubeTexture^ CubeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), size, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, NULL );
	}

	CubeTexture^ CubeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), size, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, NULL, NULL );
	}

	CubeTexture^ CubeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, Usage usage, Pool pool )
	{
		return CubeTexture::FromMemory( device, memory, D3DX::Default, D3DX::Default,
			usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	CubeTexture^ CubeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory )
	{
		return CubeTexture::FromMemory( device, memory, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette )
	{
		DataStream^ ds;
		array<Byte>^ data = Utilities::ReadStream( stream, sizeBytes, &ds );
		
		if( data == nullptr )
		{
			palette = gcnew array<PaletteEntry>( 256 );
			pin_ptr<PaletteEntry> pinnedPalette = &palette[0];
			imageInformation = ImageInformation();
			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

			CubeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, size, numLevels,
				usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, pinnedPalette );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return CubeTexture::FromMemory( device, data, size, numLevels, usage, format, pool, filter, mipFilter, 
			colorKey, imageInformation, palette );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		DataStream^ ds;
		array<Byte>^ data = Utilities::ReadStream( stream, sizeBytes, &ds );
		
		if( data == nullptr )
		{
			imageInformation = ImageInformation();
			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

			CubeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, size, numLevels,
				usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, NULL );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return CubeTexture::FromMemory( device, data, size, numLevels, usage, format, pool, filter, mipFilter, 
			colorKey, imageInformation );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		DataStream^ ds;
		array<Byte>^ data = Utilities::ReadStream( stream, sizeBytes, &ds );
		
		if( data == nullptr )
		{
			CubeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, size, numLevels,
				usage, format, pool, filter, mipFilter, colorKey, NULL, NULL );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return CubeTexture::FromMemory( device, data, size, numLevels, usage, format, pool, filter, mipFilter, colorKey );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		return CubeTexture::FromStream( device, stream, 0, size, numLevels, usage, format, pool, filter, mipFilter, colorKey );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, Usage usage, Pool pool )
	{
		return CubeTexture::FromStream( device, stream, D3DX::Default, D3DX::Default,
			usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	CubeTexture^ CubeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream )
	{
		return CubeTexture::FromStream( device, stream, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	CubeTexture^ CubeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette )
	{
		IDirect3DCubeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		imageInformation = ImageInformation();
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		palette = gcnew array<PaletteEntry>( 256 );
		pin_ptr<PaletteEntry> pinnedPalette = &palette[0];

		HRESULT hr = D3DXCreateCubeTextureFromFileEx( device->InternalPointer, pinnedName, size, 
			numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ), static_cast<DWORD>( mipFilter ), 
			colorKey, reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ), reinterpret_cast<PALETTEENTRY*>( pinnedPalette ), &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
		{
			palette = nullptr;
			return nullptr;
		}

		CubeTexture^ result = gcnew CubeTexture( texture, nullptr );;
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	CubeTexture^ CubeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		IDirect3DCubeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

		HRESULT hr = D3DXCreateCubeTextureFromFileEx( device->InternalPointer, pinnedName, size, 
			numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ), static_cast<DWORD>( mipFilter ), 
			colorKey, reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ), NULL, &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		CubeTexture^ result = gcnew CubeTexture( texture, nullptr );;
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	CubeTexture^ CubeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int size, int numLevels,
		Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		IDirect3DCubeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXCreateCubeTextureFromFileEx( device->InternalPointer, pinnedName, size, 
			numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ), static_cast<DWORD>( mipFilter ), 
			colorKey, NULL, NULL, &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		CubeTexture^ result = gcnew CubeTexture( texture, nullptr );;
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	CubeTexture^ CubeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, Usage usage, Pool pool )
	{
		return CubeTexture::FromFile( device, fileName, D3DX::Default, D3DX::Default,
			usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	CubeTexture^ CubeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName )
	{
		return CubeTexture::FromFile( device, fileName, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	DataRectangle^ CubeTexture::LockRectangle( CubeMapFace face, int level, System::Drawing::Rectangle rect, LockFlags flags )
	{
		D3DLOCKED_RECT lockedRect;
		RECT nativeRect = { rect.Left, rect.Top, rect.Right, rect.Bottom };
		HRESULT hr = InternalPointer->LockRect( static_cast<D3DCUBEMAP_FACES>( face ), level, &lockedRect, &nativeRect, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		
		D3DSURFACE_DESC desc;
		hr = InternalPointer->GetLevelDesc(level, &desc);
		if (RECORD_D3D9(hr).IsFailure)
			return nullptr;

		int lockedSize = lockedRect.Pitch * desc.Height;
		if (Utilities::IsCompressed(desc.Format))
			lockedSize /= 4;	// all compression blocks are 4x4
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataRectangle^ outRect = gcnew DataRectangle( lockedRect.Pitch, gcnew DataStream( lockedRect.pBits, lockedSize, true, !readOnly, false ) );
		return outRect;
	}

	DataRectangle^ CubeTexture::LockRectangle( CubeMapFace face, int level, LockFlags flags )
	{
		D3DLOCKED_RECT lockedRect;
		HRESULT hr = InternalPointer->LockRect( static_cast<D3DCUBEMAP_FACES>( face ), level, &lockedRect, NULL, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		
		D3DSURFACE_DESC desc;
		hr = InternalPointer->GetLevelDesc(level, &desc);
		if (RECORD_D3D9(hr).IsFailure)
			return nullptr;

		int lockedSize = lockedRect.Pitch * desc.Height;
		if (Utilities::IsCompressed(desc.Format))
			lockedSize /= 4;	// all compression blocks are 4x4

		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataRectangle^ outRect = gcnew DataRectangle( lockedRect.Pitch, gcnew DataStream( lockedRect.pBits, lockedSize, true, !readOnly, false ) );
		return outRect;
	}

	Result CubeTexture::UnlockRectangle( CubeMapFace face, int level )
	{
		HRESULT hr = InternalPointer->UnlockRect( static_cast<D3DCUBEMAP_FACES>( face ), level );
		return RECORD_D3D9( hr );
	}

	Result CubeTexture::AddDirtyRectangle( CubeMapFace face )
	{
		HRESULT hr = InternalPointer->AddDirtyRect( static_cast<D3DCUBEMAP_FACES>( face ), NULL );
		return RECORD_D3D9( hr );
	}

	Result CubeTexture::AddDirtyRectangle( CubeMapFace face, System::Drawing::Rectangle rect )
	{
		RECT nativeRect = { rect.Left, rect.Top, rect.Right, rect.Bottom };
		HRESULT hr = InternalPointer->AddDirtyRect( static_cast<D3DCUBEMAP_FACES>( face ), &nativeRect );
		return RECORD_D3D9( hr );
	}

	SurfaceDescription CubeTexture::GetLevelDescription( int level )
	{
		SurfaceDescription description;
		HRESULT hr = InternalPointer->GetLevelDesc( level, reinterpret_cast<D3DSURFACE_DESC*>( &description ) );
		RECORD_D3D9( hr );
		return description;
	}

	Surface^ CubeTexture::GetCubeMapSurface( CubeMapFace face, int level )
	{
		IDirect3DSurface9* surface;
		HRESULT hr = InternalPointer->GetCubeMapSurface( static_cast<D3DCUBEMAP_FACES>( face ), level, &surface );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		return Surface::FromPointer( surface );
	}

	Result CubeTexture::Fill(Fill3DCallback^ callback)
	{
		HRESULT hr;		// Error code.

		// Call the function.
		hr = D3DXFillCubeTexture(InternalPointer, NativeD3DXFill3D, Marshal::GetFunctionPointerForDelegate(callback).ToPointer());

		return RECORD_D3D9(hr);
	}

	Result CubeTexture::Fill( TextureShader^ shader )
	{
		HRESULT hr = D3DXFillCubeTextureTX( InternalPointer, shader->InternalPointer );
		return RECORD_D3D9( hr );
	}
}
}