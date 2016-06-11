#include "stdafx.h"
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
#include <d3d9.h>
#include <d3dx9.h>

#include "PipelineTimings.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool PipelineTimings::operator == ( PipelineTimings left, PipelineTimings right )
	{
		return PipelineTimings::Equals( left, right );
	}

	bool PipelineTimings::operator != ( PipelineTimings left, PipelineTimings right )
	{
		return !PipelineTimings::Equals( left, right );
	}

	int PipelineTimings::GetHashCode()
	{
		return VertexProcessingTimePercent.GetHashCode() + PixelProcessingTimePercent.GetHashCode() + OtherGPUProcessingTimePercent.GetHashCode()
			 + GPUIdleTimePercent.GetHashCode();
	}

	bool PipelineTimings::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PipelineTimings>( value ) );
	}

	bool PipelineTimings::Equals( PipelineTimings value )
	{
		return ( VertexProcessingTimePercent == value.VertexProcessingTimePercent && PixelProcessingTimePercent == value.PixelProcessingTimePercent && OtherGPUProcessingTimePercent == value.OtherGPUProcessingTimePercent
			 && GPUIdleTimePercent == value.GPUIdleTimePercent );
	}

	bool PipelineTimings::Equals( PipelineTimings% value1, PipelineTimings% value2 )
	{
		return ( value1.VertexProcessingTimePercent == value2.VertexProcessingTimePercent && value1.PixelProcessingTimePercent == value2.PixelProcessingTimePercent && value1.OtherGPUProcessingTimePercent == value2.OtherGPUProcessingTimePercent
			 && value1.GPUIdleTimePercent == value2.GPUIdleTimePercent );
	}
}
}