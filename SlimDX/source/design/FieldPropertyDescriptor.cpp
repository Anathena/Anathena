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

#include "FieldPropertyDescriptor.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::ComponentModel::Design::Serialization;
using namespace System::Reflection;

namespace SlimDX
{
namespace Design
{
	FieldPropertyDescriptor::FieldPropertyDescriptor( FieldInfo^ fieldInfo )
		: PropertyDescriptor( fieldInfo->Name, 
		static_cast<array<Attribute^>^>( fieldInfo->GetCustomAttributes(true) ) )
	{
		m_FieldInfo = fieldInfo;
	}

	Type^ FieldPropertyDescriptor::ComponentType::get()
	{
		return m_FieldInfo->DeclaringType;
	}

	bool FieldPropertyDescriptor::IsReadOnly::get()
	{
		return false;
	}

	Type^ FieldPropertyDescriptor::PropertyType::get()
	{
		return m_FieldInfo->FieldType;
	}

	bool FieldPropertyDescriptor::CanResetValue( Object^ )
	{
		return false;
	}

	void FieldPropertyDescriptor::ResetValue( Object^ )
	{
		//don't need to do anything
	}

	Object^ FieldPropertyDescriptor::GetValue( Object^ component )
	{
		return m_FieldInfo->GetValue( component );
	}

	void FieldPropertyDescriptor::SetValue( Object^ component, Object^ value )
	{
		m_FieldInfo->SetValue( component, value );
	}

	bool FieldPropertyDescriptor::ShouldSerializeValue( Object^ )
	{
		return true;
	}
}
}