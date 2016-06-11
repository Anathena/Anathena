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

#include "WaveStream.h"
#include "AdpcmWaveFormat.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Multimedia
{
	WaveStream::WaveStream( String^ path )
	{
		if( String::IsNullOrEmpty( path ) )
			throw gcnew ArgumentNullException( "path" );

		if( !File::Exists( path ) )
			throw gcnew FileNotFoundException( "Could not find wave file", path );

		pin_ptr<const wchar_t> pinnedPath = PtrToStringChars( path );
		handle = mmioOpen( const_cast<LPWSTR>( reinterpret_cast<LPCWSTR>( pinnedPath ) ), NULL, MMIO_ALLOCBUF | MMIO_READ );
		if( handle == NULL )
			throw gcnew FileLoadException( "Could not open the file", path );

		Init();
	}

	WaveStream::WaveStream( Stream^ stream )
	{
		InitStream( stream, 0 );
	}

	WaveStream::WaveStream( Stream^ stream, int length )
	{
		InitStream( stream, length );
	}

	void WaveStream::InitStream( Stream^ stream, int length )
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
			throw gcnew InvalidDataException( "Invalid wave file." );

		Init();
	}

	void WaveStream::Init()
	{
		MMCKINFO fileInfo;
		MMCKINFO chunk;
		PCMWAVEFORMAT pcmFormat;		

		if( mmioDescend( handle, &fileInfo, NULL, 0 ) != 0 )
			throw gcnew InvalidDataException( "Invalid wave file." );

		if( fileInfo.ckid != FOURCC_RIFF ||
			fileInfo.fccType != mmioFOURCC( 'W', 'A', 'V', 'E' ) )
			throw gcnew InvalidDataException( "Invalid wave file." );

		chunk.ckid = mmioFOURCC( 'f', 'm', 't', ' ' );
		if( mmioDescend( handle, &chunk, &fileInfo, MMIO_FINDCHUNK ) != 0 )
			throw gcnew InvalidDataException( "Invalid wave file." );

		if( chunk.cksize < static_cast<LONG>( sizeof( PCMWAVEFORMAT ) ) )
			throw gcnew InvalidDataException( "Invalid wave file." );

		if( mmioRead( handle, reinterpret_cast<HPSTR>( &pcmFormat ), sizeof( pcmFormat ) ) != sizeof( pcmFormat ) )
			throw gcnew InvalidDataException( "Invalid wave file." );

		switch (pcmFormat.wf.wFormatTag)
		{
		case WAVE_FORMAT_PCM:
		case WAVE_FORMAT_IEEE_FLOAT:
			{
				auto_array<WAVEFORMATEX> tempFormat( reinterpret_cast<WAVEFORMATEX*>( new BYTE[sizeof( WAVEFORMATEX )] ) );
				memcpy( tempFormat.get(), &pcmFormat, sizeof( pcmFormat ) );
				tempFormat->cbSize = 0;

				format = WaveFormat::FromUnmanaged( *tempFormat.get() );
				break;
			}

		case WAVE_FORMAT_EXTENSIBLE:
			{
				WORD extraBytes = 0;
				if( mmioRead( handle, reinterpret_cast<CHAR*>( &extraBytes ), sizeof( WORD ) ) != sizeof( WORD ) )
					throw gcnew InvalidDataException( "Invalid wave file." );

				auto_array<WAVEFORMATEX> tempFormat( reinterpret_cast<WAVEFORMATEX*>( new BYTE[sizeof( WAVEFORMATEX ) + extraBytes] ) );
				memcpy( tempFormat.get(), &pcmFormat, sizeof( pcmFormat ) );
				tempFormat->cbSize = extraBytes;

				if( mmioRead( handle, reinterpret_cast<CHAR*>( reinterpret_cast<BYTE*>( &tempFormat->cbSize ) + sizeof( WORD ) ), extraBytes ) != extraBytes )
					throw gcnew InvalidDataException( "Invalid wave file." );

				format = WaveFormatExtensible::FromBase( tempFormat.get() );
				break;
			}

		case WAVE_FORMAT_ADPCM:
			{
				WORD extraBytes = 0;
				if( mmioRead( handle, reinterpret_cast<CHAR*>( &extraBytes ), sizeof( WORD ) ) != sizeof( WORD ) )
					throw gcnew InvalidDataException( "Invalid wave file." );

				auto_array<WAVEFORMATEX> tempFormat( reinterpret_cast<WAVEFORMATEX*>( new BYTE[sizeof( WAVEFORMATEX ) + extraBytes] ) );
				memcpy( tempFormat.get(), &pcmFormat, sizeof( pcmFormat ) );
				tempFormat->cbSize = extraBytes;

				if( mmioRead( handle, reinterpret_cast<CHAR*>( reinterpret_cast<BYTE*>( &tempFormat->cbSize ) + sizeof( WORD ) ), extraBytes ) != extraBytes )
					throw gcnew InvalidDataException( "Invalid wave file." );

				format = AdpcmWaveFormat::FromBase( tempFormat.get() );
				break;
			}

		case WAVE_FORMAT_WMAUDIO2:
		case WAVE_FORMAT_WMAUDIO3:
			throw gcnew InvalidDataException("WaveStream does not support xWMA streams. Use the XWMAStream instead for this format.");

		default:
			throw gcnew InvalidDataException("Unknown or unsupported wave format.");
		}

		if( mmioAscend( handle, &chunk, 0 ) != 0 )
			throw gcnew InvalidDataException( "Invalid wave file." );

		chunk.ckid = mmioFOURCC( 'd', 'a', 't', 'a' );
		if( mmioDescend( handle, &chunk, &fileInfo, MMIO_FINDCHUNK ) != 0 )
			throw gcnew InvalidDataException( "Invalid wave file." );

		dataOffset = mmioSeek( handle, 0, SEEK_CUR );
		size = chunk.cksize;

		if( dataOffset < 0 || size <= 0 )
			throw gcnew InvalidDataException( "Invalid wave file." );

		if (internalMemory != nullptr)
		    publicMemory = gcnew DataStream(internalMemory->RawPointer + dataOffset, internalMemory->Length - dataOffset, true, false, false);
	}

	WaveStream::~WaveStream()
	{
		Destruct();
		GC::SuppressFinalize( this );
	}
	
	WaveStream::!WaveStream()
	{
		Destruct();
	}

	void WaveStream::Destruct()
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

	Int64 WaveStream::Seek( Int64 offset, SeekOrigin origin )
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

	void WaveStream::Write( array<Byte>^ buffer, int offset, int count )
	{
		SLIMDX_UNREFERENCED_PARAMETER(buffer);
		SLIMDX_UNREFERENCED_PARAMETER(offset);
		SLIMDX_UNREFERENCED_PARAMETER(count);

		throw gcnew NotSupportedException("WaveStream objects cannot be written to.");
	}

	int WaveStream::Read( array<Byte>^ buffer, int offset, int count )
	{		
		Utilities::CheckArrayBounds( buffer, offset, count );

		// truncate the count to the end of the stream
		size_t actualCount = min( static_cast<size_t>( Length - Position ), static_cast<size_t>( count ) );

		pin_ptr<Byte> pinnedBuffer = &buffer[offset];
		int read = mmioRead( handle, reinterpret_cast<HPSTR>( pinnedBuffer ), static_cast<LONG>( actualCount ) );

		return read;
	}

	void WaveStream::Flush()
	{
		throw gcnew NotSupportedException("WaveStream objects cannot be flushed.");
	}

	void WaveStream::SetLength( Int64 value )
	{
		SLIMDX_UNREFERENCED_PARAMETER(value);
		throw gcnew NotSupportedException("WaveStream objects cannot be resized.");
	}

	Int64 WaveStream::Position::get()
	{
		return mmioSeek( handle, 0, SEEK_CUR ) - dataOffset;
	}

	void WaveStream::Position::set( System::Int64 value )
	{
		Seek( value, System::IO::SeekOrigin::Begin );
	}
	
	Int64 WaveStream::Length::get()
	{
		return size;
	}
}
}