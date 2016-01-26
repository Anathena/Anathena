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
                "\t" + "var Entry = Ana:GetModuleAddress('" + ModuleName + "') + 0x" + ModuleOffset.ToString("X") + "\n" +
                "\t" + "AddKeyword('exit', Ana:GetReturnAddress(Entry))" + "\n" +
                "\t" + "var Assembly = ([[" + "\n" +
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
            //ScriptEngine.DoString(@" import = function () end ");
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