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

namespace SlimDX
{
	namespace DirectSound
	{
#ifdef XMLDOCS
		ref class SoundBuffer;
#endif

		/// <summary>
		/// Describes the capabilities of a DirectSound buffer object.
		/// </summary>
		/// <unmanaged>DSBCAPS</unmanaged>
		public value class BufferCapabilities : System::IEquatable<BufferCapabilities>
		{
		internal:
			BufferCapabilities( const DSBCAPS &caps );

		public:
			/// <summary>
			/// Size of this buffer, in bytes.
			/// </summary>
			property int BufferSize;

			/// <summary>
			/// The rate, in kilobytes per second, at which data is transferred to the buffer memory when <see cref="SlimDX::DirectSound::SoundBuffer">SoundBuffer.Unlock</see> is called.
			/// </summary>
			property int UnlockTransferRate;

			/// <summary>
			/// The processing overhead as a percentage of main processor cycles needed to mix this sound buffer.
			/// </summary>
			property int PlayCpuOverhead;

			/// <summary>
			/// Gets or Sets whether the buffer uses the new behavior of the play cursor when retrieving the current position.
			/// </summary>
			property bool CanGetCurrentPosition;

			/// <summary>
			/// Gets or Sets whether the buffer has 3D control capability.
			/// </summary>
			property bool Control3D;

			/// <summary>
			/// Gets or Sets whether the buffer has frequency control capability.
			/// </summary>
			property bool ControlFrequency;

			/// <summary>
			/// Gets or Sets whether the buffer has volume control capability.
			/// </summary>
			property bool ControlVolume;

			/// <summary>
			/// Gets or Sets whether the buffer has pan control capability.
			/// </summary>
			property bool ControlPan;

			/// <summary>
			/// Gets or Sets whether the buffer has position notification capability.
			/// </summary>
			property bool ControlPositionNotify;

			/// <summary>
			/// Gets or Sets whether the buffer supports effects processing.
			/// </summary>
			property bool ControlEffects;

			/// <summary>
			/// Gets or Sets whether the buffer is a global sound buffer.
			/// </summary>
			property bool GlobalFocus;

			/// <summary>
			/// Gets or Sets whether the buffer uses hardware mixing.
			/// </summary>
			property bool LocateInHardware;

			/// <summary>
			/// Gets or Sets whether the buffer is in software memory and uses software mixing.
			/// </summary>
			property bool LocateInSoftware;

			/// <summary>
			/// Gets or Sets whether the buffer can be assigned to a hardware or software resource at play time.
			/// </summary>
			property bool LocationDefer;

			/// <summary>
			/// Gets or Sets whether the sound is reduced to silence at the maximum distance. The buffer will stop playing when the maximum distance is exceeded, so that processor time is not wasted. Applies only to software buffers.
			/// </summary>
			property bool Mute3DAtMaximumDistance;

			/// <summary>
			/// Gets or Sets whether the buffer is a primary buffer.
			/// </summary>
			property bool PrimaryBuffer;

			/// <summary>
			/// Gets or Sets whether the buffer is in on-board hardware memory.
			/// </summary>
			property bool StaticBuffer;

			/// <summary>
			/// Gets or Sets whether the buffer has sticky focus.
			/// </summary>
			property bool StickyFocus;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( BufferCapabilities left, BufferCapabilities right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( BufferCapabilities left, BufferCapabilities right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( BufferCapabilities other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( BufferCapabilities% value1, BufferCapabilities% value2 );	
		};
	}
}
	