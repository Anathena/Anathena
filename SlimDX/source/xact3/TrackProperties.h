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
	namespace XACT3
	{
		/// <summary>
		/// The properties of a track in an active sound variation in a cue instance.
		/// </summary>
		/// <unmanaged>XACT_TRACK_PROPERTIES</unmanaged>
		public value class TrackProperties
		{
		private:
			int duration;
			int numVariations;
			int numChannels;
			int waveVariation;
			int loopCount;

		internal:
			TrackProperties(const XACT_TRACK_PROPERTIES& data);

		public:
			/// <summary>
			/// Gets the duration of the track in milliseconds.
			/// </summary>
			property int Duration
			{
				int get() { return duration; }
			}

			/// <summary>
			/// Gets the number of wave variations in the track.
			/// </summary>
			property int VariationCount
			{
				int get() { return numVariations; }
			}

			/// <summary>
			/// Gets the number of channels that are required to play the track.
			/// </summary>
			property int ChannelCount
			{
				int get() { return numChannels; }
			}

			/// <summary>
			/// Gets the index of the currently active wave variation.
			/// </summary>
			property int WaveVariation
			{
				int get() { return waveVariation; }
			}

			/// <summary>
			/// Gets the current loop count on the track.
			/// </summary>
			property int LoopCount
			{
				int get() { return loopCount; }
			}
		};
	}
}
