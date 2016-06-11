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

#include "WaveFormat.h"
#include "WaveFormatExtensible.h"

namespace SlimDX
{
	namespace Multimedia
	{
		public ref class WaveStream : System::IO::Stream
		{
		private:
			HMMIO handle;
			int dataOffset;
			int size;
			WaveFormat^ format;
			DataStream^ internalMemory;
			DataStream^ publicMemory;

			void Destruct();
			void Init();
			void InitStream( System::IO::Stream^ stream, int length );

		internal:
			property DataStream^ InternalMemory
			{
				DataStream^ get() { return publicMemory; }
			}

		public:
			WaveStream( System::String^ path );
			WaveStream( System::IO::Stream^ stream );
			WaveStream( System::IO::Stream^ stream, int length );
			~WaveStream();
			!WaveStream();

			/// <summary>
			/// Sets the position within the current stream.
			/// </summary>
			/// <exception cref="InvalidOperationException">Attempted to seek outside of the bounds of the stream.</exception>
			virtual System::Int64 Seek( System::Int64 offset, System::IO::SeekOrigin origin ) override;

			/// <summary>
			/// Not supported.
			/// </summary>
			/// <exception cref="NotSupportedException">Always thrown.</exception>
			virtual void Write( array<System::Byte>^ buffer, int offset, int count ) override;

			/// <summary>
			/// Reads a sequence of bytes from the current stream and advances the position
			/// within the stream by the number of bytes read.
			/// </summary>
			/// <param name="buffer">An array of values to be read from the stream.</param>
			/// <param name="offset">The zero-based byte offset in buffer at which to begin storing
			/// the data read from the current stream.</param>
			/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
			/// <returns>The number of bytes read from the stream.</returns>
			/// <exception cref="NotSupportedException">This stream does not support reading.</exception>
			/// <exception cref="ArgumentNullException"><paramref name="buffer" /> is a null reference.</exception>
			/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
			/// <exception cref="ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
			virtual int Read( array<System::Byte>^ buffer, int offset, int count ) override;

			/// <summary>
			/// Not supported.
			/// </summary>
			/// <exception cref="NotSupportedException">Always thrown.</exception>
			virtual void Flush() override;

			/// <summary>
			/// Not supported.
			/// </summary>
			/// <param name="value">Always ignored.</param>
			/// <exception cref="NotSupportedException">Always thrown.</exception>
			virtual void SetLength( System::Int64 value ) override;

			/// <summary>
			/// Gets a value indicating whether the current stream supports reading.
			/// </summary>
			/// <value>Always <c>true</c>.</value>
			property bool CanRead
			{
				virtual bool get() override { return true; }
			}

			/// <summary>
			/// Gets a value indicating whether the current stream supports seeking.
			/// </summary>
			/// <value>Always <c>true</c>.</value>
			property bool CanSeek
			{
				virtual bool get() override { return true; }
			}

			/// <summary>
			/// Gets a value indicating whether the current stream supports writing.
			/// </summary>
			/// <value>Always <c>false</c>.</value>
			property bool CanWrite
			{
				virtual bool get() override { return false; }
			}

			/// <summary>
			/// Gets the length in bytes of the stream.
			/// </summary>
			/// <value>A long value representing the length of the stream in bytes.</value>
			property System::Int64 Length
			{
				virtual System::Int64 get() override;
			}

			/// <summary>
			/// Gets or sets the position within the current stream.
			/// </summary>
			/// <value>The current position within the stream.</value>
			/// <seealso cref="Stream">Stream Class</seealso>
			property System::Int64 Position
			{
				virtual System::Int64 get() override;
				virtual void set( System::Int64 ) override;
			}

			property WaveFormat^ Format
			{
				WaveFormat^ get() { return format; }
			}
		};
	}
}