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

#include "DataBox.h"

using namespace System;

namespace SlimDX
{
	DataBox::DataBox( int rowPitch, int slicePitch, DataStream^ data )
	: m_RowPitch( rowPitch), m_SlicePitch( slicePitch ), m_Data( data )
	{
		if( data == nullptr )
			throw gcnew ArgumentNullException( "data" );
			
		if( rowPitch < 0 )
			throw gcnew ArgumentOutOfRangeException( "rowPitch", "Row pitch cannot be negative." );
		
		if( slicePitch < 0 )
			throw gcnew ArgumentOutOfRangeException( "slicePitch", "Slice pitch cannot be negative." );
	}
	
	int DataBox::RowPitch::get()
	{
		return m_RowPitch;
	}
	
	void DataBox::RowPitch::set( int value )
	{
		if( value < 0 )
			throw gcnew ArgumentOutOfRangeException( "value", "Row pitch cannot be negative." );
		m_RowPitch = value;
	}
	
	int DataBox::SlicePitch::get()
	{
		return m_SlicePitch;
	}
	
	void DataBox::SlicePitch::set( int value )
	{
		if( value < 0 )
			throw gcnew ArgumentOutOfRangeException( "value", "Slice pitch cannot be negative." );
		m_SlicePitch = value;
	}
	
	DataStream^ DataBox::Data::get()
	{
		return m_Data;
	}
}
