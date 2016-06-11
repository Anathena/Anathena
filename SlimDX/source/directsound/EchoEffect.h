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
		/// The SoundEffectEcho object is used to set and retrieve effect parameters on a buffer that supports echo.
		/// </summary>
		/// <unmanaged>IDirectSoundFXEcho</unmanaged>
		public ref class EchoEffect : public ComObject
		{
			COMOBJECT(IDirectSoundFXEcho, EchoEffect);

		public:
			/// <summary>
			/// Delay for right channel, in milliseconds. The default value is 500 ms.
			/// </summary>
			property float RightDelay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Delay for left channel, in milliseconds. The default value is 500 ms.
			/// </summary>
			property float LeftDelay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Value that specifies whether to swap left and right delays with each successive echo. The default value is zero, meaning no swap.
			/// </summary>
			property int PanDelay
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Percentage of output fed back into input. The default value is 50.
			/// </summary>
			property float Feedback
			{
				float get();
				void set( float value );
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
			/// Default ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixDefault = 50.0f;

			/// <summary>
			/// Maximum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMax = DSFXECHO_WETDRYMIX_MAX;

			/// <summary>
			/// Minimum ratio of wet (processed) signal to dry (unprocessed) signal.
			/// </summary>
			literal float WetDryMixMin = DSFXECHO_WETDRYMIX_MIN;

			/// <summary>
			/// Default delay for left channel, in milliseconds.
			/// </summary>
			literal float LeftDelayDefault = 500.0f;

			/// <summary>
			/// Maximum delay for left channel, in milliseconds.
			/// </summary>
			literal float LeftDelayMax = DSFXECHO_LEFTDELAY_MAX;

			/// <summary>
			/// Minimum delay for left channel, in milliseconds.
			/// </summary>
			literal float LeftDelayMin = DSFXECHO_LEFTDELAY_MIN;

			/// <summary>
			/// Default delay for right channel, in milliseconds.
			/// </summary>
			literal float RightDelayDefault = 500.0f;

			/// <summary>
			/// Maximum delay for right channel, in milliseconds.
			/// </summary>
			literal float RightDelayMax = DSFXECHO_RIGHTDELAY_MAX;

			/// <summary>
			/// Minimum delay for right channel, in milliseconds.
			/// </summary>
			literal float RightDelayMin = DSFXECHO_RIGHTDELAY_MIN;

			/// <summary>
			/// Default value that specifies whether to swap left and right delays with each successive echo. The default value is zero, meaning no swap.
			/// </summary>
			literal int   PanDelayDefault = 0;

			/// <summary>
			/// Maximum value that specifies whether to swap left and right delays with each successive echo. The default value is zero, meaning no swap.
			/// </summary>
			literal int   PanDelayMax = DSFXECHO_PANDELAY_MAX;

			/// <summary>
			/// Minimum value that specifies whether to swap left and right delays with each successive echo. The default value is zero, meaning no swap.
			/// </summary>
			literal int   PanDelayMin = DSFXECHO_PANDELAY_MIN;

			/// <summary>
			/// Default percentage of output fed back into input.
			/// </summary>
			literal float FeedbackDefault = 50.0f;

			/// <summary>
			/// Maximum percentage of output fed back into input.
			/// </summary>
			literal float FeedbackMax = DSFXECHO_FEEDBACK_MAX;

			/// <summary>
			/// Minimum percentage of output fed back into input.
			/// </summary>
			literal float FeedbackMin = DSFXECHO_FEEDBACK_MIN;
		};
	}
}