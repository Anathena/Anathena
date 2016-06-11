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

#include "VoiceState.h"

namespace SlimDX
{
namespace XAudio2
{
	bool VoiceState::operator == ( VoiceState left, VoiceState right )
	{
		return VoiceState::Equals( left, right );
	}

	bool VoiceState::operator != ( VoiceState left, VoiceState right )
	{
		return !VoiceState::Equals( left, right );
	}

	int VoiceState::GetHashCode()
	{
		return Context.GetHashCode() + BuffersQueued.GetHashCode() + SamplesPlayed.GetHashCode();
	}

	bool VoiceState::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<VoiceState>( value ) );
	}

	bool VoiceState::Equals( VoiceState value )
	{
		return ( Context == value.Context && BuffersQueued == value.BuffersQueued && 
			SamplesPlayed == value.SamplesPlayed );
	}

	bool VoiceState::Equals( VoiceState% value1, VoiceState% value2 )
	{
		return ( value1.Context == value2.Context && value1.BuffersQueued == value2.BuffersQueued && 
			value1.SamplesPlayed == value2.SamplesPlayed );
	}
}
}