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

#include "../VersionConfig.h"
#include "../Utilities.h"
#include "../DataStream.h"

#include "AudioBuffer.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace XAudio2
{
	XAUDIO2_BUFFER AudioBuffer::ToUnmanaged()
	{
		XAUDIO2_BUFFER result;

		result.Flags = static_cast<UINT32>( Flags );
		result.AudioBytes = AudioBytes;
		result.PlayBegin = PlayBegin;
		result.PlayLength = PlayLength;
		result.LoopBegin = LoopBegin;
		result.LoopLength = LoopLength;
		result.LoopCount = LoopCount;
		result.pContext = Context.ToPointer();

		DataStream^ dataStream;
		array<Byte>^ bytes = Utilities::ReadStream( AudioData, AudioBytes, &dataStream );

		if( bytes == nullptr )
			result.pAudioData = reinterpret_cast<BYTE*>( dataStream->PositionPointer );
		else
		{
			Destruct();
			handle = GCHandle::Alloc( bytes, GCHandleType::Pinned );

#if SLIMDX_XAUDIO2_VERSION < 23
			result.pAudioData = reinterpret_cast<const BYTE*>( handle.AddrOfPinnedObject().ToPointer() );
#else
			result.pAudioData = reinterpret_cast<BYTE*>( handle.AddrOfPinnedObject().ToPointer() );
#endif
		}

		return result;
	}

	void AudioBuffer::Destruct()
	{
		if( handle.IsAllocated )
			handle.Free();
	}
}
}