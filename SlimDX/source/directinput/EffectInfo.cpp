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
#include "stdafx.h"
#include <dinput.h>

#include "../Utilities.h"

#include "EffectInfo.h"

using namespace System;

namespace SlimDX
{
namespace DirectInput
{
	EffectInfo::EffectInfo( const DIEFFECTINFO &info )
	{
		Guid = Utilities::ConvertNativeGuid( info.guid );
		Type = static_cast<EffectType>( info.dwEffType );
		StaticParameters = static_cast<EffectParameterFlags>( info.dwStaticParams );
		DynamicParameters = static_cast<EffectParameterFlags>( info.dwDynamicParams );
		Name = gcnew String( info.tszName );
	}

	bool EffectInfo::operator == ( EffectInfo left, EffectInfo right )
	{
		return EffectInfo::Equals( left, right );
	}

	bool EffectInfo::operator != ( EffectInfo left, EffectInfo right )
	{
		return !EffectInfo::Equals( left, right );
	}

	int EffectInfo::GetHashCode()
	{
		return Guid.GetHashCode() + StaticParameters.GetHashCode() + DynamicParameters.GetHashCode()
			 + Type.GetHashCode() + Name->GetHashCode();
	}

	bool EffectInfo::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectInfo>( value ) );
	}

	bool EffectInfo::Equals( EffectInfo value )
	{
		return ( Guid == value.Guid && StaticParameters == value.StaticParameters && DynamicParameters == value.DynamicParameters
			 && Type == value.Type && Name == value.Name );
	}

	bool EffectInfo::Equals( EffectInfo% value1, EffectInfo% value2 )
	{
		return ( value1.Guid == value2.Guid && value1.StaticParameters == value2.StaticParameters && value1.DynamicParameters == value2.DynamicParameters
			 && value1.Type == value2.Type && value1.Name == value2.Name );
	}
}
}