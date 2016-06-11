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
#pragma once

#include <dsound.h>

#include "CapabilitiesDS.h"

namespace SlimDX
{
namespace DirectSound
{
	Capabilities::Capabilities( const DSCAPS &caps )
	{
		MinSecondarySampleRate = caps.dwMinSecondarySampleRate;
		MaxSecondarySampleRate = caps.dwMaxSecondarySampleRate;
		PrimaryBuffers = caps.dwPrimaryBuffers;
		MaxHardwareMixingAllBuffers = caps.dwMaxHwMixingAllBuffers;
		MaxHardwareMixingStaticBuffers = caps.dwMaxHwMixingStaticBuffers;
		MaxHardwareMixingStreamingBuffers = caps.dwMaxHwMixingStreamingBuffers;
		FreeHardwareMixingAllBuffers = caps.dwFreeHwMixingAllBuffers;
		FreeHardwareMixingStaticBuffers = caps.dwFreeHwMixingStaticBuffers;
		FreeHardwareMixingStreamingBuffers = caps.dwFreeHwMixingStreamingBuffers;
		MaxHardware3DAllBuffers = caps.dwMaxHw3DAllBuffers;
		MaxHardware3DStaticBuffers = caps.dwMaxHw3DStaticBuffers;
		MaxHardware3DStreamingBuffers = caps.dwMaxHw3DStreamingBuffers;
		FreeHardware3DAllBuffers = caps.dwFreeHw3DAllBuffers;
		FreeHardware3DStaticBuffers = caps.dwFreeHw3DStaticBuffers;
		FreeHardware3DStreamingBuffers = caps.dwFreeHw3DStreamingBuffers;
		TotalHardwareMemory = caps.dwTotalHwMemBytes;
		FreeHardwareMemory = caps.dwFreeHwMemBytes;
		MaxContiguousFreeHardwareMemoryBytes = caps.dwMaxContigFreeHwMemBytes;
		UnlockTransferRateHardwareBuffers = caps.dwUnlockTransferRateHwBuffers;
		PlayCpuOverheadSoftwareBuffers = caps.dwPlayCpuOverheadSwBuffers;

		Certified = ( caps.dwFlags & DSCAPS_CERTIFIED ) != 0;
		ContinuousRate = ( caps.dwFlags & DSCAPS_CONTINUOUSRATE ) != 0;
		EmulatedDriver = ( caps.dwFlags & DSCAPS_EMULDRIVER ) != 0;
		PrimaryMono = ( caps.dwFlags & DSCAPS_PRIMARYMONO ) != 0;
		PrimaryStereo = ( caps.dwFlags & DSCAPS_PRIMARYSTEREO ) != 0;
		Primary16Bit = ( caps.dwFlags & DSCAPS_PRIMARY16BIT ) != 0;
		Primary8Bit = ( caps.dwFlags & DSCAPS_PRIMARY8BIT ) != 0;
		SecondaryMono = ( caps.dwFlags & DSCAPS_SECONDARYMONO ) != 0;
		SecondaryStereo = ( caps.dwFlags & DSCAPS_SECONDARYSTEREO ) != 0;
		Secondary16Bit = ( caps.dwFlags & DSCAPS_SECONDARY16BIT ) != 0;
		Secondary8Bit = ( caps.dwFlags & DSCAPS_SECONDARY8BIT ) != 0;
	}
}
}