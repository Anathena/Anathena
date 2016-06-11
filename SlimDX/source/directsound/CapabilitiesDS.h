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

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>
		/// Describes the capabilities of a DirectSound device.
		/// </summary>
		/// <unmanaged>DSCAPS</unmanaged>
		public ref class Capabilities
		{
		internal:
			Capabilities( const DSCAPS& caps );

		public:
			/// <summary>
			/// The driver has been tested and certified by Microsoft. This flag is always set for WDM drivers.
			/// </summary>
			property bool Certified;

			/// <summary>
			/// The device supports all sample rates between the MinSecondarySampleRate and MaxSecondarySampleRate member values. Typically, this means that the actual output rate will be within +/- 10 hertz (Hz) of the requested frequency.
			/// </summary>
			property bool ContinuousRate;

			/// <summary>
			/// The device does not have a DirectSound driver installed, so it is being emulated through the waveform-audio functions. Performance degradation should be expected.
			/// </summary>
			property bool EmulatedDriver;

			/// <summary>
			/// Number of unallocated 3-D buffers.
			/// </summary>
			property int FreeHardware3DAllBuffers;

			/// <summary>
			/// Number of unallocated static 3-D buffers.
			/// </summary>
			property int FreeHardware3DStaticBuffers;

			/// <summary>
			/// Number of unallocated streaming 3-D buffers.
			/// </summary>
			property int FreeHardware3DStreamingBuffers;

			/// <summary>
			/// Size, in bytes, of the free memory on the sound card.
			/// </summary>
			property int FreeHardwareMemory;

			/// <summary>
			/// Number of unallocated buffers. On WDM drivers, this includes FreeHardware3DAllBuffers.
			/// </summary>
			property int FreeHardwareMixingAllBuffers;

			/// <summary>
			/// Number of unallocated static buffers.
			/// </summary>
			property int FreeHardwareMixingStaticBuffers;

			/// <summary>
			/// Number of unallocated streaming buffers.
			/// </summary>
			property int FreeHardwareMixingStreamingBuffers;

			/// <summary>
			/// Size, in bytes, of the largest contiguous block of free memory on the sound card.
			/// </summary>
			property int MaxContiguousFreeHardwareMemoryBytes;

			/// <summary>
			/// Maximum number of 3-D buffers.
			/// </summary>
			property int MaxHardware3DAllBuffers;

			/// <summary>
			/// Maximum number of static 3-D buffers.
			/// </summary>
			property int MaxHardware3DStaticBuffers;

			/// <summary>
			/// Maximum number of streaming 3-D buffers.
			/// </summary>
			property int MaxHardware3DStreamingBuffers;

			/// <summary>
			/// Number of buffers that can be mixed in hardware. This member can be less than the sum of MaxHardwareMixingStaticBuffers and MaxHardwareMixingStreamingBuffers. Resource tradeoffs frequently occur.
			/// </summary>
			property int MaxHardwareMixingAllBuffers;

			/// <summary>
			/// Maximum number of static buffers.
			/// </summary>
			property int MaxHardwareMixingStaticBuffers;

			/// <summary>
			/// Maximum number of streaming sound buffers.
			/// </summary>
			property int MaxHardwareMixingStreamingBuffers;

			/// <summary>
			/// Maximum sample rate specifications that are supported by this device's hardware secondary sound buffers.
			/// </summary>
			property int MaxSecondarySampleRate;

			/// <summary>
			/// Minimum sample rate specifications that are supported by this device's hardware secondary sound buffers.
			/// </summary>
			property int MinSecondarySampleRate;

			/// <summary>
			/// The processing overhead, as a percentage of main processor cycles, needed to mix software buffers. This varies according to the bus type, the processor type, and the clock speed.
			/// </summary>
			property int PlayCpuOverheadSoftwareBuffers;

			/// <summary>
			/// The device supports a primary buffer with 16-bit samples.
			/// </summary>
			property bool Primary16Bit;

			/// <summary>
			/// The device supports primary buffers with 8-bit samples.
			/// </summary>
			property bool Primary8Bit;

			/// <summary>
			/// Number of primary buffers supported. This value will always be 1.
			/// </summary>
			property int PrimaryBuffers;

			/// <summary>
			/// The device supports monophonic primary buffers.
			/// </summary>
			property bool PrimaryMono;

			/// <summary>
			/// The device supports stereo primary buffers.
			/// </summary>
			property bool PrimaryStereo;

			/// <summary>
			/// The device supports hardware-mixed secondary sound buffers with 16-bit samples.
			/// </summary>
			property bool Secondary16Bit;

			/// <summary>
			/// The device supports hardware-mixed secondary sound buffers with 8-bit samples.
			/// </summary>
			property bool Secondary8Bit;

			/// <summary>
			/// The device supports hardware-mixed monophonic secondary buffers.
			/// </summary>
			property bool SecondaryMono;

			/// <summary>
			/// The device supports hardware-mixed stereo secondary buffers.
			/// </summary>
			property bool SecondaryStereo;

			/// <summary>
			/// Size, in bytes, of the amount of memory on the sound card that stores static sound buffers.
			/// </summary>
			property int TotalHardwareMemory;

			/// <summary>
			/// The rate, in kilobytes per second, at which data can be transferred to hardware static sound buffers. This and the number of bytes transferred determines the duration of a call to the Buffer.Read or Buffer.Write method.
			/// </summary>
			property int UnlockTransferRateHardwareBuffers;
		};
	}
}