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

using namespace System;

namespace Binarysharp {
	namespace Assemblers {
		namespace Fasm {
			/// <summary>
			/// The exception that is thrown when a FASM compiler error occurs.
			/// </summary>
			public ref class FasmAssemblerException : public ApplicationException
			{
			private:
				/// <summary>
				/// The private field containing the error code.
				/// </summary>
				FasmErrors _errorCode;
				/// <summary>
				/// The private field containing the line where is the error.
				/// </summary>
				int _errorLine;
				/// <summary>
				/// The private field containing the offset within the file where the line starts.
				/// </summary>
				int _errorOffset;
				/// <summary>
				/// The private field containing the assembled mnemonics when the error occurred.
				/// </summary>
				String^ _mnemonics;
			public:
				/// <summary>
				/// The error code.
				/// </summary>
				property FasmErrors ErrorCode
				{
					FasmErrors get() { return _errorCode; }
				}
				/// <summary>
				/// The line where is the error.
				/// </summary>
				property int ErrorLine
				{
					int get() { return _errorLine; }
				}
				/// <summary>
				/// The offset within the file where the line starts.
				/// </summary>
				property int ErrorOffset
				{
					int get() { return _errorOffset; }
				}
				/// <summary>
				/// The assembled mnemonics when the error occurred.
				/// </summary>
				property String^ Mnemonics
				{
					String^ get() { return _mnemonics; }
				}
				/// <summary>
				/// Initializes a new instance of the <see cref="FasmAssemblerException" /> class.
				/// </summary>
				/// <param name="errorCode">The error code.</param>
				/// <param name="errorLine">The line where is the error.</param>
				/// <param name="errorOffset">The offset within the file where the line starts.</param>
				/// <param name="mnemonics">The assembled mnemonics when the error occurred.</param>
				FasmAssemblerException(FasmErrors errorCode, int errorLine, int errorOffset, String^ mnemonics);
			};
		}
	}
}