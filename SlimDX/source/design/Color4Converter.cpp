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
#include "../math/Color4.h"

#include "Color4Converter.h"
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
	Color4Converter::Color4Converter()
	{
		Type^ type = Color4::typeid;
		array<PropertyDescriptor^>^ propArray =
		{
			gcnew FieldPropertyDescriptor(type->GetField("Red")),
			gcnew FieldPropertyDescriptor(type->GetField("Green")),
			gcnew FieldPropertyDescriptor(type->GetField("Blue")),
			gcnew FieldPropertyDescriptor(type->GetField("Alpha")),
		};

		m_Properties = gcnew PropertyDescriptorCollection(propArray);
	}

	bool Color4Converter::CanConvertTo(ITypeDescriptorContext^ context, Type^ destinationType)
	{
		if( destinationType == String::typeid || destinationType == InstanceDescriptor::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertTo(context, destinationType);
	}

	bool Color4Converter::CanConvertFrom(ITypeDescriptorContext^ context, Type^ sourceType)
	{
		if( sourceType == String::typeid )
			return true;
		else
			return ExpandableObjectConverter::CanConvertFrom(context, sourceType);
	}

	Object^ Color4Converter::ConvertTo(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value, Type^ destinationType)
	{
		if( destinationType == nullptr )
			throw gcnew ArgumentNullException( "destinationType" );

		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		Color4^ color = dynamic_cast<Color4^>( value );

		if( destinationType == String::typeid && color != nullptr )
		{
			String^ separator = culture->TextInfo->ListSeparator + " ";
			TypeConverter^ converter = TypeDescriptor::GetConverter(float::typeid);
			array<String^>^ stringArray = gcnew array<String^>( 4 );

			stringArray[0] = converter->ConvertToString( context, culture, color->Alpha );
			stringArray[1] = converter->ConvertToString( context, culture, color->Red );
			stringArray[2] = converter->ConvertToString( context, culture, color->Green );
			stringArray[3] = converter->ConvertToString( context, culture, color->Blue );

			return String::Join( separator, stringArray );
		}
		else if( destinationType == InstanceDescriptor::typeid && color != nullptr )
		{
			ConstructorInfo^ info = (Color4::typeid)->GetConstructor( gcnew array<Type^> { float::typeid, float::typeid, float::typeid, float::typeid } );
			if( info != nullptr )
				return gcnew InstanceDescriptor( info, gcnew array<Object^> { color->Alpha, color->Red, color->Green, color->Blue } );
		}

		return ExpandableObjectConverter::ConvertTo(context, culture, value, destinationType);
	}

	Object^ Color4Converter::ConvertFrom(ITypeDescriptorContext^ context, CultureInfo^ culture, Object^ value)
	{
		if( culture == nullptr )
			culture = CultureInfo::CurrentCulture;

		String^ string = dynamic_cast<String^>( value );

		if( string != nullptr )
		{
			string = string->Trim();
			TypeConverter^ floatConverter = TypeDescriptor::GetConverter(float::typeid);
			array<String^>^ stringArray = string->Split( culture->TextInfo->ListSeparator[0] );

			if( stringArray->Length == 1 )
			{
				int number = 0;
				if( int::TryParse( string, number ) )
					return gcnew Color4( number );

				TypeConverter^ colorConverter = TypeDescriptor::GetConverter(Color::typeid);
				return gcnew Color4( safe_cast<Color>( colorConverter->ConvertFromString( context, culture, string ) ) );
			}
			else if( stringArray->Length == 3 )
			{
				int red;
				int green;
				int blue;
				if( int::TryParse( stringArray[0], red ) && int::TryParse( stringArray[1], green ) && int::TryParse( stringArray[2], blue ) )
					return gcnew Color4( Color::FromArgb( red, green, blue ) );

				float r = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[0] ) );
				float g = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[1] ) );
				float b = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[2] ) );

				return gcnew Color4( r, g, b );
			}
			else if( stringArray->Length == 4 )
			{
				int red;
				int green;
				int blue;
				int alpha;
				if( int::TryParse( stringArray[0], alpha ) && int::TryParse( stringArray[1], red ) && int::TryParse( stringArray[2], green ) && int::TryParse( stringArray[3], blue ) )
					return gcnew Color4( Color::FromArgb( alpha, red, green, blue ) );

				float a = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[0] ) );
				float r = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[1] ) );
				float g = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[2] ) );
				float b = safe_cast<float>( floatConverter->ConvertFromString( context, culture, stringArray[3] ) );

				return gcnew Color4( a, r, g, b );
			}
			else
				throw gcnew ArgumentException("Invalid color format.");
		}

		return ExpandableObjectConverter::ConvertFrom(context, culture, value);
	}

	bool Color4Converter::GetCreateInstanceSupported(ITypeDescriptorContext^ context)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		return true;
	}

	Object^ Color4Converter::CreateInstance(ITypeDescriptorContext^ context, IDictionary^ propertyValues)
	{
		SLIMDX_UNREFERENCED_PARAMETER(context);

		if( propertyValues == nullptr )
			throw gcnew ArgumentNullException( "propertyValues" );

		return gcnew Color4( safe_cast<float>( propertyValues["Alpha"] ), safe_cast<float>( propertyValues["Red"] ),
			safe_cast<float>( propertyValues["Green"] ), safe_cast<float>( propertyValues["Blue"] ) );
	}

	bool Color4Converter::GetPropertiesSupported(ITypeDescriptorContext^)
	{
		return true;
	}

	PropertyDescriptorCollection^ Color4Converter::GetProperties(ITypeDescriptorContext^, Object^, array<Attribute^>^)
	{
		return m_Properties;
	}
}
}