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

#include "D3DCompilerException.h"

#include "ShaderReflectionDC.h"
#include "ShaderBytecodeDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{
	ShaderReflection::ShaderReflection( ShaderBytecode^ byteCode )
	{
		ID3D11ShaderReflection* result = 0;
		HRESULT hr = D3DReflect( byteCode->InternalPointer->GetBufferPointer(), byteCode->InternalPointer->GetBufferSize(), IID_ID3D11ShaderReflection, reinterpret_cast<void**>( &result ) );
		if( RECORD_D3DC( hr ).IsFailure )
			throw gcnew D3DCompilerException( Result::Last );

		Construct( result );

		D3D11_SHADER_DESC desc;
		InternalPointer->GetDesc( &desc );
		description = gcnew ShaderDescription( desc );
	}

	ConstantBuffer^ ShaderReflection::GetConstantBuffer( int index )
	{
		return gcnew ConstantBuffer( InternalPointer->GetConstantBufferByIndex( index ) );
	}

	ConstantBuffer^ ShaderReflection::GetConstantBuffer( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		return gcnew ConstantBuffer( InternalPointer->GetConstantBufferByName( reinterpret_cast<LPCSTR>(pinnedName) ) );
	}

	ShaderParameterDescription ShaderReflection::GetInputParameterDescription( int index )
	{
		D3D11_SIGNATURE_PARAMETER_DESC desc;

		HRESULT hr = InternalPointer->GetInputParameterDesc( index, &desc );
		RECORD_D3DC( hr );

		return ShaderParameterDescription( desc );
	}

	ShaderParameterDescription ShaderReflection::GetOutputParameterDescription( int index )
	{
		D3D11_SIGNATURE_PARAMETER_DESC desc;

		HRESULT hr = InternalPointer->GetOutputParameterDesc( index, &desc );
		RECORD_D3DC( hr );

		return ShaderParameterDescription( desc );
	}

	ShaderParameterDescription ShaderReflection::GetPatchConstantParameterDescription( int index )
	{
		D3D11_SIGNATURE_PARAMETER_DESC desc;

		HRESULT hr = InternalPointer->GetPatchConstantParameterDesc( index, &desc );
		RECORD_D3DC( hr );

		return ShaderParameterDescription( desc );
	}

	InputBindingDescription ShaderReflection::GetResourceBindingDescription( int index )
	{
		D3D11_SHADER_INPUT_BIND_DESC desc;

		HRESULT hr = InternalPointer->GetResourceBindingDesc( index, &desc );
		RECORD_D3DC( hr );

		return InputBindingDescription( desc );
	}

	InputBindingDescription ShaderReflection::GetResourceBindingDescription( String^ name )
	{
		D3D11_SHADER_INPUT_BIND_DESC desc;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->GetResourceBindingDescByName( reinterpret_cast<LPCSTR>(pinnedName), &desc );
		RECORD_D3DC( hr );

		return InputBindingDescription( desc );
	}

	Direct3D11::FeatureLevel ShaderReflection::MinimumFeatureLevel::get()
	{
		D3D_FEATURE_LEVEL fl;
		InternalPointer->GetMinFeatureLevel( &fl );

		return static_cast<Direct3D11::FeatureLevel>( fl );
	}

	InputPrimitive ShaderReflection::GeometryShaderInputPrimitive::get()
	{
		return static_cast<InputPrimitive>( InternalPointer->GetGSInputPrimitive() );
	}
	
	int ShaderReflection::BitwiseInstructionCount::get()
	{
		return InternalPointer->GetBitwiseInstructionCount();
	}
	
	int ShaderReflection::ConversionInstructionCount::get()
	{
		return InternalPointer->GetConversionInstructionCount();
	}
	
	int ShaderReflection::MoveInstructionCount::get()
	{
		return InternalPointer->GetMovInstructionCount();
	}
	
	int ShaderReflection::ConditionalMoveInstructionCount::get()
	{
		return InternalPointer->GetMovcInstructionCount() ;
	}
	
	int ShaderReflection::InterfaceSlotCount::get()
	{
		return InternalPointer->GetNumInterfaceSlots();
	}
	
	bool ShaderReflection::IsSampleFrequencyShader::get()
	{
		return InternalPointer->IsSampleFrequencyShader() > 0;
	}
}
}