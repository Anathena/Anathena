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
		/// The SoundEffectDistortion object is used to set and retrieve effect parameters on a buffer that supports distortion.
		/// </summary>
		/// <unmanaged>IDirectSoundFXDistortion</unmanaged>
		public ref class DistortionEffect : public ComObject
		{
			COMOBJECT(IDirectSoundFXDistortion, DistortionEffect);

		public:
			/// <summary>
			/// Amount of signal change after distortion. The default value is -18 dB.
			/// </summary>
			property float Gain
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Percentage of distortion intensity. The default value is 15 percent.
			/// </summary>
			property float Edge
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Center frequency of harmonic content addition. The default value is 2400 Hz.
			/// </summary>
			property float PostEQCenterFrequency
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Width of frequency band that determines range of harmonic content addition. The default value is 2400 Hz.
			/// </summary>
			property float PostEQBandwidth
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Filter cutoff for high-frequency harmonics attenuation. The default value is 8000 Hz.
			/// </summary>
			property float PreLowPassCutoff
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Default amount of signal change after distortion.
			/// </summary>
			literal float GainDefault = -18.0f;

			/// <summary>
			/// Maximum amount of signal change after distortion.
			/// </summary>
			literal float GainMax = DSFXDISTORTION_GAIN_MAX;

			/// <summary>
			/// Minimum amount of signal change after distortion.
			/// </summary>
			literal float GainMin = DSFXDISTORTION_GAIN_MIN;

			/// <summary>
			///  Default percentage of distortion intensity.
			/// </summary>
			literal float EdgeDefault = 15.0f;

			/// <summary>
			/// Maximum percentage of distortion intensity.
			/// </summary>
			literal float EdgeMax = DSFXDISTORTION_EDGE_MAX;

			/// <summary>
			/// Minimum percentage of distortion intensity.
			/// </summary>
			literal float EdgeMin = DSFXDISTORTION_EDGE_MIN;

			/// <summary>
			/// Default center frequency of harmonic content addition.
			/// </summary>
			literal float PostEQCenterFrequencyDefault = 2400.0f;

			/// <summary>
			/// Maximum center frequency of harmonic content addition.
			/// </summary>
			literal float PostEQCenterFrequencyMax = DSFXDISTORTION_POSTEQCENTERFREQUENCY_MAX;

			/// <summary>
			/// Minimum center frequency of harmonic content addition.
			/// </summary>
			literal float PostEQCenterFrequencyMin = DSFXDISTORTION_POSTEQCENTERFREQUENCY_MIN;

			/// <summary>
			/// Default width of frequency band that determines range of harmonic content addition.
			/// </summary>
			literal float PostEQBandwidthDefault = 2400.0f;

			/// <summary>
			/// Maximum width of frequency band that determines range of harmonic content addition.
			/// </summary>
			literal float PostEQBandwidthMax = DSFXDISTORTION_POSTEQBANDWIDTH_MAX;

			/// <summary>
			/// Minimum width of frequency band that determines range of harmonic content addition.
			/// </summary>
			literal float PostEQBandwidthMin = DSFXDISTORTION_POSTEQBANDWIDTH_MIN;

			/// <summary>
			/// Default filter cutoff for high-frequency harmonics attenuation.
			/// </summary>
			literal float PreLowPassCutoffDefault = 8000.0f;

			/// <summary>
			/// Maximum filter cutoff for high-frequency harmonics attenuation.
			/// </summary>
			literal float PreLowPassCutoffMax = DSFXDISTORTION_PRELOWPASSCUTOFF_MAX;

			/// <summary>
			/// Minimum filter cutoff for high-frequency harmonics attenuation.
			/// </summary>
			literal float PreLowPassCutoffMin = DSFXDISTORTION_PRELOWPASSCUTOFF_MIN;
		};
	}
}