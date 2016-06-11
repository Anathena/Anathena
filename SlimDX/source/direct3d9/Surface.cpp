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
#include <vcclr.h>

#include "../ComObject.h"
#include "../DataStream.h"
#include "../InternalHelpers.h"

#include "Direct3D9Exception.h"

#include "DeviceEx.h"
#include "Surface.h"
#include "Texture.h"

using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace msclr::interop;

namespace SlimDX
{
namespace Direct3D9
{
	int Surface::ComputeLockedSize(const D3DLOCKED_RECT& lockedRect)
	{
		D3DSURFACE_DESC desc;
		InternalPointer->GetDesc(&desc);

		int height = desc.Height;
		//compute size differently for DXT surfaces
		if(desc.Format >= D3DFMT_DXT1 && desc.Format <= D3DFMT_DXT5 && desc.Height > 0)
			height = (desc.Height + 3) / 4;

		return lockedRect.Pitch * height;
	}

	Surface^ Surface::CreateRenderTarget( SlimDX::Direct3D9::Device^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool lockable )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateRenderTarget( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, lockable, &surface, NULL );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Surface^ Surface::CreateOffscreenPlain( SlimDX::Direct3D9::Device^ device, int width, int height, Format format, Pool pool )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateOffscreenPlainSurface( width, height,
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &surface, NULL );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;

		return result;
	}

	Surface^ Surface::CreateDepthStencil( SlimDX::Direct3D9::Device^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool discard )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateDepthStencilSurface( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, discard, &surface, NULL );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Surface^ Surface::CreateRenderTarget( SlimDX::Direct3D9::Device^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool lockable, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE sharedHandleNative = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateRenderTarget( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, lockable, &surface, &sharedHandleNative );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		sharedHandle = IntPtr(sharedHandleNative);
		return result;
	}

	Surface^ Surface::CreateOffscreenPlain( SlimDX::Direct3D9::Device^ device, int width, int height, Format format,
		Pool pool, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE sharedHandleNative = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateOffscreenPlainSurface( width, height,
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &surface, &sharedHandleNative );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;
		sharedHandle = IntPtr(sharedHandleNative);

		return result;
	}

	Surface^ Surface::CreateDepthStencil( SlimDX::Direct3D9::Device^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool discard, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE sharedHandleNative = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateDepthStencilSurface( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, discard, &surface, &sharedHandleNative );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		sharedHandle = IntPtr(sharedHandleNative);
		return result;
	}

