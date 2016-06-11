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
		/// The SoundEffectParametricEqualizer object is used to set and retrieve effect parameters on a buffer that supports parametric equalizer effects.
		/// </summary>
		/// <unmanaged>IDirectSoundFXParamEq</unmanaged>
		public ref class ParametricEqualizer : public ComObject
		{
			COMOBJECT(IDirectSoundFXParamEq, ParametricEqualizer);

		public:
			/// <summary>
			/// Center frequency, in hertz. This value cannot exceed one-third of the frequency of the buffer. The default value is 8000.
			/// </summary>
			property float Center
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Bandwidth, in semitones. The default value is 12.
			/// </summary>
			property float Bandwidth
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Gain, The default value is 0.
			/// </summary>
			property float Gain
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Default center frequency, in hertz.
			/// </summary>
			literal float CenterDefault = 8000.0f;

			/// <summary>
			/// Maximum center frequency, in hertz.
			/// </summary>
			literal float CenterMax = DSFXPARAMEQ_CENTER_MAX;

			/// <summary>
			/// Minimum center frequency, in hertz.
			/// </summary>
			literal float CenterMin = DSFXPARAMEQ_CENTER_MIN;

			/// <summary>
			/// Default bandwidth, in semitones.
			/// </summary>
			literal float BandwidthDefault = 12.0f;

			/// <summary>
			/// Maximum bandwidth, in semitones.
			/// </summary>
			literal float BandwidthMax = DSFXPARAMEQ_BANDWIDTH_MAX;

			/// <summary>
			/// Minimum bandwidth, in semitones.
			/// </summary>
			literal float BandwidthMin = DSFXPARAMEQ_BANDWIDTH_MIN;

			/// <summary>
			/// Default gain.
			/// </summary>
			literal float GainDefault = 0.0f;

			/// <summary>
			/// Maximum gain.
			/// </summary>
			literal float GainMax = DSFXPARAMEQ_GAIN_MAX;

			/// <summary>
			/// Minimum gain.
			/// </summary>
			literal float GainMin = DSFXPARAMEQ_GAIN_MIN;
		};
	}
}