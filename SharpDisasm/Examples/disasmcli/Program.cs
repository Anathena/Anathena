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
