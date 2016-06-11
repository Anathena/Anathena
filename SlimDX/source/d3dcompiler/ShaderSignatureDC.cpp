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

#include "D3DCompilerException.h"
#include "ShaderSignatureDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{
	ShaderSignature::ShaderSignature( DataStream^ data )
		: data(data)
	{
	}

	ShaderSignature::~ShaderSignature()
	{
		delete data;
		data = nullptr;
	}

	ShaderSignature^ ShaderSignature::GetInputSignature( ShaderBytecode^ shaderBytecode )
	{
		ID3D10Blob *blob;

		HRESULT hr = D3DGetInputSignatureBlob( shaderBytecode->InternalPointer->GetBufferPointer(), shaderBytecode->InternalPointer->GetBufferSize(), &blob );
		if( RECORD_D3DC( hr ).IsFailure )
			return nullptr;

		ShaderSignature^ result = gcnew ShaderSignature( gcnew DataStream( blob->GetBufferPointer(), blob->GetBufferSize(), true, false, true ) );
		blob->Release();
		return result;
	}

	ShaderSignature^ ShaderSignature::GetOutputSignature( ShaderBytecode^ shaderBytecode )
	{
		ID3D10Blob *blob;

		HRESULT hr = D3DGetOutputSignatureBlob( shaderBytecode->InternalPointer->GetBufferPointer(), shaderBytecode->InternalPointer->GetBufferSize(), &blob );
		if( RECORD_D3DC( hr ).IsFailure )
			return nullptr;

		ShaderSignature^ result = gcnew ShaderSignature( gcnew DataStream( blob->GetBufferPointer(), blob->GetBufferSize(), true, false, true ) );
		blob->Release();
		return result;
	}

	ShaderSignature^ ShaderSignature::GetInputOutputSignature( ShaderBytecode^ shaderBytecode )
	{
		ID3D10Blob *blob;

		HRESULT hr = D3DGetInputAndOutputSignatureBlob( shaderBytecode->InternalPointer->GetBufferPointer(), shaderBytecode->InternalPointer->GetBufferSize(), &blob );
		if( RECORD_D3DC( hr ).IsFailure )
			return nullptr;

		ShaderSignature^ result = gcnew ShaderSignature( gcnew DataStream( blob->GetBufferPointer(), blob->GetBufferSize(), true, false, true ) );
		blob->Release();
		return result;
	}

	int ShaderSignature::GetHashCode()
	{
		return reinterpret_cast<int>( data->RawPointer );
	}
}
}
