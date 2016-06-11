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
		/// The SoundEffectChorus object is used to set and retrieve effect parameters on a buffer that supports chorus.
		/// </summary>
		/// <unmanaged>IDirectSoundFXChorus</unmanaged>
		public ref class ChorusEffect : public ComObject
		{
			COMOBJECT(IDirectSoundFXChorus, ChorusEffect);

		public:
			/// <summary>
			/// Number of milliseconds the input is delayed before it is played back.
			/// </summary>
			property float Delay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Percentage by which the delay time is modulated by the low-frequency oscillator, in hundredths of a percentage point.
			/// </summary>
			property float Depth
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Percentage of output signal to feed back into the effect's input.
			/// </summary>
			property float Feedback
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Frequency of the LFO.
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
			/// Waveform shape of the LFO.
			/// </summary>
			property int Waveform
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			property float WetDryMix
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Default number of milliseconds the input is delayed before it is played back. The default value is 50. 
			/// </summary>
			literal float DelayDefault = 16.0f;

			/// <summary>
			/// Maximum number of milliseconds the input is delayed before it is played back. 
			/// </summary>
			literal float DelayMax = DSFXCHORUS_DELAY_MAX;

			/// <summary>
			/// Minimum number of milliseconds the input is delayed before it is played back. 
			/// </summary>
			literal float DelayMin = DSFXCHORUS_DELAY_MIN;

			/// <summary>
			/// Default percentage by which the delay time is modulated by the low-frequency oscillator, in hundredths of a percentage point. The default value is 10.
			/// </summary>
			literal float DepthDefault = 10.0f;

			/// <summary>
			/// Maximum percentage by which the delay time is modulated by the low-frequency oscillator, in hundredths of a percentage point.
			/// </summary>
			literal float DepthMax = DSFXCHORUS_DEPTH_MAX;

			/// <summary>
			/// Minimum percentage by which the delay time is modulated by the low-frequency oscillator, in hundredths of a percentage point.
			/// </summary>
			literal float DepthMin = DSFXCHORUS_DEPTH_MIN;

			/// <summary>
			/// Default percentage of output signal to feed back into the effect's input. The default value is 25.
			/// </summary>
			literal float FeedbackDefault = 25.0f;

			/// <summary>
			/// Maximum percentage of output signal to feed back into the effect's input.
			/// </summary>
			literal float FeedbackMax = DSFXCHORUS_FEEDBACK_MAX;

			/// <summary>
			/// Minimum percentage of output signal to feed back into the effect's input.
			/// </summary>
			literal float FeedbackMin = DSFXCHORUS_FEEDBACK_MIN;

			/// <summary>
			/// Default frequency of the LFO. The default value is 1.1. 
			/// </summary>
			literal float FrequencyDefault = 1.1f;

			/// <summary>
			/// Maximum frequency of the LFO.
			/// </summary>
			literal float FrequencyMax = DSFXCHORUS_FREQUENCY_MAX;

			/// <summary>
			/// Minimum frequency of the LFO.
			/// </summary>
			literal float FrequencyMin = DSFXCHORUS_FREQUENCY_MIN;

			/// <summary>
			/// Positive 180 phase differential between left and right LFOs.
			/// </summary>
			literal int Phase180 = DSFXCHORUS_PHASE_180;

			/// <summary>
			/// Positive 90 phase differential between left and right LFOs.
			/// </summary>
			literal int Phase90 = DSFXCHORUS_PHASE_90;

			/// <summary>
			/// Default phase differential between left and right LFOs. The default value is Phase90.
			/// </summary>
			literal int PhaseDefault = DSFXCHORUS_PHASE_90;

			/// <summary>
			/// Maximum phase differential between left and right LFOs.
			/// </summary>
			literal int PhaseMax = DSFXCHORUS_PHASE_MAX;

			/// <summary>
			/// Minimum phase differential between left and right LFOs.
			/// </summary>
			literal int PhaseMin = DSFXCHORUS_PHASE_MIN;

			/// <summary>
			/// Negative 180 phase differential between left and right LFOs.
			/// </summary>
			literal int PhaseNegative180 = DSFXCHORUS_PHASE_NEG_180;

			/// <summary>
			/// Negative 90 phase differential between left and right LFOs.
			/// </summary>
			literal int PhaseNegative90 = DSFXCHORUS_PHASE_NEG_90;

			/// <summary>
			/// Zero phase differential between left and right LFOs.
			/// </summary>
			literal int PhaseZero = DSFXCHORUS_PHASE_ZERO;

			/// <summary>
			/// Default waveform shape of the LFO. By default, the waveform is a sine.
			/// </summary>
			literal int WaveformDefault = DSFXCHORUS_WAVE_SIN;

			/// <summary>
			/// Sine waveform shape of the LFO.
			/// </summary>
			literal int WaveformSin = DSFXCHORUS_WAVE_SIN;

			/// <summary>
			/// Triangle waveform shape of the LFO.
			/// </summary>
			literal int WaveformTriangle = DSFXCHORUS_WAVE_TRIANGLE;

			/// <summary>
			/// Default ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixDefault = 50.0f;

			/// <summary>
			/// Maximum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMax = DSFXCHORUS_WETDRYMIX_MAX;

			/// <summary>
			/// Minimum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMin = DSFXCHORUS_WETDRYMIX_MIN;
		};
	}
}