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
		/// The properties for the variation that is currently active in a cue instance.
		/// </summary>
		/// <unmanaged>XACT_VARIATION_PROPERTIES</unmanaged>
		public value class VariationProperties
		{
		private:
			int index;
			int weight;
			float variableMin;
			float variableMax;
			bool shouldLinger;

		internal:
			VariationProperties(const XACT_VARIATION_PROPERTIES& data);

		public:
			/// <summary>
			/// Gets the index of the variation in the parent variation list.
			/// </summary>
			property int Index
			{
				int get() { return index; }
			}

			/// <summary>
			/// Gets the weight that is assigned to the variation. For non-interactive variations only.
			/// </summary>
			property int Weight
			{
				int get() { return weight; }
			}

			/// <summary>
			/// Gets the lower bound of the interactive variable that controls the variation.
			/// </summary>
			property float VariableMin
			{
				float get() { return variableMin; }
			}

			/// <summary>
			/// Gets the upper bound of the interactive variable that controls the variation.
			/// </summary>
			property float VariableMax
			{
				float get() { return variableMax; }
			}

			/// <summary>
			/// Gets a value that indicates if the variation should linger.
			/// </summary>
			property bool ShouldLinger
			{
				bool get() { return shouldLinger; }
			}
		};
	}
}
