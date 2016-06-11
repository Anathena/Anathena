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

#include <windows.h>
#include <x3daudio.h>

#include "Cone.h"

using namespace System;

namespace SlimDX
{
namespace X3DAudio
{
	X3DAUDIO_CONE Cone::ToUnmanaged()
	{
		X3DAUDIO_CONE result;

		result.InnerAngle = InnerAngle;
		result.OuterAngle = OuterAngle;
		result.InnerVolume = InnerVolume;
		result.OuterVolume = OuterVolume;
		result.InnerLPF = InnerLpf;
		result.OuterLPF = OuterLpf;
		result.InnerReverb = InnerReverb;
		result.OuterReverb = OuterReverb;

		return result;
	}

	Cone^ Cone::Clone()
	{
		Cone^ result = gcnew Cone();

		result->InnerAngle = InnerAngle;
		result->OuterAngle = OuterAngle;
		result->InnerVolume = InnerVolume;
		result->OuterVolume = OuterVolume;
		result->InnerLpf = InnerLpf;
		result->OuterLpf = OuterLpf;
		result->InnerReverb = InnerReverb;
		result->OuterReverb = OuterReverb;

		return result;
	}

	bool Cone::operator == ( Cone^ left, Cone^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return Cone::Equals( left, right );
	}

	bool Cone::operator != ( Cone^ left, Cone^ right )
	{
		return !( left == right );
	}

	int Cone::GetHashCode()
	{
		return InnerAngle.GetHashCode() + OuterAngle.GetHashCode() + InnerVolume.GetHashCode() + 
			OuterVolume.GetHashCode() + InnerLpf.GetHashCode() + OuterLpf.GetHashCode() + 
			InnerReverb.GetHashCode() + OuterReverb.GetHashCode();
	}

	bool Cone::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Cone^>( value ) );
	}

	bool Cone::Equals( Cone^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( InnerAngle == value->InnerAngle && OuterAngle == value->OuterAngle && 
			InnerVolume == value->InnerVolume && OuterVolume == value->OuterVolume && 
			InnerLpf == value->InnerLpf && OuterLpf == value->OuterLpf && 
			InnerReverb == value->InnerReverb && OuterReverb == value->OuterReverb );
	}

	bool Cone::Equals( Cone^ value1, Cone^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}