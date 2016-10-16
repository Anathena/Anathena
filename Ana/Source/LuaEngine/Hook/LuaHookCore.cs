﻿namespace Ana.Source.LuaEngine.Hook
{
    using Engine.SpeedManipulator;
    using System;

    internal class LuaHookCore : IHookCore
    {
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