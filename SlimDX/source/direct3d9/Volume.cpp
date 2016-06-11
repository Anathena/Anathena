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

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Volume.h"
#include "Texture.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Direct3D9
{
	VolumeDescription Volume::Description::get()
	{
		VolumeDescription description;

		HRESULT hr = InternalPointer->GetDesc( reinterpret_cast<D3DVOLUME_DESC*>( &description ) );
		RECORD_D3D9( hr );

		return description;
	}

	DataBox^ Volume::LockBox( LockFlags flags )
	{
		D3DLOCKED_BOX lockedBox;

		HRESULT hr = InternalPointer->LockBox( &lockedBox, NULL, static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		int lockedSize = lockedBox.SlicePitch * Description.Depth;

		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataBox^ outBox = gcnew DataBox( lockedBox.RowPitch, lockedBox.SlicePitch, gcnew DataStream( lockedBox.pBits, lockedSize, true, !readOnly, false ) );
		return outBox;
	}

	DataBox^ Volume::LockBox( Box box, LockFlags flags )
	{
		D3DLOCKED_BOX lockedBox;

		HRESULT hr = InternalPointer->LockBox( &lockedBox, reinterpret_cast<D3DBOX*>( &box ), static_cast<DWORD>( flags ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;
		
		int lockedSize = lockedBox.SlicePitch * Description.Depth;
		
		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		DataBox^ outBox = gcnew DataBox( lockedBox.RowPitch, lockedBox.SlicePitch, gcnew DataStream( lockedBox.pBits, lockedSize, true, !readOnly, false ) );
		return outBox;
	}

	Result Volume::UnlockBox()
	{
		HRESULT hr = InternalPointer->UnlockBox();
		return RECORD_D3D9( hr );
	}

	Result Volume::FromFileInMemory_Internal( Volume^ volume, const void* memory, UINT size, Filter filter, int colorKey,
		Box* sourceBox, Box* destinationBox, PaletteEntry* palette, ImageInformation* imageInformation )
	{
		HRESULT hr = D3DXLoadVolumeFromFileInMemory( volume->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( palette ),
			reinterpret_cast<const D3DBOX*>( destinationBox ), memory, size,
			reinterpret_cast<const D3DBOX*>( sourceBox ), static_cast<DWORD>( filter ),
			static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( imageInformation ) );

		return RECORD_D3D9( hr );
	}

	Result Volume::FromFileInMemory( Volume^ volume, array<Byte>^ memory, Filter filter, int colorKey, Box sourceBox,
		Box destinationBox, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromFileInMemory_Internal( volume, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&sourceBox, &destinationBox, pinnedPalette, pinnedImageInfo );
	}

	Result Volume::FromFileInMemory( Volume^ volume, array<Byte>^ memory, Filter filter, int colorKey, Box sourceBox,
		Box destinationBox, [Out] ImageInformation% imageInformation )
	{
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromFileInMemory_Internal( volume, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&sourceBox, &destinationBox, NULL, pinnedImageInfo );
	}

	Result Volume::FromFileInMemory( Volume^ volume, array<Byte>^ memory, Filter filter, int colorKey, Box sourceBox,
		Box destinationBox )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromFileInMemory_Internal( volume, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			&sourceBox, &destinationBox, NULL, NULL );
	}

	Result Volume::FromFileInMemory( Volume^ volume, array<Byte>^ memory, Filter filter, int colorKey )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];

		return FromFileInMemory_Internal( volume, pinnedMemory, static_cast<UINT>( memory->Length ), filter, colorKey,
			NULL, NULL, NULL, NULL );
	}

	Result Volume::FromFileInStream( Volume^ volume, Stream^ stream, Filter filter, int colorKey, Box sourceBox, 
		Box destinationBox, array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
			pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
			UINT size = static_cast<UINT>( ds->RemainingLength );

			return FromFileInMemory_Internal( volume, ds->SeekToEnd(), size, filter, colorKey,
				&sourceBox, &destinationBox, pinnedPalette, pinnedImageInfo );
		}

		return FromFileInMemory( volume, data, filter, colorKey, sourceBox, destinationBox,
			palette, imageInformation );
	}

	Result Volume::FromFileInStream( Volume^ volume, Stream^ stream, Filter filter, int colorKey, Box sourceBox,
		Box destinationBox, [Out] ImageInformation% imageInformation )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
			UINT size = static_cast<UINT>( ds->RemainingLength );

			return FromFileInMemory_Internal( volume, ds->SeekToEnd(), size, filter, colorKey,
				&sourceBox, &destinationBox, NULL, pinnedImageInfo );
		}

		return FromFileInMemory( volume, data, filter, colorKey, sourceBox, destinationBox,
			imageInformation );
	}

	Result Volume::FromFileInStream( Volume^ volume, Stream^ stream, Filter filter, int colorKey, Box sourceBox, 
		Box destinationBox )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );

			return FromFileInMemory_Internal( volume, ds->SeekToEnd(), size, filter, colorKey,
				&sourceBox, &destinationBox, NULL, NULL );
		}

		return FromFileInMemory( volume, data, filter, colorKey, sourceBox, destinationBox );
	}

	Result Volume::FromFileInStream( Volume^ volume, Stream^ stream, Filter filter, int colorKey )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );

			return FromFileInMemory_Internal( volume, ds->SeekToEnd(), size, filter, colorKey,
				NULL, NULL, NULL, NULL );
		}

		return FromFileInMemory( volume, data, filter, colorKey );
	}

	Result Volume::FromFile( Volume^ volume, String^ fileName, Filter filter, int colorKey, 
		Box sourceBox, Box destinationBox,
		array<PaletteEntry>^ palette, [Out] ImageInformation% imageInformation )
	{
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXLoadVolumeFromFile( volume->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ),
			reinterpret_cast<const D3DBOX*>( &destinationBox ), pinnedName,
			reinterpret_cast<const D3DBOX*>( &sourceBox ), static_cast<DWORD>( filter ),
			static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ) );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromFile( Volume^ volume, String^ fileName, Filter filter, int colorKey, 
		Box sourceBox, Box destinationBox,
		[Out] ImageInformation% imageInformation )
	{
		pin_ptr<ImageInformation> pinnedImageInfo = &imageInformation;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXLoadVolumeFromFile( volume->InternalPointer, NULL,
			reinterpret_cast<const D3DBOX*>( &destinationBox ), pinnedName,
			reinterpret_cast<const D3DBOX*>( &sourceBox ), static_cast<DWORD>( filter ),
			static_cast<D3DCOLOR>( colorKey ), reinterpret_cast<D3DXIMAGE_INFO*>( pinnedImageInfo ) );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromFile( Volume^ volume, String^ fileName, Filter filter, int colorKey, 
		Box sourceBox, Box destinationBox )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXLoadVolumeFromFile( volume->InternalPointer, NULL,
			reinterpret_cast<const D3DBOX*>( &destinationBox ), pinnedName,
			reinterpret_cast<const D3DBOX*>( &sourceBox ), static_cast<DWORD>( filter ),
			static_cast<D3DCOLOR>( colorKey ), NULL );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromFile( Volume^ volume, String^ fileName, Filter filter, int colorKey )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXLoadVolumeFromFile( volume->InternalPointer, NULL, NULL, pinnedName,
			NULL, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ), NULL );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey,
		Box sourceBox, Box destinationBox,
		array<PaletteEntry>^ destinationPalette, array<PaletteEntry>^ sourcePalette )
	{
		pin_ptr<PaletteEntry> pinnedSource = sourcePalette == nullptr ? nullptr : &sourcePalette[0];
		pin_ptr<PaletteEntry> pinnedDest = destinationPalette == nullptr ? nullptr : &destinationPalette[0];

		HRESULT hr = D3DXLoadVolumeFromVolume( destinationVolume->InternalPointer, 
			reinterpret_cast<const PALETTEENTRY*>( pinnedDest ), reinterpret_cast<const D3DBOX*>( &destinationBox ),
			sourceVolume->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedSource ), 
			reinterpret_cast<const D3DBOX*>( &sourceBox ), static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey,
		Box sourceBox, Box destinationBox )
	{
		HRESULT hr = D3DXLoadVolumeFromVolume( destinationVolume->InternalPointer, 
			NULL, reinterpret_cast<const D3DBOX*>( &destinationBox ), sourceVolume->InternalPointer, NULL, 
			reinterpret_cast<const D3DBOX*>( &sourceBox ), static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	Result Volume::FromVolume( Volume^ destinationVolume, Volume^ sourceVolume, Filter filter, int colorKey )
	{
		HRESULT hr = D3DXLoadVolumeFromVolume( destinationVolume->InternalPointer, NULL, NULL, 
			sourceVolume->InternalPointer, NULL, NULL, static_cast<DWORD>( filter ), static_cast<D3DCOLOR>( colorKey ) );
		return RECORD_D3D9( hr );
	}

	DataStream^ Volume::ToStream( Volume^ volume, ImageFileFormat format, Box box, array<PaletteEntry>^ palette )
	{
		ID3DXBuffer *result = NULL;
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];

		HRESULT hr = D3DXSaveVolumeToFileInMemory( &result, static_cast<D3DXIMAGE_FILEFORMAT>( format ),
			volume->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ), reinterpret_cast<const D3DBOX*>( &box ) );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ Volume::ToStream( Volume^ volume, ImageFileFormat format, Box box )
	{
		return ToStream( volume, format, box, nullptr );
	}

	DataStream^ Volume::ToStream( Volume^ volume, ImageFileFormat format )
	{
		ID3DXBuffer *result = NULL;

		HRESULT hr = D3DXSaveVolumeToFileInMemory( &result, static_cast<D3DXIMAGE_FILEFORMAT>( format ),
			volume->InternalPointer, NULL, NULL );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	Result Volume::ToFile( Volume^ volume, String^ fileName, ImageFileFormat format, Box box, array<PaletteEntry>^ palette )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		
		HRESULT hr = D3DXSaveVolumeToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			volume->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ),
			reinterpret_cast<const D3DBOX*>( &box ) );
		return RECORD_D3D9( hr );
	}

	Result Volume::ToFile( Volume^ volume, String^ fileName, ImageFileFormat format, Box box )
	{
		return ToFile( volume, fileName, format, box, nullptr );
	}

	Result Volume::ToFile( Volume^ volume, String^ fileName, ImageFileFormat format )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		
		HRESULT hr = D3DXSaveVolumeToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			volume->InternalPointer, NULL, NULL );
		return RECORD_D3D9( hr );
	}
}
}