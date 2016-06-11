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

#include "TrackProperties.h"

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// The properties for a sound that is referenced by the active variation.
		/// </summary>
		/// <unmanaged>XACT_SOUND_PROPERTIES</unmanaged>
		public value class SoundProperties
		{
		private:
			int category;
			int priority;
			int pitch;
			float volume;
			array<TrackProperties>^ trackProperties;

		internal:
			SoundProperties(const XACT_SOUND_PROPERTIES& data);

		public:
			/// <summary>
			/// Gets the category that this sound belongs to.
			/// </summary>
			property int Category
			{
				int get() { return category; }
			}

			/// <summary>
			/// Gets the priority that is assigned to this sound.
			/// </summary>
			property int Priority
			{
				int get() { return priority; }
			}

			/// <summary>
			/// Gets the current pitch of the sound.
			/// </summary>
			property int Pitch
			{
				int get() { return pitch; }
			}

			/// <summary>
			/// Gets the current volume of the sound.
			/// </summary>
			property float Volume
			{
				float get() { return volume; }
			}

			/// <summary>
			/// Gets an array of tracks contained in this sound.
			/// </summary>
			property array<TrackProperties>^ Tracks
			{
				array<TrackProperties>^ get() { return trackProperties; }
			}
		};
	}
}
