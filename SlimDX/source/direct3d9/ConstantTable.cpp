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
#include <windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include <vcclr.h>

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"
#include "../DataStream.h"

#include "Direct3D9Exception.h"

#include "Effect9.h"
#include "ConstantTable.h"
#include "Device.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D9
{
	EffectHandle^ ConstantTable::GetConstant(SlimDX::Direct3D9::EffectHandle ^handle, int index)
	{
		D3DXHANDLE parentHandle = handle != nullptr ? handle->InternalHandle : NULL;
		D3DXHANDLE result = InternalPointer->GetConstant( parentHandle, index );
		GC::KeepAlive( handle );
		
		if( result == NULL )
			return nullptr;
		return gcnew EffectHandle( result );
	}
	
	EffectHandle^ ConstantTable::GetConstant(SlimDX::Direct3D9::EffectHandle ^handle, String^ name )
	{
		array<unsigned char>^ rawName = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &rawName[0];
		
		D3DXHANDLE parentHandle = handle != nullptr ? handle->InternalHandle : NULL;
		D3DXHANDLE result = InternalPointer->GetConstantByName( parentHandle, reinterpret_cast<const char*>( pinnedName ) );
		GC::KeepAlive( handle );
		
		if( result == NULL )
			return nullptr;
		return gcnew EffectHandle( result );
	}
	
	EffectHandle^ ConstantTable::GetConstantElement( EffectHandle^ handle, int index )
	{
		D3DXHANDLE parentHandle = handle != nullptr ? handle->InternalHandle : NULL;
		D3DXHANDLE result = InternalPointer->GetConstantElement( parentHandle, index );
		GC::KeepAlive( handle );
		
		if( result == NULL )
			return nullptr;
		return gcnew EffectHandle( result );
	}

	ConstantDescription ConstantTable::GetConstantDescription( EffectHandle^ handle )
	{
		D3DXCONSTANT_DESC nativeDesc;
		ConstantDescription description;

		D3DXHANDLE nativeHandle = handle != nullptr ? handle->InternalHandle : NULL;
		unsigned int count = 1;

		HRESULT hr = InternalPointer->GetConstantDesc( nativeHandle, &nativeDesc, &count );
		GC::KeepAlive( handle );
		if( RECORD_D3D9( hr ).IsFailure )
			return ConstantDescription();

		description.Initialize( nativeDesc );
		return description;
	}

	array<ConstantDescription>^ ConstantTable::GetConstantDescriptionArray( EffectHandle^ handle )
	{
		D3DXHANDLE nativeHandle = handle != nullptr ? handle->InternalHandle : NULL;
		unsigned int count = 0;

		HRESULT hr = InternalPointer->GetConstantDesc( nativeHandle, NULL, &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		stack_array<D3DXCONSTANT_DESC> nativeDescArray = stackalloc( D3DXCONSTANT_DESC, count );
		hr = InternalPointer->GetConstantDesc( nativeHandle, &nativeDescArray[0], &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		array<ConstantDescription>^ descArray = gcnew array<ConstantDescription>( count );
		for( unsigned int i = 0; i < count; ++i )
		{
			descArray[i].Initialize( nativeDescArray[i] );
		}

		GC::KeepAlive( handle );
		return descArray;
	}

	int ConstantTable::GetSamplerIndex( EffectHandle^ sampler )
	{
		D3DXHANDLE handle = sampler != nullptr ? sampler->InternalHandle : NULL;
		int result = InternalPointer->GetSamplerIndex( handle );
		GC::KeepAlive( sampler );
		return result;
	}

	DataStream^ ConstantTable::Buffer::get()
	{
		return gcnew DataStream( InternalPointer->GetBufferPointer(), InternalPointer->GetBufferSize(), true, true, false );
	}

	Result ConstantTable::SetDefaults( Device^ device )
	{
		HRESULT hr = InternalPointer->SetDefaults( device->InternalPointer );
		return RECORD_D3D9( hr );
	}

	generic<typename T> where T : value class
	Result ConstantTable::SetValue( Device^ device, EffectHandle^ parameter, T value )
	{
		HRESULT hr;
		D3DXHANDLE handle = parameter != nullptr ? parameter->InternalHandle : NULL;

		if( T::typeid == bool::typeid )
		{
			BOOL newValue = Convert::ToInt32( value, CultureInfo::InvariantCulture );
			hr = InternalPointer->SetBool( device->InternalPointer, handle, newValue );
		}
		else if( T::typeid == float::typeid )
		{
			hr = InternalPointer->SetFloat( device->InternalPointer, handle, static_cast<FLOAT>( value ) );
		}
		else if( T::typeid == int::typeid )
		{
			hr = InternalPointer->SetInt( device->InternalPointer, handle, static_cast<INT>( value ) );
		}
		else if( T::typeid == Matrix::typeid )
		{
			hr = InternalPointer->SetMatrix( device->InternalPointer, handle, reinterpret_cast<D3DXMATRIX*>( &value ) );
		}
		else if( T::typeid == Vector4::typeid )
		{
			hr = InternalPointer->SetVector( device->InternalPointer, handle, reinterpret_cast<D3DXVECTOR4*>( &value ) );
		}
		else
		{
			hr = InternalPointer->SetValue( device->InternalPointer, handle, &value, static_cast<DWORD>( sizeof(T) ) );
		}

		GC::KeepAlive( parameter );
		return RECORD_D3D9( hr );
	}

	generic<typename T> where T : value class
	Result ConstantTable::SetValue( Device^ device, EffectHandle^ parameter, array<T>^ values )
	{
		HRESULT hr;
		D3DXHANDLE handle = parameter != nullptr ? parameter->InternalHandle : NULL;

		if( T::typeid == bool::typeid )
		{
			array<BOOL>^ newValues = Array::ConvertAll<bool, int>( safe_cast<array<bool>>( values ), gcnew Converter<bool, int>( Convert::ToInt32 ) );
			pin_ptr<BOOL> pinnedValues = &newValues[0];

			hr = InternalPointer->SetBoolArray( device->InternalPointer, handle, pinnedValues, values->Length );
		}
		else if( T::typeid == float::typeid )
		{
			pin_ptr<T> pinnedData = &values[0];
			hr = InternalPointer->SetFloatArray( device->InternalPointer, handle, reinterpret_cast<FLOAT*>( pinnedData ), values->Length );
		}
		else if( T::typeid == int::typeid )
		{
			pin_ptr<T> pinnedData = &values[0];
			hr = InternalPointer->SetIntArray( device->InternalPointer, handle, reinterpret_cast<INT*>( pinnedData ), values->Length );
		}
		else if( T::typeid == Matrix::typeid )
		{
			pin_ptr<T> pinnedData = &values[0];
			hr = InternalPointer->SetMatrixArray( device->InternalPointer, handle, reinterpret_cast<D3DXMATRIX*>( pinnedData ), values->Length );
		}
		else if( T::typeid == Vector4::typeid )
		{
			pin_ptr<T> pinnedData = &values[0];
			hr = InternalPointer->SetVectorArray( device->InternalPointer, handle, reinterpret_cast<D3DXVECTOR4*>( pinnedData ), values->Length );
		}
		else
		{
			pin_ptr<T> pinnedData = &values[0];
			hr = InternalPointer->SetValue( device->InternalPointer, handle, pinnedData, static_cast<DWORD>( sizeof(T) ) * values->Length );
		}

		GC::KeepAlive( parameter );
		return RECORD_D3D9( hr );
	}

	ConstantTableDescription ConstantTable::Description::get()
	{
		D3DXCONSTANTTABLE_DESC nativeDesc;
		ConstantTableDescription description;

		HRESULT hr = InternalPointer->GetDesc( &nativeDesc );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return ConstantTableDescription();

		description.Creator = gcnew String( nativeDesc.Creator );
		description.Version = gcnew Version( D3DSHADER_VERSION_MAJOR( nativeDesc.Version ), D3DSHADER_VERSION_MINOR( nativeDesc.Version ) );
		description.Constants = nativeDesc.Constants;
		return description;
	}
}
}
