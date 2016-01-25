using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace Binarysharp.MemoryManagement
{
    public class LuaEngine : LuaFunctions
    {
        private Lua ScriptEngine;
        private MemorySharp MemoryEditor;

        public LuaEngine(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
            ScriptEngine = new Lua();
            BindFunctions();
        }

        public static String AddCodeInjectionTemplate(String Script, String ModuleName, UInt64 ModuleOffset)
        {
            String CodeInjection =
                "var SavedCode = { }" + "\n\n" + 
                "function OnActivate()" + "\n\n" +
                "\t" + "CheatA()" + "\n" +
                "end" + "\n\n" +

                "function CheatA()" + "\n\n" +
                "\t" + "var Entry = GetModuleAddress('" + ModuleName + "') + 0x" + ModuleOffset.ToString("X") + "\n" +
                "\t" + "AddKeyword('exit', GetReturnAddress(Entry))" + "\n" +
                "\t" + "var Assembly = ('" + "\n" +
                "\t\n" +
                "\t" + "')" + "\n" +
                "\ttable.insert(SavedCode, CreateCodeCave(Entry, Assembly))" + "\n" +
                "\tClearKeywords()" + "\n\n" +
                "end" + "\n\n" +

                "function OnDeactivate()" + "\n\n" +
                "\t" + "RestoreCode(SavedCode);" + "\n\n" +
                "end";

            return CodeInjection + Script;
        }

        private void BindFunctions()
        {
            // Disallow users to import libraries
            ScriptEngine.DoString(@" import = function () end ");
        }

        public Boolean RunActivationFunction(String Script)
        {
            try
            {
                LuaFunction Function = ScriptEngine["OnActivated"] as LuaFunction;
                ScriptEngine.DoString(Script);
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
                LuaFunction Function = ScriptEngine["OnDeactivated"] as LuaFunction;
                ScriptEngine.DoString(Script);
                Function.Call();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Function definitions

        UInt64 GetModuleAddress(String ModuleName)
        {
            return 0;
        }

        ulong LuaFunctions.GetModuleAddress(string ModuleName)
        {
            throw new NotImplementedException();
        }

        public ulong GetReturnAddress(ulong Address)
        {
            throw new NotImplementedException();
        }

        public byte[] CreateCodeCave()
        {
            throw new NotImplementedException();
        }

        public void RestoreCode()
        {
            throw new NotImplementedException();
        }

        #endregion

    } // End class

} // End namespace