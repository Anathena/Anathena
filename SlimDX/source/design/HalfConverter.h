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
#pragma once

using namespace System::ComponentModel;

namespace SlimDX
{
	value class Half;

	namespace Design
	{
		/// <summary>
		/// Provides a type converter to convert <see cref="Half"/> objects to and from various 
		/// other representations.
		/// </summary>
		public ref class HalfConverter : System::ComponentModel::ExpandableObjectConverter
		{
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="HalfConverter"/> class.
			/// </summary>
			HalfConverter() { }

			/// <summary>
			/// Returns whether this converter can convert the object to the specified type, using the specified context.
			/// </summary>
			/// <param name="context">A <see cref="System::ComponentModel::ITypeDescriptorContext"/> that provides a format context.</param>
			/// <param name="destinationType">A <see cref="System::Type"/> that represents the type you want to convert to.</param>
			/// <returns><c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
			virtual bool CanConvertTo(System::ComponentModel::ITypeDescriptorContext^ context, 
				System::Type^ destinationType) override;

			/// <summary>
			/// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
			/// </summary>
			/// <param name="context">A <see cref="System::ComponentModel::ITypeDescriptorContext"/> that provides a format context.</param>
			/// <param name="sourceType">A System::Type that represents the type you want to convert from.</param>
			/// <returns><c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
			virtual bool CanConvertFrom(System::ComponentModel::ITypeDescriptorContext^ context, 
				System::Type^ sourceType) override;

			/// <summary>
			/// Converts the given value object to the specified type, using the specified context and culture information.
			/// </summary>
			/// <param name="context">A <see cref="System::ComponentModel::ITypeDescriptorContext"/> that provides a format context.</param>
			/// <param name="culture">A <see cref="System::Globalization::CultureInfo"/>. If <c>null</c> is passed, the current culture is assumed.</param>
			/// <param name="value">The <see cref="System::Object"/> to convert.</param>
			/// <param name="destinationType">A <see cref="System::Type"/> that represents the type you want to convert to.</param>
			/// <returns>An <see cref="System::Object"/> that represents the converted value.</returns>
			virtual System::Object^ ConvertTo(System::ComponentModel::ITypeDescriptorContext^ context, 
				System::Globalization::CultureInfo^ culture, System::Object^ value, System::Type^ destinationType) override;

			/// <summary>
			/// Converts the given object to the type of this converter, using the specified context and culture information.
			/// </summary>
			/// <param name="context">A <see cref="System::ComponentModel::ITypeDescriptorContext"/> that provides a format context.</param>
			/// <param name="culture">A <see cref="System::Globalization::CultureInfo"/>. If <c>null</c> is passed, the current culture is assumed.</param>
			/// <param name="value">The <see cref="System::Object"/> to convert.</param>
			/// <returns>An <see cref="System::Object"/> that represents the converted value.</returns>
			virtual System::Object^ ConvertFrom(System::ComponentModel::ITypeDescriptorContext^ context, 
				System::Globalization::CultureInfo^ culture, System::Object^ value) override;
		};
	}
}