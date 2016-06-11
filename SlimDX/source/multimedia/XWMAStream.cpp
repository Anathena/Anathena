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

#include "../InternalHelpers.h"
#include "../Utilities.h"
#include "../DataStream.h"

#include "XWMAStream.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Multimedia
{
	XWMAStream::XWMAStream( String^ path )
	{
		if( String::IsNullOrEmpty( path ) )
			throw gcnew ArgumentNullException( "path" );

		if( !File::Exists( path ) )
			throw gcnew FileNotFoundException( "Could not find xWMA file", path );

		pin_ptr<const wchar_t> pinnedPath = PtrToStringChars( path );
		handle = mmioOpen( const_cast<LPWSTR>( reinterpret_cast<LPCWSTR>( pinnedPath ) ), NULL, MMIO_ALLOCBUF | MMIO_READ );
		if( handle == NULL )
			throw gcnew FileLoadException( "Could not open the file", path );

		Init();
	}

	XWMAStream::XWMAStream( Stream^ stream )
	{
		InitStream( stream, 0 );
	}

	XWMAStream::XWMAStream( Stream^ stream, int length )
	{
		InitStream( stream, length );
	}

	void XWMAStream::InitStream( Stream^ stream, int length )
	{
		if( stream == nullptr )
			throw gcnew ArgumentNullException( "stream" );

		DataStream^ ds = nullptr;
		array<Byte>^ bytes = Utilities::ReadStream( stream, length, &ds );

		if( bytes == nullptr )
		{
			Int64 size = ds->RemainingLength;
			internalMemory = gcnew DataStream( size, true, true );
			internalMemory->WriteRange( IntPtr( ds->SeekToEnd() ), size );
		}
		else
		{
			internalMemory = gcnew DataStream( bytes->LongLength, true, true );
			internalMemory->Write( bytes, 0, bytes->Length );
		}
		internalMemory->Position = 0;

		MMIOINFO info;
		ZeroMemory( &info, sizeof( info ) );
		info.fccIOProc = FOURCC_MEM;
		info.cchBuffer = bytes->Length;
		info.pchBuffer = internalMemory->PositionPointer;

		handle = mmioOpen( NULL, &info, MMIO_READ );
		if( handle == NULL )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		Init();
	}

	void XWMAStream::Init()
	{
		MMCKINFO fileInfo;
		MMCKINFO chunk;
		PCMWAVEFORMAT pcmFormat;		

		if( mmioDescend( handle, &fileInfo, NULL, 0 ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		if( fileInfo.ckid != FOURCC_RIFF ||
			fileInfo.fccType != mmioFOURCC( 'X', 'W', 'M', 'A' ) )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		chunk.ckid = mmioFOURCC( 'f', 'm', 't', ' ' );
		if( mmioDescend( handle, &chunk, &fileInfo, MMIO_FINDCHUNK ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		if( chunk.cksize < static_cast<LONG>( sizeof( PCMWAVEFORMAT ) ) )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		if( mmioRead( handle, reinterpret_cast<HPSTR>( &pcmFormat ), sizeof( pcmFormat ) ) != sizeof( pcmFormat ) )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		if( (pcmFormat.wf.wFormatTag != WAVE_FORMAT_WMAUDIO2) &&
			(pcmFormat.wf.wFormatTag != WAVE_FORMAT_WMAUDIO3))
			throw gcnew InvalidDataException( "Invalid xWMA file." );
		
		if(pcmFormat.wf.nChannels <= 2)
		{
			auto_array<WAVEFORMATEX> tempFormat( reinterpret_cast<WAVEFORMATEX*>( new BYTE[sizeof( WAVEFORMATEX )] ) );
			memcpy( tempFormat.get(), &pcmFormat, sizeof( pcmFormat ) );
			tempFormat->cbSize = 0;

			format = WaveFormat::FromUnmanaged( *tempFormat.get() );
		}
		else
		{
			WORD extraBytes = 0;
			if( mmioRead( handle, reinterpret_cast<CHAR*>( &extraBytes ), sizeof( WORD ) ) != sizeof( WORD ) )
				throw gcnew InvalidDataException( "Invalid xWMA file." );

			auto_array<WAVEFORMATEX> tempFormat( reinterpret_cast<WAVEFORMATEX*>( new BYTE[sizeof( WAVEFORMATEX ) + extraBytes] ) );
			memcpy( tempFormat.get(), &pcmFormat, sizeof( pcmFormat ) );
			tempFormat->cbSize = extraBytes;

			if( mmioRead( handle, reinterpret_cast<CHAR*>( reinterpret_cast<BYTE*>( &tempFormat->cbSize ) + sizeof( WORD ) ), extraBytes ) != extraBytes )
				throw gcnew InvalidDataException( "Invalid xWMA file." );

			format = WaveFormatExtensible::FromBase( tempFormat.get() );
		}

		if( mmioAscend( handle, &chunk, 0 ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		chunk.ckid = mmioFOURCC( 'd', 'p', 'd', 's' );
		if( mmioDescend( handle, &chunk, &fileInfo, MMIO_FINDCHUNK ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );
		
		int numpackets = chunk.cksize / sizeof(int);
		decodedpacketsinfo = gcnew array<int>(numpackets);
		pin_ptr<int> pinnedData = &decodedpacketsinfo[0];
		if( mmioRead( handle, reinterpret_cast<CHAR*>(pinnedData), chunk.cksize ) != (LONG)chunk.cksize )
			throw gcnew InvalidDataException( "Invalid xWMA file." );
		
		if( mmioAscend( handle, &chunk, 0 ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );
		
		chunk.ckid = mmioFOURCC( 'd', 'a', 't', 'a' );
		if( mmioDescend( handle, &chunk, &fileInfo, MMIO_FINDCHUNK ) != 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );
		
		dataOffset = mmioSeek( handle, 0, SEEK_CUR );
		size = chunk.cksize;

		if( dataOffset < 0 || size <= 0 )
			throw gcnew InvalidDataException( "Invalid xWMA file." );

		if (internalMemory != nullptr)
		    publicMemory = gcnew DataStream(internalMemory->RawPointer + dataOffset, internalMemory->Length - dataOffset, true, false, false);
	}

	XWMAStream::~XWMAStream()
	{
		Destruct();
		GC::SuppressFinalize( this );
	}
	
	XWMAStream::!XWMAStream()
	{
		Destruct();
	}

	void XWMAStream::Destruct()
	{
		if( handle != NULL )
		{
			mmioClose( handle, 0 );
			handle = NULL;
		}

		if( internalMemory != nullptr )
		{
			delete internalMemory;
			internalMemory = nullptr;
		}

		if( publicMemory != nullptr )
		{
			delete publicMemory;
			publicMemory = nullptr;
		}
	}

	Int64 XWMAStream::Seek( Int64 offset, SeekOrigin origin )
	{
		int targetPosition = static_cast<int>( offset );
		int targetOrigin = SEEK_CUR;

		if( origin == SeekOrigin::Begin )
		{
			targetOrigin = SEEK_SET;
			targetPosition += dataOffset;
		}
		else if( origin == SeekOrigin::End )
		{
			targetOrigin = SEEK_END;
			targetPosition = dataOffset;
		}

		int result = mmioSeek( handle, targetPosition, targetOrigin );
		if( result < 0 )
			throw gcnew InvalidOperationException("Cannot seek beyond the end of the stream.");

		return result - dataOffset;
	}

	void XWMAStream::Write( array<Byte>^ buffer, int offset, int count )
	{
		SLIMDX_UNREFERENCED_PARAMETER(buffer);
		SLIMDX_UNREFERENCED_PARAMETER(offset);
		SLIMDX_UNREFERENCED_PARAMETER(count);

		throw gcnew NotSupportedException("XWMAStream objects cannot be written to.");
	}

	int XWMAStream::Read( array<Byte>^ buffer, int offset, int count )
	{		
		Utilities::CheckArrayBounds( buffer, offset, count );

		// truncate the count to the end of the stream
		size_t actualCount = min( static_cast<size_t>( Length - Position ), static_cast<size_t>( count ) );

		pin_ptr<Byte> pinnedBuffer = &buffer[offset];
		int read = mmioRead( handle, reinterpret_cast<HPSTR>( pinnedBuffer ), static_cast<LONG>( actualCount ) );

		return read;
	}

	void XWMAStream::Flush()
	{
		throw gcnew NotSupportedException("XWMAStream objects cannot be flushed.");
	}

	void XWMAStream::SetLength( Int64 value )
	{
		SLIMDX_UNREFERENCED_PARAMETER(value);
		throw gcnew NotSupportedException("XWMAStream objects cannot be resized.");
	}

	Int64 XWMAStream::Position::get()
	{
		return mmioSeek( handle, 0, SEEK_CUR ) - dataOffset;
	}

	void XWMAStream::Position::set( System::Int64 value )
	{
		Seek( value, System::IO::SeekOrigin::Begin );
	}
	
	Int64 XWMAStream::Length::get()
	{
		return size;
	}
}
}