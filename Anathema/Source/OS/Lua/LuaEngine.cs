using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace Binarysharp.MemoryManagement
{
    public class LuaEngine
    {
        private Lua ScriptEngine;
        private LuaMemoryCore LuaMemoryCore;
        private MemorySharp MemoryEditor;

        private static String _LuaKeywords = "and break do else elseif end false for function if" +
                " in local nil not or repeat return then true until while" +
                " _VERSION assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " print rawget rawset require tonumber tostring type unpack" +
                " assert collectgarbage dofile error gcinfo loadfile loadstring" +
                " print rawget rawset require tonumber tostring type unpack" +
                " abs acos asin atan atan2 ceil cos deg exp" +
                " floor format frexp gsub ldexp log log10 max min mod rad random randomseed" +
                " sin sqrt strbyte strchar strfind strlen strlower strrep strsub strupper tan" +
                " string.byte string.char string.dump string.find string.len";
        public static String LuaKeywords { get { SortKeywords(); return _LuaKeywords; } private set { _LuaKeywords = value; } }

        private static String _AsmKeywords = "rax rbx rcx rdx rbp rsi rdi rsp r8 r9 r10 r11 r12 r13 r14 r15 rip" +
                " eax ebx ecx edx ebp esi edi esp r8d r9d r10d r11d r12d r13d r14d r15d eip" +
                " ax bx cx dx bp si di sp r8w r9w r10w r11w r12w r13w r14w r15w" +
                " al bl cl dl bpl sil dil spl r8b r9b r10b r11b r12b r13b r14b r15b" +
                " ah bh ch dh" +
                " fpr0 fpr1 fpr2 fpr3 fpr4 fpr5 fpr6 fpr7 mmx0 mmx1 mmx2 mmx3 mmx4 mmx5 mmx6 mmx7" +
                " xmm0 xmm1 xmm2 xmm3 xmm4 xmm5 xmm6 xmm7 xmm8 xmm9 xmm10 xmm11 xmm12 xmm13 xmm14 xmm15";
        public static String AsmKeywords { get { SortKeywords(); return _AsmKeywords; } private set { _AsmKeywords = value; } }

        private static String _AnathemaKeywords = "Ana";
        public static String AnathemaKeywords { get { SortKeywords(); return _AnathemaKeywords; } private set { _AnathemaKeywords = value; } }

        private static Boolean Sorted = false;
        private static void SortKeywords()
        {
            if (Sorted)
                return;

            List<String> Keywords;
            String SortedKeywords;

            SortedKeywords = String.Empty;
            Keywords = new List<String>(_LuaKeywords.Split(' '));
            Keywords.Sort();
            Keywords.ForEach(x => SortedKeywords += x + " ");
            LuaEngine._LuaKeywords = SortedKeywords;

            SortedKeywords = String.Empty;
            Keywords = new List<String>(_AsmKeywords.Split(' '));
            Keywords.Sort();
            Keywords.ForEach(x => SortedKeywords += x + " ");
            LuaEngine._AsmKeywords = SortedKeywords;

            List<String> FunctionKeywords = new List<String>();
            typeof(LuaFunctions).GetMethods().ToList().ForEach(x => FunctionKeywords.Add(x.Name));
            _AnathemaKeywords = String.Join(" ", _AnathemaKeywords.Split(' ').Concat(FunctionKeywords.ToArray()));
            SortedKeywords = String.Empty;
            Keywords = new List<String>(_AnathemaKeywords.Split(' '));
            Keywords.Sort();
            Keywords.ForEach(x => SortedKeywords += x + " ");
            LuaEngine._AnathemaKeywords = SortedKeywords;

            Sorted = true;
        }

        public LuaEngine(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
            LuaMemoryCore = new LuaMemoryCore(MemoryEditor);
            ScriptEngine = new Lua();
            BindFunctions();
        }

        public static String AddCodeInjectionTemplate(String Script, String ModuleName, UInt64 ModuleOffset)
        {
            String CodeInjection =
                "function OnActivate()" + "\n\n" +
                "\t" + "CheatA()" + "\n\n" +
                "end" + "\n\n" +

                "function CheatA()" + "\n\n" +
                "\t" + "local Entry = Ana:GetModuleAddress('" + ModuleName + "') + 0x" + ModuleOffset.ToString("X") + "\n" +
                "\t" + "Ana:AddKeyword('exit', Ana:GetReturnAddress(Entry))" + "\n" +
                "\t" + "local Assembly = ([[" + "\n" +
                "\t\n" +
                "\t" + "]])" + "\n" +
                "\tAna:SaveCode(Entry)" + "\n" +
                "\tAna:CreateCodeCave(Entry, Assembly)" + "\n" +
                "\tAna:ClearKeywords()" + "\n\n" +
                "end" + "\n\n" +

                "function OnDeactivate()" + "\n\n" +
                "\t" + "Ana:RestoreCode();" + "\n\n" +
                "end";

            return CodeInjection + Script;
        }

        private void BindFunctions()
        {
            // Disallow users to import libraries
            ScriptEngine.DoString(@" import = function () end ");
            ScriptEngine["Ana"] = LuaMemoryCore;
        }

        public Boolean RunActivationFunction(String Script)
        {
            try
            {
                ScriptEngine.DoString(Script);
                LuaFunction Function = ScriptEngine["OnActivate"] as LuaFunction;
                Function.Call();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean RunDeactivationFunction(String Script)
        {
            try
            {
                ScriptEngine.DoString(Script);
                LuaFunction Function = ScriptEngine["OnDeactivate"] as LuaFunction;
                Function.Call();
                return true;
            }
            catch
            {
                return false;
            }
        }

    } // End class

} // End namespace