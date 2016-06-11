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

#include "SoundBuffer.h"
#include "Buffer3DSettings.h"

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>
		/// The SoundBuffer3D object is used to retrieve and set parameters that describe the position, orientation, and environment of a sound buffer in 3-D space.
		/// </summary>
		/// <unmanaged>IDirectSound3DBuffer</unmanaged>
		public ref class SoundBuffer3D : public ComObject
		{
			COMOBJECT(IDirectSound3DBuffer, SoundBuffer3D);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectSound::SoundBuffer3D"/> class.
			/// </summary>
			/// <param name="soundBuffer"></param>
			/// <returns></returns>
			SoundBuffer3D( SoundBuffer^ soundBuffer );

			/// <summary>
			/// Gets or sets all the  parameters of a buffer
			/// </summary>
			property Buffer3DSettings AllParameters
			{
				Buffer3DSettings get();
				void set( Buffer3DSettings value );
			}

			/// <summary>
			/// The inside angle of the sound projection cone.
			/// </summary>
			property int InsideConeAngle
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// The outside angle of the sound projection cone.
			/// </summary>
			property int OutsideConeAngle
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// The orientation of the sound projection cone.
			/// </summary>
			property Vector3 ConeOrientation
			{
				Vector3 get();
				void set( Vector3 value );
			}

			/// <summary>
			/// The volume of the sound outside the outside angle of the sound projection cone.
			/// </summary>
			property int ConeOutsideVolume
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Settings are not applied until the application calls the SoundListener3D.CommitDeferredSettings() if true.
			/// </summary>
			property bool Deferred;

			/// <summary>
			/// The maximum distance, which is the distance from the listener beyond which sounds in this buffer are no longer attenuated.
			/// </summary>
			property float MaxDistance
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// The minimum distance, which is the distance from the listener at which sounds in this buffer begin to be attenuated.
			/// </summary>
			property float MinDistance
			{
				float get();
				void set( float value );
			}

			/// <summary>
			/// The operation mode for 3-D sound processing.
			/// </summary>
			property Mode3D Mode
			{
				Mode3D get();
				void set( Mode3D value );
			}

			/// <summary>
			/// The position of the sound source.
			/// </summary>
			property Vector3 Position
			{
				Vector3 get();
				void set( Vector3 value );
			}

			/// <summary>
			/// The velocity of the sound source.
			/// </summary>
			property Vector3 Velocity
			{
				Vector3 get();
				void set( Vector3 value );
			}

			/// <summary>
			/// Default minimum distance, in meters.
			/// </summary>
			literal float DefaultMinDistance = DS3D_DEFAULTMINDISTANCE;

			/// <summary>
			/// Default maximum distance, in meters.
			/// </summary>
			literal float DefaultMaxDistance = DS3D_DEFAULTMAXDISTANCE;

			/// <summary>
			/// Default cone angle, in degrees.
			/// </summary>
			literal float DefaultConeAngle = DS3D_DEFAULTCONEANGLE;

			/// <summary>
			/// Default outside cone volume. Volume levels are expressed as attenuation, in hundredths of a decibel.
			/// </summary>
			literal int DefaultConeOutsideVolume = DS3D_DEFAULTCONEOUTSIDEVOLUME;

			/// <summary>
			/// Minimum cone angle, in degrees.
			/// </summary>
			literal float MinConeAngle = DS3D_MINCONEANGLE;

			/// <summary>
			/// Maximum cone angle, in degrees.
			/// </summary>
			literal float MaxConeAngle = DS3D_MAXCONEANGLE;
		};
	}
}