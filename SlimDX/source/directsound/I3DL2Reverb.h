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

#include "Enums.h"

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>
		/// The SoundEffectI3DL2Reverb object is used to set and retrieve effect parameters on a buffer that supports I3DL2 (Interactive 3D Audio Level 2) reverberation effects.
		/// </summary>
		/// <unmanaged>IDirectSoundFXI3DL2Reverb</unmanaged>
		public ref class I3DL2Reverb : public ComObject
		{
			COMOBJECT(IDirectSoundFXI3DL2Reverb, I3DL2Reverb);

		public:
			/// <summary>
			/// Attenuation of the room effect, in millibels (mB).
			/// </summary>
			property int Room
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Attenuation of the room high-frequency effect, in mB.
			/// </summary>
			property int RoomHF
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Rolloff factor for the reflected signals.
			/// </summary>
			property float RoomRolloffFactor
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Decay time, in seconds.
			/// </summary>
			property float DecayTime
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Ratio of the decay time at high frequencies to the decay time at low frequencies.
			/// </summary>
			property float DecayHFRatio
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Attenuation of early reflections relative to Room, in mB.
			/// </summary>
			property int Reflections
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Delay time of the first reflection relative to the direct path, in seconds.
			/// </summary>
			property float ReflectionsDelay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Attenuation of late reverberation relative to Room, in mB.
			/// </summary>
			property int Reverb
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Time limit between the early reflections and the late reverberation relative to the time of the first reflection, in seconds.
			/// </summary>
			property float ReverbDelay
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Echo density in the late reverberation decay, in percent.
			/// </summary>
			property float Diffusion
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Modal density in the late reverberation decay, in percent.
			/// </summary>
			property float Density
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Reference high frequency, in hertz.
			/// </summary>
			property float HFReference
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// Gets or sets the quality of the environmental reverberation effect.
			/// </summary>
			property int Quality
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets or sets the standard reverberation parameters of a buffer.
			/// </summary>
			property I3DL2ReverbPreset Preset
			{
				I3DL2ReverbPreset get();
				void set( I3DL2ReverbPreset value );
			}

			/// <summary>
			/// Default attenuation of the room effect, in millibels (mB).
			/// </summary>
			literal int   RoomDefault = DSFX_I3DL2REVERB_ROOM_DEFAULT;

			/// <summary>
			/// Maximum attenuation of the room effect, in millibels (mB).
			/// </summary>
			literal int   RoomMax = DSFX_I3DL2REVERB_ROOM_MAX;

			/// <summary>
			/// Minimum attenuation of the room effect, in millibels (mB).
			/// </summary>
			literal int   RoomMin = DSFX_I3DL2REVERB_ROOM_MIN;

			/// <summary>
			/// Default attenuation of the room high-frequency effect, in mB.
			/// </summary>
			literal int   RoomHFDefault = DSFX_I3DL2REVERB_ROOMHF_DEFAULT;

			/// <summary>
			/// Maximum attenuation of the room high-frequency effect, in mB.
			/// </summary>
			literal int   RoomHFMax = DSFX_I3DL2REVERB_ROOMHF_MAX;

			/// <summary>
			/// Minimum attenuation of the room high-frequency effect, in mB.
			/// </summary>
			literal int   RoomHFMin = DSFX_I3DL2REVERB_ROOMHF_MIN;

			/// <summary>
			/// Default rolloff factor for the reflected signals. The rolloff factor for the direct path is controlled by the DirectSound listener.
			/// </summary>
			literal float RoomRolloffFactorDefault = DSFX_I3DL2REVERB_ROOMROLLOFFFACTOR_DEFAULT;

			/// <summary>
			/// Maximum rolloff factor for the reflected signals. The rolloff factor for the direct path is controlled by the DirectSound listener.
			/// </summary>
			literal float RoomRolloffFactorMax = DSFX_I3DL2REVERB_ROOMROLLOFFFACTOR_MAX;

			/// <summary>
			/// Minimum rolloff factor for the reflected signals. The rolloff factor for the direct path is controlled by the DirectSound listener.
			/// </summary>
			literal float RoomRolloffFactorMin = DSFX_I3DL2REVERB_ROOMROLLOFFFACTOR_MIN;

			/// <summary>
			/// Default decay time, in seconds.
			/// </summary>
			literal float DecayTimeDefault = DSFX_I3DL2REVERB_DECAYTIME_DEFAULT;

			/// <summary>
			/// Maximum decay time, in seconds.
			/// </summary>
			literal float DecayTimeMax = DSFX_I3DL2REVERB_DECAYTIME_MAX;

			/// <summary>
			/// Minimum decay time, in seconds.
			/// </summary>
			literal float DecayTimeMin = DSFX_I3DL2REVERB_DECAYTIME_MIN;

			/// <summary>
			/// Default ratio of the decay time at high frequencies to the decay time at low frequencies.
			/// </summary>
			literal float DecayHFRatioDefault = DSFX_I3DL2REVERB_DECAYHFRATIO_DEFAULT;

			/// <summary>
			/// Maximum ratio of the decay time at high frequencies to the decay time at low frequencies.
			/// </summary>
			literal float DecayHFRatioMax = DSFX_I3DL2REVERB_DECAYHFRATIO_MAX;

			/// <summary>
			/// Minimum ratio of the decay time at high frequencies to the decay time at low frequencies.
			/// </summary>
			literal float DecayHFRatioMin = DSFX_I3DL2REVERB_DECAYHFRATIO_MIN;

			/// <summary>
			/// Default attenuation of early reflections relative to Room, in mB.
			/// </summary>
			literal int   ReflectionsDefault = DSFX_I3DL2REVERB_REFLECTIONS_DEFAULT;

			/// <summary>
			/// Maximum attenuation of early reflections relative to Room, in mB.
			/// </summary>
			literal int   ReflectionsMax = DSFX_I3DL2REVERB_REFLECTIONS_MAX;

			/// <summary>
			/// Minimum attenuation of early reflections relative to Room, in mB.
			/// </summary>
			literal int   ReflectionsMin = DSFX_I3DL2REVERB_REFLECTIONS_MIN;

			/// <summary>
			/// Default delay time of the first reflection relative to the direct path, in seconds.
			/// </summary>
			literal float ReflectionsDelayDefault = DSFX_I3DL2REVERB_REFLECTIONSDELAY_DEFAULT;

			/// <summary>
			/// Maximum delay time of the first reflection relative to the direct path, in seconds.
			/// </summary>
			literal float ReflectionsDelayMax = DSFX_I3DL2REVERB_REFLECTIONSDELAY_MAX;

			/// <summary>
			/// Minimum delay time of the first reflection relative to the direct path, in seconds.
			/// </summary>
			literal float ReflectionsDelayMin = DSFX_I3DL2REVERB_REFLECTIONSDELAY_MIN;

			/// <summary>
			/// Default attenuation of late reverberation relative to Room, in mB.
			/// </summary>
			literal int   ReverbDefault = DSFX_I3DL2REVERB_REVERB_DEFAULT;

			/// <summary>
			/// Maximum attenuation of late reverberation relative to Room, in mB.
			/// </summary>
			literal int   ReverbMax = DSFX_I3DL2REVERB_REVERB_MAX;

			/// <summary>
			/// Minimum attenuation of late reverberation relative to Room, in mB.
			/// </summary>
			literal int   ReverbMin = DSFX_I3DL2REVERB_REVERB_MIN;

			/// <summary>
			/// Default time limit between the early reflections and the late reverberation relative to the time of the first reflection, in seconds.
			/// </summary>
			literal float ReverbDelayDefault = DSFX_I3DL2REVERB_REVERBDELAY_DEFAULT;

			/// <summary>
			/// Maximum time limit between the early reflections and the late reverberation relative to the time of the first reflection, in seconds.
			/// </summary>
			literal float ReverbDelayMax = DSFX_I3DL2REVERB_REVERBDELAY_MAX;

			/// <summary>
			/// Minimum time limit between the early reflections and the late reverberation relative to the time of the first reflection, in seconds.
			/// </summary>
			literal float ReverbDelayMin = DSFX_I3DL2REVERB_REVERBDELAY_MIN;

			/// <summary>
			/// Default modal density in the late reverberation decay, in percent.
			/// </summary>
			literal float DensityDefault = DSFX_I3DL2REVERB_DENSITY_DEFAULT;

			/// <summary>
			/// Maximum modal density in the late reverberation decay, in percent.
			/// </summary>
			literal float DensityMax = DSFX_I3DL2REVERB_DENSITY_MAX;

			/// <summary>
			/// Minimum modal density in the late reverberation decay, in percent.
			/// </summary>
			literal float DensityMin = DSFX_I3DL2REVERB_DENSITY_MIN;

			/// <summary>
			/// Default echo density in the late reverberation decay, in percent.
			/// </summary>
			literal float DiffusionDefault = DSFX_I3DL2REVERB_DIFFUSION_DEFAULT;

			/// <summary>
			/// Maximum echo density in the late reverberation decay, in percent.
			/// </summary>
			literal float DiffusionMax = DSFX_I3DL2REVERB_DIFFUSION_MAX;

			/// <summary>
			/// Minimum echo density in the late reverberation decay, in percent.
			/// </summary>
			literal float DiffusionMin = DSFX_I3DL2REVERB_DIFFUSION_MIN;

			/// <summary>
			/// Default reference high frequency, in hertz.
			/// </summary>
			literal float HFReferenceDefault = DSFX_I3DL2REVERB_HFREFERENCE_DEFAULT;

			/// <summary>
			/// Maximum reference high frequency, in hertz.
			/// </summary>
			literal float HFReferenceMax = DSFX_I3DL2REVERB_HFREFERENCE_MAX;

			/// <summary>
			/// Minimum reference high frequency, in hertz.
			/// </summary>
			literal float HFReferenceMin = DSFX_I3DL2REVERB_HFREFERENCE_MIN;
		};
	}
}