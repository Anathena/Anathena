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

#include "../stack_array.h"
#include "../DataStream.h"
#include "../ComObject.h"
#include "../Utilities.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "D3DX.h"
#include "Effect9.h"
#include "ConstantTable.h"
#include "TextureShader.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	TextureShader::TextureShader( DataStream^ stream )
	{
		ID3DXTextureShader *result;

		HRESULT hr = D3DXCreateTextureShader( reinterpret_cast<const DWORD*>( stream->PositionPointer ), &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	EffectHandle^ TextureShader::GetConstant(SlimDX::Direct3D9::EffectHandle ^handle, int index)
	{
		D3DXHANDLE parentHandle = handle != nullptr ? handle->InternalHandle : NULL;
		D3DXHANDLE result = InternalPointer->GetConstant( parentHandle, index );
		GC::KeepAlive( handle );
		
		if( result == NULL )
			return nullptr;
		return gcnew EffectHandle( result );
	}
	
	EffectHandle^ TextureShader::GetConstant(SlimDX::Direct3D9::EffectHandle ^handle, String^ name )
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
	
	EffectHandle^ TextureShader::GetConstantElement( EffectHandle^ handle, int index )
	{
		D3DXHANDLE parentHandle = handle != nullptr ? handle->InternalHandle : NULL;
		D3DXHANDLE result = InternalPointer->GetConstantElement( parentHandle, index );
		GC::KeepAlive( handle );
		
		if( result == NULL )
			return nullptr;
		return gcnew EffectHandle( result );
	}

	ConstantDescription TextureShader::GetConstantDescription( EffectHandle^ handle )
	{
		D3DXCONSTANT_DESC nativeDesc;
		ConstantDescription description;

		D3DXHANDLE nativeHandle = handle != nullptr ? handle->InternalHandle : NULL;
		unsigned int count = 1;

		HRESULT hr = InternalPointer->GetConstantDesc( nativeHandle, &nativeDesc, &count );
		GC::KeepAlive( handle );
		
		if( RECORD_D3D9(hr).IsFailure )
			return description;

		description.Initialize( nativeDesc );
		return description;
	}

	array<ConstantDescription>^ TextureShader::GetConstantDescriptionArray( EffectHandle^ handle )
	{
		D3DXHANDLE nativeHandle = handle != nullptr ? handle->InternalHandle : NULL;
		unsigned int count = 0;

		HRESULT hr = InternalPointer->GetConstantDesc( nativeHandle, NULL, &count );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		stack_array<D3DXCONSTANT_DESC> nativeDescArray = stackalloc( D3DXCONSTANT_DESC, count );
		hr = InternalPointer->GetConstantDesc( nativeHandle, &nativeDescArray[0], &count );
		GC::KeepAlive( handle );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		array<ConstantDescription>^ descArray = gcnew array<ConstantDescription>( count );
		for( unsigned int i = 0; i < count; ++i )
		{
			descArray[i].Initialize( nativeDescArray[i] );
		}

		return descArray;
	}

	DataStream^ TextureShader::ConstantBuffer::get()
	{
		ID3DXBuffer *result = NULL;
		HRESULT hr = InternalPointer->GetConstantBuffer( &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ TextureShader::FunctionStream::get()
	{
		ID3DXBuffer *result = NULL;
		HRESULT hr = InternalPointer->GetFunction( &result );
		
		if( RECORD_D3D9(hr).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	Result TextureShader::SetDefaults()
	{
		HRESULT hr = InternalPointer->SetDefaults();
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, bool value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetBool( handle, value );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ parameter, array<bool>^ values )
	{
		//implementing set for bool array is REALLY ANNOYING.
		//Win32 uses BOOL, which is an int
		array<BOOL>^ expandedArray = gcnew array<BOOL>( values->Length );
		Array::Copy( values, expandedArray, values->Length );

		D3DXHANDLE handle = parameter != nullptr ? parameter->InternalHandle : NULL;
		pin_ptr<BOOL> pinnedValue = &expandedArray[0];
		HRESULT hr = InternalPointer->SetBoolArray( handle, pinnedValue, values->Length );
		GC::KeepAlive( parameter );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, int value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetInt( handle, value );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, array<int>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<int> pinned_value = &values[0];
		HRESULT hr = InternalPointer->SetIntArray( handle, pinned_value, values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, float value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetFloat( handle, value );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, array<float>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<float> pinned_values = &values[0];
		HRESULT hr = InternalPointer->SetFloatArray( handle, pinned_values, values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, Vector4 value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetVector( handle, reinterpret_cast<const D3DXVECTOR4*>( &value ) );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, array<Vector4>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<Vector4> pinned_value = &values[0];
		HRESULT hr = InternalPointer->SetVectorArray( handle, reinterpret_cast<const D3DXVECTOR4*>( pinned_value ), values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, Color4 value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetVector( handle, reinterpret_cast<const D3DXVECTOR4*>( &value ) );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, array<Color4>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<Color4> pinned_value = &values[0];
		HRESULT hr = InternalPointer->SetVectorArray( handle, reinterpret_cast<const D3DXVECTOR4*>( pinned_value ), values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, Matrix value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetMatrix( handle, reinterpret_cast<const D3DXMATRIX*>( &value ) );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValue( EffectHandle^ constant, array<Matrix>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<Matrix> pinned_value = &values[0];
		HRESULT hr = InternalPointer->SetMatrixArray( handle, reinterpret_cast<const D3DXMATRIX*>( pinned_value ), values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValueTranspose( EffectHandle^ constant, Matrix value )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		HRESULT hr = InternalPointer->SetMatrixTranspose( handle, reinterpret_cast<const D3DXMATRIX*>( &value ) );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	Result TextureShader::SetValueTranspose( EffectHandle^ constant, array<Matrix>^ values )
	{
		D3DXHANDLE handle = constant != nullptr ? constant->InternalHandle : NULL;
		pin_ptr<Matrix> pinned_value = &values[0];
		HRESULT hr = InternalPointer->SetMatrixTransposeArray( handle, reinterpret_cast<const D3DXMATRIX*>( pinned_value ), values->Length );
		GC::KeepAlive( constant );
		return RECORD_D3D9( hr );
	}

	ConstantTableDescription TextureShader::Description::get()
	{
		D3DXCONSTANTTABLE_DESC nativeDesc;
		ConstantTableDescription description;

		HRESULT hr = InternalPointer->GetDesc( &nativeDesc );
		
		if( RECORD_D3D9(hr).IsFailure )
			return description;

		description.Creator = gcnew String( nativeDesc.Creator );
		description.Version = gcnew Version( D3DSHADER_VERSION_MAJOR( nativeDesc.Version ), D3DSHADER_VERSION_MINOR( nativeDesc.Version ) );
		description.Constants = nativeDesc.Constants;
		return description;
	}
}
}