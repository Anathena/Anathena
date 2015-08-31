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

/// <summary>
/// The native function to get the version of FASM compiler embedded in Fasm.obj.
/// </summary>
/// <returns>The return valus is a double word containg major version in lower 16 bits, and minor version in the higher 16 bits.</returns>
extern "C" int fasm_GetVersion();
/// <summary>
/// The native function to assemble mnemonics of FASM compiler embedded in Fasm.obj.
/// </summary>
/// <param name="source">The mnemonic to assemble.</param>
/// <param name="lpMemory">The pointer to a buffer used to assemble mnemonics.</param>
/// <param name="memorySize">The memory size allocated for the buffer.</param>
/// <param name="passLimit">The maximum number of pass to perform.</param>
/// <returns>The return value is a <see cref="FasmResults"> enumeration.</returns>
extern "C" int fasm_Assemble(char* szSource, byte* lpMemory, int nSize, int nPassesLimit, void* hDisplayPipe);