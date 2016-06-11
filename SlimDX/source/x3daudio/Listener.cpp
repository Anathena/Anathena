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

#include "Listener.h"

using namespace System;

namespace SlimDX
{
namespace X3DAudio
{
	X3DAUDIO_VECTOR Vector3ToX3DAudio( Vector3 value )
	{
		X3DAUDIO_VECTOR result;
		
		result.x = value.X;
		result.y = value.Y;
		result.z = value.Z;

		return result;
	}

	X3DAUDIO_LISTENER Listener::ToUnmanaged()
	{
		X3DAUDIO_LISTENER result;

		result.OrientFront = Vector3ToX3DAudio( OrientFront );
		result.OrientTop = Vector3ToX3DAudio( OrientTop );
		result.Position = Vector3ToX3DAudio( Position );
		result.Velocity = Vector3ToX3DAudio( Velocity );

		return result;
	}

	Listener^ Listener::Clone()
	{
		Listener^ result = gcnew Listener();

		result->OrientFront = OrientFront;
		result->OrientTop = OrientTop;
		result->Position = Position;
		result->Velocity = Velocity;
		result->Cone = Cone;

		return result;
	}

	bool Listener::operator == ( Listener^ left, Listener^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return Listener::Equals( left, right );
	}

	bool Listener::operator != ( Listener^ left, Listener^ right )
	{
		return !( left == right );
	}

	int Listener::GetHashCode()
	{
		return OrientFront.GetHashCode() + OrientTop.GetHashCode() + Position.GetHashCode() + 
			Velocity.GetHashCode() + Cone->GetHashCode();
	}

	bool Listener::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Listener^>( value ) );
	}

	bool Listener::Equals( Listener^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( OrientFront == value->OrientFront && OrientTop == value->OrientTop && 
			Position == value->Position && Velocity == value->Velocity && Cone == value->Cone );
	}

	bool Listener::Equals( Listener^ value1, Listener^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}