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

#include "../Utilities.h"
#include "../DataStream.h"

#include "Direct3D9Exception.h"
#include "ImageInformation.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Direct3D9
{
	bool ImageInformation::operator == ( ImageInformation left, ImageInformation right )
	{
		return ImageInformation::Equals( left, right );
	}

	bool ImageInformation::operator != ( ImageInformation left, ImageInformation right )
	{
		return !ImageInformation::Equals( left, right );
	}

	int ImageInformation::GetHashCode()
	{
		return Width.GetHashCode() + Height.GetHashCode() + Depth.GetHashCode()
			 + MipLevels.GetHashCode() + Format.GetHashCode() + ResourceType.GetHashCode()
			 + ImageFileFormat.GetHashCode();
	}

	bool ImageInformation::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ImageInformation>( value ) );
	}

	bool ImageInformation::Equals( ImageInformation value )
	{
		return ( Width == value.Width && Height == value.Height && Depth == value.Depth
			 && MipLevels == value.MipLevels && Format == value.Format && ResourceType == value.ResourceType
			 && ImageFileFormat == value.ImageFileFormat );
	}

	bool ImageInformation::Equals( ImageInformation% value1, ImageInformation% value2 )
	{
		return ( value1.Width == value2.Width && value1.Height == value2.Height && value1.Depth == value2.Depth
			 && value1.MipLevels == value2.MipLevels && value1.Format == value2.Format && value1.ResourceType == value2.ResourceType
			 && value1.ImageFileFormat == value2.ImageFileFormat );
	}

	ImageInformation ImageInformation::FromStream(Stream^ stream, bool peek)
	{
		array<Byte>^ buffer = nullptr;			// Buffer for the data.
		Int64 prevPosition = 0;				// Previous stream position.

		if (stream == nullptr)
			throw gcnew ArgumentNullException("stream");

		if (peek)
			prevPosition = stream->Position;

		// Create buffer.
		DataStream^ ds = nullptr;
		buffer = Utilities::ReadStream( stream, static_cast<int>( stream->Length ), &ds );

		if (peek)
			stream->Position = prevPosition;

		if( buffer == nullptr )
		{
			UINT size = static_cast<UINT>( ds->RemainingLength );
			return FromMemory_Internal( ds->SeekToEnd(), size );
		}

		// Extract from the byte buffer.
		return FromMemory(buffer);
	}

	ImageInformation ImageInformation::FromStream(Stream^ stream)
	{
		return FromStream(stream, true);
	}

	ImageInformation ImageInformation::FromFile( String^ fileName )
	{
		ImageInformation info;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXGetImageInfoFromFile( pinnedName, reinterpret_cast<D3DXIMAGE_INFO*>( &info ) );
		RECORD_D3D9( hr );

		return info;
	}

	ImageInformation ImageInformation::FromMemory_Internal( const void* data, UINT size )
	{
		ImageInformation info;

		HRESULT hr = D3DXGetImageInfoFromFileInMemory( data, size, reinterpret_cast<D3DXIMAGE_INFO*>( &info ) );
		RECORD_D3D9( hr );

		return info;
	}

	ImageInformation ImageInformation::FromMemory( array<Byte>^ memory )
	{
		pin_ptr<const unsigned char> pinnedMemory = &memory[0];
		return FromMemory_Internal( pinnedMemory, static_cast<UINT>( memory->Length ) );
	}
}
}
