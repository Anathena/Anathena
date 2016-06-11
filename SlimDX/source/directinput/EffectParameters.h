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
#include "Envelope.h"
#include "TypeSpecificParameters.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace DirectInput
	{
		public value class EffectParameters
		{
		private:
			array<int>^ axes;
			array<int>^ directions;

		internal:
			DIEFFECT ToUnmanaged();
			void Cleanup( const DIEFFECT &effect );

			EffectParameters( const DIEFFECT &effect );

		public:
			property EffectFlags Flags;
			property int Duration;
			property int SamplePeriod;
			property int Gain;
			property int TriggerButton;
			property int TriggerRepeatInterval;
			property int StartDelay;
			property System::Nullable<Envelope> Envelope;
			property TypeSpecificParameters^ Parameters;

			void SetAxes( array<int>^ axes, array<int>^ directions );
			void GetAxes( [Out] array<int>^ %axes, [Out] array<int>^ %directions );
		};
	}
}