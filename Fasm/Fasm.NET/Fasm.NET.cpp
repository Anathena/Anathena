/*
* Fasm.NET
* https://github.com/ZenLulz/Fasm.NET
*
* Copyright 2013 ZenLulz ~ Jämes Ménétrey
* Released under the MIT license
*
* Date: 2013-07-10
*/

#include "stdafx.h"
#include "Fasm.NET.h"

namespace Binarysharp {
	namespace Assemblers {
		namespace Fasm {
			#pragma region Static Methods
			#pragma region FasmNet::GetVersion
			/// <summary>
			/// Gets the version of FASM compiler.
			/// </summary>
			Version^ FasmNet::GetVersion()
			{
				// Call the native function to get the version
				int nativeVersion = fasm_GetVersion();
				// Create and return a managed version object
				return gcnew Version(nativeVersion & 0xff, (nativeVersion >> 16) & 0xff);
			}
			#pragma endregion
			#pragma region FasmNet::Assemble
			/// <summary>
			/// Assembles the given mnemonics.
			/// </summary>
			/// <param name="source">The mnemonics to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			array<byte>^ FasmNet::Assemble(String^ source, int memorySize, int passLimit)
			{
				// If the string is null or empty
				if(String::IsNullOrEmpty(source))
				{
					// Return an empty array
					return gcnew array<byte>{};
				}

				// Check if the memory size was given
				if(memorySize < 1)
					memorySize = DEFAULT_MEMORY_SIZE;

				// Check if the number of maximum pass was given
				if(passLimit < 1)
					passLimit = DEFAULT_PASS_LIMIT;

				//Create some useful vars
				IntPtr nativeSource, nativeBuffer;

				try
				{
					// Allocate a chunk of memory for the native mnemonics array
					nativeSource = Marshal::StringToHGlobalAnsi(source);

					// Allocate the buffer for the assembly code
					nativeBuffer = Marshal::AllocHGlobal(memorySize);

					// Assemble the mnemonics
					FasmResults ret = (FasmResults)fasm_Assemble((char*)nativeSource.ToPointer(), (byte*)nativeBuffer.ToPointer(), memorySize, passLimit, NULL);
					NativeFasmState* state = static_cast<NativeFasmState*>(nativeBuffer.ToPointer());

					// If all was right
					if(ret == FasmResults::Ok)
					{
						// Get the assembly code
						array<byte>^ assembly = gcnew array<byte>(state->OutputLength);
						Marshal::Copy((IntPtr)state->OutputData, assembly, 0, state->OutputLength);
						return assembly;
					}
					else
					{
						throw gcnew FasmAssemblerException((FasmErrors)state->ErrorCode, state->ErrorLine->LineNumber, state->ErrorLine->FileOffset, source);
					}
				}
				finally
				{
					// Free the resources
					if(nativeSource != IntPtr::Zero)
						Marshal::FreeHGlobal(nativeSource);

					if(nativeBuffer != IntPtr::Zero)
						Marshal::FreeHGlobal(nativeBuffer);
				}
			}

			/// <summary>
			/// Assembles the given mnemonics.
			/// </summary>
			/// <param name="source">The mnemonics to assemble.</param>
			/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
			array<byte>^ FasmNet::Assemble(String^ source)
			{
				return Assemble(source, DEFAULT_MEMORY_SIZE, DEFAULT_PASS_LIMIT);
			}

			/// <summary>
			/// Assembles the given mnemonics.
			/// </summary>
			/// <param name="source">The array containing mnemonics to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			array<byte>^ FasmNet::Assemble(array<String^>^ source, int memorySize, int passLimit)
			{
				return Assemble(String::Join(Environment::NewLine, source), memorySize, passLimit);
			}

			/// <summary>
			/// Assembles the given mnemonics.
			/// </summary>
			/// <param name="source">The array containing mnemonics to assemble.</param>
			/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
			array<byte>^ FasmNet::Assemble(array<String^>^ source)
			{
				return Assemble(String::Join(Environment::NewLine, source), DEFAULT_MEMORY_SIZE, DEFAULT_PASS_LIMIT);
			}
			#pragma endregion
			#pragma region FasmNet::AssembleFile
			/// <summary>
			/// Assembles the specified file.
			/// </summary>
			/// <param name="path">The path of the file to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			array<byte>^ FasmNet::AssembleFile(String^ path, int memorySize, int passLimit)
			{
				return AssembleFiles(gcnew array<String^> { path }, memorySize, passLimit);
			}

