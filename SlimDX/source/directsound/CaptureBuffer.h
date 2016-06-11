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

#include "Enums.h"
#include "DirectSoundCapture.h"
#include "CaptureBufferDescription.h"
#include "NotificationPosition.h"

namespace SlimDX
{
	ref class DataStream;

	namespace DirectSound
	{
		/// <summary>
		/// The CaptureBuffer object is used to manipulate sound capture buffers.
		/// </summary>
		/// <unmanaged>IDirectSoundCaptureBuffer8</unmanaged>
		public ref class CaptureBuffer : public ComObject
		{
			COMOBJECT(IDirectSoundCaptureBuffer8, CaptureBuffer);

		private:
			DataStream^ Lock( int offset, int sizeBytes, bool lockEntireBuffer, [Out] DataStream^% secondPart );
			Result Unlock( DataStream^ firstPart, DataStream^ secondPart );

			generic<typename T> where T : value class
			Result InternalRead( array<T>^ data, int startIndex, int count, int bufferOffset, bool lockEntireBuffer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectSound::CaptureBuffer"/> class.
			/// </summary>
			/// <param name="capture"></param>
			/// <param name="description"></param>
			CaptureBuffer( DirectSoundCapture^ capture, CaptureBufferDescription description );
			
			generic<typename T> where T : ComObject
			T GetEffect( int index );

			/// <summary>
			/// Retrieves the status of capture effects.
			/// </summary>
			/// <param name="effectCount"></param>
			/// <returns></returns>
			array<CaptureEffectResult>^ GetEffectStatus( int effectCount );

			generic<typename T> where T : value class
			Result Read( array<T>^ data, int startIndex, int count, int bufferOffset );

			generic<typename T> where T : value class
			Result Read( array<T>^ data, int bufferOffset, bool lockEntireBuffer );

			Result SetNotificationPositions( array<NotificationPosition>^ positions );

			/// <summary>
			/// Begins capturing data into the buffer. If the buffer is already capturing, the method has no effect.
			/// </summary>
			Result Start( bool looping );

			/// <summary>
			/// Stops the buffer so that it is no longer capturing data. If the buffer is not capturing, the method has no effect.
			/// </summary>
			Result Stop();

			/// <summary>
			/// True if the buffer is currently capturing.
			/// </summary>
			property bool Capturing
			{
				bool get();
			}

			/// <summary>
			/// True if the capture buffer is looping.
			/// </summary>
			property bool Looping
			{
				bool get();
			}

			/// <summary>
			/// Retrieves the waveform format of the capture buffer.
			/// </summary>
			property SlimDX::Multimedia::WaveFormat^ Format
			{
				SlimDX::Multimedia::WaveFormat^ get();
			}

			/// <summary>
			/// The size, in bytes, of the capture buffer.
			/// </summary>
			property int SizeInBytes
			{
				int get();
			}

			/// <summary>
			/// True if the buffer wave mapped.
			/// </summary>
			property bool WaveMapped
			{
				bool get();
			}

			/// <summary>
			/// Retrieves the position of the capture cursor in the buffer. The capture cursor is ahead of the read cursor. The data after the read position up to and including the capture position is not necessarily valid data.
			/// </summary>
			property int CurrentCapturePosition
			{
				int get();
			}

			/// <summary>
			/// Retrieves the position of the read cursor in the buffer. The capture cursor is ahead of the read cursor. The data after the read position up to and including the capture position is not necessarily valid data.
			/// </summary>
			property int CurrentReadPosition
			{
				int get();
			}
		};
	}
}