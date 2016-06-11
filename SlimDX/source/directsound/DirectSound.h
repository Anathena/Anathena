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

#include "../multimedia/Enums.h"

#include "Enums.h"
#include "CapabilitiesDS.h"
#include "DeviceCollection.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace DirectSound
	{
		ref class SoundBuffer;

		/// <summary>
		/// The DirectSound object is used to create buffer objects, manage devices, and set up the environment.
		/// </summary>
		/// <unmanaged>IDirectSound8</unmanaged>
		public ref class DirectSound : public ComObject
		{
			COMOBJECT(IDirectSound8, DirectSound);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="DirectSound"/> class.
			/// </summary>
			DirectSound();

			/// <summary>
			/// Initializes a new instance of the <see cref="DirectSound"/> class.
			/// </summary>
			/// <param name="device"></param>
			DirectSound( System::Guid device );
			
			/// <summary>
			/// Sets the cooperative level of the application for this sound device.
			/// </summary>
			/// <param name="windowHandle"></param>
			/// <param name="cooperativeLevel"></param>
			Result SetCooperativeLevel( System::IntPtr windowHandle, CooperativeLevel cooperativeLevel );

			/// <summary>
			/// Sets the speaker configuration of the device.
			/// </summary>
			/// <param name="speakerSet"></param>
			/// <param name="geometry"></param>
			Result SetSpeakerConfiguration( SpeakerConfiguration speakerSet, SpeakerGeometry geometry );

			/// <summary>
			/// Retrieves the speaker configuration of the device.
			/// </summary>
			/// <param name="speakerSet"></param>
			/// <param name="geometry"></param>
			Result GetSpeakerConfiguration( [Out] SpeakerConfiguration% speakerSet, [Out] SpeakerGeometry% geometry );
			
			/// <summary>
			/// Creates a duplicate of the original sound buffer that shares the originals memory.
			/// </summary>
			/// <param name="original"></param>
			/// <param name="result"></param>
			Result DuplicateSoundBuffer( SoundBuffer^ original, [Out] SoundBuffer^% result ); 

			/// <summary>
			/// Ascertains whether the device driver is certified for DirectX.
			/// </summary>
			bool VerifyCertification();

			/// <summary>
			/// Retrieves the capabilities of the hardware device.
			/// </summary>
			property Capabilities^ Capabilities
			{
				SlimDX::DirectSound::Capabilities^ get();
			}

			/// <summary>
			/// Enumerates the DirectSound devices installed in the system.
			/// </summary>
			/// <returns>A collection of the devices found.</returns>
			static DeviceCollection^ GetDevices();
		};
	}
}