namespace Ana.Source.LuaEngine.Hook
{
    using Engine.SpeedManipulator;
    using System;

    /// <summary>
    /// Provides access to environment manipulations in a hooked process for Lua scripts
    /// </summary>
    internal class LuaHookCore : IHookCore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LuaHookCore" /> class
        /// </summary>
        public LuaHookCore()
        {
            this.AccessLock = new Object();
        }

        private Object AccessLock { get; set; }

        public void SetSpeed(Double speed)
        {
        }

        public void SetRandomSeed(UInt32 seed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gives access to the speed hook in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private ISpeedManipulator GetSpeedHackInterface()
        {
            return null;
        }
    }
    //// End class
}
//// End namespace