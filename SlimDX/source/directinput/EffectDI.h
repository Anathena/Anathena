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
#include "EffectParameters.h"

namespace SlimDX
{
	namespace DirectInput
	{
		ref class Device;

		public ref class Effect : ComObject
		{
			COMOBJECT(IDirectInputEffect, Effect);

		public:
			Effect( Device^ device, System::Guid guid );
			Effect( Device^ device, System::Guid guid, EffectParameters parameters );

			EffectParameters GetParameters();
			EffectParameters GetParameters( EffectParameterFlags flags );

			Result SetParameters( EffectParameters parameters );
			Result SetParameters( EffectParameters parameters, EffectParameterFlags flags );

			Result Start();
			Result Start( int iterations );
			Result Start( int iterations, EffectPlayFlags flags );
			Result Start( EffectPlayFlags flags );

			Result Stop();
			Result Unload();
			Result Download();

			array<System::Byte>^ Escape( int command, array<System::Byte>^ data, int outputSize );

			property EffectStatus Status
			{
				EffectStatus get();
			}

			property System::Guid Guid
			{
				System::Guid get();
			}
		};
	}
}