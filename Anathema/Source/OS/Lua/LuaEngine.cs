using System;
using NLua;
using Binarysharp.MemoryManagement;

namespace Anathema
{
    public class LuaEngine
    {
        private Lua ScriptEngine;
        private LuaMemoryCore LuaMemoryCore;
        
        public LuaEngine()
        {
            LuaMemoryCore = new LuaMemoryCore();
            ScriptEngine = new Lua();
            BindFunctions();
        }

        public static String AddCodeInjectionTemplate(String Script, String ModuleName, UInt64 ModuleOffset)
        {
            String CodeInjection =
                "-- No Description" + "\n\n" +
                "function OnActivate()" + "\n\t\n" +
                "\t" + "CheatA()" + "\n\t\n" +
                "end" + "\n\n" +

                "function CheatA()" + "\n\n" +
                "\t" + "local Entry = Ana:GetModuleAddress(\"" + ModuleName + "\") + 0x" + ModuleOffset.ToString("X") + "\n" +
                "\t" + "Ana:SetKeyword(\"exit\", Ana:GetReturnAddress(Entry))" + "\n\n" +
                "\t" + "local Assembly = (" + "\n" +
                "\t[asm]" + "\n" +
                "\t\n" +
                "\tjmp exit" + "\n" +
                "\t" + "[/asm])" + "\n\n" +
                "\tAna:SaveMemory(Entry)" + "\n" +
                "\tAna:CreateCodeCave(Entry, Assembly)" + "\n" +
                "\tAna:ClearAllKeywords()" + "\n\n" +
                "end" + "\n\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "\t" + "Ana:RestoreAllMemory();" + "\n\t\n" +
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
            Script = ReplaceTags(Script);

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
            Script = ReplaceTags(Script);
            
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

        private String ReplaceTags(String Script)
        {
            // Removes the assembly tags and instead places a string body.
            // This is done such that the LUA frontend can do its syntax highlighting with minmal effort
            return Script.Replace("[asm]", "[[").Replace("[/asm]", "]]");
        }

    } // End class

} // End namespace