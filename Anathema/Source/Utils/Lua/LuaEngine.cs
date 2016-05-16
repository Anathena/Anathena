using NLua;
using System;

namespace Anathema.Utils.LUA
{
    public class LuaEngine
    {
        private Lua ScriptEngine;
        private LuaMemoryCore LuaMemoryCore;
        
        public LuaEngine()
        {
            InitializeLuaEngine();
        }

        private void InitializeLuaEngine()
        {
            ScriptEngine = new Lua();
            LuaMemoryCore = new LuaMemoryCore();

            BindFunctions();
            LuaMemoryCore.Initialize();
        }

        public static String AddCodeInjectionTemplate(String Script, String ModuleName, IntPtr ModuleOffset)
        {
            String CodeInjection =
                "-- No Description" + "\n\t\n" +
                "function OnActivate()" + "\n\t\n" +
                "\t" + "CheatA()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function CheatA()" + "\n\t\n" +
                "\t" + "local Entry = Ana:GetModuleAddress(\"" + ModuleName + "\") + 0x" + ModuleOffset.ToString("x") + "\n" +
                "\t" + "Ana:SetKeyword(\"exit\", Ana:GetCaveExitAddress(Entry))" + "\n\t\n" +

                "\t" + "local Assembly = (" + "\n" +
                "\t" + "[fasm]" + "\n" +
                "\t" + "\n" +
                "\t" + "jmp exit" + "\n" +
                "\t" + "[/fasm])" + "\n\t\n" +

                "\tAna:CreateCodeCave(Entry, Assembly)" + "\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "\t" + "Ana:ClearAllKeywords()" + "\n" +
                "\t" + "Ana:RemoveAllCodeCaves()" + "\n\t\n" +
                "end";

            return Script + CodeInjection;
        }

        private void BindFunctions()
        {
            // Disallow users to import libraries
            ScriptEngine.DoString(@" import = function () end ");
            ScriptEngine["Ana"] = LuaMemoryCore;
        }

        public Boolean RunActivationFunction(String Script)
        {
            // Prevent issues that may come from internal LUA crashes (caused by malformed scripts) by reinitializing engine
            InitializeLuaEngine();

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
            //Script = Regex.Replace(Script, "\\[fasm\\]", "[[", RegexOptions.IgnoreCase);
            //Script = Regex.Replace(Script, "\\[/fasm\\]", "]]", RegexOptions.IgnoreCase);

            Script = Script.Replace("[fasm]", "[[");
            Script = Script.Replace("[/fasm]", "]]");

            return Script;
        }

    } // End class

} // End namespace