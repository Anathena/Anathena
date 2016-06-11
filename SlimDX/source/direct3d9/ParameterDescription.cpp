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

#include "ParameterDescription.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool ParameterDescription::operator == ( ParameterDescription left, ParameterDescription right )
	{
		return ParameterDescription::Equals( left, right );
	}

	bool ParameterDescription::operator != ( ParameterDescription left, ParameterDescription right )
	{
		return !ParameterDescription::Equals( left, right );
	}

	int ParameterDescription::GetHashCode()
	{
		return Name->GetHashCode() + Semantic->GetHashCode() + Class.GetHashCode()
			 + Type.GetHashCode() + Rows.GetHashCode() + Columns.GetHashCode()
			 + Elements.GetHashCode() + Annotations.GetHashCode() + StructMembers.GetHashCode()
			 + Flags.GetHashCode() + Bytes.GetHashCode();
	}

	bool ParameterDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ParameterDescription>( value ) );
	}

	bool ParameterDescription::Equals( ParameterDescription value )
	{
		return ( Name == value.Name && Semantic == value.Semantic && Class == value.Class
			 && Type == value.Type && Rows == value.Rows && Columns == value.Columns
			 && Elements == value.Elements && Annotations == value.Annotations && StructMembers == value.StructMembers
			 && Flags == value.Flags && Bytes == value.Bytes );
	}

	bool ParameterDescription::Equals( ParameterDescription% value1, ParameterDescription% value2 )
	{
		return ( value1.Name == value2.Name && value1.Semantic == value2.Semantic && value1.Class == value2.Class
			 && value1.Type == value2.Type && value1.Rows == value2.Rows && value1.Columns == value2.Columns
			 && value1.Elements == value2.Elements && value1.Annotations == value2.Annotations && value1.StructMembers == value2.StructMembers
			 && value1.Flags == value2.Flags && value1.Bytes == value2.Bytes );
	}
}
}