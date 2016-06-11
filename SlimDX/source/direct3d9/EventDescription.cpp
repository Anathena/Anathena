#include "stdafx.h"
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

#include <d3d9.h>
#include <d3dx9.h>

#include "EventDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool EventDescription::operator == ( EventDescription left, EventDescription right )
	{
		return EventDescription::Equals( left, right );
	}

	bool EventDescription::operator != ( EventDescription left, EventDescription right )
	{
		return !EventDescription::Equals( left, right );
	}

	int EventDescription::GetHashCode()
	{
		return Type.GetHashCode() + Track.GetHashCode() + StartTime.GetHashCode()
			 + Duration.GetHashCode() + Transition.GetHashCode() + Weight.GetHashCode()
			 + Speed.GetHashCode() + Position.GetHashCode() + Enabled.GetHashCode();
	}

	bool EventDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EventDescription>( value ) );
	}

	bool EventDescription::Equals( EventDescription value )
	{
		return ( Type == value.Type && Track == value.Track && StartTime == value.StartTime
			 && Duration == value.Duration && Transition == value.Transition && Weight == value.Weight
			 && Speed == value.Speed && Position == value.Position && Enabled == value.Enabled );
	}

	bool EventDescription::Equals( EventDescription% value1, EventDescription% value2 )
	{
		return ( value1.Type == value2.Type && value1.Track == value2.Track && value1.StartTime == value2.StartTime
			 && value1.Duration == value2.Duration && value1.Transition == value2.Transition && value1.Weight == value2.Weight
			 && value1.Speed == value2.Speed && value1.Position == value2.Position && value1.Enabled == value2.Enabled );
	}
}
}