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
		/// The SoundEffectGargle object is used to set and retrieve effect parameters on a buffer that supports amplitude modulation.
		/// </summary>
		/// <unmanaged>IDirectSoundFXGargle</unmanaged>
		public ref class GargleEffect : public ComObject
		{
			COMOBJECT(IDirectSoundFXGargle, GargleEffect);

		public:
			/// <summary>
			/// Rate of modulation, in Hertz. The default value is 20.
			/// </summary>
			property int Rate
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Shape of the modulation waveform. By default the waveform is a triangle.
			/// </summary>
			property int WaveShape
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Default shape of the modulation waveform.
			/// </summary>
			literal int WaveShapeDefault = DSFXGARGLE_WAVE_TRIANGLE;

			/// <summary>
			/// Triangular shape of the modulation waveform.
			/// </summary>
			literal int WaveShapeTriangle = DSFXGARGLE_WAVE_TRIANGLE;

			/// <summary>
			/// Square shape of the modulation waveform.
			/// </summary>
			literal int WaveShapeSquare = DSFXGARGLE_WAVE_SQUARE;

			/// <summary>
			/// Default rate of modulation, in Hertz.
			/// </summary>
			literal int RateDefault = 20;

			/// <summary>
			/// Maximum rate of modulation, in Hertz.
			/// </summary>
			literal int RateMax = DSFXGARGLE_RATEHZ_MAX;

			/// <summary>
			/// Minimum rate of modulation, in Hertz.
			/// </summary>
			literal int RateMin = DSFXGARGLE_RATEHZ_MIN;
		};
	}
}