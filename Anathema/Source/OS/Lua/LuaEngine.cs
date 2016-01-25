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
                "var SavedCode = { }" + "\n" +
                "function OnActivate()" + "\n" +
                "   CheatA()" + "\n" +
                "end" + "\n" +

                "function CheatA()" + "\n" +
                "   var Entry = GetModuleAddress('" + ModuleName + "') + 0x" + ModuleOffset.ToString("X") + "\n" +
                "   AddKeyword('exit', GetReturnAddress(Entry))" + "\n" +
                "   var Assembly = ('[ASM]" + "\n" +
                "   ')" + "\n" +
                "   table.insert(SavedCode, CreateCodeCave(Entry, Assembly))" + "\n" +
                "   ClearKeywords()" + "\n" +
                "end" + "\n" +

                "function OnDeactivate()" + "\n" +
                "   RestoreCode(SavedCode);" + "\n" +
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