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
#include <xaudio2.h>

#include "VoiceSendDescriptor.h"
#if SLIMDX_XAUDIO2_VERSION >= 24 

#include "Voice.h"

using namespace System;

namespace SlimDX
{
namespace XAudio2
{
	VoiceSendDescriptor::VoiceSendDescriptor( Voice^ voice, VoiceSendFlags flags )
	{
		if( voice == nullptr )
			throw gcnew ArgumentNullException( "voice" );

		OutputVoice = voice;
		Flags = flags;
	}

	XAUDIO2_SEND_DESCRIPTOR VoiceSendDescriptor::CreateNativeVersion()
	{
		XAUDIO2_SEND_DESCRIPTOR native;
		native.Flags = static_cast<UINT32>( Flags );
		native.pOutputVoice = OutputVoice == nullptr ? 0 : OutputVoice->InternalPointer;
		
		return native;
	}

	bool VoiceSendDescriptor::operator == ( VoiceSendDescriptor left, VoiceSendDescriptor right )
	{
		return VoiceSendDescriptor::Equals( left, right );
	}

	bool VoiceSendDescriptor::operator != ( VoiceSendDescriptor left, VoiceSendDescriptor right )
	{
		return !VoiceSendDescriptor::Equals( left, right );
	}

	int VoiceSendDescriptor::GetHashCode()
	{
		return OutputVoice->GetHashCode() + Flags.GetHashCode();
	}

	bool VoiceSendDescriptor::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<VoiceSendDescriptor>( value ) );
	}

	bool VoiceSendDescriptor::Equals( VoiceSendDescriptor value )
	{
		return ( OutputVoice == value.OutputVoice && Flags == value.Flags );
	}

	bool VoiceSendDescriptor::Equals( VoiceSendDescriptor% value1, VoiceSendDescriptor% value2 )
	{
		return ( value1.OutputVoice == value2.OutputVoice && value1.Flags == value2.Flags );
	}
}
}

#endif