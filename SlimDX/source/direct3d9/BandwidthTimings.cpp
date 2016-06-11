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

#include "BandwidthTimings.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool BandwidthTimings::operator == ( BandwidthTimings left, BandwidthTimings right )
	{
		return BandwidthTimings::Equals( left, right );
	}

	bool BandwidthTimings::operator != ( BandwidthTimings left, BandwidthTimings right )
	{
		return !BandwidthTimings::Equals( left, right );
	}

	int BandwidthTimings::GetHashCode()
	{
		return MaximumBandwidthUtilized.GetHashCode() + FrontEndUploadMemoryUtilizedPercent.GetHashCode() + VertexRateUtilizedPercent.GetHashCode()
			 + TriangleSetupRateUtilizedPercent.GetHashCode() + FillRateUtilizedPercent.GetHashCode();
	}

	bool BandwidthTimings::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<BandwidthTimings>( value ) );
	}

	bool BandwidthTimings::Equals( BandwidthTimings value )
	{
		return ( MaximumBandwidthUtilized == value.MaximumBandwidthUtilized && FrontEndUploadMemoryUtilizedPercent == value.FrontEndUploadMemoryUtilizedPercent && VertexRateUtilizedPercent == value.VertexRateUtilizedPercent
			 && TriangleSetupRateUtilizedPercent == value.TriangleSetupRateUtilizedPercent && FillRateUtilizedPercent == value.FillRateUtilizedPercent );
	}

	bool BandwidthTimings::Equals( BandwidthTimings% value1, BandwidthTimings% value2 )
	{
		return ( value1.MaximumBandwidthUtilized == value2.MaximumBandwidthUtilized && value1.FrontEndUploadMemoryUtilizedPercent == value2.FrontEndUploadMemoryUtilizedPercent && value1.VertexRateUtilizedPercent == value2.VertexRateUtilizedPercent
			 && value1.TriangleSetupRateUtilizedPercent == value2.TriangleSetupRateUtilizedPercent && value1.FillRateUtilizedPercent == value2.FillRateUtilizedPercent );
	}
}
}