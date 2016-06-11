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

#include "StreamOutputBufferBinding11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Defines a wrapper for stream-output related commands on the device.
		/// </summary>
		public ref class StreamOutputWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			StreamOutputWrapper( ID3D11DeviceContext* device );
			
		public:
			/// <summary>
			/// Sets the target output buffers for the stream-output stage of the pipeline.
			/// </summary>
			/// <param name="bufferBindings">A set of stream output buffer bindings to set.</param>
			void SetTargets( ... array<StreamOutputBufferBinding>^ bufferBindings );

			/// <summary>
			/// Gets the target output buffers currently set for the stream-output stage.
			/// </summary>
			/// <param name="count">The number of targets to retrieve.</param>
			/// <returns>An array of stream output target buffers.</returns>
			array<Buffer^>^ GetTargets( int count );
		};
	}
};