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

#include "../ComObject.h"

#include "IncludeDC.h"
#include "ShaderMacroDC.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		/// <summary>
		/// Represents the compiled bytecode of a shader or effect.
		/// </summary>
		/// <unmanaged>ID3D10Blob</unmanaged>
		public ref class ShaderBytecode : ComObject
		{
			COMOBJECT(ID3D10Blob, ShaderBytecode);
		
		internal:
			ShaderBytecode( const BYTE* data, UINT length );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ShaderBytecode"/> class.
			/// </summary>
			/// <param name="data">A <see cref="DataStream"/> containing the compiled bytecode.</param>
			ShaderBytecode( DataStream^ data );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ profile );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="sourceName">The name of the source being compiled. This is only used for error reporting purposes.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, System::String^ sourceName, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="sourceName">The name of the source being compiled. This is only used for error reporting purposes.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( System::String^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, System::String^ sourceName, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ profile );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="sourceName">The name of the source being compiled. This is only used for error reporting purposes.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, System::String^ sourceName, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );
			
			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="sourceName">The name of the source being compiled. This is only used for error reporting purposes.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ shaderSource, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, System::String^ sourceName, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ profile );

			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );
			
			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags );
			
			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include );
			
			/// <summary>
			/// Compiles a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="entryPoint">The name of the shader entry-point function, or <c>null</c> for an effect file.</param>
			/// <param name="profile">The shader target or set of shader features to compile against.</param>
			/// <param name="shaderFlags">Shader compilation options.</param>
			/// <param name="effectFlags">Effect compilation options.</param>
			/// <param name="defines">A set of macros to define during compilation.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if compilation succeeded.</param>
			/// <returns>The compiled shader bytecode, or <c>null</c> if the method fails.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ entryPoint, System::String^ profile, ShaderFlags shaderFlags, EffectFlags effectFlags, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to preprocess.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( System::String^ shaderSource );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to preprocess.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( System::String^ shaderSource, array<ShaderMacro>^ defines, Include^ include );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">A string containing the source of the shader or effect to preprocess.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if preprocessing succeeded.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( System::String^ shaderSource, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to preprocess.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( array<System::Byte>^ shaderSource );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to preprocess.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( array<System::Byte>^ shaderSource, array<ShaderMacro>^ defines, Include^ include );

			/// <summary>
			/// Preprocesses the provided shader or effect source.
			/// </summary>
			/// <param name="shaderSource">An array of bytes containing the raw source of the shader or effect to preprocess.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if preprocessing succeeded.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ Preprocess( array<System::Byte>^ shaderSource, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Preprocesses a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName );

			/// <summary>
			/// Preprocesses a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName, array<ShaderMacro>^ defines, Include^ include );

			/// <summary>
			/// Preprocesses a shader or effect from a file on disk.
			/// </summary>
			/// <param name="fileName">The name of the source file to compile.</param>
			/// <param name="defines">A set of macros to define during preprocessing.</param>
			/// <param name="include">An interface for handling include files.</param>
			/// <param name="compilationErrors">When the method completes, contains a string of compilation errors, or an empty string if preprocessing succeeded.</param>
			/// <returns>The preprocessed shader source.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName, array<ShaderMacro>^ defines, Include^ include, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Gets the raw data of the compiled bytecode.
			/// </summary>
			property DataStream^ Data
			{
				DataStream^ get();
			}

			/// <summary>
			/// Disassembles compiled HLSL code back into textual source.
			/// </summary>
			/// <returns>The textual source of the shader or effect.</returns>
			System::String^ Disassemble();

			/// <summary>
			/// Disassembles compiled HLSL code back into textual source.
			/// </summary>
			/// <param name="flags">Flags affecting the output of the disassembly.</param>
			/// <returns>The textual source of the shader or effect.</returns>
			System::String^ Disassemble(DisassemblyFlags flags);

			/// <summary>
			/// Disassembles compiled HLSL code back into textual source.
			/// </summary>
			/// <param name="flags">Flags affecting the output of the disassembly.</param>
			/// <param name="comments">Commenting information to embed in the disassembly.</param>
			/// <returns>The textual source of the shader or effect.</returns>
			System::String^ Disassemble(DisassemblyFlags flags, System::String^ comments);

			/// <summary>
			/// Strips extraneous information from a compiled shader or effect.
			/// </summary>
			/// <param name="flags">Options specifying what to remove from the shader.</param>
			/// <returns>The stripped shader bytecode.</returns>
			ShaderBytecode^ Strip(StripFlags flags);

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;
		};
	}
};