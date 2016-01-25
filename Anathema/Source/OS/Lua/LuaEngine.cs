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

        private void BindFunctions()
        {
            // Disallow users to import libraries
            ScriptEngine.DoString(@" import = function () end ");
        }

        public void RunActivationFunction(String Script)
        {
            LuaFunction Function = ScriptEngine["OnActivated"] as LuaFunction;
            ScriptEngine.DoString(Script);
            Function.Call();
        }

    } // End class

} // End namespace