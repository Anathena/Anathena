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

namespace Binarysharp {
	namespace Assemblers {
		namespace Fasm {
			/// <summary>
			/// The enumeration containing all results of FASM compiler.
			/// </summary>
			public enum class FasmResults
			{
				Ok = 0,
				Working = 1,
				Error = 2,
				InvalidParameter = -1,
				OutOfMemory = -2,
				StackOverflow = -3,
				SourceNotFound = -4,
				UnexpectedEndOfSource = -5,
				CannotGenerateCode = -6,
				FormatLimitationsExcedded = -7,
				WriteFailed = -8
			};

			/// <summary>
			/// The enumeration containing all errors of FASM compiler.
			/// </summary>
			public enum class FasmErrors
			{
				FileNotFound = -101,
				ErrorReadingFile = -102,
				InvalidFileFormat = -103,
				InvalidMacroArguments = -104,
				IncompleteMacro = -105,
				UnexpectedCharacters = -106,
				InvalidArgument = -107,
				IllegalInstruction = -108,
				InvalidOperand = -109,
				InvalidOperandSize = -110,
				OperandSizeNotSpecified = -111,
				OperandSizesDoNotMatch = -112,
				InvalidAddressSize = -113,
				AddressSizesDoNotAgree = -114,
				DisallowedCombinationOfRegisters = -115,
				LongImmediateNotEncodable = -116,
				RelativeJumpOutOfRange = -117,
				InvalidExpression = -118,
				InvalidAddress = -119,
				InvalidValue = -120,
				ValueOutOfRange = -121,
				UndefinedSymbol = -122,
				InvalidUseOfSymbol = -123,
				NameTooLong = -124,
				InvalidName = -125,
				ReservedWordUsedAsSymbol = -126,
				SymbolAlreadyDefined = -127,
				MissingEndQuote = -128,
				MissingEndDirective = -129,
				UnexpectedInstruction = -130,
				ExtraCharactersOnLine = -131,
				SectionNotAlignedEnough = -132,
				SettingAlreadySpecified = -133,
				DataAlreadyDefined = -134,
				TooManyRepeats = -135,
				SymbolOutOfScope = -136,
				UserError = -140,
				AssertionFailed = -141
			};
		}
	}
}