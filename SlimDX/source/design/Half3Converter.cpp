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
#include <d3dx9.h>

#include "../InternalHelpers.h"
#include "../math/Half.h"
#include "../math/Half3.h"

#include "Half3Converter.h"
#include "FieldPropertyDescriptor.h"

using namespace System;
using namespace System::Collections;
using namespace System::Drawing;
using namespace System::ComponentModel;
using namespace System::ComponentModel::Design::Serialization;
using namespace System::Globalization;
using namespace System::Reflection;

namespace SlimDX
{
namespace Design
{
	Half3Converter::Half3Converter()
	{
		Type^ type = Half3::typeid;
		array<PropertyDescriptor^>^ propArray =
		{
			gcnew FieldPropertyDescriptor(type->GetField("X")),
			gcnew FieldPropertyDescriptor(type->GetField("Y")),
			gcnew FieldPropertyDescriptor(type->GetField("Z")),
		};

		m_Properties = gcnew PropertyDescriptorCollection(propArray);
	}

	bool Half3Converter::CanConvertTo(ITypeDescriptorContext^ context, Type^ destinationType)
	{
		if( destinationType == String::typeid || destinationType == InstanceDescriptor::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertTo(context, destinationType);
	}

	bool Half3Converter::CanConvertFrom(ITypeDescriptorContext^ context, Type^ sourceType)
	{
		if( sourceType == String::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertFrom(context, sourceType);
	}

	Object^ Half3Converter::ConvertTo(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value, Type^ destinationType)
	{
		if( destinationType == nullptr )
			throw gcnew ArgumentNullException( "destinationType" );

		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		Half3^ half = dynamic_cast<Half3^>( value );

		if( destinationType == String::typeid && half != nullptr )
		{
			String^ separator = culture->TextInfo->ListSeparator + " ";
			TypeConverter^ converter = TypeDescriptor::GetConverter(Half::typeid);
			array<String^>^ stringArray = gcnew array<String^>( 3 );

			stringArray[0] = converter->ConvertToString( context, culture, half->X );
			stringArray[1] = converter->ConvertToString( context, culture, half->Y );
			stringArray[2] = converter->ConvertToString( context, culture, half->Z );

			return String::Join( separator, stringArray );
		}
		else if( destinationType == InstanceDescriptor::typeid && half != nullptr )
		{
			ConstructorInfo^ info = (Half3::typeid)->GetConstructor( gcnew array<Type^> { Half::typeid, Half::typeid, Half::typeid } );
			if( info != nullptr )
				return gcnew InstanceDescriptor( info, gcnew array<Object^> { half->X, half->Y, half->Z } );
		}

		return ExpandableObjectConverter::ConvertTo(context, culture, value, destinationType);
	}

	Object^ Half3Converter::ConvertFrom(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value)
	{
		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		String^ string = dynamic_cast<String^>( value );

		if( string != nullptr )
		{
			string = string->Trim();
			TypeConverter^ converter = TypeDescriptor::GetConverter(Half::typeid);
			array<String^>^ stringArray = string->Split( culture->TextInfo->ListSeparator[0] );

			if( stringArray->Length != 3 )
				throw gcnew ArgumentException("Invalid half format.");

			Half X = safe_cast<Half>( converter->ConvertFromString( context, culture, stringArray[0] ) );
			Half Y = safe_cast<Half>( converter->ConvertFromString( context, culture, stringArray[1] ) );
			Half Z = safe_cast<Half>( converter->ConvertFromString( context, culture, stringArray[2] ) );

			return gcnew Half3(X, Y, Z);
		}

		return ExpandableObjectConverter::ConvertFrom(context, culture, value);
	}

	bool Half3Converter::GetCreateInstanceSupported(ITypeDescriptorContext^ context)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		return true;
	}

	Object^ Half3Converter::CreateInstance(ITypeDescriptorContext^ context, IDictionary^ propertyValues)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		if( propertyValues == nullptr )
			throw gcnew ArgumentNullException( "propertyValues" );

		return gcnew Half3( safe_cast<Half>( propertyValues["X"] ), safe_cast<Half>( propertyValues["Y"] ),
			safe_cast<Half>( propertyValues["Z"] ) );
	}

	bool Half3Converter::GetPropertiesSupported(ITypeDescriptorContext^)
	{
		return true;
	}

	PropertyDescriptorCollection^ Half3Converter::GetProperties(ITypeDescriptorContext^, Object^, array<Attribute^>^)
	{
		return m_Properties;
	}
}
}