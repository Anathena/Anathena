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

#include <d3dx9.h>

#ifdef XMLDOCS
using System::InvalidOperationException;
using System::ArgumentException;
using System::ArgumentNullException;
using System::ArgumentOutOfRangeException;
using System::NotSupportedException;
using System::IO::EndOfStreamException;
#endif

namespace SlimDX
{
	/// <summary>
	/// Provides a stream interface to a buffer located in unmanaged memory.
	/// </summary>
	/// <unmanaged>None</unmanaged>
	public ref class DataStream : public System::IO::Stream, System::IDisposable
	{
	private:
		char* m_Buffer;
		bool m_OwnsBuffer;
		ID3DXBuffer *m_ID3DXBuffer;
		
		System::Int64 m_Size;
		System::Int64 m_Position;
		
		initonly bool m_CanRead;
		initonly bool m_CanWrite;

		System::Runtime::InteropServices::GCHandle m_GCHandle;

	internal:
		DataStream( ID3DXBuffer *buffer );
		DataStream( void* buffer, System::Int64 sizeInBytes, bool canRead, bool canWrite, bool makeCopy );
		DataStream( const void *buffer, System::Int64 sizeInBytes, bool canRead, bool makeCopy );

		property char* RawPointer
		{
			char* get();
		}

		property char* PositionPointer
		{
			char* get();
		}

		property System::Int64 RemainingLength
		{
			System::Int64 get();
		}

		char* SeekToEnd();

		ID3DXBuffer* GetD3DBuffer();
		void Destruct();

	public:
		/// <summary>
		/// Initializes a new instance of the <see cref="DataStream"/> class, and allocates a new buffer to use as a backing store.
		/// </summary>
		/// <param name="sizeInBytes">The size of the buffer to be allocated, in bytes.</param>
		/// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
		/// <param name="canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="sizeInBytes" /> is less than 1.</exception>
		DataStream( System::Int64 sizeInBytes, bool canRead, bool canWrite );

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStream"/> class, using an unmanaged buffer as a backing store.
		/// </summary>
		/// <param name="userBuffer">A pointer to the buffer to be used as a backing store.</param>
		/// <param name="sizeInBytes">The size of the buffer provided, in bytes.</param>
		/// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
		/// <param name="canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
		/// <exception cref="ArgumentNullException"><paramref name="userBuffer" /> is a zero pointer.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="sizeInBytes" /> is less than 1.</exception>
		DataStream( System::IntPtr userBuffer, System::Int64 sizeInBytes, bool canRead, bool canWrite );
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DataStream"/> class, using a managed buffer as a backing store.
		/// </summary>
		/// <param name="userBuffer">A managed array to be used as a backing store.</param>
		/// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
		/// <param name="canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
		DataStream( System::Array^ userBuffer, bool canRead, bool canWrite );
		
		/// <summary>
		/// Releases all resources used by the <see cref="DataStream"/>.
		/// </summary>
		~DataStream();

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="DataStream"/> is reclaimed by garbage collection.
		/// </summary>
		!DataStream();
		
		/// <summary>Sets the position within the current stream.</summary>
		/// <exception cref="InvalidOperationException">Attempted to seek outside of the bounds of the stream.</exception>
		virtual System::Int64 Seek( System::Int64 offset, System::IO::SeekOrigin origin ) override;

		/// <summary>
		/// Writes a single value to the stream, and advances the current position
		/// within this stream by the number of bytes written.
		/// </summary>
		/// <typeparam name="T">The type of the value to be written to the stream.</typeparam>
		/// <param name="value">The value to write to the stream.</param>
		/// <exception cref="NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="EndOfStreamException">There is not enough space
		/// remaining in the stream to write this type of value.</exception>
		generic<typename T> where T : value class
		void Write( T value );

		/// <summary>
		/// Writes a sequence of bytes to the current stream and advances the current
		/// position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="NotSupportedException">This stream does not support writing.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="buffer" /> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
		virtual void Write( array<System::Byte>^ buffer, int offset, int count ) override;

		/// <summary>
		/// Writes an array of values to the current stream, and advances the current position
		/// within this stream by the number of bytes written.
		/// </summary>
		/// <typeparam name="T">The type of the values to be written to the stream.</typeparam>
		/// <param name="data">An array of values to be written to the stream.</param>
		/// <param name="offset">The zero-based offset in data at which to begin copying values to the current stream.</param>
		/// <param name="count">The number of values to be written to the current stream. If this is zero,
		/// all of the contents <paramref name="data" /> will be written.</param>
		/// <exception cref="NotSupportedException">This stream does not support writing.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="data" /> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
		generic<typename T> where T : value class
		void WriteRange( array<T>^ data, int offset, int count );

		/// <summary>
		/// Writes an array of values to the current stream, and advances the current position
		/// within this stream by the number of bytes written.
		/// </summary>
		/// <param name="data">An array of values to be written to the current stream.</param>
		/// <exception cref="NotSupportedException">This stream does not support writing.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="data" /> is a null reference.</exception>
		generic<typename T> where T : value class
		void WriteRange( array<T>^ data ) { WriteRange( data, 0, 0 ); }
		
		/// <summary>
		/// Writes a range of bytes to the current stream, and advances the current position
		/// within this stream by the number of bytes written.
		/// </summary>
		/// <param name="source">A pointer to the location to start copying from.</param>
		/// <param name="count">The number of bytes to copy from source to the current stream.</param>
		/// <exception cref="NotSupportedException">This stream does not support writing.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="source" /> is a zero pointer.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count" /> is negative.</exception>
		void WriteRange( System::IntPtr source, System::Int64 count );

		/// <summary>
		/// Reads a single value from the current stream and advances the current
		/// position within this stream by the number of bytes read.
		/// </summary>
		/// <typeparam name="T">The type of the value to be read from the stream.</typeparam>
		/// <returns>The value that was read.</returns>
		/// <exception cref="NotSupportedException">This stream does not support reading.</exception>
		/// <exception cref="EndOfStreamException">There is not enough space in the current
		/// stream to read a value of this type.</exception>
		generic<typename T> where T : value class
		T Read();

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
		/// Reads a sequence of elements from the current stream into a target buffer and
		/// advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of values to be read from the stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing
		/// the data read from the current stream.</param>
		/// <param name="count">The number of values to be read from the current stream.</param>
		/// <returns>The number of bytes read from the stream.</returns>
		/// <exception cref="NotSupportedException">This stream does not support reading.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="buffer" /> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
		generic<typename T> where T : value class
		int ReadRange( array<T>^ buffer, int offset, int count );

		/// <summary>
		/// Reads an array of values from the current stream, and advances the current position
		/// within this stream by the number of bytes written.
		/// </summary>
		/// <typeparam name="T">The type of the values to be read from the stream.</typeparam>
		/// <returns>An array of values that was read from the current stream.</returns> 
		/// <exception cref="NotSupportedException">This stream does not support reading.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count" /> is negative.</exception>
		generic<typename T> where T : value class
		array<T>^ ReadRange( int count );

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

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <value><c>true</c> if the stream supports reading; otherwise, <c>false</c>.</value>
		property bool CanRead
		{
			virtual bool get() override { return m_CanRead; }
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <value>Always <c>true</c>.</value>
		property bool CanSeek
		{
			virtual bool get() override { return true; }
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <value><c>true</c> if the stream supports writing; otherwise, <c>false</c>.</value>
		property bool CanWrite
		{
			virtual bool get() override { return m_CanWrite; }
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <value>A long value representing the length of the stream in bytes.</value>
		property System::Int64 Length
		{
			virtual System::Int64 get() override;
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <value>The current position within the stream.</value>
		/// <seealso cref="Stream">Stream Class</seealso>
		property System::Int64 Position
		{
			virtual System::Int64 get() override;
			virtual void set( System::Int64 ) override;
		}

		/// <summary>Gets the internal pointer to the current stream's backing store.</summary>
		/// <value>An IntPtr to the buffer being used as a backing store.</value>
		property System::IntPtr DataPointer
		{
			System::IntPtr get() { return System::IntPtr( m_Buffer ); }
		}
	};
}
