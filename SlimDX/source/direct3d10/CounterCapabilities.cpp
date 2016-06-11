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

#include <d3d10.h>

#include "CounterCapabilities.h"

namespace SlimDX
{
namespace Direct3D10
{ 
	CounterCapabilities::CounterCapabilities( const D3D10_COUNTER_INFO& native )
	{
		m_LastDeviceDependentCounter = static_cast<int>( native.LastDeviceDependentCounter );
		m_NumSimultaneousCounters = native.NumSimultaneousCounters;
		m_NumDetectableParallelUnits = native.NumDetectableParallelUnits;
	}
	
	int CounterCapabilities::LastDeviceDependantCounter::get()
	{
		return m_LastDeviceDependentCounter;
	}
	
	int CounterCapabilities::MaximumSimultaneousCounters::get()
	{
		return m_NumSimultaneousCounters;
	}
	
	int CounterCapabilities::MaximumParallelUnits::get()
	{
		return m_NumDetectableParallelUnits;
	}

	bool CounterCapabilities::operator == ( CounterCapabilities left, CounterCapabilities right )
	{
		return CounterCapabilities::Equals( left, right );
	}

	bool CounterCapabilities::operator != ( CounterCapabilities left, CounterCapabilities right )
	{
		return !CounterCapabilities::Equals( left, right );
	}

	int CounterCapabilities::GetHashCode()
	{
		return m_LastDeviceDependentCounter.GetHashCode() + m_NumSimultaneousCounters.GetHashCode() + m_NumDetectableParallelUnits.GetHashCode();
	}

	bool CounterCapabilities::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CounterCapabilities>( value ) );
	}

	bool CounterCapabilities::Equals( CounterCapabilities value )
	{
		return ( m_LastDeviceDependentCounter == value.m_LastDeviceDependentCounter && m_NumSimultaneousCounters == value.m_NumSimultaneousCounters && m_NumDetectableParallelUnits == value.m_NumDetectableParallelUnits );
	}

	bool CounterCapabilities::Equals( CounterCapabilities% value1, CounterCapabilities% value2 )
	{
		return ( value1.m_LastDeviceDependentCounter == value2.m_LastDeviceDependentCounter && value1.m_NumSimultaneousCounters == value2.m_NumSimultaneousCounters && value1.m_NumDetectableParallelUnits == value2.m_NumDetectableParallelUnits );
	}
}
}
