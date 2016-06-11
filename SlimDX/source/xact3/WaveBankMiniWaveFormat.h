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

#include "Enums.h"

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// The mini-wave format for an XACT wave bank.
		/// </summary>
		/// <unmanaged>WAVEBANKMINIWAVEFORMAT</unmanaged>
		public value class WaveBankMiniWaveFormat
		{
		private:
			WaveBankMiniFormatTag formatTag;
			int channels;
			int samplesPerSecond;
			int blockAlign;
			int bitsPerSample;
			int bytesPerSecond;
			bool isWmaPro;

		internal:
			WaveBankMiniWaveFormat(const WAVEBANKMINIWAVEFORMAT& format);

		public:
			/// <summary>
			/// Gets the format of the wave file.
			/// </summary>
			property WaveBankMiniFormatTag FormatTag
			{
				WaveBankMiniFormatTag get() { return formatTag; } 
			}

			/// <summary>
			/// Gets the number of channels in the wave file
			/// </summary>
			property int Channels
			{
				int get() { return channels; } 
			}

			/// <summary>
			/// Gets the sample rate of the wave file or of the decoded audio for compressed formats.
			/// </summary>
			property int SamplesPerSecond
			{
				int get() { return samplesPerSecond; } 
			}

			/// <summary>
			/// Gets the block alignment index.
			/// </summary>
			property int BlockAlignment
			{
				int get() { return blockAlign; } 
			}

			/// <summary>
			/// Gets the bit depth of the wave file.
			/// </summary>
			property int BitsPerSample
			{
				int get() { return bitsPerSample; } 
			}

			/// <summary>
			/// Gets the bytes per second index of the xWMA file.
			/// </summary>
			property int BytesPerSecond
			{
				int get() { return bytesPerSecond; } 
			}

			property bool IsWmaPro
			{
				bool get() { return isWmaPro; }
			}
		};
	}
}
