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
	namespace Direct3D11
	{
		ref class Buffer;
		
		/// <summary>
		/// Properties defining the way a buffer is bound to the pipeline as a target for stream output operations.
		/// </summary>
		public value class StreamOutputBufferBinding : System::IEquatable<StreamOutputBufferBinding>
		{
		private:
			Buffer^ m_Buffer;
			int m_Offset;

		public:
			/// <summary>
			/// Gets or sets the buffer being bound.
			/// </summary>
			property SlimDX::Direct3D11::Buffer^ Buffer
			{
				Direct3D11::Buffer^ get();
				void set( Direct3D11::Buffer^ value );
			}

			/// <summary>
			/// Gets or sets the offset from the start of the buffer of the first vertex to use (in bytes).
			/// </summary>
			property int Offset
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="StreamOutputBufferBinding"/> struct.
			/// </summary>
			/// <param name="buffer">The buffer being bound.</param>
			/// <param name="offset">The offset to the first vertex (in bytes).</param>
			StreamOutputBufferBinding( Direct3D11::Buffer^ buffer, int offset );

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( StreamOutputBufferBinding left, StreamOutputBufferBinding right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( StreamOutputBufferBinding left, StreamOutputBufferBinding right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="obj"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="other">A <see cref="SlimDX::Direct3D11::StreamOutputBufferBinding"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( StreamOutputBufferBinding other );

			/// <summary>
			/// Returns a value indicating whether the two objects are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( StreamOutputBufferBinding% value1, StreamOutputBufferBinding% value2 );
		};
	}
};