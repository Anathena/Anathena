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

#include "Engine.h"
#include "Enums.h"
#include "CueInstanceProperties.h"

namespace SlimDX
{
	namespace XACT3
	{
		public ref class Cue
		{
		private:
			IXACT3Cue* InternalPointer;

		internal:
			Cue( IXACT3Cue *cue );

		public:
			Result Destroy();

			/// <summary>
			/// Get the value for a variable.
			/// </summary>
			/// <param name="index">The index of the variable.</param>
			/// <returns>The value that is currently held by the variable.</returns>
			float GetVariable(int index);

			/// <summary>
			/// Get the corresponding index for a variable name string.
			/// </summary>
			/// <param name="friendlyName">A string that contains the friendly name of the variable.</param>
			/// <returns>The index for the variable if it exists, otherwise -1.</returns>
			int GetVariableIndex(System::String^ friendlyName);

			/// <summary>
			/// Pause playback of the cue.
			/// </summary>
			/// <param name="pause">The action to take. If true, playback is paused, if false, playback is resumed.</param>
			Result Pause(bool pause);

			/// <summary>
			/// Play the cue.
			/// </summary>
			Result Play();

			Result SetMatrixCoefficients(int sourceChannelCount, int destinationChannelCount, array<float>^ pMatrixCoefficients);

			/// <summary>
			/// Set the value of a variable.
			/// </summary>
			/// <param name="index">The index of the variable.</param>
			/// <param name="value">The new value of the variable.</param>
			Result SetVariable(int index, float value);

			/// <summary>
			/// Stop the cue.
			/// </summary>
			/// <param name="flags"><see cref="StopFlags"/> that specify how the cue is stopped.</param>
			Result Stop(StopFlags flags);

			/// <summary>
			/// Gets a <see cref="CueInstanceProperties"/> object that contains the properties of the cue instance.
			/// </summary>
			property CueInstanceProperties Properties
			{
				CueInstanceProperties get();
			}

			/// <summary>
			/// Get a <see cref="CueState"/> value representing the current state of the cue.
			/// </summary>
			property CueState State
			{
				CueState get();
			}
		};
	}
}
