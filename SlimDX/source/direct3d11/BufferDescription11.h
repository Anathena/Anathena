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

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{	
		/// <summary>
		/// Describes a buffer resource.
		/// </summary>
		/// <unmanaged>D3D11_BUFFER_DESC</unmanaged>
		public value class BufferDescription : System::IEquatable<BufferDescription>
		{
		private:
			int m_ByteWidth;
			ResourceUsage m_Usage;
			BindFlags m_BindFlags;
			CpuAccessFlags m_CPUAccessFlags;
			ResourceOptionFlags m_MiscFlags;
			int m_StructureByteStride;

		internal:
			BufferDescription( const D3D11_BUFFER_DESC& native );
			
		public:
			/// <summary>
			/// Gets or sets the size, in bytes, of the buffer.
			/// </summary>
			property int SizeInBytes
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the intended usage pattern of the buffer.
			/// </summary>
			property ResourceUsage Usage
			{
				ResourceUsage get();
				void set( ResourceUsage value );
			}

			/// <summary>
			/// Gets or sets the flags specifying how the buffer is bound to the pipeline.
			/// </summary>
			property Direct3D11::BindFlags BindFlags
			{
				Direct3D11::BindFlags get();
				void set( Direct3D11::BindFlags value );
			}

			/// <summary>
			/// Gets or sets the flags specifying how the CPU will be allowed to access the buffer.
			/// </summary>
			property Direct3D11::CpuAccessFlags CpuAccessFlags
			{
				Direct3D11::CpuAccessFlags get();
				void set( Direct3D11::CpuAccessFlags value );
			}

			/// <summary>
			/// Gets or sets the flags specifying miscellaneous resource options.
			/// </summary>
			property ResourceOptionFlags OptionFlags
			{
				ResourceOptionFlags get();
				void set( ResourceOptionFlags value );
			}
			
			/// <summary>
			/// Gets or sets the size (in bytes) of the structure used for elements of a structured buffer.
			/// </summary>
			property int StructureByteStride
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="BufferDescription"/> struct.
			/// </summary>
			/// <param name="sizeInBytes">The size, in bytes, of the buffer.</param>
			/// <param name="usage">The usage pattern for the buffer.</param>
			/// <param name="bindFlags">Flags specifying how the buffer will be bound to the pipeline.</param>
			/// <param name="accessFlags">Flags specifying how the buffer will be accessible from the CPU.</param>
			/// <param name="optionFlags">Miscellaneous resource options.</param>
			/// <param name="structureByteStride">The size (in bytes) of the structure for a structured buffer.</param>
			BufferDescription( int sizeInBytes, ResourceUsage usage, Direct3D11::BindFlags bindFlags, Direct3D11::CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride );
		
			/// <summary>
			/// Tests for equality between two BufferDescriptions.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( BufferDescription left, BufferDescription right );
			
			/// <summary>
			/// Tests for inequality between two BufferDescriptions.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( BufferDescription left, BufferDescription right );
			
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
			/// <param name="other">A <see cref="BufferDescription"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( BufferDescription other );

			/// <summary>
			/// Returns a value indicating whether the two BufferDescriptions are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( BufferDescription% value1, BufferDescription% value2 );
		};
	}
};