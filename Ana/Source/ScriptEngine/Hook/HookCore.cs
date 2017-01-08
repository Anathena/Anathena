namespace Ana.Source.ScriptEngine.Hook
{
    using Source.Engine.SpeedManipulator;
    using System;

    /// <summary>
    /// Provides access to environment manipulations in a hooked process for scripts.
    /// </summary>
    internal class HookCore : IHookCore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HookCore" /> class.
        /// </summary>
        public HookCore()
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
        /// Gives access to the speed hook in the target process, injecting the hooks if needed.
        /// </summary>
        /// <returns>An interface providing access to the speed hook.</returns>
        private ISpeedManipulator GetSpeedHackInterface()
        {
            return null;
        }
    }
    //// End class
}
//// End namespace