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
#include "VolumeTexture.h"

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

	VolumeTexture::VolumeTexture( SlimDX::Direct3D9::Device^ device, int width, int height, int depth, int numLevels, Usage usage, Format format, Pool pool )
	{
		IDirect3DVolumeTexture9* texture = NULL;
		HRESULT hr = device->InternalPointer->CreateVolumeTexture( width, height, depth, numLevels,
			static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &texture, NULL );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(texture);
		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}

	VolumeTexture::VolumeTexture( SlimDX::Direct3D9::Device^ device, int width, int height, int depth, int numLevels, Usage usage, Format format, Pool pool, [Out] IntPtr% sharedHandle )
	{
		IDirect3DVolumeTexture9* texture = NULL;
		HANDLE sharedHandleNative = NULL;
		HRESULT hr = device->InternalPointer->CreateVolumeTexture( width, height, depth, numLevels,
			static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &texture, &sharedHandleNative );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(texture);
		sharedHandle = IntPtr(sharedHandleNative);
		if( pool == Pool::Default )
			this->IsDefaultPool = true;
	}

	VolumeTextureRequirements VolumeTexture::CheckRequirements(SlimDX::Direct3D9::Device^ device, int width, int height, int depth,
		int numMipLevels, Usage usage, Format format, Pool pool)
	{
		VolumeTextureRequirements result;					// Result.
		D3DFORMAT d3dFormat = static_cast<D3DFORMAT>( format );	// Format.
		HRESULT hr;									// Error code.

		// Get texture requirements.
		hr = D3DXCheckVolumeTextureRequirements(device->InternalPointer,
			reinterpret_cast<UINT*>( &width ),
			reinterpret_cast<UINT*>( &height ),
			reinterpret_cast<UINT*>( &depth ),
			reinterpret_cast<UINT*>( &numMipLevels ),
			static_cast<DWORD>( usage ),
			&d3dFormat,
			static_cast<D3DPOOL>( pool ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return VolumeTextureRequirements();

		// Return proposed values.
		result.Width = width;
		result.Height = height;
		result.Depth = depth;
		result.Format = format;
		result.MipLevelCount = numMipLevels;

		return result;
	}

	VolumeTexture^ VolumeTexture::FromMemory_Internal( SlimDX::Direct3D9::Device^ device, const void* memory, UINT size,
		int width, int height, int depth, int numLevels, Usage usage, Format format, Pool pool, Filter filter,
		Filter mipFilter, int colorKey, ImageInformation* imageInformation, PaletteEntry* palette )
	{
		IDirect3DVolumeTexture9* texture = NULL;

		HRESULT hr = D3DXCreateVolumeTextureFromFileInMemoryEx( device->InternalPointer, memory, size,
			width, height, depth, numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ),
			static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ), static_cast<DWORD>( mipFilter ),
			static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( imageInformation ),
			reinterpret_cast<PALETTEENTRY*>( palette ), &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
		{
			palette = nullptr;
			return nullptr;
		}

		VolumeTexture^ result = gcnew VolumeTexture( texture, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	VolumeTexture^ VolumeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		imageInformation = ImageInformation();
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		palette = gcnew array<PaletteEntry>( 256 );
		pin_ptr<PaletteEntry> pinnedPalette = &palette[0];

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, pinnedPalette );
	}

	VolumeTexture^ VolumeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, NULL );
	}

	VolumeTexture^ VolumeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromMemory_Internal( device, pinnedMemory, static_cast<UINT>( memory->Length ), width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, NULL, NULL );
	}

	VolumeTexture^ VolumeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory, Usage usage, Pool pool )
	{
		return VolumeTexture::FromMemory( device, memory, D3DX::Default, D3DX::Default, D3DX::Default, D3DX::Default,
			usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	VolumeTexture^ VolumeTexture::FromMemory( SlimDX::Direct3D9::Device^ device, array<Byte>^ memory )
	{
		return VolumeTexture::FromMemory( device, memory, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
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

			VolumeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, width, height, depth,
				numLevels, usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, pinnedPalette );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return VolumeTexture::FromMemory( device, data, width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, imageInformation, palette );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		DataStream^ ds;
		array<Byte>^ data = Utilities::ReadStream( stream, sizeBytes, &ds );
		
		if( data == nullptr )
		{
			imageInformation = ImageInformation();
			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

			VolumeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, width, height, depth,
				numLevels, usage, format, pool, filter, mipFilter, colorKey, pinnedImageInfo, NULL );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return VolumeTexture::FromMemory( device, data, width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey, imageInformation );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int sizeBytes, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		DataStream^ ds;
		array<Byte>^ data = Utilities::ReadStream( stream, sizeBytes, &ds );
		
		if( data == nullptr )
		{
			VolumeTexture^ texture = FromMemory_Internal( device, ds->PositionPointer, sizeBytes, width, height, depth,
				numLevels, usage, format, pool, filter, mipFilter, colorKey, NULL, NULL );

			ds->Seek( sizeBytes, SeekOrigin::Current );
			return texture;
		}

		return VolumeTexture::FromMemory( device, data, width, height, depth, numLevels,
			usage, format, pool, filter, mipFilter, colorKey );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		return VolumeTexture::FromStream( device, stream, 0, width, height, depth,
			numLevels, usage, format, pool, filter, mipFilter, colorKey );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, Usage usage, Pool pool )
	{
		return VolumeTexture::FromStream( device, stream, D3DX::Default, D3DX::Default, D3DX::Default,
			D3DX::Default, usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	VolumeTexture^ VolumeTexture::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream )
	{
		return VolumeTexture::FromStream( device, stream, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	VolumeTexture^ VolumeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation, [Out] array<PaletteEntry>^% palette )
	{
		IDirect3DVolumeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		imageInformation = ImageInformation();
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		palette = gcnew array<PaletteEntry>( 256 );
		pin_ptr<PaletteEntry> pinnedPalette = &palette[0];

		HRESULT hr = D3DXCreateVolumeTextureFromFileEx( device->InternalPointer, pinnedName, width, height,
			depth, numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ),
			static_cast<DWORD>( mipFilter ), colorKey, reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ), 
			reinterpret_cast<PALETTEENTRY*>( pinnedPalette ), &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
		{
			palette = nullptr;
			return nullptr;
		}

		VolumeTexture^ result = gcnew VolumeTexture( texture, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	VolumeTexture^ VolumeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey,
		[Out] ImageInformation% imageInformation )
	{
		IDirect3DVolumeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;

		HRESULT hr = D3DXCreateVolumeTextureFromFileEx( device->InternalPointer, pinnedName, width, height,
			depth, numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ),
			static_cast<DWORD>( mipFilter ), colorKey, reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ), 
			NULL, &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		VolumeTexture^ result = gcnew VolumeTexture( texture, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	VolumeTexture^ VolumeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, int width, int height, int depth,
		int numLevels, Usage usage, Format format, Pool pool, Filter filter, Filter mipFilter, int colorKey )
	{
		IDirect3DVolumeTexture9* texture = NULL;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXCreateVolumeTextureFromFileEx( device->InternalPointer, pinnedName, width, height,
			depth, numLevels, static_cast<DWORD>( usage ), static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), static_cast<DWORD>( filter ),
			static_cast<DWORD>( mipFilter ), colorKey, NULL, NULL, &texture );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		VolumeTexture^ result = gcnew VolumeTexture( texture, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		return result;
	}

	VolumeTexture^ VolumeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName, Usage usage, Pool pool )
	{
		return VolumeTexture::FromFile( device, fileName, D3DX::Default, D3DX::Default, D3DX::Default,
			D3DX::Default, usage, Format::Unknown, pool, Filter::Default, Filter::Default, 0 );
	}

	VolumeTexture^ VolumeTexture::FromFile( SlimDX::Direct3D9::Device^ device, String^ fileName )
	{
		return VolumeTexture::FromFile( device, fileName, Usage::None, dynamic_cast<DeviceEx^>(device) != nullptr ? Pool::Default : Pool::Managed );
	}

	DataBox^ VolumeTexture::LockBox( int level, Box box, LockFlags flags )
	{
		D3DLOCKED_BOX lockedBox;
		HRESULT hr = InternalPointer->LockBox( level, &lockedBox, reinterpret_cast<D3DBOX*>( &box ),
			static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		
		int lockedSize = lockedBox.SlicePitch * GetLevelDescription( level ).Depth;
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataBox^ outBox = gcnew DataBox( lockedBox.RowPitch, lockedBox.SlicePitch, gcnew DataStream( lockedBox.pBits, lockedSize, true, !readOnly, false ) );
		return outBox;
	}

	DataBox^ VolumeTexture::LockBox( int level, LockFlags flags )
	{
		D3DLOCKED_BOX lockedBox;
		HRESULT hr = InternalPointer->LockBox( level, &lockedBox, NULL, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		
		int lockedSize = lockedBox.SlicePitch * GetLevelDescription( level ).Depth;
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataBox^ outBox = gcnew DataBox( lockedBox.RowPitch, lockedBox.SlicePitch, gcnew DataStream( lockedBox.pBits, lockedSize, true, !readOnly, false ) );
		return outBox;
	}

	Result VolumeTexture::UnlockBox( int level )
	{
		HRESULT hr = InternalPointer->UnlockBox( level );
		return RECORD_D3D9( hr );
	}

	Result VolumeTexture::AddDirtyBox()
	{
		HRESULT hr = InternalPointer->AddDirtyBox( NULL );
		return RECORD_D3D9( hr );
	}

	Result VolumeTexture::AddDirtyBox( Box box )
	{
		HRESULT hr = InternalPointer->AddDirtyBox( reinterpret_cast<D3DBOX*>( &box ) );
		return RECORD_D3D9( hr );
	}
	
	VolumeDescription VolumeTexture::GetLevelDescription( int level )
	{
		D3DVOLUME_DESC description;
		HRESULT hr = InternalPointer->GetLevelDesc( level, &description );
		
		if( RECORD_D3D9(hr).IsFailure )
			return VolumeDescription();
		
		VolumeDescription outDesc;
		outDesc.Format = static_cast<Format>( description.Format );
		outDesc.Type = static_cast<SlimDX::Direct3D9::ResourceType>( description.Type );
		outDesc.Usage = static_cast<Usage>( description.Usage );
		outDesc.Pool = static_cast<Pool>( description.Pool );
		outDesc.Width = description.Width;
		outDesc.Height = description.Height;
		outDesc.Depth = description.Depth;
		
		return outDesc;
	}

	Volume^ VolumeTexture::GetVolumeLevel( int level )
	{
		IDirect3DVolume9* result;

		HRESULT hr = InternalPointer->GetVolumeLevel( level, &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return Volume::FromPointer( result );
	}

	Result VolumeTexture::Fill(Fill3DCallback^ callback)
	{
		HRESULT hr;		// Error code.

		// Call the function.
		hr = D3DXFillVolumeTexture(InternalPointer, NativeD3DXFill3D, Marshal::GetFunctionPointerForDelegate(callback).ToPointer());

		return RECORD_D3D9(hr);
	}

	Result VolumeTexture::Fill( TextureShader^ shader )
	{
		HRESULT hr = D3DXFillVolumeTextureTX( InternalPointer, shader->InternalPointer );
		return RECORD_D3D9( hr );
	}
}
}