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

#include "../ComObject.h"
#include "../multimedia/WaveFormatExtensible.h"

#include "RuntimeParameters.h"
#include "RendererDetails.h"
#include "Enums.h"

namespace SlimDX
{
	using namespace Multimedia;

	namespace XACT3
	{
		ref class SoundBank;
		ref class WaveBank;

		public ref class Engine : public ComObject
		{
			COMOBJECT(IXACT3Engine, Engine);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Engine"/> class.
			/// </summary>
			Engine();
			Engine( CreationFlags flags );

			SoundBank^ CreateSoundBank( DataStream^ data );
			WaveBank^ CreateWaveBank( DataStream^ data );
			WaveBank^ CreateStreamingWaveBank( System::String^ fileName, int offset, int packetSize );

			/// <summary>
			/// Performs periodic work that is required by the XACT engine. Typically this should be called every 30 to 100 milliseconds.
			/// </summary>
			Result DoWork();

			/// <summary>
			/// Get the sound category index that corresponds to a friendly name string.
			/// </summary>
			/// <param name="friendlyName">A string describing the category's friendly name.</param>
			/// <returns>The category index for the friendly name if successful, otherwise -1.</returns>
			int GetCategory(System::String^ friendlyName);

			/// <summary>
			/// Get the value for a global variable.
			/// </summary>
			/// <param name="index">The index of the global variable.</param>
			/// <returns>The value that is currently held by the global variable.</returns>
			float GetGlobalVariable(int index);

			/// <summary>
			/// Get the corresponding index for a global variable name string.
			/// </summary>
			/// <param name="friendlyName">A string that contains the friendly name of the global variable.</param>
			/// <returns>The index for the global variable if it exists, otherwise -1.</returns>
			int GetGlobalVariableIndex(System::String^ friendlyName);

			/// <summary>
			/// Gets details about a specific renderer.
			/// </summary>
			/// <param name="rendererIndex">Index of the renderer to retrieve information from.</param>
			/// <returns>A new <see cref="RendererDetails"/> object that contains details of the renderer.</returns>
			RendererDetails GetRendererDetails(int rendererIndex);

			/// <summary>
			/// Initialize the XACT engine using common default settings.
			/// </summary>
			Result Initialize( RuntimeParameters parameters );

			/// <summary>
			/// Pause a sound category.
			/// </summary>
			/// <param name="category">Index of the category to pause, as returned by <see cref="GetCategory"/>.</param>
			/// <param name="pause">The action to take. If true, playback is paused, if false, playback is resumed.</param>
			Result Pause(int category, bool pause);

			/// <summary>
			/// Set the value of a global variable.
			/// </summary>
			/// <param name="index">The index of the global variable.</param>
			/// <param name="value">The new value of the global variable.</param>
			Result SetGlobalVariable(int index, float value);

			/// <summary>
			/// Set the volume of a sound category.
			/// </summary>
			/// <param name="category">Index of the category for which to set the volume, as returned by <see cref="GetCategory"/>.</param>
			/// <param name="volume">The volume level to set. This is a floating point scale where 0.0 means -96db, 1.0 means 0db and 2.0 means +6db.</param>
			Result SetVolume(int category, float volume);

			/// <summary>
			/// Shut down the XACT engine and free any resources in use.
			/// </summary>
			Result Shutdown();

			/// <summary>
			/// Stop a sound category.
			/// </summary>
			/// <param name="category">Index of the category to stop, as returned by <see cref="GetCategory"/>.</param>
			/// <param name="flags"><see cref="StopFlags"/> that specify how the cetegory is stopped.</param>
			Result Stop(int category, StopFlags flags);

			/// <summary>
			/// Gets the total number of audio rendering devices that are available in the system.
			/// </summary>
			property int RendererCount
			{
				int get();
			}

			/// <summary>
			/// Gets a <see cref="WaveFormatExtensible"/> object that represents the final mix format.
			/// </summary>
			property WaveFormatExtensible^ FinalMixFormat
			{
				WaveFormatExtensible^ get();
			}
		};
	}
}
