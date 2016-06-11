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
#include "Resource11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		value class BufferDescription;
		
		/// <summary>
		/// Represents a sequential collection of typed data elements,
		/// typically used to contain vertices, indices, or shader constant data.
		/// </summary>
		/// <unmanaged>ID3D11Buffer</unmanaged>
		public ref class Buffer : public Resource
		{
			COMOBJECT(ID3D11Buffer, Buffer);
			
		private:
			static ID3D11Buffer* Build( SlimDX::Direct3D11::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride );
		
		public:
			/// <summary>
			/// Gets the buffer's description.
			/// </summary>
			property BufferDescription Description
			{
				BufferDescription get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Buffer"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the buffer.</param>
			/// <param name="description">The description of the buffer.</param>
			Buffer( SlimDX::Direct3D11::Device^ device, BufferDescription description );

			/// <summary>
			/// Initializes a new instance of the <see cref="Buffer"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the buffer.</param>
			/// <param name="data">Initial data used to initialize the buffer.</param>
			/// <param name="description">The description of the buffer.</param>
			Buffer( SlimDX::Direct3D11::Device^ device, DataStream^ data, BufferDescription description );

			/// <summary>
			/// Initializes a new instance of the <see cref="Buffer"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the buffer.</param>
			/// <param name="sizeInBytes">The size, in bytes, of the buffer.</param>
			/// <param name="usage">The usage pattern for the buffer.</param>
			/// <param name="bindFlags">Flags specifying how the buffer will be bound to the pipeline.</param>
			/// <param name="accessFlags">Flags specifying how the buffer will be accessible from the CPU.</param>
			/// <param name="optionFlags">Miscellaneous resource options.</param>
			/// <param name="structureByteStride">The size (in bytes) of the structure element for structured buffers.</param>
			Buffer( SlimDX::Direct3D11::Device^ device, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D11::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Buffer"/> class.
			/// </summary>
			/// <param name="device">The device with which to associate the buffer.</param>
			/// <param name="data">Initial data used to initialize the buffer.</param>
			/// <param name="sizeInBytes">The size, in bytes, of the buffer.</param>
			/// <param name="usage">The usage pattern for the buffer.</param>
			/// <param name="bindFlags">Flags specifying how the buffer will be bound to the pipeline.</param>
			/// <param name="accessFlags">Flags specifying how the buffer will be accessible from the CPU.</param>
			/// <param name="optionFlags">Miscellaneous resource options.</param>
			/// <param name="structureByteStride">The size (in bytes) of the structure element for structured buffers.</param>
			Buffer( SlimDX::Direct3D11::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D11::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags, int structureByteStride );
		};
	}
};