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

RWTexture2D <float> outputTex; 

[numthreads(16, 16, 1)]
void CS( uint3 DTid : SV_DispatchThreadID )
{
	uint sum = 0;
	[unroll]for(int i = -1;i <= 1; ++i)
		[unroll]for(int j = -1;j <= 1; ++j)	 
			sum += outputTex[uint2(DTid.x + i,DTid.y + j)] > 0.5;
			
	uint x = outputTex[DTid.xy] > 0.5;
	outputTex[DTid.xy] = ((sum == 3) && (1 - x)) + ((sum == 3 || sum == 4) && x);
}