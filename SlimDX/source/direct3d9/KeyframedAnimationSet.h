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
		/// An application uses the methods of this interface to implement a key frame animation set.
		/// </summary>
		/// <unmanaged>ID3DXKeyframedAnimationSet</unmanaged>
		public ref class KeyframedAnimationSet : public AnimationSet
		{
			COMOBJECT(ID3DXKeyframedAnimationSet, KeyframedAnimationSet);

		public:
			KeyframedAnimationSet( System::String^ name, double ticksPerSecond, PlaybackType playbackType, int animationCount, array<CallbackKey>^ callbackKeys );
			virtual ~KeyframedAnimationSet() { }

			DataStream^ Compress( float lossiness );
			DataStream^ Compress( float lossiness, Frame^ frameHierarchy );

			CallbackKey GetCallbackKey( int animation );
			array<CallbackKey>^ GetCallbackKeys();
			Result SetCallbackKey( int animation, CallbackKey callbackKey );

			RotationKey GetRotationKey( int animation, int key );
			array<RotationKey>^ GetRotationKeys( int animation );
			Result SetRotationKey( int animation, int key, RotationKey rotationKey );
			Result UnregisterRotationKey( int animation, int key );
			int GetRotationKeyCount( int animation );

			ScaleKey GetScaleKey( int animation, int key );
			array<ScaleKey>^ GetScaleKeys( int animation );
			Result SetScaleKey( int animation, int key, ScaleKey scaleKey );
			Result UnregisterScaleKey( int animation, int key );
			int GetScaleKeyCount( int animation );

			TranslationKey GetTranslationKey( int animation, int key );
			array<TranslationKey>^ GetTranslationKeys( int animation );
			Result SetTranslationKey( int animation, int key, TranslationKey translationKey );
			Result UnregisterTranslationKey( int animation, int key );
			int GetTranslationKeyCount( int animation );

			int RegisterAnimationKeys( System::String^ name, array<ScaleKey>^ scaleKeys, array<RotationKey>^ rotationKeys, array<TranslationKey>^ translationKeys );
			Result UnregisterAnimation( int animation );

			property int CallbackKeyCount { int get(); }
			property PlaybackType PlaybackType { SlimDX::Direct3D9::PlaybackType get(); }
			property double SourceTicksPerSecond { double get(); }
		};
	}
}