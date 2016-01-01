using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDisasm.Udis86;

#pragma warning disable 1591
namespace SharpDisasm.Tests
{
    [TestClass]
    public class Decode64bitTests
    {
        [TestInitialize]
        public void Initialise()
        {
        }

        [TestMethod]
        public void Disp64Test()
        {
            var disasm = new Disassembler(new byte[] {
                0x67, 0x66, 0x8b, 0x40, 0xf0      
                , 0x67, 0x66, 0x03, 0x5e, 0x10      
                , 0x48, 0x03, 0x04, 0x25, 0xff, 0xff, 0x00, 0x00
                , 0x67, 0x66, 0x03, 0x44, 0xbe, 0xf0    
                , 0x4c, 0x03, 0x84, 0x98, 0x00, 0x00, 0x00, 0x80
                , 0x48, 0xa1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00
            }, ArchitectureMode.x86_64);
//0000000000000000 67668b40f0       mov ax, [eax-0x10]      
//0000000000000005 6766035e10       add bx, [esi+0x10]      
//000000000000000a 48030425ffff0000 add rax, [0xffff]       
//0000000000000012 67660344bef0     add ax, [esi+edi*4-0x10]
//0000000000000018 4c03849800000080 add r8, [rax+rbx*4-0x80000000]
//0000000000000020 48a1000000000080 mov rax, [0x800000000000]

            Instruction insn = null;

            insn = disasm.NextInstruction();
            Assert.AreEqual("mov ax, [eax-0x10]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("add bx, [esi+0x10]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("add rax, [0xffff]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("add ax, [esi+edi*4-0x10]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("add r8, [rax+rbx*4-0x80000000]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("mov rax, [0x800000000000]", insn.ToString());

        }

        [TestMethod]
        public void NegativeRIPAddress()
        {
            var disasm = new Disassembler(new byte[] {
                0x48, 0x8B, 0x05, 0xF7, 0xFF, 0xFF, 0xFF, // mov rax, [rip-0x9]
                0xFF, 0x15, 0xF7, 0xFF, 0xFF, 0xFF,       // call qword [rip-0x9]
            }, ArchitectureMode.x86_64);

            Instruction insn = null;

            insn = disasm.NextInstruction();
            Assert.AreEqual("mov rax, [rip-0x9]", insn.ToString());

            insn = disasm.NextInstruction();
            Assert.AreEqual("call qword [rip-0x9]", insn.ToString());
        }
    }
}
