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

#include <d3d11.h>

#include "StreamOutputStatistics11.h"

namespace SlimDX
{
namespace Direct3D11
{
	StreamOutputStatistics::StreamOutputStatistics( const D3D11_QUERY_DATA_SO_STATISTICS& native )
	{
		PrimitivesWritten = native.NumPrimitivesWritten;
		StorageNeeded = native.PrimitivesStorageNeeded;
	}
	
	D3D11_QUERY_DATA_SO_STATISTICS StreamOutputStatistics::CreateNativeVersion()
	{
		D3D11_QUERY_DATA_SO_STATISTICS native;
		
		native.NumPrimitivesWritten = PrimitivesWritten;
		native.PrimitivesStorageNeeded = StorageNeeded;

		return native;
	}

	bool StreamOutputStatistics::operator == ( StreamOutputStatistics left, StreamOutputStatistics right )
	{
		return StreamOutputStatistics::Equals( left, right );
	}

	bool StreamOutputStatistics::operator != ( StreamOutputStatistics left, StreamOutputStatistics right )
	{
		return !StreamOutputStatistics::Equals( left, right );
	}

	int StreamOutputStatistics::GetHashCode()
	{
		return PrimitivesWritten.GetHashCode() + StorageNeeded.GetHashCode();
	}

	bool StreamOutputStatistics::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<StreamOutputStatistics>( value ) );
	}

	bool StreamOutputStatistics::Equals( StreamOutputStatistics value )
	{
		return ( PrimitivesWritten == value.PrimitivesWritten && StorageNeeded == value.StorageNeeded );
	}

	bool StreamOutputStatistics::Equals( StreamOutputStatistics% value1, StreamOutputStatistics% value2 )
	{
		return ( value1.PrimitivesWritten == value2.PrimitivesWritten && value1.StorageNeeded == value2.StorageNeeded );
	}
}
}
