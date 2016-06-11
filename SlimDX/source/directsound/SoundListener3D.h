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

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>
		/// The Listener3D object is used to retrieve and set parameters that describe a listener's position, orientation, and listening environment in 3-D space.
		/// </summary>
		/// <unmanaged>IDirectSound3DListener</unmanaged>
		public ref class SoundListener3D : public ComObject
		{
			COMOBJECT(IDirectSound3DListener, SoundListener3D);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectSound::SoundListener3D"/> class
			/// with a previously created sound buffer.
			/// </summary>
			/// <param name="soundBuffer">The SlimDX::DirectSound::SoundBuffer object.</param>
			SoundListener3D( SoundBuffer^ soundBuffer );

			/// <summary>
			/// Commits any deferred settings made since the last call to this method.
			/// </summary>
			Result CommitDeferredSettings();

			/// <summary>
			/// Describes the listener's front orientation.
			/// </summary>
			property Vector3 FrontOrientation
			{
				Vector3 get();
				void set( Vector3 value );
			}

			/// <summary>
			/// Describes the listener's top orientation.
			/// </summary>
			property Vector3 TopOrientation
			{
				Vector3 get();
				void set( Vector3 value );
			}

			/// <summary>
			/// Determines if settings are set immediately or deferred.
			/// </summary>
			property bool Deferred;

			/// <summary>
			/// Gets or sets the distance factor, which is the number of meters in a vector unit.
			/// </summary>
			property float DistanceFactor
			{
			   float get();
			   void set( float value );
			}

			/// <summary>
			/// Gets or sets the multiplier for the Doppler effect.
			/// </summary>
			property float DopplerFactor
			{
			   float get();
			   void set( float value );
			}

			/// <summary>
			/// Gets or sets the rolloff factor, which determines the rate of attenuation over distance.
			/// </summary>
			property float RolloffFactor
			{
			   float get();
			   void set( float value );
			}

			/// <summary>
			/// Gets or sets the listener's position.
			/// </summary>
			property Vector3 Position
			{
			   Vector3 get();
			   void set( Vector3 value );
			}

			/// <summary>
			/// Gets or sets the listener's velocity.
			/// </summary>
			property Vector3 Velocity
			{
			   Vector3 get();
			   void set( Vector3 value );
			}

			/// <summary>
			/// Default distance factor. The default value is (1.0).
			/// </summary>
			literal float DefaultDistanceFactor = DS3D_DEFAULTDISTANCEFACTOR;

			/// <summary>
			/// Default Doppler factor. The default value is (1.0).
			/// </summary>
			literal float DefaultDopplerFactor = DS3D_DEFAULTDOPPLERFACTOR;

			/// <summary>
			/// Default rolloff factor. The default value is (1.0).
			/// </summary>
			literal float DefaultRolloffFactor = DS3D_DEFAULTROLLOFFFACTOR;

			/// <summary>
			/// Minimum distance factor. The default value is (1.0).
			/// </summary>
			literal float MinDistanceFactor = DS3D_MINDISTANCEFACTOR;

			/// <summary>
			/// Minimum Doppler factor. The default value is (1.0).
			/// </summary>
			literal float MinDopplerFactor = DS3D_MINDOPPLERFACTOR;

			/// <summary>
			/// Minimum rolloff factor. The default value is (1.0).
			/// </summary>
			literal float MinRolloffFactor = DS3D_MINROLLOFFFACTOR;

			/// <summary>
			/// Maximum distance factor. The default value is (1.0).
			/// </summary>
			literal float MaxDistanceFactor = DS3D_MAXDISTANCEFACTOR;

			/// <summary>
			/// Maximum Doppler factor. The default value is (1.0).
			/// </summary>
			literal float MaxDopplerFactor = DS3D_MAXDOPPLERFACTOR;

			/// <summary>
			/// Maximum rolloff factor. The default value is (1.0).
			/// </summary>
			literal float MaxRolloffFactor = DS3D_MAXROLLOFFFACTOR;
		};
	}
}