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

#include "Effect9.h"
#include "ConstantTable.h"
#include "ShaderBytecode.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Compiles an effect from a function or a vertex shader.</summary>
		/// <unmanaged>ID3DXEffectCompiler</unmanaged>
		public ref class EffectCompiler : public BaseEffect
		{
			COMOBJECT(ID3DXEffectCompiler, EffectCompiler);

		private:
			static EffectCompiler^ FromMemory_Internal( const char* memory, UINT size, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, System::String^* compilationErrors, System::Exception^* exception );

		public:
			EffectCompiler( System::String^ data, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% compilationErrors );
			EffectCompiler( System::String^ data, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			EffectCompiler( System::String^ data, ShaderFlags flags );

			static EffectCompiler^ FromMemory( array<System::Byte>^ data, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% compilationErrors );
			static EffectCompiler^ FromMemory( array<System::Byte>^ data, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			static EffectCompiler^ FromMemory( array<System::Byte>^ data, ShaderFlags flags );

			static EffectCompiler^ FromStream( System::IO::Stream^ stream, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% compilationErrors );
			static EffectCompiler^ FromStream( System::IO::Stream^ stream, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			static EffectCompiler^ FromStream( System::IO::Stream^ stream, ShaderFlags flags );

			static EffectCompiler^ FromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% compilationErrors );
			static EffectCompiler^ FromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			static EffectCompiler^ FromFile( System::String^ fileName, ShaderFlags flags );

			ShaderBytecode^ CompileShader( EffectHandle^ functionHandle, System::String^ target, ShaderFlags flags, [Out] System::String^% compilationErrors, [Out] ConstantTable^% constantTable );
			ShaderBytecode^ CompileShader( EffectHandle^ functionHandle, System::String^ target, ShaderFlags flags, [Out] System::String^% compilationErrors );
			ShaderBytecode^ CompileShader( EffectHandle^ functionHandle, System::String^ target, ShaderFlags flags );

			DataStream^ CompileEffect( ShaderFlags flags, [Out] System::String^% compilationErrors );
			DataStream^ CompileEffect( ShaderFlags flags );

			Result SetLiteral( EffectHandle^ handle, bool literal );
			bool GetLiteral( EffectHandle^ handle );
		};
	}
}