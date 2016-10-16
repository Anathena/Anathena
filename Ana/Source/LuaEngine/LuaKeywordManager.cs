namespace Ana.Source.LuaEngine
{
    using Graphics;
    using Hook;
    using Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utils.Extensions;

    internal class LuaKeywordManager
    {
        private static Boolean sorted = false;

        private static String luaKeywords = "and break do else elseif end false for function if" +
                " in local nil not or repeat return then true until while" +
                " _VERSION assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " print rawget rawset require tonumber tostring type unpack" +
                " assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " abs acos asin atan atan2 ceil cos deg exp" +
                " floor format frexp gsub ldexp log log10 max min mod rad random randomseed" +
                " sin sqrt strbyte strchar strfind strlen strlower strrep strsub strupper tan" +
                " string.byte string.char string.dump string.find string.len";

        private static String asmRegisterKeywords = "rax rbx rcx rdx rbp rsi rdi rsp r8 r9 r10 r11 r12 r13 r14 r15 rip" +
                " eax ebx ecx edx ebp esi edi esp r8d r9d r10d r11d r12d r13d r14d r15d eip" +
                " ax bx cx dx bp si di sp r8w r9w r10w r11w r12w r13w r14w r15w" +
                " al bl cl dl bpl sil dil spl r8b r9b r10b r11b r12b r13b r14b r15b" +
                " ah bh ch dh" +
                " fpr0 fpr1 fpr2 fpr3 fpr4 fpr5 fpr6 fpr7 mmx0 mmx1 mmx2 mmx3 mmx4 mmx5 mmx6 mmx7" +
                " xmm0 xmm1 xmm2 xmm3 xmm4 xmm5 xmm6 xmm7 xmm8 xmm9 xmm10 xmm11 xmm12 xmm13 xmm14 xmm15";

        private static string asmInstructionKeywords = "AAA AAD AAM AAS ADC ADCX ADD ADDPD ADDPS ADDSD ADDSS ADDSUBPD" +
            " ADDSUBPS ADOX AESDEC AESDECLAST AESENC AESENCLAST AESIMC AESKEYGENASSIST AND ANDN ANDNPD ANDNPS ANDPD ANDPS" +
            " ARPL BEXTR BLENDPD BLENDPS BLENDVPD BLENDVPS BLSI BLSMSK BLSR BOUND BSF BSR BSWAP BT BTC BTR BTS BZHI CALL" +
            " CBW CDQ CDQE CLAC CLC CLD CLFLUSH CLI CLTS CMC CMOVcc CMP CMPPD CMPPS CMPS CMPSB CMPSD CMPSD CMPSQ CMPSS CMPSW" +
            " CMPXCHG CMPXCHG16B CMPXCHG8B COMISD COMISS CPUID CQO CRC32 CVTDQ2PD CVTDQ2PS CVTPD2DQ CVTPD2PI CVTPD2PS CVTPI2PD" +
            " CVTPI2PS CVTPS2DQ CVTPS2PD CVTPS2PI CVTSD2SI CVTSD2SS CVTSI2SD CVTSI2SS CVTSS2SD CVTSS2SI CVTTPD2DQ CVTTPD2PI" +
            " CVTTPS2DQ CVTTPS2PI CVTTSD2SI CVTTSS2SI CWD CWDE DAA DAS DEC DIV DIVPD DIVPS DIVSD DIVSS DPPD DPPS EMMS ENTER" +
            " EXTRACTPS F2XM1 FABS FADD FADDP FBLD FBSTP FCHS FCLEX FCMOVcc FCOM FCOMI FCOMIP FCOMP FCOMPP FCOS FDECSTP FDIV" +
            " FDIVP FDIVR FDIVRP FFREE FIADD FICOM FICOMP FIDIV FIDIVR FILD FIMUL FINCSTP FINIT FIST FISTP FISTTP FISUB FISUBR" +
            " FLD FLD1 FLDCW FLDENV FLDL2E FLDL2T FLDLG2 FLDLN2 FLDPI FLDZ FMUL FMULP FNCLEX FNINIT FNOP FNSAVE FNSTCW FNSTENV" +
            " FNSTSW FPATAN FPREM FPREM1 FPTAN FRNDINT FRSTOR FSAVE FSCALE FSIN FSINCOS FSQRT FST FSTCW FSTENV FSTP FSTSW FSUB" +
            " FSUBP FSUBR FSUBRP FTST FUCOM FUCOMI FUCOMIP FUCOMP FUCOMPP FWAIT FXAM FXCH FXRSTOR FXSAVE FXTRACT FYL2X FYL2XP1" +
            " HADDPD HADDPS HLT HSUBPD HSUBPS IDIV IMUL IN INC INS INSB INSD INSERTPS INSW INT3 INTn INTO INVD INVLPG INVPCID" +
            " IRET IRETD JMP Jcc LAHF LAR LDDQU LDMXCSR LDS LEA LEAVE LES LFENCE LFS LGDT LGS LIDT LLDT LMSW LOCK LODS LODSB" +
            " LODSD LODSQ LODSW LOOP LOOPcc LSL LSS LTR LZCNT MASKMOVDQU MASKMOVQ MAXPD MAXPS MAXSD MAXSS MFENCE MINPD MINPS" +
            " MINSD MINSS MONITOR MOV MOV MOV MOVAPD MOVAPS MOVBE MOVD MOVDDUP MOVDQ2Q MOVDQA MOVDQU MOVHLPS MOVHPD MOVHPS" +
            " MOVLHPS MOVLPD MOVLPS MOVMSKPD MOVMSKPS MOVNTDQ MOVNTDQA MOVNTI MOVNTPD MOVNTPS MOVNTQ MOVQ MOVQ MOVQ2DQ MOVS" +
            " MOVSB MOVSD MOVSD MOVSHDUP MOVSLDUP MOVSQ MOVSS MOVSW MOVSX MOVSXD MOVUPD MOVUPS MOVZX MPSADBW MUL MULPD MULPS" +
            " MULSD MULSS MWAIT MULX MWAIT NEG NOP NOT OR ORPD ORPS OUT OUTS OUTSB OUTSD OUTSW PABSB PABSD PABSW PACKSSDW" +
            " PACKSSWB PACKUSDW PACKUSWB PADDB PADDD PADDQ PADDSB PADDSW PADDUSB PADDUSW PADDW PALIGNR PAND PANDN PAUSE" +
            " PAVGB PAVGW PBLENDVB PBLENDW PCLMULQDQ PCMPEQB PCMPEQD PCMPEQQ PCMPEQW PCMPESTRI PCMPESTRM PCMPGTB PCMPGTD" +
            " PCMPGTQ PCMPGTW PCMPISTRI PCMPISTRM PDEP PEXT PEXTRB PEXTRD PEXTRQ PEXTRW PHADDD PHADDSW PHADDW PHMINPOSUW PHSUBD" +
            " PHSUBSW PHSUBW PINSRB PINSRD PINSRQ PINSRW PMADDUBSW PMADDWD PMAXSB PMAXSD PMAXSW PMAXUB PMAXUD PMAXUW PMINSB" +
            " PMINSD PMINSW PMINUB PMINUD PMINUW PMOVMSKB PMOVSX PMOVZX PMULDQ PMULHRSW PMULHUW PMULHW PMULLD PMULLW PMULUDQ" +
            " POP POPA POPAD POPCNT POPF POPFD POPFQ POR PREFETCHW PREFETCHWT1 PREFETCHh PSADBW PSHUFB PSHUFD PSHUFHW PSHUFLW" +
            " PSHUFW PSIGNB PSIGND PSIGNW PSLLD PSLLDQ PSLLQ PSLLW PSRAD PSRAW PSRLD PSRLDQ PSRLQ PSRLW PSUBB PSUBD PSUBQ PSUBSB" +
            " PSUBSW PSUBUSB PSUBUSW PSUBW PTEST PUNPCKHBW PUNPCKHDQ PUNPCKHQDQ PUNPCKHWD PUNPCKLBW PUNPCKLDQ PUNPCKLQDQ" +
            " PUNPCKLWD PUSH PUSHA PUSHAD PUSHF PUSHFD PXOR RCL RCPPS RCPSS RCR RDFSBASE RDGSBASE RDMSR RDPMC RDRAND RDSEED" +
            " RDTSC RDTSCP REP REPE REPNE REPNZ REPZ RET ROL ROR RORX ROUNDPD ROUNDPS ROUNDSD ROUNDSS RSM RSQRTPS RSQRTSS SAHF" +
            " SAL SAR SARX SBB SCAS SCASB SCASD SCASW SETcc SFENCE SGDT SHL SHLD SHLX SHR SHRD SHRX SHUFPD SHUFPS SIDT SLDT" +
            "SMSW SQRTPD SQRTPS SQRTSD SQRTSS STAC STC STD STI STMXCSR STOS STOSB STOSD STOSQ STOSW STR SUB SUBPD SUBPS SUBSD" +
            " SUBSS SWAPGS SYSCALL SYSENTER SYSEXIT SYSRET TEST TZCNT UCOMISD UCOMISS UD2 UNPCKHPD UNPCKHPS UNPCKLPD UNPCKLPS" +
            " VBROADCAST VCVTPH2PS VCVTPS2PH VERR VERW VEXTRACTF128 VEXTRACTI128 VFMADD132PD VFMADD132PS VFMADD132SD VFMADD132SS" +
            " VFMADD213PD VFMADD213PS VFMADD213SD VFMADD213SS VFMADD231PD VFMADD231PS VFMADD231SD VFMADD231SS VFMADDSUB132PD" +
            " VFMADDSUB132PS VFMADDSUB213PD VFMADDSUB213PS VFMADDSUB231PD VFMADDSUB231PS VFMSUB132PD VFMSUB132PS VFMSUB132SD" +
            " VFMSUB132SS VFMSUB213PD VFMSUB213PS VFMSUB213SD VFMSUB213SS VFMSUB231PD VFMSUB231PS VFMSUB231SD VFMSUB231SS" +
            " VFMSUBADD132PD VFMSUBADD132PS VFMSUBADD213PD VFMSUBADD213PS VFMSUBADD231PD VFMSUBADD231PS VFNMADD132PD VFNMADD132PS" +
            " VFNMADD132SD VFNMADD132SS VFNMADD213PD VFNMADD213PS VFNMADD213SD VFNMADD213SS VFNMADD231PD VFNMADD231PS VFNMADD231SD" +
            " VFNMADD231SS VFNMSUB132PD VFNMSUB132PS VFNMSUB132SD VFNMSUB132SS VFNMSUB213PD VFNMSUB213PS VFNMSUB213SD VFNMSUB213SS" +
            " VFNMSUB231PD VFNMSUB231PS VFNMSUB231SD VFNMSUB231SS VGATHERDPD VGATHERDPS VGATHERQPD VGATHERQPS VINSERTF128 VINSERTI128" +
            " VMASKMOV VPBLENDD VPBROADCAST VPERM2F128 VPERM2I128 VPERMD VPERMILPD VPERMILPS VPERMPD VPERMPS VPERMQ VPGATHERDD" +
            " VPGATHERDQ VPGATHERQD VPGATHERQQ VPMASKMOV VPSLLVD VPSLLVQ VPSRAVD VPSRLVD VPSRLVQ VTESTPD VTESTPS VZEROALL VZEROUPPER" +
            " WAIT WBINVD WRFSBASE WRGSBASE WRMSR XABORT XACQUIRE XADD XBEGIN XCHG XEND XGETBV XLAT XLATB XOR XORPD XORPS XRELEASE" +
            " XRSTOR XRSTORS XSAVE XSAVEC XSAVEOPT XSAVES XSETBV XTEST";

        private static String allLuaKeywords = String.Empty;

        private static String allAsmKeywords = String.Empty;

        private static String anathenaKeywords = "Memory Graphics Hook fasm nasm masm";

        public static String LuaKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return LuaKeywordManager.luaKeywords;
            }

            private set
            {
                LuaKeywordManager.luaKeywords = value;
            }
        }

        public static String AsmRegisterKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return LuaKeywordManager.asmRegisterKeywords;
            }

            private set
            {
                LuaKeywordManager.asmRegisterKeywords = value;
            }
        }

        public static String AsmInstructionKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return LuaKeywordManager.asmInstructionKeywords;
            }

            private set
            {
                LuaKeywordManager.asmInstructionKeywords = value;
            }
        }

        public static String AnathenaKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return LuaKeywordManager.anathenaKeywords;
            }

            private set
            {
                LuaKeywordManager.anathenaKeywords = value;
            }
        }

        public static String AllLuaKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return allLuaKeywords;
            }

            private set
            {
                LuaKeywordManager.allLuaKeywords = value;
            }
        }

        public static String AllAsmKeywords
        {
            get
            {
                LuaKeywordManager.SortKeywords();
                return LuaKeywordManager.allAsmKeywords;
            }

            private set
            {
                LuaKeywordManager.allAsmKeywords = value;
            }
        }

        private static void SortKeywords()
        {
            if (LuaKeywordManager.sorted)
            {
                return;
            }

            List<String> keywords;
            String sortedKeywords;

            // Sort Lua keywords
            sortedKeywords = String.Empty;
            keywords = new List<String>(luaKeywords.Split(' '));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x + " ");
            luaKeywords = sortedKeywords;

            // Sort Asm register keywords
            sortedKeywords = String.Empty;
            keywords = new List<String>(asmRegisterKeywords.Split(' '));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x.ToLower() + " ");
            asmRegisterKeywords = sortedKeywords;

            // Sort Asm instruction keywords
            sortedKeywords = String.Empty;
            keywords = new List<String>(asmInstructionKeywords.Split(' '));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x.ToLower() + " ");
            asmInstructionKeywords = sortedKeywords;

            // Sort Anathena keywords
            List<String> functionKeywords = new List<String>();
            typeof(IMemoryCore).GetMethods().ForEach(X => functionKeywords.Add(X.Name));
            typeof(IGraphicsCore).GetMethods().ForEach(X => functionKeywords.Add(X.Name));
            typeof(IHookCore).GetMethods().ForEach(X => functionKeywords.Add(X.Name));
            anathenaKeywords = String.Join(" ", anathenaKeywords.Split(' ').Concat(functionKeywords));
            sortedKeywords = String.Empty;
            keywords = new List<String>(anathenaKeywords.Split(' '));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x + " ");
            anathenaKeywords = sortedKeywords;

            // Sort all Lua keywords
            sortedKeywords = String.Empty;
            keywords = new List<String>(luaKeywords.Split(' ').Concat(anathenaKeywords.Split(' ')));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x + " ");
            allLuaKeywords = sortedKeywords;

            // Sort all Asm keywords
            sortedKeywords = String.Empty;
            keywords = new List<String>(asmRegisterKeywords.Split(' ').Concat(asmInstructionKeywords.Split(' ')));
            keywords.Sort();
            keywords.ForEach(x => sortedKeywords += x + " ");
            allAsmKeywords = sortedKeywords;

            sorted = true;
        }
    }
    //// End class
}
//// End namespace