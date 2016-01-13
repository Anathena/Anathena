/*
* Fasm.NET
* https://github.com/ZenLulz/Fasm.NET
*
* Copyright 2013 ZenLulz ~ Jämes Ménétrey
* Released under the MIT license
*
* Date: 2013-07-10
*/

#include "Stdafx.h"
#include "FasmAssemblerException.h"

using namespace System;

namespace Binarysharp {
	namespace Assemblers {
		namespace Fasm {
			/// <summary>
			/// Initializes a new instance of the <see cref="FasmAssemblerException" /> class.
			/// </summary>
			/// <param name="errorCode">The error code.</param>
			/// <param name="errorLine">The line where is the error.</param>
			/// <param name="errorOffset">The offset within the file where the line starts.</param>
			/// <param name="mnemonics">The assembled mnemonics when the error occurred.</param>
			FasmAssemblerException::FasmAssemblerException(FasmErrors errorCode, int errorLine, int errorOffset, String^ mnemonics)
				: ApplicationException(String::Format("An error occurred during FASM was assembling mnemonics. Error code: {0} ({1}); Error line: {2}; Error offset: {3}",
				(int)errorCode, errorCode, errorLine, errorOffset))
			{
				// Save the parameters
				_errorCode = errorCode;
				_errorLine = errorLine;
				_errorOffset = errorOffset;
				_mnemonics = mnemonics;
			}
		}
	}
}