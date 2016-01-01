using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#pragma warning disable 1591
namespace SharpDisasm.Tests
{
    [TestClass]
    public class DisassemblerTests
    {
        [TestMethod]
        public void DisassembleBytesDecoded()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] {
                0x00, 0x00, // add [eax], al
                0x00, 0x67, // invalid
            }, ArchitectureMode.x86_32, 0, false);

            foreach (SharpDisasm.Instruction instruction in disasm.Disassemble())
            {
                Assert.IsTrue(instruction.Length > 0);
            }

            // Only 1 valid instruction is read that is 2 bytes long
            // The final 2 bytes are discarded as an invalid instruction
            Assert.AreEqual(2, disasm.BytesDecoded);
        }

        [TestMethod]
        public void DisassembleBufferTest()
        {
            var code = new byte[] {
                0x67, 0x66, 0x8b, 0x40, 0xf0                                    // mov ax, [eax-0x10]
                , 0x67, 0x66, 0x03, 0x5e, 0x10                                  // add bx, [esi+0x10]
                , 0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00                // add rax, [0xffff]
                , 0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0                            // add ax, [esi+edi*4-0x10]
                , 0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80                // add r8, [rax+rbx*4-0x80000000]
                , 0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00    // mov rax, [0x800000000000]
            };
            var disasm = new SharpDisasm.Disassembler(code, ArchitectureMode.x86_64, 0, false);

            var results = disasm.Disassemble().ToArray();
            Assert.AreEqual(6, results.Length, "There should be 6 instructions");

            Assert.AreEqual(4, (from insn in results
                     where insn.Length > 5
                     select insn).Count(), "There should be 4 instructions that are larger than 5 bytes");

            Assert.AreEqual("mov rax, [0x800000000000]", (from insn in results
                                where insn.Length == 10
                                select insn).First().ToString());


            foreach (SharpDisasm.Instruction instruction in results)
            {
                Assert.IsFalse(instruction.Error);
                Assert.IsTrue(instruction.Length > 0);
            }

            Assert.AreEqual(code.Length, disasm.BytesDecoded);

            foreach (var ins in results)
            {
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassembleVendorTest()
        {
            var bytes = new byte[] {
                0x0F, 0x01, 0xDD,            // clgi (AMD)
                0x66, 0x0F, 0x38, 0x80, 0x00 // invept eax,[eax] (intel)
            };
            
            // Any vendor
            var disam = new Disassembler(bytes, ArchitectureMode.x86_64, 0x0, false, Vendor.Any);
            foreach (var ins in disam.Disassemble())
            {
                Assert.IsFalse(ins.Error);
                Assert.AreNotEqual(Udis86.ud_mnemonic_code.UD_Iinvalid, ins.Mnemonic);
            }
            
            // AMD only
            disam = new Disassembler(bytes, ArchitectureMode.x86_64, 0x0, false, Vendor.AMD);
            var results = disam.Disassemble().ToArray();
            Assert.IsFalse(results.First().Error);
            Assert.IsTrue(results.Last().Error);

            // Intel only
            disam = new Disassembler(bytes, ArchitectureMode.x86_64, 0x0, false, Vendor.Intel);
            results = disam.Disassemble().ToArray();
            Assert.IsTrue(results.First().Error);
            Assert.IsFalse(results.Last().Error);
        }

        [TestMethod]
        public void DisassembleLargeBuffer()
        {
            var b = new byte[] {
                0x67, 0x66, 0x8b, 0x40, 0xf0                                    // mov ax, [eax-0x10]
                , 0x67, 0x66, 0x03, 0x5e, 0x10                                  // add bx, [esi+0x10]
                , 0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00                // add rax, [0xffff]
                , 0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0                            // add ax, [esi+edi*4-0x10]
                , 0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80                // add r8, [rax+rbx*4-0x80000000]
                , 0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00    // mov rax, [0x800000000000]
            };

            int iterations = 100000;
            List<byte> bytes = new List<byte>(b.Length * iterations);

            for (var i = 0; i < iterations; i++)
            {
                bytes.AddRange(b);
            }

            var disasm = new Disassembler(bytes.ToArray(), ArchitectureMode.x86_64, 0, false);

            Stopwatch sw = new Stopwatch();
            int instructionCount = 0;
            int totalBytes = 0;
            sw.Start();
            foreach (var ins in disasm.Disassemble())
            {
                instructionCount++;
                totalBytes += ins.Length;
                //var s = ins.ToString();
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);

            // Should be completed in less than 1 seconds even in debug (usually completes 600k instructions within 200-600ms)
            //Assert.IsTrue(sw.Elapsed < new TimeSpan(0, 0, 1));

            // Ensure correct number of instructions were disassembled
            Assert.AreEqual(6 * iterations, instructionCount);

            // Ensure correct number of bytes in total
            Assert.AreEqual(b.Length * iterations, totalBytes);
        }

        [TestMethod]
        public void DisassembleBufferOffset()
        {
            byte[] b = new byte[] { };

            var disasm = new SharpDisasm.Disassembler(new byte[] {
                0x67, 0x66, 0x8b, 0x40, 0xf0                                  // mov ax, [eax-0x10]
                , 0x67, 0x66, 0x03, 0x5e, 0x10                                // add bx, [esi+0x10]
                , 0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00              // add rax, [0xffff]
                , 0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0                          // add ax, [esi+edi*4-0x10]
                , 0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80              // add r8, [rax+rbx*4-0x80000000]
                , 0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00  // mov rax, [0x800000000000]
            }, ArchitectureMode.x86_64, 0x10000, false);
            var result = disasm.Disassemble().ToArray();
            Assert.AreEqual((ulong)0x10000, (ulong)disasm.Disassemble().First().Offset);

            foreach (var ins in disasm.Disassemble())
            {
                Assert.IsFalse(ins.Error);
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassembleScaleIndexBaseTests()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] { 
                0x8B, 0x04, 0xAA,                         // mov eax, [edx+ebp*4]
                0x8B, 0x44, 0x15, 0x00,                   // mov eax, [ebp+edx]
                0x8B, 0x04, 0x2A,                         // mov eax, [edx+ebp]
                0x8B, 0x45, 0x00,                         // mov eax, [ebp]
                0x8B, 0x04, 0x6D, 0x00, 0x00, 0x00, 0x00, // mov eax, [ebp*2]
                0x8B, 0x44, 0x2A, 0x85,                   // mov eax, [edx+ebp-0x7b]
            }, ArchitectureMode.x86_32, 0, false);

            var results = disasm.Disassemble().ToArray();

            // mov eax, [edx+ebp*4]
            Assert.AreEqual(4, results[0].Operands.Last().Scale);
            Assert.AreEqual(Udis86.ud_type.UD_R_EAX, results[0].Operands.First().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EDX, results[0].Operands.Last().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[0].Operands.Last().Index);

            // mov eax, [ebp+edx]
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[1].Operands.Last().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EDX, results[1].Operands.Last().Index);

            // mov eax, [edx+ebp]
            Assert.AreEqual(Udis86.ud_type.UD_R_EDX, results[2].Operands.Last().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[2].Operands.Last().Index);

            // mov eax, [ebp]
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[3].Operands.Last().Base);

            // mov eax, [ebp*2]
            Assert.AreEqual(Udis86.ud_type.UD_NONE, results[4].Operands.Last().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[4].Operands.Last().Index);
            Assert.AreEqual(2, results[4].Operands.Last().Scale);

            // mov eax, [edx+ebp-0x7b]
            Assert.AreEqual(Udis86.ud_type.UD_R_EDX, results[5].Operands.Last().Base);
            Assert.AreEqual(Udis86.ud_type.UD_R_EBP, results[5].Operands.Last().Index);
            Assert.AreEqual((long)-0x7b, results[5].Operands.Last().Value);
            
            foreach (var ins in disasm.Disassemble())
            {
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassembleInvalidInstruction()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xA1, 0x37
                },
               ArchitectureMode.x86_32, 0, false);
            Assert.AreEqual(disasm.Disassemble().Last().Mnemonic, Udis86.ud_mnemonic_code.UD_Iinvalid);
            foreach (var ins in disasm.Disassemble())
            {
                Assert.IsTrue(ins.Error);
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassembleTests()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xA1, 0xC9, 0xFD, 0xFF, 0xFF, // mov    eax,[0xfffffdc9] (or ds:0xfffffdc9)
                    0xA1, 0x37, 0x02, 0x00, 0x00, // mov    eax,[0x237] (or ds:0x237)
                    0xB8, 0x37, 0x02, 0x00, 0x00, // mov    eax,0x237
                    0xB4, 0x09,                   // mov    ah,0x9
                    0x8B, 0x04, 0x6D, 0x85, 0xFF, 0xFF, 0xFF, // mov eax, [ebp*2-0x7b]
                },
                ArchitectureMode.x86_32, 0, false);
            Assert.AreEqual((long)-0x7b, disasm.Disassemble().Last().Operands.Last().Value);
            foreach (var ins in disasm.Disassemble())
            {
                Debug.WriteLine(ins.ToString());
            }

            disasm = new SharpDisasm.Disassembler(new byte[] { 
                 0x67, 0x66, 0x03, 0x07,  // add ax, [bx]
                 0x66, 0xB8, 0xF7, 0xFF, // mov ax, 0xfff7 (or -0x9)
            }, ArchitectureMode.x86_64, 0, false);
            Assert.AreEqual((long)-9, disasm.Disassemble().Last().Operands.Last().Value);
            foreach (var ins in disasm.Disassemble())
            {
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void Disassemble64BitRIPRelative()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] { 
                 0x48, 0x8B, 0x05, 0xF7, 0xFF, 0xFF, 0xFF, // mov rax, [rip-0x9]
            }, ArchitectureMode.x86_64, 0, true);

            Assert.AreEqual((long)-9, disasm.Disassemble().Last().Operands.Last().Value);
            Assert.AreEqual("mov rax, [rip-0x9]", disasm.Disassemble().First().ToString());

            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;

            foreach (var ins in disasm.Disassemble())
            {
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassemblerPrintIntelSyntax()
        {
            var disasm = new SharpDisasm.Disassembler(new byte[] { 
                 0x48, 0x8B, 0x05, 0xF7, 0xFF, 0xFF, 0xFF,                    // mov rax, [rip-0x9]
                0x67, 0x66, 0x8b, 0x40, 0xf0                                  // mov ax, [eax-0x10]
                , 0x67, 0x66, 0x03, 0x5e, 0x10                                // add bx, [esi+0x10]
                , 0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00              // add rax, [0xffff]
                , 0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0                          // add ax, [esi+edi*4-0x10]
                , 0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80              // add r8, [rax+rbx*4-0x80000000]
                , 0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00  // mov rax, [0x800000000000]
            }, ArchitectureMode.x86_64, 0, true);

            var results = disasm.Disassemble().ToArray();
            foreach (var ins in results)
            {
                Debug.WriteLine(ins.ToString());
            }
            Debug.WriteLine("-------------------");

            disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xA1, 0xC9, 0xFD, 0xFF, 0xFF,       // mov    eax,[0xfffffdc9] (or ds:0xfffffdc9)
                    0xA1, 0x37, 0x02, 0x00, 0x00,       // mov    eax,[0x237] (or ds:0x237)
                    0xB8, 0x37, 0x02, 0x00, 0x00,       // mov    eax,0x237
                    0xB4, 0x09,                         // mov    ah,0x9
                    0x8A, 0x25, 0x09, 0x00, 0x00, 0x00, // mov    ah,[0x9]
                    0x8B, 0x04, 0x6D, 0x85, 0xFF, 0xFF, 0xFF, // mov eax, [ebp*2-0x7b]
                    0x89, 0x45, 0xEC,                   // mov DWORD [ebp-0x14],eax
                },
            ArchitectureMode.x86_32, 0x1000, true);

            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;

            results = disasm.Disassemble().ToArray();

            Assert.IsTrue(results.First().ToString().StartsWith("00001000 "), "Incorrect instruction address/IP/PC, expected 00001000");
            foreach (var ins in results)
            {
                Debug.WriteLine(ins.ToString());
            }
            Debug.WriteLine("-------------------");

            disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xC8, 0x04, 0x00, 0x00,             // enter  0x4, 0
                    0xA1, 0xC9, 0xFD, 0xFF, 0xFF,       // mov    eax,[0xfffffdc9] (or ds:0xfffffdc9)
                    0xA1, 0x37, 0x02, 0x00, 0x00,       // mov    eax,[0x237] (or ds:0x237)
                    0xB8, 0x37, 0x02, 0x00, 0x00,       // mov    eax,0x237
                    0xB4, 0x09,                         // mov    ah,0x9
                    0x8A, 0x25, 0x09, 0x00, 0x00, 0x00, // mov    ah,[0x9]
                    0x8B, 0x04, 0x6D, 0x85, 0xFF, 0xFF, 0xFF, // mov eax, [ebp*2-0x7b]
                    0x89, 0x45, 0xEC,                   // mov DWORD [ebp-0x14],eax
                },
            ArchitectureMode.x86_32, 0, false);

            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;

            results = disasm.Disassemble().ToArray();
            foreach (var ins in results)
            {
                Debug.WriteLine(ins.ToString());
            }
        }

        [TestMethod]
        public void DisassemblerPrintATTSyntax()
        {
            var defaultTranslator = SharpDisasm.Disassembler.Translator;
            // Set translator to AT&T Syntax
            SharpDisasm.Disassembler.Translator = new SharpDisasm.Translators.ATTTranslator()
            {
                IncludeAddress = true,
                IncludeBinary = false,
                SymbolResolver = null
            };
            try
            {
                var disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0x48, 0x8B, 0x05, 0xF7, 0xFF, 0xFF, 0xFF,                    // movq -0x9(%rip), %rax
                    0x67, 0x66, 0x8b, 0x40, 0xf0,                                // movw -0x10(%eax), %ax
                    0x67, 0x66, 0x03, 0x5e, 0x10,                                // addw 0x10(%esi), %bx
                    0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00,              // addq 0xffff, %rax
                    0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0,                          // addw -0x10(%esi,%edi,4), %ax
                    0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80,              // addq -0x80000000(%rax,%rbx,4), %r8
                    0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00,  // movq 0x800000000000, %rax
                    0x67, 0x8D, 0x04, 0x85, 0x08, 0x00, 0x00, 0x00,              // leal 0x8(%eax,4), %eax
                }, ArchitectureMode.x86_64, 0, true);

                var results = disasm.Disassemble().ToArray();
                foreach (var ins in results)
                {
                    Debug.WriteLine(ins.ToString());
                }
                Debug.WriteLine("-------------------");

                disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xA1, 0xC9, 0xFD, 0xFF, 0xFF,                   // movl 0xfffffdc9, %eax
                    0xA1, 0x37, 0x02, 0x00, 0x00,                   // movl 0x237, %eax
                    0xB8, 0x37, 0x02, 0x00, 0x00,                   // mov $0x237, %eax
                    0xB4, 0x09,                                     // mov $0x9, %ah
                    0x8A, 0x25, 0x09, 0x00, 0x00, 0x00,             // movb 0x9, %ah
                    0x8B, 0x04, 0x6D, 0x85, 0xFF, 0xFF, 0xFF,       // movl -0x7b(%ebp,2), %eax
                    0x89, 0x45, 0xEC,                               // movl %eax, -0x14(%ebp)
                    0xB8, 0x05, 0x00, 0x00, 0x00,                   // mov $0x5, %eax
                    0x8B, 0x45, 0x04,                               // movl 0x4(%ebp), %eax
                    0x66, 0x8B, 0x8C, 0x83, 0x00, 0x00, 0x00, 0x00, // movw (%ebx,%eax,4), %cx
                    0x8D, 0x04, 0x85, 0x08, 0x00, 0x00, 0x00,       // leal 0x8(%eax,4), %eax
                    0x67, 0x89, 0x07,                               // movl %eax, (%bx)
                    0xA3, 0xFF, 0x00, 0x00, 0x00,                   // movl %eax, 0xff
                    0x83, 0xEC, 0x12,                               // sub $0x12, %esp
                },
                ArchitectureMode.x86_32, 0x1000, true);

                Disassembler.Translator.IncludeAddress = true;
                Disassembler.Translator.IncludeBinary = true;

                results = disasm.Disassemble().ToArray();

                Assert.IsTrue(results.First().ToString().StartsWith("00001000 "), "Incorrect instruction address/IP/PC, expected 00001000");
                foreach (var ins in results)
                {
                    Debug.WriteLine(ins.ToString());
                }
                Debug.WriteLine("-------------------");

                disasm = new SharpDisasm.Disassembler(new byte[] { 
                    0xA1, 0xC9, 0xFD, 0xFF, 0xFF,               // movl 0xfffffdc9, %eax
                    0xA1, 0x37, 0x02, 0x00, 0x00,               // movl 0x237, %eax
                    0xB8, 0x37, 0x02, 0x00, 0x00,               // mov $0x237, %eax
                    0xB4, 0x09,                                 // mov $0x9, %ah
                    0x8A, 0x25, 0x09, 0x00, 0x00, 0x00,         // movb 0x9, %ah
                    0x8B, 0x04, 0x6D, 0x85, 0xFF, 0xFF, 0xFF,   // movl -0x7b(%ebp,2), %eax
                    0x89, 0x45, 0xEC,                           // movl %eax, -0x14(%ebp)
                },
                ArchitectureMode.x86_32, 0, false);

                Disassembler.Translator.IncludeAddress = true;
                Disassembler.Translator.IncludeBinary = true;

                results = disasm.Disassemble().ToArray();
                foreach (var ins in results)
                {
                    Debug.WriteLine(ins.ToString());
                }
            }
            finally
            {
                SharpDisasm.Disassembler.Translator = defaultTranslator;
            }
        }
    }
}
