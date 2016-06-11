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

#include "Enums.h"
#include "Resource.h"
#include "../DataStream.h"

#include "IndexBufferDescription.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Represents a buffer of index data.</summary>
		/// <unmanaged href="bb205865">IDirect3DIndexBuffer9</unmanaged>
		public ref class IndexBuffer : public Resource
		{
			COMOBJECT(IDirect3DIndexBuffer9, IndexBuffer);
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="IndexBuffer"/> class.
			/// </summary>
			/// <param name="device">The device that will be used to create the buffer.</param>
			/// <param name="sizeInBytes">Size of the buffer, in bytes.</param>
			/// <param name="usage">The requested usage of the buffer.</param>
			/// <param name="pool">The memory class into which the resource will be placed.</param>
			/// <param name="sixteenBit"><c>true</c> to create a buffer of 16-bit indices; <c>false</c> to create a buffer of 32-bit indices.</param>
			IndexBuffer( SlimDX::Direct3D9::Device^ device, int sizeInBytes, Usage usage, Pool pool, bool sixteenBit );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="IndexBuffer"/> class.
			/// </summary>
			/// <param name="device">The device that will be used to create the buffer.</param>
			/// <param name="sizeInBytes">Size of the buffer, in bytes.</param>
			/// <param name="usage">The requested usage of the buffer.</param>
			/// <param name="pool">The memory class into which the resource will be placed.</param>
			/// <param name="sixteenBit"><c>true</c> to create a buffer of 16-bit indices; <c>false</c> to create a buffer of 32-bit indices.</param>
			/// <param name="sharedHandle">The variable that will receive the shared handle for this resource.</param>
			/// <remarks>This method is only available in Direct3D9 Ex.</remarks>
			IndexBuffer( SlimDX::Direct3D9::Device^ device, int sizeInBytes, Usage usage, Pool pool, bool sixteenBit, [Out] System::IntPtr% sharedHandle );
			
			/// <summary>
			/// Locks the buffer and obtains a pointer to the memory.
			/// </summary>
			/// <param name="offset">Offset into the index data to lock, in bytes.</param>
			/// <param name="size">Size of the index data to lock, in bytes.</param>
			/// <param name="flags">Locking flags.</param>
			/// <returns>A <see cref="DataStream"/> containing the index data.</returns>
			DataStream^ Lock( int offset, int size, LockFlags flags );

			/// <summary>
			/// Unlocks previously locked data.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Unlock();
			
			/// <summary>
			/// Gets a description of the buffer.
			/// </summary>
			property IndexBufferDescription Description
			{
				IndexBufferDescription get();
			}
		};
	}
}
