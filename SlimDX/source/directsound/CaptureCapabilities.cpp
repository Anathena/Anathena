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

#include "CaptureCapabilities.h"

namespace SlimDX
{
namespace DirectSound
{
	CaptureCapabilities::CaptureCapabilities( const DSCCAPS &caps )
	{
		Channels = caps.dwChannels;

		Certified = ( caps.dwFlags & DSCCAPS_CERTIFIED ) != 0;
		EmulatedDriver = ( caps.dwFlags & DSCCAPS_EMULDRIVER ) != 0;
		MultipleCapture = ( caps.dwFlags & DSCCAPS_MULTIPLECAPTURE ) != 0;
		Format11KhzMono16Bit = ( caps.dwFormats & WAVE_FORMAT_1M16 ) != 0;
		Format11KhzMono8Bit = ( caps.dwFormats & WAVE_FORMAT_1M08 ) != 0;
		Format11KhzStereo16Bit = ( caps.dwFormats & WAVE_FORMAT_1S16 ) != 0;
		Format11KhzStereo8Bit = ( caps.dwFormats & WAVE_FORMAT_1S08 ) != 0;
		Format22KhzMono16Bit = ( caps.dwFormats & WAVE_FORMAT_2M16 ) != 0;
		Format22KhzMono8Bit = ( caps.dwFormats & WAVE_FORMAT_2M08 ) != 0;
		Format22KhzStereo16Bit = ( caps.dwFormats & WAVE_FORMAT_2S16 ) != 0;
		Format22KhzStereo8Bit = ( caps.dwFormats & WAVE_FORMAT_2S08 ) != 0;
		Format44KhzMono16Bit = ( caps.dwFormats & WAVE_FORMAT_4M16 ) != 0;
		Format44KhzMono8Bit = ( caps.dwFormats & WAVE_FORMAT_4M08 ) != 0;
		Format44KhzStereo16Bit = ( caps.dwFormats & WAVE_FORMAT_4S16 ) != 0;
		Format44KhzStereo8Bit = ( caps.dwFormats & WAVE_FORMAT_4S08 ) != 0;
		Format48KhzMono16Bit = ( caps.dwFormats & WAVE_FORMAT_48M16 ) != 0;
		Format48KhzMono8Bit = ( caps.dwFormats & WAVE_FORMAT_48M08 ) != 0;
		Format48KhzStereo16Bit = ( caps.dwFormats & WAVE_FORMAT_48S16 ) != 0;
		Format48KhzStereo8Bit = ( caps.dwFormats & WAVE_FORMAT_48S08 ) != 0;
		Format96KhzMono16Bit = ( caps.dwFormats & WAVE_FORMAT_96M16 ) != 0;
		Format96KhzMono8Bit = ( caps.dwFormats & WAVE_FORMAT_96M08 ) != 0;
		Format96KhzStereo16Bit = ( caps.dwFormats & WAVE_FORMAT_96S16 ) != 0;
		Format96KhzStereo8Bit = ( caps.dwFormats & WAVE_FORMAT_96S08 ) != 0;
	}

	bool CaptureCapabilities::operator == ( CaptureCapabilities left, CaptureCapabilities right )
	{
		return CaptureCapabilities::Equals( left, right );
	}

	bool CaptureCapabilities::operator != ( CaptureCapabilities left, CaptureCapabilities right )
	{
		return !CaptureCapabilities::Equals( left, right );
	}

	int CaptureCapabilities::GetHashCode()
	{
		return MultipleCapture.GetHashCode() + Format96KhzStereo8Bit.GetHashCode() + Format96KhzStereo16Bit.GetHashCode() + Format96KhzMono8Bit.GetHashCode() + 
			Format96KhzMono16Bit.GetHashCode() + Format48KhzStereo8Bit.GetHashCode() + Format48KhzStereo16Bit.GetHashCode() + Format48KhzMono8Bit.GetHashCode() + 
			Format48KhzMono16Bit.GetHashCode() + Format44KhzStereo8Bit.GetHashCode() + Format44KhzStereo16Bit.GetHashCode() + Format44KhzMono8Bit.GetHashCode() + 
			Format44KhzMono16Bit.GetHashCode() + Format22KhzStereo8Bit.GetHashCode() + Format22KhzStereo16Bit.GetHashCode() + Format22KhzMono8Bit.GetHashCode() + 
			Format22KhzMono16Bit.GetHashCode() + Format11KhzStereo8Bit.GetHashCode() + Format11KhzStereo16Bit.GetHashCode() + Format11KhzMono8Bit.GetHashCode() + 
			Format11KhzMono16Bit.GetHashCode() + EmulatedDriver.GetHashCode() + Channels.GetHashCode() + Certified.GetHashCode();
	}

	bool CaptureCapabilities::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CaptureCapabilities>( value ) );
	}

	bool CaptureCapabilities::Equals( CaptureCapabilities value )
	{
		return ( MultipleCapture == value.MultipleCapture && Format96KhzStereo8Bit == value.Format96KhzStereo8Bit && Format96KhzStereo16Bit == value.Format96KhzStereo16Bit 
			&& Format96KhzMono8Bit == value.Format96KhzMono8Bit && Format96KhzMono16Bit == value.Format96KhzMono16Bit && Format48KhzStereo8Bit == value.Format48KhzStereo8Bit && 
			Format48KhzStereo16Bit == value.Format48KhzStereo16Bit && Format48KhzMono8Bit == value.Format48KhzMono8Bit && Format48KhzMono16Bit == value.Format48KhzMono16Bit && 
			Format44KhzStereo8Bit == value.Format44KhzStereo8Bit && Format44KhzStereo16Bit == value.Format44KhzStereo16Bit && Format44KhzMono8Bit == value.Format44KhzMono8Bit && 
			Format44KhzMono16Bit == value.Format44KhzMono16Bit && Format22KhzStereo8Bit == value.Format22KhzStereo8Bit && Format22KhzStereo16Bit == value.Format22KhzStereo16Bit && 
			Format22KhzMono8Bit == value.Format22KhzMono8Bit && Format22KhzMono16Bit == value.Format22KhzMono16Bit && Format11KhzStereo8Bit == value.Format11KhzStereo8Bit && 
			Format11KhzStereo16Bit == value.Format11KhzStereo16Bit && Format11KhzMono8Bit == value.Format11KhzMono8Bit && Format11KhzMono16Bit == value.Format11KhzMono16Bit && 
			EmulatedDriver == value.EmulatedDriver && Channels == value.Channels && Certified == value.Certified );
	}

	bool CaptureCapabilities::Equals( CaptureCapabilities% value1, CaptureCapabilities% value2 )
	{
		return value1.Equals( value2 );
	}
}
}