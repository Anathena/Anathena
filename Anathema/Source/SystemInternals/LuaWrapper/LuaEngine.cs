using Anathema.Source.SystemInternals.LuaWrapper.Graphics;
using Anathema.Source.SystemInternals.LuaWrapper.Hook;
using Anathema.Source.SystemInternals.LuaWrapper.Memory;
using NLua;
using System;

namespace Anathema.Source.SystemInternals.LuaWrapper
{
    public class LuaEngine
    {
        private Lua ScriptEngine;
        private IMemoryCore LuaMemoryCore;
        private IGraphicsCore LuaGraphicsCore;
        private IHookCore LuaHookCore;

        public LuaEngine()
        {
            InitializeLuaEngine();
        }

        private void InitializeLuaEngine()
        {
            ScriptEngine = new Lua();
            LuaMemoryCore = new LuaMemoryCore();
            LuaGraphicsCore = new LuaGraphicsCore();
            LuaHookCore = new LuaHookCore();

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
                "\t" + "local Entry = Memory:GetModuleAddress(\"" + ModuleName + "\") + 0x" + ModuleOffset.ToString("x") + "\n" +
                "\t" + "Memory:SetKeyword(\"exit\", Memory:GetCaveExitAddress(Entry))" + "\n\t\n" +

                "\t" + "local Assembly = (" + "\n" +
                "\t" + "[fasm]" + "\n" +
                "\t" + "\n" +
                "\t" + "jmp exit" + "\n" +
                "\t" + "[/fasm])" + "\n\t\n" +

                "\t" + "Memory:CreateCodeCave(Entry, Assembly)" + "\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "\t" + "Memory:ClearAllKeywords()" + "\n" +
                "\t" + "Memory:RemoveAllCodeCaves()" + "\n\t\n" +
                "end";

            return Script + CodeInjection;
        }

        public static String AddGraphicsOverlayTemplate(String Script)
        {
            String CodeInjection =
                "-- No Description" + "\n\t\n" +
                "function OnActivate()" + "\n\t\n" +
                "\t" + "OverlayA()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function OverlayA()" + "\n\t\n" +
                "\t" + "Graphics:Inject()" + "\n\t\n" +
                "end" + "\n\t\n" +

                "function OnDeactivate()" + "\n\t\n" +
                "end";

            return Script + CodeInjection;
        }

        private void BindFunctions()
        {
            // Disallow users to import libraries
            ScriptEngine.DoString(@" import = function () end ");

            // Bind the lua functions to a user accessible object
            ScriptEngine["Memory"] = LuaMemoryCore;
            ScriptEngine["Graphics"] = LuaGraphicsCore;
            ScriptEngine["Hook"] = LuaHookCore;
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