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

#include "../Utilities.h"

#include "AdpcmWaveFormat.h"

using namespace System;
using namespace System::IO;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Multimedia
{
	AdpcmWaveFormat^ AdpcmWaveFormat::FromBase( const WAVEFORMATEX *format )
	{
		AdpcmWaveFormat^ result = gcnew AdpcmWaveFormat();

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

		result->SamplesPerBlock = BitConverter::ToInt16(bytes, 0);
		short count = BitConverter::ToInt16(bytes, 2);

		result->Coefficients = gcnew array<int>(count);
		for (int i = 0; i < count; i++)
			result->Coefficients[i] = BitConverter::ToInt32(bytes, 4 + (i * 4));

		return result;
	}

	AdpcmWaveFormat::AdpcmWaveFormat()
	{
	}

	AdpcmWaveFormat^ AdpcmWaveFormat::Clone()
	{
		AdpcmWaveFormat^ result = gcnew AdpcmWaveFormat();

		result->AverageBytesPerSecond = AverageBytesPerSecond;
		result->BitsPerSample = BitsPerSample;
		result->BlockAlignment = BlockAlignment;
		result->Channels = Channels;
		result->SamplesPerSecond = SamplesPerSecond;
		result->SamplesPerBlock = SamplesPerBlock;
		result->Coefficients = safe_cast<array<int>^>(Coefficients->Clone());

		return result;
	}

	array<Byte>^ AdpcmWaveFormat::GetBytes()
	{
		List<Byte>^ result = gcnew List<Byte>();

		//see WaveFormat.cpp
#if _MSC_VER < 1500
#define VS05_PATCH(val) Array::AsReadOnly( val )
#else
#define VS05_PATCH(val) val
#endif

		result->AddRange( VS05_PATCH( WaveFormat::GetBytes() ) );
		result->AddRange( VS05_PATCH( BitConverter::GetBytes( SamplesPerBlock ) ) );
		result->AddRange( VS05_PATCH( BitConverter::GetBytes( static_cast<short>( Coefficients->Length ) ) ) );

		for (int i = 0; i < Coefficients->Length; i++)
			result->AddRange(VS05_PATCH(BitConverter::GetBytes(Coefficients[i])));

		return result->ToArray();
	}

	bool AdpcmWaveFormat::operator == ( AdpcmWaveFormat^ left, AdpcmWaveFormat^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return AdpcmWaveFormat::Equals( left, right );
	}

	bool AdpcmWaveFormat::operator != ( AdpcmWaveFormat^ left, AdpcmWaveFormat^ right )
	{
		return !( left == right );
	}

	int AdpcmWaveFormat::GetHashCode()
	{		
		return WaveFormat::GetHashCode() + SamplesPerBlock.GetHashCode();
	}

	bool AdpcmWaveFormat::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<AdpcmWaveFormat^>( value ) );
	}

	bool AdpcmWaveFormat::Equals( AdpcmWaveFormat^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( WaveFormat::Equals( value ) && SamplesPerBlock == value->SamplesPerBlock );
	}

	bool AdpcmWaveFormat::Equals( AdpcmWaveFormat^ value1, AdpcmWaveFormat^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}