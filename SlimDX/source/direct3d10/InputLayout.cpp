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

#include "../DataStream.h"

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "InputLayout.h"
#include "../d3dcompiler/ShaderBytecodeDC.h"
#include "../d3dcompiler/ShaderSignatureDC.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{
	InputLayout::InputLayout( SlimDX::Direct3D10::Device^ device, array<InputElement>^ elements, D3DCompiler::ShaderSignature^ shaderSignature )
	{
		if( shaderSignature == nullptr )
			throw gcnew ArgumentNullException( "shaderSignature" );

		Init( device, shaderSignature->Data->RawPointer, static_cast<int>(shaderSignature->Data->Length), elements );
	}

	InputLayout::InputLayout( SlimDX::Direct3D10::Device^ device, D3DCompiler::ShaderSignature^ shaderSignature, array<InputElement>^ elements )
	{
		if( shaderSignature == nullptr )
			throw gcnew ArgumentNullException( "shaderSignature" );

		Init( device, shaderSignature->Data->RawPointer, static_cast<int>(shaderSignature->Data->Length), elements );
	}

	InputLayout::InputLayout( SlimDX::Direct3D10::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, array<InputElement>^ elements )
	{
		if( shaderBytecode == nullptr )
			throw gcnew ArgumentNullException( "shaderBytecode" );

		Init( device, shaderBytecode->InternalPointer->GetBufferPointer(), static_cast<int>( shaderBytecode->InternalPointer->GetBufferSize() ), elements );
	}

	void InputLayout::Init( SlimDX::Direct3D10::Device^ device, const void* shader, int length, array<InputElement>^ elements )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
		if( elements == nullptr )
			throw gcnew ArgumentNullException( "elements" );

		D3D10_INPUT_ELEMENT_DESC nativeElements[D3D10_IA_VERTEX_INPUT_STRUCTURE_ELEMENT_COUNT];
		for( int i = 0; i < elements->Length; ++i )
			nativeElements[i] = elements[i].CreateNativeVersion();
			
		ID3D10InputLayout* layout = 0;
		HRESULT hr = device->InternalPointer->CreateInputLayout( nativeElements, elements->Length, shader, length, &layout );

		for( int i = 0; i < elements->Length; i++ )
			Utilities::FreeNativeString( nativeElements[i].SemanticName );

		if( RECORD_D3D10( hr ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );

		Construct( layout );
	}
}
}
