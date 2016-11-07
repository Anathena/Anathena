namespace Ana.Source.LuaEngine
{
    using Graphics;
    using Hook;
    using Input;
    using Memory;
    using NLua;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class LuaCore
    {
        /// <summary>
        /// Time to wait for the update loop to finish on deactivation
        /// </summary>
        private const Int32 AbortTime = 500;

        /// <summary>
        /// Update time in milliseconds
        /// </summary>
        private const Int32 UpdateTime = 1000 / 15;

        public LuaCore()
        {
            this.InitializeLuaEngine();
        }

        public LuaCore(LuaScript luaScript) : base()
        {
            this.ScriptRaw = luaScript?.Script;
        }

        private Lua ScriptEngine { get; set; }

        private IMemoryCore LuaMemoryCore { get; set; }

        private IGraphicsCore LuaGraphicsCore { get; set; }

        private IHookCore LuaHookCore { get; set; }

        private IInputCore LuaInputCore { get; set; }

        /// <summary>
        /// Gets or sets a cancelation request for the update loop
        /// </summary>
        private CancellationTokenSource CancelRequest { get; set; }

        /// <summary>
        /// Gets or sets the task for the update loop
        /// </summary>
        private Task Task { get; set; }

        private String ScriptRaw { get; set; }

        /// <summary>
        /// Runs the activation function in the Lua script
        /// </summary>
        /// <returns></returns>
        public Boolean RunActivationFunction()
        {
            // Prevent issues that may come from internal LUA crashes (caused by malformed scripts) by reinitializing engine
            this.InitializeLuaEngine();

            try
            {
                this.ScriptEngine.DoString(this.ScriptRaw);
                LuaFunction function = this.ScriptEngine["OnActivate"] as LuaFunction;

                if (function == null)
                {
                    return false;
                }

                function.Call();

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
                if (this.ScriptEngine["OnUpdate"] as LuaFunction == null)
                {
                    return;
                }
            }
            catch
            {
            }

            DateTime previousTime = DateTime.Now;
            TimeSpan elapsedTime;

            this.CancelRequest = new CancellationTokenSource();

            try
            {
                this.Task = Task.Run(
                    async () =>
                {
                    LuaFunction Function = ScriptEngine["OnUpdate"] as LuaFunction;

                    while (true)
                    {
                        DateTime currentTime = DateTime.Now;
                        elapsedTime = currentTime - previousTime;

                        // Call the update function, giving the elapsed milliseconds since the previous call
                        Function.Call(elapsedTime.TotalMilliseconds);

                        previousTime = currentTime;

                        // Await with cancellation
                        await Task.Delay(LuaCore.UpdateTime, this.CancelRequest.Token);
                    }
                },
                    this.CancelRequest.Token);

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
        public void RunDeactivationFunction()
        {
            try
            {
                // Abort the update loop
                try
                {
                    this.CancelRequest?.Cancel();
                    this.Task?.Wait(LuaCore.AbortTime);
                }
                catch (Exception)
                {
                }

                if (this.ScriptEngine == null)
                {
                    return;
                }

                LuaFunction function = this.ScriptEngine["OnDeactivate"] as LuaFunction;
                function.Call();
                return;
            }
            catch
            {
                // On failure we do nothing, since we are deactivating the script anyways
            }
        }

        private void InitializeLuaEngine()
        {
            this.ScriptEngine = new Lua();
            this.LuaMemoryCore = new LuaMemoryCore();
            this.LuaGraphicsCore = new LuaGraphicsCore();
            this.LuaHookCore = new LuaHookCore();
            this.LuaInputCore = new LuaInputCore();

            this.BindFunctions();
        }

        private void BindFunctions()
        {
            // Disallow users to import libraries
            this.ScriptEngine.DoString(@" import = function () end ");

            // Bind the lua functions to a user accessible object
            this.ScriptEngine["Memory"] = this.LuaMemoryCore;
            this.ScriptEngine["Graphics"] = this.LuaGraphicsCore;
            this.ScriptEngine["Hook"] = this.LuaHookCore;
        }
    }
    //// End class
}
//// End namespace