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
#include <dsound.h>

#include "CaptureBufferDescription.h"

using namespace SlimDX::Multimedia;

namespace SlimDX
{
namespace DirectSound
{
	DSCBUFFERDESC CaptureBufferDescription::ToUnmanaged()
	{
		DSCBUFFERDESC description;
		ZeroMemory( &description, sizeof(description) );
		description.dwSize = sizeof( DSCBUFFERDESC );
		description.dwBufferBytes = BufferBytes;
		description.dwFXCount = 0;
		description.dwFlags = 0;
		description.dwReserved = 0;
		description.lpwfxFormat = NULL;
		description.lpDSCFXDesc = NULL;

		if( ControlEffects && ( EffectDescriptions != nullptr ) )
		{
			// Manual Allocation: ugly, but necessary
			// we are only called by one other method, which performs the clean up
			description.dwFXCount = EffectDescriptions->Length;
			LPDSCEFFECTDESC lpDSCFXDesc = new DSCEFFECTDESC[ description.dwFXCount ];

			for( DWORD i = 0; i < description.dwFXCount; i++ )
				lpDSCFXDesc[i] = EffectDescriptions[i].Marshal();

			description.lpDSCFXDesc = lpDSCFXDesc;
		}

		if( ControlEffects && WaveMapped )
			description.dwFlags = DSCBCAPS_CTRLFX | DSCBCAPS_WAVEMAPPED;
		else if( ControlEffects )
			description.dwFlags = DSCBCAPS_CTRLFX;
		else if( WaveMapped )
			description.dwFlags = DSCBCAPS_WAVEMAPPED;

		if( Format != nullptr )
		{
			// Manual Allocation: ugly, but necessary
			// we are only called by one other method, which performs the clean up
			auto_array<WAVEFORMATEX> format = WaveFormat::ToUnmanaged( Format );
			description.lpwfxFormat = format.release();
		}

		return description;
	}

	bool CaptureBufferDescription::operator == ( CaptureBufferDescription left, CaptureBufferDescription right )
	{
		return CaptureBufferDescription::Equals( left, right );
	}

	bool CaptureBufferDescription::operator != ( CaptureBufferDescription left, CaptureBufferDescription right )
	{
		return !CaptureBufferDescription::Equals( left, right );
	}

	int CaptureBufferDescription::GetHashCode()
	{
		return EffectDescriptions->GetHashCode() + Format->GetHashCode() + WaveMapped.GetHashCode() + ControlEffects.GetHashCode() + BufferBytes.GetHashCode();
	}

	bool CaptureBufferDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CaptureBufferDescription>( value ) );
	}

	bool CaptureBufferDescription::Equals( CaptureBufferDescription value )
	{
		return ( EffectDescriptions == value.EffectDescriptions && Format == value.Format && WaveMapped == value.WaveMapped && 
			ControlEffects == value.ControlEffects && BufferBytes == value.BufferBytes );
	}

	bool CaptureBufferDescription::Equals( CaptureBufferDescription% value1, CaptureBufferDescription% value2 )
	{
		return value1.Equals( value2 );
	}
}
}