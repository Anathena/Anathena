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

#include "VoiceDetails.h"

namespace SlimDX
{
namespace XAudio2
{
	bool VoiceDetails::operator == ( VoiceDetails left, VoiceDetails right )
	{
		return VoiceDetails::Equals( left, right );
	}

	bool VoiceDetails::operator != ( VoiceDetails left, VoiceDetails right )
	{
		return !VoiceDetails::Equals( left, right );
	}

	int VoiceDetails::GetHashCode()
	{
		return CreationFlags.GetHashCode() + InputChannels.GetHashCode() + InputSampleRate.GetHashCode();
	}

	bool VoiceDetails::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<VoiceDetails>( value ) );
	}

	bool VoiceDetails::Equals( VoiceDetails value )
	{
		return ( CreationFlags == value.CreationFlags && InputChannels == value.InputChannels && InputSampleRate == value.InputSampleRate );
	}

	bool VoiceDetails::Equals( VoiceDetails% value1, VoiceDetails% value2 )
	{
		return ( value1.CreationFlags == value2.CreationFlags && value1.InputChannels == value2.InputChannels && value1.InputSampleRate == value2.InputSampleRate );
	}
}
}