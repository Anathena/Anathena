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

#include "ShaderSemantic.h"
#include "ConstantTable.h"
#include "Include.h"
#include "Macro.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		ref class ConstantTable;

		/// <summary>
		/// Represents a raw shader function stream.
		/// </summary>
		/// <unmanaged>ID3DXBuffer</unmanaged>
		public ref class ShaderBytecode : ComObject
		{
			COMOBJECT(ID3DXBuffer, ShaderBytecode);

			ConstantTable^ m_constantTable;

		public:
			ShaderBytecode( array<System::Byte>^ data );

			/// <summary>
			/// Releases all resources used by the <see cref="ShaderBytecode"/>.
			/// </summary>
			virtual ~ShaderBytecode() { delete m_constantTable; }
			
			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			
			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( array<System::Byte>^ sourceData, ShaderFlags flags );

			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			
			/// <summary>
			/// Assembles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Assemble( System::String^ sourceData, ShaderFlags flags );

			/// <summary>
			/// Assembles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ AssembleFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Assembles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ AssembleFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, ShaderFlags flags );
			
			/// <summary>
			/// Assembles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader file.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ AssembleFromFile( System::String^ fileName, ShaderFlags flags );

			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags );
			
			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( array<System::Byte>^ sourceData, System::String^ functionName, System::String^ profile, ShaderFlags flags );

			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags );
			
			/// <summary>
			/// Compiles a shader from the given source data.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ Compile( System::String^ sourceData, System::String^ functionName, System::String^ profile, ShaderFlags flags );

			/// <summary>
			/// Compiles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags, [Out] System::String^% errors );
			
			/// <summary>
			/// Compiles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, System::String^ functionName, System::String^ profile, ShaderFlags flags );
			
			/// <summary>
			/// Compiles a shader from file.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <param name="functionName">The name of the shader entry point.</param>
			/// <param name="profile">The shader profile that determines the shader instruction set.</param>
			/// <param name="flags">Compilation options.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static ShaderBytecode^ CompileFromFile( System::String^ fileName, System::String^ functionName, System::String^ profile, ShaderFlags flags );

			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile, [Out] System::String^% errors );
			
			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( array<System::Byte>^ sourceData, array<Macro>^ defines, Include^ includeFile );
			
			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( array<System::Byte>^ sourceData );

			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile, [Out] System::String^% errors );
			
			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( System::String^ sourceData, array<Macro>^ defines, Include^ includeFile );
			
			/// <summary>
			/// Preprocesses a shader without performing compilation.
			/// </summary>
			/// <param name="sourceData">The source shader data.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ Preprocess( System::String^ sourceData );

			/// <summary>
			/// Preprocesses a shader file without performing compilation.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <param name="errors">When the method completes, contains any compilation errors and warnings.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile, [Out] System::String^% errors );
			
			/// <summary>
			/// Preprocesses a shader file without performing compilation.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <param name="defines">Macro definitions.</param>
			/// <param name="includeFile">An <see cref="Include"/> interface to use for handling #include directives.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName, array<Macro>^ defines, Include^ includeFile );
			
			/// <summary>
			/// Preprocesses a shader file without performing compilation.
			/// </summary>
			/// <param name="fileName">Name of the shader source file.</param>
			/// <returns>A <see cref="ShaderBytecode"/> object representing the raw shader stream.</returns>
			static System::String^ PreprocessFromFile( System::String^ fileName );

			/// <summary>
			/// Extracts the major version component of a shader version number.
			/// </summary>
			/// <param name="version">The shader version number.</param>
			/// <returns>The major version component.</returns>
			static int MajorVersion( int version );

			/// <summary>
			/// Extracts the minor version component of a shader version number.
			/// </summary>
			/// <param name="version">The shader version number.</param>
			/// <returns>The minor version component.</returns>
			static int MinorVersion( int version );

			/// <summary>
			/// Converts a shader version number into a managed <see cref="System::Version"/> object.
			/// </summary>
			/// <param name="version">The shader version number.</param>
			/// <returns>The parsed shader version information.</returns>
			static System::Version^ ParseVersion( int version );

			/// <summary>
			/// Disassembles the shader bytecode.
			/// </summary>
			/// <param name="enableColorCode">Determines whether the disassembly should be color coded for easier reading.</param>
			/// <param name="comments">A shader comment string.</param>
			/// <returns>The disassembled shader.</returns>
			DataStream^ Disassemble( bool enableColorCode, System::String^ comments );

			/// <summary>
			/// Disassembles the shader bytecode.
			/// </summary>
			/// <param name="enableColorCode">Determines whether the disassembly should be color coded for easier reading.</param>
			/// <returns>The disassembled shader.</returns>
			DataStream^ Disassemble( bool enableColorCode );

			/// <summary>
			/// Disassembles the shader bytecode.
			/// </summary>
			/// <returns>The disassembled shader.</returns>
			DataStream^ Disassemble();

			/// <summary>
			/// Searches through the shader for the specified comment.
			/// </summary>
			/// <param name="fourCC">A FOURCC code used to identify the comment.</param>
			/// <returns>The comment data.</returns>
			DataStream^ FindComment( Format fourCC );

			/// <summary>
			/// Gets the set of semantics for shader inputs.
			/// </summary>
			/// <returns>The set of semantics for shader inputs.</returns>
			array<ShaderSemantic>^ GetInputSemantics();

			/// <summary>
			/// Gets the set of semantics for shader outputs.
			/// </summary>
			/// <returns>The set of semantics for shader outputs.</returns>
			array<ShaderSemantic>^ GetOutputSemantics();

			/// <summary>
			/// Gets the sampler names references in the shader.
			/// </summary>
			/// <returns>The set of referenced sampler names.</returns>
			array<System::String^>^ GetSamplers();

			/// <summary>
			/// Gets the raw shader function stream.
			/// </summary>
			property DataStream^ Data
			{
				DataStream^ get();
			}

			/// <summary>
			/// Gets the shader constant table.
			/// </summary>
			property ConstantTable^ ConstantTable
			{
				SlimDX::Direct3D9::ConstantTable^ get();
			}

			/// <summary>
			/// Gets the version of the shader.
			/// </summary>
			property int Version
			{
				int get();
			}
		};
	}
}