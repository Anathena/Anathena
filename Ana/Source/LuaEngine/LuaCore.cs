using Ana.Source.LuaEngine.Graphics;
using Ana.Source.LuaEngine.Hook;
using Ana.Source.LuaEngine.Input;
using Ana.Source.LuaEngine.Memory;
using NLua;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ana.Source.LuaEngine
{
    internal class LuaCore
    {
        private const Int32 AbortTime = 500;    // Time to wait for the update loop to finish on deactivation
        private const Int32 UpdateTime = 65;    // Update called ~15 times per second

        private Lua ScriptEngine;
        private IMemoryCore LuaMemoryCore;
        private IGraphicsCore LuaGraphicsCore;
        private IHookCore LuaHookCore;
        private IInputCore LuaInputCore;

        private CancellationTokenSource CancelRequest;  // Tells the task to finish
        private Task Task;                              // Event that constantly checks the target process for changes

        private String LuaScriptRaw;

        public LuaCore()
        {
            InitializeLuaEngine();
        }

        public LuaCore(LuaScript LuaScript) : base()
        {
            this.LuaScriptRaw = ReplaceTags(LuaScript?.Script);
        }

        private void InitializeLuaEngine()
        {
            ScriptEngine = new Lua();
            LuaMemoryCore = new LuaMemoryCore();
            LuaGraphicsCore = new LuaGraphicsCore();
            LuaHookCore = new LuaHookCore();
            LuaInputCore = new LuaInputCore();

            BindFunctions();
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

        /// <summary>
        /// Runs the activation function in the Lua script
        /// </summary>
        /// <returns></returns>
        public Boolean RunActivationFunction()
        {
            // Prevent issues that may come from internal LUA crashes (caused by malformed scripts) by reinitializing engine
            InitializeLuaEngine();

            try
            {
                ScriptEngine.DoString(LuaScriptRaw);
                LuaFunction Function = ScriptEngine["OnActivate"] as LuaFunction;

                if (Function == null)
                    return false;

                Function.Call();

                // Indicate successful activation
                return true;
            }
            catch
            {
                // Indicate failed activation
                return false;
            }
        }

        /// <summary>
        /// Continously runs the update function in the Lua script
        /// </summary>
        public void RunUpdateFunction()
        {
            try
            {
                if (ScriptEngine["OnUpdate"] as LuaFunction == null)
                    return;
            }
            catch { }

            DateTime PreviousTime = DateTime.Now;
            TimeSpan ElapsedTime;

            CancelRequest = new CancellationTokenSource();

            try
            {
                Task = Task.Run(async () =>
                {
                    LuaFunction Function = ScriptEngine["OnUpdate"] as LuaFunction;

                    while (true)
                    {
                        DateTime CurrentTime = DateTime.Now;
                        ElapsedTime = CurrentTime - PreviousTime;

                        // Call the update function, giving the elapsed milliseconds since the previous call
                        Function.Call(ElapsedTime.TotalMilliseconds);

                        PreviousTime = CurrentTime;

                        // Await with cancellation
                        await Task.Delay(UpdateTime, CancelRequest.Token);
                    }

                }, CancelRequest.Token);

                return;
            }
            catch
            {
                // On failure we do nothing, since the OnUpdate function is optional
            }
        }

        /// <summary>
        /// Runs the deactivation function in the Lua script
        /// </summary>
        /// <param name="Script"></param>
        /// <returns></returns>
        public void RunDeactivationFunction()
        {
            try
            {
                // Abort the update loop
                try
                {
                    CancelRequest?.Cancel();
                    Task?.Wait(AbortTime);
                }
                catch (Exception) { }

                LuaFunction Function = ScriptEngine["OnDeactivate"] as LuaFunction;
                Function.Call();
                return;
            }
            catch
            {
                // On failure we do nothing, since we are deactivating the script anyways
            }
        }

        private String ReplaceTags(String Script)
        {
            if (Script == null)
                return String.Empty;

            // Removes the assembly tags and instead places a string body.
            // This is done such that the LUA frontend can do its syntax highlighting with minmal effort
            // Script = Regex.Replace(Script, "\\[fasm\\]", "[[", RegexOptions.IgnoreCase);
            // Script = Regex.Replace(Script, "\\[/fasm\\]", "]]", RegexOptions.IgnoreCase);

            Script = Script.Replace("[fasm]", "[[");
            Script = Script.Replace("[/fasm]", "]]");

            return Script;
        }

    } // End class

} // End namespace