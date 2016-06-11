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
#include "DirectSound.h"
#include "SoundBufferDescription.h"
#include "BufferCapabilities.h"
#include "NotificationPosition.h"

namespace SlimDX
{
	ref class DataStream;

	namespace DirectSound
	{
		/// <summary>
		/// The SoundBuffer object is used to manage sound buffers.
		/// </summary>
		/// <unmanaged>IDirectSoundBuffer</unmanaged>
		public ref class SoundBuffer abstract : public ComObject
		{
			COMOBJECT_BASE(IDirectSoundBuffer);

		private:
			bool notVirtualized;

		protected:
			SoundBuffer() { }

			DataStream^ Lock( int offset, int sizeBytes, LockFlags flags, [Out] DataStream^% secondPart );
			Result Unlock( DataStream^ firstPart, DataStream^ secondPart );

		public:
			/// <summary>
			/// Restores the memory allocation for a lost sound buffer.
			/// </summary>
			Result Restore();

			/// <summary>
			/// Causes the sound buffer to play, starting at the play cursor.
			/// </summary>
			Result Play( int priority, PlayFlags flags );

			/// <summary>
			/// Causes the sound buffer to stop playing.
			/// </summary>
			Result Stop();

			/// <summary>
			/// Writes data to the buffer.
			/// </summary>
			/// <returns></returns>
			generic<typename T> where T : value class
			Result Write( array<T>^ data, int startIndex, int count, int bufferOffset, LockFlags flags );

			/// <summary>
			/// Writes data to the buffer.
			/// </summary>
			/// <returns></returns>
			generic<typename T> where T : value class
			Result Write( array<T>^ data, int bufferOffset, LockFlags flags );

			Result SetNotificationPositions( array<NotificationPosition>^ positions );

			property SlimDX::Multimedia::WaveFormat^ Format
			{
				SlimDX::Multimedia::WaveFormat^ get();
				void set( SlimDX::Multimedia::WaveFormat^ value );
			}

			/// <summary>
			/// Retrieves the position of the write cursor in the sound buffer.
			/// </summary>
			property int CurrentWritePosition
			{
				int get();
			}

			/// <summary>
			/// Retrieves or sets the position of the play cursor.
			/// </summary>
			property int CurrentPlayPosition
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Retrieves or sets the frequency, in samples per second, at which the buffer is playing.
			/// </summary>
			property int Frequency
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Retrieves or sets the attenuation of the sound.
			/// </summary>
			property int Volume
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Retrieves or sets the relative volume of the left and right audio channels.
			/// </summary>
			property int Pan
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Retrieves the status of the sound buffer.
			/// </summary>
			property BufferStatus Status
			{
				BufferStatus get();
			}

			/// <summary>
			/// True if a requested 3D algorithm was not available and stereo panning was substituted.
			/// </summary>
			property bool NoVirtualization
			{
				bool get() { return notVirtualized; }
			protected:
				void set( bool value ) { notVirtualized = value; }
			}

			/// <summary>
			/// Retrieves the capabilities of the buffer object.
			/// </summary>
			property BufferCapabilities Capabilities
			{
				BufferCapabilities get();
			}
		};
	}
}