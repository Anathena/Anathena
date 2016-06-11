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
		/// The SoundEffectFlanger object is used to set and retrieve effect parameters on a buffer that supports flange.
		/// </summary>
		/// <unmanaged>IDirectSoundFXFlanger</unmanaged>
		public ref class FlangerEffect : public ComObject
		{
			COMOBJECT(IDirectSoundFXFlanger, FlangerEffect);

		public:
			/// <summary>
			/// Number of milliseconds the input is delayed before it is played back. The default value is 2 ms.
			/// </summary>
			property float Delay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Percentage by which the delay time is modulated by the low-frequency oscillator (LFO), in hundredths of a percentage point. The default value is 100.
			/// </summary>
			property float Depth
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Percentage of output signal to feed back into the effect's input. The default value is -50.
			/// </summary>
			property float Feedback
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Frequency of the LFO. The default value is 0.25.
			/// </summary>
			property float Frequency
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Phase differential between left and right LFOs.
			/// </summary>
			property int Phase
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Waveform shape of the LFO. By default, the waveform is a sine.
			/// </summary>
			property int Waveform
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Ratio of wet (processed) signal to dry (unprocessed) signal. The default value is 50.
			/// </summary>
			property float WetDryMix
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Default number of milliseconds the input is delayed before it is played back.
			/// </summary>
			literal float DelayDefault = 2.0f;

			/// <summary>
			/// Maximum number of milliseconds the input is delayed before it is played back.
			/// </summary>
			literal float DelayMax = DSFXFLANGER_DELAY_MAX;

			/// <summary>
			/// Minimum number of milliseconds the input is delayed before it is played back.
			/// </summary>
			literal float DelayMin = DSFXFLANGER_DELAY_MIN;

			/// <summary>
			/// Default percentage by which the delay time is modulated by the low-frequency oscillator (LFO), in hundredths of a percentage point.
			/// </summary>
			literal float DepthDefault = 100.0f;

			/// <summary>
			/// Maximum percentage by which the delay time is modulated by the low-frequency oscillator (LFO), in hundredths of a percentage point.
			/// </summary>
			literal float DepthMax = DSFXFLANGER_DEPTH_MAX;

			/// <summary>
			/// Minimum percentage by which the delay time is modulated by the low-frequency oscillator (LFO), in hundredths of a percentage point.
			/// </summary>
			literal float DepthMin = DSFXFLANGER_DEPTH_MIN;

			/// <summary>
			/// Default percentage of output signal to feed back into the effect's input.
			/// </summary>
			literal float FeedbackDefault = -50.0f;

			/// <summary>
			/// Maximum percentage of output signal to feed back into the effect's input.
			/// </summary>
			literal float FeedbackMax = DSFXFLANGER_FEEDBACK_MAX;

			/// <summary>
			/// Minimum percentage of output signal to feed back into the effect's input.
			/// </summary>
			literal float FeedbackMin = DSFXFLANGER_FEEDBACK_MIN;

			/// <summary>
			/// Default frequency of the LFO(low-frequency oscillator).
			/// </summary>
			literal float FrequencyDefault = 0.25f;

			/// <summary>
			/// Maximum frequency of the LFO(low-frequency oscillator).
			/// </summary>
			literal float FrequencyMax = DSFXFLANGER_FREQUENCY_MAX;

			/// <summary>
			/// Minimum frequency of the LFO(low-frequency oscillator).
			/// </summary>
			literal float FrequencyMin = DSFXFLANGER_FREQUENCY_MIN;

			/// <summary>
			/// Default phase differential between left and right LFOs.
			/// </summary>
			literal int   PhaseDefault = DSFXFLANGER_PHASE_ZERO;

			/// <summary>
			/// Maximum phase differential between left and right LFOs.
			/// </summary>
			literal int   PhaseMax = DSFXFLANGER_PHASE_MAX;

			/// <summary>
			/// Minimum phase differential between left and right LFOs.
			/// </summary>
			literal int   PhaseMin = DSFXFLANGER_PHASE_MIN;

			/// <summary>
			/// Negative 180 phase differential between left and right LFOs.
			/// </summary>
			literal int   PhaseNegative180 = DSFXFLANGER_PHASE_NEG_180;

			/// <summary>
			/// Negative 90 phase differential between left and right LFOs.
			/// </summary>
			literal int   PhaseNegative90 = DSFXFLANGER_PHASE_NEG_90;

			/// <summary>
			/// Zero phase differential between left and right LFOs.
			/// </summary>
			literal int	  PhaseZero = DSFXFLANGER_PHASE_ZERO;

			/// <summary>
			/// Positive 180 phase differential between left and right LFOs.
			/// </summary>
			literal int   Phase180 = DSFXFLANGER_PHASE_180;

			/// <summary>
			/// Positive 90 phase differential between left and right LFOs.
			/// </summary>
			literal int   Phase90 = DSFXFLANGER_PHASE_90;

			/// <summary>
			/// Default waveform shape of the LFO. By default, the waveform is a sine.
			/// </summary>
			literal int   WaveformDefault = DSFXFLANGER_WAVE_SIN;

			/// <summary>
			/// Triangular waveform shape of the LFO. By default, the waveform is a sine.
			/// </summary>
			literal int   WaveformTriangle = DSFXFLANGER_WAVE_TRIANGLE;

			/// <summary>
			/// Sine waveform shape of the LFO. By default, the waveform is a sine.
			/// </summary>
			literal int   WaveformSin = DSFXFLANGER_WAVE_SIN;

			/// <summary>
			/// Default ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixDefault = 50.0f;

			/// <summary>
			/// Maximum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMax = DSFXFLANGER_WETDRYMIX_MAX;

			/// <summary>
			/// Minimum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMin = DSFXFLANGER_WETDRYMIX_MIN;
		};
	}
}