using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SharpDisasm.Udis86;

#pragma warning disable 1591
namespace SharpDisasm.Tests
{
    [TestClass]
    public class Decode32bitTests
    {
        [TestInitialize]
        public void Initialise()
        {
        }

        [TestMethod]
        public void Decode32Test()
        {
            var disasm = new Disassembler(new byte[] {
                0xb8, 0x34, 0x12, 0x00, 0x00,   // mov eax, 0x1234
                0xa1, 0x34, 0x12, 0x00, 0x00,   // mov eax, [0x1234]
                0x89, 0x45, 0xEC,               // mov [ebp-0x14], eax
                0x83, 0xe2, 0xdf,               // and edx, 0xffffffdf
            }, ArchitectureMode.x86_32);

            var insn = disasm.NextInstruction();
            Assert.AreEqual("mov eax, 0x1234", insn.ToString());
            Assert.AreEqual(5, insn.Length);

            insn = disasm.NextInstruction();
            Assert.AreEqual("mov eax, [0x1234]", insn.ToString());
            Assert.AreEqual(5, insn.Length);

            insn = disasm.NextInstruction();
            Assert.AreEqual("mov [ebp-0x14], eax", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("and edx, 0xffffffdf", insn.ToString());
        }

        [TestMethod]
        public void Corner32Test()
        {
            var disasm = new Disassembler(new byte[] {
                0x67, 0x0f, 0x02, 0x00,
                0x90,
                0xf3, 0x90
            }, ArchitectureMode.x86_32);
//0000000000000000 670f0200         lar eax, word [bx+si]   
//0000000000000004 90               nop                     
//0000000000000005 f390             pause                   

            var insn = disasm.NextInstruction();
            Assert.AreEqual("lar eax, word [bx+si]", insn.ToString());
            Assert.AreEqual(4, insn.Length);

            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            Assert.AreEqual(1, insn.Length);

            insn = disasm.NextInstruction();
            Assert.AreEqual("pause", insn.ToString());
            Assert.AreEqual(2, insn.Length);
        }

        [TestMethod]
        public void InvalidSeg32Test()
        {
            var disasm = new Disassembler(new byte[] {
                0x8c, 0x38
            }, ArchitectureMode.x86_32);

            var insn = disasm.NextInstruction();
            Assert.AreEqual("invalid", insn.ToString());
        }

        [TestMethod]
        public void RelativeJump32Test()
        {
            // set program counter offset
            var disasm = new Disassembler(new byte[] {
                0x90,
                0x90,
                0x90,
                0x90,
                0x90,
                0xeb, 0xf9,
                0x90,
                0x66, 0xe9, 0x0a, 0x00,
                0x90,
                0x90,
                0xe9, 0x03, 0x00, 0x00, 0x00,
                0x90,
                0x90,
                0x90,
                0x90,
                0x90,
                0xeb, 0xe6,
                0x89, 0x45, 0xEC,
            }, ArchitectureMode.x86_32, 0x80000000);
/*
0000000080000000 90               nop                     
0000000080000001 90               nop                     
0000000080000002 90               nop                     
0000000080000003 90               nop                     
0000000080000004 90               nop                     
0000000080000005 ebf9             jmp 0x80000000          
0000000080000007 90               nop                     
0000000080000008 66e90a00         jmp 0x16                
000000008000000c 90               nop                     
000000008000000d 90               nop                     
000000008000000e e903000000       jmp 0x80000016          
0000000080000013 90               nop                     
0000000080000014 90               nop                     
0000000080000015 90               nop                     
0000000080000016 90               nop                     
0000000080000017 90               nop                     
0000000080000018 ebe6             jmp 0x80000000  
    * */

            var insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("jmp 0x80000000", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("jmp 0x16", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("jmp 0x80000016", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());
            insn = disasm.NextInstruction();
            Assert.AreEqual("nop", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("jmp 0x80000000", insn.ToString());
        }

        [TestMethod]
        public void Obscure32Test()
        {
            var disasm = new Disassembler(new byte[] {
                0xd1, 0xf6,
                0xd0, 0xf6,
                0xd9, 0xd9,
                0xdc, 0xd0, 
                0xdc, 0xd8,
                0xdd, 0xc8,
                0xde, 0xd1,
                0xdf, 0xc3,
                0xdf, 0xd0,
                0xdf, 0xd8,
            }, ArchitectureMode.x86_32);
//0000000000000000 d1f6             shl esi, 1              
//0000000000000002 d0f6             shl dh, 1               
//0000000000000004 d9d9             fstp1 st1               
//0000000000000006 dcd0             fcom2 st0               
//0000000000000008 dcd8             fcomp3 st0              
//000000000000000a ddc8             fxch4 st0               
//000000000000000c ded1             fcomp5 st1              
//000000000000000e dfc3             ffreep st3              
//0000000000000010 dfd0             fstp8 st0               
//0000000000000012 dfd8             fstp9 st0               
//0000000000000014 83e2df           and edx, 0xffffffdf     

            var insn = disasm.NextInstruction();
            Assert.AreEqual("shl esi, 1", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("shl dh, 1", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fstp1 st1", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fcom2 st0", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fcomp3 st0", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fxch4 st0", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fcomp5 st1", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("ffreep st3", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fstp8 st0", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("fstp9 st0", insn.ToString());
        }
    }
}
