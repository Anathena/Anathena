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
#ifdef XMLDOCS
		value class SoundBufferDescription;
#endif

		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>
		/// Values indicating the acoustic echo cancellation operation mode.
		/// </summary>
		public enum class AcousticEchoCancelMode : System::Int32
		{
			/// <summary>
			/// The effect is passing capture and render data through without modifying it.
			/// </summary>
			PassThrough = DSCFX_AEC_MODE_PASS_THROUGH,

			/// <summary>
			/// The effect is running in half duplex mode. Not presently supported.
			/// </summary>
			HalfDuplex = DSCFX_AEC_MODE_HALF_DUPLEX,

			/// <summary>
			/// The effect is running in full duplex mode.
			/// </summary>
			FullDuplex = DSCFX_AEC_MODE_FULL_DUPLEX,
		};

		/// <summary>
		/// Flags specifying the capabilities of the buffer.
		/// </summary>
		[System::Flags]
		public enum class BufferFlags : System::Int32
		{
			None = 0,

			/// <summary>
			/// The buffer is a primary buffer.
			/// </summary>
			PrimaryBuffer = DSBCAPS_PRIMARYBUFFER,

			/// <summary>
			/// The buffer is in on-board hardware memory.
			/// </summary>
			Static = DSBCAPS_STATIC,

			/// <summary>
			/// The buffer uses hardware mixing.
			/// </summary>
			Hardware = DSBCAPS_LOCHARDWARE,

			/// <summary>
			/// The buffer is in software memory and uses software mixing.
			/// </summary>
			Software = DSBCAPS_LOCSOFTWARE,

			/// <summary>
			/// The buffer has 3D control capability.
			/// </summary>
			Control3D = DSBCAPS_CTRL3D,

			/// <summary>
			/// The buffer has frequency control capability.
			/// </summary>
			ControlFrequency = DSBCAPS_CTRLFREQUENCY,

			/// <summary>
			/// The buffer has pan control capability.
			/// </summary>
			ControlPan = DSBCAPS_CTRLPAN,

			/// <summary>
			/// The buffer has volume control capability.
			/// </summary>
			ControlVolume = DSBCAPS_CTRLVOLUME,

			/// <summary>
			/// The buffer has position notification capability. 
			/// </summary>
			ControlPositionNotify = DSBCAPS_CTRLPOSITIONNOTIFY,

			/// <summary>
			/// The buffer supports effects processing.
			/// </summary>
			ControlEffects = DSBCAPS_CTRLFX,

			/// <summary>
			/// The buffer has sticky focus. 
			/// If the user switches to another application not using DirectSound, the buffer is still audible. 
			/// However, if the user switches to another DirectSound application, the buffer is muted.
			/// </summary>
			StickyFocus = DSBCAPS_STICKYFOCUS,

			/// <summary>
			/// The buffer is a global sound buffer. 
			/// With this flag set, an application using DirectSound can continue to play its buffers if the user switches focus to another application, 
			/// even if the new application uses DirectSound.
			/// </summary>
			GlobalFocus = DSBCAPS_GLOBALFOCUS,

			/// <summary>
			/// The buffer uses the new behavior of the play cursor when retrieving the current position.
			/// </summary>
			GetCurrentPosition2 = DSBCAPS_GETCURRENTPOSITION2,

			/// <summary>
			/// The sound is reduced to silence at the maximum distance. 
			/// The buffer will stop playing when the maximum distance is exceeded, so that processor time is not wasted. 
			/// Applies only to software buffers.
			/// </summary>
			Mute3DAtMaxDistance = DSBCAPS_MUTE3DATMAXDISTANCE,

			/// <summary>
			/// The buffer can be assigned to a hardware or software resource at play time.
			/// </summary>
			Defer = DSBCAPS_LOCDEFER,
		};

		/// <summary>
		/// Flags specifying the status of the buffer.
		/// </summary>
		[System::Flags]
		public enum class BufferStatus : System::Int32
		{
			None = 0,

			/// <summary>
			/// The buffer is playing. If this value is not set, the buffer is stopped.
			/// </summary>
			Playing = DSBSTATUS_PLAYING,

			/// <summary>
			/// The buffer is lost and must be restored before it can be played or locked.
			/// </summary>
			BufferLost = DSBSTATUS_BUFFERLOST,

			/// <summary>
			/// The buffer is being looped. If this value is not set, the buffer will stop when it reaches the end of the sound data. 
			/// This value is returned only in combination with BufferStatus::Playing.
			/// </summary>
			Looping = DSBSTATUS_LOOPING,

			/// <summary>
			/// The buffer is playing in hardware. Set only for buffers created with the BufferFlags::Defer flag.
			/// </summary>
			Hardware = DSBSTATUS_LOCHARDWARE,

			/// <summary>
			/// The buffer is playing in software. Set only for buffers created with the BufferFlags::Defer flag.
			/// </summary>
			Software = DSBSTATUS_LOCSOFTWARE,

			/// <summary>
			/// The buffer was prematurely terminated by the voice manager and is not playing. 
			/// Set only for buffers created with the BufferFlags::Defer flag.
			/// </summary>
			Terminated = DSBSTATUS_TERMINATED,
		};

		/// <summary>
		/// Return values for Capture effects.
		/// </summary>
		public enum class CaptureEffectResult : System::Int32
		{
			/// <summary>
			/// Effect is instantiated in hardware.
			/// </summary>
			LocatedInHardware = DSCFX_LOCHARDWARE,

			/// <summary>
			/// Effect is instantiated in software.
			/// </summary>
			LocatedInSoftware = DSCFX_LOCSOFTWARE,
		};

		/// <summary>
		/// Values specifying the cooperative level of the device.
		/// </summary>
		[System::Flags]
		public enum class CooperativeLevel : System::Int32
		{
			/// <summary>
			/// Sets the normal level. 
			/// This level has the smoothest multitasking and resource-sharing behavior, 
			/// but because it does not allow the primary buffer format to change, output is restricted to the default 8-bit format.
			/// </summary>
			Normal = DSSCL_NORMAL,

			/// <summary>
			/// Sets the priority level.
			/// </summary>
			Priority = DSSCL_PRIORITY,

			/// <summary>
			/// Sets the write-primary level. The application has write access to the primary buffer. No secondary buffers can be played. 
			/// </summary>
			WritePrimary = DSSCL_WRITEPRIMARY,
		};

		/// <summary>
		/// Various sound buffer locking flags.
		/// </summary>
		[System::Flags]
		public enum class LockFlags : System::Int32
		{
			None = 0,

			/// <summary>
			/// Start the lock at the write cursor.
			/// </summary>
			FromWriteCursor = DSBLOCK_FROMWRITECURSOR,

			/// <summary>
			/// Lock the entire buffer.
			/// </summary>
			EntireBuffer = DSBLOCK_ENTIREBUFFER,
		};

		/// <summary>
		/// The 3-D sound processing mode to be set.
		/// </summary>
		[System::Flags]
		public enum class Mode3D : System::Int32
		{
			/// <summary>
			/// Normal processing. This is the default mode.
			/// </summary>
			Normal = DS3DMODE_NORMAL,

			/// <summary>
			/// Sound parameters (position, velocity, and orientation) are relative to the listener's parameters. 
			/// In this mode, the absolute parameters of the sound are updated automatically as the listener's parameters change, so that the relative parameters remain constant.
			/// </summary>
			HeadRelative = DS3DMODE_HEADRELATIVE,

			/// <summary>
			/// Processing of 3D sound is disabled. The sound seems to originate from the center of the listener's head.
			/// </summary>
			Disable = DS3DMODE_DISABLE,
		};

		/// <summary>
		/// Indicates the result of the attempt to set an effect in the buffer. 
		/// </summary>
		public enum class SoundEffectResult : System::Int32
		{
			/// <summary>
			/// Effect is available but was not created because one of the other effects requested could not be created. 
			/// If any of the effects requested cannot be created, none of them are, and the call fails.
			/// </summary>
			Present = DSFXR_PRESENT,

			/// <summary>
			/// Effect is instantiated in hardware.
			/// </summary>
			LocatedInHardware = DSFXR_LOCHARDWARE,

			/// <summary>
			/// Effect is instantiated in software.
			/// </summary>
			LocatedInSoftware = DSFXR_LOCSOFTWARE,

			/// <summary>
			/// Effect has not yet been assigned to hardware or software. 
			/// This value is returned if the buffer was created with the BufferFlags::Defer flag in <see cref="SlimDX::DirectSound::SoundBufferDescription"/> Flags.
			/// </summary>
			Unallocated = DSFXR_UNALLOCATED,

			/// <summary>
			/// No effect was created because resources weren't available.
			/// </summary>
			Failed = DSFXR_FAILED,

			/// <summary>
			/// Effect is not registered on the system, and the method failed as a result.
			/// </summary>
			Unknown = DSFXR_UNKNOWN,
		};

		public enum class SpeakerConfiguration : System::Int32
		{
			/// <summary>
			/// The audio is passed through directly, without being configured for 
			/// speakers. This specifies a speakerless configuration in which the 
			/// channels in the playback stream from the application are output 
			/// directly to the audio adapter without being interpreted as speaker 
			/// positions. However, the input stream can still be modified by 
			/// sample-rate conversion, attenuation, filtering, and other types of 
			/// processing that require no assumptions about the assignment of speakers 
			/// to channels.
			/// </summary>
			DirectOut = DSSPEAKER_DIRECTOUT,

			/// <summary>
			/// The audio is played through (stereo) headphones.
			/// </summary>
			Headphone = DSSPEAKER_HEADPHONE,

			/// <summary>
			/// The audio is played through a single speaker.
			/// </summary>
			Mono = DSSPEAKER_MONO,

			/// <summary>
			/// The audio is played through quadraphonic speakers.
			/// </summary>
			Quadrophonic = DSSPEAKER_QUAD,

			/// <summary>
			/// The audio is played through stereo speakers (default value).
			/// </summary>
			Stereo = DSSPEAKER_STEREO,

			/// <summary>
			/// The audio is played through surround speakers.
			/// This specifies a four-channel PCM format in which the channels are 
			/// mapped to left, right, center, and back speakers.
			/// </summary>
			Surround = DSSPEAKER_SURROUND,

			/// <summary>
			/// obsolete 5.1 setting
			/// </summary>
			FivePointOne = DSSPEAKER_5POINT1,

			/// <summary>
			/// obsolete 7.1 setting
			/// </summary>
			SevenPointOne = DSSPEAKER_7POINT1,

			/// <summary>
			/// correct 7.1 Home Theater setting
			/// </summary>
			SevenPointOneSurround = DSSPEAKER_7POINT1_SURROUND,

			/// <summary>
			/// same as SevenPointOne
			/// </summary>
			SevenPointOneWide = DSSPEAKER_7POINT1_WIDE
		};

		/// <summary>
		/// Various speaker configurations.
		/// </summary>
		public enum class SpeakerGeometry : System::Int32
		{
			None = 0,

			/// <summary>
			/// The speakers are directed over an arc of 5 degrees.
			/// </summary>
			Minimum = DSSPEAKER_GEOMETRY_MIN,

			/// <summary>
			/// The speakers are directed over an arc of 10 degrees.
			/// </summary>
			Narrow = DSSPEAKER_GEOMETRY_NARROW,

			/// <summary>
			/// The speakers are directed over an arc of 20 degrees.
			/// </summary>
			Wide = DSSPEAKER_GEOMETRY_WIDE,

			/// <summary>
			/// The speakers are directed over an arc of 180 degrees.
			/// </summary>
			Maximum = DSSPEAKER_GEOMETRY_MAX,
		};

		/// <summary>
		/// Values that specify a standard reverberation preset. 
		/// </summary>
		public enum class I3DL2ReverbPreset : System::Int32
		{
			/// <summary>
			/// 
			/// </summary>
			Default = DSFX_I3DL2_ENVIRONMENT_PRESET_DEFAULT,

			/// <summary>
			/// 
			/// </summary>
			Generic = DSFX_I3DL2_ENVIRONMENT_PRESET_GENERIC,

			/// <summary>
			/// 
			/// </summary>
			PaddedCell = DSFX_I3DL2_ENVIRONMENT_PRESET_PADDEDCELL,

			/// <summary>
			/// 
			/// </summary>
			Room = DSFX_I3DL2_ENVIRONMENT_PRESET_ROOM,

			/// <summary>
			/// 
			/// </summary>
			Bathroom = DSFX_I3DL2_ENVIRONMENT_PRESET_BATHROOM,

			/// <summary>
			/// 
			/// </summary>
			LivingRoom = DSFX_I3DL2_ENVIRONMENT_PRESET_LIVINGROOM,

			/// <summary>
			/// 
			/// </summary>
			StoneRoom = DSFX_I3DL2_ENVIRONMENT_PRESET_STONEROOM,

			/// <summary>
			/// 
			/// </summary>
			Auditorium = DSFX_I3DL2_ENVIRONMENT_PRESET_AUDITORIUM,

			/// <summary>
			/// 
			/// </summary>
			ConcertHall = DSFX_I3DL2_ENVIRONMENT_PRESET_CONCERTHALL,

			/// <summary>
			/// 
			/// </summary>
			Cave = DSFX_I3DL2_ENVIRONMENT_PRESET_CAVE,

			/// <summary>
			/// 
			/// </summary>
			Arena = DSFX_I3DL2_ENVIRONMENT_PRESET_ARENA,

			/// <summary>
			/// 
			/// </summary>
			Hangar = DSFX_I3DL2_ENVIRONMENT_PRESET_HANGAR,

			/// <summary>
			/// 
			/// </summary>
			CarpetedHallway = DSFX_I3DL2_ENVIRONMENT_PRESET_CARPETEDHALLWAY,

			/// <summary>
			/// 
			/// </summary>
			Hallway = DSFX_I3DL2_ENVIRONMENT_PRESET_HALLWAY,

			/// <summary>
			/// 
			/// </summary>
			StoneCorridor = DSFX_I3DL2_ENVIRONMENT_PRESET_STONECORRIDOR,

			/// <summary>
			/// 
			/// </summary>
			Alley = DSFX_I3DL2_ENVIRONMENT_PRESET_ALLEY,

			/// <summary>
			/// 
			/// </summary>
			Forest = DSFX_I3DL2_ENVIRONMENT_PRESET_FOREST,

			/// <summary>
			/// 
			/// </summary>
			City = DSFX_I3DL2_ENVIRONMENT_PRESET_CITY,

			/// <summary>
			/// 
			/// </summary>
			Mountains = DSFX_I3DL2_ENVIRONMENT_PRESET_MOUNTAINS,

			/// <summary>
			/// 
			/// </summary>
			Quarry = DSFX_I3DL2_ENVIRONMENT_PRESET_QUARRY,

			/// <summary>
			/// 
			/// </summary>
			Plain = DSFX_I3DL2_ENVIRONMENT_PRESET_PLAIN,

			/// <summary>
			/// 
			/// </summary>
			ParkingLot = DSFX_I3DL2_ENVIRONMENT_PRESET_PARKINGLOT,

			/// <summary>
			/// 
			/// </summary>
			SewerPipe = DSFX_I3DL2_ENVIRONMENT_PRESET_SEWERPIPE,

			/// <summary>
			/// 
			/// </summary>
			Underwater = DSFX_I3DL2_ENVIRONMENT_PRESET_UNDERWATER,

			/// <summary>
			/// 
			/// </summary>
			SmallRoom = DSFX_I3DL2_ENVIRONMENT_PRESET_SMALLROOM,

			/// <summary>
			/// 
			/// </summary>
			MediumRoom = DSFX_I3DL2_ENVIRONMENT_PRESET_MEDIUMROOM,

			/// <summary>
			/// 
			/// </summary>
			LargeRoom = DSFX_I3DL2_ENVIRONMENT_PRESET_LARGEROOM,

			/// <summary>
			/// 
			/// </summary>
			MediumHall = DSFX_I3DL2_ENVIRONMENT_PRESET_MEDIUMHALL,

			/// <summary>
			/// 
			/// </summary>
			LargeHall = DSFX_I3DL2_ENVIRONMENT_PRESET_LARGEHALL,

			/// <summary>
			/// 
			/// </summary>
			Plate = DSFX_I3DL2_ENVIRONMENT_PRESET_PLATE
		};

		/// <summary>
		/// Flags specifying the play behavior of the buffer.
		/// </summary>
		[System::Flags]
		public enum class PlayFlags : System::Int32
		{
			/// <summary>
			/// 
			/// </summary>
			None = 0,

			/// <summary>
			/// Play continues until explicitly stopped. This flag must be set when playing a primary buffer.
			/// </summary>
			Looping = DSBPLAY_LOOPING,

			/// <summary>
			/// Play this voice in a hardware buffer only.
			/// </summary>
			Hardware = DSBPLAY_LOCHARDWARE,

			/// <summary>
			/// Play this voice in a software buffer only.
			/// </summary>
			Software = DSBPLAY_LOCSOFTWARE,

			/// <summary>
			/// If the hardware has no available voices, a currently playing nonlooping buffer will be stopped to make room for the new buffer. 
			/// The buffer prematurely terminated is the one with the least time left to play.
			/// </summary>
			TerminateByTime = DSBPLAY_TERMINATEBY_TIME,

			/// <summary>
			/// If the hardware has no available voices, a currently playing buffer will be stopped to make room for the new buffer. 
			/// The buffer prematurely terminated will be selected from buffers that have the buffer's BufferFlags::Mute3DAtMaxDistance flag set and are beyond their maximum distance. If there are no such buffers, the method fails.
			/// </summary>
			TerminateByDistance = DSBPLAY_TERMINATEBY_DISTANCE,

			/// <summary>
			/// If the hardware has no available voices, a currently playing buffer will be stopped to make room for the new buffer. 
			/// The buffer prematurely terminated will be the one with the lowest priority as set by the priority parameter passed to SecondarySoundBuffer::Play for the buffer.
			/// </summary>
			TerminateByPriority = DSBPLAY_TERMINATEBY_PRIORITY,
		};

		/// <summary>
		/// Predefined audio volume attenuation values.
		/// </summary>
		public enum class Volume : System::Int32
		{
			/// <summary>
			/// Indicates an audio volume attenuated by 100 dB, which, for all practical purposes, is silence.
			/// </summary>
			Minimum = DSBVOLUME_MIN,

			/// <summary>
			/// Represents the original, unadjusted volume of the stream.
			/// </summary>
			Maximum = DSBVOLUME_MAX,
		};
	}
}