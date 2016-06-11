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
		/// The SoundEffectWavesReverb object is used to set and retrieve effect parameters on a buffer that supports waves reverberation.
		/// </summary>
		/// <unmanaged>IDirectSoundFXWavesReverb</unmanaged>
		public ref class WavesReverb : public ComObject
		{
			COMOBJECT(IDirectSoundFXWavesReverb, WavesReverb);

		public:
			/// <summary>
			/// Input gain of signal, in decibels (dB), the default value is 0 dB.
			/// </summary>
			property float InGain
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Reverb mix, in dB.
			/// </summary>
			property float ReverbMix
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Reverb time, in milliseconds.
			/// </summary>
			property float ReverbTime
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// High-frequency reverb time ratio.
			/// </summary>
			property float HighFrequencyRTRatio
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Default input gain of signal, in decibels (dB).
			/// </summary>
			literal float InGainDefault = DSFX_WAVESREVERB_INGAIN_DEFAULT;

			/// <summary>
			/// Maximum input gain of signal, in decibels (dB).
			/// </summary>
			literal float InGainMax = DSFX_WAVESREVERB_INGAIN_MAX;

			/// <summary>
			/// Minimum input gain of signal, in decibels (dB).
			/// </summary>
			literal float InGainMin = DSFX_WAVESREVERB_INGAIN_MIN;

			/// <summary>
			/// Default reverb mix, in dB.
			/// </summary>
			literal float ReverbMixDefault = DSFX_WAVESREVERB_REVERBMIX_DEFAULT;

			/// <summary>
			/// Maximum reverb mix, in dB.
			/// </summary>
			literal float ReverbMixMax = DSFX_WAVESREVERB_REVERBMIX_MAX;

			/// <summary>
			/// Minimum reverb mix, in dB.
			/// </summary>
			literal float ReverbMixMin = DSFX_WAVESREVERB_REVERBMIX_MIN;

			/// <summary>
			/// Default reverb time, in milliseconds.
			/// </summary>
			literal float ReverbTimeDefault = DSFX_WAVESREVERB_REVERBTIME_DEFAULT;

			/// <summary>
			/// Maximum reverb time, in milliseconds.
			/// </summary>
			literal float ReverbTimeMax = DSFX_WAVESREVERB_REVERBTIME_MAX;

			/// <summary>
			/// Minimum reverb time, in milliseconds.
			/// </summary>
			literal float ReverbTimeMin = DSFX_WAVESREVERB_REVERBTIME_MIN;

			/// <summary>
			/// Default high-frequency reverb time ratio.
			/// </summary>
			literal float HighFrequencyRTRatioDefault = DSFX_WAVESREVERB_HIGHFREQRTRATIO_DEFAULT;

			/// <summary>
			/// Maximum high-frequency reverb time ratio.
			/// </summary>
			literal float HighFrequencyRTRatioMax = DSFX_WAVESREVERB_HIGHFREQRTRATIO_MAX;

			/// <summary>
			/// Minimum high-frequency reverb time ratio.
			/// </summary>
			literal float HighFrequencyRTRatioMin = DSFX_WAVESREVERB_HIGHFREQRTRATIO_MIN;
		};
	}
}