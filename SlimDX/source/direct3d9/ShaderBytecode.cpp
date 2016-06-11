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
#include "../ComObject.h"
#include "../DataStream.h"
#include "../CompilationException.h"

#include "Direct3D9Exception.h"
#include "ConstantTable.h"
#include "ShaderBytecode.h"

#include "Device.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	ShaderBytecode::ShaderBytecode( array<Byte>^ data )
	{
		ID3DXBuffer *buffer = NULL;

		if( data == nullptr )
			throw gcnew ArgumentNullException( "data" );

		HRESULT hr = D3DXCreateBuffer( data->Length, &buffer );
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		pin_ptr<Byte> pinnedData = &data[0];

		memcpy( buffer->GetBufferPointer(), pinnedData, data->Length );

		Construct( buffer );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXAssembleShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		Macro::Unmarshal( handles );
		errors = Utilities::BufferToString( errorBuffer );

		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXAssembleShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		Macro::Unmarshal( handles );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( array<Byte>^ sourceData, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		HRESULT hr = D3DXAssembleShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, NULL, NULL,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Assemble( data, defines, includeFile, flags, errors );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Assemble( data, defines, includeFile, flags );
	}

	ShaderBytecode^ ShaderBytecode::Assemble( String^ sourceData, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Assemble( data, flags );
	}

	ShaderBytecode^ ShaderBytecode::AssembleFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXAssembleShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		Macro::Unmarshal( handles );

		errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::AssembleFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer *errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXAssembleShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), macrosPtr, includePtr,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		Macro::Unmarshal( handles );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::AssembleFromFile( String^ fileName, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXAssembleShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), NULL, NULL,
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCompileShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );

		Macro::Unmarshal( handles );
		
		errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCompileShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );

		Macro::Unmarshal( handles );
		
		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ sourceData, String^ functionName, String^ profile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		HRESULT hr = D3DXCompileShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, NULL, NULL,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );
		
		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags, [Out] String^% errors )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Compile( data, defines, includeFile, functionName, profile, flags, errors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Compile( data, defines, includeFile, functionName, profile, flags );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ sourceData, String^ functionName, String^ profile, ShaderFlags flags )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Compile( data, functionName, profile, flags );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags, [Out] String^% errors )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedFileName = PtrToStringChars( fileName );
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCompileShaderFromFile( pinnedFileName, macrosPtr, includePtr,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );

		Macro::Unmarshal( handles );

		errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, String^ functionName, String^ profile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedFileName = PtrToStringChars( fileName );
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXCompileShaderFromFile( pinnedFileName, macrosPtr, includePtr,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );

		Macro::Unmarshal( handles );
		
		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ functionName, String^ profile, ShaderFlags flags )
	{
		ID3DXBuffer* shaderBuffer;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedFileName = PtrToStringChars( fileName );
		
		array<Byte>^ functionBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( functionName );
		pin_ptr<Byte> pinnedFunction = &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];

		HRESULT hr = D3DXCompileShaderFromFile( pinnedFileName, NULL, NULL,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ),
			static_cast<DWORD>( flags ), &shaderBuffer, &errorBuffer, NULL );
		
		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( shaderBuffer );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ sourceData, array<Macro>^ defines,
		Include^ includeFile, [Out] String^% errors )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXPreprocessShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			&shaderText, &errorBuffer );

		Macro::Unmarshal( handles );
		
		errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXPreprocessShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, macrosPtr, includePtr,
			&shaderText, &errorBuffer );

		Macro::Unmarshal( handles );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ sourceData )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<Byte> pinnedData = &sourceData[0];

		HRESULT hr = D3DXPreprocessShader( reinterpret_cast<LPCSTR>( pinnedData ), sourceData->Length, NULL, NULL,
			&shaderText, &errorBuffer );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	String^ ShaderBytecode::Preprocess( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile, [Out] String^% errors )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Preprocess( data, defines, includeFile, errors );
	}

	String^ ShaderBytecode::Preprocess( String^ sourceData, array<Macro>^ defines,
		Include^ includeFile )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Preprocess( data, defines, includeFile );
	}

	String^ ShaderBytecode::Preprocess( String^ sourceData )
	{
		array<Byte>^ data = System::Text::ASCIIEncoding::ASCII->GetBytes( sourceData );
		return Preprocess( data );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile, [Out] String^% errors )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXPreprocessShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), macrosPtr, includePtr,
			&shaderText, &errorBuffer );

		Macro::Unmarshal( handles );
		
		errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName, array<Macro>^ defines,
		Include^ includeFile )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		IncludeShim includeShim = IncludeShim( includeFile );
		ID3DXInclude* includePtr = NULL;
		if( includeFile != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3DXMACRO> macros = Macro::Marshal( defines, handles );
		D3DXMACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;

		HRESULT hr = D3DXPreprocessShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), macrosPtr, includePtr,
			&shaderText, &errorBuffer );

		Macro::Unmarshal( handles );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName )
	{
		ID3DXBuffer* shaderText;
		ID3DXBuffer* errorBuffer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DXPreprocessShaderFromFile( reinterpret_cast<LPCWSTR>( pinnedName ), NULL, NULL,
			&shaderText, &errorBuffer );

		String^ errors = Utilities::BufferToString( errorBuffer );
		Exception^ e = CompilationException::Check<Direct3D9Exception^>(hr, errors);
		if (e != nullptr)
			throw e;

		return Utilities::BufferToString( shaderText );
	}

	int ShaderBytecode::MajorVersion( int version )
	{
		return D3DSHADER_VERSION_MAJOR( version );
	}

	int ShaderBytecode::MinorVersion( int version )
	{
		return D3DSHADER_VERSION_MINOR( version );
	}

	Version^ ShaderBytecode::ParseVersion( int version )
	{
		return gcnew System::Version( MajorVersion( version ), MinorVersion( version ) );
	}

	DataStream^ ShaderBytecode::Disassemble( bool enableColorCode, System::String^ comments )
	{
		ID3DXBuffer *result;

		array<unsigned char>^ commentBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( comments );
		pin_ptr<unsigned char> pinnedComments = &commentBytes[0];

		HRESULT hr = D3DXDisassembleShader( reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() ),
			enableColorCode, reinterpret_cast<LPCSTR>( pinnedComments ), &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ ShaderBytecode::Disassemble( bool enableColorCode )
	{
		ID3DXBuffer *result;

		HRESULT hr = D3DXDisassembleShader( reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() ),
			enableColorCode, NULL, &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( result );
	}

	DataStream^ ShaderBytecode::Disassemble()
	{
		return Disassemble( false );
	}

	DataStream^ ShaderBytecode::FindComment( Format fourCC )
	{
		LPCVOID data;
		UINT size;

		HRESULT hr = D3DXFindShaderComment( reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() ),
			static_cast<DWORD>( fourCC ), &data, &size );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew DataStream( data, size, true, false );
	}

	DataStream^ ShaderBytecode::Data::get()
	{
		return gcnew DataStream( InternalPointer->GetBufferPointer(), InternalPointer->GetBufferSize(), true, true, false );
	}

	ConstantTable^ ShaderBytecode::ConstantTable::get()
	{
		if( m_constantTable != nullptr )
			return m_constantTable;

		ID3DXConstantTable* constantTable;
		HRESULT hr = D3DXGetShaderConstantTable( reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() ), &constantTable );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		m_constantTable = SlimDX::Direct3D9::ConstantTable::FromPointer( constantTable );

		return m_constantTable;
	}

	array<ShaderSemantic>^ ShaderBytecode::GetInputSemantics()
	{
		UINT count = 0;
		const DWORD* function = reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() );

		HRESULT hr = D3DXGetShaderInputSemantics( function, NULL, &count );
		
		if( RECORD_D3D9( hr ).IsFailure || count == 0 )
			return nullptr;
		
		array<ShaderSemantic>^ inputs = gcnew array<ShaderSemantic>( count );
		pin_ptr<ShaderSemantic> pinnedInputs = &inputs[0];

		hr = D3DXGetShaderInputSemantics( function, reinterpret_cast<D3DXSEMANTIC*>( pinnedInputs ), &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		return inputs;
	}

	array<ShaderSemantic>^ ShaderBytecode::GetOutputSemantics()
	{
		UINT count = 0;
		const DWORD* function = reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() );

		HRESULT hr = D3DXGetShaderOutputSemantics( function, NULL, &count );
		
		if( RECORD_D3D9( hr ).IsFailure || count == 0 )
			return nullptr;
		
		array<ShaderSemantic>^ outputs = gcnew array<ShaderSemantic>( count );
		pin_ptr<ShaderSemantic> pinnedOutputs = &outputs[0];

		hr = D3DXGetShaderOutputSemantics( function, reinterpret_cast<D3DXSEMANTIC*>( pinnedOutputs ), &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		return outputs;
	}

	array<String^>^ ShaderBytecode::GetSamplers()
	{
		UINT count = 0;
		const DWORD* function = reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() );

		HRESULT hr = D3DXGetShaderSamplers( function, NULL, &count );
		
		if( RECORD_D3D9( hr ).IsFailure || count == 0 )
			return nullptr;
		
		stack_array<LPCSTR> samplers = stackalloc( LPCSTR, count );

		hr = D3DXGetShaderSamplers( function, &samplers[0], &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		array<String^>^ outputSamplers = gcnew array<String^>( count );
		for( UINT i = 0; i < count; ++i )
			outputSamplers[i] = gcnew String( samplers[i] );

		return outputSamplers;
	}

	int ShaderBytecode::Version::get()
	{
		const DWORD* function = reinterpret_cast<const DWORD*>( InternalPointer->GetBufferPointer() );
		return static_cast<int>( D3DXGetShaderVersion( function ) );
	}
}
}