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

#include "Direct3D9Exception.h"

#include "Device.h"
#include "BaseTexture.h"
#include "Texture.h"
#include "VolumeTexture.h"
#include "CubeTexture.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	BaseTexture^ BaseTexture::FromUnmanaged( IDirect3DBaseTexture9 *texture )
	{
		if( texture == NULL )
			return nullptr;

		switch( texture->GetType() )
		{
		case D3DRTYPE_TEXTURE:
			return Texture::FromPointer( static_cast<IDirect3DTexture9*>( texture ) );
		case D3DRTYPE_VOLUMETEXTURE:
			return VolumeTexture::FromPointer( static_cast<IDirect3DVolumeTexture9*>( texture ) );
		case D3DRTYPE_CUBETEXTURE:
			return CubeTexture::FromPointer( static_cast<IDirect3DCubeTexture9*>( texture ) );

		default:
			return nullptr;
		}
	}

	void BaseTexture::GenerateMipSublevels()
	{
		InternalPointer->GenerateMipSubLevels();
	}

	DataStream^ BaseTexture::ToStream( BaseTexture^ texture, ImageFileFormat format, array<PaletteEntry>^ palette )
	{
		ID3DXBuffer *buffer = NULL;
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		
		HRESULT hr = D3DXSaveTextureToFileInMemory( &buffer, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			texture->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ) );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( buffer );
	}

	DataStream^ BaseTexture::ToStream( BaseTexture^ texture, ImageFileFormat format )
	{
		ID3DXBuffer *buffer = NULL;
		
		HRESULT hr = D3DXSaveTextureToFileInMemory( &buffer, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			texture->InternalPointer, NULL );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( buffer );
	}

	Result BaseTexture::ToFile( BaseTexture^ texture, String^ fileName, ImageFileFormat format, array<PaletteEntry>^ palette )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];
		
		HRESULT hr = D3DXSaveTextureToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			texture->InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ) );
		return RECORD_D3D9( hr );
	}

	Result BaseTexture::ToFile( BaseTexture^ texture, String^ fileName, ImageFileFormat format )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(fileName);
		
		HRESULT hr = D3DXSaveTextureToFile( pinnedName, static_cast<D3DXIMAGE_FILEFORMAT>( format ), 
			texture->InternalPointer, NULL );
		return RECORD_D3D9( hr );
	}

	Result BaseTexture::FilterTexture( int sourceLevel, Filter filter, array<PaletteEntry>^ palette )
	{
		pin_ptr<PaletteEntry> pinnedPalette = palette == nullptr ? nullptr : &palette[0];

		HRESULT hr = D3DXFilterTexture( InternalPointer, reinterpret_cast<const PALETTEENTRY*>( pinnedPalette ),
			sourceLevel, static_cast<DWORD>( filter ) );
		return RECORD_D3D9( hr );
	}

	Result BaseTexture::FilterTexture( int sourceLevel, Filter filter )
	{
		HRESULT hr = D3DXFilterTexture( InternalPointer, NULL, sourceLevel, static_cast<DWORD>( filter ) );
		return RECORD_D3D9( hr );
	}
}
}