	Surface^ Surface::CreateRenderTargetEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool lockable, Usage usage )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateRenderTargetEx( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality,
			lockable, &surface, NULL, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Surface^ Surface::CreateRenderTargetEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool lockable, Usage usage, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE localHandle = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateRenderTargetEx( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, lockable,
			&surface, &localHandle, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		sharedHandle = IntPtr( localHandle );
		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Surface^ Surface::CreateOffscreenPlainEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height,
		Format format, Pool pool, Usage usage )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateOffscreenPlainSurfaceEx( width, height,
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &surface,
			NULL, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;

		return result;
	}

	Surface^ Surface::CreateOffscreenPlainEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height,
		Format format, Pool pool, Usage usage, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE localHandle = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateOffscreenPlainSurfaceEx( width, height,
			static_cast<D3DFORMAT>( format ), static_cast<D3DPOOL>( pool ), &surface,
			&localHandle, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		sharedHandle = IntPtr( localHandle );
		Surface^ result = gcnew Surface( surface, nullptr );
		if( pool == Pool::Default )
			result->IsDefaultPool = true;

		return result;
	}

	Surface^ Surface::CreateDepthStencilEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool discard, Usage usage )
	{
		IDirect3DSurface9* surface = NULL;

		HRESULT hr = device->InternalPointer->CreateDepthStencilSurfaceEx( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, discard,
			&surface, NULL, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Surface^ Surface::CreateDepthStencilEx( SlimDX::Direct3D9::DeviceEx^ device, int width, int height, Format format,
		MultisampleType multiSampleType, int multiSampleQuality, bool discard, Usage usage, IntPtr% sharedHandle )
	{
		IDirect3DSurface9* surface = NULL;
		HANDLE localHandle = sharedHandle.ToPointer();

		HRESULT hr = device->InternalPointer->CreateDepthStencilSurfaceEx( width, height, static_cast<D3DFORMAT>( format ),
			static_cast<D3DMULTISAMPLE_TYPE>( multiSampleType ), multiSampleQuality, discard,
			&surface, &localHandle, static_cast<DWORD>( usage ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		sharedHandle = IntPtr( localHandle );
		Surface^ result = gcnew Surface( surface, nullptr );
		result->IsDefaultPool = true;
		return result;
	}

	Result Surface::FromMemory_Internal( Surface^ surface, const void* memory, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, const RECT* sourceRectangle, const RECT* destinationRectangle, 
		array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette )
	{
		pin_ptr<PaletteEntry> pinnedSourcePalette = sourcePalette == nullptr ? nullptr : &sourcePalette[0];
		pin_ptr<PaletteEntry> pinnedDestPalette = destinationPalette == nullptr ? nullptr : &destinationPalette[0];

		HRESULT hr = D3DXLoadSurfaceFromMemory( surface->InternalPointer,
			reinterpret_cast<const PALETTEENTRY*>( pinnedDestPalette ),
			destinationRectangle, memory, static_cast<D3DFORMAT>( sourceFormat ),
			sourcePitch, reinterpret_cast<const PALETTEENTRY*>( pinnedSourcePalette ),
			sourceRectangle, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );

		return RECORD_D3D9( hr );
	}

	Result Surface::FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ sourcePalette,
		array<PaletteEntry>^ destinationPalette )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromMemory_Internal( surface, pinnedMemory, filter, colorKey, sourceFormat, sourcePitch,
			&source, &dest, sourcePalette, destinationPalette );
	}

	Result Surface::FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		System::Drawing::Rectangle destinationRectangle )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromMemory_Internal( surface, pinnedMemory, filter, colorKey, sourceFormat, sourcePitch,
			&source, &dest, nullptr, nullptr );
	}

	Result Surface::FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		array<PaletteEntry>^ sourcePalette,	array<PaletteEntry>^ destinationPalette )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromMemory_Internal( surface, pinnedMemory, filter, colorKey, sourceFormat, sourcePitch,
			&source, NULL, sourcePalette, destinationPalette );
	}

	Result Surface::FromMemory( Surface^ surface, array<System::Byte>^ memory, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromMemory_Internal( surface, pinnedMemory, filter, colorKey, sourceFormat, sourcePitch,
			&source, NULL, nullptr, nullptr );
	}

	Result Surface::FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		System::Drawing::Rectangle destinationRectangle, array<PaletteEntry>^ sourcePalette,
		array<PaletteEntry>^ destinationPalette )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );
			RECT dest = marshal_as<RECT>( destinationRectangle );

			return FromMemory_Internal( surface, ds->SeekToEnd(), filter, colorKey, sourceFormat, sourcePitch,
				&source, &dest, sourcePalette, destinationPalette );
		}

		return Surface::FromMemory( surface, data, filter, colorKey, sourceFormat, sourcePitch,
			sourceRectangle, destinationRectangle, sourcePalette, destinationPalette );
	}

	Result Surface::FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		array<PaletteEntry>^ sourcePalette, array<PaletteEntry>^ destinationPalette )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );

			return FromMemory_Internal( surface, ds->SeekToEnd(), filter, colorKey, sourceFormat, sourcePitch,
				&source, NULL, sourcePalette, destinationPalette );
		}

		return Surface::FromMemory( surface, data, filter, colorKey, sourceFormat, sourcePitch,
			sourceRectangle, sourcePalette, destinationPalette );
	}

	Result Surface::FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle,
		System::Drawing::Rectangle destinationRectangle )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );
			RECT dest = marshal_as<RECT>( destinationRectangle );

			return FromMemory_Internal( surface, ds->SeekToEnd(), filter, colorKey, sourceFormat, sourcePitch,
				&source, &dest, nullptr, nullptr );
		}

		return Surface::FromMemory( surface, data, filter, colorKey, sourceFormat, sourcePitch,
			sourceRectangle, destinationRectangle );
	}

	Result Surface::FromStream( Surface^ surface, System::IO::Stream^ stream, Filter filter, int colorKey,
		Format sourceFormat, int sourcePitch, System::Drawing::Rectangle sourceRectangle )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );

			return FromMemory_Internal( surface, ds->SeekToEnd(), filter, colorKey, sourceFormat, sourcePitch,
				&source, NULL, nullptr, nullptr );
		}

		return Surface::FromMemory( surface, data, filter, colorKey, sourceFormat, sourcePitch, sourceRectangle );
	}

	Result Surface::FromFileInMemory_Internal( Surface^ surface, const void* memory, UINT size, Filter filter,
		int colorKey, const RECT* sourceRectangle, const RECT* destinationRectangle,
		array<PaletteEntry>^ palette, ImageInformation* imageInformation )
	{
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];

		HRESULT hr = D3DXLoadSurfaceFromFileInMemory( surface->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ), destinationRectangle, memory, size, sourceRectangle, 
			static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( imageInformation ) );
		
		return RECORD_D3D9( hr );
	}

	Result Surface::FromFileInMemory( Surface^ surface, array<Byte>^ memory, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle,	System::Drawing::Rectangle destinationRectangle,
		array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		return FromFileInMemory_Internal( surface, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&source, &dest, palette, pinnedImageInfo );
	}

	Result Surface::FromFileInMemory( Surface^ surface, array<Byte>^ memory, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle,	System::Drawing::Rectangle destinationRectangle,
		[Out] ImageInformation% imageInformation )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		return FromFileInMemory_Internal( surface, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&source, &dest, nullptr, pinnedImageInfo );
	}

	Result Surface::FromFileInMemory( Surface^ surface, array<Byte>^ memory, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle,	System::Drawing::Rectangle destinationRectangle )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromFileInMemory_Internal( surface, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&source, &dest, nullptr, NULL );
	}

	Result Surface::FromFileInMemory( Surface^ surface, array<Byte>^ memory, Filter filter, int colorKey )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return FromFileInMemory_Internal( surface, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			NULL, NULL, nullptr, NULL );
	}

	Result Surface::FromFileInStream( Surface^ surface, Stream^ stream, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle,
		array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );
			RECT dest = marshal_as<RECT>( destinationRectangle );

			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromFileInMemory_Internal( surface, ds->SeekToEnd(), size, filter, colorKey,
				&source, &dest, palette, pinnedImageInfo );
		}

		return Surface::FromFileInMemory( surface, data, filter, colorKey, sourceRectangle, destinationRectangle,
			palette, imageInformation );
	}

	Result Surface::FromFileInStream( Surface^ surface, Stream^ stream, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle,
		[Out] ImageInformation% imageInformation )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );
			RECT dest = marshal_as<RECT>( destinationRectangle );

			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromFileInMemory_Internal( surface, ds->SeekToEnd(), size, filter, colorKey,
				&source, &dest, nullptr, pinnedImageInfo );
		}

		return Surface::FromFileInMemory( surface, data, filter, colorKey, sourceRectangle, destinationRectangle,
			imageInformation );
	}

	Result Surface::FromFileInStream( Surface^ surface, Stream^ stream, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			RECT source = marshal_as<RECT>( sourceRectangle );
			RECT dest = marshal_as<RECT>( destinationRectangle );

			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromFileInMemory_Internal( surface, ds->SeekToEnd(), size, filter, colorKey,
				&source, &dest, nullptr, NULL );
		}

		return Surface::FromFileInMemory( surface, data, filter, colorKey, sourceRectangle, destinationRectangle );
	}

	Result Surface::FromFileInStream( Surface^ surface, Stream^ stream, Filter filter, int colorKey )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromFileInMemory_Internal( surface, ds->SeekToEnd(), size, filter, colorKey,
				NULL, NULL, nullptr, NULL );
		}

		return Surface::FromFileInMemory( surface, data, filter, colorKey );
	}

	Result Surface::FromFile( Surface^ surface, String^ fileName, Filter filter, int colorKey, 
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle,
		array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		HRESULT hr = D3DXLoadSurfaceFromFile( surface->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ), &dest, pinnedName,
			&source, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ) );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromFile( Surface^ surface, String^ fileName, Filter filter, int colorKey, 
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle,
		[Out] ImageInformation% imageInformation )
	{
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		HRESULT hr = D3DXLoadSurfaceFromFile( surface->InternalPointer, NULL, &dest, pinnedName,
			&source, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ) );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromFile( Surface^ surface, String^ fileName, Filter filter, int colorKey, 
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		HRESULT hr = D3DXLoadSurfaceFromFile( surface->InternalPointer, NULL, &dest, pinnedName,
			&source, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), NULL );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromFile( Surface^ surface, String^ fileName, Filter filter, int colorKey )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXLoadSurfaceFromFile( surface->InternalPointer, NULL, NULL, pinnedName,
			NULL, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), NULL );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle,
		array<PaletteEntry>^ destinationPalette, array<PaletteEntry>^ sourcePalette )
	{
		pin_ptr<PaletteEntry> pinnedSource = sourcePalette == nullptr ? nullptr : &sourcePalette[0];
		pin_ptr<PaletteEntry> pinnedDest = destinationPalette == nullptr ? nullptr : &destinationPalette[0];

		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		HRESULT hr = D3DXLoadSurfaceFromSurface( destinationSurface->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( pinnedDest ), &dest,
			sourceSurface->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedSource ), 
			&source, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey,
		System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle )
	{
		RECT source = marshal_as<RECT>( sourceRectangle );
		RECT dest = marshal_as<RECT>( destinationRectangle );

		HRESULT hr = D3DXLoadSurfaceFromSurface( destinationSurface->InternalPointer, 
			NULL, &dest, sourceSurface->InternalPointer, NULL, &source, static_cast<DWORD>( filter ), 
			static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	Result Surface::FromSurface( Surface^ destinationSurface, Surface^ sourceSurface, Filter filter, int colorKey )
	{
		HRESULT hr = D3DXLoadSurfaceFromSurface( destinationSurface->InternalPointer, NULL, NULL, 
			sourceSurface->InternalPointer, NULL, NULL, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	DataStream^ Surface::ToStream( Surface^ surface, ImageFileFormat format, System::Drawing::Rectangle rectangle,
		array<PaletteEntry>^ palette )
	{
		ID3DXBuffer *result = NULL;
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];

		RECT rect = marshal_as<RECT>( rectangle );

		HRESULT hr = D3DXSaveSurfaceToFileInMemory( &result, static_cast<D3DXIMAGE_FILEFORMAT>( format ),
			surface->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ), &rect );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ Surface::ToStream( Surface^ surface, ImageFileFormat format, System::Drawing::Rectangle rectangle )
	{
		ID3DXBuffer *result = NULL;
		RECT rect = marshal_as<RECT>( rectangle );

		HRESULT hr = D3DXSaveSurfaceToFileInMemory( &result, static_cast<D3DXIMAGE_FILEFORMAT>( format ),
			surface->InternalPointer, NULL, &rect );
	
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ Surface::ToStream( Surface^ surface, ImageFileFormat format )
	{
		ID3DXBuffer *result = NULL;

		HRESULT hr = D3DXSaveSurfaceToFileInMemory( &result, static_cast<D3DXIMAGE_FILEFORMAT>( format ),
			surface->InternalPointer, NULL, NULL );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	Result Surface::ToFile( Surface^ surface, String^ fileName, ImageFileFormat format, 
		System::Drawing::Rectangle rectangle, array<PaletteEntry>^ palette )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];

		RECT rect = marshal_as<RECT>( rectangle );
		
		HRESULT hr = D3DXSaveSurfaceToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			surface->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ), &rect );
		return RECORD_D3D9( hr );
	}

	Result Surface::ToFile( Surface^ surface, String^ fileName, ImageFileFormat format, 
		System::Drawing::Rectangle rectangle )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		RECT rect = marshal_as<RECT>( rectangle );
		
		HRESULT hr = D3DXSaveSurfaceToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			surface->InternalPointer, NULL, &rect );
		return RECORD_D3D9( hr );
	}

	Result Surface::ToFile( Surface^ surface, String^ fileName, ImageFileFormat format )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		
		HRESULT hr = D3DXSaveSurfaceToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			surface->InternalPointer, NULL, NULL );
		return RECORD_D3D9( hr );
	}

	SurfaceDescription Surface::Description::get()
	{
		SurfaceDescription description;

		HRESULT hr = InternalPointer->GetDesc( reinterpret_cast<D3DSURFACE_DESC*>( &description ) );
		RECORD_D3D9( hr );

		return description;
	}

	DataRectangle^ Surface::LockRectangle( LockFlags flags )
	{
		D3DLOCKED_RECT lockedRect;
		
		HRESULT hr = InternalPointer->LockRect( &lockedRect, NULL, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		int lockedSize = ComputeLockedSize(lockedRect);
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataRectangle^ outRect = gcnew DataRectangle( lockedRect.Pitch, gcnew DataStream( lockedRect.pBits, lockedSize, true, !readOnly, false ) );
		return outRect;
	}

	DataRectangle^ Surface::LockRectangle( System::Drawing::Rectangle rect, LockFlags flags )
	{
		D3DLOCKED_RECT lockedRect;
		RECT nativeRect = { rect.Left, rect.Top, rect.Right, rect.Bottom };

		HRESULT hr = InternalPointer->LockRect( &lockedRect, &nativeRect, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		int lockedSize = lockedRect.Pitch * Description.Height;
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataRectangle^ outRect = gcnew DataRectangle( lockedRect.Pitch, gcnew DataStream( lockedRect.pBits, lockedSize, true, !readOnly, false ) );
		return outRect;
	}

	Result Surface::UnlockRectangle()
	{
		HRESULT hr = InternalPointer->UnlockRect();
		return RECORD_D3D9( hr );
	}

	IntPtr Surface::GetDC()
	{
		HDC hdc;
		
		HRESULT hr = InternalPointer->GetDC( &hdc );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return IntPtr::Zero;

		IntPtr ptr( hdc );
		return ptr;
	}

	Result Surface::ReleaseDC( IntPtr hdc )
	{
		HRESULT hr = InternalPointer->ReleaseDC( static_cast<HDC>( hdc.ToPointer() ) );
		return RECORD_D3D9( hr );
	}

	generic<typename TContainer>
	TContainer Surface::GetContainer()
	{
		GUID guid = Utilities::GetNativeGuidForType( TContainer::typeid );
		void *resultPointer;

		HRESULT hr = InternalPointer->GetContainer( guid, &resultPointer );
		if( RECORD_D3D9( hr ).IsFailure )
			return TContainer();

		MethodInfo^ method = TContainer::typeid->GetMethod( "FromPointer", BindingFlags::Public | BindingFlags::Static );
		return safe_cast<TContainer>( method->Invoke( nullptr, gcnew array<Object^> { IntPtr( resultPointer ) } ) );
	}
}
}
