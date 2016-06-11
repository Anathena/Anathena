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

// Indexing in constant buffers works on 16-byte boundaries --
// even though the input data is a float[16], declaring the
// cbuffer as such will misalign the data. 16 individual floats
// would work properly, but such a structure cannot be indexed
// at runtime. But an array of four float4 objects allows runtime
// indexing and satisfies the alignment issues.
cbuffer inputs {
	float4 gInput[4];
}

RWStructuredBuffer<float> gOutput;

// Utilize 16 threads per group. The thread ID will be used
// to index into the constant buffer.
[numthreads(16, 1, 1)]
void main(uint3 threadId : SV_DispatchThreadID)
{
	// The complex indexing is required due to the inability to
	// simply declare gInput as a float[16].
	gOutput[threadId.x] = 2 * gInput[threadId.x / 4][threadId.x % 4];
}