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

#include "Asserts.h"

using namespace testing;
using namespace System;
using namespace SlimDX;

TEST( DatastreamTests, ConstructFromAllocation )
{
	DataStream^ stream = nullptr;
	ASSERT_NO_THROW( stream = gcnew DataStream( 32, true, true ) );
	
	delete stream;
}

TEST( DatastreamTests, ConstructFromAllocationTooSmall )
{
	DataStream^ stream = nullptr;
	ASSERT_MANAGED_THROW( stream = gcnew DataStream( 0, true, true ), ArgumentOutOfRangeException );
}

TEST( DatastreamTests, ConstructFromManagedArrayOfByte )
{
	array<Byte>^ byteArray = gcnew array<Byte>( 64 );
	DataStream^ stream = gcnew DataStream( byteArray, true, true );
	delete stream;
}

TEST( DatastreamTests, ManagedArrayBackingReadBytesFromStart )
{
	array<Byte>^ byteArray = gcnew array<Byte>( 16 );
	for( int byteIndex = 0; byteIndex < byteArray->Length; ++byteIndex )
	{
		byteArray[byteIndex] = byteIndex;	
	}
	
	DataStream^ stream = gcnew DataStream( byteArray, true, true );
	for( int byteIndex = 0; byteIndex < byteArray->Length; ++byteIndex )
	{
		ASSERT_EQ( byteIndex, stream->ReadByte() );
	}
	
	// Should be at the end of the stream now.
	ASSERT_EQ( -1, stream->ReadByte() );
	delete stream;
}
