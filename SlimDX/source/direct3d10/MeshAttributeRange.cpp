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

#include "MeshAttributeRange.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	MeshAttributeRange::MeshAttributeRange( const D3DX10_ATTRIBUTE_RANGE& native )
	{
		m_AttribId = native.AttribId;
		m_FaceStart = native.FaceStart;
		m_FaceCount = native.FaceCount;
		m_VertexStart = native.VertexStart;
		m_VertexCount = native.VertexCount;
	}

	D3DX10_ATTRIBUTE_RANGE MeshAttributeRange::CreateNativeVersion()
	{
		D3DX10_ATTRIBUTE_RANGE native;
		native.AttribId = m_AttribId;
		native.FaceStart = m_FaceStart;
		native.FaceCount = m_FaceCount;
		native.VertexStart = m_VertexStart;
		native.VertexCount = m_VertexCount;
		return native;
	}

	int MeshAttributeRange::Id::get()
	{
		return m_AttribId;
	}
	
	void MeshAttributeRange::Id::set( int value )
	{
		m_AttribId = value;
	}
	
	int MeshAttributeRange::FaceStart::get()
	{
		return m_FaceStart;
	}
	
	void MeshAttributeRange::FaceStart::set( int value )
	{
		m_FaceStart = value;
	}

	int MeshAttributeRange::FaceCount::get()
	{
		return m_FaceCount;
	}
	
	void MeshAttributeRange::FaceCount::set( int value )
	{
		m_FaceCount = value;
	}

	int MeshAttributeRange::VertexStart::get()
	{
		return m_VertexStart;
	}
	
	void MeshAttributeRange::VertexStart::set( int value )
	{
		m_VertexStart = value;
	}

	int MeshAttributeRange::VertexCount::get()
	{
		return m_VertexCount;
	}
	
	void MeshAttributeRange::VertexCount::set( int value )
	{
		m_VertexCount = value;
	}

	bool MeshAttributeRange::operator == ( MeshAttributeRange left, MeshAttributeRange right )
	{
		return MeshAttributeRange::Equals( left, right );
	}

	bool MeshAttributeRange::operator != ( MeshAttributeRange left, MeshAttributeRange right )
	{
		return !MeshAttributeRange::Equals( left, right );
	}

	int MeshAttributeRange::GetHashCode()
	{
		return m_AttribId.GetHashCode() + m_FaceStart.GetHashCode() + m_FaceCount.GetHashCode() + m_VertexStart.GetHashCode() + m_VertexCount.GetHashCode();
	}

	bool MeshAttributeRange::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<MeshAttributeRange>( value ) );
	}

	bool MeshAttributeRange::Equals( MeshAttributeRange value )
	{
		return ( m_AttribId == value.m_AttribId && m_FaceStart == value.m_FaceStart && m_FaceCount == value.m_FaceCount && m_VertexStart == value.m_VertexStart && m_VertexCount == value.m_VertexCount );
	}

	bool MeshAttributeRange::Equals( MeshAttributeRange% value1, MeshAttributeRange% value2 )
	{
		return ( value1.m_AttribId == value2.m_AttribId && value1.m_FaceStart == value2.m_FaceStart && value1.m_FaceCount == value2.m_FaceCount && value1.m_VertexStart == value2.m_VertexStart && value1.m_VertexCount == value2.m_VertexCount );
	}
}
}