			/// <summary>
			/// Assembles the specified file.
			/// </summary>
			/// <param name="path">The path of the file to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
			array<byte>^ FasmNet::AssembleFile(String^ path)
			{
				return AssembleFile(path, DEFAULT_MEMORY_SIZE, DEFAULT_PASS_LIMIT);
			}
			#pragma endregion
			#pragma region FasmNet::AssembleFiles
			/// <summary>
			/// Assembles the specified files by appending them.
			/// </summary>
			/// <param name="paths">The path of the files to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			array<byte>^ FasmNet::AssembleFiles(IEnumerable<String^>^ paths, int memorySize, int passLimit)
			{
				// Allocate a builder to store the content of all the file
				StringBuilder^ ret = gcnew StringBuilder();

				// For each file
				for each(String^ path in paths)
				{
					ret->AppendLine(File::ReadAllText(path));
				}

				// Assemble the builder
				return Assemble(ret->ToString(), memorySize, passLimit);
			}

			/// <summary>
			/// Assembles the specified files by appending them.
			/// </summary>
			/// <param name="paths">The path of the files to assemble.</param>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
			array<byte>^ FasmNet::AssembleFiles(IEnumerable<String^>^ paths)
			{
				return AssembleFiles(paths, DEFAULT_MEMORY_SIZE, DEFAULT_PASS_LIMIT);
			}
			#pragma endregion
			#pragma endregion

			#pragma region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="Fasm"/> class.
			/// </summary>
			/// <remarks>The default memory size used is 4096 bytes and the maximum number of pass is 100.</remarks>
			FasmNet::FasmNet()
			{
				// Call the overloaded constructor (no delegating constructor in C++/CLI :'( )
				this->FasmNet::FasmNet(DEFAULT_MEMORY_SIZE, DEFAULT_PASS_LIMIT);
			}
			/// <summary>
			/// Initializes a new instance of the <see cref="Fasm"/> class.
			/// </summary>
			/// <param name="memorySize">The memory size allocated for the buffer.</param>
			/// <param name="passLimit">The maximum number of pass to perform.</param>
			FasmNet::FasmNet(int memorySize, int passLimit)
			{
				// Save the parameters
				_memorySize = memorySize;
				_passLimit = passLimit;
				// Create a StringBuilder instance
				_mnemonics = gcnew StringBuilder();
			}
			#pragma endregion

			#pragma region Methods
			#pragma region FasmNet::AddLine
			/// <summary>
			/// Adds the text representation of the specified array of objects, followed by the current line terminator.
			/// </summary>
			/// <param name="format">The composite format string.</param>
			/// <param name="args">The array of objects to write using format.</param>
			void FasmNet::AddLine(String^ format, ... array<Object^>^ args)
			{
				_mnemonics->AppendLine(String::Format(format, args));
			}
#pragma endregion
#pragma region FasmNet::Assemble
			/// <summary>
			/// Assembles the mnemonics.
			/// </summary>
			array<byte>^ FasmNet::Assemble()
			{
				return Assemble(IntPtr::Zero);
			}
			/// <summary>
			/// Assembles the mnemonics with a given origin address.
			/// </summary>
			/// <param name="origin">The address used as starting address for the assebmly code.</param>
			array<byte>^ FasmNet::Assemble(IntPtr^ origin)
			{
				// If an origin was submitted
				if(origin != IntPtr::Zero)
					InsertLine(0, "org {0}", origin);

				// Assemble and return the assembly code
				return FasmNet::Assemble(Mnemonics, _memorySize, _passLimit);
			}
			#pragma endregion
			#pragma region FasmNet::Clear
			/// <summary>
			/// Removes all characters from the current <see cref="Fasm" /> instance.
			/// </summary>
			void FasmNet::Clear()
			{
				_mnemonics->Clear();
			}
			#pragma endregion
			#pragma region FasmNet::InsertLine
			/// <summary>
			/// Inserts the text representation of the specified array of objects, followed by the current line terminator at the specified character position.
			/// </summary>
			/// <param name="index">The position in this instance where insertion begins.</param>
			/// <param name="format">The composite format string.</param>
			/// <param name="args">The array of objects to write using format.</param>
			void FasmNet::InsertLine(int index, String^ format, ... array<Object^>^ args)
			{
				_mnemonics->Insert(index, String::Format(format, args) + Environment::NewLine);
			}
			#pragma endregion
			#pragma endregion
		}
	}
}