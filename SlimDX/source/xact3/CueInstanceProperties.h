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

#include "CueProperties.h"
#include "SoundVariationProperties.h"

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// The properties of an active cue instance.
		/// </summary>
		/// <unmanaged>XACT_CUE_INSTANCE_PROPERTIES</unmanaged>
		public value class CueInstanceProperties
		{
		private:
			SlimDX::XACT3::CueProperties cueProperties;
			SoundVariationProperties activeVariationProperties;

		internal:
			CueInstanceProperties(const XACT_CUE_INSTANCE_PROPERTIES& data);

		public:
			/// <summary>
			/// Gets the properties of a cue that are shared by all instances of the cue.
			/// </summary>
			property SlimDX::XACT3::CueProperties CueProperties
			{
				SlimDX::XACT3::CueProperties get() { return cueProperties; }
			}

			/// <summary>
			/// Gets the properties the current cue variation.
			/// </summary>
			property SoundVariationProperties ActiveVariationProperties
			{
				SoundVariationProperties get() { return activeVariationProperties; }
			}
		};
	}
}
