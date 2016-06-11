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

#include "Viewport10.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D10
{
	Viewport::Viewport( int x, int y, int width, int height )
	: m_X( x ), m_Y( y ), m_Width( width ), m_Height( height ),
	  m_MinZ( 0.0f ), m_MaxZ( 1.0f )
	{
	}
	
	Viewport::Viewport( int x, int y, int width, int height, float minZ, float maxZ )
	: m_X( x ), m_Y( y ), m_Width( width ), m_Height( height ),
	  m_MinZ( minZ ), m_MaxZ( maxZ )
	{
	}

	int Viewport::X::get()
	{
		return m_X;
	}
	
	void Viewport::X::set( int value )
	{
		m_X = value;
	}
	
	int Viewport::Y::get()
	{
		return m_Y;
	}
	
	void Viewport::Y::set( int value )
	{
		m_Y = value;
	}
	
	int Viewport::Width::get()
	{
		return m_Width;
	}
	
	void Viewport::Width::set( int value )
	{
		m_Width = value;
	}
	
	int Viewport::Height::get()
	{
		return m_Height;
	}
	
	void Viewport::Height::set( int value )
	{
		m_Height = value;
	}
	
	float Viewport::MinZ::get()
	{
		return m_MinZ;
	}
	
	void Viewport::MinZ::set( float value )
	{
		m_MinZ = value;
	}
	
	float Viewport::MaxZ::get()
	{
		return m_MaxZ;
	}
	
	void Viewport::MaxZ::set( float value )
	{
		m_MaxZ = value;
	}

	bool Viewport::operator == ( Viewport left, Viewport right )
	{
		return Viewport::Equals( left, right );
	}

	bool Viewport::operator != ( Viewport left, Viewport right )
	{
		return !Viewport::Equals( left, right );
	}

	String^ Viewport::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "X:{0} Y:{1} Width:{2} Height:{3} MinZ:{4} MaxZ:{5}",
			X, Y, Width, Height, MinZ, MaxZ );
	}

	int Viewport::GetHashCode()
	{
		return m_X.GetHashCode() + m_Y.GetHashCode() + m_Width.GetHashCode()
			 + m_Height.GetHashCode() + m_MinZ.GetHashCode() + m_MaxZ.GetHashCode();
	}

	bool Viewport::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Viewport>( value ) );
	}

	bool Viewport::Equals( Viewport value )
	{
		return ( m_X == value.m_X && m_Y == value.m_Y && m_Width == value.m_Width
			 && m_Height == value.m_Height && m_MinZ == value.m_MinZ && m_MaxZ == value.m_MaxZ );
	}

	bool Viewport::Equals( Viewport% value1, Viewport% value2 )
	{
		return ( value1.m_X == value2.m_X && value1.m_Y == value2.m_Y && value1.m_Width == value2.m_Width
			 && value1.m_Height == value2.m_Height && value1.m_MinZ == value2.m_MinZ && value1.m_MaxZ == value2.m_MaxZ );
	}
}
}