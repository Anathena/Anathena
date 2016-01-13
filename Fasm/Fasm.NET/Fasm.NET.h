/*
* Fasm.NET
* https://github.com/ZenLulz/Fasm.NET
*
* Copyright 2013 ZenLulz ~ Jämes Ménétrey
* Released under the MIT license
*
* Date: 2013-07-10
*/

#pragma once

#include "Enumerations.h"
#include "FasmAssemblerException.h"
#include "Imports.h"
#include "Structures.h"

#define DEFAULT_MEMORY_SIZE 4096
#define DEFAULT_PASS_LIMIT 100

using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace System::Runtime::InteropServices;
using namespace System::Text;

namespace Binarysharp {
	namespace Assemblers {
		namespace Fasm {
			/// <summary>
			/// The managed wrapper to interfact with FASM compiler.
			/// </summary>
			public ref class FasmNet
			{
			protected:
				/// <summary>
				/// The mnemonics inserted by the user.
				/// </summary>
				StringBuilder^ _mnemonics;
				/// <summary>
				/// The memory size allocated for the buffer.
				/// </summary>
				int _memorySize;
				/// <summary>
				/// The maximum number of pass to perform.
				/// </summary>
				int _passLimit;
			public:
				/// <summary>
				/// Gets the version of FASM compiler.
				/// </summary>
				static Version^ GetVersion();
				/// <summary>
				/// Assembles the given mnemonics.
				/// </summary>
				/// <param name="source">The mnemonics to assemble.</param>
				/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
				static array<byte>^ Assemble(String^ source);
				/// <summary>
				/// Assembles the given mnemonics.
				/// </summary>
				/// <param name="source">The mnemonics to assemble.</param>
				/// <param name="memorySize">The memory size allocated for the buffer.</param>
				/// <param name="passLimit">The maximum number of pass to perform.</param>
				static array<byte>^ Assemble(String^ source, int memorySize, int passLimit);
				/// <summary>
				/// Assembles the given mnemonics.
				/// </summary>
				/// <param name="source">The array containing mnemonics to assemble.</param>
				/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
				static array<byte>^ Assemble(array<String^>^ source);
				/// <summary>
				/// Assembles the given mnemonics.
				/// </summary>
				/// <param name="source">The array containing mnemonics to assemble.</param>
				/// <param name="memorySize">The memory size allocated for the buffer.</param>
				/// <param name="passLimit">The maximum number of pass to perform.</param>
				static array<byte>^ Assemble(array<String^>^ source, int memorySize, int passLimit);
				/// <summary>
				/// Assembles the specified file.
				/// </summary>
				/// <param name="path">The path of the file to assemble.</param>
				/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
				static array<byte>^ AssembleFile(String^ path);
				/// <summary>
				/// Assembles the specified file.
				/// </summary>
				/// <param name="path">The path of the file to assemble.</param>
				/// <param name="memorySize">The memory size allocated for the buffer.</param>
				/// <param name="passLimit">The maximum number of pass to perform.</param>
				static array<byte>^ AssembleFile(String^ path, int memorySize, int passLimit);
				/// <summary>
				/// Assembles the specified files by appending them.
				/// </summary>
				/// <param name="paths">The path of the files to assemble.</param>
				/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
				static array<byte>^ AssembleFiles(IEnumerable<String^>^ paths);
				/// <summary>
				/// Assembles the specified files by appending them.
				/// </summary>
				/// <param name="paths">The path of the files to assemble.</param>
				/// <param name="memorySize">The memory size allocated for the buffer.</param>
				/// <param name="passLimit">The maximum number of pass to perform.</param>
				static array<byte>^ AssembleFiles(IEnumerable<String^>^ paths, int memorySize, int passLimit);
				/// <summary>
				/// Initializes a new instance of the <see cref="Fasm"/> class.
				/// </summary>
				/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
				FasmNet();
				/// <summary>
				/// Initializes a new instance of the <see cref="Fasm"/> class.
				/// </summary>
				/// <param name="memorySize">The memory size allocated for the buffer.</param>
				/// <param name="passLimit">The maximum number of pass to perform.</param>
				FasmNet(int memorySize, int passLimit);
				/// <summary>
				/// Adds the text representation of the specified array of objects, followed by the current line terminator.
				/// </summary>
				/// <param name="format">The composite format string.</param>
				/// <param name="args">The array of objects to write using format.</param>
				void AddLine(String^ format, ... array<Object^>^ args);
				/// <summary>
				/// Assembles the mnemonics.
				/// </summary>
				array<byte>^ Assemble();
				/// <summary>
				/// Assembles the mnemonics with a given origin address.
				/// </summary>
				/// <param name="origin">The address used as starting address for the assebmly code.</param>
				array<byte>^ Assemble(IntPtr^ origin);
				/// <summary>
				/// Removes all characters from the current <see cref="Fasm" /> instance.
				/// </summary>
				void Clear();
				/// <summary>
				/// Inserts the text representation of the specified array of objects, followed by the current line terminator at the specified character position.
				/// </summary>
				/// <param name="index">The position in this instance where insertion begins.</param>
				/// <param name="format">The composite format string.</param>
				/// <param name="args">The array of objects to write using format.</param>
				void InsertLine(int index, String^ format, ... array<Object^>^ args);
				/// <summary>
				/// Gets the mnemonics.
				/// </summary>
				property String^ Mnemonics
				{
					String^ get() { return _mnemonics->ToString(); }
				}
			};
		}
	}
}