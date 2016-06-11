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

#include "InterfaceTimings.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool InterfaceTimings::operator == ( InterfaceTimings left, InterfaceTimings right )
	{
		return InterfaceTimings::Equals( left, right );
	}

	bool InterfaceTimings::operator != ( InterfaceTimings left, InterfaceTimings right )
	{
		return !InterfaceTimings::Equals( left, right );
	}

	int InterfaceTimings::GetHashCode()
	{
		return WaitingForGPUToUseApplicationResourceTimePercent.GetHashCode() + WaitingForGPUToAcceptMoreCommandsTimePercent.GetHashCode() + WaitingForGPUToStayWithinLatencyTimePercent.GetHashCode()
			 + WaitingForGPUExclusiveResourceTimePercent.GetHashCode() + WaitingForGPUOtherTimePercent.GetHashCode();
	}

	bool InterfaceTimings::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<InterfaceTimings>( value ) );
	}

	bool InterfaceTimings::Equals( InterfaceTimings value )
	{
		return ( WaitingForGPUToUseApplicationResourceTimePercent == value.WaitingForGPUToUseApplicationResourceTimePercent && WaitingForGPUToAcceptMoreCommandsTimePercent == value.WaitingForGPUToAcceptMoreCommandsTimePercent && WaitingForGPUToStayWithinLatencyTimePercent == value.WaitingForGPUToStayWithinLatencyTimePercent
			 && WaitingForGPUExclusiveResourceTimePercent == value.WaitingForGPUExclusiveResourceTimePercent && WaitingForGPUOtherTimePercent == value.WaitingForGPUOtherTimePercent );
	}

	bool InterfaceTimings::Equals( InterfaceTimings% value1, InterfaceTimings% value2 )
	{
		return ( value1.WaitingForGPUToUseApplicationResourceTimePercent == value2.WaitingForGPUToUseApplicationResourceTimePercent && value1.WaitingForGPUToAcceptMoreCommandsTimePercent == value2.WaitingForGPUToAcceptMoreCommandsTimePercent && value1.WaitingForGPUToStayWithinLatencyTimePercent == value2.WaitingForGPUToStayWithinLatencyTimePercent
			 && value1.WaitingForGPUExclusiveResourceTimePercent == value2.WaitingForGPUExclusiveResourceTimePercent && value1.WaitingForGPUOtherTimePercent == value2.WaitingForGPUOtherTimePercent );
	}
}
}