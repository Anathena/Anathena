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
		/// The instance independent properties of a cue.
		/// </summary>
		/// <unmanaged>XACT_CUE_PROPERTIES</unmanaged>
		public value class CueProperties
		{
		private:
			System::String^ name;
			bool isInteractive;
			int variableIndex;
			int variations;
			int maxInstances;
			int currentInstances;

		internal:
			CueProperties(const XACT_CUE_PROPERTIES& cueProperties);

		public:
			/// <summary>
			/// Gets the friendly name of the cue.
			/// </summary>
			property System::String^ FriendlyName
			{
				System::String^ get() { return name; }
			}

			/// <summary>
			/// Gets a value that is true if the cue is an interactive audio cue, otherwise false.
			/// </summary>
			property bool IsInteractive
			{
				bool get() { return isInteractive; }
			}

			/// <summary>
			/// Gets the index of the variable to control the cue. This is only relevant for interactive audio cues.
			/// </summary>
			property int VariableIndex
			{
				int get() { return variableIndex; }
			}

			/// <summary>
			/// Gets the number of sound variations for this cue.
			/// </summary>
			property int VariationCount
			{
				int get() { return variations; }
			}

			/// <summary>
			/// Gets the maximum number of instances that are allowed for this cue.
			/// </summary>
			property int MaxInstances
			{
				int get() { return maxInstances; }
			}

			/// <summary>
			/// Gets the number of currently active instances of this cue.
			/// </summary>
			property int CurrentInstances
			{
				int get() { return currentInstances; }
			}
		};
	}
}
