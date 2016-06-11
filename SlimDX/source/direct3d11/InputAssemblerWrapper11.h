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
#include "VertexBufferBinding11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Buffer;
		ref class InputLayout;
		
		/// <summary>
		/// Defines a wrapper for input assembler related commands on the device.
		/// </summary>
		public ref class InputAssemblerWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			InputAssemblerWrapper( ID3D11DeviceContext* device );
			
		public:
			/// <summary>
			/// Gets or sets the layout of the input data currently bound to the input assembler.
			/// </summary>
			property InputLayout^ InputLayout
			{
				Direct3D11::InputLayout^ get();
				void set( Direct3D11::InputLayout^ value );
			}

			/// <summary>
			/// Gets or sets the topology of primitive data bound to the input assembler.
			/// </summary>
			property PrimitiveTopology PrimitiveTopology
			{
				Direct3D11::PrimitiveTopology get();
				void set( Direct3D11::PrimitiveTopology value );
			}
			
			/// <summary>
			/// Binds an index buffer to the input assembler.
			/// </summary>
			/// <param name="indexBuffer">The index buffer to bind.</param>
			/// <param name="format">The format of the data in the index buffer.</param>
			/// <param name="offset">The offset in bytes from the start of the index buffer to the first index to use.</param>
			void SetIndexBuffer( Buffer^ indexBuffer, DXGI::Format format, int offset );

			/// <summary>
			/// Gets the index buffer currently bound to the input assembler.
			/// </summary>
			/// <param name="indexBuffer">When the method completes, contains the currently bound index buffer.</param>
			/// <param name="format">When the method completes, contains the format of the data in the index buffer.</param>
			/// <param name="offset">When the method completes, contains the offset in bytes from the start of the index buffer to the first index to use.</param>
			void GetIndexBuffer( [Out] Buffer^ %indexBuffer, [Out] DXGI::Format %format, [Out] int %offset );
			
			/// <summary>
			/// Binds a single vertex buffer to the input assembler.
			/// </summary>
			/// <param name="slot">Index of the slot to which to bind the vertex buffer.</param>
			/// <param name="vertexBufferBinding">A binding for the input vertex buffer.</param>
			void SetVertexBuffers( int slot, VertexBufferBinding vertexBufferBinding );

			/// <summary>
			/// Binds an array of vertex buffers to the input assembler.
			/// </summary>
			/// <param name="firstSlot">Index of the first input slot to use for binding. The first vertex buffer is explicitly bound to the start slot; this causes each additional vertex buffer in the array to be implicitly bound to each subsequent input slot. There are 16 input slots.</param>
			/// <param name="vertexBufferBindings">An array of bindings for input vertex buffers.</param>
			void SetVertexBuffers( int firstSlot, ... array<VertexBufferBinding>^ vertexBufferBindings );

			/// <summary>
			/// Gets the set of vertex buffers currently bound to the input assembler.
			/// </summary>
			/// <param name="firstSlot">The input slot of the first vertex buffer to get.</param>
			/// <param name="count">The number of vertex buffers to retrieve.</param>
			/// <returns>An array of bound vertex buffers.</returns>
			array<VertexBufferBinding>^ GetVertexBuffers( int firstSlot, int count );
		};
	}
};