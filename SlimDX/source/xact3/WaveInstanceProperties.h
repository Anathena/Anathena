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

#include "WaveProperties.h"

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// The properties of a specific wave instance.
		/// </summary>
		/// <unmanaged>XACT_WAVE_INSTANCE_PROPERTIES</unmanaged>
		public value class WaveInstanceProperties
		{
		private:
			WaveProperties waveProperties;
			bool isBackgroundMusic;

		internal:
			WaveInstanceProperties(const XACT_WAVE_INSTANCE_PROPERTIES& data);

		public:
			/// <summary>
			/// Gets the properties that are common to all wave instances.
			/// </summary>
			property SlimDX::XACT3::WaveProperties WaveProperties
			{
				SlimDX::XACT3::WaveProperties get() { return waveProperties; }
			}

			/// <summary>
			/// Gets a value that is true if the wave is tagged as background music, otherwise false.
			/// </summary>
			property bool IsBackgroundMusic
			{
				bool get() { return isBackgroundMusic; }
			}
		};
	}
}
