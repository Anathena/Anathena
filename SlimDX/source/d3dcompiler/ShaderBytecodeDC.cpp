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

#include "../CompilationException.h"
#include "../DataStream.h"

#include "D3DCompilerException.h"
#include "ShaderBytecodeDC.h"

using namespace System;
using namespace System::IO;
using namespace System::Text;
using namespace System::Runtime::InteropServices;
using namespace Microsoft::Win32::SafeHandles;

namespace SlimDX
{
namespace D3DCompiler
{
	ShaderBytecode::ShaderBytecode( const BYTE *data, UINT length )
	{
		ID3D10Blob *blob;

		HRESULT hr = D3D10CreateBlob( length, &blob );
		if( RECORD_D3DC( hr ).IsFailure )
			throw gcnew D3DCompilerException( Result::Last );

		memcpy( blob->GetBufferPointer(), data, length );
		Construct( blob );
	}

	ShaderBytecode::ShaderBytecode( DataStream^ data )
	{
		if( data == nullptr )
			throw gcnew ArgumentNullException( "data" );

		ID3D10Blob *blob;
		HRESULT hr = D3D10CreateBlob( static_cast<SIZE_T>(data->Length), &blob );
		if( RECORD_D3DC( hr ).IsFailure )
			throw gcnew D3DCompilerException( Result::Last );

		memcpy( blob->GetBufferPointer(), data->RawPointer, static_cast<size_t>(data->Length) );

		Construct( blob );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ profile )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, ShaderFlags::None, EffectFlags::None, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}
	
	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, String^ sourceName, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, sourceName, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}
	
	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, defines, include, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( String^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, String^ sourceName, [Out] String^ %compilationErrors )
	{
		if (String::IsNullOrEmpty(shaderSource))
			throw gcnew ArgumentNullException("shaderSource");

		return Compile( Encoding::ASCII->GetBytes( shaderSource ), entryPoint, profile, shaderFlags, effectFlags, defines, include, sourceName, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ profile )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, ShaderFlags::None, EffectFlags::None, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, String^ sourceName, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, nullptr, profile, shaderFlags, effectFlags, defines, include, sourceName, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		return Compile( shaderSource, entryPoint, profile, shaderFlags, effectFlags, defines, include, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::Compile( array<Byte>^ shaderSource, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, String^ sourceName, [Out] String^ %compilationErrors )
	{
		ID3D10Blob *code;
		ID3D10Blob *errors;

		if (shaderSource == nullptr)
			throw gcnew ArgumentNullException("shaderSource");
		if (profile == nullptr)
			throw gcnew ArgumentNullException("profile");
		if (shaderSource->Length == 0)
			throw gcnew ArgumentException("Empty shader source provided.", "shaderSource");

		pin_ptr<Byte> pinnedSource = &shaderSource[0];
		array<Byte>^ functionBytes = entryPoint == nullptr ? nullptr : System::Text::ASCIIEncoding::ASCII->GetBytes( entryPoint );
		pin_ptr<Byte> pinnedFunction = functionBytes == nullptr ? nullptr : &functionBytes[0];
		array<Byte>^ profileBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( profile );
		pin_ptr<Byte> pinnedProfile = &profileBytes[0];
		array<Byte>^ sourceNameBytes = sourceName == nullptr ? nullptr : System::Text::ASCIIEncoding::ASCII->GetBytes( sourceName );
		pin_ptr<Byte> pinnedName = sourceNameBytes == nullptr ? nullptr : &sourceNameBytes[0];

		IncludeShim includeShim = IncludeShim( include );
		ID3D10Include* includePtr = NULL;
		if( include != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3D10_SHADER_MACRO> macros = ShaderMacro::Marshal( defines, handles );
		D3D10_SHADER_MACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;
		
		HRESULT hr = D3DCompile( reinterpret_cast<LPCSTR>( pinnedSource ), shaderSource->Length, reinterpret_cast<LPCSTR>( pinnedName ), macrosPtr, includePtr,
			reinterpret_cast<LPCSTR>( pinnedFunction ), reinterpret_cast<LPCSTR>( pinnedProfile ), static_cast<UINT>( shaderFlags ), static_cast<UINT>( effectFlags ), &code, &errors );

		ShaderMacro::Unmarshal( handles );

		compilationErrors = Utilities::BlobToString( errors );
		Exception^ e = CompilationException::Check<D3DCompilerException^>(hr, compilationErrors);
		if (e != nullptr)
			throw e;

		return ShaderBytecode::FromPointer( code );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ profile )
	{
		String^ compilationErrors;
		return CompileFromFile( fileName, nullptr, profile, ShaderFlags::None, EffectFlags::None, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return CompileFromFile( fileName, nullptr, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return CompileFromFile( fileName, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		return CompileFromFile( fileName, nullptr, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags )
	{
		String^ compilationErrors;
		return CompileFromFile( fileName, entryPoint, profile, shaderFlags, effectFlags, nullptr, nullptr, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ compilationErrors;
		return CompileFromFile( fileName, entryPoint, profile, shaderFlags, effectFlags, defines, include, compilationErrors );
	}

	ShaderBytecode^ ShaderBytecode::CompileFromFile( String^ fileName, String^ entryPoint, String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		if (fileName == nullptr)
			throw gcnew ArgumentNullException("fileName");
		if (profile == nullptr)
			throw gcnew ArgumentNullException("profile");

		if (!File::Exists(fileName))
			throw gcnew FileNotFoundException("Could not open the shader or effect file.", fileName);

		return Compile(File::ReadAllText(fileName), entryPoint, profile, shaderFlags, effectFlags, defines, include, fileName, compilationErrors);
	}

	String^ ShaderBytecode::Preprocess( String^ shaderSource )
	{
		String^ errors;
		return Preprocess( shaderSource, nullptr, nullptr, errors );
	}

	String^ ShaderBytecode::Preprocess( String^ shaderSource, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ errors;
		return Preprocess( shaderSource, defines, include, errors );
	}

	String^ ShaderBytecode::Preprocess( String^ shaderSource, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		if (String::IsNullOrEmpty(shaderSource))
			throw gcnew ArgumentNullException("shaderSource");

		return Preprocess( Encoding::ASCII->GetBytes( shaderSource ), defines, include, compilationErrors );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ shaderSource )
	{
		String^ errors;
		return Preprocess( shaderSource, nullptr, nullptr, errors );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ shaderSource, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ errors;
		return Preprocess( shaderSource, defines, include, errors );
	}

	String^ ShaderBytecode::Preprocess( array<Byte>^ shaderSource, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		ID3D10Blob *code;
		ID3D10Blob *errors;

		if (shaderSource == nullptr)
			throw gcnew ArgumentNullException("shaderSource");
		if (shaderSource->Length == 0)
			throw gcnew ArgumentException("Empty shader source provided.", "shaderSource");

		pin_ptr<Byte> pinnedSource = &shaderSource[0];

		IncludeShim includeShim = IncludeShim( include );
		ID3D10Include* includePtr = NULL;
		if( include != nullptr )
			includePtr = &includeShim;

		array<GCHandle>^ handles;
		stack_array<D3D10_SHADER_MACRO> macros = ShaderMacro::Marshal( defines, handles );
		D3D10_SHADER_MACRO* macrosPtr = macros.size() > 0 ? &macros[0] : NULL;
		
		HRESULT hr = D3DPreprocess( reinterpret_cast<LPCSTR>( pinnedSource ), shaderSource->Length, NULL, macrosPtr, includePtr, &code, &errors );
		ShaderMacro::Unmarshal( handles );

		compilationErrors = Utilities::BlobToString( errors );
		Exception^ e = CompilationException::Check<D3DCompilerException^>(hr, compilationErrors);
		if (e != nullptr)
			throw e;

		return Utilities::BlobToString( code );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName )
	{
		String^ errors;
		return PreprocessFromFile( fileName, nullptr, nullptr, errors );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName, array<ShaderMacro>^ defines, Include^ include )
	{
		String^ errors;
		return PreprocessFromFile( fileName, defines, include, errors );
	}

	String^ ShaderBytecode::PreprocessFromFile( String^ fileName, array<ShaderMacro>^ defines, Include^ include, [Out] String^ %compilationErrors )
	{
		if (fileName == nullptr)
			throw gcnew ArgumentNullException("fileName");

		if (!File::Exists(fileName))
			throw gcnew FileNotFoundException("Could not open the shader or effect file.", fileName);

		return Preprocess(File::ReadAllText(fileName), defines, include, compilationErrors);
	}

	String^ ShaderBytecode::Disassemble()
	{
		return Disassemble(DisassemblyFlags::None, nullptr);
	}

	String^ ShaderBytecode::Disassemble(DisassemblyFlags flags)
	{
		return Disassemble(flags, nullptr);
	}

	String^ ShaderBytecode::Disassemble(DisassemblyFlags flags, String^ comments)
	{
		ID3D10Blob *output = NULL;

		array<unsigned char>^ bytes = comments == nullptr ? nullptr : Encoding::ASCII->GetBytes(comments);
		pin_ptr<unsigned char> pinnedBytes = bytes == nullptr ? nullptr : &bytes[0];

		HRESULT hr = D3DDisassemble(InternalPointer->GetBufferPointer(), InternalPointer->GetBufferSize(), static_cast<UINT>(flags), reinterpret_cast<LPCSTR>(pinnedBytes), &output);
		if (RECORD_D3DC(hr).IsFailure)
			return nullptr;

		return Utilities::BlobToString(output);
	}

	ShaderBytecode^ ShaderBytecode::Strip(StripFlags flags)
	{
		ID3D10Blob *output = NULL;

		HRESULT hr = D3DStripShader(InternalPointer->GetBufferPointer(), InternalPointer->GetBufferSize(), static_cast<UINT>(flags), &output);
		if (RECORD_D3DC(hr).IsFailure)
			return nullptr;

		return ShaderBytecode::FromPointer(output);
	}

	DataStream^ ShaderBytecode::Data::get()
	{
		return gcnew DataStream( InternalPointer->GetBufferPointer(), InternalPointer->GetBufferSize(), true, true, false );
	}

	int ShaderBytecode::GetHashCode()
	{
		return reinterpret_cast<int>( InternalPointer->GetBufferPointer() );
	}
}
}
