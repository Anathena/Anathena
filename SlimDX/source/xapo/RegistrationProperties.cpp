
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

#include "../Utilities.h"

#include "RegistrationProperties.h"

using namespace System;

namespace SlimDX
{
namespace XAPO
{
	XAPO_REGISTRATION_PROPERTIES* RegistrationProperties::ToUnmanaged()
	{
		XAPO_REGISTRATION_PROPERTIES *props = new XAPO_REGISTRATION_PROPERTIES();
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( FriendlyName );
		pin_ptr<const wchar_t> pinnedCopyright = PtrToStringChars( CopyrightInfo );

		props->clsid = Utilities::ConvertManagedGuid( ClassId );
		wcsncpy_s( props->FriendlyName, pinnedName, XAPO_REGISTRATION_STRING_LENGTH ); props->FriendlyName[XAPO_REGISTRATION_STRING_LENGTH - 1] = NULL;
		wcsncpy_s( props->CopyrightInfo, pinnedCopyright, XAPO_REGISTRATION_STRING_LENGTH ); props->CopyrightInfo[XAPO_REGISTRATION_STRING_LENGTH - 1] = NULL;
		props->MajorVersion = MajorVersion;
		props->MinorVersion = MinorVersion;
		props->Flags = static_cast<UINT>( Flags );
		props->MinInputBufferCount = MinInputBufferCount;
		props->MaxInputBufferCount = MaxInputBufferCount;
		props->MinOutputBufferCount = MinOutputBufferCount;
		props->MaxOutputBufferCount = MaxOutputBufferCount;

		return props;
	}

	RegistrationProperties RegistrationProperties::FromUnmanaged( const XAPO_REGISTRATION_PROPERTIES &properties )
	{
		RegistrationProperties props;

		props.ClassId = Utilities::ConvertNativeGuid( properties.clsid );
		props.FriendlyName = gcnew String( properties.FriendlyName );
		props.CopyrightInfo = gcnew String( properties.CopyrightInfo );
		props.MajorVersion = properties.MajorVersion;
		props.MinorVersion = properties.MinorVersion;
		props.Flags = static_cast<PropertyFlags>( properties.Flags );
		props.MinInputBufferCount = properties.MinInputBufferCount;
		props.MaxInputBufferCount = properties.MaxInputBufferCount;
		props.MinOutputBufferCount = properties.MinOutputBufferCount;
		props.MaxOutputBufferCount = properties.MaxOutputBufferCount;

		return props;
	}

	bool RegistrationProperties::operator == ( RegistrationProperties left, RegistrationProperties right )
	{
		return RegistrationProperties::Equals( left, right );
	}

	bool RegistrationProperties::operator != ( RegistrationProperties left, RegistrationProperties right )
	{
		return !RegistrationProperties::Equals( left, right );
	}

	int RegistrationProperties::GetHashCode()
	{
		return ClassId.GetHashCode() + FriendlyName->GetHashCode() + CopyrightInfo->GetHashCode() + Flags.GetHashCode() + MajorVersion.GetHashCode() + MinorVersion.GetHashCode() +
			MinInputBufferCount.GetHashCode() + MaxInputBufferCount.GetHashCode() + MinOutputBufferCount.GetHashCode() + MaxOutputBufferCount.GetHashCode(); 
	}

	bool RegistrationProperties::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<RegistrationProperties>( value ) );
	}

	bool RegistrationProperties::Equals( RegistrationProperties value )
	{
		return ( ClassId == value.ClassId && FriendlyName == value.FriendlyName && CopyrightInfo == value.CopyrightInfo && Flags == value.Flags &&
			MajorVersion == value.MajorVersion && MinorVersion == value.MinorVersion && MinInputBufferCount == value.MinInputBufferCount && 
			MaxInputBufferCount == value.MaxInputBufferCount && MinOutputBufferCount == value.MinOutputBufferCount && MaxOutputBufferCount == value.MaxOutputBufferCount );
	}

	bool RegistrationProperties::Equals( RegistrationProperties% value1, RegistrationProperties% value2 )
	{
		return ( value1.ClassId == value2.ClassId && value1.FriendlyName == value2.FriendlyName && value1.CopyrightInfo == value2.CopyrightInfo && value1.Flags == value2.Flags &&
			value1.MajorVersion == value2.MajorVersion && value1.MinorVersion == value2.MinorVersion && value1.MinInputBufferCount == value2.MinInputBufferCount && 
			value1.MaxInputBufferCount == value2.MaxInputBufferCount && value1.MinOutputBufferCount == value2.MinOutputBufferCount && value1.MaxOutputBufferCount == value2.MaxOutputBufferCount );
	}
}
}