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

#include "../InternalHelpers.h"
#include "../math/Rational.h"

#include "RationalConverter.h"
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
	RationalConverter::RationalConverter()
	{
		Type^ type = Rational::typeid;
		array<PropertyDescriptor^>^ propArray =
		{
			gcnew FieldPropertyDescriptor(type->GetField("Numerator")),
			gcnew FieldPropertyDescriptor(type->GetField("Denominator")),
		};

		m_Properties = gcnew PropertyDescriptorCollection(propArray);
	}

	bool RationalConverter::CanConvertTo(ITypeDescriptorContext^ context, Type^ destinationType)
	{
		if( destinationType == String::typeid || destinationType == InstanceDescriptor::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertTo(context, destinationType);
	}

	bool RationalConverter::CanConvertFrom(ITypeDescriptorContext^ context, Type^ sourceType)
	{
		if( sourceType == String::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertFrom(context, sourceType);
	}

	Object^ RationalConverter::ConvertTo(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value, Type^ destinationType)
	{
		if( destinationType == nullptr )
			throw gcnew ArgumentNullException( "destinationType" );

		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		Rational^ rational = dynamic_cast<Rational^>( value );

		if( destinationType == String::typeid && rational != nullptr )
		{
			String^ separator = culture->TextInfo->ListSeparator + " ";
			TypeConverter^ converter = TypeDescriptor::GetConverter(int::typeid);
			array<String^>^ stringArray = gcnew array<String^>( 2 );

			stringArray[0] = converter->ConvertToString( context, culture, rational->Numerator );
			stringArray[1] = converter->ConvertToString( context, culture, rational->Denominator );

			return String::Join( separator, stringArray );
		}
		else if( destinationType == InstanceDescriptor::typeid && rational != nullptr )
		{
			ConstructorInfo^ info = (Rational::typeid)->GetConstructor( gcnew array<Type^> { int::typeid, int::typeid } );
			if( info != nullptr )
				return gcnew InstanceDescriptor( info, gcnew array<Object^> { rational->Numerator, rational->Denominator } );
		}

		return ExpandableObjectConverter::ConvertTo(context, culture, value, destinationType);
	}

	Object^ RationalConverter::ConvertFrom(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value)
	{
		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		String^ string = dynamic_cast<String^>( value );

		if( string != nullptr )
		{
			string = string->Trim();
			TypeConverter^ converter = TypeDescriptor::GetConverter(int::typeid);
			array<String^>^ stringArray = string->Split( culture->TextInfo->ListSeparator[0] );

			if( stringArray->Length != 2 )
				throw gcnew ArgumentException("Invalid rational format.");

			int Numerator = safe_cast<int>( converter->ConvertFromString( context, culture, stringArray[0] ) );
			int Denominator = safe_cast<int>( converter->ConvertFromString( context, culture, stringArray[1] ) );

			return gcnew Rational(Numerator, Denominator);
		}

		return ExpandableObjectConverter::ConvertFrom(context, culture, value);
	}

	bool RationalConverter::GetCreateInstanceSupported(ITypeDescriptorContext^ context)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		return true;
	}

	Object^ RationalConverter::CreateInstance(ITypeDescriptorContext^ context, IDictionary^ propertyValues)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		if( propertyValues == nullptr )
			throw gcnew ArgumentNullException( "propertyValues" );

		return gcnew Rational( safe_cast<int>( propertyValues["Numerator"] ), safe_cast<int>( propertyValues["Denominator"] ) );
	}

	bool RationalConverter::GetPropertiesSupported(ITypeDescriptorContext^)
	{
		return true;
	}

	PropertyDescriptorCollection^ RationalConverter::GetProperties(ITypeDescriptorContext^, Object^, array<Attribute^>^)
	{
		return m_Properties;
	}
}
}