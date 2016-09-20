namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm.Udis86
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// <para>The C# udis86 library ported from C</para>
    /// <para>For more information about how to use the C-based library see the udis86 project on GitHub https://github.com/vmt/udis86 </para>
    /// <para>This static class is thread safe ONLY WHEN using separate <see cref="Ud"/> instances.</para>
    /// </summary>
    /// <remarks>This class is deliberately written to match as closely as possible to the original C-library.</remarks>
    public static unsafe class Udis86
    {
        private static Decode decode = new Decode();

        /// <summary>
        /// Initializes ud_t object.
        /// </summary>
        /// <param name="u"></param>
        public static void UdInit(ref Ud u)
        {
            u = new Ud();
            UdSetMode(ref u, 16);
            u.Mnemonic = UdMnemonicCode.UD_Iinvalid;
            UdSetPc(ref u, 0);

            UdSetAsmBuffer(ref u, u.AsmBufferInt, u.AsmBufferInt.Length);
        }

        /// <summary>
        /// Disassembles one instruction and returns the number of bytes disassembled. A zero means end of disassembly.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static Int32 UdDisassemble(ref Ud u)
        {
            Int32 len;

            if (u.InputEnd > 0)
            {
                return 0;
            }

            if ((len = decode.UdDecode(ref u)) > 0)
            {
                if (u.Translator != null)
                {
                    for (Int32 i = 0; i < u.AsmBuffer.Length; i++)
                    {
                        u.AsmBuffer[i] = '\0';
                    }

                    u.Translator(ref u);
                }
            }

            return len;
        }

        /// <summary>
        /// Set Disassembly mode
        /// </summary>
        /// <param name="u"></param>
        /// <param name="m"></param>
        public static void UdSetMode(ref Ud u, Byte m)
        {
            switch (m)
            {
                case 16:
                case 32:
                case 64:
                    u.DisMode = m;
                    return;
                default:
                    u.DisMode = 16;
                    return;
            }
        }

        /// <summary>
        /// Set vendor.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public static void UdSetVendor(ref Ud u, Int32 v)
        {
            switch (v)
            {
                case Decode.UdVendorIntel:
                    u.Vendor = Decode.UdVendorIntel;
                    break;
                case Decode.UdVendorAny:
                    u.Vendor = Decode.UdVendorAny;
                    break;
                default:
                    u.Vendor = Decode.UdVendorAmd;
                    break;
            }
        }

        /// <summary>
        /// Set code origin address
        /// </summary>
        /// <param name="u"></param>
        /// <param name="o"></param>
        public static void UdSetPc(ref Ud u, UInt64 o)
        {
            u.Pc = o;
        }

        /// <summary>
        /// Returns true if the given operand is of a segment register type.
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        public static bool UdOprIsSeg(UdOperand opr)
        {
            return opr.UdType == UdType.UD_OP_REG && opr.Base >= UdType.UD_R_ES && opr.Base <= UdType.UD_R_GS;
        }

        /// <summary>
        /// Looks up mnemonic code in the mnemonic string table.
        /// Returns NULL if the mnemonic code is invalid.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static String UdLookupMnemonic(UdMnemonicCode c)
        {
            if (c < UdMnemonicCode.UD_MAX_MNEMONIC_CODE)
            {
                return InstructionTables.ud_mnemonics_str[(Int32)c];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Set the buffer as input
        /// </summary>
        /// <param name="u"></param>
        /// <param name="buf">Pointer to memory to be read from</param>
        /// <param name="len">The maximum amount of memory to be read</param>
        public static unsafe void UdSetInputBuffer(ref Ud u, IntPtr buf, Int32 len)
        {
            UdInputInitialization(ref u);
            u.InputBuffer = (Byte*)buf.ToPointer();
            u.InputBufferSize = len;
            u.InputBufferIndex = 0;
        }

        /// <summary>
        /// Sets the output syntax
        /// </summary>
        /// <param name="u"></param>
        /// <param name="t"></param>
        private static void UdSetSyntax(ref Ud u, UdTranslatorDelegate t)
        {
            u.Translator = t;
        }

        /// <summary>
        /// returns the disassembled instruction
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static String UdInsnAsm(ref Ud u)
        {
            if (u.AsmBuffer == null || u.AsmBuffer.Length == 0)
            {
                return String.Empty;
            }

            Int32 count = Array.IndexOf<Char>(u.AsmBuffer, '\0', 0);
            if (count < 0)
            {
                count = u.AsmBuffer.Length;
            }

            Char[] c = new Char[count];
            Array.Copy(u.AsmBuffer, c, count);

            return new String(c);
        }

        /// <summary>
        /// Returns the offset.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static UInt64 UdInsnOff(ref Ud u)
        {
            return u.InstructionOffset;
        }

        /// <summary>
        /// Returns hex form of disassembled instruction.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static String UdInsnHex(ref Ud u)
        {
            StringBuilder sourceHex = new StringBuilder();

            if (u.Error == 0)
            {
                UInt32 i;
                IntPtr src_ptr = UdInsnPtr(ref u);

                unsafe
                {
                    Byte* src = (Byte*)src_ptr.ToPointer();
                    for (i = 0; i < UdInsnLen(ref u); i++)
                    {
                        sourceHex.AppendFormat("{0:2X", src[i]);
                    }
                }
            }

            return sourceHex.ToString();
        }

        /// <summary>
        /// Returns a pointer to buffer containing the bytes that were disassembled.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static unsafe IntPtr UdInsnPtr(ref Ud u)
        {
            return (u.InputBuffer == null) ? u.InputSessionPinner : new IntPtr(u.InputBuffer + u.InputBufferIndex - u.InputCtr);
        }

        /// <summary>
        /// Returns the count of bytes disassembled.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static Int32 UdInsnLen(ref Ud u)
        {
            return u.InputCtr;
        }

        /// <summary>
        /// Return the operand struct representing the nth operand of
        /// the currently disassembled instruction. Returns NULL if
        /// there's no such operand.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="n"></param>
        /// <param name="op"></param>
        private static void UdInsnOpr(ref Ud u, Int32 n, out UdOperand? op)
        {
            if (n > 3 || u.Operand[n].UdType == UdType.UD_NONE)
            {
                op = null;
            }
            else
            {
                op = u.Operand[n];
            }
        }

        /// <summary>
        /// Returns true if the given operand is of a general purpose
        /// register type.
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        private static Boolean UdOprIsGpr(ref UdOperand opr)
        {
            return opr.UdType == UdType.UD_OP_REG && opr.Base >= UdType.UD_R_AL && opr.Base <= UdType.UD_R_R15;
        }

        /// <summary>
        /// Get/set user opaqute data pointer
        /// </summary>
        /// <param name="u"></param>
        /// <param name="opaque"></param>
        private static void UdSetUserOpaqueData(ref Ud u, IntPtr opaque)
        {
            u.UserOpaqueData = opaque;
        }

        private static IntPtr UdGetUserOpaqueData(ref Ud u)
        {
            return u.UserOpaqueData;
        }

        /// <summary>
        /// Allow the user to set an assembler output buffer. If `buf` is NULL, we switch back to the internal buffer.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="buf"></param>
        /// <param name="size"></param>
        private static void UdSetAsmBuffer(ref Ud u, Char[] buf, Int32 size)
        {
            if (buf == null)
            {
                UdSetAsmBuffer(ref u, u.AsmBufferInt, u.AsmBufferInt.Length);
            }
            else
            {
                u.AsmBuffer = buf;
                u.AsmBufferSize = size;
            }
        }

        /// <summary>
        /// Set symbol resolver for relative targets used in the translation phase.
        /// The resolver is a function that takes a ulong address and returns a
        /// symbolic name for the that address.The function also takes a second
        /// argument pointing to an integer that the client can optionally set to a
        /// non-zero value for offsetted targets. (symbol+offset) The function may
        /// also return NULL, in which case the translator only prints the target
        /// address.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="resolver"></param>
        private static void UdSetSymResolver(ref Ud u, UdSymbolResolverDelegate resolver)
        {
            u.SymResolver = resolver;
        }

        /// <summary>
        /// Return the current instruction mnemonic.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static UdMnemonicCode UdInsnMnemonic(ref Ud u)
        {
            return u.Mnemonic;
        }

        /// <summary>
        /// Initializes the input system
        /// </summary>
        /// <param name="u"></param>
        private static void UdInputInitialization(ref Ud u)
        {
            u.InputHook = null;
            u.InputBuffer = null;
            u.InputBufferSize = 0;
            u.InputBufferIndex = 0;
            u.InputCur = 0;
            u.InputCtr = 0;
            u.InputEnd = 0;
            u.InputPeek = Decode.UdEoi;
        }

        /// <summary>
        /// Sets input hook
        /// </summary>
        /// <param name="u"></param>
        /// <param name="hook"></param>
        private static void UdSetInputHook(ref Ud u, UdInputCallback hook)
        {
            UdInputInitialization(ref u);
            u.InputHook = hook;
        }

        /// <summary>
        /// Set FILE as input
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static Int32 InputFileHook(ref Ud u)
        {
            return u.InputFile.ReadByte();
        }

        /// <summary>
        /// Set file as input for disassembly
        /// </summary>
        /// <param name="u"></param>
        /// <param name="file">File stream that will be read from. The stream must support reading.</param>
        private static void UdSetFileInput(ref Ud u, FileStream file)
        {
            UdInputInitialization(ref u);
            u.InputHook = InputFileHook;
            u.InputFile = file;
        }

        /// <summary>
        /// Skip n input bytes.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="n"></param>
        private static void UdInputSkip(ref Ud u, Int32 n)
        {
            if (u.InputEnd > 0)
            {
                return;
            }

            if (u.InputBuffer == null)
            {
                while (n-- > 0)
                {
                    Int32 c = u.InputHook(ref u);
                    if (c == Decode.UdEoi)
                    {
                        goto eoi;
                    }
                }

                return;
            }
            else
            {
                if (n > u.InputBufferSize ||
                    u.InputBufferIndex > u.InputBufferSize - n)
                {
                    u.InputBufferIndex = u.InputBufferSize;
                    goto eoi;
                }

                u.InputBufferIndex += n;
                return;
            }

        eoi:
            u.InputEnd = 1;
            u.Error = 1;
            u.ErrorMessage = "cannot skip, eoi received\b";
            return;
        }

        /// <summary>
        /// Returns non-zero on end-of-input
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private static Int32 UdInputEnd(ref Ud u)
        {
            return u.InputEnd;
        }
    }
    //// End class
}
//// End namespace