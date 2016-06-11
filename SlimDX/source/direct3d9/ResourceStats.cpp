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

#include "ResourceStats.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool ResourceStats::operator == ( ResourceStats left, ResourceStats right )
	{
		return ResourceStats::Equals( left, right );
	}

	bool ResourceStats::operator != ( ResourceStats left, ResourceStats right )
	{
		return !ResourceStats::Equals( left, right );
	}

	int ResourceStats::GetHashCode()
	{
		return Thrashing.GetHashCode() + ApproximateBytesDownloaded.GetHashCode() + NumberEvicted.GetHashCode()
			 + NumberVideoCreated.GetHashCode() + LastPriority.GetHashCode() + NumberUsed.GetHashCode()
			 + NumberUsedInVideoMemory.GetHashCode() + WorkingSet.GetHashCode() + WorkingSetBytes.GetHashCode()
			 + TotalManaged.GetHashCode() + TotalBytes.GetHashCode();
	}

	bool ResourceStats::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ResourceStats>( value ) );
	}

	bool ResourceStats::Equals( ResourceStats value )
	{
		return ( Thrashing == value.Thrashing && ApproximateBytesDownloaded == value.ApproximateBytesDownloaded && NumberEvicted == value.NumberEvicted
			 && NumberVideoCreated == value.NumberVideoCreated && LastPriority == value.LastPriority && NumberUsed == value.NumberUsed
			 && NumberUsedInVideoMemory == value.NumberUsedInVideoMemory && WorkingSet == value.WorkingSet && WorkingSetBytes == value.WorkingSetBytes
			 && TotalManaged == value.TotalManaged && TotalBytes == value.TotalBytes );
	}

	bool ResourceStats::Equals( ResourceStats% value1, ResourceStats% value2 )
	{
		return ( value1.Thrashing == value2.Thrashing && value1.ApproximateBytesDownloaded == value2.ApproximateBytesDownloaded && value1.NumberEvicted == value2.NumberEvicted
			 && value1.NumberVideoCreated == value2.NumberVideoCreated && value1.LastPriority == value2.LastPriority && value1.NumberUsed == value2.NumberUsed
			 && value1.NumberUsedInVideoMemory == value2.NumberUsedInVideoMemory && value1.WorkingSet == value2.WorkingSet && value1.WorkingSetBytes == value2.WorkingSetBytes
			 && value1.TotalManaged == value2.TotalManaged && value1.TotalBytes == value2.TotalBytes );
	}
}
}