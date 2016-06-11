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

#include "ConstantDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool ConstantDescription::operator == ( ConstantDescription left, ConstantDescription right )
	{
		return ConstantDescription::Equals( left, right );
	}

	bool ConstantDescription::operator != ( ConstantDescription left, ConstantDescription right )
	{
		return !ConstantDescription::Equals( left, right );
	}

	int ConstantDescription::GetHashCode()
	{
		return Name->GetHashCode() + RegisterSet.GetHashCode() + RegisterIndex.GetHashCode()
			 + RegisterCount.GetHashCode() + Class.GetHashCode() + Type.GetHashCode()
			 + Rows.GetHashCode() + Columns.GetHashCode() + Elements.GetHashCode()
			 + StructMembers.GetHashCode() + Bytes.GetHashCode();
	}

	bool ConstantDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ConstantDescription>( value ) );
	}

	bool ConstantDescription::Equals( ConstantDescription value )
	{
		return ( Name == value.Name && RegisterSet == value.RegisterSet && RegisterIndex == value.RegisterIndex
			 && RegisterCount == value.RegisterCount && Class == value.Class && Type == value.Type
			 && Rows == value.Rows && Columns == value.Columns && Elements == value.Elements
			 && StructMembers == value.StructMembers && Bytes == value.Bytes );
	}

	bool ConstantDescription::Equals( ConstantDescription% value1, ConstantDescription% value2 )
	{
		return ( value1.Name == value2.Name && value1.RegisterSet == value2.RegisterSet && value1.RegisterIndex == value2.RegisterIndex
			 && value1.RegisterCount == value2.RegisterCount && value1.Class == value2.Class && value1.Type == value2.Type
			 && value1.Rows == value2.Rows && value1.Columns == value2.Columns && value1.Elements == value2.Elements
			 && value1.StructMembers == value2.StructMembers && value1.Bytes == value2.Bytes );
	}

	void ConstantDescription::Initialize( const D3DXCONSTANT_DESC& description )
	{
		Name = gcnew String( description.Name );
		RegisterSet = static_cast<SlimDX::Direct3D9::RegisterSet>( description.RegisterSet );
		RegisterIndex = description.RegisterIndex;
		RegisterCount = description.RegisterCount;
		Class = static_cast<ParameterClass>( description.Class );
		Type = static_cast<ParameterType>( description.Type );
		Rows = description.Rows;
		Columns = description.Columns;
		Elements = description.Elements;
		StructMembers = description.StructMembers;
		Bytes = description.Bytes;
	}
}
}