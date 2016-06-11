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

#include <xact3.h>

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// Flags that affect content wave preparation.
		/// </summary>
		[System::Flags]
		public enum class ContentPreparationFlags : System::Int32
		{
			/// <summary>
			/// Indicates that the units that are passed in are measured in milliseconds.
			/// </summary>
			UnitsMs = XACT_FLAG_UNITS_MS,

			/// <summary>
			/// Indicates that the units that are passed in are in samples.
			/// </summary>
			UnitsSamples = XACT_FLAG_UNITS_SAMPLES,
		};

		[System::Flags]
		public enum class CreationFlags : System::Int32
		{
			None = 0,
			AuditionMode = XACT_FLAG_API_AUDITION_MODE,
			DebugMode = XACT_FLAG_API_DEBUG_MODE
		};

		/// <summary>
		/// Flags that indicate the state of a cue.
		/// </summary>
		[System::Flags]
		public enum class CueState : System::Int32
		{
			/// <summary>
			/// The cue is created, but nothing else.
			/// </summary>
			Created = XACT_CUESTATE_CREATED,

			/// <summary>
			/// The cue is preparing to play.
			/// </summary>
			Preparing = XACT_CUESTATE_PREPARING,

			/// <summary>
			/// The cue is prepared, but not yet played.
			/// </summary>
			Prepared = XACT_CUESTATE_PREPARED,

			/// <summary>
			/// The cue is playing, but can be paused.
			/// </summary>
			Playing = XACT_CUESTATE_PLAYING,

			/// <summary>
			/// The cue is stopping.
			/// </summary>
			Stopping = XACT_CUESTATE_STOPPING,

			/// <summary>
			/// The cue is stopped.
			/// </summary>
			Stopped = XACT_CUESTATE_STOPPED,

			/// <summary>
			/// The cue is paused.
			/// </summary>
			Paused = XACT_CUESTATE_PAUSED
		};

		/// <summary>
		/// Flags that indicate the state of a sound bank.
		/// </summary>
		[System::Flags]
		public enum class SoundBankState : System::Int32
		{
			/// <summary>
			/// Indicates that the sound bank is referenced by at least one valid cue instance or other client.
			/// </summary>
			InUse = XACT_SOUNDBANKSTATE_INUSE
		};

		/// <summary>
		/// Flags that affect how playback is stopped.
		/// </summary>
		[System::Flags]
		public enum class StopFlags : System::Int32
		{
			/// <summary>
			/// Plays a sound to completion, then stops after the release (or tail) phase of a song is played.
			/// </summary>
			Release = XACT_FLAG_STOP_RELEASE,

			/// <summary>
			/// Stops playback immediately.
			/// </summary>
			Immediate = XACT_FLAG_STOP_IMMEDIATE
		};

		/// <summary>
		/// The format types of the wave bank.
		/// </summary>
		public enum class WaveBankMiniFormatTag : System::Int32
		{
			/// <summary>
			/// PCM wave format.
			/// </summary>
			Pcm = WAVEBANKMINIFORMAT_TAG_PCM,

			/// <summary>
			/// XMA format.
			/// </summary>
			Xma = WAVEBANKMINIFORMAT_TAG_XMA,

			/// <summary>
			/// ADPCM wave format.
			/// </summary>
			Adpcm = WAVEBANKMINIFORMAT_TAG_ADPCM,

			/// <summary>
			/// WMA format.
			/// </summary>
			Wma = WAVEBANKMINIFORMAT_TAG_WMA
		};

		/// <summary>
		/// Flags that indicate the state of a sound bank.
		/// </summary>
		[System::Flags]
		public enum class WaveBankState : System::Int32
		{
			/// <summary>
			/// Indicates that the wave bank is referenced by at least one valid cue instance or other client.
			/// </summary>
			InUse = XACT_WAVEBANKSTATE_INUSE,

			/// <summary>
			/// The wave is prepared for use by a cue instance.
			/// </summary>
			Prepared = XACT_WAVEBANKSTATE_PREPARED
		};

		/// <summary>
		/// Flags that indicate the state of a wave.
		/// </summary>
		public enum class WaveState : System::Int32
		{
			/// <summary>
			/// The wave is created, but nothing else.
			/// </summary>
			Created = XACT_STATE_CREATED,

			/// <summary>
			/// The wave is preparing to play.
			/// </summary>
			Preparing = XACT_STATE_PREPARING,

			/// <summary>
			/// The wave is prepared, but not yet played.
			/// </summary>
			Prepared = XACT_STATE_PREPARED,

			/// <summary>
			/// The wave is playing, but can be paused.
			/// </summary>
			Playing = XACT_STATE_PLAYING,

			/// <summary>
			/// The wave is stopping.
			/// </summary>
			Stopping = XACT_STATE_STOPPING,

			/// <summary>
			/// The wave is stopped.
			/// </summary>
			Stopped = XACT_STATE_STOPPED,

			/// <summary>
			/// The wave is paused.
			/// </summary>
			Paused = XACT_STATE_PAUSED,

			/// <summary>
			/// Indicates that the wave is in use.
			/// </summary>
			InUse = XACT_STATE_INUSE,

			/// <summary>
			/// Indicates that wave preparation failed.
			/// </summary>
			PrepareFailed = (int)XACT_STATE_PREPAREFAILED
		};
	}
}
