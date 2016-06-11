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

#include "../ComObject.h"
#include "../DataStream.h"

#include "Enums.h"
#include "Resource10.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		value class BufferDescription;
		
		/// <summary>
		/// A buffer resource. A buffer is a sequential collection of typed data elements,
		/// typically used to contain vertex, index or shader constant data.
		/// </summary>
		/// <unmanaged>ID3D10Buffer</unmanaged>
		public ref class Buffer : public Resource
		{
			COMOBJECT(ID3D10Buffer, Buffer);
			
		private:
			static ID3D10Buffer* Build( SlimDX::Direct3D10::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags );
		
		public:
			/// <summary>
			/// Gets the buffer's description.
			/// </summary>
			property BufferDescription Description
			{
				BufferDescription get();
			}
			
			/// <summary>
			/// Constructs a new Buffer object.
			/// </summary>
			/// <param name="device">The device to associate the buffer with.</param>
			/// <param name="description">The description of the buffer.</param>
			Buffer( SlimDX::Direct3D10::Device^ device, BufferDescription description );

			/// <summary>
			/// Constructs a new Buffer object.
			/// </summary>
			/// <param name="device">The device to associate the buffer with.</param>
			/// <param name="data">Initial data to fill the buffer with on creation.</param>
			/// <param name="description">The description of the buffer.</param>
			Buffer( SlimDX::Direct3D10::Device^ device, DataStream^ data, BufferDescription description );

			/// <summary>
			/// Constructs a new Buffer object.
			/// </summary>
			/// <param name="device">The device to associate the buffer with.</param>
			/// <param name="sizeInBytes">The size, in bytes, of the buffer.</param>
			/// <param name="usage">Identifies the usage pattern for the buffer.</param>
			/// <param name="bindFlags">Specifies how the buffer will be bound to the pipeline.</param>
			/// <param name="accessFlags">Specifies how the buffer will be accessible from the CPU.</param>
			/// <param name="optionFlags">Specifies miscellaneous resource options.</param>
			Buffer( SlimDX::Direct3D10::Device^ device, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D10::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags );
			
			/// <summary>
			/// Constructs a new Buffer object.
			/// </summary>
			/// <param name="device">The device to associate the buffer with.</param>
			/// <param name="data">Initial data to fill the buffer with on creation.</param>
			/// <param name="sizeInBytes">The size, in bytes, of the buffer.</param>
			/// <param name="usage">Identifies the usage pattern for the buffer.</param>
			/// <param name="bindFlags">Specifies how the buffer will be bound to the pipeline.</param>
			/// <param name="accessFlags">Specifies how the buffer will be accessible from the CPU.</param>
			/// <param name="optionFlags">Specifies miscellaneous resource options.</param>
			Buffer( SlimDX::Direct3D10::Device^ device, DataStream^ data, int sizeInBytes, ResourceUsage usage, SlimDX::Direct3D10::BindFlags bindFlags, CpuAccessFlags accessFlags, ResourceOptionFlags optionFlags );
			
			/// <summary>
			/// Maps the buffer, providing CPU access to its contents.
			/// </summary>
			/// <param name="mode">The IO operations to enable on the CPU.</param>
			/// <param name="flags">Flags indicating how the CPU should respond when the GPU
			/// is busy.</param>
			/// <returns>A data stream containing the mapped data. This data stream is invalidated
			/// when the buffer is unmapped.</returns>
			DataStream^ Map( MapMode mode, MapFlags flags );

			/// <summary>
			/// Unmaps the buffer.
			/// </summary>
			void Unmap();
		};
	}
};