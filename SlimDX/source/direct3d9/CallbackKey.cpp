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
#include <vcclr.h>

#include "../ComObject.h"
#include "../Math/Vector3.h"
#include "../DataStream.h"

#include "CallbackKey.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	CallbackKey::CallbackKey( const D3DXKEY_CALLBACK &key )
	{
		Time = key.Time;
		Data = IntPtr( key.pCallbackData );
	}

	bool CallbackKey::operator == ( CallbackKey left, CallbackKey right )
	{
		return CallbackKey::Equals( left, right );
	}

	bool CallbackKey::operator != ( CallbackKey left, CallbackKey right )
	{
		return !CallbackKey::Equals( left, right );
	}

	int CallbackKey::GetHashCode()
	{
		return Time.GetHashCode() + Data.GetHashCode();
	}

	bool CallbackKey::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<CallbackKey>( value ) );
	}

	bool CallbackKey::Equals( CallbackKey value )
	{
		return ( Time == value.Time && Data == value.Data );
	}

	bool CallbackKey::Equals( CallbackKey% value1, CallbackKey% value2 )
	{
		return ( value1.Time == value2.Time && value1.Data == value2.Data );
	}

	RotationKey::RotationKey( const D3DXKEY_QUATERNION &key )
	{
		Time = key.Time;
		Value = Quaternion( key.Value.x, key.Value.y, key.Value.z, key.Value.w );
	}

	bool RotationKey::operator == ( RotationKey left, RotationKey right )
	{
		return RotationKey::Equals( left, right );
	}

	bool RotationKey::operator != ( RotationKey left, RotationKey right )
	{
		return !RotationKey::Equals( left, right );
	}

	int RotationKey::GetHashCode()
	{
		return Time.GetHashCode() + Value.GetHashCode();
	}

	bool RotationKey::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RotationKey>( value ) );
	}

	bool RotationKey::Equals( RotationKey value )
	{
		return ( Time == value.Time && Value == value.Value );
	}

	bool RotationKey::Equals( RotationKey% value1, RotationKey% value2 )
	{
		return ( value1.Time == value2.Time && value1.Value == value2.Value );
	}

	ScaleKey::ScaleKey( const D3DXKEY_VECTOR3 &key )
	{
		Time = key.Time;
		Value = Vector3( key.Value.x, key.Value.y, key.Value.z );
	}

	bool ScaleKey::operator == ( ScaleKey left, ScaleKey right )
	{
		return ScaleKey::Equals( left, right );
	}

	bool ScaleKey::operator != ( ScaleKey left, ScaleKey right )
	{
		return !ScaleKey::Equals( left, right );
	}

	int ScaleKey::GetHashCode()
	{
		return Time.GetHashCode() + Value.GetHashCode();
	}

	bool ScaleKey::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ScaleKey>( value ) );
	}

	bool ScaleKey::Equals( ScaleKey value )
	{
		return ( Time == value.Time && Value == value.Value );
	}

	bool ScaleKey::Equals( ScaleKey% value1, ScaleKey% value2 )
	{
		return ( value1.Time == value2.Time && value1.Value == value2.Value );
	}

	TranslationKey::TranslationKey( const D3DXKEY_VECTOR3 &key )
	{
		Time = key.Time;
		Value = Vector3( key.Value.x, key.Value.y, key.Value.z );
	}

	bool TranslationKey::operator == ( TranslationKey left, TranslationKey right )
	{
		return TranslationKey::Equals( left, right );
	}

	bool TranslationKey::operator != ( TranslationKey left, TranslationKey right )
	{
		return !TranslationKey::Equals( left, right );
	}

	int TranslationKey::GetHashCode()
	{
		return Time.GetHashCode() + Value.GetHashCode();
	}

	bool TranslationKey::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<TranslationKey>( value ) );
	}

	bool TranslationKey::Equals( TranslationKey value )
	{
		return ( Time == value.Time && Value == value.Value );
	}

	bool TranslationKey::Equals( TranslationKey% value1, TranslationKey% value2 )
	{
		return ( value1.Time == value2.Time && value1.Value == value2.Value );
	}
}
}