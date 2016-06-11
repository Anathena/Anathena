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

#include <xaudio2.h>

#include "../Utilities.h"
#include "WaveFormatExtensible.h"

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Multimedia
{
	WaveFormatExtensible^ WaveFormatExtensible::FromBase( const WAVEFORMATEX *format )
	{
		if( format->cbSize < 22 )
			throw gcnew InvalidDataException( "Invalid WaveFormatExtensible format." );

		WaveFormatExtensible^ result = gcnew WaveFormatExtensible();

		result->FormatTag = static_cast<WaveFormatTag>( format->wFormatTag );
		result->Channels = format->nChannels;
		result->SamplesPerSecond = format->nSamplesPerSec;
		result->AverageBytesPerSecond = format->nAvgBytesPerSec;
		result->BlockAlignment = format->nBlockAlign;
		result->BitsPerSample = format->wBitsPerSample;
		result->Size = format->cbSize;

		const BYTE *ptr = ( reinterpret_cast<const BYTE*>( &format->cbSize ) + sizeof( WORD ) );
		array<Byte>^ bytes = gcnew array<Byte>( result->Size );
		for( int i = 0; i < result->Size; i++ )
		{
			bytes[i] = *ptr;
			ptr++;
		}

		result->unionData = BitConverter::ToInt16( bytes, 0 );
		result->ChannelMask = static_cast<Speakers>( BitConverter::ToInt32( bytes, 2 ) );

		array<Byte>^ guidBytes = gcnew array<Byte>( 16 );
		Array::Copy( bytes, 6, guidBytes, 0, 16 );

		result->SubFormat = Guid( guidBytes );

		return result;
	}

	WaveFormatExtensible::WaveFormatExtensible()
	{
		Size = 22;
	}

	WaveFormatExtensible::WaveFormatExtensible( const WAVEFORMATEXTENSIBLE &format )
	{
		FormatTag = static_cast<WaveFormatTag>( format.Format.wFormatTag );
		Channels = format.Format.nChannels;
		SamplesPerSecond = format.Format.nSamplesPerSec;
		AverageBytesPerSecond = format.Format.nAvgBytesPerSec;
		BlockAlignment = format.Format.nBlockAlign;
		BitsPerSample = format.Format.wBitsPerSample;
		ChannelMask = static_cast<Speakers>( format.dwChannelMask );
		unionData = format.Samples.wSamplesPerBlock;
		SubFormat = Utilities::ConvertNativeGuid( format.SubFormat );

		Size = 22;
	}

	WaveFormatExtensible^ WaveFormatExtensible::Clone()
	{
		WaveFormatExtensible^ result = gcnew WaveFormatExtensible();

		result->AverageBytesPerSecond = AverageBytesPerSecond;
		result->BitsPerSample = BitsPerSample;
		result->BlockAlignment = BlockAlignment;
		result->Channels = Channels;
		result->SamplesPerSecond = SamplesPerSecond;
		result->ValidBitsPerSample = ValidBitsPerSample;
		result->ChannelMask = ChannelMask;
		result->SubFormat = SubFormat;

		return result;
	}

	array<Byte>^ WaveFormatExtensible::GetBytes()
	{
		List<Byte>^ result = gcnew List<Byte>();

		//see WaveFormat.cpp
#if _MSC_VER < 1500
#define VS05_PATCH(val) Array::AsReadOnly( val )
#else
#define VS05_PATCH(val) val
#endif

		result->AddRange( VS05_PATCH( WaveFormat::GetBytes() ) );
		result->AddRange( VS05_PATCH( BitConverter::GetBytes( unionData ) ) );
		result->AddRange( VS05_PATCH( BitConverter::GetBytes( static_cast<int>( ChannelMask ) ) ) );
		result->AddRange( VS05_PATCH( SubFormat.ToByteArray() ) );

		return result->ToArray();
	}

	bool WaveFormatExtensible::operator == ( WaveFormatExtensible^ left, WaveFormatExtensible^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return WaveFormatExtensible::Equals( left, right );
	}

	bool WaveFormatExtensible::operator != ( WaveFormatExtensible^ left, WaveFormatExtensible^ right )
	{
		return !( left == right );
	}

	int WaveFormatExtensible::GetHashCode()
	{		
		return WaveFormat::GetHashCode() + unionData.GetHashCode() +
			ChannelMask.GetHashCode() + SubFormat.GetHashCode();
	}

	bool WaveFormatExtensible::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<WaveFormatExtensible^>( value ) );
	}

	bool WaveFormatExtensible::Equals( WaveFormatExtensible^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( WaveFormat::Equals( value ) &&
			unionData == value->unionData && ChannelMask == value->ChannelMask &&
			SubFormat == value->SubFormat );
	}

	bool WaveFormatExtensible::Equals( WaveFormatExtensible^ value1, WaveFormatExtensible^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}