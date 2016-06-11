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
#pragma once

#include "AnimationSet.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// An application uses the methods of this interface to implement a key frame
		/// animation set stored in a compressed data format.
		/// </summary>
		/// <unmanaged>ID3DXCompressedAnimationSet</unmanaged>
		public ref class CompressedAnimationSet : public AnimationSet
		{
			COMOBJECT(ID3DXCompressedAnimationSet, CompressedAnimationSet);

		public:
			CompressedAnimationSet( System::String^ name, double ticksPerSecond, PlaybackType playbackType, DataStream^ compressedData, array<CallbackKey>^ callbackKeys );
			virtual ~CompressedAnimationSet() { }

			array<CallbackKey>^ GetCallbackKeys();
			DataStream^ GetCompressedData();

			property int CallbackKeyCount { int get(); }
			property PlaybackType PlaybackType { SlimDX::Direct3D9::PlaybackType get(); }
			property double SourceTicksPerSecond { double get(); }
		};
	}
}