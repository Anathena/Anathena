SharpDisasm
===========

SharpDisam is a C# disassembler able to decode binary executable code for the x86 and x86-64 CPU architectures into disassembled instructions.

[![Build status](https://ci.appveyor.com/api/projects/status/vpqkaxrpceg7esgo?svg=true)](https://ci.appveyor.com/project/spazzarama/sharpdisam)

About
-----

The disassembler is able to decode more than 4 million 64-bit instructions a second (with an average instruction length of 7-bytes). When also translating the instructions to Intel syntax the number of instructions per second is around 2 million instructions per second.

The library is a C# port of the Udis86 disassembler originally written in C. The ported portion of SharpDisam is a straight port of the C Udis86 library to C# with no attempt to change the logic and make the code base more C# friendly. This was done intentionally so that future updates to the Udis86 library can be ported across without too much hassle. The SharpDisam.Disassembler class wraps the original Udis86 API in order to present a C# friendly interface to the underlying API.

The opcode table "optable.xml" is used to generate the opcode lookup tables with a T4 template "OpTable.tt". This generates an output that is comparable to the output of the original Python scripts used with Udis86 (ud_itab.py and ud_opcode.py).

Classes
-------

 * **SharpDisasm.Disassembler** - provides convenient access to the underlying libudis86 implementation through an enumerator or by explicitly requesting the next instruction to be decoded.
 * **SharpDisasm.Instruction** - represents a decoded instruction.
 * **SharpDisasm.Operand** - represents an operand of a decoded instruction.
 * **SharpDisasm.Translators.Translator** - abstract base class for implementing translators to output an instruction to assembler code.
 * **SharpDisasm.Translators.IntelTranslator** - an Intel syntax translator. This is the default translator (found on the static SharpDisasm.Disassembler.Translator property)
 * **SharpDisasm.Translators.ATTTranslator** - an AT&T syntax translator. Assign an instance of this to the SharpDisasm.Disassembler.Translator property to use this syntax.

Example
-------

Below is the output of the following console application that decodes a Hex string into instructions. It can accept instructions typed in as a single line, or piped in from a the command line or a text file.

    C:\>echo a1 c9 fd ff ff a1 37 02 00 00 b8 37 02 00 00 b4 09 8a 
    25 09 00 00 00 8b 04 6d 85 ff ff ff 89 45 f0| disasmcli 32
    
    00000000 a1 c9 fd ff ff                 mov eax, [0xfffffdc9]
    00000005 a1 37 02 00 00                 mov eax, [0x237]
    0000000a b8 37 02 00 00                 mov eax, 0x237
    0000000f b4 09                          mov ah, 0x9
    00000011 8a 25 09 00 00 00              mov ah, [0x9]
    00000017 8b 04 6d 85 ff ff ff           mov eax, [ebp*2-0x7b]
    0000001e 89 45 f0                       mov [ebp-0x10], eax
    
    C:\>echo 488b05f7ffffff67668b40f06766035e1048030425ffff
    000067660344bef04c0384980000008048a10000000000800000 | disasmcli 64
    
    0000000000000000 48 8b 05 f7 ff ff ff           mov rax, [rip-0x9]
    0000000000000007 67 66 8b 40 f0                 mov ax, [eax-0x10]
    000000000000000c 67 66 03 5e 10                 add bx, [esi+0x10]
    0000000000000011 48 03 04 25 ff ff 00 00        add rax, [0xffff]
    0000000000000019 67 66 03 44 be f0              add ax, [esi+edi*4-0x10]
    000000000000001f 4c 03 84 98 00 00 00 80        add r8, [rax+rbx*4-0x80000000]
    0000000000000027 48 a1 00 00 00 00 00 80 00 00  mov rax, [0x800000000000]

Here is the source of the **disasmcli** console application used above.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    namespace disasmcli
    {
        class Program
        {
            static void Main(string[] args)
            {
                // Determine the architecture mode or us 32-bit by default
                SharpDisasm.ArchitectureMode mode = SharpDisasm.ArchitectureMode.x86_32;
                if (args.Length > 0)
                {
                    switch (args[0])
                    {
                        case "16": { mode = SharpDisasm.ArchitectureMode.x86_16; break; }
                        case "32": { mode = SharpDisasm.ArchitectureMode.x86_32; break; }
                        case "64": { mode = SharpDisasm.ArchitectureMode.x86_64; break; }
                        default:
                            break;
                    }
                }
                // Allow input >256 chars
                Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                StringBuilder input = new StringBuilder();
                while (Console.In.Peek() != -1)
                {
                    input.Append(Console.In.ReadLine());
                }
                // Configure the translator to output instruction addresses and instruction binary as hex
                SharpDisasm.Disassembler.Translator.IncludeAddress = true;
                SharpDisasm.Disassembler.Translator.IncludeBinary = true;
                
                // Create the disassembler
                var disasm = new SharpDisasm.Disassembler(
                    HexStringToByteArray(input.ToString().Replace(" ", "")), 
                    mode, 0, true);
                // Disassemble each instruction and output to console
                foreach (var insn in disasm.Disassemble())
                    Console.Out.WriteLine(insn.ToString());
            }
            static byte[] HexStringToByteArray(string hex)
            {
                return Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();
            }
        }
    }

LICENSE
-------

SharpDisam is Copyright (c) 2015 Justin Stenning and is distributed under the 2-clause "Simplified BSD License". 

Portions of the project are ported from Udis86 Copyright (c) 2002-2012, Vivek Thampi <vivek.mt@gmail.com> https://github.com/vmt/udis86 distributed under the 2-clause "Simplified BSD License".