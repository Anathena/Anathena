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
#include "WaveProperties.h"
#include "Wave.h"

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// Instantiate and manipulate XACT wave bank objects.
		/// </summary>
		/// <unmanaged>IXACT3WaveBank</unmanaged>
		public ref class WaveBank
		{
		private:
			IXACT3WaveBank* InternalPointer;
			Microsoft::Win32::SafeHandles::SafeFileHandle^ handle;

		internal:
			WaveBank( IXACT3WaveBank *pointer );
			WaveBank( IXACT3WaveBank *pointer, Microsoft::Win32::SafeHandles::SafeFileHandle^ handle );

		public:
			Result Destroy();

			/// <summary>
			/// Get a wave index based on a string that represents the friendly name of the wave.
			/// </summary>
			/// <param name="friendlyName">A string that contains the friendly name of the wave.</param>
			/// <returns>The index for the wave if it exists, otherwise -1.</returns>
			int GetWaveIndex(System::String^ friendlyName);

			/// <summary>
			/// Get the properties of a wave.
			/// </summary>
			/// <param name="waveIndex">The index of the wave to get the properties of.</param>
			/// <returns>A <see cref="WaveProperties"/> object containing the properties of the wave.</returns>
			WaveProperties GetWaveProperties(int waveIndex);

			/// <summary>
			/// Play a wave.
			/// </summary>
			/// <param name="waveIndex">The index of the wave to play.</param>
			/// <param name="flags"><see cref="ContentPreparationFlags"/> to indicate how the wave is to be play.</param>
			/// <param name="playOffset">Play offset to use as the start of the wave. The offset can be described in milliseconds or in samples. The <paramref name="flags"/> argument determines the offset units.</param>
			/// <param name="loopCount">The number of loops for playback.</param>
			/// <returns>A <see cref="Wave"/> object containing the newly playing wave.</returns>
			Wave^ Play(int waveIndex, ContentPreparationFlags flags, int playOffset, int loopCount);

			/// <summary>
			/// Prepare a wave for playback.
			/// </summary>
			/// <param name="waveIndex">The index of the wave to prepare.</param>
			/// <param name="flags"><see cref="ContentPreparationFlags"/> to indicate how the wave is to be prepared.</param>
			/// <param name="playOffset">Play offset to use as the start of the wave. The offset can be described in milliseconds or in samples. The <paramref name="flags"/> argument determines the offset units.</param>
			/// <param name="loopCount">The number of loops for playback.</param>
			/// <returns>A <see cref="Wave"/> object containing the newly prepared wave.</returns>
			Wave^ Prepare(int waveIndex, ContentPreparationFlags flags, int playOffset, int loopCount); 

			/// <summary>
			/// Stop playback of a wave.
			/// </summary>
			/// <param name="waveIndex">The index of the wave to stop.</param>
			/// <param name="flags"><see cref="StopFlags"/> that specify how the wave is stopped.</param>
			Result Stop(int waveIndex, StopFlags flags);

			/// <summary>
			/// Get a <see cref="WaveBankState"/> value representing the current state of the wave bank.
			/// </summary>
			property WaveBankState State
			{
				WaveBankState get();
			}

			/// <summary>
			/// Get the number of wave entries in the wave bank.
			/// </summary>
			property int WaveCount
			{
				int get();
			}
		};
	}
}
